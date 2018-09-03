using Doods.Framework.Ssh.Std.Interfaces;

namespace Doods.Framework.Ssh.Std.Extensions
{
    public static class ResponseExtensions
    {
        public static ISshResponse<T> ToAsyncResponse<T>(this ISshResponse response)
        {
            return new SshResponse<T>
            {
                //ContentEncoding = response.ContentEncoding,
                ContentLength = response.ContentLength,
                //ContentType = response.ContentType,
                //Cookies = response.Cookies,
                ErrorException = response.ErrorException,
                ErrorMessage = response.ErrorMessage,
                //Headers = response.Headers,
                //RawBytes = response.RawBytes,
                ResponseStatus = response.ResponseStatus,
                //ResponseUri = response.ResponseUri,
                //Server = response.Server,
                //StatusCode = response.StatusCode,
                //StatusDescription = response.StatusDescription
                Content = response.Content
            };
        }
    }
}
