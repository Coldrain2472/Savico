namespace Savico.Infrastructure.Data.Contracts
{
    public interface IsSoftDeletable
    {
        bool IsDeleted { get; set; }
    }
}
