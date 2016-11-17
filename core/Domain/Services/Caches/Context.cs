using cm.frontend.core.Domain.Services.Caches.Base;
using Xamarin.Forms;

namespace cm.frontend.core.Domain.Services.Caches
{
    public class Context : DataCache<Domain.Models.Local.Context>
    {
        private static Context _instance;

        private Context() { }

        public static Context GetInstance()
        {
            return _instance ?? (_instance = new Context());
        }
    }
}
