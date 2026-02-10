namespace ComputerCare.Application.DTOs.Cart;

public class AddToCartDto
{
    public Guid CustomerId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; } = 1;
}
