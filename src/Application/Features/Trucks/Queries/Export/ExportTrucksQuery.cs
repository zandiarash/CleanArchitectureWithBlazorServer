// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


using CleanArchitecture.Blazor.Application.Features.Trucks.DTOs;

namespace CleanArchitecture.Blazor.Application.Features.Trucks.Queries.Export;

    public class ExportTrucksQuery : IRequest<byte[]>
    {
        public string FilterRules { get; set; }
        public string Sort { get; set; } = "Id";
        public string Order { get; set; } = "desc";
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
            //TODO:Implementing ExportTrucksQueryHandler method 
            var filters = PredicateBuilder.FromFilter<Truck>(request.FilterRules);
            var data = await _context.Trucks.Where(filters)
                       .OrderBy("{request.Sort} {request.Order}")
                       .ProjectTo<TruckDto>(_mapper.ConfigurationProvider)
                       .ToListAsync(cancellationToken);
            var result = await _excelService.ExportAsync(data,
                new Dictionary<string, Func<TruckDto, object>>()
                {
                    //{ _localizer["Id"], item => item.Id },
                }
                , _localizer["Trucks"]);
            return result;
        }
    }