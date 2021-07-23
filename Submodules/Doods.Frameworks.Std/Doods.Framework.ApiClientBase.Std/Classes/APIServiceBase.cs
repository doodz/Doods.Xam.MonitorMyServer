using Doods.Framework.ApiClientBase.Std.Interfaces;

namespace Doods.Framework.ApiClientBase.Std.Classes
{
    public abstract class APIServiceBase
    {
        protected IConnection Connection;

        public bool CanConnect()
        {
            return Connection != null;
        }
    }
}