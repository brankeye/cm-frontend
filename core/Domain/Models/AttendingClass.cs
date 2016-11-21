using System;
using cm.frontend.core.Domain.Interfaces;
using Realms;

namespace cm.frontend.core.Domain.Models
{
    public class AttendingClass : RealmObject, IEntity
    {
        [Indexed]
        public int Id { get; set; }

        [PrimaryKey]
        [Indexed]
        public int LocalId { get; set; }

        public bool IsAttending { get; set; }

        public DateTimeOffset Date { get; set; }

        public int ClassId { get; set; }

        public Class Class { get; set; }

        public int ProfileId { get; set; }

        public Profile Profile { get; set; }
    }
}
