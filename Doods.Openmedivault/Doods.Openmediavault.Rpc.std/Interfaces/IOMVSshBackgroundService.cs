using System.Threading.Tasks;
using Doods.Openmediavault.Rpc.Std.Data.V4;

namespace Doods.Openmediavault.Rpc.Std.Interfaces
{
    public interface IOMVSshBackgroundService
    {
        Task<bool> CheckRunningAsync(string filename);
        Task<Output<T>> GetOutputAsync<T>(string filename);
        Task<Output<string>> GetOutputAsync(string filename);
    }
}