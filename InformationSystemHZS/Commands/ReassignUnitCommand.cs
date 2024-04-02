using InformationSystemHZS.Classes;
using InformationSystemHZS.Models;

namespace InformationSystemHZS.Commands;

public class ReassignUnitCommand(List<string> arguments): ICommand
{
    public List<string> Arguments { get; } = arguments;
    public void Execute(SystemContext context)
    {
        if (Arguments.Count != 3)
        {
            context.OutputWriter.PrintInvalidArgumentsMessage();
            return;
        }

        var stationCallsign = Arguments[0];
        var unitCallsign = Arguments[1];
        var newStationCallsign = Arguments[2];
        
        var station = context.ScenarioObject.Stations.GetEntity(stationCallsign);

        if (station == null)
        {
            context.OutputWriter.PrintObjectWithCallsignNotFound("station", stationCallsign);
            return;
        }

        var unit = station.Units.GetEntity(unitCallsign);

        if (unit == null)
        {
            context.OutputWriter.PrintObjectWithCallsignNotFound("unit", unitCallsign);
            return;
        }
        
        station.Units.SafelyRemoveEntity(unit.Callsign);
        var newStation = context.ScenarioObject.Stations.GetEntity(newStationCallsign);

        if (newStation == null)
        {
            context.OutputWriter.PrintObjectWithCallsignNotFound("station", newStationCallsign);
            return;
        }
        
        unit.StationCallsign = newStationCallsign;
        newStation.Units.SafelyAddEntity(unit, null);
        
        context.OutputWriter.PrintSuccessfulUnitStationReassign(unit);
    }
}