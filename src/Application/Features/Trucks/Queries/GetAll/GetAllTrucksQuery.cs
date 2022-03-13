// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Trucks.DTOs;

namespace CleanArchitecture.Blazor.Application.Features.Trucks.Queries.GetAll;

    public class GetAllTrucksQuery : IRequest<IEnumerable<TruckDto>>
    {
       
    }
    
    public class GetAllTrucksQueryHandler :
         IRequestHandler<GetAllTrucksQuery, IEnumerable<TruckDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<GetAllTrucksQueryHandler> _localizer;

        public GetAllTrucksQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IStringLocalizer<GetAllTrucksQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<IEnumerable<TruckDto>> Handle(GetAllTrucksQuery request, CancellationToken cancellationToken)
        {
            //TODO:Implementing GetAllTrucksQueryHandler method 
            var data = await _context.Trucks
                         .ProjectTo<TruckDto>(_mapper.ConfigurationProvider)
                         .ToListAsync(cancellationToken);
            return data;
        }
    }


