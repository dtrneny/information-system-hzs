namespace InformationSystemHZS.Exceptions;

public class NullEnumMappingException(string value): BaseException
{
    public override string Message => $"Enum value is null after parsing string value '{value}'.";
}