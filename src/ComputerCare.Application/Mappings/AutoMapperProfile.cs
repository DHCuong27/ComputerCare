using AutoMapper;
using ComputerCare.Application.DTOs.Cart;
using ComputerCare.Application.DTOs.Order;
using ComputerCare.Application.DTOs.Product;
using ComputerCare.Application.DTOs.Repair;
using ComputerCare.Application.DTOs.Service;
using ComputerCare.Domain.Entities;
using System.Text.Json;

namespace ComputerCare.Application.Mappings;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // Product mappings
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src => DeserializeList(src.ImageUrls)))
            .ForMember(dest => dest.Specifications, opt => opt.MapFrom(src => DeserializeDictionary(src.Specifications)));

        CreateMap<CreateProductDto, Product>()
            .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src => SerializeList(src.ImageUrls)))
            .ForMember(dest => dest.Specifications, opt => opt.MapFrom(src => SerializeDictionary(src.Specifications)))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore());

        CreateMap<UpdateProductDto, Product>()
            .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src => SerializeList(src.ImageUrls)))
            .ForMember(dest => dest.Specifications, opt => opt.MapFrom(src => SerializeDictionary(src.Specifications)))
            .ForMember(dest => dest.SKU, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore());

        // Order mappings
        CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.FullName));

        CreateMap<OrderItem, OrderItemDto>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));

        CreateMap<CreateOrderDto, Order>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.OrderNumber, opt => opt.Ignore())
            .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => Domain.Enums.OrderStatus.Pending))
            .ForMember(dest => dest.PaymentStatus, opt => opt.MapFrom(_ => Domain.Enums.PaymentStatus.Pending));

        // Cart mappings
        CreateMap<Cart, CartDto>();
        
        CreateMap<CartItem, CartItemDto>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
            .ForMember(dest => dest.ProductImage, opt => opt.MapFrom(src => GetFirstImage(src.Product.ImageUrls)))
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.Product.Price))
            .ForMember(dest => dest.StockAvailable, opt => opt.MapFrom(src => src.Product.Stock));

        // Service mappings
        CreateMap<Service, ServiceDto>();
        CreateMap<CreateServiceDto, Service>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true))
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore());

        // Repair mappings
        CreateMap<RepairRequest, RepairRequestDto>()
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.FullName))
            .ForMember(dest => dest.AssignedEmployeeName, opt => opt.MapFrom(src => src.AssignedEmployee != null ? src.AssignedEmployee.FullName : null));

        CreateMap<RepairRequestItem, RepairRequestItemDto>()
            .ForMember(dest => dest.ServiceName, opt => opt.MapFrom(src => src.Service.Name));

        CreateMap<CreateRepairRequestDto, RepairRequest>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => Domain.Enums.RepairStatus.Pending))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(_ => DateTime.UtcNow));
    }

    private static List<string> DeserializeList(string json)
    {
        try
        {
            return JsonSerializer.Deserialize<List<string>>(json) ?? new List<string>();
        }
        catch
        {
            return new List<string>();
        }
    }

    private static Dictionary<string, string> DeserializeDictionary(string json)
    {
        try
        {
            return JsonSerializer.Deserialize<Dictionary<string, string>>(json) ?? new Dictionary<string, string>();
        }
        catch
        {
            return new Dictionary<string, string>();
        }
    }

    private static string GetFirstImage(string imageUrlsJson)
    {
        var images = DeserializeList(imageUrlsJson);
        return images.FirstOrDefault() ?? string.Empty;
    }

    private static string SerializeList(List<string> list)
    {
        return list != null ? JsonSerializer.Serialize(list) : "[]";
    }

    private static string SerializeDictionary(Dictionary<string, string> dictionary)
    {
        return dictionary != null ? JsonSerializer.Serialize(dictionary) : "{}";
    }
}
