namespace Savico.Infrastructure
{
    using Microsoft.EntityFrameworkCore;
    using Savico.Core.Models;
    using Savico.Infrastructure.Data.Constants;
    using Savico.Infrastructure.Data.Contracts;

    public class SavicoDbContext : DbContext
    {
        public SavicoDbContext(DbContextOptions<SavicoDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; } = null!;

        public DbSet<Income> Incomes { get; set; } = null!;

        public DbSet<Goal> Goals { get; set; } = null!;

        public DbSet<Expense> Expenses { get; set; } = null!;

        public DbSet<Budget> Budgets { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure soft delete global filter
            modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<Income>().HasQueryFilter(i => !i.IsDeleted);
            modelBuilder.Entity<Goal>().HasQueryFilter(g => !g.IsDeleted);
            modelBuilder.Entity<Expense>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Budget>().HasQueryFilter(b => !b.IsDeleted);
        }

        public override int SaveChanges()
        {
            HandleSoftDelete();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            HandleSoftDelete();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void HandleSoftDelete()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Deleted && entry.Entity is ISoftDeletable deletableEntity)
                {
                    // Mark as soft-deleted instead of actually removing
                    entry.State = EntityState.Modified;
                    deletableEntity.IsDeleted = true;
                }
            }
        }
    }
}
