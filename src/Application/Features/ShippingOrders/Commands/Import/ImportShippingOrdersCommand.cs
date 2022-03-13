// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.ShippingOrders.DTOs;

namespace CleanArchitecture.Blazor.Application.Features.ShippingOrders.Commands.Import;

    public class ImportShippingOrdersCommand: IRequest<Result>
    {
        public string FileName { get; set; }
        public byte[] Data { get; set; }
    }
    public class CreateShippingOrdersTemplateCommand : IRequest<byte[]>
    {
        public IEnumerable<string> Fields { get; set; }
        public string SheetName { get; set; }
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
           //TODO:Implementing ImportShippingOrdersCommandHandler method
           var result = await _excelService.ImportAsync(request.Data, mappers: new Dictionary<string, Func<DataRow, ShippingOrderDto, object>>
            {
                //eg. { _localizer["Name"], (row,item) => item.Name = row[_localizer["Name"]]?.ToString() },

            }, _localizer["ShippingOrders"]);
           throw new System.NotImplementedException();
        }
        public async Task<byte[]> Handle(CreateShippingOrdersTemplateCommand request, CancellationToken cancellationToken)
        {
            //TODO:Implementing ImportShippingOrdersCommandHandler method 
            var fields = new string[] {
                   //TODO:Defines the title and order of the fields to be imported's template
                   //_localizer["Name"],
                };
            var result = await _excelService.CreateTemplateAsync(fields, _localizer["ShippingOrders"]);
            return result;
        }
    }

