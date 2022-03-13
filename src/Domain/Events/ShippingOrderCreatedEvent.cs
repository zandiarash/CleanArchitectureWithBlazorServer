// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Domain.Events;

    public class ShippingOrderCreatedEvent : DomainEvent
    {
        public ShippingOrderCreatedEvent(ShippingOrder item)
        {
            Item = item;
        }

        public ShippingOrder Item { get; }
    }

