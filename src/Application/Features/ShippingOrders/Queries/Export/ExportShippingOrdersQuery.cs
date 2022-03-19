// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


using CleanArchitecture.Blazor.Application.Features.ShippingOrders.DTOs;

namespace CleanArchitecture.Blazor.Application.Features.ShippingOrders.Queries.Export;

    public class ExportShippingOrdersQuery : IRequest<byte[]>
    {
       public string OrderBy { get; set; } = "Id";
       public string SortDirection { get; set; } = "Desc";
       public string Keyword { get; set; } = String.Empty;
    }
    
    public class ExportShippingOrdersQueryHandler :
         IRequestHandler<ExportShippingOrdersQuery, byte[]>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IExcelService _excelService;
        private readonly IStringLocalizer<ExportShippingOrdersQueryHandler> _localizer;

        public ExportShippingOrdersQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IExcelService excelService,
            IStringLocalizer<ExportShippingOrdersQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _excelService = excelService;
            _localizer = localizer;
        }

        public async Task<byte[]> Handle(ExportShippingOrdersQuery request, CancellationToken cancellationToken)
        {
 
            var data = await _context.ShippingOrders//.Where(x=>x.Name.Contains(request.Keyword) || x.Description.Contains(request.Keyword))
                       .OrderBy($"{request.OrderBy} {request.SortDirection}")
                       .ProjectTo<ShippingOrderDto>(_mapper.ConfigurationProvider)
                       .ToListAsync(cancellationToken);
            var result = await _excelService.ExportAsync(data,
                new Dictionary<string, Func<ShippingOrderDto, object>>()
                {
                    { _localizer["Order No"], item => item.OrderNo },
                    { _localizer["Starting Time"], item => item.StartingTime },
                    { _localizer["Finish Time"], item => item.FinishTime },
                    { _localizer["Plate Number"], item => item.PlateNumber },
                    { _localizer["Driver"], item => item.Driver },
                    { _localizer["Phone Number"], item => item.PhoneNumber },
                    { _localizer["Dispatcher"], item => item.Dispatcher },
                    { _localizer["Description"], item => item.Description },
                    { _localizer["Freight"], item => item.Freight },
                    { _localizer["CashAdvance"], item => item.CashAdvance },
                    { _localizer["Cost"], item => item.Cost },
                    { _localizer["Gross Margin"], item => item.GrossMargin },
                    { _localizer["Status"], item => item.Status },
                    { _localizer["Remark"], item => item.Remark },
                }
                , _localizer["ShippingOrders"]);
            return result;
        }
    }