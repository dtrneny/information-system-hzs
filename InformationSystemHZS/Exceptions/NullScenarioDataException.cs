namespace InformationSystemHZS.Exceptions;

public class NullScenarioDataException: BaseException
{
    public override string Message => "ScenarioObjectDto is null.";
}