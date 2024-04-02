using InformationSystemHZS.Classes;

namespace InformationSystemHZS.Services;

public static class Logger
{
    
    private static readonly string RootProjectPath = Path.GetFullPath(@"..\..\..\");

    public static void OnInputGiven(object? sender, CommandLogEventArguments e)
    {
        var filePath = Path.Combine(RootProjectPath, "Logs/commandInputLog.txt");
        
        try
        {
            using var writer = new StreamWriter(filePath, true);
            writer.WriteLine(FormatLog(e));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing to log file: {ex.Message}");
        }
    }

    private static string FormatLog(CommandLogEventArguments e)
    {
        return e.CommandArguments.Count > 0 
            ?  $"{e.CommandName}: {string.Join(", ", e.CommandArguments)}"
            : $"{e.CommandName}";
    }
}