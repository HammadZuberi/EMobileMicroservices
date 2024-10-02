
namespace Ordering.Domain.Event
{
	public record OrderUpdatedEvent(Order Order):IDomainEvent;
}
