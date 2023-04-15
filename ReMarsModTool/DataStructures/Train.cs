using System.Collections.Generic;

namespace ReMarsModTool.DataStructures;

public class Train
{
    public int AdmissionFee { get; set; }
    public Dictionary<string, TrainingDetails> Details { get; set; }
}