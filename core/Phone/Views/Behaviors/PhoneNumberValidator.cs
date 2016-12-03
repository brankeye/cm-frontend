namespace cm.frontend.core.Phone.Views.Behaviors
{
    public class PhoneNumberValidator : RegexValidator
    {
        public PhoneNumberValidator() : base(Domain.Utilities.Regex.Expressions.PhoneNumberRegex)
        {
            
        }
    }
}
