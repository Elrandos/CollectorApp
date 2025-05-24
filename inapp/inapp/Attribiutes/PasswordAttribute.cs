using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using inapp.Constants;

namespace inapp.Attribiutes
{
    public class PasswordAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is not string password || string.IsNullOrWhiteSpace(password))
                return false;

            return Regex.IsMatch(password, AppConstants.PasswordPattern);
        }
    }
}
