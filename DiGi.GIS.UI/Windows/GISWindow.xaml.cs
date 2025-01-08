using DiGi.GIS.Classes;
using System.Windows;

namespace DiGi.GIS.UI.Windows
{
    /// <summary>
    /// Interaction logic for GISWindow.xaml
    /// </summary>
    public partial class GISWindow : Window
    {
        GISModelFileManager gISModelFileManager = new GISModelFileManager();

        public GISWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MenuItem_Load_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_About_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
