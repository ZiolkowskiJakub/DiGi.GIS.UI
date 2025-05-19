using DiGi.GIS.Classes;
using Microsoft.Win32;
using DiGi.GIS.Constans;
using System.IO;
using System.Windows;

namespace DiGi.GIS.UI
{
    public static partial class Modify
    {
        public static bool WriteAdministrativeAreal2DNames(Window owner, string path)
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

            Parallel.For(0, paths_Input.Length, i => 
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

            List<string> values = new List<string>();
            for(int i =0; i < administrativeAreal2Ds.Count; i++)
            {
                values.Add(string.Join("/t", i, administrativeAreal2Ds[i].Reference, administrativeAreal2Ds[i].Name));
            }

            File.WriteAllLines(path, values);

            return true;
        }
    }
}
