using DiGi.Core.Classes;
using DiGi.GIS.Classes;
using DiGi.GIS.Constans;
using DiGi.GIS.Emgu.CV.Classes;
using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace DiGi.GIS.UI
{
    public static partial class Modify
    {
        public static async void CalculateOrtoDatasComparisons(Window owner, OrtoDatasComparisonOptions ortoDatasComparisonOptions)
        {
            OpenFolderDialog openFolderDialog = new OpenFolderDialog();
            bool? result = openFolderDialog.ShowDialog(owner);
            if (result == null || !result.HasValue || !result.Value)
            {
                return;
            }

            string directory = openFolderDialog.FolderName;
            if (string.IsNullOrWhiteSpace(directory) || !Directory.Exists(directory))
            {
                return;
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
                            if(ortoDatasComparisonOptions == null)
                            {
                                ortoDatasComparisonOptions = new OrtoDatasComparisonOptions();
                            }

                            string path = string.Format("{0}.{1}", System.IO.Path.GetFileNameWithoutExtension(path_Input), Emgu.CV.Constans.FileExtension.OrtoDatasComparisonFile);

                            IEnumerable<GuidReference> guidReferences = Emgu.CV.Modify.CalculateOrtoDatasComparisons(gISModel, building2Ds, path, ortoDatasComparisonOptions);
                        }
                    }
                }
            };
        }
    }
}
