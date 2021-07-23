namespace Doods.Framework.Std.Validation
{
    //toto great
    public class IsGoodPassword<T> : ValidationRule<T>
    {
        public override bool Check(T value)
        {
            if (value == null) return false;
            if (!(value is string)) return false;
            var v = value as string;
            if (string.IsNullOrWhiteSpace(v)) return false;
            if (v.Length < 8) return false;
            return true;
        }
    }
}