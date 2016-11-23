using cm.frontend.core.Domain.Interfaces;
using Realms;

namespace cm.frontend.core.Domain.Models
{
    public class Student : RealmObject, IEntity
    {
        [Indexed]
        public int Id { get; set; }

        [PrimaryKey]
        [Indexed]
        public int LocalId { get; set; }

        public int ProfileId { get; set; }

        public Profile Profile { get; set; }

        public int SchoolId { get; set; }

        public School School { get; set; }
    }
}
