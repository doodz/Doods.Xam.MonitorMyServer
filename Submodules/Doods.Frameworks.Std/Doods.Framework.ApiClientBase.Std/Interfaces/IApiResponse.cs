namespace Doods.Framework.Ssh.Std.Interfaces
{
    public interface IApiResponse
    {
        string ErrorMessage { get; set; }

        /// <summary>
        ///     String representation of response content
        /// </summary>
        string Content { get; set; }
    }

    public interface IApiResponse<T> : IApiResponse
    {
        /// <summary>
        ///     Deserialized entity data
        /// </summary>
        T Data { get; set; }
    }
}