using Microsoft.Extensions.Caching.Memory;
using WLdemo.Services.Interfaces;

namespace WLdemo.Services
{
    public class MemoryCacheService : IMemoryCacheService
    {
        private readonly IMemoryCache _cache;

        public MemoryCacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public T GetOrCreate<T>(string cacheKey, Func<T> factory, TimeSpan? expiration = null)
        {
            if (!_cache.TryGetValue(cacheKey, out T? result))
            {
                result = factory();
                var cacheEntryOptions = new MemoryCacheEntryOptions();

                if (expiration.HasValue)
                {
                    cacheEntryOptions.SetAbsoluteExpiration(expiration.Value);
                }

                _cache.Set(cacheKey, result, cacheEntryOptions);
            }

            return result!;
        }

        public async Task<T> GetOrCreateAsync<T>(string cacheKey, Func<Task<T>> factory, TimeSpan? expiration = null)
        {
            if (!_cache.TryGetValue(cacheKey, out T? result))
            {
                result = await factory();
                var cacheEntryOptions = new MemoryCacheEntryOptions();

                if (expiration.HasValue)
                {
                    cacheEntryOptions.SetAbsoluteExpiration(expiration.Value);
                }

                _cache.Set(cacheKey, result, cacheEntryOptions);
            }

            return result!;
        }

        public void Remove(string cacheKey)
        {
            _cache.Remove(cacheKey);
        }
    }
}
