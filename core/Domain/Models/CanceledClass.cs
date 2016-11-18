using System;
using cm.frontend.core.Domain.Interfaces;
using Realms;

namespace cm.frontend.core.Domain.Models
{
    public class CanceledClass : RealmObject, IEntity
    {
        [Indexed]
        public int Id { get; set; }

        [PrimaryKey]
        [Indexed]
        public int LocalId { get; set; }

        public int ClassId { get; set; }

        public Class Class { get; set; }

        public DateTimeOffset Date { get; set; }
    }
}
