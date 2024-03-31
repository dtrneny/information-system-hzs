namespace InformationSystemHZS.Exceptions;

public class DuplicateIdentifierException: BaseException
{
    public override string Message => "Duplicate identifier found.";
}