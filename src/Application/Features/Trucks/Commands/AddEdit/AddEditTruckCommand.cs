// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


using CleanArchitecture.Blazor.Application.Features.Trucks.DTOs;

namespace CleanArchitecture.Blazor.Application.Features.Trucks.Commands.AddEdit;

    public class AddEditTruckCommand: TruckDto,IRequest<Result<int>>, IMapFrom<Truck>
    {
      
    }

    public class AddEditTruckCommandHandler : IRequestHandler<AddEditTruckCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AddEditTruckCommandHandler> _localizer;
        public AddEditTruckCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<AddEditTruckCommandHandler> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result<int>> Handle(AddEditTruckCommand request, CancellationToken cancellationToken)
        {
            //TODO:Implementing AddEditTruckCommandHandler method 
            if (request.Id > 0)
            {
                var item = await _context.Trucks.FindAsync(new object[] { request.Id }, cancellationToken);
                _ = item ?? throw new NotFoundException("Truck {request.Id} Not Found.");
                item = _mapper.Map(request, item);
                await _context.SaveChangesAsync(cancellationToken);
                return Result<int>.Success(item.Id);
            }
            else
            {
                var item = _mapper.Map<Truck>(request);
                _context.Trucks.Add(item);
                await _context.SaveChangesAsync(cancellationToken);
                return Result<int>.Success(item.Id);
            }
           
        }
    }

