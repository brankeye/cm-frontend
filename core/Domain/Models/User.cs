using cm.frontend.core.Domain.Attributes;
using cm.frontend.core.Domain.Interfaces;
using PropertyChanged;
using Realms;

namespace cm.frontend.core.Domain.Models
{
    public class User : RealmObject, ISyncableEntity
    {
        [Indexed]
        [IgnorePropertyMapping]
        public int Id { get; set; }

        [PrimaryKey]
        [Indexed]
        [IgnorePropertyMapping]
        public int LocalId { get; set; }

        public string Username { get; set; }

        public int ProfileId { get; set; }

        [IgnorePropertyMapping]
        public Profile Profile { get; set; }

        [Indexed]
        [IgnorePropertyMapping]
        public bool Synced { get; set; }
    }
}
