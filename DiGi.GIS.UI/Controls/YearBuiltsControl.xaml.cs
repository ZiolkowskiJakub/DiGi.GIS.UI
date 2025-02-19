using DiGi.GIS.Classes;
using DiGi.GIS.UI.Classes;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace DiGi.GIS.UI.Controls
{
    /// <summary>
    /// Interaction logic for YearBuiltsControl.xaml
    /// </summary>
    public partial class YearBuiltsControl : UserControl
    {
        public event YearBuiltActivatedEventHandler YearBuiltActivated;

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

            foreach(Building2D building2D in building2Ds)
            {
                ListBox_Main.Items.Add(new ListBoxItem() { Content = building2D.Reference, Tag = building2D });
            }

            ListBox_Main.SelectionChanged += ListBox_Main_SelectionChanged;
        }

        private void ListBox_Main_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WrapPanel_Main.Children.Clear();

            Building2D building2D = Building2D;

            Building2DControl_Main.Building2D = building2D;

            OrtoDatas ortoDatas = GIS.Query.OrtoDatas(gISModelFile, building2D);
            if(ortoDatas == null)
            {
                return;
            }

            foreach(OrtoData ortoData in ortoDatas)
            {
                YearBuiltControl yearBuiltControl = new YearBuiltControl()
                {
                    Year = ortoData.DateTime.Year,
                    BitmapImage = ortoData.BitmapImage(),
                    Tag = ortoData,
                    Active = false,
                    Width = 300,
                    Height = 300,
                    Margin = new System.Windows.Thickness(5, 5, 5, 5),
                };

                yearBuiltControl.MouseDown += YearBuiltControl_MouseDown;

                WrapPanel_Main.Children.Add(yearBuiltControl);
            }

            Image_Main.Source = Create.BitmapImage(gISModelFile, building2D, DateTime.Now.Year);
        }

        private void YearBuiltControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            foreach(YearBuiltControl yearBuiltControl_Temp in WrapPanel_Main.Children)
            {
                yearBuiltControl_Temp.Active = false;
            }

            YearBuiltControl yearBuiltControl = sender as YearBuiltControl;
            if(yearBuiltControl == null)
            {
                return;
            }

            yearBuiltControl.Active = true;

            YearBuiltActivated?.Invoke(yearBuiltControl, new YearBuiltActivatedEventArgs(Building2D, yearBuiltControl.Year));
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
    }
}
