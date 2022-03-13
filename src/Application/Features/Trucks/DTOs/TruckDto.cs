// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.Trucks.DTOs;


public class TruckDto:IMapFrom<Truck>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Truck, TruckDto>().ReverseMap();
    }
    public int Id { get; set; }
    public string? PlateNumber { get; set; }
    public string? TrailerNumber { get; set; }
    public string? VehicleType { get; set; }
    public string? Owner { get; set; }
    public string? Description { get; set; }
    public string? Driver { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Status { get; set; }
    public string? DeviceId { get; set; }

}

