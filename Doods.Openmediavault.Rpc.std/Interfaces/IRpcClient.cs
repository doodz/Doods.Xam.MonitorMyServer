using System.Threading.Tasks;
using Doods.Openmediavault.Rpc.Std;

namespace Doods.Openmedivault.Ssh.Std.Requests
{
    public interface IRpcClient
    {
        Task<T> ExecuteTaskAsync<T>(IRpcRequest request);
    }
}