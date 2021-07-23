using System;
using Doods.Framework.Std.Utilities;

namespace Doods.Framework.Ssh.Std.Serializers
{
    internal class DoodsSshRequestSerializerInternalReader
    {
        internal readonly DoodsSshRequestSerializer Serializer;

        public DoodsSshRequestSerializerInternalReader(DoodsSshRequestSerializer serializer)
        {
            ValidationUtils.ArgumentNotNull(serializer, nameof(serializer));
            Serializer = serializer;
        }

        public object Deserialize(string reader, Type objectType)
        {
            if (reader == null) throw new ArgumentNullException(nameof(reader));

            var contract = GetContractSafe(objectType);
            try
            {
                //SshConverter converter = GetMatchingConverter(DefaultContractResolver.BuiltInConverters,
                //    contract.NonNullableUnderlyingType);
                var converter = GetConverter(contract);
                object deserializedValue = null;
                if (converter != null) deserializedValue = DeserializeConvertable(converter, reader, objectType);

                return deserializedValue;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private object DeserializeConvertable(ISshConverter converter, string reader, Type objectType)
        {
            var value = converter.Read(reader, objectType);


            return value;
        }

        private SshContract GetContractSafe(Type type)
        {
            if (type == null) return null;

            return Serializer._contractResolver.ResolveContract(type);
        }


        private ISshConverter GetConverter(SshContract contract)
        {
            ISshConverter converter = null;
            if (contract != null)
            {
                ISshConverter matchingConverter;
                if (contract.Converter != null)
                    converter = contract.Converter;
                else if ((matchingConverter = Serializer.GetMatchingConverter(contract.UnderlyingType)) != null)
                    converter = matchingConverter;
                else if (contract.InternalConverter != null) converter = contract.InternalConverter;
            }

            return converter;
        }
    }
}