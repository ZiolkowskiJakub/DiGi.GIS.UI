using DiGi.Core.Interfaces;
using DiGi.Geometry.Planar.Classes;
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

            SQLite.Modify.Extract(new SQLite.Classes.SQLiteExtractOptions(path, directory) { UpdateExisting = false });

            //Modify.Extract(new DirectoryExtractOptions(path, directory) { UpdateExisting = true });
        }

        private void Button_Read_Click(object sender, RoutedEventArgs e)
        {

            GetData();
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

        private void Read_SQLite()
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

            string[] paths = Directory.GetFiles(directory, "*.sqlite3", SearchOption.AllDirectories);
            foreach(string path in paths)
            {
                List<ISerializableObject> serializableObjects = SQLite.Convert.ToDiGi<ISerializableObject>(path);
                if(serializableObjects != null)
                {

                }
            }

            //List<Building2D> building2Ds = Create.Building2Ds(directory);
        }

        private void GetData()
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

            string[] paths = Directory.GetFiles(directory, "*.sqlite3", SearchOption.AllDirectories);

            //List<Building2D> building2Ds = paths.ToList().ConvertAll(x => Core.Convert.ToDiGi<Building2D>((Core.Classes.Path?)x).FirstOrDefault());

            foreach(string path in paths)
            {
                List<Building2D> building2Ds = SQLite.Convert.ToDiGi<Building2D>(path);
                if(building2Ds == null)
                {
                    continue;
                }

                List<string> lines = new List<string>() { string.Join("\t", "Id", "Main Building Function", "Auxiliary Building Functions", "X", "Y", "Storeys", "Storey Area", "Total Area", "Thinness Ratio") };


                Dictionary<BuildingSpecificFunction, List<Building2D>> dictionary = new Dictionary<BuildingSpecificFunction, List<Building2D>>();
                foreach (Building2D building2D in building2Ds)
                {
                    if (building2D == null)
                    {
                        continue;
                    }

                    if (building2D.BuildingPhase != BuildingPhase.occupied)
                    {
                        continue;
                    }

                    if (building2D.BuildingGeneralFunction != BuildingGeneralFunction.residential_buildings)
                    {
                        continue;
                    }

                    if (building2D.BuildingSpecificFunctions == null)
                    {
                        continue;
                    }

                    if (!building2D.BuildingSpecificFunctions.Contains(BuildingSpecificFunction.single_family_building) && !building2D.BuildingSpecificFunctions.Contains(BuildingSpecificFunction.multi_family_building))
                    {
                        continue;
                    }

                    PolygonalFace2D polygonalFace2D = building2D.PolygonalFace2D;
                    if (polygonalFace2D == null)
                    {
                        continue;
                    }

                    List<BuildingSpecificFunction> buildingSpecificFunctions = new List<BuildingSpecificFunction>(building2D.BuildingSpecificFunctions);

                    BuildingSpecificFunction buildingSpecificFunction_Main = buildingSpecificFunctions.FirstOrDefault();

                    buildingSpecificFunctions.RemoveAt(0);
                    buildingSpecificFunctions.Sort((x, y) => Core.Query.Description(x).CompareTo(Core.Query.Description(y)));

                    double thinnessRatio = Geometry.Planar.Query.ThinnessRatio(polygonalFace2D.ExternalEdge);

                    Point2D point2D = polygonalFace2D.GetInternalPoint();

                    double area = polygonalFace2D.GetArea();

                    List<string> values = new List<string>();

                    values.Add(building2D.Reference);
                    values.Add(Core.Query.Description(buildingSpecificFunction_Main));
                    values.Add(string.Join("; ", buildingSpecificFunctions.ConvertAll(x => Core.Query.Description(x))));
                    values.Add(Core.Query.Round(point2D.X, Core.Constans.Tolerance.MacroDistance).ToString());
                    values.Add(Core.Query.Round(point2D.Y, Core.Constans.Tolerance.MacroDistance).ToString());
                    values.Add(building2D.Storeys.ToString());
                    values.Add(Core.Query.Round(area, Core.Constans.Tolerance.MacroDistance).ToString());
                    values.Add(Core.Query.Round(area * System.Convert.ToDouble(building2D.Storeys), Core.Constans.Tolerance.MacroDistance).ToString());
                    values.Add(Core.Query.Round(thinnessRatio, Core.Constans.Tolerance.Distance).ToString());

                    lines.Add(string.Join("\t", values));

                    foreach (BuildingSpecificFunction buildingSpecificFunction in building2D.BuildingSpecificFunctions)
                    {
                        if (!dictionary.TryGetValue(buildingSpecificFunction, out List<Building2D> building2Ds_Temp) || building2Ds_Temp == null)
                        {
                            building2Ds_Temp = new List<Building2D>();
                            dictionary[buildingSpecificFunction] = building2Ds_Temp;
                        }

                        building2Ds_Temp.Add(building2D);
                    }
                }

                File.WriteAllLines(Path.Combine(Path.GetDirectoryName(path), "buildings.txt"), lines);

                lines = new List<string>() { string.Join("\t", "Building Function", "Count", "Total Area", "Avg. Storey Area", "Avg. Building Area", "Avg. Thinness Ratio") };

                foreach (KeyValuePair<BuildingSpecificFunction, List<Building2D>> keyValuePair in dictionary)
                {
                    int count = keyValuePair.Value.Count;
                    double storeys = 0;
                    double storeyArea = 0;
                    double totalArea = 0;
                    double thinnesRatio = 0;

                    foreach(Building2D building2D in keyValuePair.Value)
                    {
                        PolygonalFace2D polygonalFace2D = building2D.PolygonalFace2D;

                        double storeys_Building = System.Convert.ToDouble(building2D.Storeys);
                        double area_Building = polygonalFace2D.GetArea();
                        double thinnessRatio_Building = Geometry.Planar.Query.ThinnessRatio(polygonalFace2D.ExternalEdge);

                        storeys += storeys_Building;
                        storeyArea += area_Building;
                        totalArea += area_Building * storeys_Building;
                        thinnesRatio += thinnessRatio_Building;
                    }

                    List<string> values = new List<string>();

                    values.Add(Core.Query.Description(keyValuePair.Key));
                    values.Add(count.ToString());
                    values.Add(Core.Query.Round(totalArea, Core.Constans.Tolerance.MacroDistance).ToString());

                    storeyArea = storeyArea / count;
                    totalArea = totalArea / count;
                    thinnesRatio = thinnesRatio / count;

                    values.Add(Core.Query.Round(storeyArea , Core.Constans.Tolerance.MacroDistance).ToString());
                    values.Add(Core.Query.Round(totalArea, Core.Constans.Tolerance.MacroDistance).ToString());
                    values.Add(Core.Query.Round(thinnesRatio, Core.Constans.Tolerance.MacroDistance).ToString());

                    lines.Add(string.Join("\t", values));
                }

                File.WriteAllLines(Path.Combine(Path.GetDirectoryName(path), "summary.txt"), lines);
            }
        }

        private void CreateOrto()
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

            string[] paths = Directory.GetFiles(directory, "*.sqlite3", SearchOption.AllDirectories);
            foreach (string path in paths)
            {
                List<Building2D> building2Ds = SQLite.Convert.ToDiGi<Building2D>(path);
                if (building2Ds == null)
                {
                    continue;
                }

                string directory_Orto = Path.Combine(Path.GetDirectoryName(path), "orto"); 
                if(!Directory.Exists(directory_Orto))
                {
                    Directory.CreateDirectory(directory_Orto);
                }

                building2Ds.Write(directory_Orto, new Core.Classes.Range<int>(2004, 2024));
            }
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

            foreach(string directory_Temp in Directory.GetDirectories(directory))
            {
                string directory_Buildings = Path.Combine(directory_Temp, "Buildings");
                if(!Directory.Exists(directory_Buildings))
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
    }
}