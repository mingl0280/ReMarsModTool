using System.Collections.Generic;

namespace ReMarsModTool.DataStructures;

public class MetaJsonObject
{
    public Dictionary<string, string>? Contents { get; set; }
    public Dictionary<string, string>? ContentsCSV { get; set; }
    public string? ConfigScript { get; set; }
}