using DiGi.Core;
using DiGi.Core.Classes;
using DiGi.Core.IO.Table.Classes;
using DiGi.Geometry.Planar;
using DiGi.Geometry.Planar.Classes;
using DiGi.GIS.Classes;
using DiGi.GIS.Constans;
using DiGi.GIS.Emgu.CV.Classes;
using Microsoft.Win32;
using System.Drawing;
using System.Drawing.Imaging;
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
            //Convert_FromBDOT10k();
            Convert_ToFiles();
        }

        private void Button_Read_Click(object sender, RoutedEventArgs e)
        {

            Read_FromZip();
        }

        private void Button_Analyse_Click(object sender, RoutedEventArgs e)
        {
            Analyse_OrtoDatasComparisons_Table();
            //Analyse_OrtoDataComparisons();
            //Analyse_OrtoDatas_SaveImage();
            //Report_Geometry(false);
            //Report_Occupancy();
        }

        private void Button_Calculate_Click(object sender, RoutedEventArgs e)
        {
            //Calculate();

            Calculate_OrtoDatas(100);

            //Test_CalculateConstructionDate();
            //Calculate_OrtoDatas();
        }

        private void Button_Reduce_Click(object sender, RoutedEventArgs e)
        {
            Reduce();
        }

        private void Calculate_Building2DGeometries()
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

            string[] paths_Input = Directory.GetFiles(directory, "*." + FileExtension.GISModelFile, SearchOption.AllDirectories);
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

        private void Calculate_AdministrativeAreal2DGeometries()
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

            string[] paths_Input = Directory.GetFiles(directory, "*." + FileExtension.GISModelFile, SearchOption.AllDirectories);
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
            Modify.CalculateGISModelFiles(this);
        }

        private void Analyse_AdministrativeAreal2Ds()
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

            string[] paths_Input = Directory.GetFiles(directory, "*." + FileExtension.GISModelFile, SearchOption.AllDirectories);

            string path_Output = System.IO.Path.Combine(directory, "administrativeArealNames.txt");

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

                string type = System.IO.Path.GetFileNameWithoutExtension(path_Input);
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

            string[] paths_Input = Directory.GetFiles(directory, "*." + FileExtension.GISModelFile, SearchOption.AllDirectories);
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

                string path_Output = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(path_Input), System.IO.Path.GetFileNameWithoutExtension(path_Input) + "_Out." + FileExtension.GISModelFile);
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
            Convert.ToDiGi_GISModelFiles(this);
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
                string directory_Buildings = System.IO.Path.Combine(directory_Temp, "Buildings");
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

                    Directory.Move(directory_Building, System.IO.Path.Combine(directory_Buildings, name));
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

        private void Analyse_Geometry(bool recalculate)
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

            string[] paths_Input = Directory.GetFiles(directory, "*." + FileExtension.GISModelFile, SearchOption.AllDirectories);
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

                    foreach (GuidReference guidReference in administrativeAreal2DBuilding2DsRelation.UniqueReferences_To)
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
                            building2DGeometryCalculationResult = DiGi.GIS.Create.Building2DGeometryCalculationResult(building2D);
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

                string path_Output = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(path_Input), System.IO.Path.GetFileNameWithoutExtension(path_Input) + "_GeometryReport.txt");

                File.WriteAllLines(path_Output, lines);
            };

            MessageBox.Show("Finished!");
        }

        private void Analyse_Occupancy()
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

            string[] paths_Input = Directory.GetFiles(directory, "*." + FileExtension.GISModelFile, SearchOption.AllDirectories);

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

                string path_Output = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(path_Input), string.Format("{0}_OccupacyReport.txt", System.IO.Path.GetFileNameWithoutExtension(path_Input)));

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

        private async void Calculate_OrtoDatas(int count = 10)
        {
            OrtoDatasOptions ortoDatasOptions = new OrtoDatasOptions()
            {
                MaxFileSize = (1024UL * 1024UL * 1024UL * 5) / 10
            };

            Modify.CalculateOrtoDatas(this, ortoDatasOptions, count);
        }

        private async void Calculate_OrtoDatasComparisons()
        {
            OrtoDatasComparisonOptions ortoDatasComparisonOptions = new OrtoDatasComparisonOptions();
            ortoDatasComparisonOptions.OrtoDatasOptions.MaxFileSize = (1024UL * 1024UL * 1024UL * 5) / 10;

            Modify.CalculateOrtoDatasComparisons(this, ortoDatasComparisonOptions);
        }

        private void Convert_ToFiles(int count = 10)
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

            string[] paths_Input = Directory.GetFiles(directory, "*." + FileExtension.OrtoDatasFile, SearchOption.AllDirectories);
            for (int i = 0; i < paths_Input.Length; i++)
            {
                string path_Input = paths_Input[i];

                using (OrtoDatasFile ortoDatasFile = new OrtoDatasFile(path_Input))
                {
                    ortoDatasFile.Open();

                    List<UniqueReference> uniqueReferences = ortoDatasFile.GetUniqueReferences()?.ToList();
                    if(uniqueReferences == null)
                    {
                        continue;
                    }

                    string directory_Temp = System.IO.Path.Combine(directory, "OrtoData");
                    if (!Directory.Exists(directory_Temp))
                    {
                        Directory.CreateDirectory(directory_Temp);
                    }

                    while (uniqueReferences.Count > 0)
                    {
                        int count_Temp = Math.Max(count, uniqueReferences.Count);

                        List<UniqueReference> uniqueReferences_Temp = uniqueReferences.GetRange(0, count_Temp);
                        uniqueReferences.RemoveRange(0, count_Temp);

                        IEnumerable<OrtoDatas> ortoDatasList = ortoDatasFile.GetValues<OrtoDatas>(uniqueReferences_Temp);
                        if(ortoDatasList != null)
                        {
                            foreach (OrtoDatas ortoDatas in ortoDatasList)
                            {
                                if(string.IsNullOrWhiteSpace(ortoDatas?.Reference))
                                {
                                    continue;
                                }

                                foreach(OrtoData ortoData in ortoDatas)
                                {
                                    if (ortoData?.Bytes == null || ortoData.Bytes.Length == 0)
                                    {
                                        continue;
                                    }

                                    string fileName = string.Format("{0}_{1}.{2}", ortoDatas.Reference, ortoData.DateTime.Year.ToString(), "jpeg");

                                    using (Image image = Image.FromStream(new MemoryStream(ortoData.Bytes)))
                                    {
                                        image.Save(System.IO.Path.Combine(directory_Temp, fileName), ImageFormat.Jpeg);
                                    }
                                }
                            }
                        }
                    }
                }
            };

            MessageBox.Show("Finished!");
        }

        private void Reduce()
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

            string[] paths_Input = Directory.GetFiles(directory, "*." + FileExtension.OrtoDatasFile, SearchOption.AllDirectories);
            for (int i = 0; i < paths_Input.Length; i++)
            {
                string path_Input = paths_Input[i];

                using (OrtoDatasFile ortoDatasFile = new OrtoDatasFile(path_Input))
                {
                    ortoDatasFile.Open();

                    string directory_Temp = @"C:\Users\jakub\Downloads\GIS Test\OrtoData";

                    IEnumerable<OrtoDatas> ortoDatasList = ortoDatasFile.Values;
                    if (ortoDatasList != null)
                    {
                        foreach (OrtoDatas ortoDatas in ortoDatasList)
                        {
                            if (ortoDatas == null)
                            {
                                continue;
                            }

                            string directory_Temp_Before = System.IO.Path.Combine(directory_Temp, "Before");
                            if(!Directory.Exists(directory_Temp_Before))
                            {
                                Directory.CreateDirectory(directory_Temp_Before);
                            }

                            foreach (OrtoData ortoData in ortoDatas)
                            {
                                if (ortoData?.Bytes == null || ortoData.Bytes.Length == 0)
                                {
                                    continue;
                                }

                                string fileName = string.Format("{0}_{1}.{2}", ortoDatas.Reference, ortoData.DateTime.Year.ToString(), "jpg");

                                using (Image image = Image.FromStream(new MemoryStream(ortoData.Bytes)))
                                {
                                    image.Save(System.IO.Path.Combine(directory_Temp_Before, fileName), ImageFormat.Jpeg);
                                }
                            }

                            ortoDatas.Reduce();

                            string directory_Temp_After = System.IO.Path.Combine(directory_Temp, "After");
                            if (!Directory.Exists(directory_Temp_After))
                            {
                                Directory.CreateDirectory(directory_Temp_After);
                            }

                            foreach (OrtoData ortoData in ortoDatas)
                            {
                                if (ortoData?.Bytes == null || ortoData.Bytes.Length == 0)
                                {
                                    continue;
                                }

                                string fileName = string.Format("{0}_{1}.{2}", ortoDatas.Reference, ortoData.DateTime.Year.ToString(), "jpg");

                                using (Image image = Image.FromStream(new MemoryStream(ortoData.Bytes)))
                                {
                                    image.Save(System.IO.Path.Combine(directory_Temp_After, fileName), ImageFormat.Jpeg);
                                }
                            }

                            ortoDatasFile.AddValue(ortoDatas);
                        }

                    }

                    ortoDatasFile.Save();

                }
            };

            MessageBox.Show("Finished!");
        }

        private void Analyse_OrtoDatas_GetBytes()
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

            string[] paths_Input = Directory.GetFiles(directory, "*." + FileExtension.OrtoDatasFile, SearchOption.AllDirectories);
            for (int i = 0; i < paths_Input.Length; i++)
            {
                string path_Input = paths_Input[i];

                using (OrtoDatasFile ortoDatasFile = new OrtoDatasFile(path_Input))
                {
                    ortoDatasFile.Open();

                    HashSet<UniqueReference> uniqueReferences = ortoDatasFile.GetUniqueReferences();
                    if (uniqueReferences == null)
                    {
                        continue;
                    }

                    List<OrtoDatas> ortoDatasList = new List<OrtoDatas>();
                    for (int j = 0; j < uniqueReferences.Count; j++)
                    {
                        OrtoDatas ortoDatas = ortoDatasFile.GetValue<OrtoDatas>(uniqueReferences.ElementAt(j));
                        if(ortoDatas == null)
                        {
                            continue;
                        }

                        byte[] bytes = ortoDatas.GetBytes(new DateTime(2023, 12, 12));

                    }
                }
            };

            MessageBox.Show("Finished!");
        }

        private void Analyse_OrtoDatas_SaveImage()
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

            string directory_Temp = System.IO.Path.Combine(directory, "OrtoData");
            if (!Directory.Exists(directory_Temp))
            {
                Directory.CreateDirectory(directory_Temp);
            }

            string[] paths_Input = Directory.GetFiles(directory, "*." + FileExtension.GISModelFile, SearchOption.AllDirectories);
            for (int i = 0; i < paths_Input.Length; i++)
            {
                string path_Input = paths_Input[i];

                using (GISModelFile gISModelFile = new GISModelFile(path_Input))
                {
                    gISModelFile.Open();

                    GISModel gISModel = gISModelFile.Value;
                    if(gISModel == null)
                    {
                        continue;
                    }

                    List<Building2D> building2Ds = gISModel.GetObjects<Building2D>();
                    if(building2Ds == null || building2Ds.Count == 0)
                    {
                        continue;
                    }

                    foreach(Building2D building2D in building2Ds)
                    {
                        OrtoDatas ortoDatas = gISModelFile.OrtoDatas(building2D);
                        if(ortoDatas == null)
                        {
                            continue;
                        }

                        Polygon2D polygon2D = building2D?.PolygonalFace2D?.ExternalEdge as Polygon2D;

                        List<Point2D> point2Ds = polygon2D?.GetPoints();
                        if (point2Ds == null)
                        {
                            continue;
                        }

                        Polygon2D polygon2D_Offset = polygon2D.Offset(0.5)?.FirstOrDefault();

                        List<Point2D> point2Ds_Offset = polygon2D_Offset?.GetPoints();

                        foreach (OrtoData ortoData in ortoDatas)
                        {
                            byte[] bytes = ortoData?.Bytes;
                            if(bytes == null)
                            {
                                continue;
                            }

                            string fileName = string.Format("{0}_{1}.{2}", ortoDatas.Reference, ortoData.DateTime.Year.ToString(), "jpeg");

                            using (Image image = Image.FromStream(new MemoryStream(bytes)))
                            {
                                List<Point2D> point2Ds_Temp = new List<Point2D>();
                                for (int j = 0; j < point2Ds.Count; j++)
                                {
                                    point2Ds_Temp.Add(ortoData.ToOrto(point2Ds[j]));
                                }

                                List<Point2D> point2Ds_Offset_Temp = null;
                                if(point2Ds_Offset != null)
                                {
                                    point2Ds_Offset_Temp = new List<Point2D>();
                                    for (int j = 0; j < point2Ds_Offset.Count; j++)
                                    {
                                        point2Ds_Offset_Temp.Add(ortoData.ToOrto(point2Ds_Offset[j]));
                                    }
                                }


                                using (Graphics graphics = Graphics.FromImage(image))
                                {
                                    Polygon2D polygon2D_OrtoData = new Polygon2D(point2Ds_Temp);
       
                                    Geometry.Drawing.Modify.Draw(graphics, polygon2D_OrtoData, new Pen(System.Drawing.Color.Black.ToDiGi(), 3), false);
                                    Geometry.Drawing.Modify.Draw(graphics, polygon2D_OrtoData.GetBoundingBox(), new Pen(System.Drawing.Color.Gray.ToDiGi(), 1), false);

                                    if(point2Ds_Offset_Temp != null)
                                    {
                                        Polygon2D polygon2D_OrtoData_Offset = new Polygon2D(point2Ds_Offset_Temp);

                                        Geometry.Drawing.Modify.Draw(graphics, polygon2D_OrtoData_Offset, new Pen(System.Drawing.Color.Red.ToDiGi(), 3), false);
                                        Geometry.Drawing.Modify.Draw(graphics, polygon2D_OrtoData_Offset.GetBoundingBox(), new Pen(System.Drawing.Color.Gray.ToDiGi(), 1), false);
                                    }
                                }
                                
                                image.Save(System.IO.Path.Combine(directory_Temp, fileName), ImageFormat.Jpeg);
                            }
                        }

                    }

                }
            };

            MessageBox.Show("Finished!");
        }

        private void Analyse_OrtoDataComparisons()
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

            string[] paths_Input = Directory.GetFiles(directory, "*." + FileExtension.GISModelFile, SearchOption.AllDirectories);
            for (int i = 0; i < paths_Input.Length; i++)
            {
                string path_Input = paths_Input[i];

                using (GISModelFile gISModelFile = new GISModelFile(path_Input))
                {
                    gISModelFile.Open();

                    GISModel gISModel = gISModelFile.Value;
                    if (gISModel != null)
                    {
                        List<Building2D> building2Ds = gISModel.GetObjects<Building2D>();
                        if (building2Ds != null)
                        {
                            for (int j = 0; j < building2Ds.Count; j++)
                            {
                                OrtoDatasComparison ortoDatasComparison = Emgu.CV.Create.OrtoDatasComparison(gISModelFile, building2Ds[i], new Range<int>(2008, 2024));
                                if(ortoDatasComparison == null)
                                {
                                    continue;
                                }                             

                            }
                        }
                    }
                }
            };

            MessageBox.Show("Finished!");
        }

        private void Test_CreateTestGISModelFile()
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

            string[] paths_Input = Directory.GetFiles(directory, "*." + FileExtension.GISModelFile, SearchOption.AllDirectories);
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

                string path_Output = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(path_Input), System.IO.Path.GetFileNameWithoutExtension(path_Input) + "_Out." + FileExtension.GISModelFile);
                using (GISModelFile gISModelFile = new GISModelFile(path_Output))
                {

                    GISModel gISModel_Output = new GISModel();
                    gISModel_Output.Update(building2D);
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

        private void Test_CalculateConstructionDate()
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

            string[] paths_Input = Directory.GetFiles(directory, "*." + FileExtension.GISModelFile, SearchOption.AllDirectories);
            for (int i = 0; i < paths_Input.Length; i++)
            {
                string path_Input = paths_Input[i];

                GISModel gISModel = null;

                using (GISModelFile gISModelFile = new GISModelFile(path_Input))
                {
                    gISModelFile.Open();
                    gISModel = gISModelFile.Value;
                }

                if(gISModel == null)
                {
                    continue;
                }

                List<Building2D> buidling2Ds = gISModel.GetObjects<Building2D>();
                if(buidling2Ds == null || buidling2Ds.Count == 0)
                {
                    continue;
                }

                foreach (Building2D building2D in buidling2Ds)
                {
                    gISModel.Update(building2D, new Building2DMachineLearningConstructionDateCalculationResult(new DateTime(2025, 1, 1)));
                }

                foreach (Building2D building2D in buidling2Ds)
                {
                    gISModel.Update(building2D, new Building2DManualConstructionDateCalculationResult(new DateTime(2024, 1, 1)));
                }

                foreach (Building2D building2D in buidling2Ds)
                {
                    gISModel.Update(building2D, new Building2DMachineLearningConstructionDateCalculationResult(new DateTime(2026, 1, 1)));
                }

                foreach (Building2D building2D in buidling2Ds)
                {
                    gISModel.Update(building2D, new Building2DManualConstructionDateCalculationResult(new DateTime(2025, 1, 1)));
                }


            };

            MessageBox.Show("Finished!");
        }

        private void Analyse_OrtoDatasComparisons_Table()
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

            string[] paths_Input = Directory.GetFiles(directory, "*." + Emgu.CV.Constans.FileExtension.OrtoDatasComparisonFile, SearchOption.AllDirectories);
            for (int i = 0; i < paths_Input.Length; i++)
            {
                string path_Input = paths_Input[i];

                using (OrtoDatasComparisonFile ortoDatasComparisonFile = new OrtoDatasComparisonFile(path_Input))
                {
                    ortoDatasComparisonFile.Open();

                    HashSet<UniqueReference> uniqueReferences = ortoDatasComparisonFile.GetUniqueReferences();
                    if (uniqueReferences == null)
                    {
                        continue;
                    }

                    IEnumerable<OrtoDatasComparison> ortoDatasComparisons = ortoDatasComparisonFile.GetValues<OrtoDatasComparison>(uniqueReferences);
                    if (ortoDatasComparisons == null)
                    {
                        continue;
                    }

                    Table table = Emgu.CV.Convert.ToDiGi_Table(ortoDatasComparisons);

                    Core.IO.DelimitedData.Modify.Write(table, System.IO.Path.Combine(System.IO.Path.GetDirectoryName(path_Input), "OrtoDatasComparison.txt"), '\t');
                }
            };

            MessageBox.Show("Finished!");
        }
    }
}