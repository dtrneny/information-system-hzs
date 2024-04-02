namespace InformationSystemHZS.Classes;

public class CommandLogEventArguments(string commandName, List<string> commandArguments): EventArgs
{
    public string CommandName { get; } = commandName;
    public List<string> CommandArguments { get; } = commandArguments;
}