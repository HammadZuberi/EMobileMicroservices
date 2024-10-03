
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Enum;

namespace Ordering.Infrastructure.Data.Configuration
{
	public class OrderConfiguration : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.HasKey(o => o.Id);

			//convert Value type to Db typer  representation
			builder.HasKey(c => c.Id);
			builder.Property(c => c.Id).HasConversion(
				orderId => orderId.Value,
				dbId => OrderId.of(dbId));

			//every order has a cx
			builder.HasOne<Customer>()
				.WithMany()
				.HasForeignKey(oi => oi.CustomerId)
				.IsRequired();

			//every order has many orderitems many to one
			builder.HasMany<OrderItem>()
				.WithOne()
				.HasForeignKey(o => o.OrderId);

			//builder.Property(c => c.OrderName).HasMaxLength(100).IsRequired();
			//for prop but ordername is of value type so use Complex property 

			builder.ComplexProperty(
				o => o.OrderName, namebuilder =>
				{
					namebuilder.Property(n => n.Value)
					.HasColumnName(nameof(OrderName))
					.HasMaxLength(100)
					.IsRequired();
				});

			//shipping
			builder.ComplexProperty(
				o => o.ShippingAddress, propbuilder =>
				{
					propbuilder.Property(a => a.FirstName)
					.HasMaxLength(100)
					.IsRequired();

					propbuilder.Property(a => a.LastName)
						.HasMaxLength(50)
						.IsRequired();

					propbuilder.Property(a => a.EmailAddress)
						.HasMaxLength(100)
						.IsRequired();

					propbuilder.Property(a => a.AddressLine)
						.HasMaxLength(180)
						.IsRequired();

					propbuilder.Property(a => a.Country).HasMaxLength(50);

					propbuilder.Property(a => a.State).HasMaxLength(50);

					propbuilder.Property(a => a.ZipCode)
						.HasMaxLength(50)
						.IsRequired();
				});

			//billing
			builder.ComplexProperty(
			o => o.BillingAddress, propbuilder =>
			{
				propbuilder.Property(a => a.FirstName)
				.HasMaxLength(100)
				.IsRequired();

				propbuilder.Property(a => a.LastName)
					.HasMaxLength(50)
					.IsRequired();

				propbuilder.Property(a => a.EmailAddress)
					.HasMaxLength(100)
					.IsRequired();

				propbuilder.Property(a => a.AddressLine)
					.HasMaxLength(180)
					.IsRequired();

				propbuilder.Property(a => a.Country).HasMaxLength(50);

				propbuilder.Property(a => a.State).HasMaxLength(50);

				propbuilder.Property(a => a.ZipCode)
					.HasMaxLength(50)
					.IsRequired();
			});

			//Payment

			builder.ComplexProperty(
			o => o.Payment, propbuilder =>
			{
				propbuilder.Property(n => n.CardName)
				.HasMaxLength(50)
				.IsRequired();

				propbuilder.Property(n => n.CardNumber)
				.HasMaxLength(24)
				.IsRequired();

				propbuilder.Property(n => n.Expiration)
				.HasMaxLength(10);

				propbuilder.Property(n => n.CVV)
				.HasMaxLength(3);

				propbuilder.Property(n => n.PaymentMethod);

			});

			//orderStatus
			//convert enum as Db column

			builder.Property(o => o.Status)
				.HasDefaultValue(OrderStatus.Draft)
				.HasConversion(s => s.ToString(),
				dbbstatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbbstatus));

			builder.Property(o => o.TotalPrice);

		}
	}
}
