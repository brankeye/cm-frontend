using System;
using cm.frontend.core.Phone.ViewModels.Base;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.Views.Pages.Base
{
    public class ContentCore<TViewModel> : ContentPage
        where TViewModel : ViewModels.Base.Core, new()
    {
        public TViewModel ViewModel => _vm ?? (_vm = BindingContext as TViewModel);
        private TViewModel _vm;

        public ContentCore()
        {
            BindingContext = new TViewModel();
            SetBinding(NavigationProperty, new Binding("Navigation"));

            ViewModel.AlertRaised += ViewModel_OnAlertRaised;
            ViewModel.QuestionAlertRaised += ViewModel_OnQuestionAlertRaised;
        }

        private void ViewModel_OnAlertRaised(object sender, AlertRaisedEventArgs alertRaisedEventArgs)
        {
            var title = (string) alertRaisedEventArgs.Title;
            var message = (string) alertRaisedEventArgs.Message;
            var cancel = (string) alertRaisedEventArgs.Cancel;
            DisplayAlert(title, message, cancel);
        }

        private void ViewModel_OnQuestionAlertRaised(object sender, QuestionAlertRaisedEventArgs alertRaisedEventArgs)
        {
            var title = (string) alertRaisedEventArgs.Title;
            var message = (string) alertRaisedEventArgs.Message;
            var accept = (string) alertRaisedEventArgs.Accept;
            var cancel = (string) alertRaisedEventArgs.Cancel;
            DisplayAlert(title, message, accept, cancel);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnAppearing();
            ViewModel.OnDisappearing();
        }
    }

    public class MasterDetailCore<TViewModel> : MasterDetailPage
        where TViewModel : ViewModels.Base.Core, new()
    {
        public TViewModel ViewModel => _vm ?? (_vm = BindingContext as TViewModel);
        private TViewModel _vm;

        public MasterDetailCore()
        {
            BindingContext = new TViewModel();
            SetBinding(NavigationProperty, new Binding("Navigation"));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnAppearing();
            ViewModel.OnDisappearing();
        }
    }
}
