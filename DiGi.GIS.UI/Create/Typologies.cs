using DiGi.GIS.Classes;
using DiGi.GIS.Constants;
using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace DiGi.GIS.UI
{
    public static partial class Create
    {
        /// <summary>
        /// Prompts the user to select a folder containing GIS model files and retrieves the associated typologies.
        /// </summary>
        /// <param name="owner">The owner window for the folder selection dialog.</param>
        /// <param name="directory">When this method returns, contains the path of the selected directory, or <see langword="null"/> if no directory was selected.</param>
        /// <returns>A list of <see cref="Typology.Classes.Typology"/> objects found in the selected folder, or <see langword="null"/> if the operation was cancelled or failed.</returns>
        public static List<Typology.Classes.Typology>? Typologies(this Window? owner, out string? directory)
        {
            directory = null;

            OpenFolderDialog openFolderDialog;
            bool? dialogResult;

            openFolderDialog = new()
            {
                Title = "Select GIS Model files folder"
            };
            dialogResult = openFolderDialog.ShowDialog(owner);
            if (dialogResult == null || !dialogResult.HasValue || !dialogResult.Value)
            {
                return null;
            }

            directory = openFolderDialog.FolderName;
            if (string.IsNullOrWhiteSpace(directory) || !Directory.Exists(directory))
            {
                return null;
            }

            //string directoryName = Path.GetFileName(directory);

            List<string>? paths_GISModel = Directory.GetFiles(directory, "*." + FileExtension.GISModelFile, SearchOption.AllDirectories)?.ToList();
            if (paths_GISModel == null || paths_GISModel.Count == 0)
            {
                return null;
            }

            List<Typology.Classes.Typology> result = [];

            foreach (string path_GISModel in paths_GISModel)
            {
                if (string.IsNullOrWhiteSpace(path_GISModel) || !File.Exists(path_GISModel))
                {
                    continue;
                }

                using GISModelFile gISModelFile = new(path_GISModel);

                gISModelFile.Open();

                if (gISModelFile.Value is not GISModel gISModel)
                {
                    return null;
                }

                string fileName = Path.GetFileNameWithoutExtension(path_GISModel);

                Typology.Classes.Typology? typology = null;

                typology = Typology_Residential_1(gISModel, fileName);
                if (typology is not null)
                {
                    result.Add(typology);
                }

                typology = Typology_Residential_2(gISModel, fileName);
                if (typology is not null)
                {
                    result.Add(typology);
                }

                typology = Typology_Residential_3(gISModel, fileName);
                if (typology is not null)
                {
                    result.Add(typology);
                }

                typology = Typology_Residential_4(gISModel, fileName);
                if (typology is not null)
                {
                    result.Add(typology);
                }

                typology = Typology_Residential_5(gISModel, fileName);
                if (typology is not null)
                {
                    result.Add(typology);
                }

                typology = Typology_NonResidential_1(gISModel, fileName);
                if (typology is not null)
                {
                    result.Add(typology);
                }

                typology = Typology_NonResidential_2(gISModel, fileName);
                if (typology is not null)
                {
                    result.Add(typology);
                }

                typology = Typology_NonResidential_3(gISModel, fileName);
                if (typology is not null)
                {
                    result.Add(typology);
                }

                typology = Typology_Residential_YearBuiltData_1(gISModelFile, fileName);
                if (typology is not null)
                {
                    result.Add(typology);
                }

                typology = Typology_Residential_YearBuiltData_2(gISModelFile, fileName);
                if (typology is not null)
                {
                    result.Add(typology);
                }

                typology = Typology_Residential_YearBuiltData_3(gISModelFile, fileName);
                if (typology is not null)
                {
                    result.Add(typology);
                }

                typology = Typology_Residential_YearBuiltData_4(gISModelFile, fileName);
                if (typology is not null)
                {
                    result.Add(typology);
                }

                typology = Typology_Residential_YearBuiltData_5(gISModelFile, fileName);
                if (typology is not null)
                {
                    result.Add(typology);
                }

                typology = Typology_NonResidential_YearBuiltData_1(gISModelFile, fileName);
                if (typology is not null)
                {
                    result.Add(typology);
                }

                typology = Typology_NonResidential_YearBuiltData_2(gISModelFile, fileName);
                if (typology is not null)
                {
                    result.Add(typology);
                }

                typology = Typology_NonResidential_YearBuiltData_3(gISModelFile, fileName);
                if (typology is not null)
                {
                    result.Add(typology);
                }
            }

            return result;
        }
    }
}