using System.Collections.Generic;
using Newtonsoft.Json;

namespace ReMarsModTool.DataStructures;

public class BuildingItem
{
    [JsonIgnore]
    public string Name { get; set; }
    public string? Display { get; set; }
    public Dictionary<string, int>? ConstructItems { get; set; }
    public LandOccupy? LandOccupy { get; set; }
    public double? ConstructWorkload { get; set; }
    public CanBeDamaged? CanBeDamaged { get; set; }
    public SkillCaster? SkillCaster { get; set; }
    public InfoTransfer? InfoTransfer { get; set; }
    public bool? ShowState { get; set; }
    public bool? Ramp { get; set; }
    public bool? IgnoreRotation { get; set; }
    public Road? Road { get; set; }
    public List<string>? IgnorePipeItems { get; set; }
    public List<string>? Tags { get; set; }
    public int? LevelScore { get; set; }
    public ElecConsume? ElecConsume { get; set; }
    public Maintain? Maintain { get; set; }
    public List<SubContainer>? SubContainers { get; set; }
    public Producer? Producer { get; set; }
    public Employer? Employer { get; set; }
    public bool? Airborne { get; set; }
    public string? AirborneFX { get; set; }
    public string? DBScript { get; set; }
    public Container? Container { get; set; }
    public Market? Market { get; set; }
    public CustomerAcceptance? CustomerAcceptance { get; set; }
    public HumanityGen? HumanityGen { get; set; }
    public InfluenceGen? InfluenceGen { get; set; }
    public int? NumLimitPerRegion { get; set; }
    public string? Pipe { get; set; }
    public CreatureHost? CreatureHost { get; set; }
    public BuildingBuff? Researcher { get; set; }
    public int? RegularWorkload { get; set; }
    public Train? Train { get; set; }
    public ElecGenerate? ElecGenerate { get; set; }
    public ElecConnect? ElecConnect { get; set; }
    public ElecStorage? ElecStorage { get; set; }
    public bool? Charger { get; set; }
    public List<string>? ReplaceBuildings { get; set; }
    public LandResourceExploit? LandResourceExploit { get; set; }
    public PumpingWell? PumpingWell { get; set; }
    public RegionExplore? RegionExplore { get; set; }
    public UnitControl? UnitControl { get; set; }
    public Transporter? Transporter { get; set; }
    public PassengerTransporter? PassengerTransporter { get; set; }
    public PassengerBinHolder? PassengerBinHolder { get; set; }
    public Residence? Residence { get; set; }
    public SpaceEjector? SpaceEjector { get; set; }
    public SpaceBase? SpaceBase { get; set; }
    public int? ObstacleWidth { get; set; }
    public int? ObstacleHeight { get; set; }
    public CoreDrill? CoreDrill { get; set; }
    public GeoDiversionWell? GeoDiversionWell { get; set; }
}
public class GeoDiversionWell
{
    public int? ExportWorkload { get; set; }
    public int? WorkRate { get; set; }
    public List<GenItem>? GenItems { get; set; }
    public double? GenItemsGeoCoef { get; set; }
}

public class GenItem
{
    public int? Prob { get; set; }
    public string? Item { get; set; }
    public int? CountMin { get; set; }
    public int? CountMax { get; set; }
}

public class CoreDrill
{
    public float? WorkloadCoef { get; set; }
    public int? ShipToCoreWorkrate { get; set; }
    public int? ShipToCoreWorkload { get; set; }
    public List<CoreExplodeGenItem>? CoreExplodeGenItems { get; set; }
    public float? CoreExplodeGenItemsGeoCoef { get; set; }
}

public class CoreExplodeGenItem
{
    public int? Prob { get; set; }
    public string? Item { get; set; }
    public int? CountMin { get; set; }
    public int? CountMax { get; set; }
}
public class SpaceBase
{
    public int? FillFuelWorkload { get; set; }
    public int? DeploySatWorkload { get; set; }
    public int? AutoWorkRate { get; set; }
}
public class SpaceEjector { }

public class Road
{
    public int? Level { get; set; }
}

public class PumpingWell
{
    public List<Offset>? EcologyDetectOffsets { get; set; }
    public int? EcologyDemand { get; set; }
    public double? EcologyToProduceCoef { get; set; }
    public int? WorkloadRate { get; set; }
    public int? WorkloadPerCycle { get; set; }
    public Dictionary<string, int>? ProduceItems { get; set; }
}
public class RegionExplore
{
    public int? WorkloadRate { get; set; }
    public int? RangeMax { get; set; }
}
public class UnitControl
{
    public int? SeatMax { get; set; }
}

public class TransporterBase
{

}
public class Transporter : TransporterBase { }
public class PassengerTransporter : TransporterBase { }
public class Residence
{
    public int? HousingComfort { get; set; }
    public int? Capacity { get; set; }
    public int? CapacityLimit { get; set; }
    public int? DailyRent { get; set; }
}

