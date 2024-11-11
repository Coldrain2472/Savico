namespace Savico.Infrastructure
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Savico.Core.Models;
    using Savico.Infrastructure.Data.DataSeeding.Configurations;
    using Savico.Infrastructure.Data.Models;

    public class SavicoDbContext : IdentityDbContext<User>
    {
        public SavicoDbContext(DbContextOptions<SavicoDbContext> options) : base(options)
        {

        }

        public DbSet<Income> Incomes { get; set; } = null!;

        public DbSet<Goal> Goals { get; set; } = null!;

        public DbSet<Expense> Expenses { get; set; } = null!;

        public DbSet<Budget> Budgets { get; set; } = null!;

        //public DbSet<ExpenseCategory> ExpenseCategories { get; set; } = null!;

        public DbSet<Category> Categories { get; set; } = null!;

        public DbSet<Report> Reports { get; set; } = null!;

        public DbSet<Tip> Tips {  get; set; } = null!;

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
                .Property(p => p.TotalIncome)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Report>()
                .Property(p => p.TotalExpense)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Goal>()
                .Property(p => p.MonthlyContribution)
                .HasPrecision(18, 2);

            //explicitly defining the relationships

            modelBuilder.Entity<User>()
                .HasOne(u => u.Budget)
                .WithOne(u => u.User)
                .HasForeignKey<Budget>(b => b.UserId);

            modelBuilder.Entity<Income>()
                .HasKey(i => i.Id);

            modelBuilder.Entity<Income>()
                .HasOne(i => i.User)
                .WithMany(i => i.Incomes) // a user can have multiple incomes
                .HasForeignKey(i => i.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Expense>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<Expense>()
                .HasOne(e => e.User)
                .WithMany(u => u.Expenses) // a user can have multiple expenses
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Expense>()
                .HasOne(e => e.Budget)
                .WithMany(b => b.Expenses) // a budget can have multiple expenses
                .HasForeignKey(e => e.BudgetId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Expense>()
          .HasOne(e => e.Category)
          .WithMany(c => c.Expenses)         // each category has many expenses
          .HasForeignKey(e => e.CategoryId)
          .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Goal>()
                .HasKey(g => g.Id);

            modelBuilder.Entity<Goal>()
                .HasOne(g => g.User)
                .WithMany(g => g.Goals) // a user can have multiple goals
                .HasForeignKey(g => g.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Budget>()
                .HasKey(b => b.Id);

            modelBuilder.Entity<Category>()
           .HasKey(bc => bc.Id);

            //modelBuilder.Entity<ExpenseCategory>()
            //    .HasKey(ec => new { ec.CategoryId, ec.ExpenseId });

            //modelBuilder.Entity<ExpenseCategory>()
            // .HasOne(ec => ec.Expense)          
            // .WithMany(e => e.ExpenseCategories) 
            // .HasForeignKey(ec => ec.ExpenseId)  
            // .OnDelete(DeleteBehavior.Restrict);  

            //// Each ExpenseCategory can have one Category
            //modelBuilder.Entity<ExpenseCategory>()
            //    .HasOne(ec => ec.Category)        
            //    .WithMany(c => c.ExpenseCategories) 
            //    .HasForeignKey(ec => ec.CategoryId);

            modelBuilder.Entity<Tip>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<Report>()
            .HasKey(r => r.Id);

            modelBuilder.Entity<Report>()
                .HasOne(r => r.User)
                .WithMany(r => r.Reports) // a user can create multiple reports
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.ApplyConfiguration(new BudgetConfiguration());
            //modelBuilder.ApplyConfiguration(new ExpenseConfiguration());
            //modelBuilder.ApplyConfiguration(new GoalConfiguration());
            //modelBuilder.ApplyConfiguration(new IncomeConfiguration());
            //modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new TipConfiguration());

            // configure soft delete global filter
            modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<Income>().HasQueryFilter(i => !i.IsDeleted);
            modelBuilder.Entity<Goal>().HasQueryFilter(g => !g.IsDeleted);
            modelBuilder.Entity<Expense>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Budget>().HasQueryFilter(b => !b.IsDeleted);
            // modelBuilder.Entity<ExpenseCategory>().HasQueryFilter(ec => !ec.IsDeleted);
            modelBuilder.Entity<Report>().HasQueryFilter(r => !r.IsDeleted);
        }
    }
}
