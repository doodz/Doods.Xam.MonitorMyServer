using Doods.Framework.Ssh.Std.Enums;
using Doods.Framework.Ssh.Std.Interfaces;
using Renci.SshNet;
using System;
using System.Diagnostics;

namespace Doods.Framework.Ssh.Std
{
    [DebuggerDisplay("{DebuggerDisplay()}")]
    public class SshResponse : SshResponseBase, ISshResponse
    {
    }

    [DebuggerDisplay("{DebuggerDisplay()}")]
    public abstract class SshResponseBase
    {
        public ResponseStatus ResponseStatus { get; set; }
        public long ContentLength { get; set; }
        public string Content { get; set; }
        public Exception ErrorException { get; set; }
        public string ErrorMessage { get; set; }
        public int ExitStatus { get; set; }
        public ISshRequest Request { get; set; }

        public SshResponseBase()
        {
            ResponseStatus = ResponseStatus.None;
        }

        protected string DebuggerDisplay()
        {
            return
                $"StatusCode: , Content-Length: {ContentLength})";
        }
    }


    [DebuggerDisplay("{DebuggerDisplay()}")]
    public class SshResponse<T> : SshResponseBase, ISshResponse<T>
    {
        public T Data { get; set; }

        public static explicit operator SshResponse<T>(SshResponse response)
        {
            var restResponse = new SshResponse<T>();

            restResponse.ContentLength = response.ContentLength;

            restResponse.ErrorMessage = response.ErrorMessage;
            restResponse.ErrorException = response.ErrorException;

            restResponse.ResponseStatus = response.ResponseStatus;

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