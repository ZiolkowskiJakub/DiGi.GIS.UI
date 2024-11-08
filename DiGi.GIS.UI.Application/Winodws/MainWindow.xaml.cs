using DiGi.GIS.Classes;
using DiGi.GIS.Enums;
using Microsoft.Win32;
using System.IO;

using System.Windows;

namespace DiGi.GIS.UI.Application.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Write_Click(object sender, RoutedEventArgs e)
        {
            Convert_FromBDOT10k();
        }

        private void Button_Convert_Click(object sender, RoutedEventArgs e)
        {
            Convert_FromBDOT10k();
        }

        private void Button_Read_Click(object sender, RoutedEventArgs e)
        {

            Read_FromZip();
        }

        private void Button_Analyse_Click(object sender, RoutedEventArgs e)
        {
            Report(true);
        }

        private void Button_Calculate_Click(object sender, RoutedEventArgs e)
        {
            Calculate();
        }

        private void CalculateBuilding2DGeometries()
        {
            OpenFolderDialog openFolderDialog = new OpenFolderDialog();
            bool? result = openFolderDialog.ShowDialog(this);
            if (result == null || !result.HasValue || !result.Value)
            {
                return;
            }

            string directory = openFolderDialog.FolderName;
            if (string.IsNullOrWhiteSpace(directory) || !Directory.Exists(directory))
            {
                return;
            }

            string[] paths_Input = Directory.GetFiles(directory, "*." + Core.IO.File.Constans.FileExtension.Zip, SearchOption.AllDirectories);
            for (int i = 0; i < paths_Input.Length; i++)
            {
                string path_Input = paths_Input[i];

                GISModel gISModel = null;

                using (GISModelFile gISModelFile = new GISModelFile(path_Input))
                {
                    gISModelFile.Open();
                    gISModel = gISModelFile.Value;

                    gISModel.CalculateBuilding2DGeometries();

                    gISModelFile.Value = gISModel;
                    gISModelFile.Save();
                }
            };

            MessageBox.Show("Finished!");
        }

        private void CalculateAdministrativeAreal2DGeometries()
        {
            OpenFolderDialog openFolderDialog = new OpenFolderDialog();
            bool? result = openFolderDialog.ShowDialog(this);
            if (result == null || !result.HasValue || !result.Value)
            {
                return;
            }

            string directory = openFolderDialog.FolderName;
            if (string.IsNullOrWhiteSpace(directory) || !Directory.Exists(directory))
            {
                return;
            }

            string[] paths_Input = Directory.GetFiles(directory, "*." + Core.IO.File.Constans.FileExtension.Zip, SearchOption.AllDirectories);
            for (int i = 0; i < paths_Input.Length; i++)
            {
                string path_Input = paths_Input[i];

                GISModel gISModel = null;

                using (GISModelFile gISModelFile = new GISModelFile(path_Input))
                {
                    gISModelFile.Open();
                    gISModel = gISModelFile.Value;

                    gISModel.CalculateAdministrativeAreal2DGeometries();

                    gISModelFile.Value = gISModel;
                    gISModelFile.Save();
                }
            };

            MessageBox.Show("Finished!");
        }

        private void Calculate()
        {
            OpenFolderDialog openFolderDialog = new OpenFolderDialog();
            bool? result = openFolderDialog.ShowDialog(this);
            if (result == null || !result.HasValue || !result.Value)
            {
                return;
            }

            string directory = openFolderDialog.FolderName;
            if (string.IsNullOrWhiteSpace(directory) || !Directory.Exists(directory))
            {
                return;
            }

            string[] paths_Input = Directory.GetFiles(directory, "*." + Core.IO.File.Constans.FileExtension.Zip, SearchOption.AllDirectories);
            for (int i = 0; i < paths_Input.Length; i++)
            {
                string path_Input = paths_Input[i];

                GISModel gISModel = null;

                using (GISModelFile gISModelFile = new GISModelFile(path_Input))
                {
                    gISModelFile.Open();
                    gISModel = gISModelFile.Value;

                    gISModel.Calculate();

                    gISModelFile.Value = gISModel;
                    gISModelFile.Save();
                }
            };

            MessageBox.Show("Finished!");
        }

        private void ListAdministrativeAreal2Ds()
        {
            OpenFolderDialog openFolderDialog = new OpenFolderDialog();
            bool? result = openFolderDialog.ShowDialog(this);
            if (result == null || !result.HasValue || !result.Value)
            {
                return;
            }

            string directory = openFolderDialog.FolderName;
            if (string.IsNullOrWhiteSpace(directory) || !Directory.Exists(directory))
            {
                return;
            }

            string[] paths_Input = Directory.GetFiles(directory, "*." + Core.IO.File.Constans.FileExtension.Zip, SearchOption.AllDirectories);

            string path_Output = Path.Combine(directory, "administrativeArealNames.txt");

            HashSet<string> types = new HashSet<string>();
            if (File.Exists(path_Output))
            {
                string[] lines = File.ReadAllLines(path_Output);
                foreach (string line in lines)
                {
                    string[] values = line?.Split('\t');
                    if (values != null && values.Length > 1)
                    {
                        types.Add(values[0]);
                    }
                }
            }

            for (int i = 0; i < paths_Input.Length; i++)
            {
                string path_Input = paths_Input[i];

                string type = Path.GetFileNameWithoutExtension(path_Input);
                if (types.Contains(type))
                {
                    continue;
                }

                GISModel gISModel = null;

                using (GISModelFile gISModelFile = new GISModelFile(path_Input))
                {
                    gISModelFile.Open();
                    gISModel = gISModelFile.Value;
                }

                if (gISModel == null)
                {
                    return;
                }

                List<AdministrativeAreal2D> administrativeAreal2Ds = gISModel.GetObjects<AdministrativeAreal2D>();
                if (administrativeAreal2Ds == null)
                {
                    return;
                }

                List<string> lines = new List<string>();
                foreach (AdministrativeAreal2D administrativeAreal2D in administrativeAreal2Ds)
                {
                    lines.Add(string.Format("{0}\t{1}\t{2}", type, administrativeAreal2D.Name, administrativeAreal2D.AdministrativeArealType.ToString()));
                }

                File.AppendAllLines(path_Output, lines);
            };

            //MessageBox.Show("Finished!");
        }

        private void Read_FromZip()
        {
            bool? result;

            OpenFolderDialog openFolderDialog = new OpenFolderDialog();
            result = openFolderDialog.ShowDialog(this);
            if (result == null || !result.HasValue || !result.Value)
            {
                return;
            }

            string directory = openFolderDialog.FolderName;
            if (string.IsNullOrWhiteSpace(directory) || !Directory.Exists(directory))
            {
                return;
            }

            string[] paths_Input = Directory.GetFiles(directory, "*." + Core.IO.File.Constans.FileExtension.Zip, SearchOption.AllDirectories);
            foreach (string path_Input in paths_Input)
            {
                GISModel gISModel_Input = null;

                using (GISModelFile gISModelFile = new GISModelFile(path_Input))
                {
                    gISModelFile.Open();
                    gISModel_Input = gISModelFile.Value;
                }

                if (gISModel_Input == null)
                {
                    continue;
                }

                Building2D building2D = gISModel_Input.GetObject<Building2D>();

                string path_Output = Path.Combine(Path.GetDirectoryName(path_Input), Path.GetFileNameWithoutExtension(path_Input) + "_Out." + Core.IO.File.Constans.FileExtension.Zip);
                using (GISModelFile gISModelFile = new GISModelFile(path_Output))
                {

                    GISModel gISModel_Output = new GISModel();
                    gISModel_Output.Update(building2D);
                    gISModel_Output.CalculateBuilding2DGeometries();

                    gISModel_Output.CalculateBuilding2DGeometries();

                    gISModelFile.Value = gISModel_Output;
                    gISModelFile.Save();
                }

                using (GISModelFile gISModelFile = new GISModelFile(path_Output))
                {
                    gISModelFile.Open();
                    GISModel gISModel_Output = gISModelFile.Value;
                }

            }

        }

        private void Convert_FromBDOT10k()
        {
            bool? result;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "zip files (*.zip)|*.zip|All files (*.*)|*.*";
            result = openFileDialog.ShowDialog(this);
            if (result == null || !result.HasValue || !result.Value)
            {
                return;
            }

            string path = openFileDialog.FileName;
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
            {
                return;
            }

            OpenFolderDialog openFolderDialog = new OpenFolderDialog();
            result = openFolderDialog.ShowDialog(this);
            if (result == null || !result.HasValue || !result.Value)
            {
                return;
            }

            string directory = openFolderDialog.FolderName;
            if (string.IsNullOrWhiteSpace(directory) || !Directory.Exists(directory))
            {
                return;
            }

            Convert.ToDiGi(path, directory);
        }

        private void Read_Files()
        {
            OpenFolderDialog openFolderDialog = new OpenFolderDialog();
            bool? result = openFolderDialog.ShowDialog(this);
            if (result == null || !result.HasValue || !result.Value)
            {
                return;
            }

            string directory = openFolderDialog.FolderName;
            if (string.IsNullOrWhiteSpace(directory) || !Directory.Exists(directory))
            {
                return;
            }

            List<Building2D> building2Ds = Create.Building2Ds(directory);
        }

        private void FindTest()
        {

        }

        private void Reorganize()
        {
            OpenFolderDialog openFolderDialog = new OpenFolderDialog();
            bool? result = openFolderDialog.ShowDialog(this);
            if (result == null || !result.HasValue || !result.Value)
            {
                return;
            }

            string directory = openFolderDialog.FolderName;
            if (string.IsNullOrWhiteSpace(directory) || !Directory.Exists(directory))
            {
                return;
            }

            foreach (string directory_Temp in Directory.GetDirectories(directory))
            {
                string directory_Buildings = Path.Combine(directory_Temp, "Buildings");
                if (!Directory.Exists(directory_Buildings))
                {
                    Directory.CreateDirectory(directory_Buildings);
                }

                string[] directories = Directory.GetDirectories(directory_Temp);

                foreach (string directory_Building in directories)
                {
                    string name = new DirectoryInfo(directory_Building).Name;

                    if (name == "Buildings")
                    {
                        continue;
                    }

                    Directory.Move(directory_Building, Path.Combine(directory_Buildings, name));
                }
            }
        }

        private void Delete()
        {
            //OpenFolderDialog openFolderDialog = new OpenFolderDialog();
            //bool? result = openFolderDialog.ShowDialog(this);
            //if (result == null || !result.HasValue || !result.Value)
            //{
            //    return;
            //}

            //string directory = openFolderDialog.FolderName;
            string directory = @"C:\Users\jakub\Nextcloud\Data\GIS\Output";
            if (string.IsNullOrWhiteSpace(directory) || !Directory.Exists(directory))
            {
                return;
            }

            foreach (string directory_Temp in Directory.GetDirectories(directory))
            {
                Directory.Delete(directory_Temp, true);
            }
        }

        private void Report(bool recalculate)
        {
            OpenFolderDialog openFolderDialog = new OpenFolderDialog();
            bool? result = openFolderDialog.ShowDialog(this);
            if (result == null || !result.HasValue || !result.Value)
            {
                return;
            }

            string directory = openFolderDialog.FolderName;
            if (string.IsNullOrWhiteSpace(directory) || !Directory.Exists(directory))
            {
                return;
            }

            //Dictionary<AdministrativeArealType, Tuple<int, double, double, double>> dictionary = new Dictionary<AdministrativeArealType, Tuple<int, double, double, double>>();

            string[] paths_Input = Directory.GetFiles(directory, "*." + Core.IO.File.Constans.FileExtension.Zip, SearchOption.AllDirectories);
            for (int i = 0; i < paths_Input.Length; i++)
            {
                string path_Input = paths_Input[i];

                GISModel gISModel = null;

                using (GISModelFile gISModelFile = new GISModelFile(path_Input))
                {
                    gISModelFile.Open();
                    gISModel = gISModelFile.Value;

                    if(recalculate)
                    {
                        gISModel.Calculate();
                        gISModelFile.Value = gISModel;
                        gISModelFile.Save();
                    }
                }

                List<AdministrativeAreal2D> administrativeAreal2Ds = gISModel.GetObjects<AdministrativeAreal2D>();
                if (administrativeAreal2Ds == null)
                {
                    continue;
                }

                List<string> lines = new List<string>();

                List<string> values;

                values = new List<string>()
                {
                    "Name",
                    "Type",
                    "Building Count",
                    "Total Area",
                    "Avg. Area",
                    "Avg. Thinness Ratio",
                    "Avg. Rectangularity",
                };

                lines.Add(string.Join("\t", values));

                foreach (AdministrativeAreal2D administrativeAreal2D in administrativeAreal2Ds)
                {
                    AdministrativeAreal2DBuilding2DsRelation administrativeAreal2DBuilding2DsRelation = gISModel.GetRelation<AdministrativeAreal2DBuilding2DsRelation>(administrativeAreal2D);
                    if (administrativeAreal2DBuilding2DsRelation == null)
                    {
                        continue;
                    }

                    int count = 0;

                    double area = 0;

                    double thinessRatio = 0;
                    double rectangularity = 0;

                    foreach (Core.Classes.GuidReference guidReference in administrativeAreal2DBuilding2DsRelation.UniqueReferences_To)
                    {
                        Building2D building2D = gISModel.GetObject<Building2D>(guidReference);
                        if (building2D == null)
                        {
                            continue;
                        }

                        if (!building2D.IsOccupied())
                        {
                            continue;
                        }

                        Building2DGeometryCalculationResult building2DGeometryCalculationResult = gISModel.GetRelatedObjects<Building2DGeometryCalculationResult>(building2D)?.FirstOrDefault();
                        if (building2DGeometryCalculationResult == null)
                        {
                            building2DGeometryCalculationResult = Create.Building2DGeometryCalculationResult(building2D);
                        }

                        double area_Building2D = building2DGeometryCalculationResult.Area * building2D.Storeys;
                        if(double.IsNaN(area_Building2D) || area_Building2D == 0)
                        {
                            continue;
                        }

                        count++;

                        area += area_Building2D;

                        thinessRatio += building2DGeometryCalculationResult.ThinnessRatio * area_Building2D;
                        rectangularity += building2DGeometryCalculationResult.Rectangularity * area_Building2D;
                    }

                    if(area == 0)
                    {
                        continue;
                    }

                    values = new List<string>()
                    {
                        administrativeAreal2D.Name,
                        administrativeAreal2D.AdministrativeArealType.ToString(),
                        count.ToString(),
                        Core.Query.Round(area, 0.1).ToString(),
                        Core.Query.Round(area / count, 0.1).ToString(),
                        Core.Query.Round(thinessRatio / area, 0.001).ToString(),
                        Core.Query.Round(rectangularity / area, 0.001).ToString(),
                    };

                    lines.Add(string.Join("\t", values));

                    //if(!dictionary.TryGetValue(administrativeAreal2D.AdministrativeArealType, out Tuple<int, double, double, double> tuple))
                    //{
                    //    tuple = new Tuple<int, double, double, double>(0, 0, 0, 0);
                    //    dictionary[administrativeAreal2D.AdministrativeArealType] = tuple;
                    //}

                    //dictionary[administrativeAreal2D.AdministrativeArealType] = new Tuple<int, double, double, double>(tuple.Item1 + count, tuple.Item2 + area, tuple.Item3 + (thinessRatio / area), tuple.Item4 + (rectangularity / area));
                }

                string path_Output = Path.Combine(Path.GetDirectoryName(path_Input), Path.GetFileNameWithoutExtension(path_Input) + "_Report.txt");

                File.WriteAllLines(path_Output, lines);
            };

            //List<string> summary = new List<string>();
            //summary.Add(string.Join("\t", new List<string> 
            //{
            //        "Type",
            //        "Building Count",
            //        "Total Area",
            //        "Avg. Area",
            //        "Avg. Thinness Ratio",
            //        "Avg. Rectangularity",
            //}));

            //foreach (KeyValuePair<AdministrativeArealType, Tuple<int, double, double, double>> keyValuePair in dictionary)
            //{

            //    List<string> values = new List<string>()
            //    {
            //        keyValuePair.Key.ToString(),
            //        keyValuePair.Value.Item1.ToString(),
            //        keyValuePair.Value.Item2.ToString(),
            //        Core.Query.Round(keyValuePair.Value.Item2 / keyValuePair.Value.Item1, 0.1).ToString(),
            //        Core.Query.Round(keyValuePair.Value.Item3 / keyValuePair.Value.Item2, 0.001).ToString(),
            //        Core.Query.Round(keyValuePair.Value.Item4 / keyValuePair.Value.Item2, 0.001).ToString(),
            //    };

            //    summary.Add(string.Join("\t", values));
            //}

            //string path_Summary = Path.Combine(directory, "Report.txt");
            //File.WriteAllLines(path_Summary, summary);

            //MessageBox.Show("Finished!");
        }


    }
}