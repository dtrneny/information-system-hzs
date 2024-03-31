namespace InformationSystemHZS.Exceptions;

public class InvalidVehicleTypeException(string type): BaseException
{
    public override string Message => $"Vehicle type '{type}' is invalid.";
}