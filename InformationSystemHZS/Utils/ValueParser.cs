using InformationSystemHZS.Utils.Enums;

namespace InformationSystemHZS.Utils;

public static class ValueParser
{
    public static T? ParseEnumValueFromString<T>(string value) where T : struct
    {
        return Enum.TryParse<T>(value, out var output) ? output : null;
    }
    
    public static int? ParseIntValueFromString(string value)
    {
        return int.TryParse(value, out var output) ? output : null;
    }
}