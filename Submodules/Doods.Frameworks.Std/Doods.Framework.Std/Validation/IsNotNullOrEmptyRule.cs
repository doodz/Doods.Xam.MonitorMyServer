namespace Doods.Framework.Std.Validation
{
    public class IsNotNullOrEmptyRule<T> : ValidationRule<T>
    {
      
        public override bool Check(T value)
        {
            if (value == null) return false;
            return !string.IsNullOrWhiteSpace(value.ToString());
        }
    }
}