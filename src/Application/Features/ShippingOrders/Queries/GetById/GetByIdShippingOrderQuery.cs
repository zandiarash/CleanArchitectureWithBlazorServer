// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.ShippingOrders.Caching;
using CleanArchitecture.Blazor.Application.Features.ShippingOrders.DTOs;

namespace CleanArchitecture.Blazor.Application.Features.ShippingOrders.Queries.GetAll;

public class GetByIdShippingOrderQuery : IRequest<ShippingOrderDto>, ICacheable
{
    public string CacheKey => ShippingOrderCacheKey.GetByIdCacheKey(Id);
    public MemoryCacheEntryOptions? Options => new MemoryCacheEntryOptions().AddExpirationToken(new CancellationChangeToken(ShippingOrderCacheKey.SharedExpiryTokenSource.Token));
    public int Id { get; set; }
    public GetByIdShippingOrderQuery(int id)
    {
        Id = id;
    }
}
 
public class GetByIdShippingOrderQueryHandler :
     IRequestHandler<GetByIdShippingOrderQuery, ShippingOrderDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<GetAllShippingOrdersQueryHandler> _localizer;

    public GetByIdShippingOrderQueryHandler(
        IApplicationDbContext context,
        IMapper mapper,
        IStringLocalizer<GetAllShippingOrdersQueryHandler> localizer
        )
    {
        _context = context;
        _mapper = mapper;
        _localizer = localizer;
    }

    public async Task<ShippingOrderDto> Handle(GetByIdShippingOrderQuery request, CancellationToken cancellationToken)
    {
        var order = await _context.ShippingOrders.FindAsync(request.Id);
        if (order == null) throw new NotFoundException($"not found shipping order by id:{request.Id}");
        var orderdto = _mapper.Map<ShippingOrderDto>(order);
        var costdetails = await _context.CostDetails.Where(x => x.ShippingOrderId == order.Id).ToListAsync();
        var goodsdetails = await _context.GoodsDetails.Where(x => x.ShippingOrderId == order.Id).ToListAsync();
        foreach (var costitem in costdetails)
        {
            var costdto = _mapper.Map<CostDetailDto>(costitem);
            orderdto.CostDetailDtos.Add(costdto);
        }
        foreach (var goodsitem in goodsdetails)
        {
            var goodsdto = _mapper.Map<GoodsDetailDto>(goodsitem);
            orderdto.GoodsDetailDtos.Add(goodsdto);
        }
        return orderdto;
    }
}


