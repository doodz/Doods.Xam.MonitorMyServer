using Doods.Framework.Ssh.Std.Interfaces;

namespace Doods.Framework.Ssh.Std.Extensions
{
    public static class ResponseExtensions
    {
        public static ISshApiResponse<T> ToAsyncResponse<T>(this ISshApiResponse apiResponse)
        {
            return new SshApiResponse<T>
            {
                //ContentEncoding = apiResponse.ContentEncoding,
                ContentLength = apiResponse.ContentLength,
                //ContentType = apiResponse.ContentType,
                //Cookies = apiResponse.Cookies,
                ErrorException = apiResponse.ErrorException,
                ErrorMessage = apiResponse.ErrorMessage,
                StatusCode = apiResponse.StatusCode,
                //Headers = apiResponse.Headers,
                //RawBytes = apiResponse.RawBytes,
                ResponseStatus = apiResponse.ResponseStatus,
                //ResponseUri = apiResponse.ResponseUri,
                //Server = apiResponse.Server,
                //StatusCode = apiResponse.StatusCode,
                //StatusDescription = apiResponse.StatusDescription
                Content = apiResponse.Content
            };
        }
    }
}