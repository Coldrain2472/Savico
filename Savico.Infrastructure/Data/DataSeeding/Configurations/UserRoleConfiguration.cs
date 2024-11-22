namespace Savico.Infrastructure.Data.DataSeeding.Configurations
{
	using Microsoft.AspNetCore.Identity;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
	{
		public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
		{
			builder.HasData(CreateUserRoles());
		}

		private IEnumerable<IdentityUserRole<string>> CreateUserRoles()
		{
			return new[]
			{
                // test user to User role
				 new IdentityUserRole<string>
				 {
					 UserId = "ad5a5ccb-2be1-4866-b151-0f09a58416f6",
					 RoleId = "9365c92c-dcb7-4c90-aa4c-c3460905eea0"
				 }, 

                // test admin to Admin role
                new IdentityUserRole<string>
				{
					UserId = "7c55600b-d374-4bfb-8352-a65bf7d44cc1",
					RoleId = "f2aaa3b1-3cd0-4384-b843-0c022bbfcf63"
				}
			};
		}
	}
}
