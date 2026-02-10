using ComputerCare.Domain.Entities;
using ComputerCare.Domain.Enums;

namespace ComputerCare.Domain.Interfaces;

public interface IRepairRequestRepository : IRepository<RepairRequest>
{
    Task<IEnumerable<RepairRequest>> GetByCustomerIdAsync(Guid customerId);
    Task<IEnumerable<RepairRequest>> GetByStatusAsync(RepairStatus status);
    Task<IEnumerable<RepairRequest>> GetByEmployeeIdAsync(Guid employeeId);
    Task<RepairRequest?> GetWithDetailsAsync(Guid id);
}
