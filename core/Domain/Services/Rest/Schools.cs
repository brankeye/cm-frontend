using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cm.frontend.core.Domain.Services.Rest
{
    public class Schools : Base.Core<Domain.Models.School>
    {
        public Schools() : base("Schools")
        {
            
        }
    }
}
