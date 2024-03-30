using InformationSystemHZS.Exceptions;
using InformationSystemHZS.Utils.Enums;

namespace InformationSystemHZS.Classes;

public class IncidentCharacteristics(IncidentType type)
{
    public IncidentType Type { get; } = type;
    public int SolutionTime { get; } = type switch
    {
        IncidentType.FIRE => 10,
        IncidentType.ACCIDENT => 6,
        IncidentType.DISASTER => 8,
        IncidentType.HAZARD => 10,
        IncidentType.TECHNICAL => 4,
        IncidentType.RESCUE => 6,
        _ => throw new UnknownEnumCaseException(true, type.ToString())
    };
}