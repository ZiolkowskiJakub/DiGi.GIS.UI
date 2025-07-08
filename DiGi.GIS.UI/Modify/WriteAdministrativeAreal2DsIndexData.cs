using DiGi.GIS.Classes;
using DiGi.GIS.Constans;
using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace DiGi.GIS.UI
{
    public static partial class Modify
    {
        public static bool WriteAdministrativeAreal2DsIndexData(Window owner, string path)
        {
            OpenFolderDialog openFolderDialog = new OpenFolderDialog();
            bool? result = openFolderDialog.ShowDialog(owner);
            if (result == null || !result.HasValue || !result.Value)
            {
                return false;
            }

            string directory = openFolderDialog.FolderName;
            if (string.IsNullOrWhiteSpace(directory) || !Directory.Exists(directory))
            {
                return false;
            }

            string[] paths_Input = Directory.GetFiles(directory, "*." + FileExtension.GISModelFile, SearchOption.AllDirectories);
            Dictionary<string, List<AdministrativeAreal2D>> dictionary = new Dictionary<string, List<AdministrativeAreal2D>>();
            foreach (string path_Input in paths_Input)
            {
                dictionary[path_Input] = null;
            }

            ParallelOptions parallelOptions = Core.Create.ParallelOptions(0.5);

            //ParallelOptions parallelOptions = new ParallelOptions()
            //{
            //    MaxDegreeOfParallelism = GIS.Query.DefaultProcessorCount(0.5)
            //};

            Parallel.For(0, paths_Input.Length, parallelOptions, i => 
            {
                string path_Input = paths_Input[i];

                using (GISModelFile gISModelFile = new GISModelFile(path_Input))
                {
                    gISModelFile.Open();

                    GISModel gISModel = gISModelFile.Value;
                    if (gISModel != null)
                    {
                        dictionary[path_Input] = gISModel.GetObjects<AdministrativeAreal2D>();
                    }
                }
            });

            List<AdministrativeAreal2D> administrativeAreal2Ds = new List<AdministrativeAreal2D>();
            foreach(List<AdministrativeAreal2D> administrativeAreal2Ds_Temp in dictionary.Values)
            {
                administrativeAreal2Ds.AddRange(administrativeAreal2Ds_Temp);
            }

            administrativeAreal2Ds.Sort((x, y) => x.Name.CompareTo(y.Name));

            IndexDataFile indexDataFile = GIS.Create.IndexDataFile(administrativeAreal2Ds);
            if(indexDataFile == null)
            {
                return false;
            }


            return indexDataFile.Write(path);
        }
    }
}
