using ComputerCare.Domain.Entities;
using ComputerCare.Domain.Enums;
using ComputerCare.Infrastructure.Identity;
using ComputerCare.Shared.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ComputerCare.Infrastructure.Data;

public static class DbInitializer
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        // Apply migrations
        await context.Database.MigrateAsync();

        // Seed roles
        await SeedRolesAsync(roleManager);

        // Seed admin user
        await SeedAdminUserAsync(userManager);

        // Seed data only if database is empty
        if (!await context.Categories.AnyAsync())
        {
            await SeedCategoriesAsync(context);
            await SeedProductsAsync(context);
            await SeedServicesAsync(context);
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        string[] roles = 
        { 
            AppRoles.SuperAdmin, 
            AppRoles.Admin, 
            AppRoles.Manager, 
            AppRoles.Technician, 
            AppRoles.Sales, 
            AppRoles.Customer, 
            AppRoles.VipCustomer 
        };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }

    private static async Task SeedAdminUserAsync(UserManager<ApplicationUser> userManager)
    {
        var superAdminEmail = "admin@computercare.com";
        var superAdmin = await userManager.FindByEmailAsync(superAdminEmail);

        if (superAdmin == null)
        {
            superAdmin = new ApplicationUser
            {
                UserName = superAdminEmail,
                Email = superAdminEmail,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                FirstName = "Super",
                LastName = "Admin",
                CreatedDate = DateTime.UtcNow,
                IsActive = true
            };

            var result = await userManager.CreateAsync(superAdmin, "Admin@123456");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(superAdmin, AppRoles.SuperAdmin);
            }
        }
    }

    private static async Task SeedCategoriesAsync(ApplicationDbContext context)
    {
        var categories = new List<Category>
        {
            new Category
            {
                Id = Guid.NewGuid(),
                Name = "CPU - Bộ vi xử lý",
                Description = "Bộ vi xử lý cho máy tính",
                CreatedDate = DateTime.UtcNow
            },
            new Category
            {
                Id = Guid.NewGuid(),
                Name = "Mainboard",
                Description = "Bo mạch chủ",
                CreatedDate = DateTime.UtcNow
            },
            new Category
            {
                Id = Guid.NewGuid(),
                Name = "RAM",
                Description = "Bộ nhớ trong",
                CreatedDate = DateTime.UtcNow
            },
            new Category
            {
                Id = Guid.NewGuid(),
                Name = "VGA - Card đồ họa",
                Description = "Card màn hình",
                CreatedDate = DateTime.UtcNow
            },
            new Category
            {
                Id = Guid.NewGuid(),
                Name = "Ổ cứng",
                Description = "SSD, HDD",
                CreatedDate = DateTime.UtcNow
            },
            new Category
            {
                Id = Guid.NewGuid(),
                Name = "Case - Vỏ máy tính",
                Description = "Thùng máy tính",
                CreatedDate = DateTime.UtcNow
            }
        };

        await context.Categories.AddRangeAsync(categories);
    }

    private static async Task SeedProductsAsync(ApplicationDbContext context)
    {
        var cpuCategory = await context.Categories.FirstAsync(c => c.Name.Contains("CPU"));

        var products = new List<Product>
        {
            new Product
            {
                Id = Guid.NewGuid(),
                CategoryId = cpuCategory.Id,
                Name = "Intel Core i5-12400F",
                Description = "6 nhân 12 luồng, tốc độ tối đa 4.4GHz",
                Price = 3690000,
                Stock = 50,
                SKU = "CPU-I5-12400F",
                Brand = "Intel",
                Model = "Core i5-12400F",
                ImageUrls = "[]",
                Specifications = "{\"cores\":\"6\",\"threads\":\"12\",\"baseClock\":\"2.5GHz\",\"boostClock\":\"4.4GHz\"}",
                ProductType = ProductType.CPU,
                IsActive = true,
                CreatedDate = DateTime.UtcNow
            },
            new Product
            {
                Id = Guid.NewGuid(),
                CategoryId = cpuCategory.Id,
                Name = "AMD Ryzen 5 5600X",
                Description = "6 nhân 12 luồng, tốc độ tối đa 4.6GHz",
                Price = 4290000,
                Stock = 30,
                SKU = "CPU-R5-5600X",
                Brand = "AMD",
                Model = "Ryzen 5 5600X",
                ImageUrls = "[]",
                Specifications = "{\"cores\":\"6\",\"threads\":\"12\",\"baseClock\":\"3.7GHz\",\"boostClock\":\"4.6GHz\"}",
                ProductType = ProductType.CPU,
                IsActive = true,
                CreatedDate = DateTime.UtcNow
            }
        };

        await context.Products.AddRangeAsync(products);
    }

    private static async Task SeedServicesAsync(ApplicationDbContext context)
    {
        var services = new List<Service>
        {
            new Service
            {
                Id = Guid.NewGuid(),
                ServiceType = ServiceType.Repair,
                Name = "Sửa chữa máy tính",
                Description = "Dịch vụ sửa chữa máy tính tại nhà",
                BasePrice = 200000,
                EstimatedDurationMinutes = 120,
                IsActive = true,
                CreatedDate = DateTime.UtcNow
            },
            new Service
            {
                Id = Guid.NewGuid(),
                ServiceType = ServiceType.BuildPC,
                Name = "Lắp ráp máy tính",
                Description = "Tư vấn và lắp ráp máy tính theo yêu cầu",
                BasePrice = 300000,
                EstimatedDurationMinutes = 180,
                IsActive = true,
                CreatedDate = DateTime.UtcNow
            },
            new Service
            {
                Id = Guid.NewGuid(),
                ServiceType = ServiceType.Maintenance,
                Name = "Bảo trì định kỳ",
                Description = "Vệ sinh, kiểm tra và tối ưu hóa máy tính",
                BasePrice = 150000,
                EstimatedDurationMinutes = 90,
                IsActive = true,
                CreatedDate = DateTime.UtcNow
            }
        };

        await context.Services.AddRangeAsync(services);
    }
}
