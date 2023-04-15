using System.Collections.Generic;
using Newtonsoft.Json;

namespace ReMarsModTool.DataStructures;

public class ResearchItem
{
    [JsonIgnore]
    public string Name { get; set; }
    public int Workload { get; set; }
    public bool? Hide { get; set; }
    public List<Effect> Effects { get; set; }
    public List<string> EnableInGameModes { get; set; }
    public List<string> PreResearches { get; set; }
    public List<string> DisableInGameModes { get; set; }
}