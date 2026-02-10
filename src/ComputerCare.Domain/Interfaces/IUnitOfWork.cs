namespace ComputerCare.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IProductRepository Products { get; }
    IOrderRepository Orders { get; }
    IServiceRepository Services { get; }
    IRepairRequestRepository RepairRequests { get; }
    IRepository<T> Repository<T>() where T : class;
    
    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}
