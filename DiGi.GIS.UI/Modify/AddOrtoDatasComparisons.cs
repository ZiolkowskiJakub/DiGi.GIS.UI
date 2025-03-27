using System.IO;
using DiGi.Core.Classes;
using DiGi.GIS.Emgu.CV.Classes;
using Microsoft.Win32;
using DiGi.Core.IO.Table.Classes;
using System.Windows;

namespace DiGi.GIS.UI
{
    public static partial class Modify
    {
        public static void AddOrtoDatasComparisons(this Table table, Window owner)
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

            string[] paths_Input = Directory.GetFiles(directory, "*." + Emgu.CV.Constans.FileExtension.OrtoDatasComparisonFile, SearchOption.AllDirectories);
            for (int i = 0; i < paths_Input.Length; i++)
            {
                string path_Input = paths_Input[i];

                using (OrtoDatasComparisonFile ortoDatasComparisonFile = new OrtoDatasComparisonFile(path_Input))
                {
                    ortoDatasComparisonFile.Open();

                    HashSet<UniqueReference> uniqueReferences = ortoDatasComparisonFile.GetUniqueReferences();
                    if (uniqueReferences == null)
                    {
                        continue;
                    }

                    IEnumerable<OrtoDatasComparison> ortoDatasComparisons = ortoDatasComparisonFile.GetValues<OrtoDatasComparison>(uniqueReferences);
                    if (ortoDatasComparisons == null)
                    {
                        continue;
                    }

                    Table table = Emgu.CV.Convert.ToDiGi_Table(ortoDatasComparisons);

                    Core.IO.DelimitedData.Modify.Write(table, System.IO.Path.Combine(System.IO.Path.GetDirectoryName(path_Input), "OrtoDatasComparison.txt"), '\t');
                }
            }
            ;

        }
    }
}
