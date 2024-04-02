using InformationSystemHZS.Classes;
using InformationSystemHZS.Collections;
using InformationSystemHZS.Exceptions;
using InformationSystemHZS.Models;
using InformationSystemHZS.Models.HelperModels;
using InformationSystemHZS.Utils.Enums;

namespace InformationSystemHZS.Utils;

public static class DtoMapper
{
    public static Member MapMemberDtoToMember(MemberDto dto)
    {
        
        var memberRank = ValueParser.ParseEnumValueFromString<MemberRank>(dto.Rank);

        if (memberRank == null) { throw new NullEnumMappingException(dto.Rank); }
        
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
        var incidentType = ValueParser.ParseEnumValueFromString<IncidentType>(dto.Type);

        if (incidentType == null) { throw new NullEnumMappingException(dto.Type); }
        
        var incidentCharacteristics = new IncidentCharacteristics((IncidentType) incidentType);
        
        return new RecordedIncident(
            incidentCharacteristics,
            MapPositionDtoToPosition(dto.Location),
            dto.Description, 
            dto.IncidentStartTIme,
            dto.AssignedStation,
            dto.AssignedUnit,
            true
        );
    }
    
    public static Vehicle MapVehicleDtoToVehicle(VehicleDto dto)
    {
        var vehicleType = ValueParser.ParseEnumValueFromString<VehicleType>(dto.Type);

        if (vehicleType == null) { throw new NullEnumMappingException(dto.Type); }
        
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
        var members = new CallsignEntityMap<Member>('H');

        foreach (var member in dto.Members.Select(MapMemberDtoToMember))
        {
            members.SafelyAddEntity(member, member.Callsign);
        }
        
        return new Unit(
            dto.Callsign,
            dto.StationCallsign,
            UnitState.AVAILABLE,
            MapVehicleDtoToVehicle(dto.VehicleDto),
            members
        );
    }
    
    public static Station MapStationDtoToStation(StationDto dto)
    {
        var units = new CallsignEntityMap<Unit>('J');

        foreach (var unit in dto.Units.Select(MapUnitDtoToUnit))
        {
            units.SafelyAddEntity(unit, unit.Callsign);
        }
        
        return new Station(
            dto.Callsign,
            MapPositionDtoToPosition(dto.PositionDto),
            dto.Name,
            units
        );
    } 
    
    public static ScenarioObject MapScenarionObjectDtoToScenarioObject(ScenarioObjectDto dto)
    {
        var recordedIncidents = dto.IncidentsHistory.Select(MapRecordedIncidentDtoToRecordedIncident).ToList();
        var stations = new CallsignEntityMap<Station>('S');

        foreach (var station in dto.Stations.Select(MapStationDtoToStation))
        {
            stations.SafelyAddEntity(station, station.Callsign);
        }

        return new ScenarioObject(stations, recordedIncidents);
    } 
}