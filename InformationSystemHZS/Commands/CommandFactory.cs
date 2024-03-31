
namespace InformationSystemHZS.Commands;

public static class CommandFactory
{
    private static readonly Dictionary<string, Func<List<string>, ICommand>> Commands =
        new()
        {
            { "list-stations", _ => new ListStationsCommand() },
            { "list-units", _ => new ListUnitsCommand() },
            { "list-incidents", _ => new ListIncidentsCommand() },
            { "add-member", arguments => new AddMemberCommand(arguments) },
            { "remove-member", arguments => new RemoveMemberCommand(arguments) },
            { "reassign-member", arguments => new ReassignMemberCommand(arguments) },
            { "reassign-unit", arguments => new ReassignUnitCommand(arguments) },
            { "report", arguments => new ReportCommand(arguments) },
        };
    
    public static ICommand? GetCommandByName(string commandName, List<string> arguments)
    {
        return Commands.TryGetValue(commandName, out var command)
            ? command.Invoke(arguments)
            : null;
    }
}