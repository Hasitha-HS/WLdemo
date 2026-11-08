namespace WLdemo.Services.Interfaces
{
    public interface IMemoryCacheService
    {
        T GetOrCreate<T>(string cacheKey, Func<T> factory, TimeSpan? expiration = null);
        Task<T> GetOrCreateAsync<T>(string cacheKey, Func<Task<T>> factory, TimeSpan? expiration = null);
        void Remove(string cacheKey);
    }
}
