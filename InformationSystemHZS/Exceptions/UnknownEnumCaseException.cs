namespace InformationSystemHZS.Exceptions;

public class UnknownEnumCaseException(string enumCase): BaseException
{
    public override string Message => $"Invalid enum case: {enumCase}.";
}