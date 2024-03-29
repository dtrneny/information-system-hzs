using InformationSystemHZS.Models.Interfaces;
using InformationSystemHZS.Utils.Enums;

namespace InformationSystemHZS.Models;

public class Unit(
    string callsign,
    string stationCallsign,
    UnitState state,
    Vehicle vehicleDto,
    List<Member> members
): IBaseModel
{
    public string Callsign { get; set; } = callsign;
    public string StationCallsign { get; set; } = stationCallsign;
    public UnitState State { get; set; } = state;
    public Vehicle VehicleDto { get; set; } = vehicleDto;
    public List<Member> Members { get; set; } = members;
}