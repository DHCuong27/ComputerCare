using System.Text;
using System.Globalization;

namespace ComputerCare.Shared.Helpers;

public static class StringHelper
{
    public static string ToSlug(this string str)
    {
        if (string.IsNullOrEmpty(str))
            return string.Empty;

        str = str.ToLowerInvariant();
        str = RemoveDiacritics(str);
        str = System.Text.RegularExpressions.Regex.Replace(str, @"[^a-z0-9\s-]", "");
        str = System.Text.RegularExpressions.Regex.Replace(str, @"\s+", " ").Trim();
        str = str.Replace(" ", "-");
        return str;
    }

    public static string RemoveDiacritics(string text)
    {
        var normalizedString = text.Normalize(NormalizationForm.FormD);
        var stringBuilder = new StringBuilder();

        foreach (var c in normalizedString)
        {
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
            {
                stringBuilder.Append(c);
            }
        }

        return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
    }

    public static string Truncate(this string str, int maxLength)
    {
        if (string.IsNullOrEmpty(str) || str.Length <= maxLength)
            return str;

        return str.Substring(0, maxLength) + "...";
    }
}
