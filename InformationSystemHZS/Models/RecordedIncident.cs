using InformationSystemHZS.Classes;

namespace InformationSystemHZS.Models;

public class RecordedIncident(
    IncidentCharacteristics characteristics,
    Position location,
    string description,
    string incidentStartTIme,
    string assignedStation,
    string assignedUnit,
    bool resolved = false
)
{
    public IncidentCharacteristics Characteristics { get; set; } = characteristics;
    public Position Location { get; set; } = location;
    public string Description { get; set; } = description;
    public string IncidentStartTIme { get; set; } = incidentStartTIme;
    public string AssignedStation { get; set; } = assignedStation;
    public string AssignedUnit { get; set; } = assignedUnit;
    public bool Resolved { get; set; } = resolved;
    
    public override string ToString()
    {
        return $"{Characteristics.Type} | ({Location.X}, {Location.Y}) | {IncidentStartTIme} | {Description} | {AssignedStation} | {AssignedUnit}";
    }
}