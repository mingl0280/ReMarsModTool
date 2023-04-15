using System.Collections.Generic;
using Newtonsoft.Json;

namespace ReMarsModTool.DataStructures;

public class UnitItem
{
    [JsonIgnore]
    public string Name { get; set; }
    public string? Display { get; set; }
    public int MoveSpeed { get; set; }
    public int TurnSpeed { get; set; }
    public bool FlyingUnit { get; set; }
    public int HeightOffset { get; set; }
    public List<string>? WorkCategories { get; set; }
    public LandResourceExploit? LandResourceExploit { get; set; }
    public Container? Container { get; set; }
    public LandSurvey? LandSurvey { get; set; }
    public Construct? Construct { get; set; }
    public CanBeDamaged? CanBeDamaged { get; set; }
    public double Mass { get; set; }
    public double MassVar { get; set; }
    public double ColRadius { get; set; }
    public int EnergyMax { get; set; }
    public int ConsumePower { get; set; }
    public int SolarPower { get; set; }
    public int CtrlSeatNum { get; set; }
    public int CtrlWorkrateDemand { get; set; }
    public CtrlWorkrateCoef? CtrlWorkrateCoef { get; set; }
    public bool Charger { get; set; }
    public SkillCaster? SkillCaster { get; set; }
    public LandReform? LandReform { get; set; }
    public LandEcologyReap? LandEcologyReap { get; set; }
    public int ViewRadius { get; set; }
    public int OutputPower { get; set; }
    public PassengerBinHolder? PassengerBinHolder { get; set; }
    public List<string>? Tags { get; set; }
}