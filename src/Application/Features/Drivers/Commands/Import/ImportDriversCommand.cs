// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Drivers.DTOs;
using CleanArchitecture.Blazor.Application.Features.Drivers.Caching;
using CleanArchitecture.Blazor.Application.Features.Drivers.Commands.Create;

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
                { _localizer["Phone Number"], (row,item) => item.PhoneNumber = row[_localizer["Phone Number"]]?.ToString() },
                { _localizer["Identity No"], (row,item) => item.IdentityNo = row[_localizer["Identity No"]]?.ToString() },
                { _localizer["Address"], (row,item) => item.Address = row[_localizer["Address"]]?.ToString() },
                { _localizer["Brithday"], (row,item) => item.BrithDay = row.IsNull(_localizer["Brithday"])?null: Convert.ToDateTime(row[_localizer["Brithday"]].ToString()) },
                { _localizer["Age"], (row,item) => item.Age =row.IsNull(_localizer["Age"])?null: Convert.ToInt32(row[_localizer["Age"]].ToString()) },
                { _localizer["Driving No"], (row,item) => item.DrivingNo = row[_localizer["Driving No"]]?.ToString() },
                { _localizer["Driving Type"], (row,item) => item.DrivingType = row[_localizer["Driving Type"]]?.ToString() },
                { _localizer["Pay Period"], (row,item) => item.PayPeriod = row[_localizer["Pay Period"]]?.ToString() },
                { _localizer["Remark"], (row,item) => item.Remark = row[_localizer["Remark"]]?.ToString() },
            }, _localizer["Drivers"]);
        if (result.Succeeded)
        {
            foreach (var dto in result.Data)
            {
                var item = _mapper.Map<Driver>(dto);
                await _context.Drivers.AddAsync(item, cancellationToken);
            }
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        return Result.Failure(result.Errors);
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

