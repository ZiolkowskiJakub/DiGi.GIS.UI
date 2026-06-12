using DiGi.Core.Interfaces;
using DiGi.GIS.Classes;
using DiGi.GIS.Constants;
using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace DiGi.GIS.UI
{
    public static partial class Modify
    {
        /// <summary>
        /// Calculates and retrieves a set of GIS model files from a directory selected by the user via a folder dialog.
        /// </summary>
        /// <param name="owner">The owner window for the folder selection dialog.</param>
        /// <param name="determinateWorker">An optional worker used to report determinate progress during the calculation process.</param>
        /// <returns>A <see cref="HashSet{T}"/> containing the paths of the identified GIS model files, or <c>null</c> if the user cancels the dialog or no folder is selected.</returns>
        public static HashSet<string>? CalculateGISModelFiles(Window? owner, IDeterminateWorker? determinateWorker = null)
        {
            OpenFolderDialog openFolderDialog = new();
            bool? openFolderDialogResult = openFolderDialog.ShowDialog(owner);
            if (openFolderDialogResult == null || !openFolderDialogResult.HasValue || !openFolderDialogResult.Value)
            {
                return null;
            }

            string directory = openFolderDialog.FolderName;
            if (string.IsNullOrWhiteSpace(directory) || !Directory.Exists(directory))
            {
                return null;
            }

            List<string>? paths_Input = Directory.GetFiles(directory, "*." + FileExtension.GISModelFile, SearchOption.AllDirectories)?.ToList();
            if (paths_Input == null || paths_Input.Count == 0)
            {
                return null;
            }

            paths_Input.RemoveAll(x => string.IsNullOrWhiteSpace(x));

            if (determinateWorker != null)
            {
                determinateWorker.Maximum = paths_Input.Count;
            }

            paths_Input.Sort((x, y) => new FileInfo(y).Length.CompareTo(new FileInfo(x).Length));

            //int count = GIS.Query.DefaultProcessorCount();

            //ParallelOptions parallelOptions = new ParallelOptions()
            //{
            //    MaxDegreeOfParallelism = count
            //};

            ParallelOptions parallelOptions = Core.Create.ParallelOptions();

            int reportCount = 0;

            HashSet<string> result = [];

            while (paths_Input.Count > 0)
            {
                //int count_Temp = Math.Min(count, paths_Input.Count);
                int count_Temp = Math.Min(parallelOptions.MaxDegreeOfParallelism, paths_Input.Count);

                List<Tuple<string, GISModel?>?> tuples = [.. Enumerable.Repeat((Tuple<string, GISModel?>?)null, count_Temp)];
                for (int i = 0; i < count_Temp; i++)
                {
                    string path = paths_Input[i];

                    using GISModelFile gISModelFile = new(path);

                    gISModelFile.Open();
                    tuples[i] = new Tuple<string, GISModel?>(path, gISModelFile?.Value);
                }

                Parallel.For(0, tuples.Count, parallelOptions, i =>
                {
                    tuples[i]?.Item2?.Calculate();
                });

                for (int i = 0; i < count_Temp; i++)
                {
                    string path = paths_Input[i];

                    using GISModelFile gISModelFile = new(path);
                    gISModelFile.Value = tuples[i]?.Item2;
                    gISModelFile.Save();
                    result.Add(path);
                }

                paths_Input.RemoveRange(0, count_Temp);

                reportCount += count_Temp;
                determinateWorker?.Report(reportCount);
            }

            return result;
        }
    }
}