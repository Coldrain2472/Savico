namespace Savico.Infrastructure.Data.Contracts
{
    public interface ISoftDeletable
    {
        bool IsDeleted { get; set; }
    }
}
