using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using inapp.Constants;

namespace inapp.Attribiutes
{
    public class ValidationEmailAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is not string email || string.IsNullOrWhiteSpace(email))
                return false;

            return Regex.IsMatch(email, AppConstants.EmailPattern, RegexOptions.IgnoreCase);
        }
    }
}
