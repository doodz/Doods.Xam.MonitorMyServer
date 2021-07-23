using System;
using System.ComponentModel;
using System.Linq;
using Doods.Framework.Ssh.Std.Converters;
using Doods.Framework.Ssh.Std.Queries;
using Doods.Framework.Std.Utilities;

namespace Doods.Framework.Ssh.Std.Serializers
{
    public class DefaultContractResolver : IContractResolver
    {
        internal static readonly SshConverter[] BuiltInConverters =
        {
            new SshToStringConverter(),
            new SshToCpuInfoConverter(),
            new SshToHostnamectlConverter(),
            new SshToDiskUsageConverter(),
            new SshToBlockdeviceBeanConverter(),
            new SshToAptListConverter(),
            new SshToSimpleStringConverter(),
            new SshToMemoryUsageConverter(),
            new SshToLastloginConverter(),
            new SshToProcessConverter()
        }; //TODO

        private readonly ThreadSafeStore<Type, SshContract> _contractCache;

        public DefaultContractResolver()
        {
            _contractCache = new ThreadSafeStore<Type, SshContract>(CreateContract);
        }

        internal static IContractResolver Instance { get; } = new DefaultContractResolver();


        public virtual SshContract ResolveContract(Type type)
        {
            ValidationUtils.ArgumentNotNull(type, nameof(type));

            return _contractCache.Get(type);
        }

        protected virtual SshConverter ResolveContractConverter(Type objectType)
        {
            return null;
        }

        private void InitializeContract(SshContract contract)
        {
            contract.Converter = ResolveContractConverter(contract.NonNullableUnderlyingType);
            contract.InternalConverter =
                DoodsSshRequestSerializer.GetMatchingConverter(BuiltInConverters,
                    contract.NonNullableUnderlyingType);
        }

        protected virtual SshContract CreateContract(Type objectType)
        {
            var t = ReflectionUtils.EnsureNotByRefType(objectType);
            t = ReflectionUtils.EnsureNotNullableType(t);


            //if (typeof(IEnumerable).IsAssignableFrom(t))
            //{
            //    return CreateArrayContract(t);
            //}

            if (BuiltInConverters.Any(c => c.CanConvert(objectType)))
                return CreateObjectContract(objectType);

            if (CanConvertToString(t)) return CreateStringContract(objectType);

            return null; //TODO
        }

        protected virtual SshObjectContract CreateObjectContract(Type objectType)
        {
            var contract = new SshObjectContract(objectType);
            InitializeContract(contract);

            return contract;
        }

        protected virtual SshStringContract CreateStringContract(Type objectType)
        {
            var contract = new SshStringContract(objectType);
            InitializeContract(contract);

            return contract;
        }


        internal static bool CanConvertToString(Type type)
        {
            if (CanTypeDescriptorConvertString(type, out _)) return true;

            if (type == typeof(Type) || type.IsSubclassOf(typeof(Type))) return true;

            return false;
        }

        public static bool CanTypeDescriptorConvertString(Type type, out TypeConverter typeConverter)
        {
            typeConverter = TypeDescriptor.GetConverter(type);

            // use the objectType's TypeConverter if it has one and can convert to a string
            if (typeConverter != null)
            {
                var converterType = typeConverter.GetType();

                if (!string.Equals(converterType.FullName, "System.ComponentModel.ComponentConverter",
                        StringComparison.Ordinal)
                    && !string.Equals(converterType.FullName, "System.ComponentModel.ReferenceConverter",
                        StringComparison.Ordinal)
                    && !string.Equals(converterType.FullName, "System.Windows.Forms.Design.DataSourceConverter",
                        StringComparison.Ordinal)
                    && converterType != typeof(TypeConverter))
                    return typeConverter.CanConvertTo(typeof(string));
            }

            return false;
        }
    }
}