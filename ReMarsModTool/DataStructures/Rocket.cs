namespace ReMarsModTool.DataStructures;

public class Rocket
{
    public string Fuel { get; set; }
    public int FuelDemand { get; set; }
    public LoadCtn LoadCtn { get; set; }
    public FuelCtn FuelCtn { get; set; }
    public int LaunchingTime { get; set; }
    public AnimFXs AnimFXs { get; set; }
}