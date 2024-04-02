using InformationSystemHZS.Classes;
using InformationSystemHZS.Models;

namespace InformationSystemHZS.Commands;

public class ReassignMemberCommand(List<string> arguments): ICommand
{
    public List<string> Arguments { get; } = arguments;
    public void Execute(SystemContext context)
    {
        if (Arguments.Count != 5)
        {
            context.OutputWriter.PrintInvalidArgumentsMessage();
            return;
        }

        var stationCallsign = Arguments[0];
        var unitCallsign = Arguments[1];
        var memberCallsign = Arguments[2];
        var newStationCallsign = Arguments[3];
        var newUnitCallsign = Arguments[4];
        
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
        
        var member = unit.Members.GetEntity(memberCallsign);

        if (member == null)
        {
            context.OutputWriter.PrintObjectWithCallsignNotFound("member", memberCallsign);
            return;
        }

        if (unit.Members.GetEntitiesCount() - 1 == 0)
        {
            context.OutputWriter.PrintEmptyUnitMessage();
            return;
        }
        
        unit.Members.SafelyRemoveEntity(member.Callsign);
        var newStation = context.ScenarioObject.Stations.GetEntity(newStationCallsign);

        if (newStation == null)
        {
            context.OutputWriter.PrintObjectWithCallsignNotFound("station", newStationCallsign);
            return;
        }

        var newUnit = newStation.Units.GetEntity(newUnitCallsign);

        if (newUnit == null)
        {
            context.OutputWriter.PrintObjectWithCallsignNotFound("unit", newUnitCallsign);
            return;
        }

        if (newUnit.Members.GetEntitiesCount() + 1 > newUnit.Vehicle.Capacity)
        {
            context.OutputWriter.PrintVehicleCapacityExceededMessage();
            return;
        }
        
        member.UnitCallsign = newUnitCallsign;
        newUnit.Members.SafelyAddEntity(member, null);
        
        context.OutputWriter.PrintSuccessfulMemberUnitReassign(member);
    }
}