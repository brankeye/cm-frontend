using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cm.frontend.core.Domain.Utilities.Regex
{
    public class Expressions
    {
        public static readonly string DayRegex = @"^(?:Sunday?|Monday?|Tuesday?|Wednesday?|Thursday?|Friday?|Saturday?)$";

        public static readonly string EmailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
        @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

        // Minimum 8 characters at least 1 Alphabet and 1 Number and 1 Special Character
        public static readonly string PasswordRegex = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$";

        // phone regex, only XXX-XXX-XXXX is valid
        public static readonly string PhoneNumberRegex = @"^(\+0?1\s)?\(?\d{3}\)?[-]\d{3}[-]\d{4}$";
    }
}
