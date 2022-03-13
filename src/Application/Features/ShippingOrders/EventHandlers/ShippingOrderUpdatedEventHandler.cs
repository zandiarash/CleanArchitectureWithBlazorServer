// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.ShippingOrders.EventHandlers;

    public class ShippingOrderUpdatedEventHandler : INotificationHandler<DomainEventNotification<ShippingOrderUpdatedEvent>>
    {
        private readonly ILogger<ShippingOrderUpdatedEventHandler> _logger;

        public ShippingOrderUpdatedEventHandler(
            ILogger<ShippingOrderUpdatedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(DomainEventNotification<ShippingOrderUpdatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
