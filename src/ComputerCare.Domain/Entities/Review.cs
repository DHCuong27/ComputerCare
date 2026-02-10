namespace ComputerCare.Domain.Entities;

public class Review : BaseEntity
{
    public Guid ProductId { get; set; }
    public Guid CustomerId { get; set; }
    public int Rating { get; set; } // 1-5
    public string Comment { get; set; } = string.Empty;

    // Navigation properties
    public Product Product { get; set; } = null!;
    public Customer Customer { get; set; } = null!;
}
