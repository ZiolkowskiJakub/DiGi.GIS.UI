using DiGi.Core.Classes;
using DiGi.Core.IO.Table.Classes;
using DiGi.GIS.Classes;
using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace DiGi.GIS.UI
{
    public static partial class Modify
    {
        public static void AppendComparisonTable(Window owner)
        {
            bool? result;

            OpenFolderDialog openFolderDialog;

            openFolderDialog = new OpenFolderDialog();
            openFolderDialog.Title = "Select GIS Model Files directory";
            result = openFolderDialog.ShowDialog(owner);
            if (result == null || !result.HasValue || !result.Value)
            {
                return;
            }

            string directory_GISModelFiles = openFolderDialog.FolderName;
            if (string.IsNullOrWhiteSpace(directory_GISModelFiles) || !Directory.Exists(directory_GISModelFiles))
            {
                return;
            }


            openFolderDialog = new OpenFolderDialog();
            openFolderDialog.Title = "Select Statistical Data Directory";
            result = openFolderDialog.ShowDialog(owner);
            if (result == null || !result.HasValue || !result.Value)
            {
                return;
            }

            string directory_Statistical = openFolderDialog.FolderName;
            if (string.IsNullOrWhiteSpace(directory_Statistical) || !Directory.Exists(directory_Statistical))
            {
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            result = saveFileDialog.ShowDialog(owner);
            if (result == null || !result.HasValue || !result.Value)
            {
                return;
            }

            ComparisonTableConversionOptions comparisonTableConversionOptions = new ComparisonTableConversionOptions()
            {
                IncludeModel = true,
                IncludeStatistical = true,
                IncludeYearBuilt = true,
                YearBuiltOnly = true,
                IncludeOrtoDatasComparison = true,
                Years = new Range<int>(2008, DateTime.Now.Year),
                StatisticalDirectory = directory_Statistical
            };

            string[] paths_Input = Directory.GetFiles(directory_GISModelFiles, "*." + Constans.FileExtension.GISModelFile, SearchOption.AllDirectories);
            for (int i = 0; i < paths_Input.Length; i++)
            {
                string path_Input = paths_Input[i];

                Table table = null;
                using (GISModelFile gISModelFile = new GISModelFile(path_Input))
                {
                    gISModelFile.Open();

                    table = Convert.ToDiGi_Table(gISModelFile, comparisonTableConversionOptions: comparisonTableConversionOptions);
                }

                if(table != null)
                {
                    Core.IO.DelimitedData.Modify.Append(saveFileDialog.FileName, table, '\t');
                }
            }
        }
    }
}
