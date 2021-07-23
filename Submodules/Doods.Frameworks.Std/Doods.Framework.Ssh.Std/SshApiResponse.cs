using System;
using System.Diagnostics;
using Doods.Framework.Ssh.Std.Enums;
using Doods.Framework.Ssh.Std.Interfaces;
using Renci.SshNet;

namespace Doods.Framework.Ssh.Std
{
    [DebuggerDisplay("{DebuggerDisplay()}")]
    public class SshApiResponse : SshResponseBase, ISshApiResponse
    {
    }

    [DebuggerDisplay("{DebuggerDisplay()}")]
    public abstract class SshResponseBase
    {
        public SshResponseBase()
        {
            ResponseStatus = ResponseStatus.None;
        }

        public ResponseStatus ResponseStatus { get; set; }
        public long ContentLength { get; set; }
        public string Content { get; set; }
        public Exception ErrorException { get; set; }
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public int ExitStatus { get; set; }
        public ISshRequest Request { get; set; }

        protected string DebuggerDisplay()
        {
            return
                $"StatusCode: , Content-Length: {ContentLength})";
        }
    }


    [DebuggerDisplay("{DebuggerDisplay()}")]
    public class SshApiResponse<T> : SshResponseBase, ISshApiResponse<T>
    {
        public T Data { get; set; }

        public static explicit operator SshApiResponse<T>(SshApiResponse apiResponse)
        {
            var restResponse = new SshApiResponse<T>();

            restResponse.ContentLength = apiResponse.ContentLength;

            restResponse.ErrorMessage = apiResponse.ErrorMessage;
            restResponse.ErrorException = apiResponse.ErrorException;

            restResponse.ResponseStatus = apiResponse.ResponseStatus;

            return restResponse;
        }
    }


    public class SshRequestAsyncHandle
    {
        public SshCommand SshRequest;

        public SshRequestAsyncHandle()
        {
        }

        public SshRequestAsyncHandle(SshCommand sshRequest)
        {
            SshRequest = sshRequest;
        }

        public void Abort()
        {
            SshRequest?.Dispose();
        }
    }
}