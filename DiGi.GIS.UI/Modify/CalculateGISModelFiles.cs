using DiGi.GIS.Classes;
using DiGi.GIS.Constans;
using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace DiGi.GIS.UI
{
    public static partial class Modify
    {
        public static void CalculateGISModelFiles(Window owner)
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

            List<string> paths_Input = Directory.GetFiles(directory, "*." + FileExtension.GISModelFile, SearchOption.AllDirectories)?.ToList();
            if (paths_Input == null || paths_Input.Count == 0)
            {
                return;
            }

            paths_Input.RemoveAll(x => string.IsNullOrWhiteSpace(x));

            paths_Input.Sort((x, y) => new FileInfo(y).Length.CompareTo(new FileInfo(x).Length));

            //int count = GIS.Query.DefaultProcessorCount();

            //ParallelOptions parallelOptions = new ParallelOptions()
            //{
            //    MaxDegreeOfParallelism = count
            //};

            ParallelOptions parallelOptions = Core.Create.ParallelOptions();

            while (paths_Input.Count > 0)
            {
                int count_Temp = Math.Min(parallelOptions.MaxDegreeOfParallelism, paths_Input.Count);
                //int count_Temp = Math.Min(count, paths_Input.Count);

                List<Tuple<string, GISModel>> tuples = Enumerable.Repeat((Tuple<string, GISModel>)null, count_Temp).ToList();
                for (int i = 0; i < count_Temp; i++)
                {
                    string path = paths_Input[i];

                    using (GISModelFile gISModelFile = new GISModelFile(path))
                    {
                        gISModelFile.Open();
                        tuples[i] = new Tuple<string, GISModel>(path, gISModelFile.Value);
                    }
                }

                Parallel.For(0, tuples.Count, parallelOptions, i =>
                {
                    tuples[i].Item2.Calculate();
                });

                for (int i = 0; i < count_Temp; i++)
                {
                    string path = paths_Input[i];

                    using (GISModelFile gISModelFile = new GISModelFile(path))
                    {
                        gISModelFile.Value = tuples[i].Item2;
                        gISModelFile.Save();
                    }
                }

                paths_Input.RemoveRange(0, count_Temp);
            }

            //for (int i = 0; i < paths_Input.Length; i++)
            //{
            //    string path_Input = paths_Input[i];
            //    if(string.IsNullOrWhiteSpace(path_Input))
            //    {
            //        return;
            //    }

            //    using (GISModelFile gISModelFile = new GISModelFile(path_Input))
            //    {
            //        gISModelFile.Open();
            //        GISModel gISModel = gISModelFile.Value;

            //        gISModel.Calculate();

            //        gISModelFile.Value = gISModel;
            //        gISModelFile.Save();
            //    }
            //}

            MessageBox.Show("Finished!");
        }
    }
}
