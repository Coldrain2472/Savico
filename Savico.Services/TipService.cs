namespace Savico.Services
{
    using Microsoft.EntityFrameworkCore;
    using Savico.Infrastructure;
    using Savico.Services.Contracts;
    using System.Threading.Tasks;

    public class TipService : ITipService
    {
        private readonly SavicoDbContext context;

        public TipService(SavicoDbContext context)
        {
            this.context = context;
        }

        public async Task<string> GetRandomTipAsync()
        {
            var random = new Random();
            var count = await context.Tips.CountAsync();
            var randomIndex = random.Next(count);

            var tip = await context.Tips
                .Skip(randomIndex)
                .Take(1)
                .FirstOrDefaultAsync();

            return tip?.Content;
        }
    }
}
