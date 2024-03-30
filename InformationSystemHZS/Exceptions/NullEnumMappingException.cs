namespace InformationSystemHZS.Exceptions;

public class NullEnumMappingException(bool terminating, string value): BaseException
{
    public override string Message => $"Enum value is null after parsing string value '{value}'.";
    public override bool Terminating => terminating;
}