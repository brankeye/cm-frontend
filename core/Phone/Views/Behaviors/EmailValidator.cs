namespace cm.frontend.core.Phone.Views.Behaviors
{
    public class EmailValidator : RegexValidator
    {
        public EmailValidator() : base(Domain.Utilities.Regex.Expressions.EmailRegex)
        {
            
        }
    }
}
