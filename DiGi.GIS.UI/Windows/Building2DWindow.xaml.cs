using DiGi.GIS.Classes;
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
