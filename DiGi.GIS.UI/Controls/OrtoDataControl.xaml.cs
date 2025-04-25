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
                return GetYear();
            }

            set
            {
                SetYear(value);
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
        
        private bool SetYear(int year)
        {
            TextBlock_Main.Text = year > 0 ? year.ToString() : null;

            return !string.IsNullOrWhiteSpace(TextBlock_Main.Text);
        }

        private short GetYear()
        {
            string value = TextBlock_Main.Text;
            if(string.IsNullOrWhiteSpace(value))
            {
                return -1;
            }

            if(!short.TryParse(value, out short result))
            {
                return -1;
            }

            return result;

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
