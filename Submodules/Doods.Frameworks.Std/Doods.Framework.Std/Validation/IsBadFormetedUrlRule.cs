using System.Text.RegularExpressions;

namespace Doods.Framework.Std.Validation
{
    public class IsBadFormetedUrlRule<T> : ValidationRule<T>
    {
        //private const string regex = @"^(https?:\/\/)";
        private readonly Regex _haveHttpS = new Regex(@"^(https?:\/\/)");
        private readonly bool _needHttp;

        public IsBadFormetedUrlRule(bool needHttp)
        {
            _needHttp = needHttp;
        }



        public override bool Check(T value)
        {
            if (value == null) return false;

            var localval = value.ToString().ToLower();

            if(string.IsNullOrWhiteSpace(localval)) return false;

            var res = _haveHttpS.IsMatch(localval);

            if (!_needHttp) return !res;
            return res;
        }
    }
}