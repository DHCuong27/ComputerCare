using ComputerCare.Domain.Enums;

namespace ComputerCare.Domain.Entities;

public class Appointment : BaseEntity
{
    public Guid CustomerId { get; set; }
    public Guid ServiceId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public AppointmentStatus Status { get; set; }
    public string Notes { get; set; } = string.Empty;

    // Navigation properties
    public Customer Customer { get; set; } = null!;
    public Service Service { get; set; } = null!;
}
