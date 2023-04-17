using System.Collections.Generic;
using Newtonsoft.Json;

namespace ReMarsModTool.DataStructures;

public class HeightOffsetFilter
{
    public ForceHeight? ForceHeight { get; set; }
    public bool? IgnoreHeightDif { get; set; }
    public List<Offset>? Offsets { get; set; }
    [JsonProperty("Ref")]
    public Reference? Reference { get; set; }
}