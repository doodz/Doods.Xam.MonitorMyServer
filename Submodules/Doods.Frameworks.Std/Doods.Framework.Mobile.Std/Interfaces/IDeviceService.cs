using System.Threading.Tasks;

namespace Doods.Framework.Mobile.Std.Interfaces
{
    public interface IDeviceService
    {
        bool IsInitialize { get; }

        bool IsAndroid { get; }

        void Initialize();

        Task Call(string tel);

        Task Email(string adresse);

        Task NavigateToAdresse(string adresse);

        Task NavigateToPosition(double latitude, double longitude);

        Task Share(string message);

        Task<string> SpeechToTextAsync();

        Task Web(string url);
    }
}