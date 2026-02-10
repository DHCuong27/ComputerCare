using ComputerCare.Application.DTOs.Common;
using ComputerCare.Application.DTOs.Order;
using ComputerCare.Domain.Enums;

namespace ComputerCare.Application.Services.Interfaces;

public interface IOrderService
{
    Task<ResponseDto<OrderDto>> GetByIdAsync(Guid id);
    Task<ResponseDto<IEnumerable<OrderDto>>> GetByCustomerIdAsync(Guid customerId);
    Task<ResponseDto<IEnumerable<OrderDto>>> GetByStatusAsync(OrderStatus status);
    Task<ResponseDto<OrderDto>> CreateAsync(CreateOrderDto dto);
    Task<ResponseDto<OrderDto>> UpdateStatusAsync(Guid id, OrderStatus status);
    Task<ResponseDto<OrderDto>> UpdatePaymentStatusAsync(Guid id, PaymentStatus paymentStatus);
    Task<ResponseDto<PaginatedResult<OrderDto>>> GetAllAsync(int pageNumber = 1, int pageSize = 20);
}
