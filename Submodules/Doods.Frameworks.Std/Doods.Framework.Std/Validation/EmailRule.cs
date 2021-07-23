using System.Text.RegularExpressions;

namespace Doods.Framework.Std.Validation
{
    public class EmailRule<T> : ValidationRule<T>
    {
      
        public override bool Check(T value)
        {
            if (value == null) return false;

            var str = value as string;
            var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            var match = regex.Match(str);

            return match.Success;
        }
    }
}