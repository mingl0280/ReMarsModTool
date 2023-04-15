using System.Collections.Generic;
using Newtonsoft.Json;

namespace ReMarsModTool.DataStructures;


public class ItemInstance
{
    [JsonIgnore]
    public string Name { get; set; }
    public string Icon { get; set; }
    public int StackingMax { get; set; }
    public bool IsFluid { get; set; }
    public List<string> Tags { get; set; }
    [JsonIgnore]
    public bool IsFood { get; set; }
    [JsonIgnore]
    public bool IsGas { get; set; }
    [JsonIgnore]
    public bool IsGrocery { get; set; }
    [JsonIgnore]
    public bool ContainsDurability { get; set; }
    [JsonIgnore]
    public bool IsRocket { get; set; }
    [JsonIgnore]
    public bool IsSatCreate { get; set; }
    [JsonIgnore]
    public bool IsPersonalConsumeType { get; set; }

    public FoodInstance? Food { get; set; }
    public GeoGasInstance? GeoGas { get; set; }
    public GroceryInstance? Grocery { get; set; }
    public Durability? Durability { get; set; }
    public Recipe? Recipe { get; set; }

    public int DefaultPrice { get; set; }

    public string? PersonConsumeType { get; set; }
    public bool? Hidden { get; set; } // If null not exist. 
    public Rocket? Rocket { get; set; }
    public SatCreate? SatCreate { get; set; }

    public Bomb? Bomb { get; set; }
}