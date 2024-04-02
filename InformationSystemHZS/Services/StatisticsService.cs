using InformationSystemHZS.Models;
using InformationSystemHZS.Utils.Enums;

namespace InformationSystemHZS.Services;

// IMPORTANT NOTE: For this part of code use only LINQ.
// NOTE: You can change the signature of functions to take a single parameter, i.e. something like List<Station>.

public static class StatisticsService
{
    /// <summary>
    /// Returns the total number of all 'FIRE_ENGINE' across all stations.
    /// </summary>
    public static int GetTotalFireEnginesCount(List<Station> stations)
    {
        return stations
            .SelectMany(station => station.Units.GetAllEntities())
            .Count(unit => unit.Vehicle.Characteristics.Type == VehicleType.FIRE_ENGINE);
    }

    /// <summary>
    /// Returns the name of the station closest to the hospital at coordinates (45, 60).
    /// </summary>
    public static string GetClosestToHospital(List<Station> stations)
    {
        if (stations.Count == 0)
        {
            return "none [error]";
        }

        return stations
            .Select(station => (station.Name, DistanceService.CalculateDistance(
                station.Position.X,
                station.Position.Y,
                45,
                60
            )))
            .MinBy(item => item.Item2)
            .Name;
    }

    /// <summary>
    /// Returns the callsign of the unit with the fastest vehicle. If no decision can be made, an error is printed.
    /// </summary>
    public static string GetFastestVehicleUnit(List<Unit> units)
    {
        var unitCallsignWithFastestCar = units
            .MaxBy(item => item.Vehicle.Speed);

        return unitCallsignWithFastestCar != null
            ? unitCallsignWithFastestCar.Callsign
            : "none [error]";
    }

    /// <summary>
    /// Returns the callsign of the station that has the most firefighters under it.
    /// </summary>
    public static string GetStationWithMostPersonel(List<Station> stations)
    {
        var stationWithMostMembers = stations
            .OrderByDescending(station => station.Units
                .GetAllEntities()
                .Sum(unit => unit.Members.GetEntitiesCount())
            )
            .ToList();

        return stationWithMostMembers.Count > 0
            ? stationWithMostMembers.First().Callsign
            : "none [error]";
    }

    /// <summary>
    /// Returns a list of all vehicle names sorted by fuel consumption (first with the lowest, last with the highest).
    /// Duplicate names must not appear in the list.
    /// </summary>
    public static List<string> GetVehiclesByFuelConsumption(List<Unit> units)
    {
        return units
            .Select(unit => unit.Vehicle)
            .OrderBy(vehicle => vehicle.FuelConsumption)
            .Select(vehicle => vehicle.Name)
            .Distinct()
            .ToList();
    }

    /// <summary>
    /// Returns the callsign of the unit that has historically resolved the highest number of events.
    /// </summary>
    public static string GetMostBusyUnit(List<RecordedIncident> incidents)
    {
        var busiestUnit = incidents
            .GroupBy(incident => incident.AssignedUnit)
            .MaxBy(group => group.Count())?
            .Key;

        return busiestUnit ?? "none [error]";
    }

    /// <summary>
    /// Returns the callsign of the unit that has consumed the most fuel with its vehicle in the sum of all its historical events.
    /// </summary>
    public static string MostFuelConsumedUnit(List<(Unit Unit, Position UnitPosition, RecordedIncident Incident)> positionedUnitsWithIncidentsList)
    {
        var mostFuelConsumedUnit = positionedUnitsWithIncidentsList
            .Select(tuple => new
            {
                tuple.Unit.Callsign,
                tuple.Unit.StationCallsign,
                FuelConsumed = DistanceService.CalculateFuelConsumed(
                    2 * DistanceService.CalculateDistance(
                        tuple.UnitPosition,
                        tuple.Incident.Location
                    ),
                    tuple.Unit.Vehicle.FuelConsumption
                )
            })
            .GroupBy(entry => (entry.Callsign, entry.StationCallsign))
            .Select(group => new
            {
                Callsigns = group.Key,
                TotalFuelConsumed = group.Sum(entry => entry.FuelConsumed)
            })
            .ToList()
            .MaxBy(x => x.TotalFuelConsumed);
        

        return mostFuelConsumedUnit != null
            ? mostFuelConsumedUnit.Callsigns.Callsign
            : "none [error]";
    }
}
    