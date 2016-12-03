using System;
using cm.frontend.core.Domain.Attributes;
using cm.frontend.core.Domain.Interfaces;
using Realms;
using System.ComponentModel;

namespace cm.frontend.core.Domain.Models
{
    public class AttendanceRecord : RealmObject, ISyncableEntity, INotifyPropertyChanged
    {
        [Indexed]
        [IgnorePropertyMapping]
        public int Id { get; set; }

        [PrimaryKey]
        [Indexed]
        [IgnorePropertyMapping]
        public int LocalId { get; set; }

        public bool IsAttending { get; set; }

        public DateTimeOffset Date { get; set; }

        public int ClassId { get; set; }

        [IgnorePropertyMapping]
        public Class Class { get; set; }

        public int ProfileId { get; set; }

        [IgnorePropertyMapping]
        public Profile Profile { get; set; }

        [Indexed]
        [IgnorePropertyMapping]
        public bool Synced { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
