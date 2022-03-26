// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Drivers.DTOs;
using CleanArchitecture.Blazor.Application.Features.Drivers.Caching;

namespace CleanArchitecture.Blazor.Application.Features.Drivers.Commands.Update;

    public class UpdateDriverCommand: DriverDto,IRequest<Result>, IMapFrom<Driver>, ICacheInvalidator
    {
        public string CacheKey => DriverCacheKey.GetAllCacheKey;
        public CancellationTokenSource? SharedExpiryTokenSource => DriverCacheKey.SharedExpiryTokenSource;
    }

    public class UpdateDriverCommandHandler : IRequestHandler<UpdateDriverCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<UpdateDriverCommandHandler> _localizer;
        public UpdateDriverCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<UpdateDriverCommandHandler> localizer,
             IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result> Handle(UpdateDriverCommand request, CancellationToken cancellationToken)
        {
   
           var item =await _context.Drivers.FindAsync( new object[] { request.Id }, cancellationToken);
           if (item != null)
           {
                item = _mapper.Map(request, item);
                await _context.SaveChangesAsync(cancellationToken);
           }
           return Result.Success();
        }
    }

