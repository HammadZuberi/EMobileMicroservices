﻿
namespace Ordering.Domain.Abstraction
{
	//generic model
	public interface IAggregate<T> : IAggregate, IEntity<T>
	{

	}
	public interface IAggregate : IEntity
	{

		IReadOnlyList<IDomainEvent> DomainEvents { get; }

		IDomainEvent[] ClearDomainEvents();
	}
}
