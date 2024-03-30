namespace InformationSystemHZS.Exceptions;

public abstract class BaseException: Exception
{
    public abstract string Message { get; }
    public abstract bool Terminating { get;  }
}