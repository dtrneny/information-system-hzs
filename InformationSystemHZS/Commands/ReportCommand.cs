using InformationSystemHZS.Classes;
using InformationSystemHZS.Models;
using InformationSystemHZS.Services;
using InformationSystemHZS.Utils;
using InformationSystemHZS.Utils.Enums;

namespace InformationSystemHZS.Commands;

public class ReportCommand(List<string> arguments): ICommand
{
    public List<string> Arguments { get; } = arguments;
    public void Execute(SystemContext context)
    {
        if (Arguments.Count != 4)
        {
            context.OutputWriter.PrintInvalidArgumentsMessage();
            return;
        }
        
        var xCoordinateString = Arguments[0];
        var yCoordinateString = Arguments[1];
        var incidentTypeString = Arguments[2];
        var description = Arguments[3];

        var xCoordinate = ValueParser.ParseIntValueFromString(xCoordinateString);
        var yCoordinate = ValueParser.ParseIntValueFromString(yCoordinateString);

        if (!xCoordinate.HasValue || !yCoordinate.HasValue)
        {
            context.OutputWriter.PrintInvalidArgumentsMessage();
            return;
        }
        
        var xInRange = xCoordinate.Value is >= 0 and <= 99;
        var yInRange = yCoordinate.Value is >= 0 and <= 99;

        if (!xInRange || !yInRange)
        {
            context.OutputWriter.PrintCoordinatesMessage();
            return;
        }
        
        var incidentType = ValueParser.ParseEnumValueFromString<IncidentType>(incidentTypeString);

        if (!incidentType.HasValue)
        {
            context.OutputWriter.PrintInvalidIncidentTypeMessage();
            return;
        }
        
        var location = new Position(xCoordinate.Value, yCoordinate.Value);
        var incidentCharacteristics = new IncidentCharacteristics(incidentType.Value);
        var currentTime = DateTime.Now;

        var suitableUnit = context.ScenarioObject.GetSuitableUnitForIncident(
            incidentCharacteristics,
            location
        );
        
        if (suitableUnit == null)
        {
            context.OutputWriter.PrintFailureIncidentAddition();
            return;
        }

        var station = context.ScenarioObject.Stations.GetEntity(suitableUnit.StationCallsign);
        
        if (station == null)
        {
            context.OutputWriter.PrintFailureIncidentAddition();
            return;
        }
        
        var newIncident = new RecordedIncident(
            incidentCharacteristics,
            location,
            description,
            currentTime.ToString("dd.MM.yyyy HH:mm:ss"),
            suitableUnit.StationCallsign,
            suitableUnit.Callsign,
            false
        );

        var routeTime = DistanceService.CalculateTimeTaken(
            DistanceService.CalculateDistance(station.Position, newIncident.Location),
            suitableUnit.Vehicle.Speed
        );

        suitableUnit.ActiveIncidentChar = new ActiveIncidentCharacteristics(
            newIncident,
            routeTime
        );
        suitableUnit.State = UnitState.EN_ROUTE;
        
        context.ScenarioObject.IncidentsHistory.Add(newIncident);
        context.OutputWriter.PrintSuccessfulIncidentAddition(newIncident);
    }
}