using System.Collections.Generic;

namespace ReMarsModTool.DataStructures;

public class Producer
{
    public double WorkloadRate { get; set; }
    public List<string>? Recipes { get; set; }
}