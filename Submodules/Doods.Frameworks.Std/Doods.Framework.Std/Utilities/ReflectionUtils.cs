using System;

namespace Doods.Framework.Std.Utilities
{
    public static class ReflectionUtils
    {
        public static bool IsNullableType(Type t)
        {
            ValidationUtils.ArgumentNotNull(t, nameof(t));

            return t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        public static bool IsNullable(Type t)
        {
            ValidationUtils.ArgumentNotNull(t, nameof(t));

            if (t.IsValueType) return IsNullableType(t);

            return true;
        }

        public static Type EnsureNotByRefType(Type t)
        {
            return t.IsByRef && t.HasElementType
                ? t.GetElementType()
                : t;
        }

        public static Type EnsureNotNullableType(Type t)
        {
            return IsNullableType(t)
                ? Nullable.GetUnderlyingType(t)
                : t;
        }
    }
}