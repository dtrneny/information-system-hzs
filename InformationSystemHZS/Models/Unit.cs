using InformationSystemHZS.Collections;
using InformationSystemHZS.Models.Interfaces;
using InformationSystemHZS.Services;
using InformationSystemHZS.Utils.Enums;

namespace InformationSystemHZS.Models;

public class Unit(
    string callsign,
    string stationCallsign,
    UnitState state,
    Vehicle vehicleDto,
    CallsignEntityMap<Member> members
): IBaseModel
{
    public string Callsign { get; set; } = callsign;
    public string StationCallsign { get; set; } = stationCallsign;
    public UnitState State { get; set; } = state;
    public Vehicle Vehicle { get; set; } = vehicleDto;
    public CallsignEntityMap<Member> Members { get; set; } = members;
    public ActiveIncidentCharacteristics? ActiveIncident { get; set; }
    
    public override string ToString()
    {
        return $"{StationCallsign} | {Callsign} | {Vehicle.Name} | {Members.GetEntitiesCount()}/{Vehicle.Capacity} | {State}{(ActiveIncident != null ? $" | {ActiveIncident.IncidentDuration.Minutes:00}:{ActiveIncident.IncidentDuration.Seconds:00}" : "")}";
    }

    public void UpdateUnitState()
    {
        if (ActiveIncident == null) { return; }
        
        var duration = TimestampService.GetDifferenceBetweenTwoTimestamps(
            ActiveIncident.StartTime,
            TimestampService.GetCurrentTimestamp()
        );
        
        if (!duration.HasValue) { return; }

        var newState = GetUnitStateBasedOnDuration(ActiveIncident, duration.Value);

        if (!newState.HasValue) { return; }
        
        State = newState.Value;

        if (newState.Value != UnitState.AVAILABLE)
        {
            ActiveIncident.IncidentDuration = duration.Value;
            return;            
        }

        ActiveIncident = null;
    }

    public UnitState? GetUnitStateBasedOnDuration(ActiveIncidentCharacteristics characteristics, TimeSpan duration)
    {
        var startTime = TimestampService.ParseTimestampFromString(characteristics.StartTime);

        if (!startTime.HasValue) { return null; }

        var arrivalTime = (startTime.Value - startTime.Value.AddSeconds(characteristics.RouteTime)).Duration();
        var resolvedTime = arrivalTime.Add(TimeSpan.FromSeconds(characteristics.SolutionTime));
        var returnedTime = resolvedTime.Add(TimeSpan.FromSeconds(characteristics.RouteTime));

        if (duration < arrivalTime)
        {
            return UnitState.EN_ROUTE;
        }
        else if (duration < resolvedTime)
        {
            return UnitState.ON_SITE;
        }
        else if (duration < returnedTime)
        {
            return UnitState.RETURNING;
        }
        else
        {
            return UnitState.AVAILABLE;
        }
    }
}