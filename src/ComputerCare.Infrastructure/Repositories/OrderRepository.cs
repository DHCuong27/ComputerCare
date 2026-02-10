using ComputerCare.Domain.Entities;
using ComputerCare.Domain.Enums;
using ComputerCare.Domain.Interfaces;
using ComputerCare.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ComputerCare.Infrastructure.Repositories;

public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    public OrderRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Order>> GetByCustomerIdAsync(Guid customerId)
    {
        return await _dbSet
            .Where(o => o.CustomerId == customerId)
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
            .OrderByDescending(o => o.OrderDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Order>> GetByStatusAsync(OrderStatus status)
    {
        return await _dbSet
            .Where(o => o.Status == status)
            .Include(o => o.Customer)
            .Include(o => o.OrderItems)
            .OrderByDescending(o => o.OrderDate)
            .ToListAsync();
    }

    public async Task<Order?> GetByOrderNumberAsync(string orderNumber)
    {
        return await _dbSet
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
            .Include(o => o.Customer)
            .FirstOrDefaultAsync(o => o.OrderNumber == orderNumber);
    }

    public async Task<IEnumerable<Order>> GetRecentOrdersAsync(int count)
    {
        return await _dbSet
            .Include(o => o.Customer)
            .OrderByDescending(o => o.OrderDate)
            .Take(count)
            .ToListAsync();
    }

    public async Task<Order?> GetOrderWithDetailsAsync(Guid id)
    {
        return await _dbSet
            .Include(o => o.Customer)
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
            .Include(o => o.Invoice)
            .FirstOrDefaultAsync(o => o.Id == id);
    }
}
