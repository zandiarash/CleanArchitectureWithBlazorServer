// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


using CleanArchitecture.Blazor.Application.Features.Drivers.DTOs;
using CleanArchitecture.Blazor.Application.Features.Drivers.Caching;
namespace CleanArchitecture.Blazor.Application.Features.Drivers.Commands.AddEdit;

    public class AddEditDriverCommand: DriverDto,IRequest<Result<int>>, IMapFrom<Driver>, ICacheInvalidator
    {
      public string CacheKey => DriverCacheKey.GetAllCacheKey;
      public CancellationTokenSource? SharedExpiryTokenSource => DriverCacheKey.SharedExpiryTokenSource;
    }

    public class AddEditDriverCommandHandler : IRequestHandler<AddEditDriverCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AddEditDriverCommandHandler> _localizer;
        public AddEditDriverCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<AddEditDriverCommandHandler> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result<int>> Handle(AddEditDriverCommand request, CancellationToken cancellationToken)
        {
            if (request.Id > 0)
            {
                var item = await _context.Drivers.FindAsync(new object[] { request.Id }, cancellationToken);
                _ = item ?? throw new NotFoundException("Driver {request.Id} Not Found.");
                item = _mapper.Map(request, item);
                await _context.SaveChangesAsync(cancellationToken);
                return Result<int>.Success(item.Id);
            }
            else
            {
                var item = _mapper.Map<Driver>(request);
                _context.Drivers.Add(item);
                await _context.SaveChangesAsync(cancellationToken);
                return Result<int>.Success(item.Id);
            }
           
        }
    }

