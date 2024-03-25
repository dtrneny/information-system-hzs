namespace InformationSystemHZS.Services;

// IMPORTANT NOTE: For this part of code use only LINQ.
// NOTE: You can change the signature of functions to take a single parameter, i.e. something like List<Station>.

public static class StatisticsService
{
    /// <summary>
    /// Returns the total number of all 'FIRE_ENGINE' across all stations.
    /// </summary>
    public static int GetTotalFireEnginesCount()
    {
        // TODO: Implement
        throw new NotImplementedException();
    }

    /// <summary>
    /// Returns the name of the station closest to the hospital at coordinates (45, 60).
    /// </summary>
    public static string GetClosestToHospital()
    {
        // TODO: Implement
        throw new NotImplementedException();
    }
    
    /// <summary>
    /// Returns the callsign of the unit with the fastest vehicle. If no decision can be made, an error is printed.
    /// </summary>
    public static string GetFastestVehicleUnit()
    {
        // TODO: Implement
        throw new NotImplementedException();
    }

    /// <summary>
    /// Returns the callsign of the station that has the most firefighters under it.
    /// </summary>
    public static string GetStationWithMostPersonel()
    {
        // TODO: Implement
        throw new NotImplementedException();
    }

    /// <summary>
    /// Returns a list of all vehicle names sorted by fuel consumption (first with the lowest, last with the highest).
    /// Duplicate names must not appear in the list.
    /// </summary>
    public static List<string> GetVehiclesByFuelConsumption()
    {
        // TODO: Implement
        throw new NotImplementedException();
    }

    /// <summary>
    /// Returns the callsign of the unit that has historically resolved the highest number of events.
    /// </summary>
    public static string GetMostBusyUnit()
    {
        // TODO: Implement
        throw new NotImplementedException();
    }

    /// <summary>
    /// Returns the callsign of the unit that has consumed the most fuel with its vehicle in the sum of all its historical events.
    /// </summary>
    public static string MostFuelConsumedUnit()
    {
        // TODO: Implement
        throw new NotImplementedException();
    }
}