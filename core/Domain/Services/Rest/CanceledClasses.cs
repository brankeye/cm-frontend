using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cm.frontend.core.Domain.Services.Rest
{
    public class CanceledClasses : Base.Core<Domain.Models.CanceledClass>
    {
        public CanceledClasses() : base("CanceledClasses")
        {
            
        }
    }
}
