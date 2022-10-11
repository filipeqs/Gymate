using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace Exercises.Infrastructure.Repositories.Cache
{
    public class CachedRepositoryDecorator<T> : ICachedRepositoryDecorator<T> where T : class
    {
        private readonly IDistributedCache _redisCache;

        public CachedRepositoryDecorator(IDistributedCache redisCache)
        {
            _redisCache = redisCache;
        }

        public async Task<T?> GetCachedValueAsync(string key)
        {
            var value = await _redisCache.GetStringAsync(key);
            if (string.IsNullOrEmpty(value))
                return default;

            return JsonSerializer.Deserialize<T>(value);
        }
        public async Task UpdateCachedValueAsync(string key, T value)
        {
            await _redisCache.SetStringAsync(key, JsonSerializer.Serialize(value));
        }

        public async Task DeleteCachedValueAsync(string key)
        {
            await _redisCache.RemoveAsync(key);
        }
    }
}
