// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.ShippingOrders.DTOs;

namespace CleanArchitecture.Blazor.Application.Features.ShippingOrders.Commands.Create;

    public class CreateShippingOrderCommand: ShippingOrderDto,IRequest<Result<int>>, IMapFrom<ShippingOrder>
    {
       
    }
    
    public class CreateShippingOrderCommandHandler : IRequestHandler<CreateShippingOrderCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<CreateShippingOrderCommand> _localizer;
        public CreateShippingOrderCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<CreateShippingOrderCommand> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result<int>> Handle(CreateShippingOrderCommand request, CancellationToken cancellationToken)
        {
      
           var item = _mapper.Map<ShippingOrder>(request);
           _context.ShippingOrders.Add(item);
           await _context.SaveChangesAsync(cancellationToken);
           return  Result<int>.Success(item.Id);
        }
    }

