namespace cm.frontend.core.Domain.Interfaces
{
    public interface ISyncableEntity : IEntity
    {
        bool Synced { get; set; }
    }
}
