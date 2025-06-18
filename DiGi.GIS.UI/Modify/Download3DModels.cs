using Microsoft.Win32;
using System.IO;
using System.IO.Compression;
using System.Windows;

namespace DiGi.GIS.UI
{
    public static partial class Modify
    {
        public static async Task<HashSet<string>> Download3DModels(this Window owner)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "zip files (*.zip)|*.zip|All files (*.*)|*.*";
            bool? dialogResult = openFileDialog.ShowDialog(owner);
            if (dialogResult == null || !dialogResult.HasValue || !dialogResult.Value)
            {
                return null;
            }

            string urlPrefix = @"https://opendata.geoportal.gov.pl/InneDane/Budynki3D";

            int[] years = [2018, 2019, 2020, 2021, 2022, 2023, 2024, 2025];

            string path_Input = openFileDialog.FileName;

            string directory_Input = Path.GetDirectoryName(path_Input);

            string directory_Output = Path.Combine(directory_Input, "Modele 3D");

            List<Tuple<string, string>> tuples = new List<Tuple<string, string>>();
            using (ZipArchive zipArchive = ZipFile.OpenRead(path_Input))
            {
                foreach (ZipArchiveEntry zipArchiveEntry in zipArchive.Entries)
                {
                    DeflateStream deflateStream = zipArchiveEntry.Open() as DeflateStream;
                    if (deflateStream == null)
                    {
                        continue;
                    }

                    using (ZipArchive zipArchive_ZipArchieve = new ZipArchive(deflateStream))
                    {
                        foreach (ZipArchiveEntry zipArchiveEntry_Zip in zipArchive_ZipArchieve.Entries)
                        {
                            DeflateStream deflateStream_Zip = zipArchiveEntry_Zip.Open() as DeflateStream;
                            if (deflateStream_Zip == null)
                            {
                                continue;
                            }

                            string region = Path.GetFileNameWithoutExtension(zipArchiveEntry_Zip.Name);
                            if (region.EndsWith("_GML"))
                            {
                                region = region.Substring(0, region.Length - 4);
                            }

                            string fileName_1, fileName_2;

                            fileName_1 = string.Format(region + "_gml.zip");
                            fileName_2 = string.Format(region + ".zip");

                            tuples.Add(new Tuple<string, string>(string.Join("/", urlPrefix, "LOD2", fileName_1), Path.Combine(directory_Output, "LOD2", fileName_2)));

                            foreach (int year in years)
                            {
                                string regionPrefix = region.Substring(0, 2);

                                fileName_1 = string.Format(region + "_gml.zip");
                                fileName_2 = string.Format(region + ".zip");

                                tuples.Add(new Tuple<string, string>(string.Join("/", urlPrefix, "LOD1", year.ToString(), regionPrefix, fileName_1), Path.Combine(directory_Output, "LOD1", year.ToString(), fileName_2)));
                                tuples.Add(new Tuple<string, string>(string.Join("/", urlPrefix, "LOD1", year.ToString(), regionPrefix, fileName_2), Path.Combine(directory_Output, "LOD1", year.ToString(), fileName_2)));
                            }
                        }
                    }
                }
            }

            HashSet<string> result = new HashSet<string>();
            foreach(Tuple<string, string> tuple in tuples)
            {
                if(result.Contains(tuple.Item2))
                {
                    continue;
                }

                bool succedded = await TryDownload(tuple.Item1, tuple.Item2);
                if(!succedded)
                {
                    continue;
                }

                if(Path.Exists(tuple.Item2))
                {
                    result.Add(tuple.Item2);
                }
            }

            return result; 
        }
    }
}
