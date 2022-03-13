// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.ShippingOrders.DTOs;

namespace CleanArchitecture.Blazor.Application.Features.ShippingOrders.Commands.Update;

    public class UpdateShippingOrderCommand: ShippingOrderDto,IRequest<Result>, IMapFrom<ShippingOrder>
    {
        
    }

    public class UpdateShippingOrderCommandHandler : IRequestHandler<UpdateShippingOrderCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<UpdateShippingOrderCommandHandler> _localizer;
        public UpdateShippingOrderCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<UpdateShippingOrderCommandHandler> localizer,
             IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result> Handle(UpdateShippingOrderCommand request, CancellationToken cancellationToken)
        {

           var item =await _context.ShippingOrders.FindAsync( new object[] { request.Id }, cancellationToken);
           if (item != null)
           {
                item = _mapper.Map(request, item);
                await _context.SaveChangesAsync(cancellationToken);
           }
           return Result.Success();
        }
    }

