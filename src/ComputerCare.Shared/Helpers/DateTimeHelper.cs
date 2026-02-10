namespace ComputerCare.Shared.Helpers;

public static class DateTimeHelper
{
    public static DateTime GetVietnamTime()
    {
        var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
        return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamTimeZone);
    }
    
    public static string ToVietnameseDateString(this DateTime dateTime)
    {
        return dateTime.ToString("dd/MM/yyyy");
    }
    
    public static string ToVietnameseDateTimeString(this DateTime dateTime)
    {
        return dateTime.ToString("dd/MM/yyyy HH:mm:ss");
    }
}
