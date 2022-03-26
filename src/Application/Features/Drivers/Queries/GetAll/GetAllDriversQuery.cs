// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Drivers.DTOs;
using CleanArchitecture.Blazor.Application.Features.Drivers.Caching;

namespace CleanArchitecture.Blazor.Application.Features.Drivers.Queries.GetAll;

    public class GetAllDriversQuery : IRequest<IEnumerable<DriverDto>>, ICacheable
    {
       public string CacheKey => DriverCacheKey.GetAllCacheKey;
       public MemoryCacheEntryOptions? Options => new MemoryCacheEntryOptions().AddExpirationToken(new CancellationChangeToken(DriverCacheKey.SharedExpiryTokenSource.Token));
    }
    
    public class GetAllDriversQueryHandler :
         IRequestHandler<GetAllDriversQuery, IEnumerable<DriverDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<GetAllDriversQueryHandler> _localizer;

        public GetAllDriversQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IStringLocalizer<GetAllDriversQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<IEnumerable<DriverDto>> Handle(GetAllDriversQuery request, CancellationToken cancellationToken)
        {
            var data = await _context.Drivers.OrderBy(x=>x.Name)
                         .ProjectTo<DriverDto>(_mapper.ConfigurationProvider)
                         .ToListAsync(cancellationToken);
            return data;
        }
    }


