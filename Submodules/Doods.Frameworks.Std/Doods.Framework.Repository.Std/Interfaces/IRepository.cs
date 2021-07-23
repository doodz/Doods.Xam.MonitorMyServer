using System.Collections.Generic;
using System.Threading.Tasks;
using Doods.Framework.Repository.Std.Tables;
using Doods.Framework.Std;
using SQLite;

namespace Doods.Framework.Repository.Std.Interfaces
{
    public interface IRepository
    {
        Task<SQLiteAsyncConnection> GetConnection(ITimeWatcher timer);

        Task<int> CountAsync<T>(ITimeWatcher timer, SQLiteAsyncConnection cnt = null) where T : TableBase, new();

        Task<List<T>> GetAllAsync<T>(ITimeWatcher timer, SQLiteAsyncConnection cnt = null, bool cache = false)
            where T : TableBase, new();

        Task<T> FindAsync<T>(ITimeWatcher timer, long? id) where T : TableBase, new();

        Task<int> InsertAsync<T>(ITimeWatcher timer, T value) where T : TableBase, new();

        Task UpdateAsync<T>(ITimeWatcher timer, T value) where T : TableBase, new();

        Task DeleteAsync<T>(ITimeWatcher timer, T value) where T : TableBase, new();

        Task DeleteAllIdsAsync<T>(ITimeWatcher timer, IEnumerable<long> ids, SQLiteAsyncConnection cnt = null)
            where T : TableBase, new();

        Task ClearCaches(ITimeWatcher timer);

        Task ClearCache<T>(ITimeWatcher timer) where T : TableBase, new();
    }
}