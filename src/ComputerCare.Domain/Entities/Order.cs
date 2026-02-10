using ComputerCare.Domain.Enums;

namespace ComputerCare.Domain.Entities;

public class Order : BaseEntity
{
    public string UserId { get; set; } = string.Empty; // ApplicationUser Id
    public string OrderNumber { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string ShippingAddress { get; set; } = string.Empty;
    public OrderStatus Status { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public string Notes { get; set; } = string.Empty;

    // Navigation properties
    // Note: Customer entity is kept for backward compatibility
    // but UserId now references ApplicationUser
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public Invoice? Invoice { get; set; }
    public ICollection<Warranty> Warranties { get; set; } = new List<Warranty>();
}
