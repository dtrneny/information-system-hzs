using InformationSystemHZS.Models.Interfaces;
using InformationSystemHZS.Utils.Enums;

namespace InformationSystemHZS.Models;

public class Member: IBaseModel
{
    public string Callsign { get; set; }
    public string UnitCallsign { get; set; }
    public string Name { get; set; }
    public MemberRank Rank { get; set; }

    public Member(
        string callsign,
        string unitCallsign,
        string name,
        MemberRank rank
    )
    {
        Callsign = callsign;
        UnitCallsign = unitCallsign;
        Name = name;
        Rank = rank;
    }
    
    public Member(
        string unitCallsign,
        string name
    )
    {
        Callsign = "";
        UnitCallsign = unitCallsign;
        Name = name;
        Rank = MemberRank.PRIVATE;
    }
}