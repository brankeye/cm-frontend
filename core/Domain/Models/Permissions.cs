using cm.frontend.core.Domain.Interfaces;
using Realms;

namespace cm.frontend.core.Domain.Models
{
    public class Permissions : RealmObject, IEntity
    {
        [Indexed]
        public int Id { get; set; }

        [PrimaryKey]
        [Indexed]
        public int LocalId { get; set; }

        public bool CanManageClasses { get; set; }

        public bool CanManageCalendar { get; set; }

        public bool CanManageEvaluations { get; set; }

        public bool CanManageStudents { get; set; }

        public int StudentId { get; set; }

        public Student Student { get; set; }
    }
}
