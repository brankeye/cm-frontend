namespace cm.frontend.core.Phone.Views.Behaviors
{
    public class DayValidator : RegexValidator
    {
        public DayValidator() : base(Domain.Utilities.Regex.Expressions.DayRegex)
        {
            
        }
    }
}
