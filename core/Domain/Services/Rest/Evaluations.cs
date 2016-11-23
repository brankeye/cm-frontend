using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cm.frontend.core.Domain.Services.Rest
{
    public class Evaluations : Base.Core<Domain.Models.Evaluation>
    {
        public Evaluations() : base("Evaluations")
        {
            
        }
    }
}
