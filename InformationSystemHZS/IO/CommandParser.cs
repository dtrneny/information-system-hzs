using System.Text.RegularExpressions;
using InformationSystemHZS.Commands;
using InformationSystemHZS.IO.Helpers.Interfaces;

namespace InformationSystemHZS.IO;

public partial class CommandParser
{
    private IConsoleManager _consoleManager;
    
    [GeneratedRegex("""("[^"]*"|\S+)""")]
    private static partial Regex ArgumentRegex();
    
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
        
        var tokens = ParseArguments(input);
        var commandName = tokens.FirstOrDefault();
        var commandArguments = tokens.Skip(1).ToList();

        if (commandName == null)
        {
            _consoleManager.WriteLine("[unknown]: Invalid or unknown command.");
            return null;
        }
        
        var command = CommandFactory.GetCommandByName(commandName, commandArguments);

        if (command != null) return command;
        
        _consoleManager.WriteLine("[unknown]: Invalid or unknown command.");
        return null;
    }
    
    private static List<string> ParseArguments(string input)
    {
        var matches = ArgumentRegex().Matches(input);
        return matches.Select(match => match.Value.Trim('"')).ToList();
    }
}