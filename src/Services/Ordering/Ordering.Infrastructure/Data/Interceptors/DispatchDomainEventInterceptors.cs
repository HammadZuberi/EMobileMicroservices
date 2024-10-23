
using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Ordering.Domain.Abstraction;

namespace Ordering.Infrastructure.Data.Interceptors
{
	public class DispatchDomainEventInterceptors(IMediator mediator) : SaveChangesInterceptor
	{


		public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
		{
			DispatchDomainEvents(eventData.Context).GetAwaiter().GetResult();

			return base.SavingChanges(eventData, result);
		}
		public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
		{

			await DispatchDomainEvents(eventData.Context);
			return await base.SavingChangesAsync(eventData, result, cancellationToken);
		}

		private async Task DispatchDomainEvents(DbContext? context)
		{

			if (context == null) return;

			//find entity with agrigrade which implement Domain events 
			var aggregates = context.ChangeTracker
				.Entries<IAggregate>()
				.Where(a => a.Entity.DomainEvents.Any())
			.Select(a => a.Entity);


			var domainEvents = aggregates
				.SelectMany(a => a.DomainEvents)
				.ToList();

			//clear DE to each entity for duplication
			aggregates.ToList().ForEach( a=> a.ClearDomainEvents());


			foreach (var domainEvent in domainEvents) {
				//dispatch DE
				await mediator.Publish(domainEvent);
			}

		}
	}
}
