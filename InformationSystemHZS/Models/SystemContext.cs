using InformationSystemHZS.IO;

namespace InformationSystemHZS.Models;

public class SystemContext(OutputWriter outputWriter, ScenarioObject scenarioObject)
{
    public OutputWriter OutputWriter { get; } = outputWriter;
    public ScenarioObject ScenarioObject { get; } = scenarioObject;
}