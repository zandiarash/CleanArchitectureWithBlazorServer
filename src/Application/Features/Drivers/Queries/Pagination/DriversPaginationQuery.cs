// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Drivers.DTOs;
using CleanArchitecture.Blazor.Application.Features.Drivers.Caching;

namespace CleanArchitecture.Blazor.Application.Features.Drivers.Queries.Pagination;

    public class DriversWithPaginationQuery : PaginationFilter, IRequest<PaginatedData<DriverDto>>, ICacheable
    {
        public string CacheKey => DriverCacheKey.GetPagtionCacheKey("{this}");
        public MemoryCacheEntryOptions? Options => new MemoryCacheEntryOptions().AddExpirationToken(new CancellationChangeToken(DriverCacheKey.SharedExpiryTokenSource.Token));
    }
    
    public class DriversWithPaginationQueryHandler :
         IRequestHandler<DriversWithPaginationQuery, PaginatedData<DriverDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<DriversWithPaginationQueryHandler> _localizer;

        public DriversWithPaginationQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IStringLocalizer<DriversWithPaginationQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<PaginatedData<DriverDto>> Handle(DriversWithPaginationQuery request, CancellationToken cancellationToken)
        {

           var data = await _context.Drivers.Where(x => x.Name.Contains(request.Keyword) || x.Address.Contains(request.Keyword) || x.PhoneNumber.Contains(request.Keyword))
                .OrderBy($"{request.OrderBy} {request.SortDirection}")
                .ProjectTo<DriverDto>(_mapper.ConfigurationProvider)
                .PaginatedDataAsync(request.PageNumber, request.PageSize);
            return data;
        }
   }