// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Trucks.DTOs;

namespace CleanArchitecture.Blazor.Application.Features.Trucks.Commands.Delete;

    public class DeleteTruckCommand: IRequest<Result>
    {
      public int Id {  get; set; }
    }
    public class DeleteCheckedTrucksCommand : IRequest<Result>
    {
      public int[] Id {  get; set; }
    }

    public class DeleteTruckCommandHandler : 
                 IRequestHandler<DeleteTruckCommand, Result>,
                 IRequestHandler<DeleteCheckedTrucksCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<DeleteTruckCommandHandler> _localizer;
        public DeleteTruckCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<DeleteTruckCommandHandler> localizer,
             IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result> Handle(DeleteTruckCommand request, CancellationToken cancellationToken)
        {
       
           var item = await _context.Trucks.FindAsync(new object[] { request.Id }, cancellationToken);
           _ = item ?? throw new NotFoundException("Truck {request.Id} Not Found.");
           _context.Trucks.Remove(item);
           await _context.SaveChangesAsync(cancellationToken);
           return Result.Success();
        }

        public async Task<Result> Handle(DeleteCheckedTrucksCommand request, CancellationToken cancellationToken)
        {
         
           var items = await _context.Trucks.Where(x => request.Id.Contains(x.Id)).ToListAsync(cancellationToken);
            foreach (var item in items)
            {
                _context.Trucks.Remove(item);
            }
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }

