using ComputerCare.Application.DTOs.Common;
using ComputerCare.Application.DTOs.Product;
using ComputerCare.Domain.Enums;

namespace ComputerCare.Application.Services.Interfaces;

public interface IProductService
{
    Task<ResponseDto<ProductDto>> GetByIdAsync(Guid id);
    Task<ResponseDto<PaginatedResult<ProductDto>>> GetAllAsync(int pageNumber = 1, int pageSize = 20);
    Task<ResponseDto<IEnumerable<ProductDto>>> GetByCategoryAsync(Guid categoryId);
    Task<ResponseDto<IEnumerable<ProductDto>>> GetByProductTypeAsync(ProductType productType);
    Task<ResponseDto<IEnumerable<ProductDto>>> SearchAsync(string searchTerm);
    Task<ResponseDto<IEnumerable<ProductDto>>> GetFeaturedProductsAsync(int count = 10);
    Task<ResponseDto<ProductDto>> CreateAsync(CreateProductDto dto);
    Task<ResponseDto<ProductDto>> UpdateAsync(UpdateProductDto dto);
    Task<ResponseDto<bool>> DeleteAsync(Guid id);
    Task<ResponseDto<IEnumerable<ProductDto>>> GetLowStockProductsAsync(int threshold = 10);
}
