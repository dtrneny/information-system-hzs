namespace InformationSystemHZS.Models;

public class ActiveIncidentCharacteristics(string startTime, int solutionTime, double routeTime)
{
    public string StartTime { get; } = startTime;
    public int SolutionTime { get; } = solutionTime;
    public double RouteTime { get; } = routeTime;
    public TimeSpan IncidentDuration { get; set; } = TimeSpan.Zero;
}