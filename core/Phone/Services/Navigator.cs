using System.Threading.Tasks;
using cm.frontend.core.Domain.Services.Navigation.Base;
using cm.frontend.core.Phone.Views.Pages;
using cm.frontend.core.Phone.Views.Pages.Editors;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.Services
{
    public class Navigator : Core
    {
        public async Task PushProfileEditorPageAsync(INavigation navigation)
        {
            if (IsNavigating) return;

            var pageCache = Domain.Services.Caches.Pages.GetInstance();
            var cachedPage = pageCache.GetCachedOrNew<Views.Pages.Editors.Profile>(nameof(Views.Pages.Editors.Profile));
            await PushAsync(navigation, cachedPage);
        }

        public async Task PushDashboardPageAsync(INavigation navigation)
        {
            if (IsNavigating) return;

            var pageCache = Domain.Services.Caches.Pages.GetInstance();
            var cachedPage = pageCache.GetCachedOrNew<Views.Pages.Dashboard>(nameof(Views.Pages.Dashboard));
            await PushAsync(navigation, cachedPage);
        }
    }
}
