using ComputerCare.Domain.Entities;
using ComputerCare.Domain.Enums;

namespace ComputerCare.Domain.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetByCategoryAsync(Guid categoryId);
    Task<IEnumerable<Product>> GetByProductTypeAsync(ProductType productType);
    Task<IEnumerable<Product>> SearchAsync(string searchTerm);
    Task<IEnumerable<Product>> GetFeaturedProductsAsync(int count);
    Task<Product?> GetBySkuAsync(string sku);
    Task<IEnumerable<Product>> GetLowStockProductsAsync(int threshold);
}
