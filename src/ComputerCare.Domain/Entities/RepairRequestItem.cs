namespace ComputerCare.Domain.Entities;

public class RepairRequestItem : BaseEntity
{
    public Guid RepairRequestId { get; set; }
    public Guid ServiceId { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Cost { get; set; }

    // Navigation properties
    public RepairRequest RepairRequest { get; set; } = null!;
    public Service Service { get; set; } = null!;
}
