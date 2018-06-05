using System;
using System.Threading.Tasks;
using Containo.Services.Orders.Storage.Caching.Interfaces;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Containo.Services.Orders.Storage.Caching
{
    public class RedisCache : ICache
    {
        /// <summary>
        ///     Retrieves a cached value
        /// </summary>
        /// <param name="cacheKey">Key to uniquely identify the entry</param>
        /// <returns>Cached value</returns>
        public async Task<TCacheItem> GetAsync<TCacheItem>(string cacheKey) where TCacheItem : class
        {
            var redisDb = GetRedisDb();
            var rawCachedValue = await redisDb.StringGetAsync(cacheKey);
            if (string.IsNullOrWhiteSpace(rawCachedValue))
            {
                return null;
            }

            var cacheItem = JsonConvert.DeserializeObject<TCacheItem>(rawCachedValue);
            return cacheItem;
        }

        /// <summary>
        ///     Sets a value in the cache
        /// </summary>
        /// <param name="cacheKey">Key to uniquely identify the entry</param>
        /// <param name="cacheItem">Value to cache</param>
        public async Task SetAsync<TCacheItem>(string cacheKey, TCacheItem cacheItem) where TCacheItem : class
        {
            var redisDb = GetRedisDb();
            var rawCacheItem = JsonConvert.SerializeObject(cacheItem);
            await redisDb.StringSetAsync(cacheKey, rawCacheItem);
        }

        private static IDatabase GetRedisDb()
        {
            var connectionString = Environment.GetEnvironmentVariable("Redis_ConnectionString");
            var redisClient = ConnectionMultiplexer.Connect(connectionString);
            var redisDb = redisClient.GetDatabase();
            return redisDb;
        }
    }
}