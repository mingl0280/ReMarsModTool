using Newtonsoft.Json;

namespace ReMarsModTool.DataStructures;

public class GroceryInstance
{
    [JsonProperty("Type")]
    public string GroceryType { get; set; }
    public int Value { get; set; }
    public PersonAffect? PersonAffect { get; set; }
}