namespace InformationSystemHZS.Exceptions;

public class NullScenarioObjectException(bool terminating): BaseException
{
    public override string Message => "Scenario object is null.";
    public override bool Terminating => terminating;
}