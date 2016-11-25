using System.Threading.Tasks;

namespace cm.frontend.core.Domain.Services.Sync.Jobs
{
    public class Classes : Base.Core<Domain.Models.Class>
    {
        public Classes() : base(nameof(Classes))
        {

        }

        public override async Task RebuildModel(int localId)
        {
            var classesRealm = new Services.Realms.Classes();
            await classesRealm.WriteAsync(tempRealm =>
            {
                var item = tempRealm.Get(localId);

                var schoolsRealm = new Services.Realms.Schools();
                var profileId = item.SchoolId;
                var schoolItem = schoolsRealm.Get(x => x.Id == profileId);
                item.School = schoolItem;
            });
        }

        public override async Task UpdateModel(int localId)
        {
            var classesRealm = new Services.Realms.Classes();
            await classesRealm.WriteAsync(tempRealm =>
            {
                var item = tempRealm.Get(localId);

                if (item.School != null)
                {
                    item.SchoolId = item.School.Id;
                }
            });
        }
    }
}
