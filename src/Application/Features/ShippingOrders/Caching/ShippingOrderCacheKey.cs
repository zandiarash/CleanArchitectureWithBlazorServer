// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.ShippingOrders.Caching;

public static class ShippingOrderCacheKey
{
    public const string GetAllCacheKey = "all-ShippingOrders";
    public static string GetPagtionCacheKey(string parameters) {
        return $"ShippingOrdersWithPaginationQuery,{parameters}";
    }
        static ShippingOrderCacheKey()
    {
        SharedExpiryTokenSource = new CancellationTokenSource(new TimeSpan(3,0,0));
    }
    public static string GetByIdCacheKey(int id) => $"GetById:{id}-ShippingOrder";
    public static CancellationTokenSource SharedExpiryTokenSource { get; private set; }
    public static MemoryCacheEntryOptions MemoryCacheEntryOptions => new MemoryCacheEntryOptions().AddExpirationToken(new CancellationChangeToken(SharedExpiryTokenSource.Token));
}

