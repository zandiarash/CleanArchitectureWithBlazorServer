using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Blazor.Domain.Entities;
public class Truck : AuditableEntity
{
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
