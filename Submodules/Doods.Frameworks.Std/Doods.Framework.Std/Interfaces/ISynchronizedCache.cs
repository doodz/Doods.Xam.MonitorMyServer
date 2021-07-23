namespace Doods.Framework.Std
{
    public interface ISynchronizedCache<T>
    {
        int Count { get; }
        string TryGetKey(T value);
        T Read(string key);
        void Add(string key, T value);
        bool AddWithTimeout(string key, T value, int timeout);
        SynchronizedCache<T>.AddOrUpdateStatus AddOrUpdate(string key, T value);
        void Delete(string key);
    }
}