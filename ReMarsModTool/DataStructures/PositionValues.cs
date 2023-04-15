namespace ReMarsModTool.DataStructures;

public class PositionValues
{
    public int X { get; set; }
    public int Y { get; set; }
    public int? Z { get; set; }
    public int? Value { get; set; }
}

public class Reference : PositionValues { }
public class ForceHeight : PositionValues { }
public class Offset : PositionValues { }