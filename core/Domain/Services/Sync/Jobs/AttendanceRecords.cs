using System.Threading.Tasks;

namespace cm.frontend.core.Domain.Services.Sync.Jobs
{
    public class AttendanceRecords : Base.Core<Domain.Models.AttendanceRecord>
    {
        public AttendanceRecords() : base(nameof(AttendanceRecords))
        {

        }

        public override async Task RebuildModel(int localId)
        {
            var attendanceRecordsRealm = new Services.Realms.AttendanceRecords();
            await attendanceRecordsRealm.WriteAsync(tempRealm =>
            {
                var item = tempRealm.Get(localId);

                var classesRealm = new Services.Realms.Classes();
                var classId = item.ClassId;
                var classItem = classesRealm.Get(x => x.Id == classId);
                item.Class = classItem;

                var profilesRealm = new Services.Realms.Profiles();
                var profileId = item.ProfileId;
                var profileItem = profilesRealm.Get(x => x.Id == profileId);
                item.Profile = profileItem;
            });
        }

        public override async Task UpdateModel(int localId)
        {
            var attendanceRecordsRealm = new Services.Realms.AttendanceRecords();
            await attendanceRecordsRealm.WriteAsync(tempRealm =>
            {
                var item = tempRealm.Get(localId);

                if (item.Class != null)
                {
                    item.ClassId = item.Class.Id;
                }

                if (item.Profile != null)
                {
                    item.ProfileId = item.Profile.Id;
                }
            });
        }
    }
}
