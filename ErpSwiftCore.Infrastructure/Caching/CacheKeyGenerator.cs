using System;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Collections.Generic;
using Microsoft.Extensions.ObjectPool;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ErpSwiftCore.SharedKernel.Base;

namespace ErpSwiftCore.Infrastructure.Caching
{
    /// <summary>
    /// Custom contract resolver that orders JSON properties alphabetically for deterministic serialization.
    /// </summary>
    public class AlphabeticalContractResolver : DefaultContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            return base.CreateProperties(type, memberSerialization)
                       .OrderBy(p => p.PropertyName, StringComparer.OrdinalIgnoreCase)
                       .ToList();
        }
    }

    /// <summary>
    /// Comprehensive cache key generator:
    /// - Builds a sorted, deterministic JSON payload for complex objects using Json.NET with alphabetical ordering.
    /// - Concatenates core parameters into a single string.
    /// - Optionally signs the raw key with HMAC-SHA256.
    /// - Computes SHA-256 hash to produce a fixed-length hex key.
    /// Thread-safe and optimized for high-throughput scenarios.
    /// </summary>
    public class CacheKeyGenerator : ICacheKeyGenerator, IDisposable
    {
        private readonly ThreadLocal<SHA256> _shaPool;
        private readonly ObjectPool<StringBuilder> _sbPool;
        private readonly CacheSettings _settings;
        private readonly JsonSerializer _jsonSerializer;

        public CacheKeyGenerator(
            IOptions<CacheSettings> settings,
            ObjectPoolProvider poolProvider)
        {
            _settings = settings.Value;

            // Thread-local SHA256 to avoid repeated instantiation and ensure thread-safety
            _shaPool = new ThreadLocal<SHA256>(() => SHA256.Create());

            // Pool StringBuilder instances to reduce GC pressure
            _sbPool = poolProvider.CreateStringBuilderPool();

            // Configure Newtonsoft.Json serializer for deterministic property ordering
            var contractResolver = new AlphabeticalContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };
            _jsonSerializer = new JsonSerializer
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.None
            };
            _jsonSerializer.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
        }

        public string GenerateKey<T>(
            string? tenantId,
            string method,
            Guid? id = null,
            object? filter = null,
            object? orderBy = null,
            bool? ascending = null,
            params string[] includes)
            where T : BaseEntity
        {
            var sb = _sbPool.Get();
            try
            {
                sb.Clear();
                sb.Append(typeof(T).FullName).Append("|")
                  .Append(method).Append("|");

                if (!string.IsNullOrWhiteSpace(tenantId))
                    sb.Append("tenant=").Append(tenantId).Append("|");

                if (id.HasValue)
                    sb.Append("id=").Append(id.Value.ToString("N")).Append("|");

                if (filter != null)
                    sb.Append("filter=").Append(Serialize(filter)).Append("|");

                if (orderBy != null)
                {
                    sb.Append("order=")
                      .Append(Serialize(orderBy))
                      .Append("|")
                      .Append("dir=")
                      .Append(ascending == true ? "asc" : "desc")
                      .Append("|");
                }

                if (includes?.Any() == true)
                {
                    var sorted = includes
                        .Where(i => !string.IsNullOrWhiteSpace(i))
                        .Select(i => i.Trim())
                        .OrderBy(i => i, StringComparer.OrdinalIgnoreCase);
                    sb.Append("includes=[")
                      .Append(string.Join(",", sorted))
                      .Append("]|");
                }

                // Raw key string
                var rawKey = sb.ToString();

                // Compute SHA-256
                var sha = _shaPool.Value!;
                var hashBytes = sha.ComputeHash(Encoding.UTF8.GetBytes(rawKey));

                // Optionally apply HMAC
                if (!string.IsNullOrWhiteSpace(_settings.HmacKey))
                {
                    using var hmac = new HMACSHA256(Convert.FromBase64String(_settings.HmacKey));
                    hashBytes = hmac.ComputeHash(hashBytes);
                }

                return Convert.ToHexString(hashBytes);
            }
            finally
            {
                _sbPool.Return(sb);
            }
        }

        private string Serialize(object obj)
        {
            using var writer = new StringWriter();
            _jsonSerializer.Serialize(writer, obj);
            return writer.ToString();
        }

        public void Dispose()
        {
            foreach (var sha in _shaPool.Values)
            {
                sha.Dispose();
            }
            _shaPool.Dispose();
        }
    }

    /// <summary>
    /// Cache settings bound from configuration (appsettings.json).
    /// </summary>
    public class CacheSettings
    {
        /// <summary>
        /// Base64-encoded key for optional HMAC signing of cache keys.
        /// </summary>
        public string HmacKey { get; set; } = string.Empty;
    }
}