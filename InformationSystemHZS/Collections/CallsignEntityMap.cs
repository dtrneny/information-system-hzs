using System.Text.RegularExpressions;

namespace InformationSystemHZS.Collections;

/// <summary>
/// Stores and manages data that maps valid callsigns to entities of a given type. 
/// </summary>
/// <typeparam name="T">IBaseModel</typeparam>
public partial class CallsignEntityMap<T>
{
    // TODO: Implement data storage
    private readonly Dictionary<string, T> _data = new ();
    private char callsignLetter { get; }
    private Regex CallsignRegex { get; }

    // TODO: Maybe a constructor might come in handy, ey?
    public CallsignEntityMap(char letter)
    {
        callsignLetter = char.ToUpper(letter);
        CallsignRegex = new Regex("^" + char.ToUpper(letter) + @"([0-9][1-9]|[1-9][0-9])$");
    }

    /// <summary>
    /// Returns an entity based on the given callsign.
    /// If the entity does not exist then returns default (see: https://learn.microsoft.com/cs-cz/dotnet/csharp/language-reference/operators/default).
    /// </summary>
    public T? GetEntity(string callsign)
    {
        // TODO: Implement
        // throw new NotImplementedException();
        return _data[callsign] ?? default(T);
    }

    /// <summary>
    /// Returns all mambers of the map.
    /// </summary>
    public List<T> GetAllEntities()
    {
        // TODO: Implement
        // throw new NotImplementedException();
        return _data.Values.ToList();
    }
    
    /// <summary>
    /// Returns the total number of entities in the map.
    /// </summary>
    public int GetEntitiesCount()
    {
        // TODO: Implement
        // throw new NotImplementedException();
        return _data.Values.Count;
    }
    
    /// <summary>
    /// Tries to safely add an entity. If callsign already exists within this map or is not in a valid format (i.e. S01, H01, J01, ...), returns false.
    /// Otherwise adds an entity to this map and returns true.
    /// If no callsign is provided, it generates a new one by incrementing the current highest callsign by 1 (i.e. generates S04, if highest available is S03).
    /// </summary>
    public bool SafelyAddEntity(T entity, string? callsign)
    {
        // TODO: Implement
        // throw new NotImplementedException();
        if (callsign != null && (_data.ContainsKey(callsign) || !ValidateCallsign(callsign))) { return false; }
        
        var highestCallsign = GetHighestCallsign();
        if (highestCallsign == null)
        {
            _data.TryAdd(callsignLetter + "01", entity);
            return true;
        }
        
        var highestCallsignNumber = GetCallsignNumber(highestCallsign);

        if (highestCallsignNumber == null) { return false; }
        
        var callsignNumber = highestCallsignNumber + 1 > 9
            ? $"{ highestCallsign + 1 }"
            : $"0{ highestCallsign + 1 }";
        
        _data.TryAdd(callsignLetter + callsignNumber, entity);
        return true;
    }

    /// <summary>
    /// Tries to safely remove an entity from this map. If it does not exist in this map, return false.
    /// If it exists, remove it from this map and return true.
    /// </summary>
    public bool SafelyRemoveEntity(string callsign)
    {
        // TODO: Implement
        // throw new NotImplementedException();
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

    public string? GetHighestCallsign()
    {
        var existingCallsigns = _data.Keys.ToList();
        return existingCallsigns.OrderByDescending(GetCallsignNumber).First();
    }

    public bool ValidateCallsign(string? callsign)
    {
        if (callsign == null) { return false; }
        
        var isMatch = CallsignRegex.IsMatch(callsign);
        
        return isMatch;
    }

    public int? GetCallsignNumber(string callsign)
    {
        if (!ValidateCallsign(callsign)) { return null; }
        
        var numericPart = callsign[^2..];
        if (!int.TryParse(numericPart, out var num)) { return null; }

        return num;
    }
}