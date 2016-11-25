using System.Threading.Tasks;

namespace cm.frontend.core.Domain.Services.Sync
{
    public class Synchronizer
    {
        public async Task SyncAll()
        {
            // Tier 1
            var profilesSync = new Jobs.Profiles();
            var schoolsSync = new Jobs.Schools();

            // Tier 2
            var usersSync = new Jobs.Users();
            var membersSync = new Jobs.Members();
            var evaluationsSync = new Jobs.Evaluations();
            var classesSync = new Jobs.Classes();

            // Tier 3
            var evaluationSectionsSync = new Jobs.EvaluationSections();
            var canceledClassesSync = new Jobs.CanceledClasses();
            var attendanceRecordsSync = new Jobs.AttendanceRecords();

            // Get AccessToken
            var contextCache = Domain.Services.Caches.Context.GetInstance();
            var currentContext = contextCache.Get("Context");
            var accessToken = currentContext.AccessToken.Access_Token;

            // Sync Posts (first)
            await profilesSync.SyncPost(accessToken);
            await schoolsSync.SyncPost(accessToken);
            await usersSync.SyncPost(accessToken);
            await membersSync.SyncPost(accessToken);
            await evaluationsSync.SyncPost(accessToken);
            await classesSync.SyncPost(accessToken);
            await evaluationSectionsSync.SyncPost(accessToken);
            await canceledClassesSync.SyncPost(accessToken);
            await attendanceRecordsSync.SyncPost(accessToken);

            // Sync Gets (second)
            await profilesSync.SyncGet(accessToken);
            await schoolsSync.SyncGet(accessToken);
            await usersSync.SyncGet(accessToken);
            await membersSync.SyncGet(accessToken);
            await evaluationsSync.SyncGet(accessToken);
            await classesSync.SyncGet(accessToken);
            await evaluationSectionsSync.SyncGet(accessToken);
            await canceledClassesSync.SyncGet(accessToken);
            await attendanceRecordsSync.SyncGet(accessToken);
        }
    }
}
