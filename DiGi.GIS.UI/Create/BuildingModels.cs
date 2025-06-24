using DiGi.Analytical.Building.Classes;
using DiGi.GIS.Classes;
using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace DiGi.GIS.UI
{
    public static partial class Create
    {
        public static Dictionary<string, List<BuildingModel>> BuildingModels(this Window owner)
        {
            OpenFolderDialog openFolderDialog;
            bool? dialogResult;

            openFolderDialog = new OpenFolderDialog();
            openFolderDialog.Title = "Select GIS Model files folder";
            dialogResult = openFolderDialog.ShowDialog(owner);
            if (dialogResult == null || !dialogResult.HasValue || !dialogResult.Value)
            {
                return null;
            }

            string directory = openFolderDialog.FolderName;
            if (string.IsNullOrWhiteSpace(directory) || !Directory.Exists(directory))
            {
                return null;
            }

            List<string> paths_GISModel = Directory.GetFiles(directory, "*." + Constans.FileExtension.GISModelFile, SearchOption.AllDirectories)?.ToList();
            if (paths_GISModel == null || paths_GISModel.Count == 0)
            {
                return null;
            }

            openFolderDialog = new OpenFolderDialog();
            openFolderDialog.Title = "Select CityGML files folder";
            dialogResult = openFolderDialog.ShowDialog(owner);
            if (dialogResult == null || !dialogResult.HasValue || !dialogResult.Value)
            {
                return null;
            }

            string directory_CityGML = openFolderDialog.FolderName;
            if (string.IsNullOrWhiteSpace(directory) || !Directory.Exists(directory))
            {
                return null;
            }

            Dictionary<string, List<BuildingModel>> result = new Dictionary<string, List<BuildingModel>>();
            foreach (string path_GISModel in paths_GISModel)
            {              
                using (GISModelFile gISModelFile = new GISModelFile(path_GISModel))
                {
                    gISModelFile.Open();

                    List<BuildingModel> buildingModels = Analytical.Create.BuildingModels(gISModelFile, directory_CityGML);
                    if(buildingModels != null)
                    {
                        result[path_GISModel] = buildingModels;
                    }
                }
            }

            return result;
        }
    }
}
