using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cm.frontend.core.Domain.Interfaces
{
    public interface IEntity
    {
        int Id { get; set; }

        int LocalId { get; set; }
    }
}
