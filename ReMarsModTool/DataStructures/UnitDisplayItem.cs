using Newtonsoft.Json;

namespace ReMarsModTool.DataStructures;

public class UnitDisplayItem
{
    [JsonIgnore]
    public string Name { get; set; }
    public string Proto { get; set; }
}