// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Drivers.DTOs;
using CleanArchitecture.Blazor.Application.Features.Drivers.Caching;

namespace CleanArchitecture.Blazor.Application.Features.Drivers.Commands.Create;

    public class CreateDriverCommand: DriverDto,IRequest<Result<int>>, IMapFrom<Driver>, ICacheInvalidator
    {
      public string CacheKey => DriverCacheKey.GetAllCacheKey;
      public CancellationTokenSource? SharedExpiryTokenSource => DriverCacheKey.SharedExpiryTokenSource;
    }
    
    public class CreateDriverCommandHandler : IRequestHandler<CreateDriverCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<CreateDriverCommand> _localizer;
        public CreateDriverCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<CreateDriverCommand> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result<int>> Handle(CreateDriverCommand request, CancellationToken cancellationToken)
        {
     
           var item = _mapper.Map<Driver>(request);
           _context.Drivers.Add(item);
           await _context.SaveChangesAsync(cancellationToken);
           return  Result<int>.Success(item.Id);
        }
    }

