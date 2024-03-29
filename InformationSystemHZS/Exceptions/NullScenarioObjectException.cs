namespace InformationSystemHZS.Exceptions;

public class NullScenarioObjectException: BaseException
{
    public override string Message => "Scenario object is null.";
}