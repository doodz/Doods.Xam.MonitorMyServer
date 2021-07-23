using System;

namespace Doods.Framework.ApiClientBase.Std.Exceptions
{
    /// <summary>
    ///     The exception that is thrown when authentication failed.
    /// </summary>
    public class DoodsApiAuthenticationException : DoodsApiException
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:DoodsApiAuthenticationException" /> class.
        /// </summary>
        public DoodsApiAuthenticationException()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:DoodsApiAuthenticationException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public DoodsApiAuthenticationException(string message)
            : base(message)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:DoodsApiAuthenticationException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public DoodsApiAuthenticationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}