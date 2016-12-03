namespace cm.frontend.core.Phone.Views.Behaviors
{
    public class PasswordValidator : RegexValidator
    {
        public PasswordValidator() : base(Domain.Utilities.Regex.Expressions.PasswordRegex)
        {
            
        }
    }
}
