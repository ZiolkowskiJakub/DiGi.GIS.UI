using DiGi.GIS.Classes;
using DiGi.GIS.Constans;
using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace DiGi.GIS.UI
{
    public static partial class Modify
    {
        public static bool AppendBuilding2DYearBuiltPredictionsFile(this Window owner)
        {
            bool? dialogResult;

            OpenFolderDialog openFolderDialog;

            openFolderDialog = new OpenFolderDialog();
            openFolderDialog.Title = "Select Bounding Box Result Files directory";
            dialogResult = openFolderDialog.ShowDialog(owner);
            if (dialogResult == null || !dialogResult.HasValue || !dialogResult.Value)
            {
                return false;
            }

            string directory_BoundingBoxResultFile = openFolderDialog.FolderName;

            string[] paths_BoundingBoxResultFile = Directory.GetFiles(directory_BoundingBoxResultFile, string.Format("*.{0}", YOLO.Constans.FileExtension.BoundingBoxResultFile), SearchOption.AllDirectories);
            if (paths_BoundingBoxResultFile == null || paths_BoundingBoxResultFile.Length == 0)
            {
                return false;
            }

            openFolderDialog = new OpenFolderDialog();
            openFolderDialog.Title = "Select GIS Model Files directory";
            dialogResult = openFolderDialog.ShowDialog(owner);
            if (dialogResult == null || !dialogResult.HasValue || !dialogResult.Value)
            {
                return false;
            }

            string directory_GISModelFile = openFolderDialog.FolderName;

            string[] paths_GISModelFile = Directory.GetFiles(directory_GISModelFile, string.Format("*.{0}", FileExtension.GISModelFile), SearchOption.AllDirectories);
            if (paths_GISModelFile == null || paths_GISModelFile.Length == 0)
            {
                return false;
            }

            bool result = false;

            string path_Building2DYearBuiltPredictionsFile_Temp = Path.Combine(directory_GISModelFile, string.Format("{0}.{1}", "TEMP", FileExtension.Building2DYearBuiltPredictionsFile));

            using (Building2DYearBuiltPredictionsFile building2DYearBuiltPredictionsFile_Temp = new Building2DYearBuiltPredictionsFile(path_Building2DYearBuiltPredictionsFile_Temp))
            {
                building2DYearBuiltPredictionsFile_Temp.Open();

                foreach (string path_Input in paths_BoundingBoxResultFile)
                {
                    YOLO.Classes.BoundingBoxResultFile boundingBoxResultFile = YOLO.Create.BoundingBoxResultFile(path_Input);
                    if (boundingBoxResultFile == null)
                    {
                        continue;
                    }

                    building2DYearBuiltPredictionsFile_Temp.Append(boundingBoxResultFile);
                }

                //foreach (string path_GISModelFile in paths_GISModelFile)
                Parallel.ForEach(paths_GISModelFile, Core.Create.ParallelOptions(), path_GISModelFile => 
                {
                    List<Building2DYearBuiltPredictions> building2DYearBuiltPredictionsList = new List<Building2DYearBuiltPredictions>();
                    using (GISModelFile gISModelFile = new GISModelFile(path_GISModelFile))
                    {
                        gISModelFile.Open();

                        HashSet<string> references = gISModelFile?.Value?.GetReferences<Building2D>();
                        if (references == null || references.Count == 0)
                        {
                            return;
                        }

                        foreach (string reference in references)
                        {
                            Building2DYearBuiltPredictions building2DYearBuiltPredictions = building2DYearBuiltPredictionsFile_Temp.GetValue<Building2DYearBuiltPredictions>(Building2DYearBuiltPredictionsFile.GetUniqueReference(reference));
                            if (building2DYearBuiltPredictions == null)
                            {
                                continue;
                            }

                            building2DYearBuiltPredictionsList.Add(building2DYearBuiltPredictions);

                        }
                    }

                    if (building2DYearBuiltPredictionsList == null || building2DYearBuiltPredictionsList.Count == 0)
                    {
                        return;
                    }

                    string path_Building2DYearBuiltPredictionsFile = Path.Combine(Path.GetDirectoryName(path_GISModelFile), string.Format("{0}.{1}", Path.GetFileNameWithoutExtension(path_GISModelFile), FileExtension.Building2DYearBuiltPredictionsFile));
                    using (Building2DYearBuiltPredictionsFile building2DYearBuiltPredictionsFile = new Building2DYearBuiltPredictionsFile(path_Building2DYearBuiltPredictionsFile))
                    {
                        building2DYearBuiltPredictionsFile.Open();

                        foreach (Building2DYearBuiltPredictions building2DYearBuiltPredictions in building2DYearBuiltPredictionsList)
                        {
                            building2DYearBuiltPredictionsFile.AddValue(building2DYearBuiltPredictions);
                        }

                        building2DYearBuiltPredictionsFile.Save();
                    }

                    result = true;
                });
            }

            return result;
        }

        //public static bool AppendBuilding2DYearBuiltPredictionsFile(this Window owner)
        //{
        //    bool? dialogResult;

        //    OpenFolderDialog openFolderDialog;

        //    openFolderDialog = new OpenFolderDialog();
        //    openFolderDialog.Title = "Select source directory";
        //    dialogResult = openFolderDialog.ShowDialog(owner);
        //    if (dialogResult == null || !dialogResult.HasValue || !dialogResult.Value)
        //    {
        //        return false;
        //    }

        //    string directory_Input = openFolderDialog.FolderName;

        //    string[] paths_Input = Directory.GetFiles(directory_Input, string.Format("*.{0}", YOLO.Constans.FileExtension.BoundingBoxResultFile));
        //    if (paths_Input == null || paths_Input.Length == 0)
        //    {
        //        return false;
        //    }

        //    openFolderDialog = new OpenFolderDialog();
        //    openFolderDialog.Title = "Select destination directory";
        //    dialogResult = openFolderDialog.ShowDialog(owner);
        //    if (dialogResult == null || !dialogResult.HasValue || !dialogResult.Value)
        //    {
        //        return false;
        //    }

        //    string directory_Output = openFolderDialog.FolderName;

        //    string directoryName = Path.GetFileName(directory_Output);

        //    string path_Output = Path.Combine(directory_Output, string.Format("{0}.{1}", directoryName, FileExtension.Building2DYearBuiltPredictionsFile));

        //    bool result = false;

        //    using (Building2DYearBuiltPredictionsFile building2DYearBuiltPredictionsFile = new Building2DYearBuiltPredictionsFile(path_Output))
        //    {
        //        building2DYearBuiltPredictionsFile.Open();

        //        foreach(string path_Input in paths_Input)
        //        {
        //            YOLO.Classes.BoundingBoxResultFile boundingBoxResultFile = YOLO.Create.BoundingBoxResultFile(path_Input);
        //            if(boundingBoxResultFile == null)
        //            {
        //                continue;
        //            }

        //            building2DYearBuiltPredictionsFile.Append(boundingBoxResultFile);
        //            result = true;
        //        }

        //        building2DYearBuiltPredictionsFile.Save();
        //    }

        //    return result;
        //}
    }
}
