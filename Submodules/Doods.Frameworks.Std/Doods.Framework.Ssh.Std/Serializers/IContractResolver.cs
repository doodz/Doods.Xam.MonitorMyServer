using System;

namespace Doods.Framework.Ssh.Std.Serializers
{
    public interface IContractResolver
    {
        SshContract ResolveContract(Type type);
    }
}