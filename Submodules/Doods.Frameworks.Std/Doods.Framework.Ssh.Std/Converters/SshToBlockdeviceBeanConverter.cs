using System;
using System.Collections.Generic;
using Doods.Framework.Ssh.Std.Beans;
using Doods.Framework.Ssh.Std.Serializers;
using Newtonsoft.Json;

namespace Doods.Framework.Ssh.Std.Converters
{
    public class SshToBlockdeviceBeanConverter : SshConverter
    {
        /// <summary>
        ///     Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        ///     <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(BlockdeviceWrapper)) return true;

            if (objectType == typeof(IEnumerable<Blockdevice>)) return true;


            return objectType == typeof(Blockdevice);
        }

        public override object Read(string content, Type objectType)

        {
            var obj = JsonConvert.DeserializeObject<BlockdeviceWrapper>(content);


            if (objectType == typeof(BlockdeviceWrapper)) return obj;
            return obj.BlockdevicesBlockdevices;
        }
    }
}