namespace ReMarsModTool.DataStructures;

public class FoodInstance
{
    public int Calories { get; set; }
    public int Nutrition { get; set; }
    public int Delicacy { get; set; }
    public string? EatDuration { get; set; }
    public PersonAffect? PersonAffect { get; set; }
}