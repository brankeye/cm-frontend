using System;
using cm.frontend.core.Domain.Objects;
using cm.frontend.core.Phone.Services;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.ViewModels.Base
{
    public class Core
    {
        public event EventHandler<AlertRaisedEventArgs> AlertRaised;
        public event EventHandler<QuestionAlertRaisedEventArgs> QuestionAlertRaised;

        public virtual void OnAppearing()
        {
            RefreshData();
        }

        public virtual void OnDisappearing()
        {

        }

        public virtual void RefreshData()
        {

        }

        public virtual void DisplayAlert(string title, string message)
        {
            AlertRaised?.Invoke(this, new AlertRaisedEventArgs(title, message, "Ok"));
        }

        public virtual void DisplayQuestionAlert(string title, string message, string accept, string cancel)
        {
            QuestionAlertRaised?.Invoke(this, new QuestionAlertRaisedEventArgs(title, message, accept, cancel));
        }

        public Context GetContext()
        {
            var contextCache = Domain.Services.Caches.Context.GetInstance();
            var currentContext = contextCache.Get("Context");
            return currentContext;
        }

        public bool UserIsTeacher()
        {
            var currentUser = GetCurrentUser();
            var membersRealm = new Domain.Services.Realms.Members();
            var profile = currentUser.Profile;
            var member = membersRealm.Get(x => x.Profile == profile);
            return member.IsTeacher;
        }

        public Domain.Models.User GetCurrentUser()
        {
            var currentContext = GetContext();
            var usersRealm = new Domain.Services.Realms.Users();
            var username = currentContext.Username;
            var user = usersRealm.Get(x => x.Username == username);

            return user;
        }

        public Domain.Models.School GetCurrentSchool()
        {
            var currentContext = GetContext();
            var schoolsRealm = new Domain.Services.Realms.Schools();
            var schoolName = currentContext.SchoolName;
            var school = schoolsRealm.Get(x => x.Name == schoolName);
            return school;
        }

        public Domain.Models.Member GetCurrentMember()
        {
            var currentUser = GetCurrentUser();
            var membersRealm = new Domain.Services.Realms.Members();
            var profile = currentUser.Profile;
            var member = membersRealm.Get(x => x.Profile == profile);
            return member;
        }

        public void SaveContext(Context context)
        {
            var contextCache = Domain.Services.Caches.Context.GetInstance();
            contextCache.Replace("Context", context);
        }

        public void SaveContext(string username, Domain.Objects.Token token, bool isAuthenticated, string schoolName)
        {
            var currentContext = GetContext();
            currentContext.Username = username;
            currentContext.AccessToken = token;
            currentContext.IsAuthenticated = isAuthenticated;
            currentContext.SchoolName = schoolName;

            SaveContext(currentContext);
        }

        public INavigation Navigation { get; set; }

        protected Services.Navigator Navigator => new Navigator();
    }

    public class AlertRaisedEventArgs : EventArgs
    {
        public AlertRaisedEventArgs(object title, object message, object cancel)
        {
            Title = title;
            Message = message;
            Cancel = cancel;
        }

        public object Title { get; }

        public object Message { get; }

        public object Cancel { get; set; }
    }

    public class QuestionAlertRaisedEventArgs : AlertRaisedEventArgs
    {
        public QuestionAlertRaisedEventArgs(object title, object message, object accept, object cancel) : base(title, message, cancel)
        {
            Accept = accept;
        }

        public object Accept { get; }
    }
}
