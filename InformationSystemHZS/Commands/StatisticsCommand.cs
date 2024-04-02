using InformationSystemHZS.Classes;
using InformationSystemHZS.Models;
using InformationSystemHZS.Services;

namespace InformationSystemHZS.Commands;

public class StatisticsCommand: ICommand
{
    public List<string> Arguments { get; } = [];
    public void Execute(SystemContext context)
    {
        var allStations = context.ScenarioObject.Stations.GetAllEntities();
        var allUnits = allStations
            .SelectMany(station => station.Units.GetAllEntities())
            .ToList();

        var historicalIncidents = context.ScenarioObject.IncidentsHistory
            .Where(incident => incident.Resolved)
            .ToList();
        
        var fireEngineCount = StatisticsService.GetTotalFireEnginesCount(allStations);
        context.OutputWriter.PrintStatisticsWaterTanksCount(fireEngineCount);
        
        var closestStationName = StatisticsService.GetClosestToHospital(allStations);
        context.OutputWriter.PrintStatisticsClosestStationToHospital(closestStationName);
        
        var fastestVehicleUnit = StatisticsService.GetFastestVehicleUnit(allUnits);
        context.OutputWriter.PrintStatisticsFastestUnit(fastestVehicleUnit);

        var stationWithMostMembers = StatisticsService.GetStationWithMostPersonel(allStations);
        context.OutputWriter.PrintStatisticsStationWithMostMembers(stationWithMostMembers);
        
        var vehicleNamesByConsumption = StatisticsService.GetVehiclesByFuelConsumption(allUnits);
        context.OutputWriter.PrintStatisticsVehiclesByConsumption(vehicleNamesByConsumption);

        var busiestUnit = StatisticsService.GetMostBusyUnit(historicalIncidents);
        context.OutputWriter.PrintStatisticsBusiestUnit(busiestUnit);
        
        
        var positionedUnits = allUnits
            .Join(allStations,
                unit => unit.StationCallsign,
                station => station.Callsign,
                (unit, station) => new { Unit = unit, Position = station.Position }
            )
            .ToList();

        var incidentsWithPositionedUnits = positionedUnits
            .Join(historicalIncidents,
                positionedUnit => new { UnitCallsign = positionedUnit.Unit.Callsign, positionedUnit.Unit.StationCallsign },
                incident => new { UnitCallsign = incident.AssignedUnit, StationCallsign = incident.AssignedStation },
                (positionedUnit, incident) => new
                {
                    positionedUnit.Unit,
                    UnitPosition = positionedUnit.Position,
                    Incident = incident
                }
            )
            .Select(item => (item.Unit, item.UnitPosition, item.Incident))
            .ToList();
        
        
        var mostFuelConsumingUnit = StatisticsService.MostFuelConsumedUnit(incidentsWithPositionedUnits);
        context.OutputWriter.PrintStatisticsMostFuelConsumed(mostFuelConsumingUnit);
    }
}