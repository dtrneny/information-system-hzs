using InformationSystemHZS.Collections;
using InformationSystemHZS.Models.Interfaces;

namespace InformationSystemHZS.Models;

public class Station(
    string callsign,
    Position positionDto,
    string name,
    CallsignEntityMap<Unit> units
): IBaseModel
{
    public string Callsign { get; set; } = callsign;
    public Position Position { get; set; } = positionDto;
    public string Name { get; set; } = name;
    public CallsignEntityMap<Unit> Units { get; set; } = units;
}