using System.Collections.Generic;

namespace ReMarsModTool.DataStructures;

public class LandOccupy
{
    public int Width { get; set; }
    public int Height { get; set; }
    public List<int>? EcologyDemandRange { get; set; }
    public SeaFilter? SeaFilter { get; set; }
    public string? Layer { get; set; }
    public HeightOffsetFilter? HeightOffsetFilter { get; set; }
}