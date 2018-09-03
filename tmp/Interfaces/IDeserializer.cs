namespace Doods.Framework.Ssh.Std.Interfaces
{
    public interface IDeserializer
    {
        T Deserialize<T>(ISshResponse response);
    }
}