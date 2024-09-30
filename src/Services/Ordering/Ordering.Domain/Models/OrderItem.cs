namespace Ordering.Domain.Models
{
	public class OrderItem : Entity<OrderItemID>
	{
		//can easly replace orderid with product id in guid so strongtype
		//typesaftey >2 refactor >3 readability
		//public OrderItem(Guid orderId, Guid productId, int quantity, decimal price)
		public OrderItem(OrderId orderId, ProductId productId, int quantity, decimal price)
		{
			Id = OrderItemID.of(Guid.NewGuid());
			OrderId = orderId;
			ProductId = productId;
			Quantity = quantity;
			Price = price;
		}
		public OrderId OrderId { get; private set; } = default!;
		public ProductId ProductId { get; private set; } = default!;
		public int Quantity { get; private set; } = default!;
		public decimal Price { get; private set; } = default!;
    }
}
