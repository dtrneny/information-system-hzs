using InformationSystemHZS.Collections;

namespace InformationSystemHZS.Models;

public class ScenarioObject(
    CallsignEntityMap<Station> stations,
    List<RecordedIncident> incidentsHistory
)
{
    public CallsignEntityMap<Station> Stations { get; set; } = stations;
    public List<RecordedIncident> IncidentsHistory { get; set; } = incidentsHistory;
}