using Newtonsoft.Json;

namespace ReMarsModTool.DataStructures;

public class LandRes
{
    [JsonIgnore]
    public string Name { get; set; }
    public string OutputItem { get; set; }
    public Display Display { get; set; }
    public Generate Generate { get; set; }
    public GenerateGlobal GenerateGlobal { get; set; }
    public override string ToString()
    {
        return Name;
    }
}