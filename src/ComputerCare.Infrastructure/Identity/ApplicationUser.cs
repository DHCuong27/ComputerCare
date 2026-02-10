using Microsoft.AspNetCore.Identity;
using ComputerCare.Domain.Entities;

namespace ComputerCare.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName => $"{FirstName} {LastName}";
    public string? Avatar { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? District { get; set; }
    public string? Ward { get; set; }
    public int LoyaltyPoints { get; set; } = 0;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? LastLoginDate { get; set; }
    public bool IsActive { get; set; } = true;
    
    // Navigation properties
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    public virtual ICollection<RepairRequest> RepairRequests { get; set; } = new List<RepairRequest>();
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    public virtual Cart? Cart { get; set; }
}
