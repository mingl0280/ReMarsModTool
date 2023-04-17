using System.Collections.Generic;

namespace ReMarsModTool.DataStructures;

public class CreatureHost
{
    public int? Room { get; set; }
    public double? WorkloadRate { get; set; }
    public List<string>? Creatures { get; set; }
}