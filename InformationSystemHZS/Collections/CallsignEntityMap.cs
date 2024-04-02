using System.Text.RegularExpressions;
using InformationSystemHZS.Models.Interfaces;

namespace InformationSystemHZS.Collections;

/// <summary>
/// Stores and manages data that maps valid callsigns to entities of a given type. 
/// </summary>
/// <typeparam name="T">IBaseModel</typeparam>
public partial class CallsignEntityMap<T> where T : IBaseModel
{
    private readonly Dictionary<string, T> _data = new ();
    private char CallsignLetter { get; }
    private Regex CallsignRegex { get; }

    public CallsignEntityMap(char letter)
    {
        CallsignLetter = char.ToUpper(letter);
        CallsignRegex = new Regex("^" + char.ToUpper(letter) + @"([0-9][1-9]|[1-9][0-9])$");
    }

    /// <summary>
    /// Returns an entity based on the given callsign.
    /// If the entity does not exist then returns default (see: https://learn.microsoft.com/cs-cz/dotnet/csharp/language-reference/operators/default).
    /// </summary>
    public T? GetEntity(string callsign)
    {
        return _data.TryGetValue(callsign, out var value) ? value : default(T);
    }

    /// <summary>
    /// Returns all mambers of the map.
    /// </summary>
    public List<T> GetAllEntities()
    {
        return _data.Values.ToList();
    }
    
    /// <summary>
    /// Returns the total number of entities in the map.
    /// </summary>
    public int GetEntitiesCount()
    {
        return _data.Values.Count;
    }
    
    /// <summary>
    /// Tries to safely add an entity. If callsign already exists within this map or is not in a valid format (i.e. S01, H01, J01, ...), returns false.
    /// Otherwise adds an entity to this map and returns true.
    /// If no callsign is provided, it generates a new one by incrementing the current highest callsign by 1 (i.e. generates S04, if highest available is S03).
    /// </summary>
    public bool SafelyAddEntity(T entity, string? callsign)
    {
        if (callsign != null && (_data.ContainsKey(callsign) || !ValidateCallsign(callsign))) { return false; }
        
        var highestCallsign = GetHighestCallsign();
        if (highestCallsign == null)
        {
            _data.TryAdd(CallsignLetter + "01", entity);
            return true;
        }
        
        var highestCallsignNumber = GetCallsignNumber(highestCallsign);

        if (highestCallsignNumber == null) { return false; }
        
        var callsignNumber = highestCallsignNumber + 1 > 9
            ? $"{ highestCallsignNumber + 1 }"
            : $"0{ highestCallsignNumber + 1 }";

        entity.Callsign = CallsignLetter + callsignNumber;
        _data.TryAdd(entity.Callsign, entity);
        return true;
    }

    /// <summary>
    /// Tries to safely remove an entity from this map. If it does not exist in this map, return false.
    /// If it exists, remove it from this map and return true.
    /// </summary>
    public bool SafelyRemoveEntity(string callsign)
    {
        return _data.Remove(callsign);
    }
    
    /// <summary>
    /// Function which returns list of callsigns from map.
    /// </summary>
    /// <returns>List of callsigns</returns>
    public List<string> GetAllCallsigns()
    {
        return [.._data.Keys];
    }

    private string? GetHighestCallsign()
    {
        var existingCallsigns = _data.Keys.ToList();
        return existingCallsigns.Count != 0 ? existingCallsigns.OrderByDescending(GetCallsignNumber).First() : null;
    }

    private bool ValidateCallsign(string? callsign)
    {
        return callsign != null && CallsignRegex.IsMatch(callsign);
    }

    private int? GetCallsignNumber(string callsign)
    {
        if (!ValidateCallsign(callsign)) { return null; }
        
        var numericPart = callsign[^2..];
        if (!int.TryParse(numericPart, out var num)) { return null; }

        return num;
    }
}