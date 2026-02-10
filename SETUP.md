# ComputerCare Development Setup Guide

## Prerequisites

Before you begin, ensure you have the following installed:

- ✅ .NET 8.0 SDK or later
- ✅ PostgreSQL 14 or later
- ✅ Visual Studio 2022 / VS Code / Rider
- ✅ Git
- ✅ Docker Desktop (optional, for containerized development)

## Step-by-Step Setup

### 1. Clone the Repository

```bash
git clone https://github.com/DHCuong27/ComputerCare.git
cd ComputerCare
```

### 2. PostgreSQL Setup

#### Option A: Local PostgreSQL Installation

1. Install PostgreSQL from https://www.postgresql.org/download/
2. Create a new database:

```sql
CREATE DATABASE ComputerCareDb;
```

3. Create a user (optional):

```sql
CREATE USER computercare_user WITH PASSWORD 'your_password';
GRANT ALL PRIVILEGES ON DATABASE ComputerCareDb TO computercare_user;
```

#### Option B: Using Docker

```bash
docker run --name computercare-db \
  -e POSTGRES_DB=ComputerCareDb \
  -e POSTGRES_USER=postgres \
  -e POSTGRES_PASSWORD=Admin@123 \
  -p 5432:5432 \
  -d postgres:14-alpine
```

### 3. Configuration

#### Update API Configuration

Edit `src/ComputerCare.API/appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=ComputerCareDb;Username=postgres;Password=Admin@123"
  },
  "JwtSettings": {
    "SecretKey": "your-super-secret-key-min-32-characters-long",
    "Issuer": "ComputerCare",
    "Audience": "ComputerCareUsers",
    "ExpiryMinutes": 60
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

#### Update Web Configuration

Edit `src/ComputerCare.Web/appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=ComputerCareDb;Username=postgres;Password=Admin@123"
  },
  "ApiSettings": {
    "BaseUrl": "https://localhost:7001"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

### 4. Build the Solution

```bash
dotnet restore
dotnet build
```

### 5. Database Migration

Navigate to Infrastructure project and run migrations:

```bash
cd src/ComputerCare.Infrastructure

# Add initial migration (if not exists)
dotnet ef migrations add InitialCreate --startup-project ../ComputerCare.API

# Update database
dotnet ef database update --startup-project ../ComputerCare.API
```

### 6. Seed Initial Data

The database will be automatically seeded with initial data on first run, including:

- Admin user (admin@computercare.com / Admin@123)
- Sample categories
- Sample products
- Sample services

### 7. Run the Applications

#### Terminal 1 - Run API:

```bash
cd src/ComputerCare.API
dotnet run
```

API will be available at:
- HTTPS: https://localhost:7001
- HTTP: http://localhost:5001
- Swagger: https://localhost:7001/swagger

#### Terminal 2 - Run Web:

```bash
cd src/ComputerCare.Web
dotnet run
```

Web will be available at:
- HTTPS: https://localhost:7002
- HTTP: http://localhost:5002

### 8. Access the Application

1. **Web Interface**: Navigate to https://localhost:7002
2. **API Documentation**: Navigate to https://localhost:7001/swagger
3. **Login**: Use admin@computercare.com / Admin@123

## Development Tools

### Visual Studio 2022

1. Open `ComputerCare.sln`
2. Set multiple startup projects:
   - Right-click Solution → Properties → Multiple startup projects
   - Set `ComputerCare.API` and `ComputerCare.Web` to "Start"
3. Press F5 to run

### VS Code

1. Open the root folder
2. Install recommended extensions:
   - C# for Visual Studio Code
   - C# Extensions
   - NuGet Gallery
3. Use the integrated terminal to run projects

### Rider

1. Open `ComputerCare.sln`
2. Configure run configurations for both API and Web
3. Run both projects

## Troubleshooting

### Database Connection Issues

If you encounter database connection errors:

1. Verify PostgreSQL is running:
   ```bash
   # Linux/Mac
   sudo service postgresql status
   
   # Windows
   # Check Services or Task Manager
   ```

2. Test connection string:
   ```bash
   psql -h localhost -U postgres -d ComputerCareDb
   ```

3. Check firewall settings (Port 5432)

### Migration Issues

If migrations fail:

1. Delete existing migrations:
   ```bash
   cd src/ComputerCare.Infrastructure
   rm -rf Migrations
   ```

2. Drop and recreate database:
   ```sql
   DROP DATABASE ComputerCareDb;
   CREATE DATABASE ComputerCareDb;
   ```

3. Create new migration:
   ```bash
   dotnet ef migrations add InitialCreate --startup-project ../ComputerCare.API
   dotnet ef database update --startup-project ../ComputerCare.API
   ```

### Port Conflicts

If ports are already in use, update `launchSettings.json` in each project:

```json
{
  "profiles": {
    "https": {
      "applicationUrl": "https://localhost:NEW_PORT;http://localhost:NEW_PORT"
    }
  }
}
```

## Testing

### API Testing with Swagger

1. Navigate to https://localhost:7001/swagger
2. Test endpoints directly from the UI
3. Authorize using JWT token (if needed)

### API Testing with Postman

1. Import the API collection (if available)
2. Set base URL to https://localhost:7001
3. Configure authorization headers

## Next Steps

1. ✅ Explore the API documentation
2. ✅ Check out the Web interface
3. ✅ Review the code structure
4. ✅ Start developing new features
5. ✅ Run tests (when available)

## Useful Commands

```bash
# Build entire solution
dotnet build

# Run tests
dotnet test

# Clean build artifacts
dotnet clean

# Watch for changes and rebuild
dotnet watch run

# Create new migration
dotnet ef migrations add MigrationName --startup-project ../ComputerCare.API

# Remove last migration
dotnet ef migrations remove --startup-project ../ComputerCare.API

# Update database
dotnet ef database update --startup-project ../ComputerCare.API
```

## Support

For issues and questions:
- Create an issue on GitHub
- Contact: [your-email]
- Documentation: [link-to-docs]
