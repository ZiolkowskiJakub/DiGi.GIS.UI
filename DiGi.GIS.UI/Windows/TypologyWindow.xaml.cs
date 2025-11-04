using DiGi.GIS.Classes;
using DiGi.Typology.Classes;
using DiGi.UI.WPF.Core.Classes;
using System.Windows;
using System.Windows.Controls;

namespace DiGi.GIS.UI.Windows
{
    /// <summary>
    /// Interaction logic for TypologyWindow.xaml
    /// </summary>
    public partial class TypologyWindow : Window
    {
        public TypologyWindow()
        {
            InitializeComponent();
        }

        private void ListBox_References_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ListBox_References.SelectedItem is not string reference)
            {
                return;
            }

            if(TreeViewControl_Typology.Tag is not TypologyFile typologyFile)
            {
                return;
            }

            if(typologyFile.Path is not string path_TypologyFile)
            {
                return;
            }

            string? directory = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(path_TypologyFile));
            if(string.IsNullOrWhiteSpace(directory) || !System.IO.Directory.Exists(directory))
            {
                return;
            }

            GISModelFile? gISModelFile = null;
            Building2D? building2D = null;

            List<string> paths_GISModelFile = System.IO.Directory.GetFiles(directory, string.Format(".{0}", Constans.FileExtension.GISModelFile), System.IO.SearchOption.TopDirectoryOnly).ToList();
            foreach(string path_GISModelFile in paths_GISModelFile)
            {
                gISModelFile = new(path_GISModelFile);
                if(!gISModelFile.Open())
                {
                    continue;
                }

                building2D = gISModelFile.Value?.GetObject<Building2D>(reference);
                if(building2D is not null)
                {
                    break;
                }

                gISModelFile = null;
            }

            if(building2D is null)
            {
                return;
            }

            string? path = gISModelFile?.Path;
            if (string.IsNullOrWhiteSpace(path))
            {
                return;
            }

            directory = System.IO.Path.GetDirectoryName(path);
            if (!System.IO.Directory.Exists(directory))
            {
                return;
            }

            directory = GIS.Query.OrtoDatasDirectory_Building2D(directory);

            OrtoDatas? ortoDatas = GIS.Query.OrtoDatas(building2D, directory);
            if (ortoDatas == null || !ortoDatas.Any())
            {
                return;
            }

            short? userYear = GIS.Query.UserYearBuilt(gISModelFile, building2D);

            short? predictedYear = GIS.Query.LatestPredictedYearBuilt(gISModelFile, building2D);
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
            if (TreeViewControl_Typology.Tag is not Typology.Classes.Typology typology)
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
