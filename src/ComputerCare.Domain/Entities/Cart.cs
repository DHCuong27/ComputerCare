namespace ComputerCare.Domain.Entities;

public class Cart : BaseEntity
{
    public string UserId { get; set; } = string.Empty; // ApplicationUser Id

    // Navigation properties
    // Note: Customer entity is kept for backward compatibility
    // but UserId now references ApplicationUser
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}
