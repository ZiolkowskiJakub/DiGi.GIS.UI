using DiGi.GIS.Classes;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using DiGi.GIS.Constans;

namespace DiGi.GIS.UI
{
    public static partial class Modify
    {
        public static bool AppendBuilding2DYearBuiltPredictionsFile(this Window owner)
        {
            bool? dialogResult;

            OpenFolderDialog openFolderDialog;

            openFolderDialog = new OpenFolderDialog();
            openFolderDialog.Title = "Select source directory";
            dialogResult = openFolderDialog.ShowDialog(owner);
            if (dialogResult == null || !dialogResult.HasValue || !dialogResult.Value)
            {
                return false;
            }

            string directory_Input = openFolderDialog.FolderName;

            string[] paths_Input = Directory.GetFiles(directory_Input, string.Format("*.{0}", YOLO.Constans.FileExtension.BoundingBoxResultFile));
            if (paths_Input == null || paths_Input.Length == 0)
            {
                return false;
            }

            openFolderDialog = new OpenFolderDialog();
            openFolderDialog.Title = "Select destination directory";
            dialogResult = openFolderDialog.ShowDialog(owner);
            if (dialogResult == null || !dialogResult.HasValue || !dialogResult.Value)
            {
                return false;
            }

            string directory_Output = openFolderDialog.FolderName;

            string directoryName = Path.GetFileName(directory_Output);

            string path_Output = Path.Combine(directory_Output, string.Format("{0}.{1}", directoryName, FileExtension.Building2DYearBuiltPredictionsFile));

            bool result = false;

            using (Building2DYearBuiltPredictionsFile building2DYearBuiltPredictionsFile = new Building2DYearBuiltPredictionsFile(path_Output))
            {
                building2DYearBuiltPredictionsFile.Open();

                foreach(string path_Input in paths_Input)
                {
                    YOLO.Classes.BoundingBoxResultFile boundingBoxResultFile = YOLO.Create.BoundingBoxResultFile(path_Input);
                    if(boundingBoxResultFile == null)
                    {
                        continue;
                    }

                    building2DYearBuiltPredictionsFile.Append(boundingBoxResultFile);
                    result = true;
                }

                building2DYearBuiltPredictionsFile.Save();
            }

            return result;
        }
    }
}
