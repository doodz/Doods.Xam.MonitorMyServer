using System;
using Doods.Framework.Std.Utilities;

namespace Doods.Framework.Ssh.Std.Serializers
{
    public static class DoodsSshRequestConvert
    {
        //public T Deserialize<T>(ISshApiResponse apiResponse)
        //{

        //    if (typeof(T) == typeof(CpuInfoBean))
        //    {
        //        return new SshToCpuInfoConverter().Deserialize(apiResponse.Content);
        //    }

        //    return default(T);
        //}

        public static object DeserializeObject(string value)
        {
            return DeserializeObject(value, null, (SshSerializerSettings) null);
        }

        public static object DeserializeObject(string value, SshSerializerSettings settings)
        {
            return DeserializeObject(value, null, settings);
        }

        public static object DeserializeObject(string value, Type type)
        {
            return DeserializeObject(value, type, (SshSerializerSettings) null);
        }

        public static T DeserializeObject<T>(string value)
        {
            return DeserializeObject<T>(value, (SshSerializerSettings) null);
        }

        public static T DeserializeObject<T>(string value, params SshConverter[] converters)
        {
            return (T) DeserializeObject(value, typeof(T), converters);
        }

        public static T DeserializeObject<T>(string value, SshSerializerSettings settings)
        {
            return (T) DeserializeObject(value, typeof(T), settings);
        }

        public static object DeserializeObject(string value, Type type, params SshConverter[] converters)
        {
            var settings = converters != null && converters.Length > 0
                ? new SshSerializerSettings {Converters = converters}
                : null;

            return DeserializeObject(value, type, settings);
        }


        public static object DeserializeObject(string value, Type type, SshSerializerSettings settings)
        {
            ValidationUtils.ArgumentNotNull(value, nameof(value));

            var sshSerializer = DoodsSshRequestSerializer.CreateDefault(settings);


            return sshSerializer.Deserialize(value, type);
        }
    }
}