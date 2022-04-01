// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections.ObjectModel;

namespace CleanArchitecture.Blazor.Application.Features.ShippingOrders.DTOs;


public  class ShippingOrderDto : IMapFrom<ShippingOrder>
{

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ShippingOrder, ShippingOrderDto>();
        profile.CreateMap<ShippingOrderDto, ShippingOrder>();
    }
    public int Id { get; set; }
    public string? OrderNo { get; set; }

    public string? Trip { get; set; }
    public DateTime? StartingTime { get; set; }
    public DateTime? FinishTime { get; set; }
    public int TruckId { get; set; }
    public int DriverId { get; set; }
    public string? PlateNumber { get; set; }
    public string? DriverName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Dispatcher { get; set; }
    public string? Description { get; set; }
    public decimal? Freight { get; set; }
    public decimal? CashAdvance { get; set; }
    public decimal? Cost { get; set; }
    public decimal? GrossMargin{get;set;}
    public string? Status { get; set; }
    public string? Remark { get; set; }

    public List<CostDetailDto> CostDetailDtos { get; set; } = new();
    public List<GoodsDetailDto> GoodsDetailDtos { get; set; } = new();

}

