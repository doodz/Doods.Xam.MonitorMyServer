using Doods.Framework.Ssh.Std.Enums;
using System;

namespace Doods.Framework.Ssh.Std.Interfaces
{
    public interface ISshResponse<T> : ISshResponse
    {
        /// <summary>
        /// Deserialized entity data
        /// </summary>
        T Data { get; set; }
    }

    public interface ISshResponse
    {
        /// <summary>
        /// Length in bytes of the response content
        /// </summary>
        long ContentLength { get; set; }
        /// <summary>
        /// String representation of response content
        /// </summary>
        string Content { get; set; }

        /// <summary>
        /// Exceptions thrown during the request, if any.  
        /// </summary>
        Exception ErrorException { get; set; }
        string ErrorMessage { get; set; }
        ResponseStatus ResponseStatus { get; set; }

        ISshRequest Request { get; set; }
    }
}