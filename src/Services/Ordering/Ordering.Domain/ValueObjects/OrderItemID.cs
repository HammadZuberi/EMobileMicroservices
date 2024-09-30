

namespace Ordering.Domain.ValueObjects
{
	public class OrderItemID
	{
		public Guid Value { get; }



		private OrderItemID(Guid value) => Value = value;
		//provide clear to create domain specifiic way customer id instance 
		public static OrderItemID of(Guid value)
		{
			//domain null 
			ArgumentNullException.ThrowIfNull(value);
			if (value == Guid.Empty)
				throw new DomainException("OrderItemID cannot be empty");
			return new OrderItemID(value);
		}
	}
}
