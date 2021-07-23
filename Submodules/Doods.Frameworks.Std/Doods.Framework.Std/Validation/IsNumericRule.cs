namespace Doods.Framework.Std.Validation
{
    public class IsNumericRule<T> : ValidationRule<T>
    {
        private readonly bool _allowDecimal;

        public IsNumericRule(bool allowDecimal)
        {
            _allowDecimal = allowDecimal;
        }

        

        public override bool Check(T value)
        {
            
            var isValid = false;
            if (_allowDecimal)
                isValid = double.TryParse(value.ToString(), out var result);
            else
                isValid = long.TryParse(value.ToString(), out var result);
            return isValid;
        }
    }
}