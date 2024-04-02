using InformationSystemHZS.Exceptions;
using InformationSystemHZS.IO;
using InformationSystemHZS.IO.Helpers.Interfaces;
using InformationSystemHZS.Models;
using InformationSystemHZS.Models.HelperModels;
using InformationSystemHZS.Services;
using InformationSystemHZS.Utils;
using Timer = System.Timers.Timer;

namespace InformationSystemHZS;

public class Runner
{
    private static Timer? _updateTimer;
    private static ScenarioObject? Scenario { get; set; }
    
    
    public static Task Main(IConsoleManager consoleManager, string entryFileName = "Brnoslava.json")
    {
        // ---- DO NOT TOUCH ----
        var commandParser = new CommandParser(consoleManager);
        var outputWriter = new OutputWriter(consoleManager);
        StartUpdateFunction();
        // ^^^^^ DO NOT TOUCH ^^^^^

        // Load initial data from JSON
        // TODO: Catch exception here in case the loading failed, write an error message containing "import" and return "Task.CompletedTask" 
        // TODO: After we obtain valid data from ScenarioLoader, it is necessary to instantiate your own objects here.
        // TODO: We also need to check that given data is in a valid form (unique IDs, valid no. of firefighters, valid vehicle type).

        try
        {
            var data = ScenarioLoader.GetInitialScenarioData(entryFileName);

            if (data == null)
            {
                throw new NullScenarioDataException();
            }
            
            Scenario = DtoMapper.MapScenarionObjectDtoToScenarioObject(data);
            
            if (Scenario == null)
            {
                throw new NullScenarioObjectException();
            }
            
            ScenarioValidation.ValidateScenarioObject(Scenario);
        }
        catch (BaseException e)
        {
            outputWriter.PrintBaseExceptionMessage(e.Message);
            return Task.CompletedTask;
        }
        catch (Exception e)
        {
            outputWriter.PrintImportErrorMessage(e.Message);
            return Task.CompletedTask;
        }
        
        var context = new SystemContext(outputWriter, Scenario);
        commandParser.CommandGiven += Logger.OnInputGiven;
        
        while (true)
        {
            var command = commandParser.GetCommand();
            command?.Execute(context);
        }
    }

    /// <summary>
    /// Starts the update function to update function on program start.
    /// DO NOT CHANGE THIS METHOD.
    /// </summary>
    private static void StartUpdateFunction()
    {
        // Set up a timer to call the Update function every second
        _updateTimer = new Timer(TimeSpan.FromSeconds(1));
        _updateTimer.Elapsed += (sender, e) => UpdateFunction();
        _updateTimer.Start();
    }

    /// <summary>
    /// Is called every second to update the state of the system and its objects.
    /// </summary>
    private static void UpdateFunction()
    {
        Scenario?.Update();
    }
}