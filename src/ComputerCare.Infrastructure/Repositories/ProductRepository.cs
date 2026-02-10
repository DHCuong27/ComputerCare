using ComputerCare.Domain.Entities;
using ComputerCare.Domain.Enums;
using ComputerCare.Domain.Interfaces;
using ComputerCare.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ComputerCare.Infrastructure.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Product>> GetByCategoryAsync(Guid categoryId)
    {
        return await _dbSet
            .Where(p => p.CategoryId == categoryId && p.IsActive)
            .Include(p => p.Category)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetByProductTypeAsync(ProductType productType)
    {
        return await _dbSet
            .Where(p => p.ProductType == productType && p.IsActive)
            .Include(p => p.Category)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> SearchAsync(string searchTerm)
    {
        return await _dbSet
            .Where(p => p.IsActive &&
                (p.Name.Contains(searchTerm) ||
                 p.Description.Contains(searchTerm) ||
                 p.Brand.Contains(searchTerm) ||
                 p.Model.Contains(searchTerm)))
            .Include(p => p.Category)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetFeaturedProductsAsync(int count)
    {
        return await _dbSet
            .Where(p => p.IsActive && p.Stock > 0)
            .OrderByDescending(p => p.CreatedDate)
            .Take(count)
            .Include(p => p.Category)
            .ToListAsync();
    }

    public async Task<Product?> GetBySkuAsync(string sku)
    {
        return await _dbSet
            .FirstOrDefaultAsync(p => p.SKU == sku);
    }

    public async Task<IEnumerable<Product>> GetLowStockProductsAsync(int threshold)
    {
        return await _dbSet
            .Where(p => p.IsActive && p.Stock <= threshold && p.Stock > 0)
            .Include(p => p.Category)
            .ToListAsync();
    }
}
