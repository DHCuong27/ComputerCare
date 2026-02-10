using ComputerCare.Domain.Entities;
using ComputerCare.Domain.Enums;
using ComputerCare.Domain.Interfaces;
using ComputerCare.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ComputerCare.Infrastructure.Repositories;

public class RepairRequestRepository : GenericRepository<RepairRequest>, IRepairRequestRepository
{
    public RepairRequestRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<RepairRequest>> GetByCustomerIdAsync(Guid customerId)
    {
        return await _dbSet
            .Where(r => r.CustomerId == customerId)
            .Include(r => r.RepairRequestItems)
                .ThenInclude(ri => ri.Service)
            .OrderByDescending(r => r.CreatedDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<RepairRequest>> GetByStatusAsync(RepairStatus status)
    {
        return await _dbSet
            .Where(r => r.Status == status)
            .Include(r => r.Customer)
            .Include(r => r.AssignedEmployee)
            .OrderByDescending(r => r.CreatedDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<RepairRequest>> GetByEmployeeIdAsync(Guid employeeId)
    {
        return await _dbSet
            .Where(r => r.AssignedEmployeeId == employeeId)
            .Include(r => r.Customer)
            .Include(r => r.RepairRequestItems)
                .ThenInclude(ri => ri.Service)
            .OrderByDescending(r => r.CreatedDate)
            .ToListAsync();
    }

    public async Task<RepairRequest?> GetWithDetailsAsync(Guid id)
    {
        return await _dbSet
            .Include(r => r.Customer)
            .Include(r => r.AssignedEmployee)
            .Include(r => r.RepairRequestItems)
                .ThenInclude(ri => ri.Service)
            .FirstOrDefaultAsync(r => r.Id == id);
    }
}
