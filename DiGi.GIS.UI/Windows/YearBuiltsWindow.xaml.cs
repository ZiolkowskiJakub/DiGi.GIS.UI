using DiGi.GIS.Classes;
using System.Windows;
using System.Windows.Controls;

namespace DiGi.GIS.UI.Windows
{
    /// <summary>
    /// Interaction logic for YearBuiltsWindow.xaml
    /// </summary>
    public partial class YearBuiltsWindow : Window
    {
        public YearBuiltsWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Load_Click(object sender, RoutedEventArgs e)
        {
            string path = Query.GISModelFilePath(this);
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            GISModelFile gISModelFile = new GISModelFile(path);
            gISModelFile.Open();

            YearBuiltsControl_Main.GISModelFile = gISModelFile;
        }

        private void MenuItem_Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MenuItem_About_Click(object sender, RoutedEventArgs e)
        {

        }

        public GISModelFile GISModelFile
        {
            get
            {
                return YearBuiltsControl_Main?.GISModelFile;
            }

            set
            {
                if (YearBuiltsControl_Main != null)
                {
                    YearBuiltsControl_Main.GISModelFile = value;
                }
            }
        }
    }
}
