using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace cm.frontend.core.Domain.Services.Navigation
{
    public class Navigator : Base.Core
    {
        /*
        public async Task PushPageAsync(INavigation navigation)
        {
            if (IsNavigating) return;

            var pageCache = Services.Caches.Pages.GetInstance();
            var cachedPage = pageCache.GetCachedOrNew<Page>(nameof(Page));
            await PushAsync(navigation, cachedPage);
        }
        */
    }
}
