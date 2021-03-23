using Autofac;
using Doods.Framework.Mobile.Std.Interfaces;
using Doods.Framework.Std;

namespace Doods.Xam.MonitorMyServer.Data
{
    public class SynchronizedCacheItemQueryShell : IQueryShellNavigationObject
    {
        private readonly string _str;

        public SynchronizedCacheItemQueryShell(object obj)
        {
            var cache = App.Container.Resolve<ISynchronizedCache<object>>();
            cache.AddOrUpdate(nameof(SynchronizedCacheItemQueryShell), obj);
            var typeName = obj.GetType().Name;
            var cacheKey = nameof(SynchronizedCacheItemQueryShell);
            _str = $"CacheKeyQuery={cacheKey}&TypeNameQuery={typeName}";
        }

        public SynchronizedCacheItemQueryShell(IQueryShellNavigationObject obj)
        {
            var cache = App.Container.Resolve<ISynchronizedCache<object>>();
            cache.AddOrUpdate(nameof(SynchronizedCacheItemQueryShell), obj);
            var typeName = obj.GetType().Name;
            var cacheKey = nameof(SynchronizedCacheItemQueryShell);
            _str = $"{obj.ToQuery()}&CacheKeyQuery={cacheKey}&TypeNameQuery={typeName}";
        }

        public string ToQuery()
        {
            return _str;
        }
    }
}