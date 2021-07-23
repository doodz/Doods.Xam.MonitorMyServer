using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Doods.Framework.Std.Utilities;

namespace Doods.Framework.Ssh.Std.Serializers
{
    public class DoodsSshRequestSerializer
    {
        internal IContractResolver _contractResolver;
        internal Collection<ISshConverter> _converters;

        public DoodsSshRequestSerializer()
        {
            _contractResolver = DefaultContractResolver.Instance;
        }

        public virtual Collection<ISshConverter> Converters
        {
            get
            {
                if (_converters == null) _converters = new Collection<ISshConverter>();
                return _converters;
            }
        }

        public virtual IContractResolver ContractResolver
        {
            get => _contractResolver;
            set => _contractResolver = value ?? DefaultContractResolver.Instance;
        }

        public static DoodsSshRequestSerializer CreateDefault(ISshSerializerSettings settings)
        {
            var serializer = CreateDefault();
            if (settings != null) ApplySerializerSettings(serializer, settings);
            return serializer;
        }

        private static void ApplySerializerSettings(DoodsSshRequestSerializer serializer,
            ISshSerializerSettings settings)
        {
            if (!CollectionUtils.IsNullOrEmpty(settings.Converters))
                for (var i = 0; i < settings.Converters.Count; i++)
                    serializer.Converters.Insert(i, settings.Converters[i]);
        }

        public static DoodsSshRequestSerializer CreateDefault()
        {
            return new DoodsSshRequestSerializer();
        }

        public T Deserialize<T>(string value)
        {
            return (T) Deserialize(value, typeof(T));
        }

        public object Deserialize(string value, Type objectType)
        {
            return DeserializeInternal(value, objectType);
        }

        internal virtual object DeserializeInternal(string reader, Type objectType)
        {
            ValidationUtils.ArgumentNotNull(reader, nameof(reader));
            var serializerReader = new DoodsSshRequestSerializerInternalReader(this);
            var value = serializerReader.Deserialize(reader, objectType);
            return value;
        }


        internal ISshConverter GetMatchingConverter(Type type)
        {
            return GetMatchingConverter(_converters, type);
        }

        internal static ISshConverter GetMatchingConverter(IList<ISshConverter> converters, Type objectType)
        {
#if DEBUG
            ValidationUtils.ArgumentNotNull(objectType, nameof(objectType));
#endif

            if (converters != null)
                for (var i = 0; i < converters.Count; i++)
                {
                    var converter = converters[i];
                    if (converter.CanConvert(objectType)) return converter;
                }

            return null;
        }
    }
}