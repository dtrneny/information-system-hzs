using InformationSystemHZS.IO;
using InformationSystemHZS.Models;

namespace InformationSystemHZS.Classes;

public class SystemContext(OutputWriter outputWriter, ScenarioObject scenarioObject)
{
    public OutputWriter OutputWriter { get; } = outputWriter;
    public ScenarioObject ScenarioObject { get; } = scenarioObject;
}