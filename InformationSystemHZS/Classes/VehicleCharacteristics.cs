using InformationSystemHZS.Exceptions;
using InformationSystemHZS.Utils.Enums;

namespace InformationSystemHZS.Classes;

public class VehicleCharacteristics(VehicleType type)
{
    public VehicleType Type { get; } = type;
    public List<IncidentType> TargetedIncidents { get; } = type switch
    {
        VehicleType.FIRE_ENGINE => [IncidentType.FIRE, IncidentType.ACCIDENT],
        VehicleType.TECHNICAL_VEHICLE => [IncidentType.DISASTER, IncidentType.TECHNICAL],
        VehicleType.ANTI_GAS_VEHICLE => [IncidentType.HAZARD],
        VehicleType.RESCUE_VEHICLE => [IncidentType.ACCIDENT, IncidentType.RESCUE],
        VehicleType.CRANE_TRUCK => [IncidentType.TECHNICAL, IncidentType.RESCUE],
        _ => throw new UnknownEnumCaseException(true, type.ToString())
    };
}