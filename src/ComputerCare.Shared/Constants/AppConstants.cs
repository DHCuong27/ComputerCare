namespace ComputerCare.Shared.Constants;

public static class AppConstants
{
    public const string DefaultCurrency = "VND";
    public const int DefaultPageSize = 20;
    public const int MaxPageSize = 100;
    public const int MinPasswordLength = 6;
    public const int MaxPasswordLength = 100;
    
    public static class Roles
    {
        public const string Admin = "Admin";
        public const string Manager = "Manager";
        public const string Employee = "Employee";
        public const string Customer = "Customer";
    }
    
    public static class CacheKeys
    {
        public const string FeaturedProducts = "FeaturedProducts";
        public const string Categories = "Categories";
        public const string ActiveServices = "ActiveServices";
    }
    
    public static class ValidationMessages
    {
        public const string RequiredField = "{0} is required";
        public const string InvalidEmail = "Invalid email format";
        public const string InvalidPhone = "Invalid phone number format";
        public const string MinLength = "{0} must be at least {1} characters";
        public const string MaxLength = "{0} must not exceed {1} characters";
    }
}
