using DiGi.Core.Classes;
using DiGi.GIS.Classes;
using DiGi.GIS.Enums;
using DiGi.GIS.UI.Controls;
using DiGi.Typology.Classes;
using DiGi.UI.WPF.Core.Classes;
using LiveCharts;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace DiGi.GIS.UI.Windows
{
    /// <summary>
    /// Interaction logic for TypologyWindow.xaml
    /// </summary>
    public partial class TypologyWindow : Window
    {
        private readonly GISModelFileManager gISModelFileManager = new();

        public TypologyWindow()
        {
            InitializeComponent();

            ContextMenu contextMenu = new();

            MenuItem menuItem;

            menuItem = new MenuItem();
            menuItem = new MenuItem { Name = "MenuItem_Properties", Header = "Properties" };
            menuItem.Click += MenuItem_Properties_Click;

            contextMenu.Items.Add(menuItem);

            menuItem = new MenuItem { Name = "MenuItem_BuildingShape", Header = "Building shape" };
            menuItem.Click += MenuItem_BuildingShape_Click;

            contextMenu.Items.Add(menuItem);

            menuItem = new MenuItem { Name = "MenuItem_RecalculateOrtoDatas", Header = "Recalculate Orto Datas" };
            menuItem.Click += MenuItem_RecalculateOrtoDatas_Click;

            contextMenu.Items.Add(menuItem);

            ListBox_References.ContextMenu = contextMenu;
        }

        private Building2D? GetActiveBuilding2D(out GISModel? gISModel)
        {
            gISModel = null;

            if (ListBox_References?.SelectedItem is not string reference)
            {
                return null;
            }

            List<GISModel>? gISModels = GetActiveGISModels();
            if (gISModels is null || gISModels.Count == 0)
            {
                return null;
            }

            foreach (GISModel gISModel_Temp in gISModels)
            {
                Building2D? result = gISModel_Temp.GetObject<Building2D>(reference);
                if (result is null)
                {
                    continue;
                }

                gISModel = gISModel_Temp;
                return result;
            }

            return null;
        }

        private string? GetActiveDirectory()
        {
            if (GetActiveTypologyFile()?.Path is not string path)
            {
                return null;
            }

            return System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(path));
        }

        private List<string>? GetActiveGISModelFilePaths()
        {
            if (GetActiveDirectory() is not string directory || string.IsNullOrWhiteSpace(directory) || !Directory.Exists(directory))
            {
                return null;
            }

            return [.. Directory.GetFiles(directory, string.Format("*.{0}", Constans.FileExtension.GISModelFile), SearchOption.TopDirectoryOnly)];
        }

        private List<GISModel>? GetActiveGISModels()
        {
            List<string>? gISModelFilePaths = GetActiveGISModelFilePaths();
            if (gISModelFilePaths is null || gISModelFilePaths.Count == 0)
            {
                return null;
            }

            List<GISModel> result = [];
            foreach (string gISModelFilePath in gISModelFilePaths)
            {
                GISModel? gISModel = null;

                if (gISModelFileManager.GetGuidExternalReference(gISModelFilePath) is GuidExternalReference guidExternalReference)
                {
                    gISModel = gISModelFileManager.GetGISModel(guidExternalReference);
                }

                if (gISModel is null)
                {
                    IndeterminateWindowWorker indeterminateProgressVisualWorker = new(this)
                    {
                        Text = "Loading..."
                    };

                    indeterminateProgressVisualWorker.DoWork += (sender, e) =>
                    {
                        GuidExternalReference? guidExternalReference_Temp = gISModelFileManager.Open(gISModelFilePath);
                        if (guidExternalReference_Temp is not null)
                        {
                            gISModel = gISModelFileManager.GetGISModel(guidExternalReference_Temp);
                        }
                    };

                    indeterminateProgressVisualWorker.RunWorkerCompleted += (sender, e) =>
                    {
                        LoadBuidling2D();
                    };

                    indeterminateProgressVisualWorker.Run();
                }

                if (gISModel is null)
                {
                    continue;
                }

                result.Add(gISModel);
            }

            return result;
        }

        private TypologyFile? GetActiveTypologyFile()
        {
            if (TreeViewControl_Typology.Tag is not TypologyFile typologyFile)
            {
                return null;
            }

            return typologyFile;
        }

        private string? GetPath(GISModel? gISModel)
        {
            return gISModelFileManager.GetPath(gISModel);
        }

        private void ListBox_References_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadBuidling2D();
        }

        private void LoadBuidling2D()
        {
            WrapPanel_Main.Children.Clear();

            Building2D? building2D = GetActiveBuilding2D(out GISModel? gISModel);

            if (GetPath(gISModel) is not string path)
            {
                return;
            }

            List<OrtoDataControl>? ortoDataControls = Create.OrtoDataControls(gISModel, path, building2D);
            if (ortoDataControls is null)
            {
                return;
            }

            foreach (OrtoDataControl ortoDataControl in ortoDataControls)
            {
                WrapPanel_Main.Children.Add(ortoDataControl);
            }

            //string? directory = System.IO.Path.GetDirectoryName(path);
            //if (string.IsNullOrWhiteSpace(directory) || !System.IO.Directory.Exists(directory))
            //{
            //    return;
            //}

            //string? directory_OrtoDatas = GIS.Query.OrtoDatasDirectory_Building2D(directory);

            //OrtoDatas? ortoDatas = GIS.Query.OrtoDatas(building2D, directory_OrtoDatas);
            //if (ortoDatas == null || !ortoDatas.Any())
            //{
            //    return;
            //}

            //short? userYear = GIS.Query.UserYearBuilt(directory, building2D);

            //short? predictedYear = GIS.Query.LatestPredictedYearBuilt(directory, building2D);
        }

        private void LoadTypologyFile(TypologyFile? typologyFile)
        {
            TreeViewControl_Typology.ClearItems();
            TreeViewControl_Typology.Tag = null;

            if (typologyFile?.Value is not Typology.Classes.Typology typology)
            {
                return;
            }

            List<TypologyPath>? typologyPaths = typology?.GetTypologyPaths(true);
            if (typologyPaths == null || typologyPaths.Count == 0)
            {
                return;
            }

            List<Typology.Classes.Typology> typologies = [];
            foreach (TypologyPath typologyPath in typologyPaths)
            {
                if (typology?.GetTypology(typologyPath) is not Typology.Classes.Typology typology_Temp)
                {
                    continue;
                }

                if (typology_Temp.References == null || typology_Temp.References.Count == 0)
                {
                    continue;
                }

                typologies.Add(typology_Temp);
            }

            TreeViewControl_Typology.Tag = typologyFile;

            TreeViewControl_Typology.ItemAdding += TreeViewControl_Typology_ItemAdding;

            TreeViewControl_Typology.SetItems(typologies);

            TreeViewControl_Typology.ItemAdding -= TreeViewControl_Typology_ItemAdding;
        }

        private void MenuItem_About_Click(object sender, RoutedEventArgs e)
        {
        }

        private void MenuItem_BuildingShape_Click(object sender, RoutedEventArgs e)
        {
            Building2D? building2D = GetActiveBuilding2D(out GISModel? gISModel);
            if (building2D is null)
            {
                return;
            }

            BuildingShapeSolver buildingShapeSolver = new()
            {
                Input = building2D
            };

            BuildingShape buildingShape = BuildingShape.Undefined;
            if (buildingShapeSolver.Solve())
            {
                buildingShape = buildingShapeSolver.Output;
            }

            MessageBox.Show(this, string.Format("Building shape: {0}", Core.Query.Description(buildingShape)), "Building Shape", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void MenuItem_Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MenuItem_Load_Click(object sender, RoutedEventArgs e)
        {
            string? path = DiGi.UI.WPF.Core.Query.Path(this, Typology.Constans.FileFilter.TypologyFile);
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            TypologyFile typologyFile = new(path);
            typologyFile.Open();

            LoadTypologyFile(typologyFile);
        }

        private void MenuItem_Properties_Click(object sender, RoutedEventArgs e)
        {
            Building2D? building2D = GetActiveBuilding2D(out GISModel? gISModel);
            if (building2D is null)
            {
                return;
            }

            Building2DWindow building2DWindow = new(building2D);
            building2DWindow.Show();
        }

        private async void MenuItem_RecalculateOrtoDatas_Click(object sender, RoutedEventArgs e)
        {
            Building2D? building2D = GetActiveBuilding2D(out GISModel? gISModel);
            if (building2D is null)
            {
                return;
            }

            if (GetPath(gISModel) is not string path || string.IsNullOrWhiteSpace(path))
            {
                return;
            }

            if (System.IO.Path.GetDirectoryName(path) is not string directoryName)
            {
                return;
            }

            string path_OrtoDatasFile = System.IO.Path.Combine(directoryName, string.Format("{0}.{1}", System.IO.Path.GetFileNameWithoutExtension(path), Constans.FileExtension.OrtoDatasFile));

            HashSet<GuidReference>? guidReferences = await GIS.Modify.CalculateOrtoDatas([building2D], path_OrtoDatasFile, Create.OrtoDatasBuilding2DOptions(), true);
            if (guidReferences != null && guidReferences.Count != 0)
            {
                LoadBuidling2D();
            }
        }

        private void TreeViewControl_Typology_ItemAdding(object sender, TreeViewItemAddingEventArgs e)
        {
            if (TreeViewControl_Typology.Tag is not TypologyFile typologyFile)
            {
                return;
            }

            if (typologyFile.Value is not Typology.Classes.Typology typology)
            {
                return;
            }

            if (e.Item is not Typology.Classes.Typology typology_Temp)
            {
                return;
            }

            e.Path = Create.ItemPath(typology, typology_Temp.TypologyPath, out string? name);
            e.Name = null;
        }

        private void TreeViewControl_Typology_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            ListBox_References.SelectionChanged -= ListBox_References_SelectionChanged;

            ListBox_References.Items.Clear();

            if (e.NewValue is not ItemPathTreeViewItem itemPathTreeViewItem)
            {
                Label_Count.Content = "Count: 0";
                return;
            }

            //ItemPath? itemPath = itemPathTreeViewItem.ItemPath;

            Typology.Classes.Typology? typology = itemPathTreeViewItem.Tag as Typology.Classes.Typology;

            HashSet<string> references = [];

            List<Typology.Classes.Typology>? typologies = typology is not null ? [typology] : DiGi.UI.WPF.Core.Query.TagItems<Typology.Classes.Typology, TreeViewItem>(itemPathTreeViewItem.Items, true, false);
            if (typologies != null)
            {
                foreach (Typology.Classes.Typology typology_Temp in typologies)
                {
                    if (typology_Temp.GetReferences(true) is HashSet<string> references_Temp)
                    {
                        references.UnionWith(references_Temp);
                    }
                }
            }

            foreach (string reference in references)
            {
                ListBox_References.Items.Add(reference);
            }

            Label_Count.Content = string.Format("Count: {0}", references.Count);

            PieChart_Main.Series = [];

            if (itemPathTreeViewItem.Items != null)
            {
                foreach (ItemPathTreeViewItem itemPathTreeViewItem_Temp in itemPathTreeViewItem.Items)
                {
                    HashSet<string> references_SubTypology = [];

                    List<Typology.Classes.Typology>? subTypologies = DiGi.UI.WPF.Core.Query.TagItems<Typology.Classes.Typology, TreeViewItem>(itemPathTreeViewItem_Temp.Items, true, false);
                    if (subTypologies != null)
                    {
                        foreach (Typology.Classes.Typology subTypology in subTypologies)
                        {
                            if (subTypology.GetReferences(true) is HashSet<string> references_Temp)
                            {
                                references_SubTypology.UnionWith(references_Temp);
                            }
                        }
                    }

                    if (references_SubTypology.Count == 0)
                    {
                        continue;
                    }

                    PieChart_Main.Series.Add(new LiveCharts.Wpf.PieSeries { Title = itemPathTreeViewItem_Temp.Header.ToString(), Values = new ChartValues<double> { references_SubTypology.Count }, DataLabels = true });
                }

                if (PieChart_Main.Series.Count == 0)
                {
                    PieChart_Main.Series.Add(new LiveCharts.Wpf.PieSeries { Title = itemPathTreeViewItem.Header.ToString(), Values = new ChartValues<double> { references.Count }, DataLabels = true });
                }
            }

            ListBox_References.SelectionChanged += ListBox_References_SelectionChanged;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TreeViewControl_Typology.SelectedItemChanged += TreeViewControl_Typology_SelectedItemChanged;
        }
    }
}