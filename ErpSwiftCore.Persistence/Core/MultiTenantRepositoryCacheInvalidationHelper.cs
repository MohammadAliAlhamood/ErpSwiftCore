

using ErpSwiftCore.SharedKernel.Base;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ErpSwiftCore.Persistence.Core.Common
{
    /// <summary>
    /// Tag‑based caching service for multi‑tenant scenarios.
    /// Each entry is stored under a fully qualified key (including tenant),
    /// and that key is recorded in per‑tenant tag sets for precise invalidation.
    /// </summary>
    public class MultiTenantTaggedCacheService<T> where T : BaseEntity
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

        public MultiTenantTaggedCacheService(
            IConnectionMultiplexer multiplexer,
            IDistributedCache cache,
            ILogger<MultiTenantTaggedCacheService<T>> logger)
        {
            _redis = multiplexer?.GetDatabase() ?? throw new ArgumentNullException(nameof(multiplexer));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Builds the full cache key including tenant and entity type.
        /// </summary>
        private string BuildKey(string tenantId, string key)
            => $"{tenantId}:{typeof(T).Name}:{key}";

        /// <summary>
        /// Builds the Redis set name for a tag under a specific tenant and type.
        /// </summary>
        private string BuildTagSet(string tenantId, string tag)
            => $"{tenantId}:{typeof(T).Name}:Tag:{tag}";

        /// <summary>
        /// Stores value under full key, and registers that key in each tenant‑scoped tag set.
        /// </summary>
        public async Task SetAsync<TResult>(
            string tenantId,
            string key,
            TResult value,
            DistributedCacheEntryOptions options,
            IEnumerable<string> tags,
            CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(tenantId)) throw new ArgumentException(nameof(tenantId));
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentException(nameof(key));
            if (tags == null || !tags.Any()) throw new ArgumentException("At least one tag required");

            string fullKey = BuildKey(tenantId, key);
            byte[] bytes = JsonSerializer.SerializeToUtf8Bytes(value, _serializerOptions);

            // 1) store in cache
            await _cache.SetAsync(fullKey, bytes, options, ct).ConfigureAwait(false);

            // 2) add key to each tag set
            var tasks = new List<Task>();
            foreach (var tag in tags.Where(t => !string.IsNullOrWhiteSpace(t)))
            {
                string setName = BuildTagSet(tenantId, tag);
                tasks.Add(_redis.SetAddAsync(setName, fullKey));

                // ensure the tag set expires at least as long as entries
                if (options.AbsoluteExpirationRelativeToNow.HasValue)
                    await _redis.KeyExpireAsync(setName, options.AbsoluteExpirationRelativeToNow).ConfigureAwait(false);
            }

            await Task.WhenAll(tasks).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieves from cache or fetches via fetcher, then stores under tags.
        /// </summary>
        public async Task<TResult> GetOrSetAsync<TResult>(
            string tenantId,
            string key,
            Func<CancellationToken, Task<TResult>> fetcher,
            DistributedCacheEntryOptions options,
            IEnumerable<string> tags,
            CancellationToken ct = default)
        {
            string fullKey = BuildKey(tenantId, key);
            byte[] blob = null;
            try { blob = await _cache.GetAsync(fullKey, ct).ConfigureAwait(false); }
            catch (Exception ex) { _logger.LogDebug(ex, "Cache read failed for {FullKey}", fullKey); }

            if (blob != null)
            {
                try { return JsonSerializer.Deserialize<TResult>(blob, _serializerOptions)!; }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Deserialization failed for {FullKey}", fullKey);
                }
            }

            // fetch from source
            var result = await fetcher(ct).ConfigureAwait(false);

            // store tagged
            try
            {
                await SetAsync(tenantId, key, result, options, tags, ct).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Cache set failed for {FullKey}", fullKey);
            }

            return result;
        }

        /// <summary>
        /// Invalidates all cache entries recorded under the given tags for this tenant & type.
        /// </summary>
        public async Task InvalidateTagsAsync(
            string tenantId,
            IEnumerable<string> tags)
        {
            if (string.IsNullOrWhiteSpace(tenantId)) throw new ArgumentException(nameof(tenantId));
            if (tags == null) throw new ArgumentNullException(nameof(tags));

            var allKeys = new HashSet<RedisValue>();
            var fetchTasks = new List<Task<RedisValue[]>>();

            // 1) gather keys per tag set
            foreach (var tag in tags.Where(t => !string.IsNullOrWhiteSpace(t)))
            {
                string setName = BuildTagSet(tenantId, tag);
                fetchTasks.Add(_redis.SetMembersAsync(setName));
            }
            var results = await Task.WhenAll(fetchTasks).ConfigureAwait(false);
            foreach (var members in results)
                foreach (var key in members)
                    allKeys.Add(key);

            if (!allKeys.Any()) return;

            // 2) remove each cache entry
            var delTasks = allKeys.Select(k => _cache.RemoveAsync((string)k)).ToArray();
            await Task.WhenAll(delTasks).ConfigureAwait(false);

            // 3) delete tag sets
            var rmSets = tags
                .Where(t => !string.IsNullOrWhiteSpace(t))
                .Select(t => BuildTagSet(tenantId, t))
                .Select(setName => _redis.KeyDeleteAsync(setName))
                .ToArray();
            await Task.WhenAll(rmSets).ConfigureAwait(false);
        }
    }
}
