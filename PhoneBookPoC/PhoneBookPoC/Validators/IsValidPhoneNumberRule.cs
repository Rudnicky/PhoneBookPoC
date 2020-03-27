using System.Text.RegularExpressions;

namespace PhoneBookPoC.Validators
{
    public class IsValidPhoneNumberRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var str = value as string;
            str = str.Replace(" ", "").Trim();

            return Regex.Match(str, @"^(\+[0-9]{11})$").Success;
        }
    }
}
