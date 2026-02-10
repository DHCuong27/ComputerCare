using ComputerCare.Domain.Interfaces;
using ComputerCare.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace ComputerCare.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IDbContextTransaction? _transaction;
    private readonly Dictionary<Type, object> _repositories;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        _repositories = new Dictionary<Type, object>();
        
        // Initialize specific repositories
        Products = new ProductRepository(context);
        Orders = new OrderRepository(context);
        Services = new ServiceRepository(context);
        RepairRequests = new RepairRequestRepository(context);
    }

    public IProductRepository Products { get; }
    public IOrderRepository Orders { get; }
    public IServiceRepository Services { get; }
    public IRepairRequestRepository RepairRequests { get; }

    public IRepository<T> Repository<T>() where T : class
    {
        var type = typeof(T);
        
        if (_repositories.ContainsKey(type))
        {
            return (IRepository<T>)_repositories[type];
        }

        var repositoryInstance = new GenericRepository<T>(_context);
        _repositories.Add(type, repositoryInstance);
        
        return repositoryInstance;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
            
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
            }
        }
        catch
        {
            await RollbackTransactionAsync();
            throw;
        }
        finally
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }
}
