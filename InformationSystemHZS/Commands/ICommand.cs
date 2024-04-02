using InformationSystemHZS.Classes;
using InformationSystemHZS.Models;

namespace InformationSystemHZS.Commands;

public interface ICommand
{
    public List<string> Arguments { get; }

    public void Execute(SystemContext context);
}