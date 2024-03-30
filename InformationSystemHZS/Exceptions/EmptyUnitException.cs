namespace InformationSystemHZS.Exceptions;

public class EmptyUnitException(bool terminating): BaseException
{
    public override string Message => "Fire unit cannot be empty.";
    public override bool Terminating => terminating;
}