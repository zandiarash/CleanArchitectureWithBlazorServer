// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Drivers.DTOs;
using CleanArchitecture.Blazor.Application.Features.Drivers.Caching;


namespace CleanArchitecture.Blazor.Application.Features.Drivers.Commands.Delete;

    public class DeleteDriverCommand: IRequest<Result>, ICacheInvalidator
    {
      public int[] Id {  get; }
      public string CacheKey => DriverCacheKey.GetAllCacheKey;
      public CancellationTokenSource? SharedExpiryTokenSource => DriverCacheKey.SharedExpiryTokenSource;
      public DeleteDriverCommand(int[] id)
      {
        Id = id;
      }
    }

    public class DeleteDriverCommandHandler : 
                 IRequestHandler<DeleteDriverCommand, Result>

    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<DeleteDriverCommandHandler> _localizer;
        public DeleteDriverCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<DeleteDriverCommandHandler> localizer,
             IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result> Handle(DeleteDriverCommand request, CancellationToken cancellationToken)
        {
            //TODO:Implementing DeleteCheckedDriversCommandHandler method 
            var items = await _context.Drivers.Where(x => request.Id.Contains(x.Id)).ToListAsync(cancellationToken);
            foreach (var item in items)
            {
                _context.Drivers.Remove(item);
            }
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

    }

