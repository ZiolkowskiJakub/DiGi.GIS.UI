using DiGi.Analytical.Building.Classes;
using DiGi.GIS.Classes;
using System.Windows;

namespace DiGi.GIS.UI
{
    public static partial class Modify
    {
        public static HashSet<string> AppendBuildingModels(this Window owner)
        {
            Dictionary<string, List<BuildingModel>> dictionary = Create.BuildingModels(owner);
            if(dictionary == null || dictionary.Count == 0)
            {
                return null;
            }


            HashSet<string> result = new HashSet<string>();
            foreach (KeyValuePair<string, List<BuildingModel>> keyValuePair in dictionary)
            {
                string path = keyValuePair.Key;
                List<BuildingModel> buildingModels = keyValuePair.Value;

                if(string.IsNullOrWhiteSpace(path))
                {
                    continue;
                }

                if(buildingModels == null || buildingModels.Count == 0)
                {
                    continue;
                }

                path = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(path), System.IO.Path.GetFileNameWithoutExtension(path) + "." + Analytical.Constans.FileExtension.BuildingModelsFile);

                using (BuildingModelsFile buildingModelsFile = new BuildingModelsFile(path))
                {
                    buildingModelsFile.Open();

                    foreach(BuildingModel buildingModel in buildingModels)
                    {
                        buildingModelsFile.AddValue(buildingModel);
                    }

                    buildingModelsFile.Save();
                }

                result.Add(path);
            }

            return result;
        }
    }
}
