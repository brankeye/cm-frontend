using System;
using System.Threading.Tasks;
using cm.frontend.core.Domain.Services.Navigation.Base;
using cm.frontend.core.Phone.Views.Pages.Details;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.Services
{
    public class Navigator : Core
    {
        public async Task PushMasterDetailPageAsync(INavigation navigation)
        {
            if (IsNavigating) return;

            var pageCache = Domain.Services.Caches.Pages.GetInstance();
            var cachedPage = pageCache.GetCachedOrNew<Views.Pages.MasterDetail>("MasterDetail");
            await PushAsync(navigation, cachedPage);
        }

        public async Task PushProfileEditorPageAsync(INavigation navigation)
        {
            if (IsNavigating) return;

            var pageCache = Domain.Services.Caches.Pages.GetInstance();
            var cachedPage = pageCache.GetCachedOrNew<Views.Pages.Editors.Profile>("ProfileEditor");
            await PushAsync(navigation, cachedPage);
        }

        public async Task PushDashboardPageAsync(INavigation navigation)
        {
            if (IsNavigating) return;

            var pageCache = Domain.Services.Caches.Pages.GetInstance();
            var cachedPage = pageCache.GetCachedOrNew<Views.Pages.Dashboard>("Dashboard");
            await PushAsync(navigation, cachedPage);
        }

        public async Task PushManagePageAsync(INavigation navigation)
        {
            if (IsNavigating) return;

            var pageCache = Domain.Services.Caches.Pages.GetInstance();
            var cachedPage = pageCache.GetCachedOrNew<Views.Pages.Manage>("Manage");
            await PushAsync(navigation, cachedPage);
        }

        public async Task PushSchoolEditorPageAsync(INavigation navigation, string schoolName, bool isManaging = false)
        {
            if (IsNavigating) return;

            var pageCache = Domain.Services.Caches.Pages.GetInstance();
            var cachedPage = pageCache.GetCachedOrNew<Views.Pages.Editors.School>("SchoolEditor");
            cachedPage.Initialize(schoolName, isManaging);
            await PushAsync(navigation, cachedPage);
        }

        public async Task PushClassesPageAsync(INavigation navigation)
        {
            if (IsNavigating) return;

            var pageCache = Domain.Services.Caches.Pages.GetInstance();
            var cachedPage = pageCache.GetCachedOrNew<Views.Pages.Classes>("Classes");
            await PushAsync(navigation, cachedPage);
        }

        public async Task PushClassEditorPageAsync(INavigation navigation)
        {
            if (IsNavigating) return;

            var pageCache = Domain.Services.Caches.Pages.GetInstance();
            var cachedPage = pageCache.GetCachedOrNew<Views.Pages.Editors.Class>("ClassEditor");
            await PushAsync(navigation, cachedPage);
        }

        public async Task PushClassEditorPageAsync(INavigation navigation, int classLocalId)
        {
            if (IsNavigating) return;

            var pageCache = Domain.Services.Caches.Pages.GetInstance();
            var cachedPage = pageCache.GetCachedOrNew<Views.Pages.Editors.Class>("ClassEditor");
            cachedPage.Initialize(classLocalId);
            await PushAsync(navigation, cachedPage);
        }

        public async Task PushCalendarPageAsync(INavigation navigation)
        {
            if (IsNavigating) return;

            var pageCache = Domain.Services.Caches.Pages.GetInstance();
            var cachedPage = pageCache.GetCachedOrNew<Views.Pages.Calendar>("Calendar");
            await PushAsync(navigation, cachedPage);
        }

        public async Task PushCalendarClassPageAsync(INavigation navigation, int classLocalId, DateTimeOffset date)
        {
            if (IsNavigating) return;

            var pageCache = Domain.Services.Caches.Pages.GetInstance();
            var cachedPage = pageCache.GetCachedOrNew<CalendarClass>("CalendarClass");
            cachedPage.Initialize(classLocalId, date);
            await PushAsync(navigation, cachedPage);
        }

        public async Task PushStudentsPageAsync(INavigation navigation)
        {
            if (IsNavigating) return;

            var pageCache = Domain.Services.Caches.Pages.GetInstance();
            var cachedPage = pageCache.GetCachedOrNew<Views.Pages.Students>("Students");
            await PushAsync(navigation, cachedPage);
        }

        public async Task PushEvaluationsPageAsync(INavigation navigation, int profileLocalId)
        {
            if (IsNavigating) return;

            var pageCache = Domain.Services.Caches.Pages.GetInstance();
            var cachedPage = pageCache.GetCachedOrNew<Views.Pages.Evaluations>("Evaluations");
            cachedPage.Initialize(profileLocalId);
            await PushAsync(navigation, cachedPage);
        }

        public async Task PushExistingEvaluationEditorPageAsync(INavigation navigation, int evaluationLocalId)
        {
            if (IsNavigating) return;

            var pageCache = Domain.Services.Caches.Pages.GetInstance();
            var cachedPage = pageCache.GetCachedOrNew<Views.Pages.Editors.Evaluation>("EvaluationEditor");
            cachedPage.InitializeExisting(evaluationLocalId);
            await PushAsync(navigation, cachedPage);
        }

        public async Task PushNewEvaluationEditorPageAsync(INavigation navigation, int memberLocalId)
        {
            if (IsNavigating) return;

            var pageCache = Domain.Services.Caches.Pages.GetInstance();
            var cachedPage = pageCache.GetCachedOrNew<Views.Pages.Editors.Evaluation>("EvaluationEditor");
            cachedPage.InitializeNew(memberLocalId);
            await PushAsync(navigation, cachedPage);
        }

        public async Task PushStudentEvaluationsPageAsync(INavigation navigation, int profileLocalId)
        {
            if (IsNavigating) return;

            var pageCache = Domain.Services.Caches.Pages.GetInstance();
            var cachedPage = pageCache.GetCachedOrNew<Views.Pages.Details.StudentEvaluations>("StudentEvaluations");
            cachedPage.Initialize(profileLocalId);
            await PushAsync(navigation, cachedPage);
        }

        public async Task PushEvaluationPageAsync(INavigation navigation, int evalLocalId)
        {
            if (IsNavigating) return;

            var pageCache = Domain.Services.Caches.Pages.GetInstance();
            var cachedPage = pageCache.GetCachedOrNew<Views.Pages.Details.Evaluation>("Evaluation");
            cachedPage.Initialize(evalLocalId);
            await PushAsync(navigation, cachedPage);
        }

        public async Task PushEvaluationSectionEditorPageAsync(INavigation navigation, int sectionLocalId, int evaluationLocalId, bool isEditingExistingSection = true)
        {
            if (IsNavigating) return;

            var pageCache = Domain.Services.Caches.Pages.GetInstance();
            var cachedPage = pageCache.GetCachedOrNew<Views.Pages.Editors.EvaluationSection>("EvaluationSectionEditor");
            cachedPage.Initialize(sectionLocalId, evaluationLocalId, isEditingExistingSection);
            await PushAsync(navigation, cachedPage);
        }

        public async Task PushProfilePageAsync(INavigation navigation, int profileLocalId)
        {
            if (IsNavigating) return;

            var pageCache = Domain.Services.Caches.Pages.GetInstance();
            var cachedPage = pageCache.GetCachedOrNew<Views.Pages.Details.Profile>("Profile");
            cachedPage.Initialize(profileLocalId);
            await PushAsync(navigation, cachedPage);
        }

        public async Task PushSchoolPageAsync(INavigation navigation)
        {
            if (IsNavigating) return;

            var pageCache = Domain.Services.Caches.Pages.GetInstance();
            var cachedPage = pageCache.GetCachedOrNew<Views.Pages.Details.School>("School");
            await PushAsync(navigation, cachedPage);
        }

        public async Task PushLoginPageAsync(INavigation navigation)
        {
            if (IsNavigating) return;

            var pageCache = Domain.Services.Caches.Pages.GetInstance();
            var cachedPage = pageCache.GetCachedOrNew<Views.Pages.Login>("Login");
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
