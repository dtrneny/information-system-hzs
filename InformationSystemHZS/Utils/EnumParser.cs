using InformationSystemHZS.Utils.Enums;

namespace InformationSystemHZS.Utils;

public static class EnumParser
{
    public static T? ParseEnumValueFromString<T>(string value) where T : struct
    {
        return Enum.TryParse<T>(value, out var output) ? output : null;
    }
}