using InformationSystemHZS.Models;

namespace InformationSystemHZS.Commands;

public class ListUnitsCommand: ICommand
{
    public List<string> Arguments { get; } = [];
    public void Execute(SystemContext context)
    {
        List<Unit> units = [];
        units.AddRange(context.ScenarioObject.Stations.GetAllEntities().SelectMany(station => station.Units.GetAllEntitiesSorted()));

        context.OutputWriter.PrintUnitList(units);
    }
}