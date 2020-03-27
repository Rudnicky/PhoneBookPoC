using Microsoft.Extensions.Caching.Memory;

namespace PhoneBookPoC.Services
{
    public class MemoryCacheWrapper : IMemoryCacheWrapper
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheWrapper()
        {
            _memoryCache = new MemoryCache(new MemoryCacheOptions() { });
        }

        public T Get<T>(string key)
        {
            if (_memoryCache.TryGetValue(key, out T value))
            {
                return value;

            }
            else
            {
                return default;
            }
        }

        public void Set<T>(string key, T value)
        {
            _memoryCache.Set(key, value);
        }

        public void Clear(object key)
        {
            _memoryCache.Remove(key);
        }
    }
}
