using System.Text.RegularExpressions;
using InformationSystemHZS.Commands;
using InformationSystemHZS.IO.Helpers.Interfaces;
using InformationSystemHZS.Utils;

namespace InformationSystemHZS.IO;

public partial class CommandParser
{
    private IConsoleManager _consoleManager;
    
    [GeneratedRegex("""("[^"]*"|\S+)""")]
    private static partial Regex ArgumentRegex();
    public event EventHandler<CommandLogEventArguments> CommandGiven;
    
    public CommandParser(IConsoleManager consoleManager)
    {
        _consoleManager = consoleManager;
    }

    public ICommand? GetCommand()
    {
        var input = _consoleManager.ReadLine();

        if (input == null)
        {
            _consoleManager.WriteLine("[unknown]: Invalid or unknown command.");
            return null;
        }
        
        var cliValues = ArgumentRegex()
            .Matches(input)
            .Select(match => match.Value.Trim('"'))
            .ToList();
        var commandName = cliValues.FirstOrDefault();
        var commandArguments = cliValues.Skip(1).ToList();

        if (commandName == null)
        {
            _consoleManager.WriteLine("[unknown]: Invalid or unknown command.");
            return null;
        }
        
        var command = CommandFactory.GetCommandByName(commandName, commandArguments);

        if (command != null)
        {
            CommandGiven.Invoke(this, new CommandLogEventArguments(commandName, commandArguments));
            return command;
        }
        
        _consoleManager.WriteLine("[unknown]: Invalid or unknown command.");
        return null;
    }
}