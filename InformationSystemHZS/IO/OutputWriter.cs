using InformationSystemHZS.IO.Helpers.Interfaces;
using InformationSystemHZS.Models;

namespace InformationSystemHZS.IO;

public class OutputWriter
{
    private IConsoleManager _consoleManager;

    public OutputWriter(IConsoleManager consoleManager)
    {
        _consoleManager = consoleManager;
    }

    public void PrintStationList(List<Station> stations)
    {
        foreach (var station in stations)
        {
            _consoleManager.WriteLine(station.ToString());
        }
    }
    
    public void PrintUnitList(List<Unit> units)
    {
        foreach (var unit in units)
        {
            _consoleManager.WriteLine(unit.ToString());
        }
    }
    
    public void PrintIncidentList(List<RecordedIncident> incidents)
    {
        foreach (var incident in incidents)
        {
            _consoleManager.WriteLine(incident.ToString());
        }
    }

    public void PrintSuccessfulMemberAddition(Member member)
    {
        _consoleManager.WriteLine($"[processed]: {member.Name} was added to unit {member.UnitCallsign}.");
    }

    public void PrintSuccessfulMemberRemoval(Member member)
    {
        _consoleManager.WriteLine($"[processed]: {member.Name} was removed from unit {member.UnitCallsign}.");
    }
    
    public void PrintSuccessfulMemberUnitReassign(Member member)
    {
        _consoleManager.WriteLine($"[processed]: {member.Name} was reassigned under the new callsign {member.Callsign}.");
    }
    
    public void PrintSuccessfulUnitStationReassign(Unit unit)
    {
        _consoleManager.WriteLine($"[processed]: Unit {unit.Callsign} was reassigned to the new station with callsign {unit.StationCallsign}.");
    }

    public void PrintInvalidArgumentsMessage()
    {
        _consoleManager.WriteLine("[invalid]: Invalid arguments.");
    }

    public void PrintObjectWithCallsignNotFound(string objectType, string callsign)
    {
        _consoleManager.WriteLine($"[invalid]: {char.ToUpper(objectType[0])}{objectType[1..]} with callsign '{callsign}' could not be found.");
    }

    public void PrintVehicleCapacityExceededMessage()
    {
        _consoleManager.WriteLine("[capacity]: Vehicle capacity was exceeded.");
    }
    
    public void PrintEmptyUnitMessage()
    {
        _consoleManager.WriteLine("[capacity]: Unit cannot be empty.");
    }

    public void PrintImportErrorMessage(string errorMessage)
    {
        _consoleManager.WriteLine($"[import]: {errorMessage}");
    }
    
    public void PrintCoordinatesMessage()
    {
        _consoleManager.WriteLine("[invalid]: Coordinates are out of bounds.");
    }
    
    public void PrintInvalidIncidentTypeMessage()
    {
        _consoleManager.WriteLine("[invalid]: Invalid incident type.");
    }
    
    public void PrintFailureIncidentAddition()
    {
        _consoleManager.WriteLine("[capacity]: All units are currently busy. Try again later.");
    }
    
    public void PrintSuccessfulIncidentAddition()
    {
        // TODO: [processed]: Unit J02 from station S02 was assigned to the incident.
        _consoleManager.WriteLine("[processed]: new incident.");
    }

    public void PrintBaseExceptionMessage(string message)
    {
        _consoleManager.WriteLine(message);
    }
}