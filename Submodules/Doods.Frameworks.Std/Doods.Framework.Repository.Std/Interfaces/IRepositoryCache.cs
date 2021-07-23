using System;
using Doods.Framework.Std;

namespace Doods.Framework.Repository.Std.Interfaces
{
    internal interface IRepositoryCache
    {
        void Set(ITimeWatcher timer, string key, object value);

        T Get<T>(ITimeWatcher timer, string key);

        bool Clear(ITimeWatcher timer, Func<string, bool> factory);

        bool ClearAll(ITimeWatcher timer);
    }
}