﻿using cm.frontend.core.Domain.Interfaces;
using Realms;

namespace cm.frontend.core.Domain.Models
{
    public class Account : RealmObject, IEntity
    {
        [Indexed]
        public int Id { get; set; }

        [PrimaryKey]
        [Indexed]
        public int LocalId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public int ProfileId { get; set; }

        public Profile Profile { get; set; }
    }
}
