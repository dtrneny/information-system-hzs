namespace InformationSystemHZS.Exceptions;

public class VehicleCapacityException(bool terminating): BaseException
{
    public override string Message => "Vehicle capacity was exceeded.";
    public override bool Terminating => terminating;
}