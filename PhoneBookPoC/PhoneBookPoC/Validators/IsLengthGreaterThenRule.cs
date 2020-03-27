namespace PhoneBookPoC.Validators
{
    public class IsLengthGreaterThenRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public int MaxLength { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var str = value as string;

            return str.Length < MaxLength;
        }
    }
}
