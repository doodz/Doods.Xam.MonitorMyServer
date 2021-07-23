using System.Collections.Generic;

namespace Doods.Framework.Ssh.Std.Serializers
{

    public interface ISshSerializerSettings
    {
        IList<ISshConverter> Converters { get; set; }
    }


    public class SshSerializerSettings: ISshSerializerSettings
    {
        public SshSerializerSettings()
        {
            Converters = new List<ISshConverter>();
        }

        public IList<ISshConverter> Converters { get; set; }
    }
}