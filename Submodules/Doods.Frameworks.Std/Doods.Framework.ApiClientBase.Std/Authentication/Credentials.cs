namespace Doods.Framework.ApiClientBase.Std.Authentication
{
    public interface ICredentials
    {
        string Login { get; }

        string Password { get; }

        AuthenticationType AuthenticationType { get; }
    }
    public class Credentials: ICredentials
    {
        public Credentials()
        {
            AuthenticationType = AuthenticationType.Anonymous;
        }

        public Credentials(string token)
        {
            Login = null;
            Password = token;
            AuthenticationType = AuthenticationType.OAuth;
        }

        public Credentials(string login, string password)
        {
            Login = login;
            Password = password;
            AuthenticationType = AuthenticationType.Simple;
        }

        public string Login { get; }

        public string Password { get; }

        public AuthenticationType AuthenticationType { get; }
    }
}