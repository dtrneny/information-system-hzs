using InformationSystemHZS.Classes;
using InformationSystemHZS.Collections;
using InformationSystemHZS.Models.Interfaces;
using InformationSystemHZS.Services;
using InformationSystemHZS.Utils.Enums;

namespace InformationSystemHZS.Models;

public class Unit: IBaseModel
{
    public string Callsign { get; set; }
    public string StationCallsign { get; set; }
    public UnitState State { get; set; }
    public Vehicle Vehicle { get; set; }
    public CallsignEntityMap<Member> Members { get; set; }
    public ActiveIncidentCharacteristics? ActiveIncidentChar { get; set; }

    public Unit(
        string callsign,
        string stationCallsign,
        UnitState state,
        Vehicle vehicle,
        CallsignEntityMap<Member> members
    )
    {
        Callsign = callsign;
        StationCallsign = stationCallsign;
        State = state;
        Vehicle = vehicle;
        Members = members;
    }
    
    public override string ToString()
    {
        return $"{StationCallsign} | {Callsign} | {Vehicle.Name} | {Members.GetEntitiesCount()}/{Vehicle.Capacity} | {State}{(ActiveIncidentChar != null ? $" | {ActiveIncidentChar.IncidentDuration.Minutes:00}:{ActiveIncidentChar.IncidentDuration.Seconds:00}" : "")}";
    }

    public void UpdateUnitState()
    {
        if (ActiveIncidentChar == null) { return; }
        
        var duration = DateTimeService.GetTimeSpanGetBetweenTimestamps(
            ActiveIncidentChar.Incident.IncidentStartTIme,
            DateTimeService.GetCurrentTimestamp()
        );
        
        if (!duration.HasValue) { return; }

        var newState = GetUnitStateBasedOnDuration(ActiveIncidentChar, duration.Value);

        if (!newState.HasValue) { return; }
        
        State = newState.Value;

        if (newState.Value != UnitState.AVAILABLE)
        {
            ActiveIncidentChar.IncidentDuration = duration.Value;
            return;            
        }

        ActiveIncidentChar.Incident.Resolved = true;
        ActiveIncidentChar = null;
    }

    private UnitState? GetUnitStateBasedOnDuration(ActiveIncidentCharacteristics characteristics, TimeSpan duration)
    {
        var startTime = DateTimeService.ParseDateTimeFromString(characteristics.Incident.IncidentStartTIme);

        if (!startTime.HasValue) { return null; }

        var arrivalTime = (startTime.Value - startTime.Value.AddSeconds(characteristics.RouteTime)).Duration();
        var resolvedTime = arrivalTime.Add(TimeSpan.FromSeconds(characteristics.Incident.Characteristics.SolutionTime));
        var returnedTime = resolvedTime.Add(TimeSpan.FromSeconds(characteristics.RouteTime));

        if (duration < arrivalTime)
        {
            return UnitState.EN_ROUTE;
        }

        if (duration < resolvedTime)
        {
            return UnitState.ON_SITE;
        }

        if (duration < returnedTime)
        {
            return UnitState.RETURNING;
        }

        return UnitState.AVAILABLE;
    }

}