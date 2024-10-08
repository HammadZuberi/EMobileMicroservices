﻿

namespace Ordering.Domain.Abstraction
{
	///base calss 
	public abstract class Aggregate<TId> : Entity<TId>, IAggregate<TId>
	{
		private readonly List<IDomainEvent> _domainEvents = new();
		public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

		public void AddDomainEvents(IDomainEvent domainEvent)
		{
			_domainEvents.Add(domainEvent);
		}
		public IDomainEvent[] ClearDomainEvents()
		{

			IDomainEvent[] domainEvents = _domainEvents.ToArray();
			_domainEvents.Clear();
			return domainEvents;
		}
	}
}
