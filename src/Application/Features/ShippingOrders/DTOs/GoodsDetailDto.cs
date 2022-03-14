using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Blazor.Domain.Entities;
public class GoodsDetailDto: IMapFrom<GoodsDetail>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<GoodsDetail, GoodsDetailDto>().ReverseMap();
    }
    public int Id { get; set; }
    public int ShippingOrderId { get; set; }
    public string? PickupAddress { get; set; }
    public string? DeliveryAddress { get; set; }
    public string? Goods { get; set; }
    public decimal? Freight { get; set; }
    public string? Customer { get; set; }
    public string? Remark { get; set; }
}
