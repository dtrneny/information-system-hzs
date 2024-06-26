using InformationSystemHZS.Classes;
using InformationSystemHZS.Collections;
using InformationSystemHZS.Services;
using InformationSystemHZS.Utils.Enums;

namespace InformationSystemHZS.Models;

public class ScenarioObject(
    CallsignEntityMap<Station> stations,
    List<RecordedIncident> incidentsHistory
)
{
    public CallsignEntityMap<Station> Stations { get; set; } = stations;
    public List<RecordedIncident> IncidentsHistory { get; set; } = incidentsHistory;

    public void Update()
    {
        var allUnits = Stations
            .GetAllEntities()
            .SelectMany(station => station.Units.GetAllEntities())
            .ToList();

        foreach (var unit in allUnits)
        {
            unit.UpdateUnitState();
        }
    }

    public Unit? GetSuitableUnitForIncident(IncidentCharacteristics characteristics, Position location)
    {
        var unitsWithDistance = GetUnitsWithDistance(location);
        var availableUnits = unitsWithDistance
            .Where(item => 
                item.unit.State == UnitState.AVAILABLE && 
                item.unit.Vehicle.Characteristics.TargetedIncidents.Contains(characteristics.Type)
            )
            .ToList();

        if (availableUnits.Count == 0) { return null; }

        var minDistance = availableUnits.Min(item => item.distance);

        var closestUnits = availableUnits
            .Where(item => item.distance.Equals(minDistance))
            .Select(item => item.unit)
            .ToList();

        if (closestUnits.Count == 1) { return closestUnits.First(); }

        var maxSpeed = closestUnits.Max(unit => unit.Vehicle.Speed);

        var suitableUnits = closestUnits
            .Where(unit => unit.Vehicle.Speed.Equals(maxSpeed))
            .ToList();
        
        if (suitableUnits.Count == 1) { return suitableUnits.First(); }

        var lowestStationCallsign = suitableUnits
            .Select(unit => unit.StationCallsign)
            .OrderBy(callsing => callsing)
            .First();

        var unitsWithLowestCallsign = suitableUnits
            .Where(unit => unit.StationCallsign.Equals(lowestStationCallsign))
            .ToList();
        
        if (unitsWithLowestCallsign.Count == 1) { return unitsWithLowestCallsign.First(); }

        return unitsWithLowestCallsign
            .OrderBy(unit => unit.Callsign)
            .First();
    }

    private List<(Unit unit, double distance)> GetUnitsWithDistance(Position position)
    {
        List<(Unit unit, double distance)> unitsWithDistance = [];
        var units = Stations
            .GetAllEntities()
            .SelectMany(station => station.Units.GetAllEntities())
            .ToList();

        foreach (var unit in units)
        {
            var station = Stations.GetEntity(unit.StationCallsign);

            if (station == null) continue;
            
            var distance = DistanceService.CalculateDistance(
                station.Position,
                position
            );

            unitsWithDistance.Add((unit, distance));
        }

        return unitsWithDistance;
    }
}