using DiGi.Analytical.Building.Classes;
using DiGi.GIS.Classes;
using System.Windows;

namespace DiGi.GIS.UI
{
    public static partial class Modify
    {
        /// <summary>
        /// Appends building models associated with the specified window owner.
        /// </summary>
        /// <param name="owner">The window that owns the operation.</param>
        /// <returns>A <see cref="HashSet{T}"/> containing the identifiers of the appended building models, or <c>null</c> if no models were found or created.</returns>
        public static HashSet<string>? AppendBuildingModels(this Window? owner)
        {
            Dictionary<string, List<BuildingModel>>? dictionary = Create.BuildingModels(owner);
            if (dictionary == null || dictionary.Count == 0)
            {
                return null;
            }

            HashSet<string> result = [];
            foreach (KeyValuePair<string, List<BuildingModel>> keyValuePair in dictionary)
            {
                string path = keyValuePair.Key;
                List<BuildingModel> buildingModels = keyValuePair.Value;

                if (string.IsNullOrWhiteSpace(path))
                {
                    continue;
                }

                if (buildingModels == null || buildingModels.Count == 0)
                {
                    continue;
                }

                if (System.IO.Path.GetDirectoryName(path) is not string directory)
                {
                    continue;
                }

                path = System.IO.Path.Combine(directory, System.IO.Path.GetFileNameWithoutExtension(path) + "." + Analytical.Constants.FileExtension.BuildingModelsFile);

                using (BuildingModelsFile buildingModelsFile = new(path))
                {
                    buildingModelsFile.Open();

                    foreach (BuildingModel buildingModel in buildingModels)
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