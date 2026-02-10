using ComputerCare.Domain.Enums;

namespace ComputerCare.Domain.Entities;

public class RepairRequest : BaseEntity
{
    public Guid CustomerId { get; set; }
    public string DeviceType { get; set; } = string.Empty;
    public string IssueDescription { get; set; } = string.Empty;
    public RepairStatus Status { get; set; }
    public DateTime? CompletedDate { get; set; }
    public decimal TotalCost { get; set; }
    public Guid? AssignedEmployeeId { get; set; }
    public string Notes { get; set; } = string.Empty;

    // Navigation properties
    public Customer Customer { get; set; } = null!;
    public Employee? AssignedEmployee { get; set; }
    public ICollection<RepairRequestItem> RepairRequestItems { get; set; } = new List<RepairRequestItem>();
}
