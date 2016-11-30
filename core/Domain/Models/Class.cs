using System;
using cm.frontend.core.Domain.Attributes;
using cm.frontend.core.Domain.Interfaces;
using PropertyChanged;
using Realms;

namespace cm.frontend.core.Domain.Models
{
    [ImplementPropertyChanged]
    public class Class : RealmObject, ISyncableEntity
    {
        [Indexed]
        [IgnorePropertyMapping]
        public int Id { get; set; }

        [PrimaryKey]
        [Indexed]
        [IgnorePropertyMapping]
        public int LocalId { get; set; }

        public string Name { get; set; }

        public string Day { get; set; }

        public DateTimeOffset StartTime { get; set; }

        public DateTimeOffset EndTime { get; set; }

        public int SchoolId { get; set; }

        [IgnorePropertyMapping]
        public School School { get; set; }

        [Indexed]
        [IgnorePropertyMapping]
        public bool Synced { get; set; }
    }
}
