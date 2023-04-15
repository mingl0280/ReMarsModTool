using System.Collections.Generic;

namespace ReMarsModTool.DataStructures;

public class SeaFilter
{
    public bool IgnoreSea { get; set; }
    public List<Offset> NeedSeaOffsets { get; set; }
}