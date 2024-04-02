using InformationSystemHZS.Classes;
using InformationSystemHZS.Models;

namespace InformationSystemHZS.Commands;

public class ListStationsCommand: ICommand
{
    public List<string> Arguments { get; } = [];
    public void Execute(SystemContext context)
    {
        var orderedStations = context.ScenarioObject.Stations.GetAllEntities()
            .OrderBy(station => station.Callsign)
            .ToList();
        
        context.OutputWriter.PrintStationList(orderedStations);
    }
}