using Doods.Framework.Std.Extensions;

namespace Doods.Framework.Std.Validation
{
    public class ValidationHelper
    {
        public bool IsBlankString(string str)
        {
            return str.IsEmpty();
        }
    }
}