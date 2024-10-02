

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


		//responsible for its own state nad state of asssosiated items
		public static Order Create(OrderId id, CustomerId customerId, OrderName orderName, Address shippingAddress, Address billingAddress, Payment payment)
		{
			var order = new Order()
			{
				Id = id,
				CustomerId = customerId,
				OrderName = orderName,
				ShippingAddress = shippingAddress,
				BillingAddress = billingAddress,
				Payment = payment,
				Status = OrderStatus.Pending,
			};


			order.AddDomainEvents(new OrderCreatedEvent(order));
			return order;
		}
		public void  Update( OrderName orderName, Address shippingAddress, Address billingAddress, Payment payment,OrderStatus orderStatus)
		{
			OrderName = orderName;
			ShippingAddress = shippingAddress;
			BillingAddress = billingAddress;
			Payment = payment;
			Status =orderStatus;
			AddDomainEvents(new OrderUpdatedEvent(this));
		}

		public void AddItem(ProductId productId,int quantity, decimal price)
		{
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

			//internal const
			var orderitem = new OrderItem(Id,productId, quantity, price);
			_orderItems.Add(orderitem);
		}

		public void RemoveItem (ProductId productId)
		{
			var orderItem= _orderItems.FirstOrDefault(p=> p.ProductId ==  productId);
			if (orderItem != null)
			{
				_orderItems.Remove(orderItem);
			}

	}
}
