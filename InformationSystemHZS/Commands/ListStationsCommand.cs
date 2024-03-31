using InformationSystemHZS.Models;

namespace InformationSystemHZS.Commands;

public class ListStationsCommand: ICommand
{
    public List<string> Arguments { get; } = [];
    public void Execute(SystemContext context)
    {
        var orderedStations = context.ScenarioObject.Stations.GetAllEntitiesSorted();
        context.OutputWriter.PrintStationList(orderedStations);
    }
}