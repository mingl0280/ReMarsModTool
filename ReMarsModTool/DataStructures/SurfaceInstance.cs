using Newtonsoft.Json;

namespace ReMarsModTool.DataStructures;

public class SurfaceInstance
{
    private LandRes? _landRes;

    [JsonIgnore]
    public string Name { get; set; }
    public string BassColor { get; set; }
    public int? TileSize { get; set; }
    public string? Normal { get; set; }
    public int NormalScale { get; set; }
    public double Smoothness { get; set; }
    public double Metallic { get; set; }
    public bool? IsBaseLayer { get; set; }
    public string? PlanetSurface { get; set; }
    public bool? IsCustom { get; set; }
    [JsonProperty("LandRes")]
    public string? LandResString { get; set; }

    [JsonIgnore]
    public LandRes? LandRes
    {
        get => _landRes;
        set
        {
            if (_landRes == null)
                LandResString = "";
            _landRes = value;
            LandResString = _landRes.ToString();
        }
    }
}