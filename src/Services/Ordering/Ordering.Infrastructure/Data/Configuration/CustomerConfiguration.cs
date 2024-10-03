using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ordering.Infrastructure.Data.Configuration
{
	public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
	{
		public void Configure(EntityTypeBuilder<Customer> builder)
		{
			//convert Value type to Db typer  representation
			builder.HasKey(c => c.Id);
			builder.Property(c => c.Id).HasConversion(
				customerId => customerId.Value,
				dbId => CustomerId.of(dbId));


			builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
			builder.Property(c => c.Email).HasMaxLength(255);

			//apply indexing on email and cehck unique
			builder.HasIndex(c => c.Email).IsUnique();

		}
	}
}
