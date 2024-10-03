using System.Reflection;

namespace Ordering.Infrastructure.Data
{
	internal class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}

		//public DbSet<Customer> Customers { get; set; }
		public DbSet<Customer> Customers => Set<Customer>();
		public DbSet<Product> Products => Set<Product>();
		public DbSet<Order> Orders => Set<Order>();
		public DbSet<OrderItem> OrderItems => Set<OrderItem>();

		
		protected override void OnModelCreating(ModelBuilder builder)
		{

			//builder.Entity<Customer>().Property( c=> c.Name ).IsRequired().HasMaxLength(100);

			//to avaoid lenghty and SOLID priciples we can apply configuration from assembly using IEntityTypeConfiguration

			builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

			base.OnModelCreating(builder);
		}
	}
}
