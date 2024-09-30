
namespace Ordering.Domain.ValueObjects
{
	public record CustomerId
	{
		public Guid Value { get; }


		private CustomerId(Guid value) => Value = value;
		//provide clear to create domain specifiic way customer id instance 
		public static CustomerId of(Guid value)
		{
			//domain null 
			ArgumentNullException.ThrowIfNull(value);
			if (value == Guid.Empty)
				throw new DomainException("CustomerId cannot be empty");
			return new CustomerId(value);
		}
	}
	public record OrderName
	{
		private const int defaultlenght = 10;
		public string Value { get; }

		private OrderName(string value) => Value = value;

		public static OrderName of(string value)
		{
			ArgumentNullException.ThrowIfNullOrWhiteSpace(value);
			ArgumentOutOfRangeException.ThrowIfNotEqual(value.Length, defaultlenght);

			return new OrderName(value);
		}
	}
	public record ProductId
	{
		public Guid Value { get; }

		private ProductId(Guid value) => Value = value;
		//provide clear to create domain specifiic way customer id instance 
		public static ProductId of(Guid value)
		{
			//domain null 
			ArgumentNullException.ThrowIfNull(value);
			if (value == Guid.Empty)
				throw new DomainException("ProductId cannot be empty");
			return new ProductId(value);
		}
	}
	public record OrderId
	{
		public Guid Value { get; }
		private OrderId(Guid value) => Value = value;
		//provide clear to create domain specifiic way customer id instance 
		public static OrderId of(Guid value)
		{
			//domain null 
			ArgumentNullException.ThrowIfNull(value);
			if (value == Guid.Empty)
				throw new DomainException("OrderId cannot be empty");
			return new OrderId(value);
		}
	}
}
