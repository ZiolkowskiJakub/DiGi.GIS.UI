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
        private GISModelFile gISModelFile;

        public YearBuiltsControl()
        {
            InitializeComponent();
        }

        private void LoadGISModelFile()
        {
            ListBox_Main.Items.Clear();
            WrapPanel_Main.Children.Clear();

            ListBox_Main.SelectionChanged -= ListBox_Main_SelectionChanged;

            GISModel gISModel = gISModelFile.Value;
            if(gISModel == null)
            {
                return;
            }

            List<Building2D> building2Ds = gISModel.GetObjects<Building2D>();
            if(building2Ds == null || building2Ds.Count == 0)
            {
                return;
            }

            for (int i = 0; i < building2Ds.Count; i++)
            {
                Building2D building2D = building2Ds[i];

                ListBox_Main.Items.Add(new ListBoxItem() { Content = string.Format("[{0}] {1}", i + 1, building2D.Reference), Tag = building2D });
            }

            ListBox_Main.SelectionChanged += ListBox_Main_SelectionChanged;
        }

        private void ListBox_Main_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WrapPanel_Main.Children.Clear();

            Building2D building2D = Building2D;

            Building2DControl_Main.Building2D = building2D;

            string path = gISModelFile.Path;
            if(string.IsNullOrWhiteSpace(path))
            {
                return;
            }

            string directory = System.IO.Path.GetDirectoryName(path);
            if(!System.IO.Directory.Exists(directory))
            {
                return;
            }

            OrtoDatas ortoDatas = GIS.Query.OrtoDatas(building2D, directory);
            if(ortoDatas == null || ortoDatas.Count() == 0)
            {
                return;
            }

            short? year = GIS.Query.YearBuilt(gISModelFile, building2D);

            SortedDictionary<short, YearBuiltControl> dictionary = new SortedDictionary<short, YearBuiltControl>();

            YearBuiltControl yearBuiltControl;

            foreach (OrtoData ortoData in ortoDatas)
            {
                short year_Temp = System.Convert.ToInt16(ortoData.DateTime.Year);

                yearBuiltControl = new YearBuiltControl()
                {
                    Year = year_Temp,
                    BitmapImage = ortoData.BitmapImage(),
                    Tag = ortoData,
                    Active = year_Temp == year,
                    Width = 300,
                    Height = 300,
                    Margin = new System.Windows.Thickness(5, 5, 5, 5),
                };

                yearBuiltControl.MouseDown += YearBuiltControl_MouseDown;

                dictionary[year_Temp] = yearBuiltControl;   
            }

            short max = System.Convert.ToInt16(Math.Min(dictionary.Keys.Max() + 1, DateTime.Now.Year));

            if (!dictionary.TryGetValue(max, out yearBuiltControl) || yearBuiltControl == null)
            {
                yearBuiltControl = new YearBuiltControl()
                {
                    Year = max,
                    BitmapImage = null,
                    Tag = null,
                    Active = max == year,
                    Width = 300,
                    Height = 300,
                    Margin = new System.Windows.Thickness(5, 5, 5, 5),
                };

                yearBuiltControl.MouseDown += YearBuiltControl_MouseDown;

                dictionary[max] = yearBuiltControl;
            }

            yearBuiltControl.BitmapImage = Create.BitmapImage(building2D, directory, DateTime.Now.Year);

            foreach (YearBuiltControl yearBuiltControl_Temp in dictionary.Values)
            {
                WrapPanel_Main.Children.Add(yearBuiltControl_Temp);
            }
        }

        private void YearBuiltControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            YearBuiltControl yearBuiltControl = sender as YearBuiltControl;

            bool active = yearBuiltControl == null ? false : yearBuiltControl.Active;

            foreach (YearBuiltControl yearBuiltControl_Temp in WrapPanel_Main.Children)
            {
                yearBuiltControl_Temp.Active = false;
            }

            if (yearBuiltControl == null)
            {
                return;
            }

            short? year = active ? null : yearBuiltControl.Year;
            yearBuiltControl.Active = !active;

            GIS.Modify.UpdateYearBuilt(gISModelFile, Building2D, year);

            //ListBox_Main.Focus();
            e.Handled = true;
        }

        public GISModelFile GISModelFile
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

        public Building2D Building2D
        {
            get
            {
                ListBoxItem listBoxItem = ListBox_Main.SelectedValue as ListBoxItem;
                if (listBoxItem == null)
                {
                    return null;
                }

                return listBoxItem.Tag as Building2D;
            }
        }

        private void Button_Next_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            int count = ListBox_Main.Items.Count;
            if(count <= 0)
            {
                return;
            }

            int index = ListBox_Main.SelectedIndex;
            index++;

            if(index >= count)
            {
                return;
            }

            ListBox_Main.SelectedIndex = index;

            ListBox_Main.ScrollIntoView(ListBox_Main.SelectedItem);
        }
    }
}
