using System.Threading.Tasks;
using Doods.Framework.Ssh.Std.Interfaces;

namespace Doods.Framework.ApiClientBase.Std.Interfaces
{
    public interface IApiClient
    {
        Task<IApiResponse> ExecuteTaskAsync(IApiRequest request);
        Task<IApiResponse<T>> ExecuteTaskAsync<T>(IApiRequest request);
    }
}