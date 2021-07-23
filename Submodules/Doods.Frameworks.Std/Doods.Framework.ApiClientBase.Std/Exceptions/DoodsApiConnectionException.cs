namespace Doods.Framework.ApiClientBase.Std.Exceptions
{
    /// <summary>
    ///     The exception that is thrown when connection was terminated.
    /// </summary>
    public class DoodsApiConnectionException : DoodsApiException
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Renci.SshNet.Common.SshConnectionException" /> class.
        /// </summary>
        public DoodsApiConnectionException()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Renci.SshNet.Common.SshConnectionException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public DoodsApiConnectionException(string message)
            : base(message)
        {
        }
    }
}