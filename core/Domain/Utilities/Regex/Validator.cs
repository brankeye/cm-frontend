using System;
using System.Text.RegularExpressions;

namespace cm.frontend.core.Domain.Utilities.Regex
{
    public class Validator
    {
        public static bool IsPhoneNumberValid(string phoneNumber)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(phoneNumber, Domain.Utilities.Regex.Expressions.PhoneNumberRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }

        public static bool IsDayValid(string day)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(day, Domain.Utilities.Regex.Expressions.DayRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }

        public static bool IsEmailValid(string email)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(email, Domain.Utilities.Regex.Expressions.EmailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }

        public static bool IsPasswordValid(string password)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(password, Domain.Utilities.Regex.Expressions.PasswordRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
    }
}
