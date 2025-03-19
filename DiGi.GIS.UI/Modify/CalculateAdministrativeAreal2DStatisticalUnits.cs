using DiGi.GIS.Classes;
using DiGi.GIS.Constans;
using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace DiGi.GIS.UI
{
    public static partial class Modify
    {
        public static void CalculateAdministrativeAreal2DStatisticalUnits(Window owner, bool overrideExisting = true, string fileNameSufix = null)
        {
            bool? result;

            OpenFileDialog openFileDialog;

            openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = string.Format("{0} (*.{1})|*.{1}|All files (*.*)|*.*", FileTypeName.StatisticalUnitFile, FileExtension.StatisticalUnitFile);
            result = openFileDialog.ShowDialog(owner);
            if (result == null || !result.HasValue || !result.Value)
            {
                return;
            }

            string path_StatisticalUnitFile = openFileDialog.FileName;

            OpenFolderDialog openFolderDialog = new OpenFolderDialog();
            result = openFolderDialog.ShowDialog(owner);
            if (result == null || !result.HasValue || !result.Value)
            {
                return;
            }

            string directory = openFolderDialog.FolderName;

            List<string> paths_GISModelFile = Directory.GetFiles(directory, "*." + FileExtension.GISModelFile, SearchOption.AllDirectories).ToList();
            if (paths_GISModelFile == null || paths_GISModelFile.Count == 0)
            {
                return;
            }

            StatisticalUnit statisticalUnit = null;
            using (StatisticalUnitFile statisticalUnitFile = new StatisticalUnitFile(path_StatisticalUnitFile))
            {
                statisticalUnitFile.Open();

                statisticalUnit = statisticalUnitFile.Value;
            }

            if (statisticalUnit == null)
            {
                return;
            }

            Dictionary<string, List<AdministrativeAreal2D>> dictionary = new Dictionary<string, List<AdministrativeAreal2D>>();
            foreach(string path_GISModelFile in paths_GISModelFile)
            {
                dictionary[path_GISModelFile] = null;
            }


            if(fileNameSufix == null)
            {
                fileNameSufix = string.Empty;
            }

            int count = System.Convert.ToInt32(Environment.ProcessorCount * 0.9);
            if (count <= 0)
            {
                count = 1;
            }

            ParallelOptions parallelOptions = new ParallelOptions()
            {
                MaxDegreeOfParallelism = count
            };

            while (paths_GISModelFile.Count > 0)
            {
                int count_Temp = Math.Min(count, paths_GISModelFile.Count);

                Parallel.For(0, count_Temp, parallelOptions, i =>
                {
                    string path_Input = paths_GISModelFile[i];

                    string path_Output = Path.Combine(Path.GetDirectoryName(path_Input), Path.GetFileNameWithoutExtension(path_Input) + fileNameSufix + Path.GetExtension(path_Input));

                    if (!overrideExisting)
                    {
                        if (File.Exists(path_Output))
                        {
                            return;
                        }
                    }

                    GISModel gISModel = null;
                    using (GISModelFile gISModelFile = new GISModelFile(path_Input))
                    {
                        gISModelFile.Open();

                        gISModel = gISModelFile.Value;
                    }

                    GIS.Modify.CalculateAdministrativeAreal2DStatisticalUnits(gISModel, statisticalUnit, out List<AdministrativeAreal2D> invalidAdministrativeAreal2Ds);

                    dictionary[path_Input] = invalidAdministrativeAreal2Ds;

                    using (GISModelFile gISModelFile = new GISModelFile(path_Output))
                    {
                        gISModelFile.Value = gISModel;

                        gISModelFile.Save();
                    }

                });

                paths_GISModelFile.RemoveRange(0, count_Temp);
            }

            List<string> report = new List<string>();
            foreach(KeyValuePair<string, List<AdministrativeAreal2D>> keyValuePair in dictionary)
            {
                if(keyValuePair.Value == null || keyValuePair.Value.Count == 0)
                {
                    continue;
                }

                string name = Path.GetFileNameWithoutExtension(keyValuePair.Key);

                foreach(AdministrativeAreal2D administrativeAreal2D in keyValuePair.Value)
                {
                    report.Add(string.Format("{0}\t{1}\t{2}", name, administrativeAreal2D.Name, administrativeAreal2D.Reference));
                }
            }

            if(report != null && report.Count != 0)
            {
                File.WriteAllLines(Path.Combine(directory, "Report.txt"), report);
            }

        }
    }
}
