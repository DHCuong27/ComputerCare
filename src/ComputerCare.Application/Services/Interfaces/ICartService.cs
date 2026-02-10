using ComputerCare.Application.DTOs.Cart;
using ComputerCare.Application.DTOs.Common;

namespace ComputerCare.Application.Services.Interfaces;

public interface ICartService
{
    Task<ResponseDto<CartDto>> GetCartByCustomerIdAsync(Guid customerId);
    Task<ResponseDto<CartDto>> AddToCartAsync(AddToCartDto dto);
    Task<ResponseDto<CartDto>> UpdateCartItemQuantityAsync(Guid cartItemId, int quantity);
    Task<ResponseDto<bool>> RemoveFromCartAsync(Guid cartItemId);
    Task<ResponseDto<bool>> ClearCartAsync(Guid customerId);
}
