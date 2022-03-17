// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Trucks.Caching;
using CleanArchitecture.Blazor.Application.Features.Trucks.Commands.AddEdit;

namespace CleanArchitecture.Blazor.Application.Features.Trucks.Commands.Import;

public class ImportTrucksCommand : IRequest<Result>, ICacheInvalidator
{
    public string FileName { get;  }
    public byte[] Data { get;  }
    public string CacheKey => TruckCacheKey.GetAllCacheKey;
    public CancellationTokenSource? SharedExpiryTokenSource => TruckCacheKey.SharedExpiryTokenSource;
    public ImportTrucksCommand(string fileName,byte[] data)
    {
        FileName = fileName;
        Data = data;
    }
}
public record CreateTrucksTemplateCommand : IRequest<byte[]>
{
   
}

public class ImportTrucksCommandHandler :
             IRequestHandler<CreateTrucksTemplateCommand, byte[]>,
             IRequestHandler<ImportTrucksCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<ImportTrucksCommandHandler> _localizer;
    private readonly IExcelService _excelService;
    private readonly IValidator<AddEditTruckCommand> _importtruckValidator;

    public ImportTrucksCommandHandler(
        IApplicationDbContext context,
        IExcelService excelService,
        IValidator<AddEditTruckCommand> importtruckValidator,
        IStringLocalizer<ImportTrucksCommandHandler> localizer,
        IMapper mapper
        )
    {
        _context = context;
        _localizer = localizer;
        _excelService = excelService;
        _importtruckValidator = importtruckValidator;
        _mapper = mapper;
    }
    public async Task<Result> Handle(ImportTrucksCommand request, CancellationToken cancellationToken)
    {
        var result = await _excelService.ImportAsync(request.Data, mappers: new Dictionary<string, Func<DataRow, AddEditTruckCommand, object>>
            {
                { _localizer["Plate Number"], (row,item) => item.PlateNumber = row[_localizer["Plate Number"]]?.ToString() },
                { _localizer["Trailer Number"], (row,item) => item.TrailerNumber = row[_localizer["Trailer Number"]]?.ToString() },
                { _localizer["Vehicle Type"], (row,item) => item.VehicleType = row[_localizer["VehicleType"]]?.ToString() },
                { _localizer["Owner"], (row,item) => item.Owner = row[_localizer["Owner"]]?.ToString() },
                { _localizer["Description"], (row,item) => item.Description = row[_localizer["Description"]]?.ToString() },
                { _localizer["Driver"], (row,item) => item.Driver = row[_localizer["Driver"]]?.ToString() },
                { _localizer["Phone Number"], (row,item) => item.PhoneNumber = row[_localizer["Phone Number"]]?.ToString() },
                { _localizer["Status"], (row,item) => item.Status = row[_localizer["Status"]]?.ToString() },
                { _localizer["Device Id"], (row,item) => item.DeviceId = row[_localizer["Device Id"]]?.ToString() },
            }, _localizer["Trucks"]);
        if (result.Succeeded)
        {
            var errors = new List<string>();
            foreach (var dto in result.Data)
            {
                var validationResult = await _importtruckValidator.ValidateAsync(dto, cancellationToken);
                if (validationResult.IsValid)
                {
                    var item = _mapper.Map<Truck>(dto);
                    _context.Trucks.Add(item);
                }
                else
                {
                    errors.AddRange(validationResult.Errors.Select(e => $"{e.ErrorMessage}"));
                }
            }
            if (errors.Count > 0)
            {
                return Result.Failure(errors);
            }
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        else
        {
            return Result.Failure(result.Errors);
        }
    }
    public async Task<byte[]> Handle(CreateTrucksTemplateCommand request, CancellationToken cancellationToken)
    {

        var fields = new string[] {
                   _localizer["Plate Number"],
                   _localizer["Trailer Number"],
                   _localizer["Vehicle Type"],
                   _localizer["Owner"],
                   _localizer["Description"],
                   _localizer["Driver"],
                   _localizer["Phone Number"],
                   _localizer["Status"],
                   _localizer["Device Id"],
                };
        var result = await _excelService.CreateTemplateAsync(fields, _localizer["Trucks"]);
        return result;
    }
}

