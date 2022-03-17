// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Trucks.Caching;
using CleanArchitecture.Blazor.Application.Features.Trucks.DTOs;

namespace CleanArchitecture.Blazor.Application.Features.Trucks.Commands.Update;

public class UpdateTruckCommand : TruckDto, IRequest<Result>, IMapFrom<Truck>, ICacheInvalidator
{
    public string CacheKey => TruckCacheKey.GetAllCacheKey;
    public CancellationTokenSource? SharedExpiryTokenSource => TruckCacheKey.SharedExpiryTokenSource;
}

public class UpdateTruckCommandHandler : IRequestHandler<UpdateTruckCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<UpdateTruckCommandHandler> _localizer;
    public UpdateTruckCommandHandler(
        IApplicationDbContext context,
        IStringLocalizer<UpdateTruckCommandHandler> localizer,
         IMapper mapper
        )
    {
        _context = context;
        _localizer = localizer;
        _mapper = mapper;
    }
    public async Task<Result> Handle(UpdateTruckCommand request, CancellationToken cancellationToken)
    {
        var item = await _context.Trucks.FindAsync(new object[] { request.Id }, cancellationToken);
        if (item != null)
        {
            item = _mapper.Map(request, item);
            await _context.SaveChangesAsync(cancellationToken);
        }
        return Result.Success();
    }
}

