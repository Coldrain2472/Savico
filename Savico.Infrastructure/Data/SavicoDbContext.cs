namespace Savico.Infrastructure
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Savico.Core.Models;
    using Savico.Infrastructure.Data.Contracts;
    using Savico.Infrastructure.Data.Models;

    public class SavicoDbContext : IdentityDbContext
    {
        public SavicoDbContext(DbContextOptions<SavicoDbContext> options) : base(options)
        {

        }

        public DbSet<Income> Incomes { get; set; } = null!;

        public DbSet<Goal> Goals { get; set; } = null!;

        public DbSet<Expense> Expenses { get; set; } = null!;

        public DbSet<Budget> Budgets { get; set; } = null!;

        public DbSet<BudgetCategory> BudgetCategories { get; set; } = null!;

        public DbSet<Report> Reports { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // decimal precision
            modelBuilder.Entity<Budget>()
                .Property(p => p.TotalAmount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Expense>()
                .Property(p => p.Amount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Goal>()
                .Property(p => p.CurrentAmount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Goal>()
                .Property(p => p.TargetAmount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Income>()
                .Property(p => p.Amount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Report>()
                .Property(p=>p.TotalIncome)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Report>()
                .Property(p=>p.TotalExpense)
                .HasPrecision(18, 2);

            //explicitly defining the relationships

            modelBuilder.Entity<Income>()
                .HasKey(i => i.Id);

            modelBuilder.Entity<Income>()
                .HasOne(i => i.User)
                .WithMany() // assuming a user can have multiple incomes
                .HasForeignKey(i => i.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Expense>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<Expense>()
                .HasOne(e => e.User)
                .WithMany() // a user can have multiple expenses
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Expense>()
                .HasOne(e => e.Budget)
                .WithMany(b => b.Expenses) // a budget can have multiple expenses
                .HasForeignKey(e => e.BudgetId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Goal>()
                .HasKey(g => g.Id);

            modelBuilder.Entity<Goal>()
                .HasOne(g => g.User)
                .WithMany() // a user can have multiple goals
                .HasForeignKey(g => g.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Budget>()
                .HasKey(b => b.Id);

            modelBuilder.Entity<Budget>()
                .HasOne(b => b.User)
                .WithMany() // a user can have multiple budgets
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<BudgetCategory>()
           .HasKey(bc => bc.Id);

            modelBuilder.Entity<BudgetCategory>()
                .HasMany(b => b.Budgets)
                .WithOne()
                .HasForeignKey(b => b.Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Report>()
                .HasKey(r => r.Id);

            modelBuilder.Entity<Report>()
                .HasOne(r => r.User)
                .WithMany() // a user can create multiple reports
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.ApplyConfiguration(new BudgetConfiguration());
            //modelBuilder.ApplyConfiguration(new ExpenseConfiguration());
            //modelBuilder.ApplyConfiguration(new GoalConfiguration());
            //modelBuilder.ApplyConfiguration(new IncomeConfiguration());
            //modelBuilder.ApplyConfiguration(new UserConfiguration());

            // configure soft delete global filter
            modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<Income>().HasQueryFilter(i => !i.IsDeleted);
            modelBuilder.Entity<Goal>().HasQueryFilter(g => !g.IsDeleted);
            modelBuilder.Entity<Expense>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Budget>().HasQueryFilter(b => !b.IsDeleted);
            modelBuilder.Entity<BudgetCategory>().HasQueryFilter(bc => !bc.IsDeleted);
            modelBuilder.Entity<Report>().HasQueryFilter(r => !r.IsDeleted);
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
                    // mark as soft-deleted instead of actually removing
                    entry.State = EntityState.Modified;
                    deletableEntity.IsDeleted = true;
                }
            }
        }
    }
}
