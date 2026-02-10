namespace ComputerCare.Domain.Entities;

public class Cart : BaseEntity
{
    public Guid CustomerId { get; set; }

    // Navigation properties
    public Customer Customer { get; set; } = null!;
    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}
