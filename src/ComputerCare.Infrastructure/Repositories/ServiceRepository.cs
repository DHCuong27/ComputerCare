using ComputerCare.Domain.Entities;
using ComputerCare.Domain.Enums;
using ComputerCare.Domain.Interfaces;
using ComputerCare.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ComputerCare.Infrastructure.Repositories;

public class ServiceRepository : GenericRepository<Service>, IServiceRepository
{
    public ServiceRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Service>> GetByServiceTypeAsync(ServiceType serviceType)
    {
        return await _dbSet
            .Where(s => s.ServiceType == serviceType && s.IsActive)
            .ToListAsync();
    }

    public async Task<IEnumerable<Service>> GetActiveServicesAsync()
    {
        return await _dbSet
            .Where(s => s.IsActive)
            .OrderBy(s => s.ServiceType)
            .ThenBy(s => s.Name)
            .ToListAsync();
    }
}
