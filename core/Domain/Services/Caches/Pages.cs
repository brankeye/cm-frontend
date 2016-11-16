using cm.frontend.core.Domain.Services.Caches.Base;
using Xamarin.Forms;

namespace cm.frontend.core.Domain.Services.Caches
{
    public class Pages : DataCache<Page>
    {
        private static Pages _instance;

        private Pages() { }

        public static Pages GetInstance()
        {
            return _instance ?? (_instance = new Pages());
        }

        public TPage GetCachedOrNew<TPage>(string key)
            where TPage : Page, new()
        {
            var page = Get(key) ?? new TPage();
            return (TPage)page;
        }

        public void Preload<TPage>(string key, TPage value)
            where TPage : Page, new()
        {
            if (Get(key) == null)
            {
                Add(key, value);
            }
        }
    }
}
