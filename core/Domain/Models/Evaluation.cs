using System;
using cm.frontend.core.Domain.Attributes;
using cm.frontend.core.Domain.Interfaces;
using PropertyChanged;
using Realms;

namespace cm.frontend.core.Domain.Models
{
    [ImplementPropertyChanged]
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

        public int MemberId { get; set; }

        [IgnorePropertyMapping]
        public Member Member { get; set; }

        [Indexed]
        [IgnorePropertyMapping]
        public bool Synced { get; set; }
    }
}
