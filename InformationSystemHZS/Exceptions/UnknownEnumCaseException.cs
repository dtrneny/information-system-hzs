namespace InformationSystemHZS.Exceptions;

public class UnknownEnumCaseException(bool terminating, string enumCase): BaseException
{
    public override string Message => $"Invalid enum case: {enumCase}.";
    public override bool Terminating => terminating;
}