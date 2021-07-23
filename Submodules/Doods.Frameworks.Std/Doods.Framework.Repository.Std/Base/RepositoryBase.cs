using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doods.Framework.Repository.Std.Interfaces;
using Doods.Framework.Std;
using Doods.Framework.Std.Extensions;
using SQLite;
using SQLiteNetExtensionsAsync.Extensions;

namespace Doods.Framework.Repository.Std.Tables
{
    internal class RepositoryBase : IRepository
    {
        private readonly IRepositoryCache _cache;
        private readonly IDatabase _database;

        public RepositoryBase(IDatabase database, IRepositoryCache cache)
        {
            _database = database;
            _cache = cache;
        }

        public async Task<int> CountAsync<T>(ITimeWatcher timer, SQLiteAsyncConnection cnt) where T : TableBase, new()
        {
            using (var watch = timer.StartWatcher("CountAsync"))
            {
                watch?.Properties?.Add("type", typeof(T).Name);
                cnt = cnt ?? await _database.GetConnection(timer);
                return await cnt.Table<T>().CountAsync();
            }
        }


        public async Task<List<T>> GetAllAsync<T>(ITimeWatcher timer, SQLiteAsyncConnection cnt, bool cache)
            where T : TableBase, new()
        {
            using (var watch = timer.StartWatcher("GetAllAsync"))
            {
                watch?.Properties?.Add("type", typeof(T).Name);
                watch?.Properties?.Add("cache", cache.ToString());

                var key = $"GetAllAsync:{typeof(T).FullName}";
                if (cache)
                {
                    var cacheValues = _cache.Get<List<T>>(timer, key);
                    if (cacheValues != null) return cacheValues;
                }

                cnt = cnt ?? await _database.GetConnection(timer);
                var rslt = await cnt.Table<T>().ToListAsync();

                watch?.Properties?.Add("count", rslt.Count.ToString());

                if (cache) _cache.Set(timer, key, rslt);

                return rslt;
            }
        }


        public async Task<T> FindAsync<T>(ITimeWatcher timer, long? id) where T : TableBase, new()
        {
            using (var watch = timer.StartWatcher("GetExploitationsAsync"))
            {
                watch?.Properties?.Add("type", typeof(T).Name);

                if (id == null) return default;
                watch?.Properties?.Add("id", id.ToString());

                var cnt = await _database.GetConnection(timer);
                return await cnt.Table<T>().Where(e => e.Id == id).FirstOrDefaultAsync();
            }
        }

        public async Task<int> InsertAsync<T>(ITimeWatcher timer, T value) where T : TableBase, new()
        {
            var cnt = await _database.GetConnection(timer);
            return await cnt.InsertOrReplaceAsync(value);
        }

        public async Task UpdateAsync<T>(ITimeWatcher timer, T value) where T : TableBase, new()
        {
            var cnt = await _database.GetConnection(timer);
            await cnt.UpdateAsync(value);
        }

        public async Task DeleteAsync<T>(ITimeWatcher timer, T value) where T : TableBase, new()
        {
            var cnt = await _database.GetConnection(timer);
            await cnt.DeleteAsync(value);
        }

        public async Task DeleteAllIdsAsync<T>(ITimeWatcher timer, IEnumerable<long> ids, SQLiteAsyncConnection cnt)
            where T : TableBase, new()
        {
            if (ids.IsNotEmpty())
                using (timer.StartWatcher("DeleteAllIdsAsync"))
                {
                    cnt = cnt ?? await _database.GetConnection(timer);
                    await cnt.DeleteAllIdsAsync<T>(ids.Select(i => (object) i));
                }
        }

        public Task<SQLiteAsyncConnection> GetConnection(ITimeWatcher timer)
        {
            return _database.GetConnection(timer);
        }

        public Task ClearCaches(ITimeWatcher timer)
        {
            _cache.ClearAll(timer);
            return Task.FromResult(0);
        }

        public Task ClearCache<T>(ITimeWatcher timer) where T : TableBase, new()
        {
            _cache.Clear(timer, k => k.Contains($"{typeof(T).FullName}"));
            return Task.FromResult(0);
        }
    }
}