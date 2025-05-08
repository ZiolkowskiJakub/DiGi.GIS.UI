using DiGi.GIS.Classes;
using DiGi.GIS.Constans;
using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace DiGi.GIS.UI
{
    public static partial class Modify
    {
        public static void ResaveOrtoDatasFiles(Window owner, bool updateScale)
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

            string[] paths_Input = Directory.GetFiles(directory, "*." + FileExtension.OrtoDatasFile, SearchOption.TopDirectoryOnly);

            if(paths_Input != null && paths_Input.Length != 0)
            {
                Action<string> resave = new Action<string>(x =>
                {
                    string path_Input = x;

                    List<OrtoDatas> ortoDatas_Temp = null;

                    using (OrtoDatasFile ortoDatasFile = new OrtoDatasFile(path_Input))
                    {
                        IEnumerable<OrtoDatas> ortoDatas_Temp_Temp = ortoDatasFile.Values;
                        if (ortoDatas_Temp_Temp != null)
                        {
                            ortoDatas_Temp = new List<OrtoDatas>(ortoDatas_Temp_Temp);
                        }
                    }

                    if (ortoDatas_Temp == null)
                    {
                        return;
                    }

                    int count = ortoDatas_Temp.Count;

                    if (count == 0)
                    {
                        return;
                    }

                    if (updateScale)
                    {
                        for (int j = 0; j < count; j++)
                        {
                            OrtoDatas ortoDatas = ortoDatas_Temp[j];

                            List<OrtoData> ortoDatas_New = new List<OrtoData>();
                            foreach (OrtoData ortoData in ortoDatas)
                            {
                                ortoDatas_New.Add(new OrtoData(ortoData.DateTime, ortoData.Bytes, 1 / ortoData.Scale, ortoData.Location));
                            }

                            ortoDatas = new OrtoDatas(ortoDatas.Reference, ortoDatas_New);
                            ortoDatas_Temp[j] = ortoDatas;
                        }
                    }

                    File.Delete(path_Input);

                    string directory_New = GIS.Query.OrtoDatasDirectory_Building2D(Path.GetDirectoryName(path_Input));
                    if (!Directory.Exists(directory_New))
                    {
                        Directory.CreateDirectory(directory_New);
                    }

                    string path_Output = Path.Combine(directory_New, Path.GetFileName(path_Input));

                    using (OrtoDatasFile ortoDatasFile = new OrtoDatasFile(path_Output))
                    {
                        ortoDatasFile.Values = ortoDatas_Temp;
                        ortoDatasFile.Save();
                    }
                });

                //resave.Invoke(paths_Input[0]);

                ParallelOptions parallelOptions = new ParallelOptions()
                {
                    MaxDegreeOfParallelism = GIS.Query.DefaultProcessorCount()
                };

                Parallel.For(0, paths_Input.Length, parallelOptions, i =>
                {
                    resave(paths_Input[i]);
                });
            }
        }
    }
}
