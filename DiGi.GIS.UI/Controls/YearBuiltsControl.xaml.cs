using DiGi.GIS.Classes;
using System.Windows.Controls;
using System.Windows.Input;

namespace DiGi.GIS.UI.Controls
{
    /// <summary>
    /// Interaction logic for YearBuiltsControl.xaml
    /// </summary>
    public partial class YearBuiltsControl : UserControl
    {
        private readonly DiGi.UI.WPF.Core.Classes.Debouncer debouncer = new(2000);

        private GISModelFile? gISModelFile;

        public YearBuiltsControl()
        {
            InitializeComponent();
        }

        public Building2D? Building2D
        {
            get
            {
                if (ListBox_Main.SelectedValue is not ListBoxItem listBoxItem)
                {
                    return null;
                }

                return listBoxItem.Tag as Building2D;
            }
        }

        public GISModelFile? GISModelFile
        {
            get
            {
                return gISModelFile;
            }

            set
            {
                gISModelFile = value;
                LoadGISModelFile();
            }
        }

        private void Button_Next_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            int count = ListBox_Main.Items.Count;
            if (count <= 0)
            {
                return;
            }

            int index = ListBox_Main.SelectedIndex;
            index++;

            if (index >= count)
            {
                return;
            }

            ListBox_Main.SelectedIndex = index;

            ListBox_Main.ScrollIntoView(ListBox_Main.SelectedItem);
        }

        private void ListBox_Main_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WrapPanel_Main.Children.Clear();

            Building2D? building2D = Building2D;

            Building2DControl_Main.Building2D = building2D;

            List<OrtoDataControl>? ortoDataControls = Create.OrtoDataControls(gISModelFile, building2D);
            if (ortoDataControls is null)
            {
                return;
            }

            foreach (OrtoDataControl ortoDataControl in ortoDataControls)
            {
                WrapPanel_Main.Children.Add(ortoDataControl);
                ortoDataControl.MouseDown += OrtoDataControl_MouseDown;
            }

            //WrapPanel_Main.Children.Clear();

            //Building2D? building2D = Building2D;

            //Building2DControl_Main.Building2D = building2D;

            //string? path = gISModelFile?.Path;
            //if(string.IsNullOrWhiteSpace(path))
            //{
            //    return;
            //}

            //string? directory = System.IO.Path.GetDirectoryName(path);
            //if(!System.IO.Directory.Exists(directory))
            //{
            //    return;
            //}

            //directory = GIS.Query.OrtoDatasDirectory_Building2D(directory);

            //OrtoDatas? ortoDatas = GIS.Query.OrtoDatas(building2D, directory);
            //if(ortoDatas == null || !ortoDatas.Any())
            //{
            //    return;
            //}

            //short? userYear = GIS.Query.UserYearBuilt(gISModelFile, building2D);

            //short? predictedYear = GIS.Query.LatestPredictedYearBuilt(gISModelFile, building2D);

            //SortedDictionary<short, OrtoDataControl> dictionary = [];

            //OrtoDataControl? ortoDataControl;

            //foreach (OrtoData ortoData in ortoDatas)
            //{
            //    short year_Temp = System.Convert.ToInt16(ortoData.DateTime.Year);

            //    ortoDataControl = new OrtoDataControl()
            //    {
            //        Year = year_Temp,
            //        BitmapImage = ortoData.BitmapImage(),
            //        Tag = ortoData,
            //        Active = year_Temp == userYear,
            //        Width = 300,
            //        Height = 300,
            //        Margin = new System.Windows.Thickness(5, 5, 5, 5),
            //    };

            //    ortoDataControl.MouseDown += OrtoDataControl_MouseDown;

            //    dictionary[year_Temp] = ortoDataControl;
            //}

            //short max = System.Convert.ToInt16(Math.Min(dictionary.Keys.Max() + 1, DateTime.Now.Year));

            //if (!dictionary.TryGetValue(max, out ortoDataControl) || ortoDataControl == null)
            //{
            //    ortoDataControl = new OrtoDataControl()
            //    {
            //        Year = max,
            //        BitmapImage = null,
            //        Tag = null,
            //        Active = max == userYear,
            //        Width = 300,
            //        Height = 300,
            //        Margin = new System.Windows.Thickness(5, 5, 5, 5),
            //    };

            //    ortoDataControl.MouseDown += OrtoDataControl_MouseDown;

            //    dictionary[max] = ortoDataControl;
            //}

            //ortoDataControl.BitmapImage = Create.BitmapImage(building2D, directory, DateTime.Now.Year);

            //foreach (OrtoDataControl ortoDataControl_Temp in dictionary.Values)
            //{
            //    WrapPanel_Main.Children.Add(ortoDataControl_Temp);
            //}

            //if(predictedYear != null && predictedYear.HasValue && dictionary.Count !=0)
            //{
            //    List<short> years = [.. dictionary.Keys];
            //    if(years.Count == 1)
            //    {
            //        dictionary.First().Value.PredictedYear = predictedYear.Value;
            //    }

            //    years.Sort();

            //    if(years.First() >= predictedYear.Value)
            //    {
            //        dictionary[years.First()].PredictedYear = predictedYear.Value;
            //    }
            //    else if (years.Last() <= predictedYear.Value)
            //    {
            //        dictionary[years.Last()].PredictedYear = predictedYear.Value;
            //    }
            //    else
            //    {
            //        for (int i = 0; i < years.Count - 1; i++)
            //        {
            //            if (years[i].Equals(predictedYear.Value))
            //            {
            //                dictionary[years[i]].PredictedYear = predictedYear.Value;
            //                break;
            //            }
            //            else if(years[i] < predictedYear.Value && years[i + 1] > predictedYear.Value)
            //            {
            //                if(predictedYear.Value - years[i] > years[i + 1] - predictedYear.Value)
            //                {
            //                    dictionary[years[i + 1]].PredictedYear = predictedYear.Value;
            //                }
            //                else
            //                {
            //                    dictionary[years[i]].PredictedYear = predictedYear.Value;
            //                }
            //                break;
            //            }
            //        }
            //    }
            //}
        }

        private void LoadGISModelFile()
        {
            ListBox_Main.Items.Clear();
            WrapPanel_Main.Children.Clear();

            ListBox_Main.SelectionChanged -= ListBox_Main_SelectionChanged;

            GISModel? gISModel = gISModelFile?.Value;
            if (gISModel == null)
            {
                return;
            }

            List<Building2D>? building2Ds = gISModel.GetObjects<Building2D>();
            if (building2Ds == null || building2Ds.Count == 0)
            {
                return;
            }

            string searchText = TextBox_Search.Text;

            for (int i = 0; i < building2Ds.Count; i++)
            {
                Building2D building2D = building2Ds[i];
                if (building2D?.Reference is not string reference)
                {
                    continue;
                }

                if (!string.IsNullOrEmpty(searchText) && !reference.Contains(searchText))
                {
                    continue;
                }

                ListBox_Main.Items.Add(new ListBoxItem() { Content = string.Format("[{0}] {1}", i + 1, reference), Tag = building2D });
            }

            ListBox_Main.SelectionChanged += ListBox_Main_SelectionChanged;
        }

        private void OrtoDataControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OrtoDataControl? ortoDataControl = sender as OrtoDataControl;

            bool active = ortoDataControl != null && ortoDataControl.Active;

            foreach (OrtoDataControl ortoDataControl_Temp in WrapPanel_Main.Children)
            {
                ortoDataControl_Temp.Active = false;
            }

            if (ortoDataControl == null)
            {
                return;
            }

            short? year = active ? null : ortoDataControl.Year;
            ortoDataControl.Active = !active;

            GIS.Modify.UpdateUserYearBuilt(gISModelFile, Building2D, year);

            //ListBox_Main.Focus();
            e.Handled = true;
        }

        private void TextBox_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            debouncer.Run(LoadGISModelFile);
        }
    }
}