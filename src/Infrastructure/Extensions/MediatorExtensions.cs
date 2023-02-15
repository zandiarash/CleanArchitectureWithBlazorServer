using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Blazor.Application.Common.PublishStrategies;
using MediatR;

namespace CleanArchitecture.Blazor.Infrastructure.Extensions;
public static class MediatorExtensions
{
    public static async Task DispatchDomainEvents(this Publisher publisher, DbContext context,List<DomainEvent> deletedDomainEvents)
    {
        // If the delete domain events list has a value publish it first.
        if (deletedDomainEvents.Any())
        {
            foreach (var domainEvent in deletedDomainEvents)
                await publisher.Publish(domainEvent, PublishStrategy.ParallelNoWait);
        }

        var entities = context.ChangeTracker
            .Entries<BaseEntity>()
            .Where(e => e.Entity.DomainEvents.Any())
            .Select(e => e.Entity);

        var domainEvents = entities
            .SelectMany(e => e.DomainEvents)
            .ToList();

        entities.ToList().ForEach(e => e.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
            await publisher.Publish(domainEvent, PublishStrategy.ParallelNoWait);
    }
    
    
   
}