// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.ShippingOrders.Caching;
using CleanArchitecture.Blazor.Application.Features.ShippingOrders.DTOs;

namespace CleanArchitecture.Blazor.Application.Features.ShippingOrders.Commands.Import;

public class ImportShippingOrdersCommand : IRequest<Result>, ICacheInvalidator
{
    public string FileName { get; }
    public byte[] Data { get; }
    public string CacheKey => ShippingOrderCacheKey.GetAllCacheKey;
    public CancellationTokenSource? SharedExpiryTokenSource => ShippingOrderCacheKey.SharedExpiryTokenSource;
    public ImportShippingOrdersCommand(string fileName, byte[] data)
    {
        FileName = fileName;
        Data = data;
    }
}
public record CreateShippingOrdersTemplateCommand : IRequest<byte[]>
{

}

public class ImportShippingOrdersCommandHandler :
             IRequestHandler<CreateShippingOrdersTemplateCommand, byte[]>,
             IRequestHandler<ImportShippingOrdersCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<ImportShippingOrdersCommandHandler> _localizer;
    private readonly IExcelService _excelService;

    public ImportShippingOrdersCommandHandler(
        IApplicationDbContext context,
        IExcelService excelService,
        IStringLocalizer<ImportShippingOrdersCommandHandler> localizer,
        IMapper mapper
        )
    {
        _context = context;
        _localizer = localizer;
        _excelService = excelService;
        _mapper = mapper;
    }
    public async Task<Result> Handle(ImportShippingOrdersCommand request, CancellationToken cancellationToken)
    {
        
        var result = await _excelService.ImportAsync(request.Data, mappers: new Dictionary<string, Func<DataRow, ShippingOrderDto, object>>
        {
            { _localizer["Order No"], (row,item) => item.OrderNo = row[_localizer["Order No"]]?.ToString() },
            { _localizer["Starting Time"], (row,item) => item.StartingTime = row.IsNull(_localizer["Starting Time"])?null:Convert.ToDateTime (row[_localizer["Starting Time"]]?.ToString()) },
            { _localizer["Finish Time"], (row,item) => item.FinishTime = row.IsNull(_localizer["Finish Time"])?null:Convert.ToDateTime (row[_localizer["Finish Time"]]?.ToString()) },
            { _localizer["Plate Number"], (row,item) => item.PlateNumber = row[_localizer["PlateNumber"]]?.ToString() },
            { _localizer["Driver"], (row,item) => item.Driver = row[_localizer["Driver"]]?.ToString() },
            { _localizer["Phone Number"], (row,item) => item.PhoneNumber = row[_localizer["Phone Number"]]?.ToString() },
            { _localizer["Dispatcher"], (row,item) => item.Dispatcher = row[_localizer["Dispatcher"]]?.ToString() },
            { _localizer["Description"], (row,item) => item.Description = row[_localizer["Description"]]?.ToString() },
            { _localizer["Freight"], (row,item) => item.Freight =row.IsNull(_localizer["Freight"])?null:Convert.ToDecimal(row[_localizer["Freight"]]?.ToString()) },
            { _localizer["Cash Advance"], (row,item) => item.CashAdvance = row.IsNull(_localizer["Cash Advance"])?null:Convert.ToDecimal(row[_localizer["Cash Advance"]]?.ToString()) },
            { _localizer["Cost"], (row,item) => item.Cost = row.IsNull(_localizer["Cost"])?null:Convert.ToDecimal(row[_localizer["Cost"]]?.ToString()) },
            { _localizer["Gross Margin"], (row,item) => item.GrossMargin = row.IsNull(_localizer["Gross Margin"])?null:Convert.ToDecimal(row[_localizer["Gross Margin"]]?.ToString()) },
            { _localizer["Status"], (row,item) => item.Status = row[_localizer["Status"]]?.ToString() },
            { _localizer["Remark"], (row,item) => item.Remark = row[_localizer["Remark"]]?.ToString() },

        }, _localizer["ShippingOrders"]);
        throw new System.NotImplementedException();
    }
    public async Task<byte[]> Handle(CreateShippingOrdersTemplateCommand request, CancellationToken cancellationToken)
    {
      
        var fields = new string[] {
                   _localizer["Name"],
                };
        var result = await _excelService.CreateTemplateAsync(fields, _localizer["ShippingOrders"]);
        return result;
    }
}

