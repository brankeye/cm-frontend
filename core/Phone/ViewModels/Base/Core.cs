using cm.frontend.core.Domain.Objects;
using cm.frontend.core.Phone.Services;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.ViewModels.Base
{
    public class Core
    {
        public virtual void OnAppearing()
        {
            
        }

        public virtual void OnDisappearing()
        {

        }

        public Context GetContext()
        {
            var contextCache = Domain.Services.Caches.Context.GetInstance();
            var currentContext = contextCache.Get("Context");
            return currentContext;
        }

        public Domain.Models.User GetCurrentUser()
        {
            var currentContext = GetContext();
            var usersRealm = new Domain.Services.Realms.Users();
            var user = usersRealm.Get(x => x.Username == currentContext.Username);
            return user;
        }

        public void SaveContext(Context context)
        {
            var contextCache = Domain.Services.Caches.Context.GetInstance();
            contextCache.Replace("Context", context);
        }

        public void SaveContext(string username, Domain.Objects.Token token, bool isAuthenticated)
        {
            var currentContext = GetContext();
            currentContext.Username = username;
            currentContext.AccessToken = token;
            currentContext.IsAuthenticated = isAuthenticated;
            SaveContext(currentContext);
        }

        public INavigation Navigation { get; set; }

        protected Services.Navigator Navigator => new Navigator();
    }
}
