using System.Threading.Tasks;

namespace Doods.Webmin.Webapi.Std.Clients
{
    public interface IWebminLoginClient
    {
        Task<bool> LoginAsync(string username, string password);
        void LogOut();
    }
}