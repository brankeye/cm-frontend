using System.Threading.Tasks;

namespace cm.frontend.core.Domain.Services.Sync.Jobs
{
    public class Evaluations : Base.Core<Domain.Models.Evaluation>
    {
        public Evaluations() : base(nameof(Evaluations))
        {

        }

        public override async Task RebuildModel(int localId)
        {
            var evaluationsRealm = new Services.Realms.Evaluations();
            await evaluationsRealm.WriteAsync(tempRealm =>
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
            var evaluationsRealm = new Services.Realms.Evaluations();
            await evaluationsRealm.WriteAsync(tempRealm =>
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
