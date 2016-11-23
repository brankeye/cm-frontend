using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cm.frontend.core.Domain.Services.Rest
{
    public class Profiles : Base.Core<Domain.Models.Profile>
    {
        public Profiles() : base("Profiles")
        {
            
        }
    }
}
