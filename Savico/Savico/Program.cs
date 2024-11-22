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

			app.MapControllerRoute(
				name: "areas",
				pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.MapRazorPages();

			app.Run();
		}
	}
}
