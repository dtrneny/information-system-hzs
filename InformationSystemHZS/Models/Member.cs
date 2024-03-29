using InformationSystemHZS.Models.Interfaces;
using InformationSystemHZS.Utils.Enums;

namespace InformationSystemHZS.Models;

public class Member(
    string callsign,
    string unitCallsign,
    string name,
    MemberRank rank
): IBaseModel
{
    public string Callsign { get; set; } = callsign;
    public string UnitCallsign { get; set; } = unitCallsign;
    public string Name { get; set; } = name;
    public MemberRank Rank { get; set; } = rank;
}