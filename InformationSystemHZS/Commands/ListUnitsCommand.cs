using InformationSystemHZS.Classes;
using InformationSystemHZS.Models;

namespace InformationSystemHZS.Commands;

public class ListUnitsCommand: ICommand
{
    public List<string> Arguments { get; } = [];
    public void Execute(SystemContext context)
    {
        var units = context.ScenarioObject.Stations.GetAllEntities()
            .SelectMany(station => station.Units.GetAllEntities())
            .OrderBy(unit => unit.StationCallsign)
            .ThenBy(unit => unit.Callsign)
            .ToList();
        
        context.OutputWriter.PrintUnitList(units);
    }
}