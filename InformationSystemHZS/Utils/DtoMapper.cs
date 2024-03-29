using InformationSystemHZS.Classes;
using InformationSystemHZS.Models;
using InformationSystemHZS.Models.HelperModels;
using InformationSystemHZS.Utils.Enums;

namespace InformationSystemHZS.Utils;

public static class DtoMapper
{
    public static Member MapMemberDtoToMember(MemberDto dto)
    {
        
        var memberRank = EnumParser.ParseEnumValueFromString<MemberRank>(dto.Rank);

        if (memberRank == null)
        {
            // TODO: custom exception
            throw new Exception();
        }
        
        return new Member(
            dto.Callsign,
            dto.UnitCallsign,
            dto.Name,
            (MemberRank) memberRank
        );
    }
    
    public static Position MapPositionDtoToPosition(PositionDto dto)
    {
        return new Position(dto.X, dto.Y);
    } 
    
    public static RecordedIncident MapRecordedIncidentDtoToRecordedIncident(RecordedIncidentDto dto)
    {
        var incidentType = EnumParser.ParseEnumValueFromString<IncidentType>(dto.Type);

        if (incidentType == null)
        {
            // TODO: custom exception
            throw new Exception();
        }
        
        var incidentCharacteristics = new IncidentCharacteristics((IncidentType) incidentType);
        
        return new RecordedIncident(
            incidentCharacteristics,
            MapPositionDtoToPosition(dto.Location),
            dto.Description, 
            dto.IncidentStartTIme,
            dto.AssignedStation,
            dto.AssignedUnit
        );
    }
    
    public static Vehicle MapVehicleDtoToVehicle(VehicleDto dto)
    {
        var vehicleType = EnumParser.ParseEnumValueFromString<VehicleType>(dto.Type);

        if (vehicleType == null)
        {
            // TODO: custom exception
            throw new Exception();
        }
        
        var vehicleCharacteristics = new VehicleCharacteristics((VehicleType) vehicleType);
        
        return new Vehicle(
            dto.Name,
            vehicleCharacteristics,
            dto.FuelConsumption,
            dto.Speed,
            dto.Capacity
        );
    } 
    
    public static Unit MapUnitDtoToUnit(UnitDto dto)
    {
        var members = dto.Members.Select(MapMemberDtoToMember).ToList();
        
        var unitState = EnumParser.ParseEnumValueFromString<UnitState>(dto.State);

        if (unitState == null)
        {
            // TODO: custom exception
            throw new Exception();
        }
        
        return new Unit(
            dto.Callsign,
            dto.StationCallsign,
            (UnitState) unitState,
            MapVehicleDtoToVehicle(dto.VehicleDto),
            members
        );
    }
    
    public static Station MapStationDtoToStation(StationDto dto)
    {
        var units = dto.Units.Select(MapUnitDtoToUnit).ToList();
        
        return new Station(
            dto.Callsign,
            MapPositionDtoToPosition(dto.PositionDto),
            dto.Name,
            units
        );
    } 
    
    public static ScenarioObject MapScenarionObjectDtoToScenarioObject(ScenarioObjectDto dto)
    {
        var stations = dto.Stations.Select(MapStationDtoToStation).ToList();
        var recordedIncidents = dto.IncidentsHistory.Select(MapRecordedIncidentDtoToRecordedIncident).ToList();
        
        return new ScenarioObject(stations, recordedIncidents);
    } 
}