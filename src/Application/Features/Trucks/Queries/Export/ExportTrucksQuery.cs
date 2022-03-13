// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


using CleanArchitecture.Blazor.Application.Features.Trucks.DTOs;

namespace CleanArchitecture.Blazor.Application.Features.Trucks.Queries.Export;

public class ExportTrucksQuery : IRequest<byte[]>
{
    public string OrderBy { get; set; } = "Id";
    public string SortDirection { get; set; } = "Desc";
    public string Keyword { get; set; } = String.Empty;
}

public class ExportTrucksQueryHandler :
     IRequestHandler<ExportTrucksQuery, byte[]>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IExcelService _excelService;
    private readonly IStringLocalizer<ExportTrucksQueryHandler> _localizer;

    public ExportTrucksQueryHandler(
        IApplicationDbContext context,
        IMapper mapper,
        IExcelService excelService,
        IStringLocalizer<ExportTrucksQueryHandler> localizer
        )
    {
        _context = context;
        _mapper = mapper;
        _excelService = excelService;
        _localizer = localizer;
    }

    public async Task<byte[]> Handle(ExportTrucksQuery request, CancellationToken cancellationToken)
    {

        var data = await _context.Trucks.Where(x => x.PlateNumber.Contains(request.Keyword) || x.Description.Contains(request.Keyword))
                    .OrderBy($"{request.OrderBy} {request.SortDirection}")
                    .ProjectTo<TruckDto>(_mapper.ConfigurationProvider)
                   .ToListAsync(cancellationToken);
        var result = await _excelService.ExportAsync(data,
            new Dictionary<string, Func<TruckDto, object>>()
            {
                    { _localizer["Plate Number"], item => item.PlateNumber },
                    { _localizer["Trailer Number"], item => item.TrailerNumber },
                    { _localizer["Vehicle Type"], item => item.VehicleType },
                    { _localizer["Owner"], item => item.Owner },
                    { _localizer["Description"], item => item.Description },
                    { _localizer["Driver"], item => item.Driver },
                    { _localizer["Phone Number"], item => item.PhoneNumber },
                    { _localizer["Status"], item => item.Status },
                    { _localizer["Device Id"], item => item.DeviceId },

            }
            , _localizer["Trucks"]);
        return result;
    }
}