using DiGi.Core.Classes;
using DiGi.GIS.Classes;
using DiGi.GIS.UI.Controls;
using DiGi.Typology.Classes;
using DiGi.UI.WPF.Core.Classes;
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
        private readonly GISModelFileManager gISModelFileManager = new ();

        public TypologyWindow()
        {
            InitializeComponent();
        }

        private TypologyFile? GetActiveTypologyFile()
        {
            if (TreeViewControl_Typology.Tag is not TypologyFile typologyFile)
            {
                return null;
            }

            return typologyFile;
        }

        private string? GetActiveDirectory()
        {
            if(GetActiveTypologyFile()?.Path is not string path)
            {
                return null;
            }

            return System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(path));
        }

        private List<string>? GetActiveGISModelFilePaths()
        {
            if(GetActiveDirectory() is not string directory || string.IsNullOrWhiteSpace(directory) || !Directory.Exists(directory))
            {
                return null;
            }

            return [.. Directory.GetFiles(directory, string.Format("*.{0}", Constans.FileExtension.GISModelFile), SearchOption.TopDirectoryOnly)];
        }

        private List<GISModel>? GetActiveGISModels()
        {
            List<string>? gISModelFilePaths = GetActiveGISModelFilePaths();
            if(gISModelFilePaths is null || gISModelFilePaths.Count == 0)
            {
                return null;
            }

            List<GISModel> result = [];
            foreach (string gISModelFilePath in gISModelFilePaths)
            {
                GISModel? gISModel = null;

                if(gISModelFileManager.GetGuidExternalReference(gISModelFilePath) is GuidExternalReference guidExternalReference)
                {
                    gISModel = gISModelFileManager.GetGISModel(guidExternalReference);
                }

                if(gISModel is null)
                {
                    GuidExternalReference? guidExternalReference_Temp = gISModelFileManager.Open(gISModelFilePath);
                    if(guidExternalReference_Temp is null)
                    {
                        continue;
                    }

                    gISModel = gISModelFileManager.GetGISModel(guidExternalReference_Temp);
                }

                if(gISModel is null)
                {
                    continue;
                }

                result.Add(gISModel);
            }

            return result;
        }

        private string? GetPath(GISModel? gISModel)
        {
            return gISModelFileManager.GetPath(gISModel);
        }

        private void ListBox_References_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WrapPanel_Main.Children.Clear();

            if (ListBox_References.SelectedItem is not string reference)
            {
                return;
            }

            List<GISModel>? gISModels = GetActiveGISModels();
            if(gISModels is null || gISModels.Count == 0)
            {
                return;
            }

            GISModel? gISModel = null;
            Building2D? building2D = null;

            foreach (GISModel gISModel_Temp in gISModels)
            {
                building2D = gISModel_Temp.GetObject<Building2D>(reference);
                if (building2D is null)
                {
                    continue;
                }

                gISModel = gISModel_Temp;
                break;
            }

            if (GetPath(gISModel) is not string path)
            {
                return;
            }

            string? directory = System.IO.Path.GetDirectoryName(path);
            if (string.IsNullOrWhiteSpace(directory) || !Directory.Exists(directory))
            {
                return;
            }

            string? directory_OrtoDatas = GIS.Query.OrtoDatasDirectory_Building2D(directory);

            List<OrtoDataControl>? ortoDataControls = Create.OrtoDataControls(gISModel, building2D, directory_OrtoDatas);
            if(ortoDataControls is null)
            {
                return;
            }

            foreach (OrtoDataControl ortoDataControl in ortoDataControls)
            {
                WrapPanel_Main.Children.Add(ortoDataControl);
                ortoDataControl.MouseDown += OrtoDataControl_MouseDown;
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

        private void OrtoDataControl_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
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

            ItemPath? itemPath = itemPathTreeViewItem.ItemPath;

            HashSet<string> references = [];

            List<Typology.Classes.Typology>? typologies = itemPathTreeViewItem.Tag is Typology.Classes.Typology typology ? [typology] : DiGi.UI.WPF.Core.Query.TagItems<Typology.Classes.Typology, TreeViewItem>(itemPathTreeViewItem.Items, true, false);
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

            ListBox_References.SelectionChanged += ListBox_References_SelectionChanged;
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TreeViewControl_Typology.SelectedItemChanged += TreeViewControl_Typology_SelectedItemChanged;
        }
    }
}
