using System.Threading.Tasks;

namespace Doods.Framework.ApiClientBase.Std.Classes
{
    public interface IWakeService
    {
        Task Wake(byte[] mac);
    }
}