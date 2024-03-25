namespace InformationSystemHZS.Collections;

/// <summary>
/// Stores and manages data that maps valid callsigns to entities of a given type. 
/// </summary>
/// <typeparam name="T">IBaseModel</typeparam>
public class CallsignEntityMap<T>
{
    // TODO: Implement data storage
    
    // TODO: Maybe a constructor might come in handy, ey?

    /// <summary>
    /// Returns an entity based on the given callsign.
    /// If the entity does not exist then returns default (see: https://learn.microsoft.com/cs-cz/dotnet/csharp/language-reference/operators/default).
    /// </summary>
    public T? GetEntity(string callsign)
    {
        // TODO: Implement
        throw new NotImplementedException();
    }

    /// <summary>
    /// Returns all mambers of the map.
    /// </summary>
    public List<T> GetAllEntities()
    {
        // TODO: Implement
        throw new NotImplementedException();
    }
    
    /// <summary>
    /// Returns the total number of entities in the map.
    /// </summary>
    public int GetEntitiesCount()
    {
        // TODO: Implement
        throw new NotImplementedException();
    }
    
    /// <summary>
    /// Tries to safely add an entity. If callsign already exists within this map or is not in a valid format (i.e. S01, H01, J01, ...), returns false.
    /// Otherwise adds an entity to this map and returns true.
    /// If no callsign is provided, it generates a new one by incrementing the current highest callsign by 1 (i.e. generates S04, if highest available is S03).
    /// </summary>
    public bool SafelyAddEntity(T entity, string? callsign)
    {
        // TODO: Implement
        throw new NotImplementedException();
    }

    /// <summary>
    /// Tries to safely remove an entity from this map. If it does not exist in this map, return false.
    /// If it exists, remove it from this map and return true.
    /// </summary>
    public bool SafelyRemoveEntity(string callsign)
    {
        // TODO: Implement
        throw new NotImplementedException();
    }
}