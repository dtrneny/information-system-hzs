namespace InformationSystemHZS.Exceptions;

public abstract class BaseException: Exception
{
    public abstract override string Message { get; }
}