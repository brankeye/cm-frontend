using System.Collections.Generic;

namespace cm.frontend.core.Domain.Interfaces
{
    public interface IStore<TModel>
        where TModel : IEntity, new()
    {
        IEnumerable<TModel> Get();
    }
}
