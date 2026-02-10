namespace ComputerCare.Shared.Constants;

public static class ErrorMessages
{
    public const string NotFound = "{0} with id {1} was not found";
    public const string AlreadyExists = "{0} already exists";
    public const string InvalidOperation = "Invalid operation: {0}";
    public const string UnauthorizedAccess = "Unauthorized access";
    public const string InsufficientStock = "Insufficient stock for product {0}";
    public const string InvalidCredentials = "Invalid email or password";
    public const string EmailAlreadyExists = "Email already exists";
    public const string InvalidPromoCode = "Invalid or expired promotion code";
    public const string OrderNotEditable = "Order cannot be modified in current status";
}
