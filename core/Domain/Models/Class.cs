using System;
using cm.frontend.core.Domain.Interfaces;
using Realms;

namespace cm.frontend.core.Domain.Models
{
    public class Class : RealmObject, IEntity
    {
        [Indexed]
        public int Id { get; set; }

        [PrimaryKey]
        [Indexed]
        public int LocalId { get; set; }

        public string Name { get; set; }

        public string Day
        {
            get { return DayInternal; }
            set
            {
                if (value.Equals("Monday") ||
                    value.Equals("Tuesday") ||
                    value.Equals("Wednesday") ||
                    value.Equals("Thursday") ||
                    value.Equals("Friday") ||
                    value.Equals("Saturday") ||
                    value.Equals("Sunday"))
                {
                    DayInternal = value;
                }
                else
                {
                    throw new Exception("Invalid day.");
                }
            }
        }

        private string DayInternal { get; set; }

        public Time StartTime { get; set; }

        public Time EndTime { get; set; }
    }
}
