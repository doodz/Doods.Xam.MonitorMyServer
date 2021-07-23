using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Doods.Framework.Std
{
    public class CacheItem<T>
    {
        public CacheItem(T value) : this(value, TimeSpan.FromMinutes(10))
        {
        }

        public CacheItem(T value, TimeSpan expiresAfter)
        {
            Value = value;
            ExpiresAfter = expiresAfter;
        }

        public T Value { get; }
        internal DateTimeOffset Created { get; } = DateTimeOffset.Now;
        internal TimeSpan ExpiresAfter { get; }
        public bool IsValid => !(DateTimeOffset.Now - Created >= ExpiresAfter);
    }

    public class SynchronizedCacheItemExpire<T> : SynchronizedCache<CacheItem<T>>
    {
        public string TryGetKey(T value)
        {
            _cacheLock.EnterReadLock();
            try
            {
                //TODO check null values !
                var result = _innerCache.FirstOrDefault(c => c.Value.Value.Equals(value)).Key;
                return result;
            }
            finally
            {
                _cacheLock.ExitReadLock();
            }
        }

        public new T Read(string key)
        {
            var item = base.Read(key);

            if (item.IsValid)
                return item.Value;

            Delete(key);
            return default;
        }
    }


    /// <summary>
    ///     Represents a lock that is used to manage access to a resource, allowing multiple threads for reading or exclusive
    ///     access for writing.
    /// </summary>
    public class SynchronizedCache<T> : ISynchronizedCache<T>
    {
        public enum AddOrUpdateStatus
        {
            Added,
            Updated,
            Unchanged
        }

        protected internal readonly ReaderWriterLockSlim _cacheLock = new ReaderWriterLockSlim();
        protected internal readonly Dictionary<string, T> _innerCache = new Dictionary<string, T>();

        public int Count => _innerCache.Count;


        public string TryGetKey(T value)
        {
            _cacheLock.EnterReadLock();
            try
            {
                var result = _innerCache.FirstOrDefault(c => c.Value.Equals(value)).Key;
                return result;
            }
            finally
            {
                _cacheLock.ExitReadLock();
            }
        }


        public T Read(string key)
        {
            _cacheLock.EnterReadLock();
            try
            {
                return _innerCache[key];
            }
            finally
            {
                _cacheLock.ExitReadLock();
            }
        }

        public void Add(string key, T value)
        {
            _cacheLock.EnterWriteLock();
            try
            {
                _innerCache.Add(key, value);
            }
            finally
            {
                _cacheLock.ExitWriteLock();
            }
        }

        public bool AddWithTimeout(string key, T value, int timeout)
        {
            if (_cacheLock.TryEnterWriteLock(timeout))
            {
                try
                {
                    _innerCache.Add(key, value);
                }
                finally
                {
                    _cacheLock.ExitWriteLock();
                }

                return true;
            }

            return false;
        }

        public AddOrUpdateStatus AddOrUpdate(string key, T value)
        {
            _cacheLock.EnterUpgradeableReadLock();
            try
            {
                if (_innerCache.TryGetValue(key, out var result))
                {
                    if (result.Equals(value))
                    {
                        return AddOrUpdateStatus.Unchanged;
                    }
                    else
                    {
                        _cacheLock.EnterWriteLock();
                        try
                        {
                            _innerCache[key] = value;
                        }
                        finally
                        {
                            _cacheLock.ExitWriteLock();
                        }

                        return AddOrUpdateStatus.Updated;
                    }
                }
                else
                {
                    _cacheLock.EnterWriteLock();
                    try
                    {
                        _innerCache.Add(key, value);
                    }
                    finally
                    {
                        _cacheLock.ExitWriteLock();
                    }

                    return AddOrUpdateStatus.Added;
                }
            }
            finally
            {
                _cacheLock.ExitUpgradeableReadLock();
            }
        }

        public void Delete(string key)
        {
            _cacheLock.EnterWriteLock();
            try
            {
                _innerCache.Remove(key);
            }
            finally
            {
                _cacheLock.ExitWriteLock();
            }
        }

        ~SynchronizedCache()
        {
            _cacheLock?.Dispose();
        }
    }
}