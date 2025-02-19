using DiGi.GIS.Classes;
using System.Windows;

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

            YearBuiltsControl_Main.YearBuiltActivated += YearBuiltsControl_Main_YearBuiltActivated;
        }

        private void YearBuiltsControl_Main_YearBuiltActivated(object sender, Classes.YearBuiltActivatedEventArgs e)
        {
            int year = e.Year;
            Building2D building2D = e.Building2D;
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
                if(YearBuiltsControl_Main != null)
                {
                    YearBuiltsControl_Main.GISModelFile = value;
                }
            }
        }
    }
}
