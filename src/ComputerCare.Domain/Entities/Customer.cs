namespace ComputerCare.Domain.Entities;

public class Customer : BaseEntity
{
    public string UserId { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int LoyaltyPoints { get; set; }

    public string FullName => $"{FirstName} {LastName}";

    // Navigation properties
    public ICollection<Order> Orders { get; set; } = new List<Order>();
    public ICollection<Cart> Carts { get; set; } = new List<Cart>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<RepairRequest> RepairRequests { get; set; } = new List<RepairRequest>();
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
