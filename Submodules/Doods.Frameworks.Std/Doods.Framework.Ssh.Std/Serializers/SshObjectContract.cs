using System;

namespace Doods.Framework.Ssh.Std.Serializers
{
    public class SshObjectContract : SshContract
    {
        internal SshObjectContract(Type underlyingType) : base(underlyingType)
        {
        }
    }
}