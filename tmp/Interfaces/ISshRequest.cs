namespace Doods.Framework.Ssh.Std.Interfaces
{
    public interface ISshRequest : IDeserializer
    {
        string CommandText { get; }
        IDeserializer Handler { get; }
    }
}