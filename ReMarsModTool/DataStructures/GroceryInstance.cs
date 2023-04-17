using Newtonsoft.Json;

namespace ReMarsModTool.DataStructures;

public class GroceryInstance
{
    public string? Type { get; set; }
    public int? Value { get; set; }
    public PersonAffect? PersonAffect { get; set; }
}