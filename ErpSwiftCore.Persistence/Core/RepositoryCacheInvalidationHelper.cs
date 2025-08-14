

using ErpSwiftCore.SharedKernel.Base;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ErpSwiftCore.Persistence.Core.Common
{
    /// <summary>
    /// Provides fine‑grained cache operations with tag‑based invalidation.
    /// Stores each entry under a key and records that key in one or more Redis sets (tags).
    /// Invalidating a tag removes all associated keys.
    /// </summary>
    public class TaggedCacheService<T> where T : BaseEntity
    {
        private readonly IDatabase _redis;
        private readonly IDistributedCache _cache;
        private readonly ILogger _logger;
        private static readonly JsonSerializerOptions _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            ReferenceHandler =  ReferenceHandler.IgnoreCycles
        };

        public TaggedCacheService(
            IConnectionMultiplexer multiplexer,
            IDistributedCache cache,
            ILogger<TaggedCacheService<T>> logger)
        {
            if (multiplexer == null) throw new ArgumentNullException(nameof(multiplexer));
            _redis = multiplexer.GetDatabase();
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Stores an object in the cache under <paramref name="key"/>, and records
        /// that key in each <paramref name="tags"/> set for later invalidation.
        /// </summary>
        public async Task SetAsync<TResult>(
            string key,
            TResult value,
            DistributedCacheEntryOptions options,
            IEnumerable<string> tags,
            CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentException(nameof(key));
            if (tags == null || !tags.Any()) throw new ArgumentException("At least one tag required", nameof(tags));

            // 1) Serialize and set cache
            var bytes = JsonSerializer.SerializeToUtf8Bytes(value, _serializerOptions);
            await _cache.SetAsync(key, bytes, options, ct).ConfigureAwait(false);

            // 2) Record key under each tag set
            var tasks = new List<Task>();
            foreach (var tag in tags)
            {
                if (string.IsNullOrWhiteSpace(tag)) continue;
                var setKey = GetTagSetKey(tag);
                tasks.Add(_redis.SetAddAsync(setKey, key));
                // optional: set TTL on the tag set equal to the longest entry expiration
                if (options.AbsoluteExpirationRelativeToNow.HasValue)
                    await _redis.KeyExpireAsync(setKey, options.AbsoluteExpirationRelativeToNow).ConfigureAwait(false);
            }
            await Task.WhenAll(tasks).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieves a cached object by <paramref name="key"/>, or fetches via <paramref name="fetcher"/>
        /// and stores it under provided <paramref name="tags"/>.
        /// </summary>
        public async Task<TResult> GetOrSetAsync<TResult>(
            string key,
            Func<CancellationToken, Task<TResult>> fetcher,
            DistributedCacheEntryOptions options,
            IEnumerable<string> tags,
            CancellationToken ct = default)
        {
            // Try fetch from cache
            byte[] blob = null;
            try { blob = await _cache.GetAsync(key, ct).ConfigureAwait(false); }
            catch (Exception ex) { _logger.LogDebug(ex, "Cache read failed for {Key}", key); }

            if (blob != null)
            {
                try { return JsonSerializer.Deserialize<TResult>(blob, _serializerOptions)!; }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Deserialization failed for {Key}", key);
                }
            }

            // Fetch from source
            var result = await fetcher(ct).ConfigureAwait(false);

            // Store with tags
            try
            {
                await SetAsync(key, result, options, tags, ct).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Cache set failed for {Key}", key);
            }

            return result;
        }

        /// <summary>
        /// Invalidate all entries associated with any of the given <paramref name="tags"/>.
        /// </summary>
        public async Task InvalidateTagsAsync(IEnumerable<string> tags)
        {
            if (tags == null) throw new ArgumentNullException(nameof(tags));

            var toDelete = new HashSet<RedisValue>();
            var tasks = new List<Task<RedisValue[]>>();

            // 1) Gather all keys from each tag set
            foreach (var tag in tags.Where(t => !string.IsNullOrWhiteSpace(t)))
            {
                var setKey = GetTagSetKey(tag);
                tasks.Add(_redis.SetMembersAsync(setKey));
            }
            var results = await Task.WhenAll(tasks).ConfigureAwait(false);
            foreach (var members in results)
                foreach (var member in members)
                    toDelete.Add(member);

            if (!toDelete.Any()) return;

            // 2) Delete each cache key
            var deleteTasks = toDelete.Select(key => _cache.RemoveAsync(key)).ToArray();
            await Task.WhenAll(deleteTasks).ConfigureAwait(false);

            // 3) Remove tag sets
            var removeSetTasks = tags
                .Where(t => !string.IsNullOrWhiteSpace(t))
                .Select(t => _redis.KeyDeleteAsync(GetTagSetKey(t)))
                .ToArray();
            await Task.WhenAll(removeSetTasks).ConfigureAwait(false);
        }

        private static string GetTagSetKey(string tag)
            => $"CacheTag:{typeof(T).FullName}:{tag}";
    }
}
