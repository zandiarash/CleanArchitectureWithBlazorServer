// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.ShippingOrders.Commands.Delete;

public class DeleteCostItemCommand : IRequest<Result>
{
    public int Id { get; set; }
    public DeleteCostItemCommand(int id)
    {
        Id = id;
    }
}


public class DeleteCostItemCommandHandler :
             IRequestHandler<DeleteCostItemCommand, Result>
{
    private readonly IApplicationDbContext _context;
    public DeleteCostItemCommandHandler(
        IApplicationDbContext context

        )
    {
        _context = context;
     
    }
    public async Task<Result> Handle(DeleteCostItemCommand request, CancellationToken cancellationToken)
    {

        var item = await _context.CostDetails.FindAsync(new object[] { request.Id }, cancellationToken);
        _ = item ?? throw new NotFoundException("Cost Item {request.Id} Not Found.");
        _context.CostDetails.Remove(item);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }


}

