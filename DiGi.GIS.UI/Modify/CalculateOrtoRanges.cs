using DiGi.GIS.Classes;
using DiGi.GIS.Constans;
using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace DiGi.GIS.UI
{
    public static partial class Modify
    {
        public static async Task<bool> CalculateOrtoRanges(Window owner, OrtoRangeOptions ortoRangeOptions)
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

            if (ortoRangeOptions == null)
            {
                ortoRangeOptions = new OrtoRangeOptions();
            }

            string[] paths_Input = Directory.GetFiles(directory, "*." + FileExtension.GISModelFile, SearchOption.AllDirectories);
            for (int i = 0; i < paths_Input.Length; i++)
            {
                string path_Input = paths_Input[i];

                List<OrtoRange> ortoRanges = null;

                using (GISModelFile gISModelFile = new GISModelFile(path_Input))
                {
                    gISModelFile.Open();

                    GISModel gISModel = gISModelFile.Value;
                    if (gISModel != null)
                    {
                        ortoRanges = GIS.Create.OrtoRanges(gISModel, null, ortoRangeOptions);
                    }
                }

                string path_Output = Path.Combine(Path.GetDirectoryName(path_Input), string.Format("{0}.{1}", Path.GetFileNameWithoutExtension(path_Input), FileExtension.OrtoRangeFile));

                if(ortoRanges != null)
                {
                    using (OrtoRangeFile ortoRangeFile = new OrtoRangeFile(path_Output))
                    {
                        ortoRangeFile.Open();
                        ortoRangeFile.Values = ortoRanges;

                        ortoRangeFile.Save();
                    }
                        
                }
            };

            return true;
        }
    }
}
