using System.Collections.Generic;

namespace ReMarsModTool.DataStructures;

public class Employer
{
    public Labor? Labor { get; set; }
    public Engineer? Engineer { get; set; }
    public CivilServant? CivilServant { get; set; }
    public Governor? Governor { get; set; }
    public Farmer? Farmer { get; set; }
    public AgriculturalMaster? AgriculturalMaster { get; set; }
    public Researcher? Researcher { get; set; }
    public Doctor? Doctor { get; set; }
    public Officer? Officer { get; set; }
    public ChiefEngineer? ChiefEngineer { get; set; }
    public Scientist? Scientist { get; set; }
    public MedicalMaster? MedicalMaster { get; set; }
    public General? General { get; set; }
}

public class Labor : BaseEmployerType { }
public class Governor : BaseEmployerType { }
public class CivilServant : BaseEmployerType { }
public class Engineer : BaseEmployerType { }
public class Farmer : BaseEmployerType { }
public class AgriculturalMaster : BaseEmployerType { }
public class Researcher : BaseEmployerType { }
public class Doctor : BaseEmployerType { }
public class Officer : BaseEmployerType { }
public class ChiefEngineer : BaseEmployerType { }
public class Scientist : BaseEmployerType { }
public class MedicalMaster : BaseEmployerType { }
public class General : BaseEmployerType { }