using System;
using Doods.Framework.Ssh.Std.Enums;

namespace Doods.Framework.Ssh.Std.Interfaces
{
    public interface ISshApiResponse<T> : ISshApiResponse, IApiResponse<T>
    {
    }

    public interface ISshApiResponse : IApiResponse
    {
        /// <summary>
        ///     Length in bytes of the apiResponse content
        /// </summary>
        long ContentLength { get; set; }


        /// <summary>
        ///     Exceptions thrown during the request, if any.
        /// </summary>
        Exception ErrorException { get; set; }

        ResponseStatus ResponseStatus { get; set; }

        ISshRequest Request { get; set; }


        int StatusCode { get; set; }
    }
}