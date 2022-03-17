// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


using CleanArchitecture.Blazor.Application.Features.ShippingOrders.Caching;
using CleanArchitecture.Blazor.Application.Features.ShippingOrders.DTOs;

namespace CleanArchitecture.Blazor.Application.Features.ShippingOrders.Commands.AddEdit;

public class AddEditShippingOrderCommand : ShippingOrderDto, IRequest<Result<int>>, IMapFrom<ShippingOrder>, ICacheInvalidator
{
    public string CacheKey => ShippingOrderCacheKey.GetAllCacheKey;
    public CancellationTokenSource? SharedExpiryTokenSource => ShippingOrderCacheKey.SharedExpiryTokenSource;
}

public class AddEditShippingOrderCommandHandler : IRequestHandler<AddEditShippingOrderCommand, Result<int>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<AddEditShippingOrderCommandHandler> _localizer;
    public AddEditShippingOrderCommandHandler(
        IApplicationDbContext context,
        IStringLocalizer<AddEditShippingOrderCommandHandler> localizer,
        IMapper mapper
        )
    {
        _context = context;
        _localizer = localizer;
        _mapper = mapper;
    }
    public async Task<Result<int>> Handle(AddEditShippingOrderCommand request, CancellationToken cancellationToken)
    {

        if (request.Id > 0)
        {
            var item = await _context.ShippingOrders.FindAsync(new object[] { request.Id }, cancellationToken);
            _ = item ?? throw new NotFoundException("ShippingOrder {request.Id} Not Found.");
            item = _mapper.Map(request, item);
            var description = "";
            foreach (var costdto in request.CostDetailDtos)
            {
                if (costdto.Id <= 0)
                {
                    var cost = _mapper.Map<CostDetail>(costdto);
                    _context.CostDetails.Add(cost);
                }
                else
                {
                    var cost = await _context.CostDetails.FindAsync(costdto.Id);
                    cost = _mapper.Map(costdto, cost);
                }



            }
            for (var i = 0; i < request.GoodsDetailDtos.Count; i++)
            {
                var goodsdto = request.GoodsDetailDtos[i];
                if (goodsdto.Id <= 0)
                {
                    var goods = _mapper.Map<GoodsDetail>(goodsdto);
                    _context.GoodsDetails.Add(goods);
                }
                else
                {
                    var goods = await _context.GoodsDetails.FindAsync(goodsdto.Id);
                    goods = _mapper.Map(goodsdto, goods);
                }
                description += $"{i + 1}. " + goodsdto.ToString() + $"{ (i + 1 < request.GoodsDetailDtos.Count ? "<br>" : "")} ";
            }
            item.Description = description;
            await _context.SaveChangesAsync(cancellationToken);
            return Result<int>.Success(item.Id);
        }
        else
        {
            var item = _mapper.Map<ShippingOrder>(request);
            var description = "";
            foreach (var costdto in request.CostDetailDtos.Where(x => !string.IsNullOrEmpty(x.Name)))
            {
                var cost = _mapper.Map<CostDetail>(costdto);
                item.CostDetails.Add(cost);

            }
            for (var i = 0; i < request.GoodsDetailDtos.Count; i++)
            {
                var goodsdto = request.GoodsDetailDtos[i];
                if (string.IsNullOrEmpty(goodsdto.PickupAddress) || string.IsNullOrEmpty(goodsdto.DeliveryAddress)) continue;
                var goods = _mapper.Map<GoodsDetail>(goodsdto);
                item.GoodsDetails.Add(goods);
                description += $"{i + 1}. " + goodsdto.ToString() + $"{ (i + 1 < request.GoodsDetailDtos.Count ? "<br>" : "")} ";
            }
            item.Description = description;
            _context.ShippingOrders.Add(item);
            await _context.SaveChangesAsync(cancellationToken);
            return Result<int>.Success(item.Id);
        }

    }
}

