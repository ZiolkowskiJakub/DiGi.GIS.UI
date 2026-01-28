using DiGi.GIS.Classes;
using System.Windows;

namespace DiGi.GIS.UI.Windows
{
    /// <summary>
    /// Interaction logic for Building2DWindow.xaml
    /// </summary>
    public partial class Building2DWindow : Window
    {
        public Building2DWindow()
        {
            InitializeComponent();
        }

        public Building2DWindow(Building2D building2D)
            : this()
        {
            InitializeComponent();

            Building2D = building2D;
        }

        public Building2D? Building2D
        {
            get
            {
                return Building2DControl_Main.Building2D;
            }

            set
            {
                Building2DControl_Main.Building2D = value;
            }
        }

        private void Button_OK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}