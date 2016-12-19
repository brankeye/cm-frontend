using cm.frontend.core.Phone.ViewModels.Base;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.Views.Pages.Base
{
    public class PageController<TViewModel>
        where TViewModel : ViewModels.Base.Core, new()
    {
        public TViewModel ViewModel => _vm ?? (_vm = CurrentPage.BindingContext as TViewModel);
        private TViewModel _vm;

        public PageController(Page page)
        {
            CurrentPage = page;
            CurrentPage.BindingContext = new TViewModel();
            CurrentPage.SetBinding(VisualElement.NavigationProperty, new Binding("Navigation"));

            CurrentPage.Appearing += (sender, args) => ViewModel.OnAppearing();
            CurrentPage.Disappearing += (sender, args) => ViewModel.OnDisappearing();

            ViewModel.AlertRaised += ViewModel_OnAlertRaised;
            ViewModel.QuestionAlertRaised += ViewModel_OnQuestionAlertRaised;
        }

        private void ViewModel_OnAlertRaised(object sender, AlertRaisedEventArgs alertRaisedEventArgs)
        {
            var title = (string) alertRaisedEventArgs.Title;
            var message = (string) alertRaisedEventArgs.Message;
            var cancel = (string) alertRaisedEventArgs.Cancel;
            CurrentPage.DisplayAlert(title, message, cancel);
        }

        private void ViewModel_OnQuestionAlertRaised(object sender, QuestionAlertRaisedEventArgs alertRaisedEventArgs)
        {
            var title = (string) alertRaisedEventArgs.Title;
            var message = (string) alertRaisedEventArgs.Message;
            var accept = (string) alertRaisedEventArgs.Accept;
            var cancel = (string) alertRaisedEventArgs.Cancel;
            CurrentPage.DisplayAlert(title, message, accept, cancel);
        }

        public Page CurrentPage { get; set; }
    }
}
