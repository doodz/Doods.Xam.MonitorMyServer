namespace Doods.Framework.Std.Validation
{
    public abstract class ValidationRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public virtual bool  Check(T value)
        {
            return true;
        }
    }
}