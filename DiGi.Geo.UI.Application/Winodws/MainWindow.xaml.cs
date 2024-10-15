using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DiGi.Geo.UI.Application.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Convert_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "zip files (*.zip)|*.zip|All files (*.*)|*.*";
            bool? result = openFileDialog.ShowDialog(this);
            if (result == null || !result.HasValue || !result.Value)
            {
                return;
            }

            string path = openFileDialog.FileName;
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
            {
                return;
            }

            await Modify.Write(path, new Core.Classes.Range<int>(2001, 2023));
        }
    }
}