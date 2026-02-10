using ComputerCare.Domain.Enums;

namespace ComputerCare.Application.DTOs.Order;

public class CreateOrderDto
{
    public Guid CustomerId { get; set; }
    public string ShippingAddress { get; set; } = string.Empty;
    public PaymentMethod PaymentMethod { get; set; }
    public string Notes { get; set; } = string.Empty;
    public List<CreateOrderItemDto> OrderItems { get; set; } = new List<CreateOrderItemDto>();
}

public class CreateOrderItemDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}
