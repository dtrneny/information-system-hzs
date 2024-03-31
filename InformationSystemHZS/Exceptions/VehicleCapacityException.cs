namespace InformationSystemHZS.Exceptions;

public class VehicleCapacityException: BaseException
{
    public override string Message => "[capacity]: Vehicle capacity was exceeded.";
}