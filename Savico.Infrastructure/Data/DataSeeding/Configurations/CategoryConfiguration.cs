namespace Savico.Infrastructure.Data.DataSeeding.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Savico.Infrastructure.Data.Models;

    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(this.SeedCategories());
        }

        private IEnumerable<Category> SeedCategories()
        {
            IEnumerable<Category> categories = new List<Category>()
            {
                new Category() {Id = 1, Name = "Utilities", },
                new Category() { Id = 2, Name = "Rent", },
                new Category() {Id = 3, Name = "Groceries", },
                new Category() {Id = 4, Name = "Entertainment", },
                new Category() {Id = 5, Name = "Mortgage", },
                new Category() {Id = 6, Name = "Property taxes", },
                new Category() { Id = 7,Name = "Insurance", },
                new Category() { Id = 8,Name = "Transportation", },
                new Category() { Id = 9, Name = "Health", },
                new Category() { Id = 10,Name = "Subscription", },
                new Category() {Id = 11, Name = "Bank loan",},
                new Category() {Id = 12, Name = "Donation"},
                new Category() {Id = 13, Name = "Clothing",}
                // can add many more -> vacation, home improvements, beauty&grooming, pet care
            };

            return categories;
        }
    }
}
