namespace InformationSystemHZS.Exceptions;

public class EmptyUnitException: BaseException
{
    public override string Message => "[capacity]: Unit cannot be empty.";
}