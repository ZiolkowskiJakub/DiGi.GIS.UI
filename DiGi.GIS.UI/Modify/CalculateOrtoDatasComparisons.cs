using DiGi.Core.Classes;
using DiGi.GIS.Classes;
using DiGi.GIS.Constants;
using DiGi.GIS.Emgu.CV.Classes;
using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace DiGi.GIS.UI
{
    public static partial class Modify
    {
        /// <summary>
        /// Calculates comparisons for orthodata based on a user-selected folder and specified options.
        /// </summary>
        /// <param name="owner">The owner window used to display the folder selection dialog.</param>
        /// <param name="ortoDatasComparisonOptions">The options used to configure the orthodata comparison process.</param>
        /// <param name="references">An optional collection of reference strings to be used in the comparison.</param>
        /// <param name="count">An optional integer specifying a count limit for the calculation.</param>
        /// <returns>A <see cref="HashSet{T}"/> containing the results of the orthodata comparisons, or <c>null</c> if the operation was cancelled by the user.</returns>
        public static HashSet<string>? CalculateOrtoDatasComparisons(Window owner, OrtoDatasComparisonOptions? ortoDatasComparisonOptions, IEnumerable<string>? references = null, int? count = null)
        {
            OpenFolderDialog openFolderDialog = new();
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

            return CalculateOrtoDatasComparisons(directory, ortoDatasComparisonOptions, references, count);
        }

        /// <summary>
        /// Calculates comparisons for orthodata located in the specified directory using the provided options and reference data.
        /// </summary>
        /// <param name="directory">The path to the directory containing the orthodata to be compared.</param>
        /// <param name="ortoDatasComparisonOptions">The options used to configure the orthodata comparison process.</param>
        /// <param name="references">An optional collection of reference strings to use during the comparison.</param>
        /// <param name="count">An optional integer specifying the number of processors or a limit for the operation.</param>
        /// <returns>A <see cref="HashSet{T}"/> containing the results of the orthodata comparisons, or <c>null</c> if the directory is invalid or does not exist.</returns>
        public static HashSet<string>? CalculateOrtoDatasComparisons(string? directory, OrtoDatasComparisonOptions? ortoDatasComparisonOptions, IEnumerable<string>? references = null, int? count = null)
        {
            if (string.IsNullOrWhiteSpace(directory) || !Directory.Exists(directory))
            {
                return null;
            }

            HashSet<string>? references_Temp = references == null ? null : [.. references];

            //int count_Temp = count == null || !count.HasValue ? GIS.Query.DefaultProcessorCount() : count.Value;
            int count_Temp = count == null || !count.HasValue ? Core.Create.ParallelOptions().MaxDegreeOfParallelism : count.Value;

            HashSet<string> result = [];

            string[] paths_Input = Directory.GetFiles(directory, "*." + FileExtension.GISModelFile, SearchOption.AllDirectories);
            for (int i = 0; i < paths_Input.Length; i++)
            {
                string path_Input = paths_Input[i];

                using GISModelFile gISModelFile = new(path_Input);

                gISModelFile.Open();

                GISModel? gISModel = gISModelFile.Value;
                if (gISModel != null)
                {
                    List<Building2D>? building2Ds = gISModel.GetObjects<Building2D>();
                    if (building2Ds != null)
                    {
                        if (references_Temp != null)
                        {
                            building2Ds = building2Ds.FindAll(x => x.Reference is string reference && references_Temp.Contains(reference));
                        }

                        ortoDatasComparisonOptions ??= new OrtoDatasComparisonOptions();

                        string? path = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(path_Input)!, string.Format("{0}.{1}", System.IO.Path.GetFileNameWithoutExtension(path_Input), Emgu.CV.Constants.FileExtension.OrtoDatasComparisonFile));

                        while (building2Ds.Count > 0)
                        {
                            int count_Min = Math.Min(building2Ds.Count, count_Temp);

                            List<Building2D> building2Ds_Temp = building2Ds.GetRange(0, count_Min);

                            IEnumerable<GuidReference>? guidReferences = Emgu.CV.Modify.CalculateOrtoDatasComparisons(building2Ds_Temp, path, ortoDatasComparisonOptions);
                            if (guidReferences != null)
                            {
                                foreach (GuidReference guidReference in guidReferences)
                                {
                                    Building2D? building2D = building2Ds_Temp.Find(x => new GuidReference(x) == guidReference);
                                    if (building2D?.Reference is string reference)
                                    {
                                        result.Add(reference);
                                    }
                                }
                            }

                            building2Ds.RemoveRange(0, count_Min);
                        }
                    }
                }
            }
            return result;
        }
    }
}