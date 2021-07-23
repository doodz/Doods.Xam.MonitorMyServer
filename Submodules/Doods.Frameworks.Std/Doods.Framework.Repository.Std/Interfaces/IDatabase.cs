using System.Threading.Tasks;
using Doods.Framework.Std;
using SQLite;

namespace Doods.Framework.Repository.Std.Interfaces
{
    public interface IDatabase
    {
        bool IsInitialize { get; }

        Task<SQLiteAsyncConnection> GetConnection(ITimeWatcher timer);

        Task Reset();
    }
}