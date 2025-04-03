using System.IO;
using DiGi.Core.Classes;
using DiGi.GIS.Emgu.CV.Classes;
using Microsoft.Win32;
using DiGi.Core.IO.Table.Classes;
using System.Windows;
using DiGi.GIS.Classes;

namespace DiGi.GIS.UI
{
    public static partial class Modify
    {
        public static void AppendTable(Window owner)
        {
            bool? result;

            OpenFolderDialog openFolderDialog = new OpenFolderDialog();
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

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            result = saveFileDialog.ShowDialog(owner);
            if (result == null || !result.HasValue || !result.Value)
            {
                return;
            }

            string[] paths_Input = Directory.GetFiles(directory, "*." + Constans.FileExtension.GISModelFile, SearchOption.AllDirectories);
            for (int i = 0; i < paths_Input.Length; i++)
            {
                string path_Input = paths_Input[i];

                Table table = null;
                using (GISModelFile gISModelFile = new GISModelFile(path_Input))
                {
                    gISModelFile.Open();

                    table = Convert.ToDiGi_Table(gISModelFile);
                }

                if(table != null)
                {
                    Core.IO.DelimitedData.Modify.Append(saveFileDialog.FileName, table, '\t');
                }
            }
        }
    }
}
