// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.ShippingOrders.EventHandlers;

    public class ShippingOrderDeletedEventHandler : INotificationHandler<DomainEventNotification<ShippingOrderDeletedEvent>>
    {
        private readonly ILogger<ShippingOrderDeletedEventHandler> _logger;

        public ShippingOrderDeletedEventHandler(
            ILogger<ShippingOrderDeletedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(DomainEventNotification<ShippingOrderDeletedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
