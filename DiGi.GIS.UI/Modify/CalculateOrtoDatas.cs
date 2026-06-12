using DiGi.Core;
using DiGi.Core.Classes;
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
        /// Calculates ortho data for 2D buildings based on the specified configuration and count.
        /// </summary>
        /// <param name="owner">The owner window used to display the folder selection dialog.</param>
        /// <param name="ortoDatasBuilding2DOptions">The configuration options for the 2D building ortho data calculation.</param>
        /// <param name="count">The number of items to process during the calculation.</param>
        /// <param name="overrideExisting">A value indicating whether existing ortho data should be overwritten.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is <c>true</c> if the calculation was successfully completed; otherwise, <c>false</c>.</returns>
        public static async Task<bool> CalculateOrtoDatas(Window? owner, OrtoDatasBuilding2DOptions? ortoDatasBuilding2DOptions, int count, bool overrideExisting = false)
        {
            OpenFolderDialog openFolderDialog = new();
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

            ortoDatasBuilding2DOptions ??= new OrtoDatasBuilding2DOptions();

            string[] paths_Input = Directory.GetFiles(directory, "*." + FileExtension.GISModelFile, SearchOption.AllDirectories);
            for (int i = 0; i < paths_Input.Length; i++)
            {
                string path_Input = paths_Input[i];

                if (System.IO.Path.GetDirectoryName(path_Input) is not string directory_Input)
                {
                    continue;
                }

                using GISModelFile gISModelFile = new(path_Input);
                gISModelFile.Open();

                GISModel? gISModel = gISModelFile.Value;
                if (gISModel != null)
                {
                    List<Building2D>? building2Ds = gISModel.GetObjects<Building2D>();
                    if (building2Ds != null)
                    {
                        string path = System.IO.Path.Combine(directory_Input, string.Format("{0}.{1}", System.IO.Path.GetFileNameWithoutExtension(path_Input), FileExtension.OrtoDatasFile));

                        while (building2Ds.Count > 0)
                        {
                            int count_Temp = building2Ds.Count > count ? count : building2Ds.Count;

                            HashSet<GuidReference>? guidReferences = await building2Ds.GetRange(0, count_Temp).CalculateOrtoDatas(path, ortoDatasBuilding2DOptions, overrideExisting);

                            building2Ds.RemoveRange(0, count_Temp);
                        }
                    }
                }
            }
            ;

            //MessageBox.Show("Finished!");

            return true;
        }

        /// <summary>
        /// Asynchronously calculates orthodata based on the specified options and count, prompting the user to select a target directory via a folder dialog.
        /// </summary>
        /// <param name="owner">The owner window for the folder selection dialog.</param>
        /// <param name="ortoDatasOrtoRangeOptions">The options used for filtering and retrieving orthodata within a specific range, including scale settings.</param>
        /// <param name="count">The number of items to process during the calculation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is <c>true</c> if the calculations were completed successfully; otherwise, <c>false</c>.</returns>
        public static async Task<bool> CalculateOrtoDatas(Window? owner, OrtoDatasOrtoRangeOptions? ortoDatasOrtoRangeOptions, int count)
        {
            OpenFolderDialog openFolderDialog = new();
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

            ortoDatasOrtoRangeOptions ??= new OrtoDatasOrtoRangeOptions();

            string[] paths_Input = Directory.GetFiles(directory, "*." + FileExtension.OrtoRangeFile, SearchOption.AllDirectories);
            for (int i = 0; i < paths_Input.Length; i++)
            {
                string path_Input = paths_Input[i];

                if (System.IO.Path.GetDirectoryName(path_Input) is not string directory_Input)
                {
                    continue;
                }

                using OrtoRangeFile ortoRangeFile = new(path_Input);
                ortoRangeFile.Open();

                List<OrtoRange?>? ortoRanges = ortoRangeFile.GetValues<OrtoRange>()?.ToList();
                if (ortoRanges != null)
                {
                    string path = System.IO.Path.Combine(directory_Input, string.Format("{0}.{1}", System.IO.Path.GetFileNameWithoutExtension(path_Input), FileExtension.OrtoDatasFile));

                    while (ortoRanges.Count > 0)
                    {
                        int count_Temp = ortoRanges.Count > count ? count : ortoRanges.Count;

                        HashSet<GuidReference>? guidReferences = await ortoRanges.GetRange(0, count_Temp).FilterNulls().CalculateOrtoDatas(path, ortoDatasOrtoRangeOptions);

                        ortoRanges.RemoveRange(0, count_Temp);
                    }
                }
            }
            ;

            //MessageBox.Show("Finished!");

            return true;
        }
    }
}