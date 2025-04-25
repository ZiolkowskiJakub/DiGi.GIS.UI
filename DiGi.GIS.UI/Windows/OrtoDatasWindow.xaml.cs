using DiGi.GIS.Classes;
using System.Windows;

namespace DiGi.GIS.UI.Windows
{
    /// <summary>
    /// Interaction logic for OrtoDatasWindow.xaml
    /// </summary>
    public partial class OrtoDatasWindow : Window
    {
        public OrtoDatasWindow()
        {
            InitializeComponent();

            OrtoDatasListControl_Main.OrtoDataSelectionChanged += OrtoDatasListControl_Main_OrtoDataSelectionChanged;
        }

        private void OrtoDatasListControl_Main_OrtoDataSelectionChanged(object sender, DiGi.UI.WPF.Core.Classes.OrtoDataSelectionChangedEventArgs e)
        {

        }

        private void MenuItem_About_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MenuItem_Load_Click(object sender, RoutedEventArgs e)
        {
            string path = DiGi.UI.WPF.Core.Query.Path(this, Constans.FileFilter.OrtoDatasFile);
            if(string.IsNullOrWhiteSpace(path) || !System.IO.File.Exists(path))
            {
                return;
            }

            List<OrtoDatas> ortoDatasList = null;
            using (OrtoDatasFile ortoDatasFile = new OrtoDatasFile(path))
            {
                ortoDatasFile.Open();

                ortoDatasList = ortoDatasFile.Values?.ToList();
            }

            OrtoDatasListControl_Main.OrtoDatasList = ortoDatasList;
        }
        private void MenuItem_New_Click(object sender, RoutedEventArgs e)
        {
            OrtoDatasListControl_Main.OrtoDatasList = null;
        }
    }
}
