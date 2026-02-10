using ComputerCare.Domain.Enums;

namespace ComputerCare.Domain.Entities;

public class Service : BaseEntity
{
    public ServiceType ServiceType { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal BasePrice { get; set; }
    public int EstimatedDurationMinutes { get; set; }
    public bool IsActive { get; set; } = true;

    // Navigation properties
    public ICollection<RepairRequestItem> RepairRequestItems { get; set; } = new List<RepairRequestItem>();
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
