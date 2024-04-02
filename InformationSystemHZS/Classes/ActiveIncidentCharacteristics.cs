using InformationSystemHZS.Models;

namespace InformationSystemHZS.Classes;

public class ActiveIncidentCharacteristics(RecordedIncident incident, double routeTime)
{
    public RecordedIncident Incident { get; set; } = incident;
    public double RouteTime { get; } = routeTime;
    public TimeSpan IncidentDuration { get; set; } = TimeSpan.Zero;
}