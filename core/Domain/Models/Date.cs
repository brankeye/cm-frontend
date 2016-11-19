using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cm.frontend.core.Domain.Interfaces;
using Realms;

namespace cm.frontend.core.Domain.Models
{
    [Preserve(AllMembers = true)]
    public class Date : RealmObject, IEntity, IComparable
    {
        public Date()
        {
        }

        public Date(int year, int month, int day)
        {
            Year = year;
            Month = month;
            Day = day;
        }

        // not used
        public int Id { get; set; }

        [PrimaryKey]
        [Indexed]
        public int LocalId { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

        public int Day { get; set; }

        public string Display => ToString();

        public override string ToString()
        {
            var dt = new DateTime(Year, Month, Day, 0, 0, 0);
            var result = dt.ToString("dddd', 'MMMM' 'd', 'yyyy");
            return result;
        }

        public int CompareTo(object other)
        {
            var otherDate = (Date) other;
            var date1 = new DateTimeOffset(otherDate.Year, otherDate.Month, otherDate.Day, 0, 0, 0, TimeSpan.Zero);
            var date2 = new DateTimeOffset(Year, Month, Day, 0, 0, 0, TimeSpan.Zero);
            return date1.CompareTo(date2);
        }

        public override bool Equals(object obj)
        {
            var other = (Date) obj;
            return CompareTo(other) == 0;
        }
    }
}
