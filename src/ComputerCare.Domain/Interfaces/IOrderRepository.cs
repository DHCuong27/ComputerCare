using ComputerCare.Domain.Entities;
using ComputerCare.Domain.Enums;

namespace ComputerCare.Domain.Interfaces;

public interface IOrderRepository : IRepository<Order>
{
    Task<IEnumerable<Order>> GetByCustomerIdAsync(Guid customerId);
    Task<IEnumerable<Order>> GetByStatusAsync(OrderStatus status);
    Task<Order?> GetByOrderNumberAsync(string orderNumber);
    Task<IEnumerable<Order>> GetRecentOrdersAsync(int count);
    Task<Order?> GetOrderWithDetailsAsync(Guid id);
}
