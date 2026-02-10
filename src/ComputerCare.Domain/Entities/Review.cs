namespace ComputerCare.Domain.Entities;

public class Review : BaseEntity
{
    public Guid ProductId { get; set; }
    public string UserId { get; set; } = string.Empty; // ApplicationUser Id
    public int Rating { get; set; } // 1-5
    public string Comment { get; set; } = string.Empty;

    // Navigation properties
    public Product Product { get; set; } = null!;
    // Note: Customer entity is kept for backward compatibility
    // but UserId now references ApplicationUser
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
}
