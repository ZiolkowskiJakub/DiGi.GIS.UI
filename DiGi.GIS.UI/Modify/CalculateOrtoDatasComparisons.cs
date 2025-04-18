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
        public static async Task<HashSet<string>> CalculateOrtoDatasComparisons(Window owner, OrtoDatasComparisonOptions ortoDatasComparisonOptions, IEnumerable<string> references = null ,int? count = null)
        {
            OpenFolderDialog openFolderDialog = new OpenFolderDialog();
            bool? result = openFolderDialog.ShowDialog(owner);
            if (result == null || !result.HasValue || !result.Value)
            {
                return null;
            }

            string directory = openFolderDialog.FolderName;
            if (string.IsNullOrWhiteSpace(directory) || !Directory.Exists(directory))
            {
                return null;
            }

            return await CalculateOrtoDatasComparisons(directory, ortoDatasComparisonOptions, references, count);
        }

        public static async Task<HashSet<string>> CalculateOrtoDatasComparisons(string directory, OrtoDatasComparisonOptions ortoDatasComparisonOptions, IEnumerable<string> references = null, int? count = null)
        {
            if(string.IsNullOrWhiteSpace(directory) || !Directory.Exists(directory))
            {
                return null;
            }

            HashSet<string> references_Temp = references == null ? null : new HashSet<string>(references);

            int count_Temp = count == null || !count.HasValue ? GIS.Query.DefaultProcessorCount() : count.Value;

            HashSet<string> result = new HashSet<string>();

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
                            if (references_Temp != null)
                            {
                                building2Ds = building2Ds.FindAll(x => references_Temp.Contains(x.Reference));
                            }

                            if (ortoDatasComparisonOptions == null)
                            {
                                ortoDatasComparisonOptions = new OrtoDatasComparisonOptions();
                            }

                            string path = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(path_Input), string.Format("{0}.{1}", System.IO.Path.GetFileNameWithoutExtension(path_Input), Emgu.CV.Constans.FileExtension.OrtoDatasComparisonFile));

                            while (building2Ds.Count > 0)
                            {
                                int count_Min = Math.Min(building2Ds.Count, count_Temp);

                                List<Building2D> building2Ds_Temp = building2Ds.GetRange(0, count_Min);

                                IEnumerable<GuidReference> guidReferences = Emgu.CV.Modify.CalculateOrtoDatasComparisons(gISModel, building2Ds_Temp, path, ortoDatasComparisonOptions);
                                if(guidReferences != null)
                                {
                                    foreach(GuidReference guidReference in guidReferences)
                                    {
                                        Building2D building2D = building2Ds_Temp.Find(x => new GuidReference(x) == guidReference);
                                        if(building2D != null)
                                        {
                                            result.Add(building2D.Reference);
                                        }

                                        
                                    }
                                }

                                building2Ds.RemoveRange(0, count_Min);
                            }
                        }
                    }
                }
            }
            return result;
        }
    }
}
