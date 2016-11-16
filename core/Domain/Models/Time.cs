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
            Hour = hour;
            Minutes = minutes;
            Seconds = seconds;
        }

        // not used
        public int Id { get; set; }

        [PrimaryKey]
        [Indexed]
        public int LocalId { get; set; }

        public int Hour
        {
            get { return HourInternal; }
            set
            {
                if (value < 0 || value > 23)
                {
                    throw new Exception("Invalid hour.");
                }
                HourInternal = value;
            }
        }

        private int HourInternal { get; set; }

        public int Minutes
        {
            get { return MinutesInternal; }
            set
            {
                if (value < 0 || value > 59)
                {
                    throw new Exception("Invalid minutes.");
                }
                MinutesInternal = value;
            }
        }

        private int MinutesInternal { get; set; }

        public int Seconds
        {
            get { return SecondsInternal; }
            set
            {
                if (value < 0 || value > 59)
                {
                    throw new Exception("Invalid seconds.");
                }
                SecondsInternal = value;
            }
        }

        private int SecondsInternal { get; set; }
    }
}
