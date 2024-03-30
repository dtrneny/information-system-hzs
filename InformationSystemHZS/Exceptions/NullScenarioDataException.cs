namespace InformationSystemHZS.Exceptions;

public class NullScenarioDataException(bool terminating): BaseException
{
    public override string Message => "ScenarioObjectDto is null.";
    public override bool Terminating => terminating;
}