namespace Doods.Framework.Ssh.Std.Interfaces
{
    public interface ISshRequest
    {
        string CommandText { get; }
        IDeserializer Handler { get; }
    }
}