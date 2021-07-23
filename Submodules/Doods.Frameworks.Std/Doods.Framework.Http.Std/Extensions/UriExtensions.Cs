using System;

namespace Doods.Framework.Http.Std.Extensions
{
    public static class UriExtensions
    {
        public static Uri SetPort(this Uri uri, int newPort)
        {
            var builder = new UriBuilder(uri);
            builder.Port = newPort;
            return builder.Uri;
        }
    }
}