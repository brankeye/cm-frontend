using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cm.frontend.core.Domain.Services.Rest
{
    public class Members : Base.Core<Domain.Models.Member>
    {
        public Members() : base("Members")
        {
            
        }
    }
}
