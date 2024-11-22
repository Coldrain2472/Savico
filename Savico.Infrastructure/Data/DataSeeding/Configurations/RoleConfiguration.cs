namespace Savico.Infrastructure.Data.DataSeeding.Configurations
{
	using Microsoft.AspNetCore.Identity;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
	{
		public void Configure(EntityTypeBuilder<IdentityRole> builder)
		{
			var roles = CreateRoles();
			builder.HasData(roles);
		}

		private IEnumerable<IdentityRole> CreateRoles()
		{
			return new[]
			{
			new IdentityRole { Id = "9365c92c-dcb7-4c90-aa4c-c3460905eea0", Name = "User", NormalizedName = "USER" },
			new IdentityRole { Id = "f2aaa3b1-3cd0-4384-b843-0c022bbfcf63", Name = "Admin", NormalizedName = "ADMIN" }
			};
		}
	}
}
