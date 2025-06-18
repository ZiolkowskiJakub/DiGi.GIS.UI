using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace DiGi.GIS.UI.Controls
{
    /// <summary>
    /// Interaction logic for OrtoDataControl.xaml
    /// </summary>
    public partial class OrtoDataControl : UserControl
    {
        private short year;
        private short? predictedYear;
        
        public OrtoDataControl()
        {
            InitializeComponent();
        }

        public bool Active
        {
            get
            {
                return Border_Main.BorderThickness.Top != 0;
            }

            set
            {
                Border_Main.BorderThickness = new Thickness(value ? 2 : 0);
            }
        }

        public short Year
        {
            get
            {
                return year;
            }

            set
            {
                year = value;
                SetText();
            }
        }

        public short? PredictedYear
        {
            get
            {
                return predictedYear;
            }
            set
            {
                predictedYear = value;
                SetText();
            }
        }

        public BitmapImage BitmapImage
        {
            get
            {
                return GetBitmapImage();
            }

            set
            {
                SetBitmapImage(value);
            }
        }

        private bool SetBitmapImage(BitmapImage bitmapImage)
        {
            Image_Main.Source = bitmapImage;
            return true;
        }

        private BitmapImage GetBitmapImage()
        {
            return Image_Main.Source as BitmapImage;
        }

        private void SetText()
        {
            if(predictedYear == null || !predictedYear.HasValue)
            {
                TextBlock_Main.Text = year.ToString();
                TextBlock_Main.FontWeight = FontWeights.Normal;
            }
            else 
            {
                TextBlock_Main.Text = year.Equals(predictedYear.Value) ? year.ToString() : string.Format("{0} ({1})", year, predictedYear);
                TextBlock_Main.FontWeight = FontWeights.Bold;
            }
        }

        private void UserControl_PreviewMouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            FrameworkElement frameworkElement = sender as FrameworkElement;

            ContextMenu contextMenu = new ContextMenu();

            MenuItem menuItem = new MenuItem { Header = "Save As..." };
            menuItem.Click += (s, args) => 
            {
                DiGi.UI.WPF.Core.Modify.Write(GetBitmapImage());
            };
           
            contextMenu.Items.Add(menuItem);

            frameworkElement.ContextMenu = contextMenu;
            contextMenu.PlacementTarget = frameworkElement;
            contextMenu.IsOpen = true;

            e.Handled = true;
        }
    }
}
