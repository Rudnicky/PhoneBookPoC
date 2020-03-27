using System.Linq;

namespace PhoneBookPoC.Validators
{
    public class DoesNotContainNumberRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var str = value as string;

            return str.All(char.IsLetter);
        }
    }
}
