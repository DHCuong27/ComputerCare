using ComputerCare.Domain.Enums;

namespace ComputerCare.Application.DTOs.Repair;

public class RepairRequestDto
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string DeviceType { get; set; } = string.Empty;
    public string IssueDescription { get; set; } = string.Empty;
    public RepairStatus Status { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? CompletedDate { get; set; }
    public decimal TotalCost { get; set; }
    public string? AssignedEmployeeName { get; set; }
    public string Notes { get; set; } = string.Empty;
    public List<RepairRequestItemDto> RepairRequestItems { get; set; } = new List<RepairRequestItemDto>();
}

public class RepairRequestItemDto
{
    public Guid Id { get; set; }
    public Guid ServiceId { get; set; }
    public string ServiceName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Cost { get; set; }
}
