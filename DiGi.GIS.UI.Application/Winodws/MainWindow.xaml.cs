using DiGi.Core.Interfaces;
using DiGi.GIS.Classes;
using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace DiGi.GIS.UI.Application.Windows
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

        private void Button_Write_Click(object sender, RoutedEventArgs e)
        {
            bool? result;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "zip files (*.zip)|*.zip|All files (*.*)|*.*";
            result = openFileDialog.ShowDialog(this);
            if (result == null || !result.HasValue || !result.Value)
            {
                return;
            }

            string path = openFileDialog.FileName;
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
            {
                return;
            }

            OpenFolderDialog openFolderDialog = new OpenFolderDialog();
            result = openFolderDialog.ShowDialog(this);
            if (result == null || !result.HasValue || !result.Value)
            {
                return;
            }

            string directory = openFolderDialog.FolderName;
            if (string.IsNullOrWhiteSpace(directory) || !Directory.Exists(directory))
            {
                return;
            }

            SQLite.Modify.Extract(new SQLite.Classes.SQLiteExtractOptions(path, directory) { UpdateExisting = false });

            //Modify.Extract(new DirectoryExtractOptions(path, directory) { UpdateExisting = false });
        }

        private void Button_Read_Click(object sender, RoutedEventArgs e)
        {

            Read_SQLite();
        }

        private void Read_Files()
        {
            OpenFolderDialog openFolderDialog = new OpenFolderDialog();
            bool? result = openFolderDialog.ShowDialog(this);
            if (result == null || !result.HasValue || !result.Value)
            {
                return;
            }

            string directory = openFolderDialog.FolderName;
            if (string.IsNullOrWhiteSpace(directory) || !Directory.Exists(directory))
            {
                return;
            }

            List<Building2D> building2Ds = Create.Building2Ds(directory);
        }

        private void Read_SQLite()
        {
            OpenFolderDialog openFolderDialog = new OpenFolderDialog();
            bool? result = openFolderDialog.ShowDialog(this);
            if (result == null || !result.HasValue || !result.Value)
            {
                return;
            }

            string directory = openFolderDialog.FolderName;
            if (string.IsNullOrWhiteSpace(directory) || !Directory.Exists(directory))
            {
                return;
            }

            string[] paths = Directory.GetFiles(directory, "*.sqlite3", SearchOption.AllDirectories);
            foreach(string path in paths)
            {
                List<ISerializableObject> serializableObjects = SQLite.Convert.ToDiGi<ISerializableObject>(path);
                if(serializableObjects != null)
                {

                }
            }

            //List<Building2D> building2Ds = Create.Building2Ds(directory);
        }

        private void Reorganize()
        {
            OpenFolderDialog openFolderDialog = new OpenFolderDialog();
            bool? result = openFolderDialog.ShowDialog(this);
            if (result == null || !result.HasValue || !result.Value)
            {
                return;
            }

            string directory = openFolderDialog.FolderName;
            if (string.IsNullOrWhiteSpace(directory) || !Directory.Exists(directory))
            {
                return;
            }

            foreach(string directory_Temp in Directory.GetDirectories(directory))
            {
                string directory_Buildings = System.IO.Path.Combine(directory_Temp, "Buildings");
                if(!Directory.Exists(directory_Buildings))
                {
                    Directory.CreateDirectory(directory_Buildings);
                }

                string[] directories = Directory.GetDirectories(directory_Temp);

                foreach (string directory_Building in directories)
                {
                    string name = new DirectoryInfo(directory_Building).Name;

                    if (name == "Buildings")
                    {
                        continue;
                    }

                    Directory.Move(directory_Building, System.IO.Path.Combine(directory_Buildings, name));
                }
            }
        }

        private void Delete()
        {
            //OpenFolderDialog openFolderDialog = new OpenFolderDialog();
            //bool? result = openFolderDialog.ShowDialog(this);
            //if (result == null || !result.HasValue || !result.Value)
            //{
            //    return;
            //}

            //string directory = openFolderDialog.FolderName;
            string directory = @"C:\Users\jakub\Nextcloud\Data\GIS\Output";
            if (string.IsNullOrWhiteSpace(directory) || !Directory.Exists(directory))
            {
                return;
            }

            foreach (string directory_Temp in Directory.GetDirectories(directory))
            {
                Directory.Delete(directory_Temp, true);
            }
        }
    }
}