using System.Collections.Generic;
using System.Threading.Tasks;
using Doods.Openmediavault.Rpc.Std.Enums;

namespace Doods.Openmediavault.Rpc.Std.Interfaces
{
    public interface IRpcClient
    {
        RequestType RequestType { get; }
        Task<T> ExecuteTaskAsync<T>(IRpcRequest request);
        Task<bool> LoginAsync(string username, string password);
        Task<IEnumerable<byte[]>> GetRrdFilesAsync(IEnumerable<string> filesPaths);
        Task<IEnumerable<string>> ListRrdFilesAsync();
        Task<byte[]> GetRrdFileAsync(string filePath);
    }
}