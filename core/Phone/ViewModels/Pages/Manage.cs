﻿using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.ViewModels.Pages
{
    public class Manage : ViewModels.Base.Core
    {
        private async void Join()
        {
            var currentContext = GetContext();

            // get school by name
            var schoolsRestService = new Domain.Services.Rest.Schools();
            var postedSchool = await schoolsRestService.GetAsync(SchoolName, currentContext.AccessToken.Access_Token);

            if (postedSchool == null) return;

            currentContext.SchoolName = SchoolName;
            SaveContext(currentContext);

            var savedSchool = await SaveSchool(postedSchool);

            // get current profile
            var profile = GetCurrentUser().Profile;

            // post member
            var member = await PostMember(savedSchool, profile);

            // save member
            await SaveMember(member, savedSchool, profile);

            var synchronizer = new Domain.Services.Sync.Synchronizer();
            await synchronizer.SyncAllAndWait();

            // navigate to master detail page for students
            App.LaunchMasterDetailPage?.Invoke(this, EventArgs.Empty);
        }

        private async Task<Domain.Models.Member> PostMember(Domain.Models.School school, Domain.Models.Profile profile)
        {
            var membersRestService = new Domain.Services.Rest.Members();
            var member = new Domain.Models.Member
            {
                ProfileId = profile.Id,
                SchoolId = school.Id
            };
            var response = await membersRestService.PostAsync(member, GetContext().AccessToken.Access_Token);
            var result = await membersRestService.ParseResponseItem(response);
            return result;
        }

        private async Task<Domain.Models.School> SaveSchool(Domain.Models.School school)
        {
            var schoolsRealm = new Domain.Services.Realms.Schools();
            var schoolRemoteId = school.Id;
            var schoolLocalId = 0;
            await schoolsRealm.WriteAsync(realm =>
            {
                var localSchool = realm.Get(x => x.Id == schoolRemoteId);
                if (localSchool == null)
                {
                    realm.Manage(school);
                    school.Synced = true;
                    schoolLocalId = school.LocalId;
                }
                else
                {
                    schoolLocalId = localSchool.LocalId;
                }
                
            });
            var theSchool = schoolsRealm.Get(schoolLocalId);
            return theSchool;
        }

        private async Task<Domain.Models.Member> SaveMember(Domain.Models.Member memberModel, Domain.Models.School school, Domain.Models.Profile profile)
        {
            var membersRealm = new Domain.Services.Realms.Members();
            var profilesRealm = new Domain.Services.Realms.Profiles();
            var schoolsRealm = new Domain.Services.Realms.Schools();
            var profileLocalId = profile.LocalId;
            var schoolLocalId = school.LocalId;
            var memberLocalId = 0;
            await membersRealm.WriteAsync(realm =>
            {
                realm.Manage(memberModel);
                memberLocalId = memberModel.LocalId;
                memberModel.Profile = profilesRealm.Get(profileLocalId);
                memberModel.School = schoolsRealm.Get(schoolLocalId);
                memberModel.Synced = true;
            });
            var member = membersRealm.Get(memberLocalId);
            return member;
        }

        private async void Create()
        {
            await Navigator.PushSchoolEditorPageAsync(Navigation, SchoolName, true);
        }

        public string SchoolName { get; set; }

        public ICommand JoinCommand => _joinCommand ?? (_joinCommand = new Command(Join));
        private ICommand _joinCommand;

        public ICommand CreateCommand => _createCommand ?? (_createCommand = new Command(Create));
        private ICommand _createCommand;
    }
}
