using AutoMapper;
using ComputerCare.Application.DTOs.Common;
using ComputerCare.Application.DTOs.Product;
using ComputerCare.Application.Services.Interfaces;
using ComputerCare.Domain.Entities;
using ComputerCare.Domain.Enums;
using ComputerCare.Domain.Interfaces;
using ComputerCare.Shared.Exceptions;

namespace ComputerCare.Application.Services.Implementations;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseDto<ProductDto>> GetByIdAsync(Guid id)
    {
        try
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null)
                return ResponseDto<ProductDto>.FailureResult($"Product with id {id} not found");

            var productDto = _mapper.Map<ProductDto>(product);
            return ResponseDto<ProductDto>.SuccessResult(productDto);
        }
        catch (Exception ex)
        {
            return ResponseDto<ProductDto>.FailureResult($"Error retrieving product: {ex.Message}");
        }
    }

    public async Task<ResponseDto<PaginatedResult<ProductDto>>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
    {
        try
        {
            var products = await _unitOfWork.Products.GetAllAsync();
            var totalCount = products.Count();
            
            var paginatedProducts = products
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var productDtos = _mapper.Map<List<ProductDto>>(paginatedProducts);

            var result = new PaginatedResult<ProductDto>
            {
                Items = productDtos,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };

            return ResponseDto<PaginatedResult<ProductDto>>.SuccessResult(result);
        }
        catch (Exception ex)
        {
            return ResponseDto<PaginatedResult<ProductDto>>.FailureResult($"Error retrieving products: {ex.Message}");
        }
    }

    public async Task<ResponseDto<IEnumerable<ProductDto>>> GetByCategoryAsync(Guid categoryId)
    {
        try
        {
            var products = await _unitOfWork.Products.GetByCategoryAsync(categoryId);
            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
            return ResponseDto<IEnumerable<ProductDto>>.SuccessResult(productDtos);
        }
        catch (Exception ex)
        {
            return ResponseDto<IEnumerable<ProductDto>>.FailureResult($"Error retrieving products: {ex.Message}");
        }
    }

    public async Task<ResponseDto<IEnumerable<ProductDto>>> GetByProductTypeAsync(ProductType productType)
    {
        try
        {
            var products = await _unitOfWork.Products.GetByProductTypeAsync(productType);
            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
            return ResponseDto<IEnumerable<ProductDto>>.SuccessResult(productDtos);
        }
        catch (Exception ex)
        {
            return ResponseDto<IEnumerable<ProductDto>>.FailureResult($"Error retrieving products: {ex.Message}");
        }
    }

    public async Task<ResponseDto<IEnumerable<ProductDto>>> SearchAsync(string searchTerm)
    {
        try
        {
            var products = await _unitOfWork.Products.SearchAsync(searchTerm);
            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
            return ResponseDto<IEnumerable<ProductDto>>.SuccessResult(productDtos);
        }
        catch (Exception ex)
        {
            return ResponseDto<IEnumerable<ProductDto>>.FailureResult($"Error searching products: {ex.Message}");
        }
    }

    public async Task<ResponseDto<IEnumerable<ProductDto>>> GetFeaturedProductsAsync(int count = 10)
    {
        try
        {
            var products = await _unitOfWork.Products.GetFeaturedProductsAsync(count);
            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
            return ResponseDto<IEnumerable<ProductDto>>.SuccessResult(productDtos);
        }
        catch (Exception ex)
        {
            return ResponseDto<IEnumerable<ProductDto>>.FailureResult($"Error retrieving featured products: {ex.Message}");
        }
    }

    public async Task<ResponseDto<ProductDto>> CreateAsync(CreateProductDto dto)
    {
        try
        {
            // Check if SKU already exists
            var existingProduct = await _unitOfWork.Products.GetBySkuAsync(dto.SKU);
            if (existingProduct != null)
                return ResponseDto<ProductDto>.FailureResult("Product with this SKU already exists");

            var product = _mapper.Map<Product>(dto);
            product.Id = Guid.NewGuid();
            product.IsActive = true;

            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();

            var productDto = _mapper.Map<ProductDto>(product);
            return ResponseDto<ProductDto>.SuccessResult(productDto, "Product created successfully");
        }
        catch (Exception ex)
        {
            return ResponseDto<ProductDto>.FailureResult($"Error creating product: {ex.Message}");
        }
    }

    public async Task<ResponseDto<ProductDto>> UpdateAsync(UpdateProductDto dto)
    {
        try
        {
            var product = await _unitOfWork.Products.GetByIdAsync(dto.Id);
            if (product == null)
                return ResponseDto<ProductDto>.FailureResult($"Product with id {dto.Id} not found");

            _mapper.Map(dto, product);
            await _unitOfWork.Products.UpdateAsync(product);
            await _unitOfWork.SaveChangesAsync();

            var productDto = _mapper.Map<ProductDto>(product);
            return ResponseDto<ProductDto>.SuccessResult(productDto, "Product updated successfully");
        }
        catch (Exception ex)
        {
            return ResponseDto<ProductDto>.FailureResult($"Error updating product: {ex.Message}");
        }
    }

    public async Task<ResponseDto<bool>> DeleteAsync(Guid id)
    {
        try
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null)
                return ResponseDto<bool>.FailureResult($"Product with id {id} not found");

            // Soft delete - just mark as inactive
            product.IsActive = false;
            await _unitOfWork.Products.UpdateAsync(product);
            await _unitOfWork.SaveChangesAsync();

            return ResponseDto<bool>.SuccessResult(true, "Product deleted successfully");
        }
        catch (Exception ex)
        {
            return ResponseDto<bool>.FailureResult($"Error deleting product: {ex.Message}");
        }
    }

    public async Task<ResponseDto<IEnumerable<ProductDto>>> GetLowStockProductsAsync(int threshold = 10)
    {
        try
        {
            var products = await _unitOfWork.Products.GetLowStockProductsAsync(threshold);
            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
            return ResponseDto<IEnumerable<ProductDto>>.SuccessResult(productDtos);
        }
        catch (Exception ex)
        {
            return ResponseDto<IEnumerable<ProductDto>>.FailureResult($"Error retrieving low stock products: {ex.Message}");
        }
    }
}
