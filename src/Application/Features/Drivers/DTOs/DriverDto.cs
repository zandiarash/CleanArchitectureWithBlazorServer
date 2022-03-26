// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.Drivers.DTOs;


public class DriverDto:IMapFrom<Driver>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Driver, DriverDto>().ReverseMap();
    }
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? PhoneNumber { get; set; }
    public string? IdentityNo { get; set; }
    public string? Address { get; set; }
    public DateTime? BrithDay { get; set; }
    public int? Age { get; set; }
    public string? DrivingNo { get; set; }
    public string? DrivingType { get; set; }
    public string? PayPeriod { get; set; }
    public string? Remark { get; set; }

}

