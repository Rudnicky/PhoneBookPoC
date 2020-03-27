namespace PhoneBookPoC.Services
{
    public interface IMemoryCacheWrapper
    {
        T Get<T>(string key);

        void Set<T>(string key, T value);

        void Clear(object key);
    }
}
