using System.Threading.Tasks;

namespace Doods.Framework.Ssh.Std.Interfaces
{
    public interface IXamarinDeviceService
    {
        Task Call(string tel);

        Task Email(string adresse);

        Task NavigateToAdresse(string adresse);

        Task NavigateToPosition(double latitude, double longitude);
    }
}
