using cm.frontend.core.Domain.Attributes;
using cm.frontend.core.Domain.Interfaces;
using Realms;

namespace cm.frontend.core.Domain.Models
{
    public class Member : RealmObject, ISyncableEntity
    {
        [Indexed]
        [IgnorePropertyMapping]
        public int Id { get; set; }

        [PrimaryKey]
        [Indexed]
        [IgnorePropertyMapping]
        public int LocalId { get; set; }

        public int ProfileId { get; set; }

        [IgnorePropertyMapping]
        public Profile Profile { get; set; }

        public int SchoolId { get; set; }

        [IgnorePropertyMapping]
        public School School { get; set; }

        public bool IsTeacher { get; set; }

        [IgnorePropertyMapping]
        public bool Synced { get; set; }
    }
}
