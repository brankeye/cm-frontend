﻿using System;
using System.ComponentModel;
using cm.frontend.core.Domain.Attributes;
using cm.frontend.core.Domain.Interfaces;
using Realms;

namespace cm.frontend.core.Domain.Models
{
    public class Profile : RealmObject, ISyncableEntity, INotifyPropertyChanged
    {
        [Indexed]
        [IgnorePropertyMapping]
        public int Id { get; set; }

        [PrimaryKey]
        [Indexed]
        [IgnorePropertyMapping]
        public int LocalId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Ignored]
        [IgnorePropertyMapping]
        public string FullName => FirstName + " " + LastName;

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
        
        public DateTimeOffset StartDate { get; set; }

        public string Level { get; set; }

        [Indexed]
        [IgnorePropertyMapping]
        public bool Synced { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
