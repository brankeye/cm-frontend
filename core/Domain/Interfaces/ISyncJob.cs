using System.Threading.Tasks;

namespace cm.frontend.core.Domain.Interfaces
{
    public interface ISyncJob
    {
        Task RebuildModel(int localId);

        Task UpdateModel(int localId);
    }
}
