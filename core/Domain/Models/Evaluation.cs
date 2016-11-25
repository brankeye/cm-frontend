using System;
using cm.frontend.core.Domain.Attributes;
using cm.frontend.core.Domain.Interfaces;
using Realms;

namespace cm.frontend.core.Domain.Models
{
    public class Evaluation : RealmObject, ISyncableEntity
    {
        [Indexed]
        [IgnorePropertyMapping]
        public int Id { get; set; }

        [PrimaryKey]
        [Indexed]
        [IgnorePropertyMapping]
        public int LocalId { get; set; }

        public string Name { get; set; }

        public DateTimeOffset Date { get; set; }

        public DateTimeOffset Time { get; set; }

        public int ProfileId { get; set; }

        [IgnorePropertyMapping]
        public Profile Profile { get; set; }

        [IgnorePropertyMapping]
        public bool Synced { get; set; }
    }
}
