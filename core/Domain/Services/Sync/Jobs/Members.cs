using System.Threading.Tasks;

namespace cm.frontend.core.Domain.Services.Sync.Jobs
{
    public class Members : Base.Core<Domain.Models.Member>
    {
        public Members() : base(nameof(Members))
        {

        }

        public override async Task RebuildModel(int localId)
        {
            var membersRealm = new Services.Realms.Members();
            await membersRealm.WriteAsync(tempRealm =>
            {
                var item = tempRealm.Get(localId);

                var profilesRealm = new Services.Realms.Profiles();
                var profileId = item.ProfileId;
                var profileItem = profilesRealm.Get(x => x.Id == profileId);
                item.Profile = profileItem;

                var schoolsRealm = new Services.Realms.Schools();
                var schoolId = item.SchoolId;
                var schoolItem = schoolsRealm.Get(x => x.Id == schoolId);
                item.School = schoolItem;
            });
        }

        public override async Task UpdateModel(int localId)
        {
            var membersRealm = new Services.Realms.Members();
            await membersRealm.WriteAsync(tempRealm =>
            {
                var item = tempRealm.Get(localId);

                if (item.Profile != null)
                {
                    item.ProfileId = item.Profile.Id;
                }

                if (item.School != null)
                {
                    item.SchoolId = item.School.Id;
                }
            });
        }
    }
}
