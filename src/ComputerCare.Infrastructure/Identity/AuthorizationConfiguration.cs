using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using ComputerCare.Shared.Constants;

namespace ComputerCare.Infrastructure.Identity;

public static class AuthorizationConfiguration
{
    public static IServiceCollection AddAuthorizationPolicies(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            // Super Admin và Admin
            options.AddPolicy("AdminOnly", policy => 
                policy.RequireRole(AppRoles.SuperAdmin, AppRoles.Admin));
            
            // Quản lý sản phẩm
            options.AddPolicy("ManageProducts", policy =>
                policy.RequireRole(AppRoles.SuperAdmin, AppRoles.Admin, AppRoles.Manager));
            
            // Quản lý đơn hàng
            options.AddPolicy("ManageOrders", policy =>
                policy.RequireRole(AppRoles.SuperAdmin, AppRoles.Admin, AppRoles.Manager, AppRoles.Sales));
            
            // Quản lý sửa chữa
            options.AddPolicy("ManageRepairs", policy =>
                policy.RequireRole(AppRoles.SuperAdmin, AppRoles.Admin, AppRoles.Manager, AppRoles.Technician));
            
            // Xem báo cáo
            options.AddPolicy("ViewReports", policy =>
                policy.RequireRole(AppRoles.SuperAdmin, AppRoles.Admin, AppRoles.Manager));
            
            // Khách hàng
            options.AddPolicy("CustomerOnly", policy =>
                policy.RequireRole(AppRoles.Customer, AppRoles.VipCustomer));
            
            // Quản lý users (chỉ SuperAdmin)
            options.AddPolicy("ManageUsers", policy =>
                policy.RequireRole(AppRoles.SuperAdmin));
        });
        
        return services;
    }
}
