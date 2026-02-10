using ComputerCare.Domain.Entities;
using ComputerCare.Domain.Enums;

namespace ComputerCare.Domain.Interfaces;

public interface IServiceRepository : IRepository<Service>
{
    Task<IEnumerable<Service>> GetByServiceTypeAsync(ServiceType serviceType);
    Task<IEnumerable<Service>> GetActiveServicesAsync();
}
