using ComputerCare.Domain.Enums;

namespace ComputerCare.Application.DTOs.Product;

public class UpdateProductDto
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public List<string> ImageUrls { get; set; } = new List<string>();
    public Dictionary<string, string> Specifications { get; set; } = new Dictionary<string, string>();
    public ProductType ProductType { get; set; }
    public bool IsActive { get; set; }
}
