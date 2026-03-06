using DiGi.GIS.Constants;
using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace DiGi.GIS.UI
{
    public static partial class Convert
    {
        public static void ToDiGi_GISModelFiles(Window owner)
        {
            bool? result;

            OpenFileDialog openFileDialog = new()
            {
                Filter = "zip files (*.zip)|*.zip|All files (*.*)|*.*"
            };
            result = openFileDialog.ShowDialog(owner);
            if (result == null || !result.HasValue || !result.Value)
            {
                return;
            }

            string path = openFileDialog.FileName;
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
            {
                return;
            }

            OpenFolderDialog openFolderDialog = new();
            result = openFolderDialog.ShowDialog(owner);
            if (result == null || !result.HasValue || !result.Value)
            {
                return;
            }

            string directory = openFolderDialog.FolderName;
            if (string.IsNullOrWhiteSpace(directory) || !Directory.Exists(directory))
            {
                return;
            }

            DiGi.UI.WPF.Core.Windows.ListBoxWindow listBoxWindow = new DiGi.UI.WPF.Core.Windows.ListBoxWindow("File types");
            listBoxWindow.SelectionMode = System.Windows.Controls.SelectionMode.Multiple;
            listBoxWindow.SetItems([FileNameSufix.OT_ADJA_A, FileNameSufix.OT_ADMS_A, FileNameSufix.OT_BUBD_A]);

            bool? dialogResult = listBoxWindow.ShowDialog();
            if (dialogResult is null || !dialogResult.Value)
            {
                return;
            }

            List<string>? sufixes = listBoxWindow.GetItems<string>();
            if (sufixes is null || sufixes.Count == 0)
            {
                return;
            }

            GIS.Convert.ToDiGi(path, directory, sufixes.Contains(FileNameSufix.OT_ADJA_A), sufixes.Contains(FileNameSufix.OT_ADMS_A), sufixes.Contains(FileNameSufix.OT_BUBD_A));

            MessageBox.Show("Finished!");
        }
    }
}