using System;
using System.Threading.Tasks;
using cm.frontend.core.Domain.Services.Navigation.Base;
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

        public async Task PushManagePageAsync(INavigation navigation)
        {
            if (IsNavigating) return;

            var pageCache = Domain.Services.Caches.Pages.GetInstance();
            var cachedPage = pageCache.GetCachedOrNew<Views.Pages.Manage>(nameof(Views.Pages.Manage));
            await PushAsync(navigation, cachedPage);
        }

        public async Task PushClassesPageAsync(INavigation navigation)
        {
            if (IsNavigating) return;

            var pageCache = Domain.Services.Caches.Pages.GetInstance();
            var cachedPage = pageCache.GetCachedOrNew<Views.Pages.Classes>(nameof(Views.Pages.Classes));
            await PushAsync(navigation, cachedPage);
        }

        public async Task PushClassEditorPageAsync(INavigation navigation)
        {
            if (IsNavigating) return;

            var pageCache = Domain.Services.Caches.Pages.GetInstance();
            var cachedPage = pageCache.GetCachedOrNew<Views.Pages.Editors.Class>(nameof(Views.Pages.Editors.Class));
            await PushAsync(navigation, cachedPage);
        }

        public async Task PushClassEditorPageAsync(INavigation navigation, int classLocalId)
        {
            if (IsNavigating) return;

            var pageCache = Domain.Services.Caches.Pages.GetInstance();
            var cachedPage = pageCache.GetCachedOrNew<Views.Pages.Editors.Class>(nameof(Views.Pages.Editors.Class));
            cachedPage.Initialize(classLocalId);
            await PushAsync(navigation, cachedPage);
        }

        public async Task PushCalendarPageAsync(INavigation navigation)
        {
            if (IsNavigating) return;

            var pageCache = Domain.Services.Caches.Pages.GetInstance();
            var cachedPage = pageCache.GetCachedOrNew<Views.Pages.Calendar>(nameof(Views.Pages.Calendar));
            await PushAsync(navigation, cachedPage);
        }

        public async Task PushCalendarClassPageAsync(INavigation navigation, int classLocalId, DateTimeOffset date)
        {
            if (IsNavigating) return;

            var pageCache = Domain.Services.Caches.Pages.GetInstance();
            var cachedPage = pageCache.GetCachedOrNew<Views.Pages.CalendarClass>(nameof(Views.Pages.CalendarClass));
            cachedPage.Initialize(classLocalId, date);
            await PushAsync(navigation, cachedPage);
        }

        public async Task PushStudentsPageAsync(INavigation navigation)
        {
            if (IsNavigating) return;

            var pageCache = Domain.Services.Caches.Pages.GetInstance();
            var cachedPage = pageCache.GetCachedOrNew<Views.Pages.Students>(nameof(Views.Pages.Students));
            await PushAsync(navigation, cachedPage);
        }

        public async Task PushEvaluationsPageAsync(INavigation navigation, int profileLocalId)
        {
            if (IsNavigating) return;

            var pageCache = Domain.Services.Caches.Pages.GetInstance();
            var cachedPage = pageCache.GetCachedOrNew<Views.Pages.Evaluations>(nameof(Views.Pages.Evaluations));
            cachedPage.Initialize(profileLocalId);
            await PushAsync(navigation, cachedPage);
        }

        public async Task PushEvaluationEditorPageAsync(INavigation navigation, int evaluationLocalId)
        {
            if (IsNavigating) return;

            var pageCache = Domain.Services.Caches.Pages.GetInstance();
            var cachedPage = pageCache.GetCachedOrNew<Views.Pages.Editors.Evaluation>(nameof(Views.Pages.Editors.Evaluation));
            cachedPage.Initialize(evaluationLocalId);
            await PushAsync(navigation, cachedPage);
        }

        public async Task PushStudentEvaluationsPageAsync(INavigation navigation, int profileLocalId)
        {
            if (IsNavigating) return;

            var pageCache = Domain.Services.Caches.Pages.GetInstance();
            var cachedPage = pageCache.GetCachedOrNew<Views.Pages.Details.StudentEvaluations>(nameof(Views.Pages.Details.StudentEvaluations));
            cachedPage.Initialize(profileLocalId);
            await PushAsync(navigation, cachedPage);
        }

        public async Task PushEvaluationPageAsync(INavigation navigation, int evalLocalId)
        {
            if (IsNavigating) return;

            var pageCache = Domain.Services.Caches.Pages.GetInstance();
            var cachedPage = pageCache.GetCachedOrNew<Views.Pages.Details.Evaluation>(nameof(Views.Pages.Details.Evaluation));
            cachedPage.Initialize(evalLocalId);
            await PushAsync(navigation, cachedPage);
        }

        public async Task PushEvaluationSectionEditorPageAsync(INavigation navigation, int sectionLocalId)
        {
            if (IsNavigating) return;

            var pageCache = Domain.Services.Caches.Pages.GetInstance();
            var cachedPage = pageCache.GetCachedOrNew<Views.Pages.Editors.EvaluationSection>(nameof(Views.Pages.Editors.EvaluationSection));
            cachedPage.Initialize(sectionLocalId);
            await PushAsync(navigation, cachedPage);
        }

        public async Task PushProfilePageAsync(INavigation navigation, int profileLocalId)
        {
            if (IsNavigating) return;

            var pageCache = Domain.Services.Caches.Pages.GetInstance();
            var cachedPage = pageCache.GetCachedOrNew<Views.Pages.Details.Profile>(nameof(Views.Pages.Details.Profile));
            cachedPage.Initialize(profileLocalId);
            await PushAsync(navigation, cachedPage);
        }

        public async Task PushSchoolPageAsync(INavigation navigation)
        {
            if (IsNavigating) return;

            var pageCache = Domain.Services.Caches.Pages.GetInstance();
            var cachedPage = pageCache.GetCachedOrNew<Views.Pages.Details.School>(nameof(Views.Pages.Details.School));
            await PushAsync(navigation, cachedPage);
        }

        public async Task PushLoginPageAsync(INavigation navigation)
        {
            if (IsNavigating) return;

            var pageCache = Domain.Services.Caches.Pages.GetInstance();
            var cachedPage = pageCache.GetCachedOrNew<Views.Pages.Login>(nameof(Views.Pages.Login));
            await PushAsync(navigation, cachedPage);
        }

        public new async Task PopAsync(INavigation navigation)
        {
            await base.PopAsync(navigation);
        }

        public new async Task PopModalAsync(INavigation navigation)
        {
            await base.PopModalAsync(navigation);
        }

        public new async Task PopToRootAsync(INavigation navigation)
        {
            await base.PopToRootAsync(navigation);
        }

        public new void RemovePage(INavigation navigation, Page page)
        {
            base.RemovePage(navigation, page);
        }
    }
}
