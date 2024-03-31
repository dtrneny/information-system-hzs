using InformationSystemHZS.Collections;
using InformationSystemHZS.Exceptions;
using InformationSystemHZS.Models;
using InformationSystemHZS.Utils.Enums;

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

    private static bool ValidateVehicleType(string vehicleType)
    {
        var type = ValueParser.ParseEnumValueFromString<VehicleType>(vehicleType);
        return type != null;
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
            throw new DuplicateIdentifierException();
        }

        if (!ValidateUnitMembersCount(unit.Members.GetEntitiesCount()))
        {
            throw new EmptyUnitException();
        }
        
        if (!ValidateVehicleCapacity(unit.Vehicle.Capacity, unit.Members.GetEntitiesCount()))
        {
            throw new VehicleCapacityException();
        }

        var vehicleType = unit.Vehicle.Characteristics.Type.ToString();
        if (!ValidateVehicleType(vehicleType))
        {
            throw new InvalidVehicleTypeException(vehicleType);
        }
    }

    private static void ValidateStation(Station station)
    {
        if (!ValidatePositionInBounds(station.Position))
        {
            throw new PositionOutOfBoundsException(station.Position);
        }
        
        if (!ValidateDuplicateIdentifiers(station.Units.GetAllCallsigns()))
        {
            throw new DuplicateIdentifierException();
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
            throw new DuplicateIdentifierException();
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
                throw new PositionOutOfBoundsException(incident.Location);
            }
        }
    }

    public static void ValidateScenarioObject(ScenarioObject scenarioObject)
    {
        ValidateStations(scenarioObject.Stations);
        ValidateIncidents(scenarioObject.IncidentsHistory);
    }
}