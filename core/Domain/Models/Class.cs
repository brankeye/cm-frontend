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
            get { return DateTime.DayOfWeek.ToString(); }
            set
            {
                var dayOfWeek = DayOfWeek.Friday;
                var now = DateTimeOffset.Now;
                Enum.TryParse(value, out dayOfWeek);
                DateTime = new DateTimeOffset(now.Year, now.Month, (int) dayOfWeek, 0, 0, 0, TimeSpan.Zero); 
            }
        }

        private DateTimeOffset DateTime { get; set; }

        public Time StartTime { get; set; }

        public Time EndTime { get; set; }
    }
}
