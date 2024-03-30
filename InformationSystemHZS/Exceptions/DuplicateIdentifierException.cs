namespace InformationSystemHZS.Exceptions;

public class DuplicateIdentifierException(bool terminating): BaseException
{
    public override string Message => "Duplicate identifier found.";
    public override bool Terminating => terminating;
}