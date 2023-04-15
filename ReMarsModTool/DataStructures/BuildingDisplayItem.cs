using Newtonsoft.Json;

namespace ReMarsModTool.DataStructures;

public class BuildingDisplayItem
{
    [JsonIgnore]
    public string Name { get; set; }
    public string Icon { get; set; }
    public double BuildingHeight { get; set; }
    public string? Proto { get; set; }
    public ModifyTerrainLayer? ModifyTerrainLayer { get; set; }
    public Light? Light { get; set; }
    public string? CustomPickupObj { get; set; }
}