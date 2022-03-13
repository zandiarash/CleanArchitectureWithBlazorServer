// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.ShippingOrders.EventHandlers;

    public class ShippingOrderCreatedEventHandler : INotificationHandler<DomainEventNotification<ShippingOrderCreatedEvent>>
    {
        private readonly ILogger<ShippingOrderCreatedEventHandler> _logger;

        public ShippingOrderCreatedEventHandler(
            ILogger<ShippingOrderCreatedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(DomainEventNotification<ShippingOrderCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
