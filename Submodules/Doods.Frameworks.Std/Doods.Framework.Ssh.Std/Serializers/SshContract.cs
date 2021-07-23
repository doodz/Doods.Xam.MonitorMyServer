using System;
using Doods.Framework.Std.Utilities;

namespace Doods.Framework.Ssh.Std.Serializers
{
    public class SshContract
    {
        internal bool IsNullable;
        internal Type NonNullableUnderlyingType;


        internal SshContract(Type underlyingType)
        {
            ValidationUtils.ArgumentNotNull(underlyingType, nameof(underlyingType));

            UnderlyingType = underlyingType;
            underlyingType = ReflectionUtils.EnsureNotByRefType(underlyingType);

            IsNullable = ReflectionUtils.IsNullable(underlyingType);

            NonNullableUnderlyingType = IsNullable && ReflectionUtils.IsNullableType(underlyingType)
                ? Nullable.GetUnderlyingType(underlyingType)
                : underlyingType;
        }

        public Type UnderlyingType { get; }
        public ISshConverter Converter { get; set; }
        internal ISshConverter InternalConverter { get; set; }
    }
}