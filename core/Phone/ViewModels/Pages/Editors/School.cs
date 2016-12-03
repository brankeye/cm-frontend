using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using cm.frontend.core.Domain.Extensions.NotifyPropertyChanged;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.ViewModels.Pages.Editors
{
    public class School : ViewModels.Base.Core, INotifyPropertyChanged
    {
        public School()
        {
            IsEditingExistingSchool = true;
        }

        public void Initialize(string schoolName, bool isManaging)
        {
            IsManaging = isManaging;
            SchoolName = schoolName;
            IsEditingExistingSchool = false;
        }

        public override void OnAppearing()
        {
            SchoolModel = new Domain.Models.School();
            if (IsEditingExistingSchool)
            {
                var mapper = new Domain.Utilities.PropertyMapper();
                var school = GetCurrentSchool();
                mapper.Map<Domain.Models.School>(school, SchoolModel);
            }
        }

        private async Task SaveNewSchool()
        {
            var currentContext = GetContext();

            // post school
            var postedSchool = await PostSchool(currentContext.AccessToken.Access_Token);

            currentContext.SchoolName = postedSchool.Name;
            SaveContext(currentContext);

            // post member
            var profile = GetCurrentUser().Profile;
            var member = new Domain.Models.Member
            {
                ProfileId = profile.Id,
                SchoolId = postedSchool.Id,
                IsTeacher = IsManaging
            };
            var postedMember = await PostMember(currentContext.AccessToken.Access_Token, member);

            // save school
            var savedSchool = await SaveSchool(postedSchool);

            // save member
            var savedMember = await SaveMember(postedMember, profile, savedSchool);

            // navigate to dashboard
            App.LaunchMasterDetailPage?.Invoke(this, EventArgs.Empty);
        }

        private async Task SaveExistingSchool()
        {
            var schoolsRealm = new Domain.Services.Realms.Schools();
            var schoolLocalId = GetCurrentSchool().LocalId;
            await schoolsRealm.WriteAsync(realm =>
            {
                var school = realm.Get(schoolLocalId);
                var mapper = new Domain.Utilities.PropertyMapper();
                mapper.Map<Domain.Models.School>(SchoolModel, school);
                school.Synced = false;
            });

            var synchronizer = new Domain.Services.Sync.Synchronizer();
            await synchronizer.SyncPostsAndWait();

            await Navigator.PopAsync(Navigation);
        }

        private async void SaveSchool()
        {
            if (!Validate()) return;

            if (IsEditingExistingSchool)
            {
                await SaveExistingSchool();
            }
            else
            {
                await SaveNewSchool();
            }
        }

        private async Task<Domain.Models.School> PostSchool(string accessToken)
        {
            var schoolsRestService = new Domain.Services.Rest.Schools();
            var mapper = new Domain.Utilities.PropertyMapper();
            var schoolObject = new Domain.Models.School();
            mapper.Map<Domain.Models.School>(SchoolModel, schoolObject);
            var httpResponse = await schoolsRestService.PostAsync(schoolObject, accessToken);
            var school = await schoolsRestService.ParseResponseItem(httpResponse);
            return school;
        }

        private async Task<Domain.Models.Member> PostMember(string accessToken, Domain.Models.Member memberModel)
        {
            var membersRestService = new Domain.Services.Rest.Members();
            var httpResponse = await membersRestService.PostAsync(memberModel, accessToken);
            var member = await membersRestService.ParseResponseItem(httpResponse);
            return member;
        }

        private async Task<Domain.Models.School> SaveSchool(Domain.Models.School school)
        {
            var schoolsRealm = new Domain.Services.Realms.Schools();
            var schoolLocalId = 0;
            await schoolsRealm.WriteAsync(realm =>
            {
                realm.Manage(school);
                schoolLocalId = school.LocalId;
                school.Synced = true;
            });
            var savedProfile = schoolsRealm.Get(schoolLocalId);
            return savedProfile;
        }

        // user => member // profile => school
        private async Task<Domain.Models.Member> SaveMember(Domain.Models.Member memberModel, Domain.Models.Profile profileModel, Domain.Models.School schoolModel)
        {
            var membersRealm = new Domain.Services.Realms.Members();
            var profilesRealm = new Domain.Services.Realms.Profiles();
            var schoolsRealm = new Domain.Services.Realms.Schools();
            var memberLocalId = 0;
            var profileLocalId = profileModel.LocalId;
            var schoolLocalId = schoolModel.LocalId;
            await membersRealm.WriteAsync(realm =>
            {
                realm.Manage(memberModel);
                memberModel.Profile = profilesRealm.Get(profileLocalId);
                memberModel.ProfileId = memberModel.Profile.Id;
                memberModel.School = schoolsRealm.Get(schoolLocalId);
                memberModel.SchoolId = memberModel.School.Id;
                memberLocalId = memberModel.LocalId;
                memberModel.Synced = true;
            });
            var savedMember = membersRealm.Get(memberLocalId);
            return savedMember;
        }

        private bool Validate()
        {
            if (!IsInputEmpty())
            {
                DisplayAlert("Invalid input", "Cannot be empty.");
                return false;
            }

            if (!IsEmailValid)
            {
                DisplayAlert("Invalid email", "Must be formatted correctly, as in 'abc123@gmail.com', for example.");
                return false;
            }

            if (!IsPhoneNumberValid)
            {
                DisplayAlert("Invalid phone number", "Must be of the form XXX-XXX-XXXX.");
                return false;
            }

            return true;
        }

        private bool IsInputEmpty()
        {
            if (string.IsNullOrWhiteSpace(SchoolModel.Name) ||
                string.IsNullOrWhiteSpace(SchoolModel.Address) ||
                string.IsNullOrWhiteSpace(SchoolModel.Website))
            {
                return true;
            }
            return false;
        }

        private async void Cancel()
        {
            await Navigator.PopAsync(Navigation);
        }

        public bool IsEditingExistingSchool { get; set; }

        public bool IsManaging { get; set; }

        public string SchoolName { get; set; }

        public bool IsEmailValid { get; set; }

        public bool IsPhoneNumberValid { get; set; }

        public Domain.Models.School SchoolModel
        {
            get { return _schoolModel; }
            set { this.SetProperty(ref _schoolModel, value, PropertyChanged); }
        }
        private Domain.Models.School _schoolModel;

        public ICommand SaveCommand => _saveCommand ?? (_saveCommand = new Command(SaveSchool));
        private ICommand _saveCommand;

        public ICommand CancelCommand => _cancelCommand ?? (_cancelCommand = new Command(Cancel));
        private ICommand _cancelCommand;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
