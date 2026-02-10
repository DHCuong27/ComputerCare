using AutoMapper;
using ComputerCare.Application.DTOs.Common;
using ComputerCare.Application.DTOs.Order;
using ComputerCare.Application.Services.Interfaces;
using ComputerCare.Domain.Entities;
using ComputerCare.Domain.Enums;
using ComputerCare.Domain.Interfaces;

namespace ComputerCare.Application.Services.Implementations;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseDto<OrderDto>> GetByIdAsync(Guid id)
    {
        try
        {
            var order = await _unitOfWork.Orders.GetOrderWithDetailsAsync(id);
            if (order == null)
                return ResponseDto<OrderDto>.FailureResult($"Order with id {id} not found");

            var orderDto = _mapper.Map<OrderDto>(order);
            return ResponseDto<OrderDto>.SuccessResult(orderDto);
        }
        catch (Exception ex)
        {
            return ResponseDto<OrderDto>.FailureResult($"Error retrieving order: {ex.Message}");
        }
    }

    public async Task<ResponseDto<IEnumerable<OrderDto>>> GetByCustomerIdAsync(Guid customerId)
    {
        try
        {
            var orders = await _unitOfWork.Orders.GetByCustomerIdAsync(customerId);
            var orderDtos = _mapper.Map<IEnumerable<OrderDto>>(orders);
            return ResponseDto<IEnumerable<OrderDto>>.SuccessResult(orderDtos);
        }
        catch (Exception ex)
        {
            return ResponseDto<IEnumerable<OrderDto>>.FailureResult($"Error retrieving orders: {ex.Message}");
        }
    }

    public async Task<ResponseDto<IEnumerable<OrderDto>>> GetByStatusAsync(OrderStatus status)
    {
        try
        {
            var orders = await _unitOfWork.Orders.GetByStatusAsync(status);
            var orderDtos = _mapper.Map<IEnumerable<OrderDto>>(orders);
            return ResponseDto<IEnumerable<OrderDto>>.SuccessResult(orderDtos);
        }
        catch (Exception ex)
        {
            return ResponseDto<IEnumerable<OrderDto>>.FailureResult($"Error retrieving orders: {ex.Message}");
        }
    }

    public async Task<ResponseDto<OrderDto>> CreateAsync(CreateOrderDto dto)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            // Create order
            var order = _mapper.Map<Order>(dto);
            order.Id = Guid.NewGuid();
            order.OrderNumber = GenerateOrderNumber();
            order.OrderDate = DateTime.UtcNow;
            order.Status = OrderStatus.Pending;
            order.PaymentStatus = PaymentStatus.Pending;

            // Calculate total and create order items
            decimal totalAmount = 0;
            var orderItems = new List<OrderItem>();

            foreach (var itemDto in dto.OrderItems)
            {
                var product = await _unitOfWork.Products.GetByIdAsync(itemDto.ProductId);
                if (product == null)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return ResponseDto<OrderDto>.FailureResult($"Product with id {itemDto.ProductId} not found");
                }

                if (product.Stock < itemDto.Quantity)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return ResponseDto<OrderDto>.FailureResult($"Insufficient stock for product {product.Name}");
                }

                var orderItem = new OrderItem
                {
                    Id = Guid.NewGuid(),
                    OrderId = order.Id,
                    ProductId = itemDto.ProductId,
                    Quantity = itemDto.Quantity,
                    UnitPrice = product.Price,
                    Discount = 0,
                    TotalPrice = product.Price * itemDto.Quantity
                };

                totalAmount += orderItem.TotalPrice;
                orderItems.Add(orderItem);

                // Update product stock
                product.Stock -= itemDto.Quantity;
                await _unitOfWork.Products.UpdateAsync(product);
            }

            order.TotalAmount = totalAmount;
            await _unitOfWork.Orders.AddAsync(order);
            
            // Add order items
            var orderItemRepository = _unitOfWork.Repository<OrderItem>();
            foreach (var item in orderItems)
            {
                await orderItemRepository.AddAsync(item);
            }

            await _unitOfWork.CommitTransactionAsync();

            // Reload order with details
            var createdOrder = await _unitOfWork.Orders.GetOrderWithDetailsAsync(order.Id);
            var orderDto = _mapper.Map<OrderDto>(createdOrder);
            
            return ResponseDto<OrderDto>.SuccessResult(orderDto, "Order created successfully");
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            return ResponseDto<OrderDto>.FailureResult($"Error creating order: {ex.Message}");
        }
    }

    public async Task<ResponseDto<OrderDto>> UpdateStatusAsync(Guid id, OrderStatus status)
    {
        try
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(id);
            if (order == null)
                return ResponseDto<OrderDto>.FailureResult($"Order with id {id} not found");

            order.Status = status;
            await _unitOfWork.Orders.UpdateAsync(order);
            await _unitOfWork.SaveChangesAsync();

            var orderDto = _mapper.Map<OrderDto>(order);
            return ResponseDto<OrderDto>.SuccessResult(orderDto, "Order status updated successfully");
        }
        catch (Exception ex)
        {
            return ResponseDto<OrderDto>.FailureResult($"Error updating order status: {ex.Message}");
        }
    }

    public async Task<ResponseDto<OrderDto>> UpdatePaymentStatusAsync(Guid id, PaymentStatus paymentStatus)
    {
        try
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(id);
            if (order == null)
                return ResponseDto<OrderDto>.FailureResult($"Order with id {id} not found");

            order.PaymentStatus = paymentStatus;
            await _unitOfWork.Orders.UpdateAsync(order);
            await _unitOfWork.SaveChangesAsync();

            var orderDto = _mapper.Map<OrderDto>(order);
            return ResponseDto<OrderDto>.SuccessResult(orderDto, "Payment status updated successfully");
        }
        catch (Exception ex)
        {
            return ResponseDto<OrderDto>.FailureResult($"Error updating payment status: {ex.Message}");
        }
    }

    public async Task<ResponseDto<PaginatedResult<OrderDto>>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
    {
        try
        {
            var orders = await _unitOfWork.Orders.GetAllAsync();
            var totalCount = orders.Count();

            var paginatedOrders = orders
                .OrderByDescending(o => o.OrderDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var orderDtos = _mapper.Map<List<OrderDto>>(paginatedOrders);

            var result = new PaginatedResult<OrderDto>
            {
                Items = orderDtos,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };

            return ResponseDto<PaginatedResult<OrderDto>>.SuccessResult(result);
        }
        catch (Exception ex)
        {
            return ResponseDto<PaginatedResult<OrderDto>>.FailureResult($"Error retrieving orders: {ex.Message}");
        }
    }

    private string GenerateOrderNumber()
    {
        return $"ORD-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";
    }
}
