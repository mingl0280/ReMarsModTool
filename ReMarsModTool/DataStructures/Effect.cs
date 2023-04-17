using System.Collections.Generic;

namespace ReMarsModTool.DataStructures;

public class Effect
{
    public string? Type { get; set; }
    public List<string>? UnlockBuildings { get; set; }
    public List<string>? UnlockRecipes { get; set; }
    public List<string>? UnlockCreatures { get; set; }
    public List<string>? UnlockUnits { get; set; }
    public List<string>? UnlockReformNames { get; set; }
}