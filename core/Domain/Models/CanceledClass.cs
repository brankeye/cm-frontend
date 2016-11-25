using System;
using cm.frontend.core.Domain.Attributes;
using cm.frontend.core.Domain.Interfaces;
using Realms;

namespace cm.frontend.core.Domain.Models
{
    public class CanceledClass : RealmObject, ISyncableEntity
    {
        [Indexed]
        [IgnorePropertyMapping]
        public int Id { get; set; }

        [PrimaryKey]
        [Indexed]
        [IgnorePropertyMapping]
        public int LocalId { get; set; }

        public DateTimeOffset Date { get; set; }

        public int ClassId { get; set; }

        [IgnorePropertyMapping]
        public Class Class { get; set; }

        [IgnorePropertyMapping]
        public bool Synced { get; set; }
    }
}
