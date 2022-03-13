using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Blazor.Domain.Entities;
public class GoodsDetail:AuditableEntity
{
    public int Id { get; set; }
    public int ShippingOrderId { get; set; }
    public virtual ShippingOrder ShippingOrder { get; set; } = default!;
    public string? PickupAddress { get; set; }
    public string? DeliveryAddress { get; set; }
    public string? Goods { get; set; }
    public decimal? Freight { get; set; }
    public string? Customer { get; set; }
    public string? Remark { get; set; }
}
