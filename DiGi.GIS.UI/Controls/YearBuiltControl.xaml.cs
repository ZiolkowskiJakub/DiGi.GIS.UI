using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace DiGi.GIS.UI.Controls
{
    /// <summary>
    /// Interaction logic for YearBuiltControl.xaml
    /// </summary>
    public partial class YearBuiltControl : UserControl
    {
        public YearBuiltControl()
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
    }
}
