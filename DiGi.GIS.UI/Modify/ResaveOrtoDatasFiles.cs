using DiGi.GIS.Classes;
using DiGi.GIS.Constans;
using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace DiGi.GIS.UI
{
    public static partial class Modify
    {
        public static async void ResaveOrtoDatasFiles(Window owner)
        {
            OpenFolderDialog openFolderDialog = new OpenFolderDialog();
            bool? result = openFolderDialog.ShowDialog(owner);
            if (result == null || !result.HasValue || !result.Value)
            {
                return;
            }

            string directory = openFolderDialog.FolderName;
            if (string.IsNullOrWhiteSpace(directory) || !Directory.Exists(directory))
            {
                return;
            }

            int count = GIS.Query.DefaultProcessorCount();

            ParallelOptions parallelOptions = new ParallelOptions()
            {
                MaxDegreeOfParallelism = count
            };

            string[] paths_Input = Directory.GetFiles(directory, "*." + FileExtension.OrtoDatasFile, SearchOption.AllDirectories);
            //for (int i = 0; i < paths_Input.Length; i++)
            Parallel.For(0, paths_Input.Length, parallelOptions, i =>
            {
                string path_Input = paths_Input[i];

                IEnumerable<OrtoDatas> ortoDatas = null;

                using (OrtoDatasFile ortoDatasFile = new OrtoDatasFile(path_Input))
                {
                    ortoDatas = ortoDatasFile.Values;
                }

                if(ortoDatas == null || ortoDatas.Count() == 0)
                {
                    return;
                }

                File.Delete(path_Input);

                using (OrtoDatasFile ortoDatasFile = new OrtoDatasFile(path_Input))
                {
                    ortoDatasFile.Values = ortoDatas;
                    ortoDatasFile.Save();
                }
            });

            MessageBox.Show("Finished!");
        }
    }
}
