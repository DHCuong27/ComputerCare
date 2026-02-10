namespace ComputerCare.Domain.Entities;

public class Warranty : BaseEntity
{
    public Guid ProductId { get; set; }
    public Guid OrderId { get; set; }
    public int WarrantyPeriodMonths { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    // Navigation properties
    public Product Product { get; set; } = null!;
    public Order Order { get; set; } = null!;
}
