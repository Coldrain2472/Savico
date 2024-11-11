namespace Savico.Services.Contracts
{
    public interface ITipService
    {
        Task<string> GetRandomTipAsync(); // get a random tip
    }
}
