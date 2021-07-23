using System;
using System.Collections.Generic;
using System.Linq;
using Doods.Framework.Repository.Std.Interfaces;
using Doods.Framework.Std;
using Doods.Framework.Std.Extensions;

namespace Doods.Framework.Repository.Std
{
    internal class RepositoryCache : IRepositoryCache
    {
        private readonly Dictionary<string, object> _cache;

        public RepositoryCache()
        {
            _cache = new Dictionary<string, object>();
        }

        public void Set(ITimeWatcher timer, string k, object value)
        {
            using (var watcher = timer.StartWatcher("RepositoryCache.Set"))
            {
                watcher?.Properties?.Add("key", k);

                if (k != null && value != null) _cache.AddOrSet(k, value);
            }
        }

        public T Get<T>(ITimeWatcher timer, string k)
        {
            using (var watcher = timer.StartWatcher("RepositoryCache.Get"))
            {
                watcher?.Properties?.Add("key", k);

                if (k != null && _cache.ContainsKey(k))
                {
                    watcher?.Properties?.Add("exists", true.ToString());
                    return (T) _cache[k];
                }

                watcher?.Properties?.Add("exists", false.ToString());

                return default;
            }
        }

        public bool Clear(ITimeWatcher timer, Func<string, bool> factory)
        {
            using (var watcher = timer.StartWatcher("RepositoryCache.Clear"))
            {
                var keys = _cache.Keys.Where(k => factory(k));
                watcher?.Properties?.Add("keys", keys.Join(","));
                if (keys.IsNotEmpty())
                {
                    foreach (var k in keys) _cache.Remove(k);

                    return true;
                }

                watcher?.Properties?.Add("exists", false.ToString());
                return false;
            }
        }

        public bool ClearAll(ITimeWatcher timer)
        {
            using (timer.StartWatcher("RepositoryCache.ClearAll"))
            {
                _cache.Clear();
            }

            return true;
        }

        private string GetKey(object value)
        {
            if (value == null) return null;

            return value.GetType().FullName;
        }
    }
}