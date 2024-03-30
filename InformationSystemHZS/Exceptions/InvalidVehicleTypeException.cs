namespace InformationSystemHZS.Exceptions;

public class InvalidVehicleTypeException(bool terminating, string type): BaseException
{
    public override string Message => $"Vehicle type '{type}' is invalid.";
    public override bool Terminating => terminating;
}