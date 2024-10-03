
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Ordering.Infrastructure.Data.Configuration
{
	internal class ProductConfiguration : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.HasKey(p => p.Id);

			//convert Value type to Db typer  representation
			builder.HasKey(c => c.Id);
			builder.Property(c => c.Id).HasConversion(
				productId => productId.Value,
				dbId => ProductId.of(dbId));


			builder.Property(c => c.Name).HasMaxLength(100).IsRequired();
		}
	}
}
