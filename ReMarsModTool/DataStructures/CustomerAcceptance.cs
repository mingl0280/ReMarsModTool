using System.Collections.Generic;

namespace ReMarsModTool.DataStructures;

public class CustomerAcceptance
{
    public string PersonConsumeType { get; set; }
    public int MaxNum { get; set; }
    public int AdmissionFee { get; set; }
    public int WorkloadDemand { get; set; }
    public Dictionary<string, ConsumeWeigh>? ConsumeWeigh { get; set; }
    public Dictionary<string, PersonAffect>? PersonAffect { get; set; }
    public List<Work>? Works { get; set; }
}