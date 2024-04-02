namespace InformationSystemHZS.Services;

public static class TimestampService
{
    public static string GetCurrentTimestamp(string format = "d.M.yyyy HH:mm:ss")
    {
        var currentTime = DateTime.Now;
        return currentTime.ToString(format);
    }

    public static TimeSpan? GetDifferenceBetweenTwoTimestamps(string? firstTimestamp, string? secondTimestamp, string format = "d.M.yyyy HH:mm:ss")
    {
        if (firstTimestamp == null || secondTimestamp == null) { return null; }

        var firstTimestampTime = ParseTimestampFromString(firstTimestamp);
        var secondTimestampTime = ParseTimestampFromString(secondTimestamp);
        
        if (!firstTimestampTime.HasValue || !secondTimestampTime.HasValue) { return null; }

        return (secondTimestampTime.Value - firstTimestampTime.Value).Duration();
    }

    public static DateTime? ParseTimestampFromString(string timestamp, string format = "d.M.yyyy HH:mm:ss")
    {
        if (!DateTime.TryParseExact(timestamp, format, null, System.Globalization.DateTimeStyles.None,
                out var timestampDateTime))
        {
            return null;
        }

        return timestampDateTime;
    }
}