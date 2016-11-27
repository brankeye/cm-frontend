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

                var membersRealm = new Services.Realms.Members();
                var memberId = item.MemberId;
                var memberItem = membersRealm.Get(x => x.Id == memberId);
                item.Member = memberItem;
            });
        }

        public override async Task UpdateModel(int localId)
        {
            var evaluationsRealm = new Services.Realms.Evaluations();
            await evaluationsRealm.WriteAsync(tempRealm =>
            {
                var item = tempRealm.Get(localId);

                if (item.Member != null)
                {
                    item.MemberId = item.Member.Id;
                }
            });
        }
    }
}
