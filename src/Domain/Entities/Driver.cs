using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Blazor.Domain.Entities;
public class Driver: AuditableEntity
{
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
