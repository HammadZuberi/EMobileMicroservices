using MediatR;

namespace Ordering.Domain.Abstraction
{
	public interface IDomainEvent : INotification
	{
		//aalow domain event to maitr lib

		//domain event pattern

		Guid EventId => Guid.NewGuid();

		public DateTime OccuredOn => DateTime.UtcNow;

		public string EventType => GetType().AssemblyQualifiedName;

	}
}
