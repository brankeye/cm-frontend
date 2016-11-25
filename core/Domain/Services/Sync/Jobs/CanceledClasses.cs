using System.Threading.Tasks;

namespace cm.frontend.core.Domain.Services.Sync.Jobs
{
    public class CanceledClasses : Base.Core<Domain.Models.CanceledClass>
    {
        public CanceledClasses() : base(nameof(CanceledClasses))
        {

        }

        public override async Task RebuildModel(int localId)
        {
            var canceledClassesRealm = new Services.Realms.CanceledClasses();
            await canceledClassesRealm.WriteAsync(tempRealm =>
            {
                var item = tempRealm.Get(localId);

                var classesRealm = new Services.Realms.Classes();
                var classId = item.ClassId;
                var classItem = classesRealm.Get(x => x.Id == classId);
                item.Class = classItem;
            });
        }

        public override async Task UpdateModel(int localId)
        {
            var canceledClassesRealm = new Services.Realms.CanceledClasses();
            await canceledClassesRealm.WriteAsync(tempRealm =>
            {
                var item = tempRealm.Get(localId);

                if (item.Class != null)
                {
                    item.ClassId = item.Class.Id;
                }
            });
        }
    }
}
