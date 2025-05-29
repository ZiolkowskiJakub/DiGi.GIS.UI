using DiGi.GIS.Classes;
using DiGi.GIS.Constans;
using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace DiGi.GIS.UI
{
    public static partial class Modify
    {
        public static bool AppendPredictionYearBuilts(Window owner)
        {
            bool? result;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Year Built Prediction File (ML output)";
            openFileDialog.Filter = "Tab-Separated Values File (*.tsv)|*.tsv|All files (*.*)|*.*";
            result = openFileDialog.ShowDialog();
            if (result == null || !result.HasValue || !result.Value)
            {
                return false;
            }

            string[] lines = File.ReadAllLines(openFileDialog.FileName);
            if(lines == null || lines.Length == 0)
            {
                return false;
            }

            Dictionary<string, short> dictionary = new Dictionary<string, short>();
            foreach (string line in lines)
            {
                string[] values = line.Split('\t');
                if(values == null || values.Length < 2)
                {
                    continue;
                }

                if (!short.TryParse(values[1], out short year))
                {
                    continue;
                }

                dictionary[values[0]] = year;
            }

            if(dictionary == null || dictionary.Count == 0)
            {
                return false;
            }

            FileInfo fileInfo = new FileInfo(openFileDialog.FileName);
            DateTime dateTime = fileInfo.LastWriteTimeUtc;

            OpenFolderDialog openFolderDialog = new OpenFolderDialog();
            openFolderDialog.Title = "Select GIS Model Files directory";
            result = openFolderDialog.ShowDialog(owner);
            if (result == null || !result.HasValue || !result.Value)
            {
                return false;
            }

            string directory_GISModelFiles = openFolderDialog.FolderName;
            if (string.IsNullOrWhiteSpace(directory_GISModelFiles) || !Directory.Exists(directory_GISModelFiles))
            {
                return false;
            }

            string[] paths_GISModelFile = Directory.GetFiles(directory_GISModelFiles, string.Format("*.{0}", FileExtension.GISModelFile), SearchOption.AllDirectories);
            if (paths_GISModelFile == null || paths_GISModelFile.Length == 0)
            {
                return false;
            }

            Parallel.ForEach(paths_GISModelFile, GIS.Query.DefaultParallelOptions(), path_GISModelFile => 
            {
                Dictionary<string, short> dictionary_GISModelFile = new Dictionary<string, short>();
                using (GISModelFile gISModelFile = new GISModelFile(path_GISModelFile))
                {
                    gISModelFile.Open();

                    HashSet<string> references = gISModelFile.Value?.GetReferences<Building2D>();
                    if (references == null || references.Count == 0)
                    {
                        return;
                    }

                    foreach (string reference in references)
                    {
                        if (dictionary.ContainsKey(reference))
                        {
                            dictionary_GISModelFile[reference] = dictionary[reference];
                        }
                    }
                }

                if (dictionary_GISModelFile == null || dictionary_GISModelFile.Count == 0)
                {
                    return;
                }

                string path = Path.Combine(Path.GetDirectoryName(path_GISModelFile), string.Format("{0}.{1}", Path.GetFileNameWithoutExtension(path_GISModelFile), FileExtension.YearBuiltDataFile));
                using (YearBuiltDataFile yearBuiltDataFile = new YearBuiltDataFile(path))
                {
                    yearBuiltDataFile.Open();
                    foreach (KeyValuePair<string, short> keyValuePair in dictionary_GISModelFile)
                    {
                        YearBuiltData yearBuiltData = yearBuiltDataFile.GetValue<YearBuiltData>(keyValuePair.Key);
                        if (yearBuiltData == null)
                        {
                            yearBuiltData = new YearBuiltData(keyValuePair.Key);
                        }

                        yearBuiltData.SetPredictedYearBuilt(dateTime, keyValuePair.Value);
                        yearBuiltDataFile.AddValue(yearBuiltData);
                    }

                    yearBuiltDataFile.Save();
                }
            });

            return true;
        }
    }
}
