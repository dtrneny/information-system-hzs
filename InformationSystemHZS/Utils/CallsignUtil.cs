using System.Text.RegularExpressions;

namespace InformationSystemHZS.Utils;

public partial class CallsignUtil
{
    
    [GeneratedRegex("^[A-Z]([1-9][0-9]|[0-9][1-9])$")]
    private static partial Regex CallsignRegex();
    
    public static bool ValidateScopedCallsign(char callsignLetter, string? callsign)
    {
        if (!ValidateGeneralCallsign(callsign)) { return false; }
        
        return callsign != null && callsign.StartsWith(callsignLetter);
    }

    public static bool ValidateGeneralCallsign(string? callsign)
    {
        if (callsign == null) { return false; }
        
        var isMatch = CallsignRegex().IsMatch(callsign);
        
        return isMatch;
    }

    public static int? GetCallsignNumber(string callsign)
    {
        if (!ValidateGeneralCallsign(callsign)) { return null; }
        
        var numericPart = callsign[^2..];
        if (!int.TryParse(numericPart, out var num)) { return null; }

        return num;
    }

    

    // TODO: maybe later implement if needed
    // public static void GetOrderedCallsignList(List<string> callsigns)
    // {
    //     
    // }
}