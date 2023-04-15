using Newtonsoft.Json;
using ReMarsModTool.DataStructures;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ReMarsModTool.GlobalData
{
    public class GlobalItems
    {
        public static string ReMarsBaseLocStr = "";
        public static string CurrentModProjDir = "";
        public static ReMarsBaseMeta ReMarsMetadata;

        public static string ReMarsRootPath
        {
            get => _reMarsRootPath; set { _reMarsRootPath = value; }
        }

        public static Dictionary<string, ItemInstance> BaseItems { get; set; }
        public static Dictionary<string, SurfaceInstance> BaseTerrainLayers { get; set; }
        public static Dictionary<string, LandRes> BaseLandResItems { get; set; }
        public static Dictionary<string, ResearchItem> BaseResearchItems { get; set; }
        public static Dictionary<string, BuildingItem> BaseBuildingItems { get; set; }
        public static Dictionary<string, BuildingDisplayItem> BaseBuildingDisplayItems { get; set; }
        public static Dictionary<string, UnitItem> BaseUnitItems { get; set; }
        public static Dictionary<string, UnitDisplayItem> BaseUnitDisplayItems { get; set; }

        public static Dictionary<string, ItemInstance> ProjectItems { get; set; }
        public static Dictionary<string, SurfaceInstance> ProjectTerrainLayers { get; set; }
        public static Dictionary<string, LandRes> ProjectLandResItems { get; set; }
        public static Dictionary<string, ResearchItem> ProjectResearchItems { get; set; }
        public static Dictionary<string, BuildingItem> ProjectBuildingItems { get; set; }
        public static Dictionary<string, BuildingDisplayItem> ProjectBuildingDisplayItems { get; set; }
        public static Dictionary<string, UnitItem> ProjectUnitItems { get; set; }
        public static Dictionary<string, UnitDisplayItem> ProjectUnitDisplayItems { get; set; }

        public static Dictionary<T, TU> GenericInitFromDir<T, TU>(string RootPath, string ContentPath, Dictionary<T, TU>? TargetVariable) where T : notnull
        {
            var dir = RootPath + "\\" + ContentPath;
            FileInfo[] files;
            if (File.Exists(dir.Replace("\\\\", "\\")))
            {
                files = new FileInfo[1] { new(dir.Replace("\\\\", "\\")) };
            }
            else
            {
                files = new DirectoryInfo(dir).GetFiles("*.json");
            }
            TargetVariable ??= new Dictionary<T, TU>();
            JsonSerializerSettings settings = new JsonSerializerSettings() { MissingMemberHandling = MissingMemberHandling.Error };
            foreach (var item in files)
            {
                var agent_variable = new Dictionary<T, TU>();
                using (StreamReader meta_file = new StreamReader(new FileStream(item.FullName,
                           FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
                {
                    agent_variable =
                        JsonConvert.DeserializeObject<Dictionary<T, TU>>(meta_file.ReadToEnd(), settings);
                    foreach (var kvp_item in agent_variable)
                    {
                        if (kvp_item.Value != null)
                        {
                            Type Ts = kvp_item.Value.GetType();
                            Ts.GetProperty("Name").SetValue(kvp_item.Value, kvp_item.Key);
                        }
                        TargetVariable.Add(kvp_item.Key, kvp_item.Value);
                    }
                }
            }

            return TargetVariable;
        }
        public static List<string> PossibleTranslations { get; set; }

        private static string _reMarsRootPath = "";
    }
}
