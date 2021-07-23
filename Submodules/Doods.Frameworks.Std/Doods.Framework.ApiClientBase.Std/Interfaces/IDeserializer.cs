namespace Doods.Framework.Ssh.Std.Interfaces
{
    public interface IDeserializer
    {
        T Deserialize<T>(IApiResponse apiResponse);
        T DeserializeError<T>(IApiResponse apiResponse);
    }
}