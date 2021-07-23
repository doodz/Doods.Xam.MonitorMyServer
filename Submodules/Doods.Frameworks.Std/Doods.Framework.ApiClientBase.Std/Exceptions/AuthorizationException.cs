using System;

namespace Doods.Framework.ApiClientBase.Std.Exceptions
{
    public class AuthorizationException : Exception
    {
        public string Htmlcontent;

        public AuthorizationException(string htmlcontent)
        {
            Htmlcontent = htmlcontent;
        }

        public override string Message => "Unauthorized";
    }
}