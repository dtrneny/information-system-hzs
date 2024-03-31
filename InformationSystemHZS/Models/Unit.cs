using InformationSystemHZS.Collections;
using InformationSystemHZS.Models.Interfaces;
using InformationSystemHZS.Utils.Enums;

namespace InformationSystemHZS.Models;

public class Unit(
    string callsign,
    string stationCallsign,
    UnitState state,
    Vehicle vehicleDto,
    CallsignEntityMap<Member> members
): IBaseModel
{
    public string Callsign { get; set; } = callsign;
    public string StationCallsign { get; set; } = stationCallsign;
    public UnitState State { get; set; } = state;
    public Vehicle Vehicle { get; set; } = vehicleDto;
    public CallsignEntityMap<Member> Members { get; set; } = members;
    
    public override string ToString()
    {
        return $"{StationCallsign} | {Callsign} | {Vehicle.Name} | {Members.GetEntitiesCount()}/{Vehicle.Capacity} | {State}";
    }
}