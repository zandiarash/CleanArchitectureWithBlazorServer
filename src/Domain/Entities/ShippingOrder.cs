using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Blazor.Domain.Entities;
public  class ShippingOrder:AuditableEntity
{
    public int Id { get; set; }
    public string? OrderNo { get; set; }
    public DateTime? StartingTime { get; set; }
    public DateTime? FinishTime { get; set; }
    public int TruckId { get; set; }
    public virtual Truck Truck { get; set; } = default!;
    public string? PlateNumber { get; set; }
    public int DriverId { get; set; }
    public virtual Driver Driver { get; set; } = default!;
    public string? DriverName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Dispatcher { get; set; }
    public string? Description { get; set; }
    public decimal? Freight { get; set; }
    public decimal? CashAdvance { get; set; }
    public decimal? Cost { get; set; }
    public decimal? GrossMargin { get; set; }
    public string? Status { get; set; }
    public string? Remark { get; set; }
    public virtual ICollection<GoodsDetail> GoodsDetails { get; set; }= new HashSet<GoodsDetail>();
    public virtual ICollection<CostDetail> CostDetails { get; set; }= new HashSet<CostDetail>();
}
