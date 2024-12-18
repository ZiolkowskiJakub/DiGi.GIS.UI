using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DiGi.GIS.UI.Windows
{
    /// <summary>
    /// Interaction logic for GISWindow.xaml
    /// </summary>
    public partial class GISWindow : Window
    {
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
