using ComputerCare.Domain.Enums;

namespace ComputerCare.Application.DTOs.Service;

public class CreateServiceDto
{
    public ServiceType ServiceType { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal BasePrice { get; set; }
    public int EstimatedDurationMinutes { get; set; }
}
