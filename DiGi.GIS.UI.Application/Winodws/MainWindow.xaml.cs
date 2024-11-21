using DiGi.Core;
using DiGi.GIS.Classes;
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
            Report_Geometry(false);
            //Report_Occupancy();
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

        private void Report_Geometry(bool recalculate)
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

                    if (recalculate)
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
                    "Avg. Perimeter",
                    "Avg. Thinness Ratio",
                    "Avg. Rectangular Thinness Ratio",
                    "Avg. Rectangularity",
                    "Avg. Isoperimetric Ratio",
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

                    double perimeter = 0;
                    double thinessRatio = 0;
                    double rectangularThinnessRatio = 0;
                    double rectangularity = 0;
                    double isoperimetricRatio = 0;

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

                        Building2DGeometryCalculationResult building2DGeometryCalculationResult = gISModel.GetRelatedObject<Building2DGeometryCalculationResult>(building2D);
                        if (building2DGeometryCalculationResult == null)
                        {
                            building2DGeometryCalculationResult = Create.Building2DGeometryCalculationResult(building2D);
                        }

                        double area_Building2D = building2DGeometryCalculationResult.Area * building2D.Storeys;
                        if (double.IsNaN(area_Building2D) || area_Building2D == 0)
                        {
                            continue;
                        }

                        count++;

                        area += area_Building2D;

                        perimeter += building2DGeometryCalculationResult.Perimeter * area_Building2D;
                        thinessRatio += building2DGeometryCalculationResult.ThinnessRatio * area_Building2D;
                        rectangularThinnessRatio += building2DGeometryCalculationResult.RectangularThinnessRatio * area_Building2D;
                        rectangularity += building2DGeometryCalculationResult.Rectangularity * area_Building2D;
                        isoperimetricRatio += building2DGeometryCalculationResult.IsoperimetricRatio * area_Building2D;
                    }

                    if (area == 0)
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
                        Core.Query.Round(perimeter / area, 0.001).ToString(),
                        Core.Query.Round(thinessRatio / area, 0.001).ToString(),
                        Core.Query.Round(rectangularThinnessRatio / area, 0.001).ToString(),
                        Core.Query.Round(rectangularity / area, 0.001).ToString(),
                        Core.Query.Round(isoperimetricRatio / area, 0.001).ToString(),
                    };

                    lines.Add(string.Join("\t", values));
                }

                string path_Output = Path.Combine(Path.GetDirectoryName(path_Input), Path.GetFileNameWithoutExtension(path_Input) + "_GeometryReport.txt");

                File.WriteAllLines(path_Output, lines);
            };

            MessageBox.Show("Finished!");
        }

        private void Report_Occupancy()
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

                    //if (recalculate)
                    //{
                    //    gISModel.Calculate();
                    //    gISModelFile.Value = gISModel;
                    //    gISModelFile.Save();
                    //}
                }

                if(gISModel == null)
                {
                    continue;
                }

                List<Building2D> building2Ds = gISModel.GetObjects<Building2D>();
                if(building2Ds == null)
                {
                    continue;
                }

                string path_Output = Path.Combine(Path.GetDirectoryName(path_Input), string.Format("{0}_OccupacyReport.txt", Path.GetFileNameWithoutExtension(path_Input)));

                List<string> lines = new List<string>();
                lines.Add(string.Join("\t", new string[]
                {
                    "Reference",
                    "IsOccupied",
                    "BuildingPhase",
                    "Storeys",
                    "BuildingGeneralFunction",
                    "BuildingSpecificFunctions",
                    "OccupancyArea",
                    "Occupancy",
                    "Area",
                    "Perimeter",
                    "ThinnessRatio",
                    "RectangularThinnessRatio",
                    "Rectangularity",
                    "IsoperimetricRatio"
                }));

                foreach(Building2D building2D in building2Ds)
                {
                    if(building2D == null)
                    {
                        continue;
                    }

                    List<string> values = new List<string>();
                    values.Add(building2D.Reference);
                    values.Add(building2D.IsOccupied() ? "True" : "False");
                    values.Add(building2D.BuildingPhase != null ? building2D.BuildingPhase.ToString() : string.Empty);
                    values.Add(building2D.Storeys.ToString());
                    values.Add(building2D.BuildingGeneralFunction != null ? building2D.BuildingGeneralFunction.ToString() : string.Empty);
                    values.Add(building2D.BuildingSpecificFunctions != null ? string.Join(";", building2D.BuildingSpecificFunctions.ToList().ConvertAll(x => x.ToString())) : string.Empty);

                    if(gISModel.TryGetRelatedObject(building2D, out OccupancyCalculationResult occupancyCalculationResult) && occupancyCalculationResult != null)
                    {
                        values.Add(occupancyCalculationResult.OccupancyArea?.ToString());
                        values.Add(occupancyCalculationResult.Occupancy?.ToString());
                    }
                    else
                    {
                        values.Add(string.Empty);
                        values.Add(string.Empty);
                    }

                    if (gISModel.TryGetRelatedObject(building2D, out Building2DGeometryCalculationResult building2DGeometryCalculationResult) && building2DGeometryCalculationResult != null)
                    {
                        values.Add(Core.Query.Round(building2DGeometryCalculationResult.Area, 0.01).ToString());
                        values.Add(Core.Query.Round(building2DGeometryCalculationResult.Perimeter, 0.01).ToString());
                        values.Add(Core.Query.Round(building2DGeometryCalculationResult.ThinnessRatio, 0.001).ToString());
                        values.Add(Core.Query.Round(building2DGeometryCalculationResult.RectangularThinnessRatio, 0.001).ToString());
                        values.Add(Core.Query.Round(building2DGeometryCalculationResult.Rectangularity, 0.001).ToString());
                        values.Add(Core.Query.Round(building2DGeometryCalculationResult.IsoperimetricRatio, 0.001).ToString());
                    }
                    else
                    {
                        values.Add(string.Empty);
                        values.Add(string.Empty);
                        values.Add(string.Empty);
                        values.Add(string.Empty);
                        values.Add(string.Empty);
                        values.Add(string.Empty);
                    }

                    lines.Add(string.Join("\t", values));
                }

                File.WriteAllLines(path_Output, lines);
                
                MessageBox.Show("Finished!");
            }
        }
    }
}