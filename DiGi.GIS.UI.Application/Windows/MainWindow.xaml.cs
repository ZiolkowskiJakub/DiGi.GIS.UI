using DiGi.Analytical.Building.Classes;
using DiGi.BDL.Classes;
using DiGi.BDL.Enums;
using DiGi.Core;
using DiGi.Core.Classes;
using DiGi.Geometry.Spatial.Classes;
using DiGi.GIS.Classes;
using DiGi.GIS.Constans;
using DiGi.GIS.Emgu.CV.Classes;
using DiGi.GIS.UI.Classes;
using Microsoft.Win32;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace DiGi.GIS.UI.Application.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DiGi.UI.WPF.Core.Classes.DeterminateWindowWorker determinateWindowWorker;

        public MainWindow()
        {
            InitializeComponent();

            determinateWindowWorker = new DiGi.UI.WPF.Core.Classes.DeterminateWindowWorker(this);

            this.Closed += MainWindow_Closed;
        }

        private static async void BDLMatchTest()
        {
            OpenFileDialog openFileDialog_Json = new OpenFileDialog();
            openFileDialog_Json.Filter = "json files (*.json)|*.json|All files (*.*)|*.*";
            bool? openFileDialog_Json_Result = openFileDialog_Json.ShowDialog();
            if (openFileDialog_Json_Result == null || !openFileDialog_Json_Result.HasValue || !openFileDialog_Json_Result.Value)
            {
                return;
            }

            string json = File.ReadAllText(openFileDialog_Json.FileName);
            List<Unit> units = JsonSerializer.Deserialize<List<Unit>>(json);

            StatisticalUnit statisticalUnit = GIS.Create.StatisticalUnit(units);

            OpenFileDialog openFileDialog_GISModelFile = new OpenFileDialog();
            openFileDialog_GISModelFile.Filter = "GISModelFile files (*.gmf)|*.gmf|All files (*.*)|*.*";
            bool? openFileDialog_GISModelFile_Result = openFileDialog_GISModelFile.ShowDialog();
            if (openFileDialog_GISModelFile_Result == null || !openFileDialog_GISModelFile_Result.HasValue || !openFileDialog_GISModelFile_Result.Value)
            {
                return;
            }

            GISModel gISModel = null;
            using (GISModelFile gISModelFile = new GISModelFile(openFileDialog_GISModelFile.FileName))
            {
                gISModelFile.Open();
                gISModel = gISModelFile.Value;
            }

            List<AdministrativeAreal2D> administrativeAreal2Ds = gISModel?.GetObjects<AdministrativeAreal2D>();
            if (administrativeAreal2Ds == null)
            {
                return;
            }

            List<string> names = new List<string>();
            foreach (AdministrativeAreal2D administrativeAreal2D in administrativeAreal2Ds)
            {
                names.Add(administrativeAreal2D.Name);
            }

        }

        private static async void BDLTest()
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "json files (*.json)|*.json|All files (*.*)|*.*";
            bool? openFileDialog_Result = openFileDialog.ShowDialog();
            if (openFileDialog_Result == null || !openFileDialog_Result.HasValue || !openFileDialog_Result.Value)
            {
                return;
            }

            string json = File.ReadAllText(openFileDialog.FileName);
            List<Unit> units = JsonSerializer.Deserialize<List<Unit>>(json);

            StatisticalUnit statisticalUnit = GIS.Create.StatisticalUnit(units);

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = string.Format("{0} (*.{1})|*.{1}|All files (*.*)|*.*", FileTypeName.StatisticalUnitFile, FileExtension.StatisticalUnitFile);
            bool? saveFileDialog_Result = saveFileDialog.ShowDialog();
            if (saveFileDialog_Result == null || !saveFileDialog_Result.HasValue || !saveFileDialog_Result.Value)
            {
                return;
            }

            using (StatisticalUnitFile statisticalUnitFile = new StatisticalUnitFile(saveFileDialog.FileName))
            {
                statisticalUnitFile.Value = statisticalUnit;
                statisticalUnitFile.Save();
            }



            //SaveFileDialog saveFileDialog = new SaveFileDialog();
            //saveFileDialog.Filter = "json files (*.json)|*.json|All files (*.*)|*.*";
            //bool? saveFileDialog_Result = saveFileDialog.ShowDialog();
            //if (saveFileDialog_Result == null || !saveFileDialog_Result.HasValue || !saveFileDialog_Result.Value)
            //{
            //    return;
            //}

            ////Write
            ////List<Unit> units = await BDL.Create.Units();
            ////string json = JsonSerializer.Serialize(units, new JsonSerializerOptions() { WriteIndented = true });
            ////File.WriteAllText(saveFileDialog.FileName, json);

            //IEnumerable<BDL.Enums.Variable> variables = Enum.GetValues(typeof(BDL.Enums.Variable)).Cast<BDL.Enums.Variable>();

            //List<int> years = new List<int>();
            //for (int i = 2008; i <= DateTime.Now.Year; i++)
            //{
            //    years.Add(i);
            //}

            //List<UnitYearlyValues> unitYearlyValuesList = new List<UnitYearlyValues>();
            //foreach (Unit unit in units)
            //{
            //    UnitYearlyValues unitYearlyValues = await BDL.Create.UnitYearlyValues(unit.id, variables, years, 50);

            //    if (unitYearlyValues == null)
            //    {
            //        continue;
            //    }

            //    unitYearlyValuesList.Add(unitYearlyValues);
            //}

            //json = JsonSerializer.Serialize(unitYearlyValuesList, new JsonSerializerOptions() { WriteIndented = true });
            //File.WriteAllText(saveFileDialog.FileName, json);
        }

        private static void CopyGISModelFiles()
        {
            bool? result;

            string fileNameSufix = "_StatisticalUnits";

            OpenFolderDialog openFolderDialog = new OpenFolderDialog();
            result = openFolderDialog.ShowDialog();
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

            Core.Query.Filter(paths_GISModelFile, x => System.IO.Path.GetFileNameWithoutExtension(x).EndsWith(fileNameSufix), out List<string> paths_GISModelFile_New, out List<string> paths_GISModelFile_Old);

            if (paths_GISModelFile_New == null || paths_GISModelFile_Old == null)
            {
                return;
            }

            if (paths_GISModelFile_New.Count != paths_GISModelFile_Old.Count)
            {
                return;
            }

            foreach (string path_GISModelFile_Old in paths_GISModelFile_Old)
            {
                if (File.Exists(path_GISModelFile_Old))
                {
                    File.Delete(path_GISModelFile_Old);
                }
            }

            foreach (string path_GISModelFile_New in paths_GISModelFile_New)
            {
                string path = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(path_GISModelFile_New), System.IO.Path.GetFileNameWithoutExtension(path_GISModelFile_New).Replace(fileNameSufix, string.Empty) + System.IO.Path.GetExtension(path_GISModelFile_New));

                File.Move(path_GISModelFile_New, path);
            }


        }

        private static void CopyGISModelFiles_Cloud()
        {
            bool? result;

            OpenFolderDialog openFolderDialog;


            openFolderDialog = new OpenFolderDialog();
            result = openFolderDialog.ShowDialog();
            if (result == null || !result.HasValue || !result.Value)
            {
                return;
            }

            string directory_Source = openFolderDialog.FolderName;

            List<string> paths_GISModelFile_Source = Directory.GetFiles(directory_Source, "*." + FileExtension.GISModelFile, SearchOption.AllDirectories).ToList();
            if (paths_GISModelFile_Source == null || paths_GISModelFile_Source.Count == 0)
            {
                return;
            }

            openFolderDialog = new OpenFolderDialog();
            result = openFolderDialog.ShowDialog();
            if (result == null || !result.HasValue || !result.Value)
            {
                return;
            }

            string directory_Destination = openFolderDialog.FolderName;

            List<string> paths_GISModelFile_Destination = Directory.GetFiles(directory_Destination, "*." + FileExtension.GISModelFile, SearchOption.AllDirectories).ToList();
            if (paths_GISModelFile_Destination == null || paths_GISModelFile_Destination.Count == 0)
            {
                return;
            }

            foreach(string path_GISModelFile_Source in paths_GISModelFile_Source)
            {
                string fileName = System.IO.Path.GetFileNameWithoutExtension(path_GISModelFile_Source);

                string path_GISModelFile_Destination = paths_GISModelFile_Destination.Find(x => System.IO.Path.GetFileNameWithoutExtension(x) == fileName);

                if(File.Exists(path_GISModelFile_Source) && File.Exists(path_GISModelFile_Destination))
                {
                    File.Copy(path_GISModelFile_Source, path_GISModelFile_Destination, true);
                }
            }
        }

        private static void CreateAdministrativeAreal2DModel()
        {
            OpenFolderDialog openFolderDialog = new OpenFolderDialog();
            bool? result = openFolderDialog.ShowDialog();
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
            if (paths_Input == null || paths_Input.Length == 0)
            {
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = string.Format("{0} (*.{1})|*.{1}|All files (*.*)|*.*", FileTypeName.GISModelFile, FileExtension.GISModelFile);
            result = saveFileDialog.ShowDialog();
            if (result == null || !result.HasValue || !result.Value)
            {
                return;
            }

            Dictionary<string, AdministrativeAreal2D> dictionary = new Dictionary<string, AdministrativeAreal2D>();

            for (int i = 0; i < paths_Input.Length; i++)
            {
                string path_Input = paths_Input[i];

                using (GISModelFile gISModelFile = new GISModelFile(path_Input))
                {
                    gISModelFile.Open();

                    List<AdministrativeAreal2D> administrativeAreal2Ds = gISModelFile.Value.GetObjects<AdministrativeAreal2D>();
                    if (administrativeAreal2Ds != null)
                    {
                        foreach (AdministrativeAreal2D administrativeAreal2D in administrativeAreal2Ds)
                        {
                            if (string.IsNullOrWhiteSpace(administrativeAreal2D?.Reference))
                            {
                                continue;
                            }

                            dictionary[administrativeAreal2D.Reference] = administrativeAreal2D;
                        }
                    }

                }
            }


            GISModel gISModel = new GISModel();
            foreach (AdministrativeAreal2D administrativeAreal2D in dictionary.Values)
            {
                gISModel.Update(administrativeAreal2D);
            }

            gISModel.CalculateAdministrativeAreal2DGeometries();
            gISModel.CalculateAdministrativeAreal2DAdministrativeAreal2Ds();

            using (GISModelFile gISModelFile = new GISModelFile(saveFileDialog.FileName))
            {
                gISModelFile.Value = gISModel;
                gISModelFile.Save();
            }

            MessageBox.Show("Finished!");
        }

        private static void GISFileModelTest()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "GIS Model files (*.gmf)|*.gmf|All files (*.*)|*.*";
            bool? result = openFileDialog.ShowDialog();
            if (result == null || !result.HasValue || !result.Value)
            {
                return;
            }

            string path = openFileDialog.FileName;
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
            {
                return;
            }

            using (GISModelFile gISModelFile = new GISModelFile(path))
            {
                gISModelFile.Open();

                GISModel gISModel = gISModelFile.Value;

                List<Building2D> building2Ds = gISModel.GetObjects<Building2D>();
                if (building2Ds != null && building2Ds.Count > 0)
                {
                    Building2D building2D = building2Ds[0];

                    List<AdministrativeAreal2D> administrativeAreal2Ds = GIS.Query.AdministrativeAreal2Ds<AdministrativeAreal2D>(gISModel, building2D);


                }

            }
        }

        private void Analyse_OrtoDatasComparisons_Table()
        {


            MessageBox.Show("Finished!");
        }

        private void AppendBuildingModels()
        {
            DateTime dateTime = DateTime.Now;

            TextBlock_Progress.Text = "Append Building Models...";

            Modify.AppendBuildingModels(this);

            TimeSpan timeSpan = new TimeSpan((DateTime.Now - dateTime).Ticks);

            TextBlock_Progress.Text = string.Format("Done Appending! [{0}]", string.Format("{0}d:{1}h:{2}m:{3}s", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds));
        }

        private void AppendPredictionYearBuilts()
        {
            bool includeOrtoRange = false;

            DateTime dateTime = DateTime.Now;

            TextBlock_Progress.Text = "Appending Prediction Year Builts...";

            Modify.AppendPredictionYearBuilts(this);

            TimeSpan timeSpan = new TimeSpan((DateTime.Now - dateTime).Ticks);

            TextBlock_Progress.Text = string.Format("Done Appending Prediction Year Builts! [{0}]", string.Format("{0}d:{1}h:{2}m:{3}s", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds));
        }

        private void AppendTable()
        {
            DateTime dateTime = DateTime.Now;

            TextBlock_Progress.Text = "Appending...";

            Modify.AppendPredictionTable(this);

            TimeSpan timeSpan = new TimeSpan((DateTime.Now - dateTime).Ticks);

            TextBlock_Progress.Text = string.Format("Done Appending! [{0}]", string.Format("{0}d:{1}h:{2}m:{3}s", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds));
        }

        private void AppendVoTTModel_OrtoRange()
        {
            DateTime dateTime = DateTime.Now;

            TextBlock_Progress.Text = "Appending VoTT Model...";

            Modify.AppendVoTTModel_OrtoRange(this);

            TimeSpan timeSpan = new TimeSpan((DateTime.Now - dateTime).Ticks);

            TextBlock_Progress.Text = string.Format("Done Appending VoTT Model! [{0}]", string.Format("{0}d:{1}h:{2}m:{3}s", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds));
        }

        private void AppendYOLOModel()
        {
            bool includeOrtoRange = false;

            DateTime dateTime = DateTime.Now;

            TextBlock_Progress.Text = "Appending YOLO Model...";

            YOLOConversionOptions yOLOConversionOptions = new YOLOConversionOptions();
            yOLOConversionOptions.ClearData = true;

            yOLOConversionOptions[YOLO.Enums.Category.Train] = 0.9;
            yOLOConversionOptions[YOLO.Enums.Category.Validate] = 0.1;
            yOLOConversionOptions[YOLO.Enums.Category.Test] = 0;

            Modify.AppendYOLOModel_Building2D(this, yOLOConversionOptions);
            
            if(includeOrtoRange)
            {
                yOLOConversionOptions.ClearData = false;
                Modify.AppendYOLOModel_OrtoRange(this, yOLOConversionOptions);
            }

            TimeSpan timeSpan = new TimeSpan((DateTime.Now - dateTime).Ticks);

            TextBlock_Progress.Text = string.Format("Done Appending YOLO Model! [{0}]", string.Format("{0}d:{1}h:{2}m:{3}s", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds));
        }
        
        private void Button_AppendBuildingModels_Click(object sender, RoutedEventArgs e)
        {
            AppendBuildingModels();
        }

        private void Button_AppendPredictions_Click(object sender, RoutedEventArgs e)
        {
            DateTime dateTime = DateTime.Now;

            TextBlock_Progress.Text = "Appending Predictions...";

            Modify.AppendBuilding2DYearBuiltPredictionsFile(this);

            TimeSpan timeSpan = new TimeSpan((DateTime.Now - dateTime).Ticks);

            TextBlock_Progress.Text = string.Format("Done Appending Predictions! [{0}]", string.Format("{0}d:{1}h:{2}m:{3}s", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds));

        }

        private void Button_AppendPredictionYearBuilts_Click(object sender, RoutedEventArgs e)
        {
            AppendPredictionYearBuilts();
        }

        private void Button_AppendTable_Click(object sender, RoutedEventArgs e)
        {
            AppendTable();
        }

        private void Button_AppendVoTTModel_Click(object sender, RoutedEventArgs e)
        {
            AppendVoTTModel_OrtoRange();
        }

        private void Button_AppendYOLOModel_Click(object sender, RoutedEventArgs e)
        {
            AppendYOLOModel();
        }

        private void Button_Calculate_Click(object sender, RoutedEventArgs e)
        {
            //Calculate();



            //Test_CalculateConstructionDate();
            //Calculate_OrtoDatas();
        }

        private void Button_CalculateAdministrativeAreal2DStatisticalUnits_Click(object sender, RoutedEventArgs e)
        {
            CalculateAdministrativeAreal2DStatisticalUnits();
        }

        private void Button_CalculateGISModelFiles_Click(object sender, RoutedEventArgs e)
        {
            CalculateGISModelFiles();
        }

        private void Button_CalculateOrtoDatas_Building2D_Click(object sender, RoutedEventArgs e)
        {
            CalculateOrtoDatas_Building2D(100);
        }

        private void Button_CalculateOrtoDatas_OrtoRange_Click(object sender, RoutedEventArgs e)
        {
            CalculateOrtoDatas_OrtoRange(100);
        }

        private void Button_CalculateOrtoDatasComparisons_Click(object sender, RoutedEventArgs e)
        {
            CalculateOrtoDatasComparisons();
        }

        private void Button_CalculateOrtoRanges_Click(object sender, RoutedEventArgs e)
        {
            CalculateOrtoRanges();
        }

        private void Button_OrtoDatas_Click(object sender, RoutedEventArgs e)
        {
            Hide();

            UI.Windows.OrtoDatasWindow yearBuiltsWindow = new UI.Windows.OrtoDatasWindow();
            yearBuiltsWindow.WindowState = WindowState.Maximized;

            yearBuiltsWindow.ShowDialog();

            Close();
        }

        private void Button_Reduce_Click(object sender, RoutedEventArgs e)
        {
            Reduce();
        }

        private void Button_ResaveOrtoDatasFiles_Click(object sender, RoutedEventArgs e)
        {
            ResaveOrtoDatasFiles();
        }

        private async void Button_Test_Click(object sender, RoutedEventArgs e)
        {
            OpenFolderDialog openFolderDialog = new OpenFolderDialog();
            bool? result = openFolderDialog.ShowDialog(this);
            if (result == null || !result.HasValue || !result.Value)
            {
                return;
            }

            string[] paths_GISModel = Directory.GetFiles(openFolderDialog.FolderName, "*." + FileExtension.GISModelFile, SearchOption.AllDirectories);
            for (int i = 0; i < paths_GISModel.Length; i++)
            {
                string path_GISModel = paths_GISModel[i];

                string path_BuidlingModelsFile = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(path_GISModel), System.IO.Path.GetFileNameWithoutExtension(path_GISModel) + "." + Analytical.Constans.FileExtension.BuildingModelsFile);

                if(!File.Exists(path_BuidlingModelsFile))
                {
                    continue;
                }

                List<Building2D> building2Ds = null;
                using (GISModelFile gISModelFile = new GISModelFile(path_GISModel))
                {
                    gISModelFile.Open();

                    building2Ds = gISModelFile?.Value?.GetObjects<Building2D>();
                }

                if(building2Ds == null || building2Ds.Count == 0)
                {
                    continue;
                }

                List<Building2D> building2Ds_Invalid = new List<Building2D>();

                Dictionary<Analytical.Enums.LOD, List<BuildingModel>> dictionary = new Dictionary<Analytical.Enums.LOD, List<BuildingModel>>();
                using (BuildingModelsFile buildingModelsFile = new BuildingModelsFile(path_BuidlingModelsFile))
                {
                    buildingModelsFile.Open();

                    foreach(Building2D building2D in building2Ds)
                    {
                        UniqueReference uniqueReference = BuildingModelsFile.GetUniqueReference(building2D?.Reference);
                        if(uniqueReference == null)
                        {
                            continue;
                        }

                        BuildingModel buildingModel = buildingModelsFile.GetValue<BuildingModel>(uniqueReference);
                        if(buildingModel == null)
                        {
                            building2Ds_Invalid.Add(building2D);
                        }

                        if (!buildingModel.TryGetValue(Analytical.Enums.BuildingModelParameter.LOD, out Analytical.Enums.LOD lOD, new Core.Parameter.Classes.GetValueSettings(true, false)))
                        {
                            lOD = Analytical.Enums.LOD.Undefined;
                        }

                        if (!dictionary.TryGetValue(lOD, out List<BuildingModel> buildingModels))
                        {
                            buildingModels = new List<BuildingModel>();
                            dictionary[lOD] = buildingModels;
                        }

                        buildingModels.Add(buildingModel);

                        Point3D point3D = GIS.Convert.ToEPSG4326(building2D.PolygonalFace2D.ExternalEdge.GetInternalPoint());

                    }

                }
            }








            //List<BuildingModel> buildingModels = Create.BuildingModels(this);


            //OpenFolderDialog openFolderDialog = new OpenFolderDialog();
            //bool? result = openFolderDialog.ShowDialog(this);
            //if (result == null || !result.HasValue || !result.Value)
            //{
            //    return;
            //}

            //string[] paths = Directory.GetFiles(openFolderDialog.FolderName, "*.zip");
            //foreach(string path in paths)
            //{
            //    string fileName = System.IO.Path.GetFileNameWithoutExtension(path);
            //    if(!fileName.EndsWith("_gml"))
            //    {
            //        continue;
            //    }

            //    fileName = fileName.Substring(0, fileName.Length - 4);

            //    string path_New = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(path), fileName + System.IO.Path.GetExtension(path));
            //    if(File.Exists(path_New))
            //    {
            //        continue;
            //    }

            //    File.Copy(path, path_New, true);
            //    File.Delete(path);
            //}


            //HashSet<string> paths = await Modify.Download3DModels(this);

            //WriteImages();

            //Modify.WriteAdministrativeAreal2DNames(this, @"C:\Users\jakub\Downloads\GIS\AdministrativeAreal2D.txt");
            //CategoryTest();

            //CopyGISModelFiles_Cloud();

            //CreateAdministrativeAreal2DModel();

            //CalculateAdministrativeAreal2DStatisticalUnits();

            //string path = @"C:\Users\jakub\Downloads\GIS\Statistics\StatisticalDataCollections.sdcf";

            //using (StatisticalDataCollectionFile statisticalDataCollectionFile = new StatisticalDataCollectionFile(path))
            //{
            //    statisticalDataCollectionFile.Open();

            //    IEnumerable<StatisticalDataCollection> statisticalDataCollection = statisticalDataCollectionFile.Values;
            //}

            //List<Building2D> building2Ds = null;

            //string directory = @"C:\Users\jakub\Downloads\GIS\0201_GML\";

            //string path = System.IO.Path.Combine(directory, "0201_GML.gmf");
            //using (GISModelFile gISModelFile = new GISModelFile(path))
            //{
            //    gISModelFile.Open();

            //    GISModel gISModel = gISModelFile.Value;

            //    building2Ds = gISModel.GetObjects<Building2D>();
            //}

            //if(building2Ds == null)
            //{
            //    return;
            //}

            //OrtoDatasComparisonOptions ortoDatasComparisonOptions = new OrtoDatasComparisonOptions()
            //{
            //    OverrideExisting = true
            //};

            //foreach (Building2D building2D in building2Ds)
            //{
            //    string reference = building2D.Reference;

            //    Dictionary<string, OrtoDatasComparison> dictionary;
            //    OrtoDatasComparison ortoDatasComparison;

            //    dictionary = Emgu.CV.Query.OrtoDatasComparisonDictionary(directory, [reference]);
            //    if(dictionary == null || !dictionary.TryGetValue(reference, out ortoDatasComparison))
            //    {
            //        continue;
            //    }

            //    HashSet<string> references = await Modify.CalculateOrtoDatasComparisons(directory, ortoDatasComparisonOptions, [reference]);
            //    if(references == null || !references.Contains(reference))
            //    {
            //        continue;
            //    }

            //    dictionary = Emgu.CV.Query.OrtoDatasComparisonDictionary(directory, [reference]);
            //    if (dictionary == null || !dictionary.TryGetValue(reference, out ortoDatasComparison))
            //    {
            //        continue;
            //    }

            //}

            //OpenFolderDialog openFolderDialog = new OpenFolderDialog();
            //bool? result = openFolderDialog.ShowDialog(this);
            //if (result == null || !result.HasValue || !result.Value)
            //{
            //    return;
            //}

            //string[] paths_Input = Directory.GetFiles(openFolderDialog.FolderName, "*." + FileExtension.YearBuiltDataFile, SearchOption.AllDirectories);
            //for (int i = 0; i < paths_Input.Length; i++)
            //{
            //    string path = paths_Input[i];

            //    //File.Copy(path, path + ".bak", true);

            //    using (YearBuiltDataFile yearBuiltDataFile = new YearBuiltDataFile(path))
            //    {
            //        IEnumerable<YearBuiltData> yearBuiltDatas =  yearBuiltDataFile.GetValues<YearBuiltData>();
            //        if(yearBuiltDatas == null || yearBuiltDatas.Count() == 0)
            //        {
            //            continue;
            //        }

            //        foreach(YearBuiltData yearBuiltData in yearBuiltDatas)
            //        {
            //            //yearBuiltData.Add(new UserYearBuilt(yearBuiltData.Year));

            //            //if (yearBuiltData.Year.Equals(yearBuiltData.GetUserYearBuilt().Year))
            //            //{
            //            //    continue;
            //            //}

            //            yearBuiltDataFile.AddValue(yearBuiltData);
            //        }

            //        yearBuiltDataFile.Save();

            //    }

            //}


        }

        private void Button_ToDiGiGISModelFiles_Click(object sender, RoutedEventArgs e)
        {
            ToDiGiGISModelFiles();
        }

        private void Button_WriteStatisticalDataCollections_Click(object sender, RoutedEventArgs e)
        {
            WriteStatisticalDataCollections();
        }

        private void Button_YearBuilts_Click(object sender, RoutedEventArgs e)
        {
            Hide();

            UI.Windows.YearBuiltsWindow yearBuiltsWindow = new UI.Windows.YearBuiltsWindow();
            yearBuiltsWindow.WindowState = WindowState.Maximized;

            yearBuiltsWindow.ShowDialog();

            Close();
        }

        private void CalculateAdministrativeAreal2DStatisticalUnits()
        {
            DateTime dateTime = DateTime.Now;

            TextBlock_Progress.Text = "Calculating...";

            Modify.CalculateAdministrativeAreal2DStatisticalUnits(this, true, "_StatisticalUnits");

            TimeSpan timeSpan = new TimeSpan((DateTime.Now - dateTime).Ticks);

            TextBlock_Progress.Text = string.Format("Done Calculating! [{0}]", string.Format("{0}d:{1}h:{2}m:{3}s", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds));

        }

        private async void CalculateGISModelFiles()
        {
            DateTime dateTime = DateTime.Now;

            TextBlock_Progress.Text = "Calculating...";

            await Modify.CalculateGISModelFilesAsync(this, determinateWindowWorker);

            TimeSpan timeSpan = new TimeSpan((DateTime.Now - dateTime).Ticks);

            TextBlock_Progress.Text = string.Format("Done Calculating! [{0}]", string.Format("{0}d:{1}h:{2}m:{3}s", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds));
        }

        private async void CalculateOrtoDatas_Building2D(int count = 100)
        {
            DateTime dateTime = DateTime.Now;

            TextBlock_Progress.Text = "Calculating...";

            OrtoDatasBuilding2DOptions ortoDatasBuilding2DOptions = new OrtoDatasBuilding2DOptions()
            {
                MaxFileSize = (1024UL * 1024UL * 1024UL * 5) / 10
            };

            bool result = await Modify.CalculateOrtoDatas(this, ortoDatasBuilding2DOptions, count);

            TimeSpan timeSpan = new TimeSpan((DateTime.Now - dateTime).Ticks);

            TextBlock_Progress.Text = string.Format("Done Calculating! [{0}]", string.Format("{0}d:{1}h:{2}m:{3}s", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds));
        }

        private async void CalculateOrtoDatas_OrtoRange(int count = 100)
        {
            DateTime dateTime = DateTime.Now;

            TextBlock_Progress.Text = "Calculating...";

            OrtoDatasOrtoRangeOptions ortoDatasOrtoRangeOptions = new OrtoDatasOrtoRangeOptions()
            {
                MaxFileSize = (1024UL * 1024UL * 1024UL * 5) / 10
            };

            bool result = await Modify.CalculateOrtoDatas(this, ortoDatasOrtoRangeOptions, count);

            TimeSpan timeSpan = new TimeSpan((DateTime.Now - dateTime).Ticks);

            TextBlock_Progress.Text = string.Format("Done Calculating! [{0}]", string.Format("{0}d:{1}h:{2}m:{3}s", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds));
        }

        private async void CalculateOrtoDatasComparisons()
        {
            DateTime dateTime = DateTime.Now;

            TextBlock_Progress.Text = "Calculating...";

            OrtoDatasComparisonOptions ortoDatasComparisonOptions = new OrtoDatasComparisonOptions()
            {
                DirectoryNames = GIS.Query.OrtoDatasDirectoryNames_Building2D()
            };
            //ortoDatasComparisonOptions.OrtoDatasOptions.MaxFileSize = (1024UL * 1024UL * 1024UL * 5) / 10;

            Modify.CalculateOrtoDatasComparisons(this, ortoDatasComparisonOptions);

            TimeSpan timeSpan = new TimeSpan((DateTime.Now - dateTime).Ticks);

            TextBlock_Progress.Text = string.Format("Done Calculating! [{0}]", string.Format("{0}d:{1}h:{2}m:{3}s", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds));
        }

        private async void CalculateOrtoRanges()
        {
            DateTime dateTime = DateTime.Now;

            TextBlock_Progress.Text = "Calculating...";

            OrtoRangeOptions ortoRangeOptions = new OrtoRangeOptions()
            {

            };

            Modify.CalculateOrtoRanges(this, ortoRangeOptions);

            TimeSpan timeSpan = new TimeSpan((DateTime.Now - dateTime).Ticks);

            TextBlock_Progress.Text = string.Format("Done Calculating! [{0}]", string.Format("{0}d:{1}h:{2}m:{3}s", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds));
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

            TextBlock_Progress.Text = "Converting...";

            string[] paths_Input = Directory.GetFiles(directory, "*." + FileExtension.OrtoDatasFile, SearchOption.AllDirectories);
            for (int i = 0; i < paths_Input.Length; i++)
            {
                string path_Input = paths_Input[i];

                using (OrtoDatasFile ortoDatasFile = new OrtoDatasFile(path_Input))
                {
                    ortoDatasFile.Open();

                    List<UniqueReference> uniqueReferences = ortoDatasFile.GetUniqueReferences()?.ToList();
                    if (uniqueReferences == null)
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
                        if (ortoDatasList != null)
                        {
                            foreach (OrtoDatas ortoDatas in ortoDatasList)
                            {
                                if (string.IsNullOrWhiteSpace(ortoDatas?.Reference))
                                {
                                    continue;
                                }

                                foreach (OrtoData ortoData in ortoDatas)
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
            }
            ;

            TextBlock_Progress.Text = "Done Converting!";

            MessageBox.Show("Finished!");
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

        private void MainWindow_Closed(object? sender, EventArgs e)
        {

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

            TextBlock_Progress.Text = "Reading...";

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

            TextBlock_Progress.Text = "Done Reading!";

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

            TextBlock_Progress.Text = "Reducing...";

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
                            if (!Directory.Exists(directory_Temp_Before))
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
            }
            ;

            TextBlock_Progress.Text = "Done reducing...";

            MessageBox.Show("Finished!");
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

        private void ResaveOrtoDatasFiles()
        {
            DateTime dateTime = DateTime.Now;

            TextBlock_Progress.Text = "Resaving...";

            Modify.ResaveOrtoDatasFiles(this, true);

            TimeSpan timeSpan = new TimeSpan((DateTime.Now - dateTime).Ticks);

            TextBlock_Progress.Text = string.Format("Done Resaving! [{0}]", string.Format("{0}d:{1}h:{2}m:{3}s", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds));

            MessageBox.Show("Finished!");
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

                if (gISModel == null)
                {
                    continue;
                }

                List<Building2D> buidling2Ds = gISModel.GetObjects<Building2D>();
                if (buidling2Ds == null || buidling2Ds.Count == 0)
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


            }
            ;

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

        private void ToDiGiGISModelFiles()
        {
            DateTime dateTime = DateTime.Now;

            TextBlock_Progress.Text = "Converting...";

            Convert.ToDiGi_GISModelFiles(this);

            TimeSpan timeSpan = new TimeSpan((DateTime.Now - dateTime).Ticks);

            TextBlock_Progress.Text = string.Format("Done Converting! [{0}]", string.Format("{0}d:{1}h:{2}m:{3}s", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds));
        }

        private void WriteImages()
        {
            DateTime dateTime = DateTime.Now;

            TextBlock_Progress.Text = "Writing...";

            Modify.WriteImages(this, false, new Range<int>(0, 10));

            TimeSpan timeSpan = new TimeSpan((DateTime.Now - dateTime).Ticks);

            TextBlock_Progress.Text = string.Format("Done Writing! [{0}]", string.Format("{0}d:{1}h:{2}m:{3}s", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds));
        }
        
        private async void WriteStatisticalDataCollections()
        {
            DateTime dateTime = DateTime.Now;

            TextBlock_Progress.Text = "Writing...";

            //Enum.GetValues<Variable>()

            await Modify.WriteStatisticalDataCollections(Enum.GetValues<Variable>(), new Range<int>(2008, DateTime.Now.Year));

            TimeSpan timeSpan = new TimeSpan((DateTime.Now - dateTime).Ticks);

            TextBlock_Progress.Text = string.Format("Done Writing! [{0}]", string.Format("{0}d:{1}h:{2}m:{3}s", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds));
        }
    }
} 