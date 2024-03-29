using InformationSystemHZS.Models.Interfaces;

namespace InformationSystemHZS.Models;

public class Station(
    string callsign,
    Position positionDto,
    string name,
    List<Unit> units
): IBaseModel
{
    public string Callsign { get; set; } = callsign;
    public Position PositionDto { get; set; } = positionDto;
    public string Name { get; set; } = name;
    public List<Unit> Units { get; set; } = units;
}