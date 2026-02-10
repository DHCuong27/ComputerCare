namespace ComputerCare.Application.DTOs.Repair;

public class CreateRepairRequestDto
{
    public Guid CustomerId { get; set; }
    public string DeviceType { get; set; } = string.Empty;
    public string IssueDescription { get; set; } = string.Empty;
}
