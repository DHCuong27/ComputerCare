namespace ComputerCare.Application.DTOs.Service;

public class ServiceBookingDto
{
    public Guid CustomerId { get; set; }
    public Guid ServiceId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string Notes { get; set; } = string.Empty;
}
