using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.WindowsAPICodePack.Dialogs;
using Microsoft.WindowsAPICodePack.Dialogs.Controls;
using Newtonsoft.Json;
using ReMarsModTool.GlobalData;
using Path = System.IO.Path;
using ReMarsModTool.DataStructures;

namespace ReMarsModTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = _notified_items;
            GlobalItems.PossibleTranslations = new List<string>();
            GlobalItems.PossibleTranslations.AddRange(new[] {"English", "ChineseSimplified" });
            MainOperations.IsEnabled = false;
        }

        

        private void OnNewClicked(object sender, RoutedEventArgs e)
        {
            // TODO: Create new folder under user selected location.
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                // Set the selected folder as the working directory
                GlobalItems.ProjectDir = dialog.FileName;

                DirectoryInfo di = new DirectoryInfo(dialog.FileName);

                // Initialize dictionaries to empty
                GlobalItems.ProjectItems = new Dictionary<string, ItemInstance>();
                GlobalItems.ProjectTerrainLayers = new Dictionary<string, SurfaceInstance>();
                GlobalItems.ProjectLandResItems = new Dictionary<string, LandRes>();
                GlobalItems.ProjectResearchItems = new Dictionary<string, ResearchItem>();
                GlobalItems.ProjectBuildingItems = new Dictionary<string, BuildingItem>();
                GlobalItems.ProjectBuildingDisplayItems = new Dictionary<string, BuildingDisplayItem>();
                GlobalItems.ProjectUnitItems = new Dictionary<string, UnitItem>();
                GlobalItems.ProjectUnitDisplayItems = new Dictionary<string, UnitDisplayItem>();
                _notified_items = new();
                MainOperations.IsEnabled = true;
                _notified_items.ModName = di.Name;
            }
        }

        private void OnOpenClicked(object sender, RoutedEventArgs e)
        {
            // TODO: Load Global Modded Items under Steam workshop location
        }

        private void OnSaveClicked(object sender, RoutedEventArgs e)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            settings.Formatting = Formatting.Indented;
            string title_json_content = "{\r\n    ";
            List<string> title_items = new List<string>();
            foreach (DataRow translation in _notified_items.Translations.Rows)
            {
                title_items.Add($"\"{translation[0]}\":\"{translation[1]}\"");
            }

            title_json_content += string.Join(",\r\n    ", title_items);
            title_json_content += "\r\n}";
            using StreamWriter title_json_writer = new StreamWriter(new FileStream(GlobalItems.ProjectDir + @"\title.json", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite));
            title_json_writer.WriteLine(title_json_content);
            title_json_writer.Flush();
            title_json_writer.Close();
            if (!Directory.Exists(GlobalItems.ProjectDir + @"\package"))
            {
                Directory.CreateDirectory(GlobalItems.ProjectDir + @"\package");
            }

            if (!string.IsNullOrWhiteSpace(_notified_items.PreviewPicture))
            {
                if (File.Exists(_notified_items.PreviewPicture))
                {
                    File.Copy(_notified_items.PreviewPicture, GlobalItems.ProjectDir + @"\preview.png");
                }
            }
            GlobalItems.ModRootPath = GlobalItems.ProjectDir + $@"\package\{_notified_items.ModID}";
            using StreamWriter meta_strm_writer = new StreamWriter(new FileStream(GlobalItems.ProjectDir + @"\package\meta.json", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite));
            ModMeta meta = new ModMeta();
            if (meta.Packages == null)
                meta.Packages = new List<string>();
            meta.Packages.Add(_notified_items.ModID);

            meta_strm_writer.WriteLine(JsonConvert.SerializeObject(meta, settings));
            meta_strm_writer.Flush();
            meta_strm_writer.Close();
            if (!Directory.Exists(GlobalItems.ModRootPath))
            {
                Directory.CreateDirectory(GlobalItems.ModRootPath);
            }

            MetaJsonObject mod_meta = new MetaJsonObject(); 
            if (mod_meta.Contents == null)
                mod_meta.Contents = new Dictionary<string, string>();
            //if (mod_meta.ContentsCSV == null)
                //mod_meta.ContentsCSV = new Dictionary<string, string>();
            if (GlobalItems.ProjectBuildingItems != null)
            {
                if (GlobalItems.ProjectBuildingItems.Count > 0)
                {
                    
                    mod_meta.Contents.Add("BuildingDB_ReMars", "building");
                }
            }
            if (GlobalItems.ProjectItems != null)
            {
                if (GlobalItems.ProjectItems.Count > 0)
                {
                    mod_meta.Contents.Add("ItemDB_ReMars", "item");
                }
            }

            if (GlobalItems.ProjectTerrainLayers != null)
            {
                if (GlobalItems.ProjectTerrainLayers.Count > 0)
                {
                    mod_meta.Contents.Add("TerrainLayerDB", "terrain");
                }
            }

            if (GlobalItems.ProjectLandResItems != null)
            {
                if (GlobalItems.ProjectLandResItems.Count > 0)
                {
                    mod_meta.Contents.Add("LandResourceDB", "landres/land_resources.json");
                }
            }

            if (GlobalItems.ProjectResearchItems != null)
            {
                if (GlobalItems.ProjectResearchItems.Count > 0)
                {
                    mod_meta.Contents.Add("ResearchDB", "research");
                }
            }

            if (GlobalItems.ProjectBuildingDisplayItems != null)
            {
                if (GlobalItems.ProjectBuildingDisplayItems.Count > 0)
                {
                    mod_meta.Contents.Add("BuildingDisplayDB_ReMars", "building_display");
                }
            }

            if (GlobalItems.ProjectUnitItems != null)
            {
                if (GlobalItems.ProjectUnitItems.Count > 0)
                {
                    mod_meta.Contents.Add("UnitDB_ReMars", "unit");
                }
            }

            if (GlobalItems.ProjectUnitDisplayItems != null)
            {
                if (GlobalItems.ProjectUnitDisplayItems.Count > 0)
                {
                    mod_meta.Contents.Add("UnitDisplayDB_ReMars", "unit_display");
                }
            }

            if (File.Exists(GlobalItems.ModRootPath + @"\config.lua"))
            {
                mod_meta.ConfigScript = GlobalItems.ModRootPath + @"\config.lua";
                mod_meta.ConfigScript = mod_meta.ConfigScript.Replace("\\\\", "\\");
            }

            using StreamWriter mod_meta_writer = new StreamWriter(new FileStream(
                GlobalItems.ModRootPath + @"\meta.json", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite));
            
            mod_meta_writer.WriteLine(JsonConvert.SerializeObject(mod_meta, settings));
            mod_meta_writer.Flush();
            mod_meta_writer.Close();

           WriteSerializedContent();

        }

        public static void WriteSerializedContent()
        {
            var mappings = new Dictionary<string, (object Data, string Path)>()
            {
                { "building", (GlobalItems.ProjectBuildingItems, "BuildingDB_ReMars")! },
                { "item", (GlobalItems.ProjectItems, "ItemDB_ReMars")! },
                { "terrain", (GlobalItems.ProjectTerrainLayers, "TerrainLayerDB")! },
                { "landres", (GlobalItems.ProjectLandResItems, "landres/land_resources.json")! },
                { "research", (GlobalItems.ProjectResearchItems, "ResearchDB")! },
                { "building_display", (GlobalItems.ProjectBuildingDisplayItems, "BuildingDisplayDB_ReMars")! },
                { "unit", (GlobalItems.ProjectUnitItems, "UnitDB_ReMars")! },
                { "unit_display", (GlobalItems.ProjectUnitDisplayItems, "UnitDisplayDB_ReMars")! }
            };

            foreach (var mapping in mappings)
            {
                if ((int)(mapping.Value.Data.GetType().GetProperty("Count", typeof(int))!.GetValue(mapping.Value.Data))! <= 0)
                    continue;

                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.NullValueHandling = NullValueHandling.Ignore;
                settings.Formatting = Formatting.Indented;
                
                string serialized_data = JsonConvert.SerializeObject(mapping.Value.Data, Formatting.Indented, settings);
                string output_path = mapping.Value.Path;

                // Check if the path has a file name
                if (Path.HasExtension(output_path))
                {
                    string? directory_path = Path.GetDirectoryName(output_path);
                    if (directory_path != null) Directory.CreateDirectory(directory_path);
                }
                else
                {
                    // If no file name is given, use the folder name as the file name
                    output_path = Path.Combine(GlobalItems.ModRootPath +"\\" + output_path, $"{mapping.Key}.json");
                }

                var output_file = new FileInfo(output_path);


                if (!Directory.Exists(output_file.DirectoryName))
                    Directory.CreateDirectory(output_file.DirectoryName);

                File.WriteAllText(output_path, serialized_data);

            }
        }

        private void OnSaveAsClicked(object sender, RoutedEventArgs e)
        {
            // TODO: Save mod to somerwhere else.
        }

        private void OnExportClicked(object sender, RoutedEventArgs e)
        {
            // TODO: Save mod to Steam workshop location
        }

        private void OnCloseClicked(object sender, RoutedEventArgs e)
        {
            // TODO: Close project
        }

        private void OnModConfigurationsClicked(object sender, RoutedEventArgs e)
        {
            // TODO: Not implmented yet.
        }

        private void OnAddBuildingClicked(object sender, RoutedEventArgs e)
        {
            // TODO: Add a building template.
        }

        private void OnAddUnitClicked(object sender, RoutedEventArgs e)
        {
            // TODO: Implement logic for when "Add Unit" is clicked
        }

        private void OnAddResearchClicked(object sender, RoutedEventArgs e)
        {
            // TODO: Implement logic for when "Add Research" is clicked
        }

        private void OnAddItemClicked(object sender, RoutedEventArgs e)
        {
            // TODO: Implement logic for when "Add Item" is clicked
        }

        private void OnAddAstroidClicked(object sender, RoutedEventArgs e)
        {
            // TODO: Implement logic for when "Add Astroid" is clicked
        }
        private void OnCloneBuildingClick(object sender, RoutedEventArgs e)
        {
            // TODO: Implement logic for when "Add Astroid" is clicked
        }

        private void HighEffeiciencyBuildings()
        {
            if (GlobalItems.ProjectBuildingItems == null)
                return;
            foreach (var project_building_item in GlobalItems.ProjectBuildingItems)
            {
                project_building_item.Value.Employer.UpdateAllEfficiency(1, 100);
            }
        }

        

        // Batch Clone Buildings to Mod
        private void BatchCloneBuildingsToMod_Click(object sender, EventArgs e)
        {
            var warn = MessageBox.Show(Properties.Resources.BatchOpWarning, Properties.Resources.Warning, MessageBoxButton.YesNo);
            if (warn != MessageBoxResult.Yes)
            {
                return;
            }

            GlobalItems.ProjectBuildingItems ??= new Dictionary<string, BuildingItem>();
            GlobalItems.ProjectBuildingDisplayItems ??= new Dictionary<string, BuildingDisplayItem>();
            GlobalItems.ProjectBuildingItems.Clear();
            GlobalItems.ProjectBuildingDisplayItems.Clear();
            GlobalItems.ProjectBuildingItems = new Dictionary<string, BuildingItem>(GlobalItems.BaseBuildingItems);
            GlobalItems.ProjectBuildingDisplayItems =
                new Dictionary<string, BuildingDisplayItem>(GlobalItems.BaseBuildingDisplayItems);
        }

        // Batch Modify Buildings
        private void BatchModifyBuildings_Click(object sender, EventArgs e)
        {
            // TODO: Implement functionality for Batch Modify Buildings menu item
        }

        // Batch Clone Units
        private void BatchCloneUnits_Click(object sender, EventArgs e)
        {
            // TODO: Implement functionality for Batch Clone Units menu item
        }

        // Batch Modify Units
        private void BatchModifyUnits_Click(object sender, EventArgs e)
        {
            // TODO: Implement functionality for Batch Modify Units menu item
        }
        NotifiedItems _notified_items = new NotifiedItems();
        
        private void BtnBrowsePreviewPic_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "JPEG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|BMP Files (*.bmp)|*.bmp";
            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                _notified_items.PreviewPicture = dlg.FileName;
                TextBoxPreviewPicPath.Text = dlg.FileName;
            }
        }

        private void TextBox_PreviewPicPath_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = (TextBox)sender;
            var filePath = textBox.Text;

            if (IsValidPicture(filePath))
            {
                _notified_items.PreviewPicture = filePath;
            }
        }

        private bool IsValidPicture(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return false;
            }

            if (!File.Exists(filePath))
            {
                return false;
            }

            var ext = Path.GetExtension(filePath).ToLower();
            if (ext == ".jpg" || ext == ".png" || ext == ".bmp")
                return true;
            return false;
        }

        private void TextBoxModName_LostFocus(object sender, RoutedEventArgs e)
        {
            DataGridTranslations.InvalidateVisual();
            DataGridTranslations.Items.Refresh();
        }

        private void OnHEBuildingClick(object Sender, RoutedEventArgs E)
        {
            HighEffeiciencyBuildings();
        }
    }
}
