using System.Collections.Generic;

namespace ReMarsModTool.DataStructures;

public class Recipe
{
    public int Workload { get; set; }
    public Dictionary<string, int> RecipeItems { get; set; }
    public Dictionary<string, int> ProduceItems { get; set; }
    public Dictionary<string, int> RecipeItemTags { get; set; }
}