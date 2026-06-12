using Microsoft.Win32;
using System.IO;
using System.IO.Compression;
using System.Windows;

namespace DiGi.GIS.UI
{
    public static partial class Modify
    {
        /// <summary>
        /// Prompts the user to select a ZIP file containing 3D models using an open file dialog.
        /// </summary>
        /// <param name="owner">The owner window that will host the file dialog.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="HashSet{T}"/> of strings if a file was selected; otherwise, <see langword="null"/>.</returns>
        public static async Task<HashSet<string>?> Download3DModels(this Window? owner)
        {
            OpenFileDialog openFileDialog = new()
            {
                Filter = "zip files (*.zip)|*.zip|All files (*.*)|*.*"
            };
            bool? dialogResult = openFileDialog.ShowDialog(owner);
            if (dialogResult == null || !dialogResult.HasValue || !dialogResult.Value)
            {
                return null;
            }

            string urlPrefix = @"https://opendata.geoportal.gov.pl/InneDane/Budynki3D";

            int[] years = [2018, 2019, 2020, 2021, 2022, 2023, 2024, 2025];

            string path_Input = openFileDialog.FileName;

            if (Path.GetDirectoryName(path_Input) is not string directory_Input)
            {
                return null;
            }

            string directory_Output = Path.Combine(directory_Input, "Modele 3D");

            List<Tuple<string, string>> tuples = [];
            using (ZipArchive zipArchive = ZipFile.OpenRead(path_Input))
            {
                foreach (ZipArchiveEntry zipArchiveEntry in zipArchive.Entries)
                {
                    if (zipArchiveEntry.Open() is not DeflateStream deflateStream)
                    {
                        continue;
                    }

                    using ZipArchive zipArchive_ZipArchieve = new(deflateStream);

                    foreach (ZipArchiveEntry zipArchiveEntry_Zip in zipArchive_ZipArchieve.Entries)
                    {
                        if (zipArchiveEntry_Zip.Open() is not DeflateStream deflateStream_Zip)
                        {
                            continue;
                        }

                        string region = Path.GetFileNameWithoutExtension(zipArchiveEntry_Zip.Name);
                        if (region.EndsWith("_GML"))
                        {
                            region = region[..^4];
                        }

                        string fileName_1, fileName_2;

                        fileName_1 = string.Format(region + "_gml.zip");
                        fileName_2 = string.Format(region + ".zip");

                        tuples.Add(new Tuple<string, string>(string.Join("/", urlPrefix, "LOD2", fileName_1), Path.Combine(directory_Output, "LOD2", fileName_2)));

                        foreach (int year in years)
                        {
                            string regionPrefix = region[..2];

                            fileName_1 = string.Format(region + "_gml.zip");
                            fileName_2 = string.Format(region + ".zip");

                            tuples.Add(new Tuple<string, string>(string.Join("/", urlPrefix, "LOD1", year.ToString(), regionPrefix, fileName_1), Path.Combine(directory_Output, "LOD1", year.ToString(), fileName_2)));
                            tuples.Add(new Tuple<string, string>(string.Join("/", urlPrefix, "LOD1", year.ToString(), regionPrefix, fileName_2), Path.Combine(directory_Output, "LOD1", year.ToString(), fileName_2)));
                        }
                    }
                }
            }

            HashSet<string> result = [];
            foreach (Tuple<string, string> tuple in tuples)
            {
                if (result.Contains(tuple.Item2))
                {
                    continue;
                }

                bool succedded = await TryDownload(tuple.Item1, tuple.Item2);
                if (!succedded)
                {
                    continue;
                }

                if (Path.Exists(tuple.Item2))
                {
                    result.Add(tuple.Item2);
                }
            }

            return result;
        }
    }
}