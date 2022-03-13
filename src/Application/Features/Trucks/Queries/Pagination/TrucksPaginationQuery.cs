// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Trucks.DTOs;

namespace CleanArchitecture.Blazor.Application.Features.Trucks.Queries.Pagination;

    public class TrucksWithPaginationQuery : PaginationFilter, IRequest<PaginatedData<TruckDto>>
    {
       
    }
    
    public class TrucksWithPaginationQueryHandler :
         IRequestHandler<TrucksWithPaginationQuery, PaginatedData<TruckDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<TrucksWithPaginationQueryHandler> _localizer;

        public TrucksWithPaginationQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IStringLocalizer<TrucksWithPaginationQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<PaginatedData<TruckDto>> Handle(TrucksWithPaginationQuery request, CancellationToken cancellationToken)
        {
            //TODO:Implementing TrucksWithPaginationQueryHandler method 
           var data = await _context.Trucks
                .OrderBy("{request.OrderBy} {request.SortDirection}")
                .ProjectTo<TruckDto>(_mapper.ConfigurationProvider)
                .PaginatedDataAsync(request.PageNumber, request.PageSize);
            return data;
        }
   }