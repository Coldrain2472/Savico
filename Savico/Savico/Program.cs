namespace Savico
{
	using Microsoft.EntityFrameworkCore;
	using Savico.Core.Models;
	using Savico.Infrastructure;
	using Savico.Services.Contracts;
	using Savico.Services;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.Extensions.Configuration;
	using System;
	using Microsoft.Extensions.Hosting;
	using Microsoft.AspNetCore.Identity;

	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
			builder.Services.AddDbContext<SavicoDbContext>(options =>
				options.UseSqlServer(connectionString));

			builder.Services.AddDatabaseDeveloperPageExceptionFilter();

			// registering application services
			builder.Services.AddScoped<IBudgetService, BudgetService>();
			builder.Services.AddScoped<IIncomeService, IncomeService>();
			builder.Services.AddScoped<IExpenseService, ExpenseService>();
			builder.Services.AddScoped<IReportService, ReportService>();
			builder.Services.AddScoped<IGoalService, GoalService>();
			builder.Services.AddScoped<ITipService, TipService>();

			// configuring identity with roles
			builder.Services.AddDefaultIdentity<User>(options =>
			{
				options.Password.RequireDigit = true;
				options.SignIn.RequireConfirmedAccount = false;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireUppercase = false;
				options.Lockout.MaxFailedAccessAttempts = 5;
				options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
			})
				.AddRoles<IdentityRole>()
				.AddEntityFrameworkStores<SavicoDbContext>();

			builder.Services.AddControllersWithViews();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseMigrationsEndPoint();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error/500");
				app.UseStatusCodePagesWithRedirects("/Home/Error?statusCode={0}");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();
			
			app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}"); 

			app.UseAuthentication();
			app.UseAuthorization();

			await SeedRolesAndAdminAsync(app);

			app.MapControllerRoute(
	            name: "areas",
	            pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.MapRazorPages();

			app.Run();
		}

		public static async Task SeedRolesAndAdminAsync(WebApplication app)
		{
			using (var scope = app.Services.CreateScope())
			{
				var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
				var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

				string[] roles = { "Administrator", "User" };

				// seeding roles
				foreach (var role in roles)
				{
					if (!await roleManager.RoleExistsAsync(role))
					{
						await roleManager.CreateAsync(new IdentityRole(role));
					}
				}

				// seeding Admin user
				var adminEmail = "admin@admin.com";
				var adminUser = await userManager.FindByEmailAsync(adminEmail);

				if (adminUser == null)
				{
					adminUser = new User
					{
						UserName = "Admin",
						Email = adminEmail,
						FirstName = "TEST",
						LastName = "ADMIN",
						Currency = "USD",
						EmailConfirmed = true
					};

					var result = await userManager.CreateAsync(adminUser, "Admin@123");
					if (result.Succeeded)
					{
						await userManager.AddToRoleAsync(adminUser, "Administrator");
					}
				}
				else if (!await userManager.IsInRoleAsync(adminUser, "Administrator"))
				{
					await userManager.AddToRoleAsync(adminUser, "Administrator");
				}

				// seeding test user
				var userEmail = "testuser123@gmail.com";
				var testUser = await userManager.FindByEmailAsync(userEmail);

				if (testUser == null)
				{
					testUser = new User
					{
						UserName = "TestUser",
						Email = userEmail,
						FirstName = "Test",
						LastName = "User",
						Currency = "EUR",
						EmailConfirmed = true
					};

					var result = await userManager.CreateAsync(testUser, "Test@123");
					if (result.Succeeded)
					{
						await userManager.AddToRoleAsync(testUser, "User");
					}
				}
				else if (!await userManager.IsInRoleAsync(testUser, "User"))
				{
					await userManager.AddToRoleAsync(testUser, "User");
				}
			}
		}
	}
}
