using System.Collections.Generic;

namespace ReMarsModTool.DataStructures;

public class Effects
{
    public List<LaunchingDust>? launching_dust { get; set; }
    public List<LaunchingImpactDust>? launching_impact_dust { get; set; }
    public List<LaunchingFire>? launching_fire { get; set; }
}