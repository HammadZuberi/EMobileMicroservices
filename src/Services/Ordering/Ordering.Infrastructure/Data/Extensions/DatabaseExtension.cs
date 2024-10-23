

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infrastructure.Data.Extensions
{
	public static class DatabaseExtension
	{

		public static async Task InitializeDatabaseAsync(this WebApplication app)
		{
			using var scope = app.Services.CreateScope();

			var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

			//context.Database.EnsureCreated();
			context.Database.MigrateAsync().GetAwaiter().GetResult();

			await SeedAsync(context);
		}

		private static async Task SeedAsync(ApplicationDbContext context)
		{
			await seedCustomerAsync(context);
			await seedProductAsync(context);
			await seedOrderandOrderItemsAsync(context);

		}

		private static async Task seedCustomerAsync(ApplicationDbContext context)
		{
			if (!await context.Customers.AnyAsync())
			{
				await context.Customers.AddRangeAsync(InitialData.Customers);
				await context.SaveChangesAsync();

			}
		}

		private static async Task seedProductAsync(ApplicationDbContext context)
		{
			if (!await context.Products.AnyAsync())
			{
				await context.Products.AddRangeAsync(InitialData.Products);
				await context.SaveChangesAsync();

			}
		}

		private static async Task seedOrderandOrderItemsAsync(ApplicationDbContext context)
		{

			if (!await context.Orders.AnyAsync())
			{
				await context.Orders.AddRangeAsync(InitialData.OrdersWithItems);
				await context.SaveChangesAsync();

			}
		}
	}
}
