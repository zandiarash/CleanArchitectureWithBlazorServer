// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


using CleanArchitecture.Blazor.Application.Features.Drivers.DTOs;

namespace CleanArchitecture.Blazor.Application.Features.Drivers.Queries.Export;

    public class ExportDriversQuery : IRequest<byte[]>
    {
       public string OrderBy { get; set; } = "Id";
       public string SortDirection { get; set; } = "Desc";
       public string Keyword { get; set; } = String.Empty;
    }
    
    public class ExportDriversQueryHandler :
         IRequestHandler<ExportDriversQuery, byte[]>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IExcelService _excelService;
        private readonly IStringLocalizer<ExportDriversQueryHandler> _localizer;

        public ExportDriversQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IExcelService excelService,
            IStringLocalizer<ExportDriversQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _excelService = excelService;
            _localizer = localizer;
        }

        public async Task<byte[]> Handle(ExportDriversQuery request, CancellationToken cancellationToken)
        {
       
  
            var data = await _context.Drivers.Where(x=>x.Name.Contains(request.Keyword) || x.Address.Contains(request.Keyword))
                       .OrderBy($"{request.OrderBy} {request.SortDirection}")
                       .ProjectTo<DriverDto>(_mapper.ConfigurationProvider)
                       .ToListAsync(cancellationToken);
            var result = await _excelService.ExportAsync(data,
                new Dictionary<string, Func<DriverDto, object>>()
                {
                    { _localizer["Name"], item => item.Name },
                }
                , _localizer["Drivers"]);
            return result;
        }
    }