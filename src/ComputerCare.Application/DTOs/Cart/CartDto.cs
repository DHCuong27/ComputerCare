namespace ComputerCare.Application.DTOs.Cart;

public class CartDto
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public List<CartItemDto> CartItems { get; set; } = new List<CartItemDto>();
    public decimal TotalAmount => CartItems.Sum(i => i.TotalPrice);
    public int TotalItems => CartItems.Sum(i => i.Quantity);
}

public class CartItemDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string ProductImage { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice => UnitPrice * Quantity;
    public int StockAvailable { get; set; }
}
