using InformationSystemHZS.Collections;
using InformationSystemHZS.Exceptions;
using InformationSystemHZS.Models;

namespace InformationSystemHZS.Utils;

public static class ScenarioValidation
{
    private static bool ValidateDuplicateIdentifiers(List<string> identifiers)
    {
        return !identifiers
            .GroupBy(x => x)
            .Any(g => g.Count() > 1);
    }

    private static bool ValidateVehicleCapacity(int capacity, int membersCount)
    {
        return capacity >= membersCount;
    }

    private static bool ValidateUnitMembersCount(int membersCount)
    {
        return membersCount > 0;
    }

    private static bool ValidatePositionInBounds(Position position)
    {
        var xInRange = position.X is >= 0 and <= 99;
        var yInRange = position.Y is >= 0 and <= 99;
        
        return xInRange && yInRange;
    }

    private static void ValidateUnit(Unit unit)
    {
        if (!ValidateDuplicateIdentifiers(unit.Members.GetAllCallsigns()))
        {
            throw new DuplicateIdentifierException(true);
        }

        if (!ValidateUnitMembersCount(unit.Members.GetEntitiesCount()))
        {
            throw new EmptyUnitException(true);
        }
        
        if (!ValidateVehicleCapacity(unit.Vehicle.Capacity, unit.Members.GetEntitiesCount()))
        {
            throw new VehicleCapacityException(true);
        }
    }

    private static void ValidateStation(Station station)
    {
        if (!ValidatePositionInBounds(station.Position))
        {
            throw new PositionOutOfBoundsException(true, station.Position);
        }
        
        if (!ValidateDuplicateIdentifiers(station.Units.GetAllCallsigns()))
        {
            throw new DuplicateIdentifierException(true);
        }
        
        foreach (var unit in station.Units.GetAllEntities())
        {
            ValidateUnit(unit);
        }
    }

    private static void ValidateStations(CallsignEntityMap<Station> stations)
    {
        if (!ValidateDuplicateIdentifiers(stations.GetAllCallsigns()))
        {
            throw new DuplicateIdentifierException(true);
        }

        foreach (var station in stations.GetAllEntities())
        {
            ValidateStation(station);
        }
    }

    private static void ValidateIncidents(List<RecordedIncident> incidents)
    {
        foreach (var incident in incidents)
        {
            if (!ValidatePositionInBounds(incident.Location))
            {
                throw new PositionOutOfBoundsException(true, incident.Location);
            }
        }
    }

    public static void ValidateScenarioObject(ScenarioObject scenarioObject)
    {
        ValidateStations(scenarioObject.Stations);
        ValidateIncidents(scenarioObject.IncidentsHistory);
    }
}