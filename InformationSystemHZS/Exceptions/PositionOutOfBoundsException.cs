using InformationSystemHZS.Models;

namespace InformationSystemHZS.Exceptions;

public class PositionOutOfBoundsException(bool terminating, Position position): BaseException
{
    public override string Message => $"Position (x: {position.X}, y: {position.Y}) is out of bounds <0,99>.";
    public override bool Terminating => terminating;
}