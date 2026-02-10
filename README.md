# ComputerCare - Hệ thống quản lý cửa hàng máy tính

## 📝 Giới thiệu

ComputerCare là một hệ thống website cửa hàng máy tính hoàn chỉnh được xây dựng với ASP.NET Core 8.0, theo kiến trúc phân tầng (Layered Architecture) chuẩn, bao gồm:

- 🛒 **Bán hàng trực tuyến**: Quản lý sản phẩm, giỏ hàng, đặt hàng
- 🔧 **Dịch vụ sửa chữa**: Tiếp nhận và quản lý yêu cầu sửa chữa
- 🖥️ **Lắp ráp máy tính**: Tư vấn và lắp ráp theo yêu cầu
- 📊 **Quản trị hệ thống**: Dashboard quản lý toàn diện
- 💬 **Real-time**: SignalR cho chat, thông báo và tracking

## 🏗️ Kiến trúc hệ thống

### Layered Architecture

```
ComputerCare/
├── src/
│   ├── ComputerCare.Domain/          # Domain Layer
│   │   ├── Entities/                 # Domain entities
│   │   ├── Enums/                    # Enumerations
│   │   ├── ValueObjects/             # Value objects
│   │   └── Interfaces/               # Repository interfaces
│   │
│   ├── ComputerCare.Application/     # Application Layer
│   │   ├── Services/                 # Business logic services
│   │   ├── DTOs/                     # Data Transfer Objects
│   │   ├── Mappings/                 # AutoMapper profiles
│   │   └── Validators/               # FluentValidation validators
│   │
│   ├── ComputerCare.Infrastructure/  # Infrastructure Layer
│   │   ├── Data/                     # DbContext, migrations
│   │   ├── Repositories/             # Repository implementations
│   │   ├── Identity/                 # ASP.NET Identity
│   │   └── Configurations/           # EF Core configurations
│   │
│   ├── ComputerCare.API/             # API Layer (WebAPI)
│   │   ├── Controllers/              # API Controllers
│   │   └── Middleware/               # Custom middleware
│   │
│   ├── ComputerCare.Web/             # Presentation Layer (MVC)
│   │   ├── Controllers/              # MVC Controllers
│   │   ├── Views/                    # Razor views
│   │   └── wwwroot/                  # Static files
│   │
│   ├── ComputerCare.WebSocket/       # WebSocket Layer
│   │   ├── Hubs/                     # SignalR hubs
│   │   └── Services/                 # Real-time services
│   │
│   └── ComputerCare.Shared/          # Shared Layer
│       ├── Constants/                # Application constants
│       ├── Exceptions/               # Custom exceptions
│       └── Helpers/                  # Helper classes
```

## 🛠️ Công nghệ sử dụng

### Backend
- **ASP.NET Core 8.0**: Framework chính
- **Entity Framework Core 8.0**: ORM
- **PostgreSQL**: Database
- **ASP.NET Core Identity**: Authentication & Authorization
- **SignalR**: Real-time communication
- **AutoMapper**: Object-to-object mapping
- **FluentValidation**: Input validation

### Frontend
- **Bootstrap 5**: UI Framework
- **jQuery**: JavaScript library
- **SignalR Client**: Real-time client

### Deployment
- **Docker**: Containerization
- **Google Cloud Platform**: Cloud hosting
  - Cloud Run / App Engine
  - Cloud SQL (PostgreSQL)
  - Cloud Storage

## 📋 Yêu cầu hệ thống

- .NET 8.0 SDK
- PostgreSQL 14+
- Node.js (for client-side packages)
- Docker (optional)

## 🚀 Cài đặt và chạy

### 1. Clone repository

```bash
git clone https://github.com/DHCuong27/ComputerCare.git
cd ComputerCare
```

### 2. Cấu hình Database

Cập nhật connection string trong `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=ComputerCareDb;Username=postgres;Password=yourpassword"
  }
}
```

### 3. Tạo Database và Migration

```bash
cd src/ComputerCare.Infrastructure
dotnet ef migrations add InitialCreate --startup-project ../ComputerCare.API
dotnet ef database update --startup-project ../ComputerCare.API
```

### 4. Chạy ứng dụng

#### Chạy API:
```bash
cd src/ComputerCare.API
dotnet run
```

#### Chạy Web MVC:
```bash
cd src/ComputerCare.Web
dotnet run
```

### 5. Truy cập ứng dụng

- **API**: https://localhost:7001
- **Web**: https://localhost:7002
- **Swagger**: https://localhost:7001/swagger

## 🔑 Tài khoản mặc định

**Admin Account:**
- Email: admin@computercare.com
- Password: Admin@123

## 🗃️ Database Schema

### Các bảng chính:

- **Products**: Sản phẩm
- **Categories**: Danh mục
- **Orders**: Đơn hàng
- **OrderItems**: Chi tiết đơn hàng
- **Customers**: Khách hàng
- **Employees**: Nhân viên
- **Services**: Dịch vụ
- **RepairRequests**: Yêu cầu sửa chữa
- **Carts**: Giỏ hàng
- **Reviews**: Đánh giá
- **Warranties**: Bảo hành

## 📡 API Endpoints

### Products
- `GET /api/products` - Lấy danh sách sản phẩm
- `GET /api/products/{id}` - Lấy chi tiết sản phẩm
- `POST /api/products` - Tạo sản phẩm mới (Admin)
- `PUT /api/products/{id}` - Cập nhật sản phẩm (Admin)
- `DELETE /api/products/{id}` - Xóa sản phẩm (Admin)

### Orders
- `GET /api/orders` - Lấy danh sách đơn hàng
- `GET /api/orders/{id}` - Lấy chi tiết đơn hàng
- `POST /api/orders` - Tạo đơn hàng mới
- `PUT /api/orders/{id}/status` - Cập nhật trạng thái (Admin)

### Cart
- `GET /api/cart` - Lấy giỏ hàng
- `POST /api/cart/items` - Thêm sản phẩm vào giỏ
- `PUT /api/cart/items/{id}` - Cập nhật số lượng
- `DELETE /api/cart/items/{id}` - Xóa khỏi giỏ

### Repairs
- `GET /api/repairs` - Lấy danh sách yêu cầu sửa chữa
- `POST /api/repairs` - Tạo yêu cầu sửa chữa
- `GET /api/repairs/{id}` - Lấy chi tiết
- `PUT /api/repairs/{id}/status` - Cập nhật trạng thái (Admin)

## 🎯 Tính năng chính

### Khách hàng
- ✅ Xem và tìm kiếm sản phẩm
- ✅ Quản lý giỏ hàng
- ✅ Đặt hàng và thanh toán
- ✅ Theo dõi đơn hàng
- ✅ Yêu cầu dịch vụ sửa chữa
- ✅ Đánh giá sản phẩm
- ✅ Chat support real-time

### Admin
- ✅ Quản lý sản phẩm (CRUD)
- ✅ Quản lý đơn hàng
- ✅ Quản lý dịch vụ sửa chữa
- ✅ Quản lý khách hàng
- ✅ Dashboard thống kê
- ✅ Quản lý tồn kho
- ✅ Báo cáo doanh thu

## 🔐 Bảo mật

- ✅ ASP.NET Core Identity
- ✅ JWT Authentication (API)
- ✅ Cookie Authentication (Web)
- ✅ Role-based Authorization
- ✅ Password hashing
- ✅ HTTPS enforcement
- ✅ CORS configuration
- ✅ Input validation
- ✅ XSS & CSRF protection

## 📊 Kiến trúc phân tầng

### Domain Layer
- Chứa business entities và domain logic
- Independent, không phụ thuộc vào layer khác
- Định nghĩa interfaces cho repositories

### Application Layer
- Business logic và use cases
- DTOs cho data transfer
- Service interfaces và implementations
- Validation rules

### Infrastructure Layer
- Data access với EF Core
- Repository implementations
- External services integration
- Database migrations

### API Layer
- RESTful API endpoints
- JWT authentication
- Swagger documentation
- Exception handling middleware

### Web Layer (MVC)
- User interface
- Cookie-based authentication
- Views với Razor
- Client-side scripts

### WebSocket Layer
- SignalR hubs
- Real-time notifications
- Chat functionality
- Order tracking

## 🐳 Docker

### Build và chạy với Docker:

```bash
docker-compose up -d
```

### Services:
- `computercare-web`: Web application
- `computercare-api`: API service
- `computercare-db`: PostgreSQL database

## ☁️ Deployment lên Google Cloud Platform

### Cloud SQL Setup:
```bash
gcloud sql instances create computercare-db \
    --database-version=POSTGRES_14 \
    --tier=db-f1-micro \
    --region=asia-southeast1
```

### Cloud Run Deployment:
```bash
gcloud run deploy computercare-api \
    --source . \
    --platform managed \
    --region asia-southeast1
```

## 📝 Code Quality Standards

- ✅ SOLID Principles
- ✅ Clean Code practices
- ✅ Repository & Unit of Work pattern
- ✅ Dependency Injection
- ✅ Async/await patterns
- ✅ Proper exception handling
- ✅ Code documentation
- ✅ Logging với Serilog

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## 📄 License

This project is licensed under the MIT License.

## 👥 Contact

- Author: DHCuong27
- Email: [your-email]
- GitHub: [https://github.com/DHCuong27](https://github.com/DHCuong27)

## 🙏 Acknowledgments

- ASP.NET Core Documentation
- Entity Framework Core Documentation
- Bootstrap
- AutoMapper
- FluentValidation
