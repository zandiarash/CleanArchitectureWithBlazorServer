using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Blazor.Domain.Entities;
public class CostDetailDto : IMapFrom<CostDetail>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CostDetail, CostDetailDto>().ReverseMap();
    }
    public int Id { get; set; }
    public int ShippingOrderId { get; set; }
    public string? Name { get; set; }
    public decimal? Cost { get; set; }
    public string? Remark { get; set; }
    public string? Picture { get; set; }
}
