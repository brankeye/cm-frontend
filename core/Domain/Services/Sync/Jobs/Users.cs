using System.Threading.Tasks;

namespace cm.frontend.core.Domain.Services.Sync.Jobs
{
    public class Users : Base.Core<Domain.Models.User>
    {
        public Users() : base(nameof(Users))
        {

        }

        public override async Task RebuildModel(int localId)
        {
            var usersRealm = new Services.Realms.Users();
            await usersRealm.WriteAsync(tempRealm =>
            {
                var item = tempRealm.Get(localId);

                var profilesRealm = new Services.Realms.Profiles();
                var profileId = item.ProfileId;
                var profileItem = profilesRealm.Get(x => x.Id == profileId);
                item.Profile = profileItem;
            });
        }

        public override async Task UpdateModel(int localId)
        {
            var usersRealm = new Services.Realms.Users();
            await usersRealm.WriteAsync(tempRealm =>
            {
                var item = tempRealm.Get(localId);

                if (item.Profile != null)
                {
                    item.ProfileId = item.Profile.Id;
                }
            });
        }
    }
}
