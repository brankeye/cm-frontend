using cm.frontend.core.Domain.Attributes;
using cm.frontend.core.Domain.Interfaces;
using PropertyChanged;
using Realms;

namespace cm.frontend.core.Domain.Models
{
    [ImplementPropertyChanged]
    public class School : RealmObject, ISyncableEntity
    {
        [Indexed]
        [IgnorePropertyMapping]
        public int Id { get; set; }

        [PrimaryKey]
        [Indexed]
        [IgnorePropertyMapping]
        public int LocalId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string Website { get; set; }

        public string PhoneNumber { get; set; }

        [Indexed]
        [IgnorePropertyMapping]
        public bool Synced { get; set; }
    }
}
