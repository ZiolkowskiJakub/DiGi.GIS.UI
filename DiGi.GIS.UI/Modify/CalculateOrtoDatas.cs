using DiGi.Core.Classes;
using DiGi.GIS.Classes;
using DiGi.GIS.Constans;
using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace DiGi.GIS.UI
{
    public static partial class Modify
    {
        public static async Task<bool> CalculateOrtoDatas(Window owner, OrtoDatasOptions ortoDatasOptions, int count)
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

            if (ortoDatasOptions == null)
            {
                ortoDatasOptions = new OrtoDatasOptions();
            }

            string[] paths_Input = Directory.GetFiles(directory, "*." + FileExtension.GISModelFile, SearchOption.AllDirectories);
            for (int i = 0; i < paths_Input.Length; i++)
            {
                string path_Input = paths_Input[i];

                using (GISModelFile gISModelFile = new GISModelFile(path_Input))
                {
                    gISModelFile.Open();

                    GISModel gISModel = gISModelFile.Value;
                    if (gISModel != null)
                    {
                        List<Building2D> building2Ds = gISModel.GetObjects<Building2D>();
                        if (building2Ds != null)
                        {
                            string path = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(path_Input), string.Format("{0}.{1}", System.IO.Path.GetFileNameWithoutExtension(path_Input), FileExtension.OrtoDatasFile));
                            
                            while (building2Ds.Count > 0)
                            {
                                int count_Temp = building2Ds.Count > count ? count : building2Ds.Count;

                                List<Building2D> building2Ds_Temp = building2Ds.GetRange(0, count_Temp);

                                HashSet<GuidReference> guidReferences = await building2Ds.GetRange(0, count_Temp).CalculateOrtoDatas(path, ortoDatasOptions);

                                building2Ds.RemoveRange(0, count_Temp);
                            }
                        }
                    }
                }
            };

            //MessageBox.Show("Finished!");

            return true;
        }
    }
}
