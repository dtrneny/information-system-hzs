using InformationSystemHZS.Models;

namespace InformationSystemHZS.Commands;

public class ListIncidentsCommand: ICommand
{
    public List<string> Arguments { get; } = [];
    public void Execute(SystemContext context)
    {
        var incidents = context.ScenarioObject.IncidentsHistory
            .Where(incident => !incident.Resolved) 
            .OrderBy(incident => incident.IncidentStartTIme)
            .ToList();
        context.OutputWriter.PrintIncidentList(incidents);
    }
}