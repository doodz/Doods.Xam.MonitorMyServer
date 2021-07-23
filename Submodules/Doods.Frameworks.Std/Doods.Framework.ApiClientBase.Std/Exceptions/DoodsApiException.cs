using System;

namespace Doods.Framework.ApiClientBase.Std.Exceptions
{
    /// <summary>
    ///     The exception that is thrown when connection was terminated.
    /// </summary>
    public class DoodsApiException : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:DoodsApiException" /> class.
        /// </summary>
        public DoodsApiException()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:DoodsApiException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public DoodsApiException(string message)
            : base(message)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:DoodsApiException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public DoodsApiException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}