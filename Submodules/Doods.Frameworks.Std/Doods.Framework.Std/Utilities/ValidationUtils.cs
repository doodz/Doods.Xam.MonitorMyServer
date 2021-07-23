using System;

namespace Doods.Framework.Std.Utilities
{
    public static class ValidationUtils
    {
        public static void ArgumentNotNull(object value, string parameterName)
        {
            if (value == null) throw new ArgumentNullException(parameterName);
        }
    }
}