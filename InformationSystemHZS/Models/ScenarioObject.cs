namespace InformationSystemHZS.Models;

public class ScenarioObject(
    List<Station> stations,
    List<RecordedIncident> incidentsHistory
)
{
    public List<Station> Stations { get; set; } = stations;
    public List<RecordedIncident> IncidentsHistory { get; set; } = incidentsHistory;
}