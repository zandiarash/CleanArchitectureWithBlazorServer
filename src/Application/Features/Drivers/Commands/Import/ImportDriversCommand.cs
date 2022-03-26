// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Drivers.DTOs;
using CleanArchitecture.Blazor.Application.Features.Drivers.Caching;

namespace CleanArchitecture.Blazor.Application.Features.Drivers.Commands.Import;

    public class ImportDriversCommand: IRequest<Result>, ICacheInvalidator
    {
        public string FileName { get;  }
        public byte[] Data { get;  }
        public string CacheKey => DriverCacheKey.GetAllCacheKey;
        public CancellationTokenSource? SharedExpiryTokenSource => DriverCacheKey.SharedExpiryTokenSource;
        public ImportDriversCommand(string fileName,byte[] data)
        {
           FileName = fileName;
           Data = data;
        }
    }
    public record class CreateDriversTemplateCommand : IRequest<byte[]>
    {
 
    }

    public class ImportDriversCommandHandler : 
                 IRequestHandler<CreateDriversTemplateCommand, byte[]>,
                 IRequestHandler<ImportDriversCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<ImportDriversCommandHandler> _localizer;
        private readonly IExcelService _excelService;

        public ImportDriversCommandHandler(
            IApplicationDbContext context,
            IExcelService excelService,
            IStringLocalizer<ImportDriversCommandHandler> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _excelService = excelService;
            _mapper = mapper;
        }
        public async Task<Result> Handle(ImportDriversCommand request, CancellationToken cancellationToken)
        {
         
           var result = await _excelService.ImportAsync(request.Data, mappers: new Dictionary<string, Func<DataRow, DriverDto, object>>
            {
                { _localizer["Name"], (row,item) => item.Name = row[_localizer["Name"]]?.ToString() },

            }, _localizer["Drivers"]);
           throw new System.NotImplementedException();
        }
        public async Task<byte[]> Handle(CreateDriversTemplateCommand request, CancellationToken cancellationToken)
        {
            
            var fields = new string[] {
                   _localizer["Name"],
                };
            var result = await _excelService.CreateTemplateAsync(fields, _localizer["Drivers"]);
            return result;
        }
    }

