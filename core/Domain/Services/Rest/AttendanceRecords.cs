using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cm.frontend.core.Domain.Services.Rest
{
    public class AttendanceRecords : Base.Core<Domain.Models.AttendanceRecord>
    {
        public AttendanceRecords() : base("AttendanceRecords")
        {
            
        }
    }
}
