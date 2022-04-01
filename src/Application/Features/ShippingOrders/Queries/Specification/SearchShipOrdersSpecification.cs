using CleanArchitecture.Blazor.Application.Features.ShippingOrders.Queries.Pagination;

namespace CleanArchitecture.Blazor.Application.Features.Products.Queries.Specification;
public class SearchShipOrdersSpecification : Specification<ShippingOrder>
{
    public SearchShipOrdersSpecification(ShippingOrdersWithPaginationQuery query)
    {
        Criteria = q => q.OrderNo != null;
        if (!string.IsNullOrEmpty(query.Keyword))
        {
            And(x => x.OrderNo.Contains(query.Keyword) || x.Description.Contains(query.Keyword));
        }
        if (!string.IsNullOrEmpty(query.OrderNo))
        {
            And(x => x.OrderNo.Contains(query.OrderNo));
        }
        if (!string.IsNullOrEmpty(query.Driver))
        {
            And(x => x.DriverName.Contains(query.Driver));
        }
        if (!string.IsNullOrEmpty(query.PlateNumber))
        {
            And(x => x.PlateNumber.Contains(query.PlateNumber));
        }
        if (!string.IsNullOrEmpty(query.Dispatcher))
        {
            And(x => x.Dispatcher.Contains(query.Dispatcher));
        }
        if (!string.IsNullOrEmpty(query.Status))
        {
            And(x => x.Status == query.Status);
        }
        if (!string.IsNullOrEmpty(query.Trip))
        {
            And(x => x.Trip == query.Trip);
        }
        if(query.DeliveryTime1 is not null && query.DeliveryTime2 is not null)
        {
            And(x => x.FinishTime >= query.DeliveryTime1 && x.FinishTime < query.DeliveryTime2.Value.AddDays(1));
        }
        if (query.PickupTime1 is not null && query.PickupTime2 is not null)
        {
            And(x => x.StartingTime >= query.PickupTime1 && x.StartingTime < query.PickupTime2.Value.AddDays(1));
        }
    }
}
