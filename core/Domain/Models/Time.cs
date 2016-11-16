using System;
using cm.frontend.core.Domain.Interfaces;
using Realms;

namespace cm.frontend.core.Domain.Models
{
    public class Time : RealmObject, IEntity
    {
        public Time()
        {

        }

        public Time(int hour, int minutes, int seconds)
        {
            if (hour < 0 || hour > 23 ||
                minutes < 0 || minutes > 59 ||
                seconds < 0 || seconds > 59)
            {
                throw new Exception("Values for Time object are out of bounds.");
            }

            Hour = hour;
            Minutes = minutes;
            Seconds = seconds;
        }

        // not used
        public int Id { get; set; }

        [PrimaryKey]
        [Indexed]
        public int LocalId { get; set; }

        public int Hour { get; set; }

        public int Minutes { get; set; }

        public int Seconds { get; set; }

        
    }
}
