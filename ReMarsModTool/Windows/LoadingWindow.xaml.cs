using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using Newtonsoft.Json;
using ReMarsModTool.DataStructures;
using ReMarsModTool.GlobalData;

namespace ReMarsModTool
{
    /// <summary>
    /// LoadingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoadingWindow : Window
    {
        public LoadingWindow()
        {
            InitializeComponent();
        }

        private class InitializeActionItem
        {
            public InitializeActionItem(string name, Action action)
            {
                StepName = name;
                Action = action;
            }
            public string StepName {get; set; }
            public Action Action { get; set; }
            public override string ToString()
            {
                return StepName;
            }
        }

        private List<InitializeActionItem> _initializeMethods = new();

        

        private delegate void UpdateInitStepsD(string NextStep);

        private delegate void InitCompleteD();

        private void OnInitComplete()
        {
            Hide();
            MainWindow main_wnd = new MainWindow();
            main_wnd.Show();
        }

        private void UpdateInitSteps(string NextStep)
        {
            ProgText.Text = $"{Properties.Resources.StepInitReMarsMainMeta} {NextStep} {Properties.Resources.StepInitReMarsMainMeta} {_loadingStepCounter} / {_totalSteps}";
            ProgLoading.Value = _loadingStepCounter;
        }

        private int _loadingStepCounter = 0, _totalSteps;

        private void LoadingWindow1_Loaded(object sender, RoutedEventArgs e)
        {
            // Set Reshaping mars install dir.
            _initializeMethods.Add(new InitializeActionItem(Properties.Resources.StepInitReMarsMainMeta, () => {
                RegistryKey? key =
                    Registry.LocalMachine.OpenSubKey(
                        @"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\Steam App 1395760");
                
                if (key == null)
                {
                    var ret = MessageBox.Show(Properties.Resources.StepInitReMarsMainMeta as string,
                        Properties.Resources.StepInitReMarsMainMeta as string, MessageBoxButton.YesNo,
                        MessageBoxImage.Error);
                    if (ret == MessageBoxResult.Yes)
                    {
                        OpenFileDialog open_file_dialog = new OpenFileDialog();
                        open_file_dialog.Filter = "ReshapingMars.exe|ReshapingMars.exe";
                        open_file_dialog.Title = Properties.Resources.StepInitReMarsMainMeta;

                        var result = open_file_dialog.ShowDialog();
                        if (result == false || result == null)
                            Environment.Exit(1);
                        var exe_info = new FileInfo(open_file_dialog.FileName);
                        var exe_path = exe_info.DirectoryName;
                        GlobalItems.ReMarsRootPath = exe_path + @"\cfgs\game\default_game\static_db\package\re_mars_base\";
                    }
                }
                else
                {
                    GlobalItems.ReMarsRootPath = (key.GetValue("InstallLocation") as string) +
                                                 @"\cfgs\game\default_game\static_db\package\re_mars_base\";
                }
            }));

            // Read out meta info
            _initializeMethods.Add(new InitializeActionItem(Properties.Resources.StepInitReMarsMainMeta,() =>
            {
                using (StreamReader meta_file = new StreamReader(new FileStream(GlobalItems.ReMarsRootPath + @"\meta.json",
                           FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    GlobalItems.ReMarsMetadata = serializer.Deserialize(meta_file, typeof(MetaJsonObject)) as MetaJsonObject;
                }
            }));


            // Read out built-in item info
            _initializeMethods.Add(new InitializeActionItem(Properties.Resources.StepInitItems, () =>
            {
                GlobalItems.BaseItems = GlobalItems.GenericInitFromDir(GlobalItems.ReMarsRootPath,
                    GlobalItems.ReMarsMetadata.Contents["ItemDB_ReMars"],
                    GlobalItems.BaseItems);
                foreach (var item_instance in GlobalItems.BaseItems)
                {
                    if (item_instance.Value.Food != null) item_instance.Value.IsFood = true;
                    if (item_instance.Value.GeoGas != null) item_instance.Value.IsGas = true;
                    if (item_instance.Value.Grocery != null) item_instance.Value.IsGrocery = true;
                    if (item_instance.Value.Durability != null) item_instance.Value.ContainsDurability = true;
                    if (item_instance.Value.Rocket != null) item_instance.Value.IsRocket = true;
                    if (item_instance.Value.SatCreate != null) item_instance.Value.IsSatCreate = true;
                    if (!string.IsNullOrEmpty(item_instance.Value.PersonConsumeType)) item_instance.Value.IsPersonalConsumeType = true;
                    if (item_instance.Value.Hidden != null) item_instance.Value.Hidden = true;
                }
            }));

            _initializeMethods.Add(new InitializeActionItem(Properties.Resources.StepInitResearch, () =>
            {
                GlobalItems.BaseLandResItems = GlobalItems.GenericInitFromDir(GlobalItems.ReMarsRootPath,
                    GlobalItems.ReMarsMetadata.Contents["LandResourceDB"],
                    GlobalItems.BaseLandResItems);
            }));

            _initializeMethods.Add(new InitializeActionItem(Properties.Resources.StepInitLayers, () =>
            {
                GlobalItems.BaseTerrainLayers = GlobalItems.GenericInitFromDir(GlobalItems.ReMarsRootPath,
                    GlobalItems.ReMarsMetadata.Contents["TerrainLayerDB"],
                    GlobalItems.BaseTerrainLayers);

                foreach (var base_terrain_layer in GlobalItems.BaseTerrainLayers)
                {
                    if (base_terrain_layer.Value.LandResString != null)
                        base_terrain_layer.Value.LandRes =
                            GlobalItems.BaseLandResItems[base_terrain_layer.Value.LandResString];
                }
            }));

            _initializeMethods.Add(new InitializeActionItem(Properties.Resources.StepInitResearch, () =>
            {
                GlobalItems.BaseResearchItems = GlobalItems.GenericInitFromDir(GlobalItems.ReMarsRootPath,
                    GlobalItems.ReMarsMetadata.Contents["ResearchDB"],
                    GlobalItems.BaseResearchItems);
            }));

            _initializeMethods.Add(new InitializeActionItem(Properties.Resources.StepInitUnits, () =>
            {
                GlobalItems.BaseUnitItems = GlobalItems.GenericInitFromDir(GlobalItems.ReMarsRootPath,
                    GlobalItems.ReMarsMetadata.Contents["UnitDB_ReMars"],
                    GlobalItems.BaseUnitItems);
                GlobalItems.BaseUnitDisplayItems = GlobalItems.GenericInitFromDir(GlobalItems.ReMarsRootPath,
                    GlobalItems.ReMarsMetadata.Contents["UnitDisplayDB_ReMars"], GlobalItems.BaseUnitDisplayItems);
            }));

            _initializeMethods.Add(new InitializeActionItem(Properties.Resources.StepInitBuildings, () =>
            {
                GlobalItems.BaseBuildingItems = GlobalItems.GenericInitFromDir(GlobalItems.ReMarsRootPath,
                    GlobalItems.ReMarsMetadata.Contents["BuildingDB_ReMars"], GlobalItems.BaseBuildingItems);
                GlobalItems.BaseBuildingDisplayItems = GlobalItems.GenericInitFromDir(GlobalItems.ReMarsRootPath,
                    GlobalItems.ReMarsMetadata.Contents["BuildingDisplayDB_ReMars"],
                    GlobalItems.BaseBuildingDisplayItems);
            }));


            _totalSteps = _initializeMethods.Count;
            ProgLoading.Maximum = _totalSteps;
            var execution_thread = new Thread(() =>
            {
                foreach (var initialize_method in _initializeMethods)
                {
                    Interlocked.Increment(ref _loadingStepCounter);
                    Dispatcher.BeginInvoke(new UpdateInitStepsD(UpdateInitSteps), initialize_method.StepName);
                    initialize_method.Action();
                }

                Dispatcher.BeginInvoke(new InitCompleteD(OnInitComplete));
            });
            execution_thread.Start();
        }
    }
}
