# ComputerCare - Security & Configuration Guide

## 🔐 Security Considerations

### Default SuperAdmin Account

**⚠️ CRITICAL: Change the default SuperAdmin password immediately after first deployment!**

**Default Credentials:**
- Email: `admin@computercare.com`
- Password: `Admin@123456`

### How to Change Default Password

#### Option 1: Via Configuration (Development Only)

Update `appsettings.json`:
```json
{
  "AdminSettings": {
    "Email": "admin@computercare.com",
    "DefaultPassword": "YourSecurePassword123!"
  }
}
```

#### Option 2: Via User Secrets (Recommended for Development)

```bash
cd src/ComputerCare.Web
dotnet user-secrets init
dotnet user-secrets set "AdminSettings:DefaultPassword" "YourSecurePassword123!"
```

#### Option 3: Via Environment Variables (Production)

Set environment variable:
```bash
# Linux/Mac
export AdminSettings__DefaultPassword="YourSecurePassword123!"

# Windows
set AdminSettings__DefaultPassword=YourSecurePassword123!
```

#### Option 4: Change After First Login

1. Login with default credentials
2. Go to Admin Panel
3. Navigate to Profile/Change Password
4. Set a strong new password

### Database Connection String

**Development:**
Use User Secrets to store connection strings:
```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Database=ComputerCare;Username=postgres;Password=YOUR_PASSWORD"
```

**Production:**
- Use Azure Key Vault
- Or Environment Variables
- Never commit connection strings to source control

### Password Requirements

The system enforces the following password requirements:
- Minimum length: 8 characters
- Must contain: uppercase letter, lowercase letter, digit, and special character
- Lockout after 5 failed attempts for 15 minutes

## 🎨 UI/UX Theme

### Color Palette

- **Primary Blue**: #0066CC
- **Primary Blue Dark**: #004C99
- **Primary Blue Light**: #3399FF
- **White**: #FFFFFF
- **Off-White**: #F8F9FA

## 👥 User Roles

1. **SuperAdmin**: Full system access, user management
2. **Admin**: Administrative access (except user management)
3. **Manager**: Manage products, orders, repairs, view reports
4. **Technician**: Manage repair requests
5. **Sales**: Manage orders
6. **Customer**: Basic customer access
7. **VipCustomer**: Enhanced customer features

## 🚀 First Time Setup

1. Clone the repository
2. Set up PostgreSQL database
3. Update connection string via User Secrets
4. Run migrations: `dotnet ef database update`
5. Run the application
6. Login with default SuperAdmin credentials
7. **IMMEDIATELY change the password**

## 📝 Notes

- The dual UserId/CustomerId pattern in entities (Order, Cart, etc.) is for backward compatibility
- Plan migration to remove legacy CustomerId fields
- All sensitive configuration should use User Secrets (dev) or Environment Variables/Key Vault (prod)
