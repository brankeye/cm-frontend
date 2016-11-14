using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace cm.frontend.core.Domain.Services.Navigation.Base
{
    public class Core
    {
        protected static bool IsNavigating { get; private set; }

        protected async Task PushAsync(INavigation navigation, Page page, bool animate = true)
        {
            if (IsNavigating) return;

            IsNavigating = true;
            await navigation.PushAsync(page, animate);
            IsNavigating = false;
        }

        protected async Task PopAsync(INavigation navigation)
        {
            if (IsNavigating) return;

            IsNavigating = true;
            await navigation.PopAsync();
            IsNavigating = false;
        }

        protected async Task PushModalAsync(INavigation navigation, Page page, bool animate = true)
        {
            if (IsNavigating) return;

            IsNavigating = true;
            await navigation.PushModalAsync(page, animate);
            IsNavigating = false;
        }

        protected async Task PopModalAsync(INavigation navigation)
        {
            await navigation.PopModalAsync();
        }

        protected async Task PopToRootAsync(INavigation navigation)
        {
            await navigation.PopToRootAsync();
        }

        protected async Task<bool> PopToTypeAsync(INavigation navigation, Type type)
        {
            var foundPageAsType = false;
            var requestedPageIndex = -1;
            for (var i = navigation.NavigationStack.Count - 1; i >= 0; i--)
            {
                var page = navigation.NavigationStack[i];
                if (page.GetType() == type)
                {
                    requestedPageIndex = i;
                    foundPageAsType = true;
                    break;
                }
            }

            if (foundPageAsType)
            {
                var pages = new List<Page>();
                for (var i = requestedPageIndex + 1; i < navigation.NavigationStack.Count - 1; i++)
                {
                    var page = navigation.NavigationStack[i];
                    pages.Add(page);
                }

                foreach (var page in pages)
                {
                    navigation.RemovePage(page);
                }

                await PopAsync(navigation);
                return true;
            }

            return false;
        }

        protected void RemovePage(INavigation navigation, Page page)
        {
            navigation.RemovePage(page);
        }

        protected void InsertPageBefore(INavigation navigation, Page page, Page other)
        {
            navigation.InsertPageBefore(page, other);
        }

        protected Page GetStackNavigation(INavigation navigation, int stackNumber)
        {
            return navigation.NavigationStack[stackNumber];
        }
    }
}
