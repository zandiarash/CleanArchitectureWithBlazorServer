// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Visitors.DTOs;
using CleanArchitecture.Blazor.Application.Features.Visitors.Caching;

namespace CleanArchitecture.Blazor.Application.Features.Visitors.Queries.Pagination;

public class VisitorsWithPaginationQuery : PaginationFilter, IRequest<PaginatedData<VisitorDto>>, ICacheable
{
    public string? Name { get; set; }
    public string? LicensePlateNumber { get; set; }
    public string? CompanyName { get; set; }
    public string? Purpose { get; set; }
    public string? Employee { get; set; }
    public DateTime? ExpectedDate1 { get; set; }
    public DateTime? ExpectedDate2 { get; set; }
    public bool? Approved { get; set; }

    public override string ToString()
    {
        return $"{base.ToString()},Name:{Name},LicensePlateNumber:{LicensePlateNumber},CompanyName:{CompanyName},Purpose:{Purpose},Employee:{Employee},ExpectedDate1:{ExpectedDate1?.ToString()},ExpectedDate2:{ExpectedDate2?.ToString()},Approved:{Approved}";
    }
    public string CacheKey => VisitorCacheKey.GetPagtionCacheKey($"{this}");
    public MemoryCacheEntryOptions? Options => new MemoryCacheEntryOptions().AddExpirationToken(new CancellationChangeToken(VisitorCacheKey.SharedExpiryTokenSource.Token));
}

public class VisitorsWithPaginationQueryHandler :
     IRequestHandler<VisitorsWithPaginationQuery, PaginatedData<VisitorDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<VisitorsWithPaginationQueryHandler> _localizer;

    public VisitorsWithPaginationQueryHandler(
        IApplicationDbContext context,
        IMapper mapper,
        IStringLocalizer<VisitorsWithPaginationQueryHandler> localizer
        )
    {
        _context = context;
        _mapper = mapper;
        _localizer = localizer;
    }

    public async Task<PaginatedData<VisitorDto>> Handle(VisitorsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var data = await _context.Visitors
          .OrderBy($"{request.OrderBy} {request.SortDirection}")
          .ProjectTo<VisitorDto>(_mapper.ConfigurationProvider)
          .PaginatedDataAsync(request.PageNumber, request.PageSize);
        return data;
    }
}