﻿using System.Threading.Tasks;

namespace cm.frontend.core.Domain.Services.Sync
{
    public class Synchronizer
    {
        public async void SyncAllAndContinue()
        {
            await SyncAll();
        }

        public async Task SyncAllAndWait()
        {
            await SyncAll();
        }

        public async void SyncPostsAndContinue()
        {
            await SyncPosts();
        }

        public async Task SyncPostsAndWait()
        {
            await SyncPosts();
        }

        private async Task SyncPosts()
        {
            // Tier 1
            var profilesSync = new Jobs.Profiles();
            var schoolsSync = new Jobs.Schools();

            // Tier 2
            var usersSync = new Jobs.Users();
            var membersSync = new Jobs.Members();
            var classesSync = new Jobs.Classes();

            // Tier 3
            var evaluationsSync = new Jobs.Evaluations();
            var canceledClassesSync = new Jobs.CanceledClasses();
            var attendanceRecordsSync = new Jobs.AttendanceRecords();

            // Tier 4
            var evaluationSectionsSync = new Jobs.EvaluationSections();

            // Get AccessToken
            var accessToken = GetAccessToken();

            // Sync Posts (first)
            await profilesSync.SyncPost(accessToken);
            await schoolsSync.SyncPost(accessToken);
            await usersSync.SyncPost(accessToken);
            await membersSync.SyncPost(accessToken);
            await classesSync.SyncPost(accessToken);
            await evaluationsSync.SyncPost(accessToken);
            await canceledClassesSync.SyncPost(accessToken);
            await attendanceRecordsSync.SyncPost(accessToken);
            await evaluationSectionsSync.SyncPost(accessToken);
        }

        private async Task SyncAll()
        {
            // Tier 1
            var profilesSync = new Jobs.Profiles();
            var schoolsSync = new Jobs.Schools();

            // Tier 2
            var usersSync = new Jobs.Users();
            var membersSync = new Jobs.Members();
            var classesSync = new Jobs.Classes();

            // Tier 3
            var evaluationsSync = new Jobs.Evaluations();
            var canceledClassesSync = new Jobs.CanceledClasses();
            var attendanceRecordsSync = new Jobs.AttendanceRecords();

            // Tier 4
            var evaluationSectionsSync = new Jobs.EvaluationSections();

            // Get AccessToken
            var accessToken = GetAccessToken();

            // Sync Posts (first)
            await profilesSync.SyncPost(accessToken);
            await schoolsSync.SyncPost(accessToken);
            await usersSync.SyncPost(accessToken);
            await membersSync.SyncPost(accessToken);
            await classesSync.SyncPost(accessToken);
            await evaluationsSync.SyncPost(accessToken);
            await canceledClassesSync.SyncPost(accessToken);
            await attendanceRecordsSync.SyncPost(accessToken);
            await evaluationSectionsSync.SyncPost(accessToken);

            // Sync Gets (second)
            await profilesSync.SyncGet(accessToken);
            await schoolsSync.SyncGet(accessToken);
            await usersSync.SyncGet(accessToken);
            await membersSync.SyncGet(accessToken);
            await classesSync.SyncGet(accessToken);
            await evaluationsSync.SyncGet(accessToken);
            await canceledClassesSync.SyncGet(accessToken);
            await attendanceRecordsSync.SyncGet(accessToken);
            await evaluationSectionsSync.SyncGet(accessToken);
        }

        private string GetAccessToken()
        {
            // Get AccessToken
            var contextCache = Domain.Services.Caches.Context.GetInstance();
            var currentContext = contextCache.Get("Context");
            var accessToken = currentContext.AccessToken.Access_Token;
            return accessToken;
        }
    }
}
