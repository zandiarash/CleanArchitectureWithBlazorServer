using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Blazor.Domain.Entities;
public class CostDetail: AuditableEntity
{
    public int Id { get; set; }
    public int ShippingOrderId { get; set; }
    public virtual ShippingOrder ShippingOrder { get; set; } = default!;
    public string? Name { get; set; }
    public decimal? Cost { get; set; }
    public string? Paid { get; set; }
    public string? Cash { get; set; }
    public string? Remark { get; set; }
    public string? Picture { get; set; }
}
