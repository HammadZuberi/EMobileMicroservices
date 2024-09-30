namespace Ordering.Domain.Models
{
	public class Order : Aggregate<OrderId>
	{

		//1:N relation can not set property outside of the work
		private readonly List<OrderItem> _orderItems = new();

		public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();

		//changing GUID primitive type to strong concreate Type CustomerId value type
		public CustomerId CustomerId { get; private set; } = default!;
		public OrderName OrderName { get; private set; } = default!;
		//value object of Address
		public Address ShippingAddress { get; private set; } = default!;
		public Address BillingAddress { get; private set; } = default!;
		public Payment Payment { get; private set; } = default!;
		public OrderStatus Status { get; private set; } = OrderStatus.Pending;


		public decimal TotalPrice
		{
			get => OrderItems.Sum(x => x.Price * x.Quantity);
			private set { }
		}
	}
}
