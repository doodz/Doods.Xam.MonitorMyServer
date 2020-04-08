using System.Threading.Tasks;
using Doods.Openmediavault.Rpc.std.Data.V4;

namespace Doods.Openmediavault.Rpc.std.Interfaces
{
    public interface IOMVSshBackgroundService
    {
        Task<bool> CheckRunningAsync(string filename);
        Task<Output<T>> GetOutputAsync<T>(string filename);
        Task<Output<string>> GetOutputAsync(string filename);
    }
}