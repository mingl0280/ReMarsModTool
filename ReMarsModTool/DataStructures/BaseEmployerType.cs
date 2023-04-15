using System.Collections.Generic;

namespace ReMarsModTool.DataStructures;

public class BaseEmployerType
{
    public int Capacity { get; set; }
    public double WorkloadIncrease { get; set; }
    public int DailySalary { get; set; }
    public List<string> WorkingShifts { get; set; }
}
