// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.ShippingOrders.Commands.Delete;

public class DeleteGoodsItemCommand : IRequest<Result>
{
    public int Id { get; set; }
    public DeleteGoodsItemCommand(int id)
    {
        Id = id;
    }
}


public class DeleteGoodsItemCommandHandler :
             IRequestHandler<DeleteGoodsItemCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public DeleteGoodsItemCommandHandler(
        IApplicationDbContext context
        )
    {
        _context = context;

    }
    public async Task<Result> Handle(DeleteGoodsItemCommand request, CancellationToken cancellationToken)
    {

        var item = await _context.GoodsDetails.FindAsync(new object[] { request.Id }, cancellationToken);
        _ = item ?? throw new NotFoundException("Goods Item {request.Id} Not Found.");
        _context.GoodsDetails.Remove(item);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }


}

