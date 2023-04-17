using Newtonsoft.Json;

namespace ReMarsModTool.DataStructures;

public class PersonAffect
{
    public Sanity? Sanity { get; set; }
    public Satisfaction? Satisfaction { get; set; }
    public Health? Health { get; set; }
    public Calories? Calories { get; set; }
    public Energy? Energy { get; set; }
    
    public double? DeltaPerHour { get; set; }
    public int? Max { get; set; }
}