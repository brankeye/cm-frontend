﻿using System;
using cm.frontend.core.Domain.Interfaces;
using Realms;

namespace cm.frontend.core.Domain.Models
{
    public class Evaluation : RealmObject, IEntity
    {
        [Indexed]
        public int Id { get; set; }

        [PrimaryKey]
        [Indexed]
        public int LocalId { get; set; }

        public string Name { get; set; }

        public DateTimeOffset Date { get; set; }

        public DateTimeOffset Time { get; set; }
    }
}
