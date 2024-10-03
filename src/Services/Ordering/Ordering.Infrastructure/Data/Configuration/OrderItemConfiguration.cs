
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ordering.Infrastructure.Data.Configuration
{
	public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
	{
		public void Configure(EntityTypeBuilder<OrderItem> builder)
		{
			builder.HasKey(oi => oi.Id);
			//config of primary
			builder.Property(c => c.Id).HasConversion(
				orderItemid => orderItemid.Value,
				dbId => OrderItemID.of(dbId));


			builder.HasOne<Product>()
				.WithMany()
				.HasForeignKey(oi => oi.ProductId);

			builder.Property(oi => oi.Price).IsRequired();
			builder.Property(oi => oi.Quantity).IsRequired();
		}
	}
}
