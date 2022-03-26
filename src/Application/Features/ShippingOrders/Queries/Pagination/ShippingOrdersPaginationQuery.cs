// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.ShippingOrders.Caching;
using CleanArchitecture.Blazor.Application.Features.ShippingOrders.DTOs;

namespace CleanArchitecture.Blazor.Application.Features.ShippingOrders.Queries.Pagination;

public class ShippingOrdersWithPaginationQuery : PaginationFilter, IRequest<PaginatedData<ShippingOrderDto>>, ICacheable
{
    public string CacheKey => ShippingOrderCacheKey.GetPagtionCacheKey($"{this}");
    public MemoryCacheEntryOptions? Options => new MemoryCacheEntryOptions().AddExpirationToken(new CancellationChangeToken(ShippingOrderCacheKey.SharedExpiryTokenSource.Token));
}

public class ShippingOrdersWithPaginationQueryHandler :
         IRequestHandler<ShippingOrdersWithPaginationQuery, PaginatedData<ShippingOrderDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<ShippingOrdersWithPaginationQueryHandler> _localizer;

    public ShippingOrdersWithPaginationQueryHandler(
        IApplicationDbContext context,
        IMapper mapper,
        IStringLocalizer<ShippingOrdersWithPaginationQueryHandler> localizer
        )
    {
        _context = context;
        _mapper = mapper;
        _localizer = localizer;
    }

    public async Task<PaginatedData<ShippingOrderDto>> Handle(ShippingOrdersWithPaginationQuery request, CancellationToken cancellationToken)
    {

        var data = await _context.ShippingOrders.Where(x => x.OrderNo.Contains(request.Keyword)
        || x.Description.Contains(request.Keyword) || x.Remark.Contains(request.Keyword)
        || x.DriverName.Contains(request.Keyword))
             .OrderBy($"{request.OrderBy} {request.SortDirection}")
             .ProjectTo<ShippingOrderDto>(_mapper.ConfigurationProvider)
             .PaginatedDataAsync(request.PageNumber, request.PageSize);
        return data;
    }
}