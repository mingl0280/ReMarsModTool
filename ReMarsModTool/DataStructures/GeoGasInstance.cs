using System.Collections.Generic;

namespace ReMarsModTool.DataStructures;

public class GeoGasInstance
{
    public string? Type { get; set; }
    public GenNoise? GenNoise { get; set; }
    public List<AffectEnv>? AffectEnv { get; set; }
}