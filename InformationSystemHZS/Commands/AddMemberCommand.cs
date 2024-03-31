using InformationSystemHZS.Exceptions;
using InformationSystemHZS.Models;
using InformationSystemHZS.Utils.Enums;

namespace InformationSystemHZS.Commands;

public class AddMemberCommand(List<string> arguments): ICommand
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
        var memberName = Arguments[2];
        
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

        if (unit.Members.GetEntitiesCount() + 1 > unit.Vehicle.Capacity)
        {
            context.OutputWriter.PrintVehicleCapacityExceededMessage();
            return;
        }

        var newMember = new Member("", unitCallsign, memberName, MemberRank.PRIVATE);
        unit.Members.SafelyAddEntity(newMember, null);
        
        context.OutputWriter.PrintSuccessfulMemberAddition(newMember);
    }
}