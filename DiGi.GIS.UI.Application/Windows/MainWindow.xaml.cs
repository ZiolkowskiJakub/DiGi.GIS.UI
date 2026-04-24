using DiGi.BDL.Classes;
using DiGi.BDL.Enums;
using DiGi.Core.Classes;
using DiGi.Core.Interfaces;
using DiGi.EPW.Classes;
using DiGi.Geometry.Planar;
using DiGi.Geometry.Planar.Classes;
using DiGi.GIS.Classes;
using DiGi.GIS.Constants;
using DiGi.GIS.Emgu.CV.Classes;
using DiGi.GIS.PostgreSQL;
using DiGi.GIS.UI.Classes;
using Microsoft.Win32;
using System.Collections.Concurrent;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Media.Imaging;

namespace DiGi.GIS.UI.Application.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DiGi.UI.WPF.Classes.DeterminateWindowWorker determinateWindowWorker;

        private readonly PostgreSQL.Classes.GISPostgreSQLConverterManager? gISPostgreSQLConverterManager = PostgreSQL.Create.GISPostgreSQLConverterManager();

        public MainWindow()
        {
            InitializeComponent();

            determinateWindowWorker = new DiGi.UI.WPF.Classes.DeterminateWindowWorker();

            this.Closed += MainWindow_Closed;
        }

        private static void Analyse_OrtoDatasComparisons_Table()
        {
            MessageBox.Show("Finished!");
        }

        private static void BDLMatchTest()
        {
            OpenFileDialog openFileDialog_Json = new()
            {
                Filter = "json files (*.json)|*.json|All files (*.*)|*.*"
            };
            bool? openFileDialog_Json_Result = openFileDialog_Json.ShowDialog();
            if (openFileDialog_Json_Result == null || !openFileDialog_Json_Result.HasValue || !openFileDialog_Json_Result.Value)
            {
                return;
            }

            string json = File.ReadAllText(openFileDialog_Json.FileName);
            List<Unit>? units = JsonSerializer.Deserialize<List<Unit>>(json);

            StatisticalUnit? statisticalUnit = GIS.Create.StatisticalUnit(units);

            OpenFileDialog openFileDialog_GISModelFile = new()
            {
                Filter = "GISModelFile files (*.gmf)|*.gmf|All files (*.*)|*.*"
            };
            bool? openFileDialog_GISModelFile_Result = openFileDialog_GISModelFile.ShowDialog();
            if (openFileDialog_GISModelFile_Result == null || !openFileDialog_GISModelFile_Result.HasValue || !openFileDialog_GISModelFile_Result.Value)
            {
                return;
            }

            GISModel? gISModel = null;
            using (GISModelFile gISModelFile = new(openFileDialog_GISModelFile.FileName))
            {
                gISModelFile.Open();
                gISModel = gISModelFile.Value;
            }

            List<AdministrativeAreal2D>? administrativeAreal2Ds = gISModel?.GetObjects<AdministrativeAreal2D>();
            if (administrativeAreal2Ds == null)
            {
                return;
            }

            List<string?> names = [];
            foreach (AdministrativeAreal2D administrativeAreal2D in administrativeAreal2Ds)
            {
                names.Add(administrativeAreal2D?.Name);
            }
        }

        private static void BDLTest()
        {
            OpenFileDialog openFileDialog = new()
            {
                Filter = "json files (*.json)|*.json|All files (*.*)|*.*"
            };
            bool? openFileDialog_Result = openFileDialog.ShowDialog();
            if (openFileDialog_Result == null || !openFileDialog_Result.HasValue || !openFileDialog_Result.Value)
            {
                return;
            }

            string json = File.ReadAllText(openFileDialog.FileName);
            List<Unit>? units = JsonSerializer.Deserialize<List<Unit>>(json);

            StatisticalUnit? statisticalUnit = GIS.Create.StatisticalUnit(units);

            SaveFileDialog saveFileDialog = new()
            {
                Filter = string.Format("{0} (*.{1})|*.{1}|All files (*.*)|*.*", FileTypeName.StatisticalUnitFile, FileExtension.StatisticalUnitFile)
            };
            bool? saveFileDialog_Result = saveFileDialog.ShowDialog();
            if (saveFileDialog_Result == null || !saveFileDialog_Result.HasValue || !saveFileDialog_Result.Value)
            {
                return;
            }

            using StatisticalUnitFile statisticalUnitFile = new(saveFileDialog.FileName);
            statisticalUnitFile.Value = statisticalUnit;
            statisticalUnitFile.Save();

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

            OpenFolderDialog openFolderDialog = new();
            result = openFolderDialog.ShowDialog();
            if (result == null || !result.HasValue || !result.Value)
            {
                return;
            }

            string directory = openFolderDialog.FolderName;

            List<string> paths_GISModelFile = [.. Directory.GetFiles(directory, "*." + FileExtension.GISModelFile, SearchOption.AllDirectories)];
            if (paths_GISModelFile == null || paths_GISModelFile.Count == 0)
            {
                return;
            }

            Core.Query.Filter(paths_GISModelFile, x => System.IO.Path.GetFileNameWithoutExtension(x)!.EndsWith(fileNameSufix), out List<string>? paths_GISModelFile_New, out List<string>? paths_GISModelFile_Old);

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
                string path = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(path_GISModelFile_New)!, System.IO.Path.GetFileNameWithoutExtension(path_GISModelFile_New).Replace(fileNameSufix, string.Empty) + System.IO.Path.GetExtension(path_GISModelFile_New));

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

            List<string> paths_GISModelFile_Source = [.. Directory.GetFiles(directory_Source, "*." + FileExtension.GISModelFile, SearchOption.AllDirectories)];
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

            List<string> paths_GISModelFile_Destination = [.. Directory.GetFiles(directory_Destination, "*." + FileExtension.GISModelFile, SearchOption.AllDirectories)];
            if (paths_GISModelFile_Destination == null || paths_GISModelFile_Destination.Count == 0)
            {
                return;
            }

            foreach (string path_GISModelFile_Source in paths_GISModelFile_Source)
            {
                string fileName = System.IO.Path.GetFileNameWithoutExtension(path_GISModelFile_Source);

                string? path_GISModelFile_Destination = paths_GISModelFile_Destination.Find(x => System.IO.Path.GetFileNameWithoutExtension(x) == fileName);

                if (File.Exists(path_GISModelFile_Source) && File.Exists(path_GISModelFile_Destination))
                {
                    File.Copy(path_GISModelFile_Source, path_GISModelFile_Destination, true);
                }
            }
        }

        private static void CreateAdministrativeAreal2DModel()
        {
            OpenFolderDialog openFolderDialog = new();
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

            SaveFileDialog saveFileDialog = new()
            {
                Filter = string.Format("{0} (*.{1})|*.{1}|All files (*.*)|*.*", FileTypeName.GISModelFile, FileExtension.GISModelFile)
            };
            result = saveFileDialog.ShowDialog();
            if (result == null || !result.HasValue || !result.Value)
            {
                return;
            }

            Dictionary<string, AdministrativeAreal2D> dictionary = [];

            for (int i = 0; i < paths_Input.Length; i++)
            {
                string path_Input = paths_Input[i];

                using GISModelFile gISModelFile = new(path_Input);
                gISModelFile.Open();

                List<AdministrativeAreal2D>? administrativeAreal2Ds = gISModelFile?.Value?.GetObjects<AdministrativeAreal2D>();
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

            GISModel gISModel = new();
            foreach (AdministrativeAreal2D administrativeAreal2D in dictionary.Values)
            {
                gISModel.Update(administrativeAreal2D);
            }

            gISModel.CalculateAdministrativeAreal2DGeometries();
            gISModel.CalculateAdministrativeAreal2DAdministrativeAreal2Ds();

            using (GISModelFile gISModelFile = new(saveFileDialog.FileName))
            {
                gISModelFile.Value = gISModel;
                gISModelFile.Save();
            }

            MessageBox.Show("Finished!");
        }

        private static void Delete()
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

        private static void GISFileModelTest()
        {
            OpenFileDialog openFileDialog = new()
            {
                Filter = "GIS Model files (*.gmf)|*.gmf|All files (*.*)|*.*"
            };
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

            using GISModelFile gISModelFile = new(path);
            gISModelFile.Open();

            GISModel? gISModel = gISModelFile.Value;

            List<Building2D>? building2Ds = gISModel?.GetObjects<Building2D>();
            if (building2Ds != null && building2Ds.Count > 0)
            {
                Building2D building2D = building2Ds[0];

                GIS.Query.AdministrativeAreal2Ds<AdministrativeAreal2D>(gISModel, building2D);
            }
        }

        private static void OrtoDatasTest()
        {
            OpenFileDialog openFileDialog = new()
            {
                Filter = "GIS Model files (*.gmf)|*.gmf|All files (*.*)|*.*"
            };
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

            string? directory_GISModel = System.IO.Path.GetDirectoryName(path);
            if (!Directory.Exists(directory_GISModel))
            {
                return;
            }

            string? directory_OrtoDatas = GIS.Query.OrtoDatasDirectory_Building2D(directory_GISModel);
            if (!Directory.Exists(directory_OrtoDatas))
            {
                return;
            }

            using GISModelFile gISModelFile = new(path);
            gISModelFile.Open();

            GISModel? gISModel = gISModelFile.Value;

            List<Building2D>? building2Ds = gISModel?.GetObjects<Building2D>();
            if (building2Ds != null && building2Ds.Count > 0)
            {
                Building2D building2D = building2Ds[0];

                Core.Convert.ToSystem_FileInfo(building2D, @"C:\Users\jakub\GitHub\DigiProject\DiGi.Test\files\OrtoDatas_BoundingBox2D_Building2D.json");

                OrtoDatas? ortoDatas = GIS.Query.OrtoDatas(building2D, directory_OrtoDatas) ?? throw new Exception("No OrtoDatas");

                Core.Convert.ToSystem_FileInfo((ISerializableObject)ortoDatas, @"C:\Users\jakub\GitHub\DigiProject\DiGi.Test\files\OrtoDatas_BoundingBox2D_OrtoDatas.json");

                BoundingBox2D? boundingBox2D = (building2D?.PolygonalFace2D?.GetBoundingBox()) ?? throw new Exception("Invalid reometry of Building2D");

                foreach (OrtoData ortoData in ortoDatas)
                {
                    BitmapImage? bitmapImage = ortoData.BitmapImage();

                    //Core.Classes.Size? size_1 = GIS.Query.Size(ortoData.Bytes);

                    //Core.Classes.Size? size_2 = ortoData.GetSize(Enums.GeometryContext.Global);

                    //Core.Classes.Size? size_3 = ortoData.GetSize(Enums.GeometryContext.Local);

                    BoundingBox2D? boundingBox2D_1 = ortoData.GetBoundingBox(Enums.GeometryContext.Global);
                    if (boundingBox2D_1 is null)
                    {
                        continue;
                    }

                    //BoundingBox2D? boundingBox2D_2 = ortoData.GetBoundingBox(Enums.GeometryContext.Local);

                    bool inside = boundingBox2D_1.Inside(boundingBox2D);
                    if (!inside)
                    {
                        throw new Exception("Invalid calculations for OrtoDatas");
                    }
                }
            }
        }

        private static void SaveGISModelToJsonFile()
        {
            bool? dialogResult;

            OpenFileDialog openFileDialog = new()
            {
                Title = $"Select {FileTypeName.GISModelFile}",
                Filter = string.Format("{0} (*.{1})|*.{1}|All files (*.*)|*.*", FileTypeName.GISModelFile, FileExtension.GISModelFile)
            };
            dialogResult = openFileDialog.ShowDialog();
            if (dialogResult == null || !dialogResult.HasValue || !dialogResult.Value)
            {
                return;
            }

            string path_GISModelFile = openFileDialog.FileName;

            SaveFileDialog saveFileDialog = new()
            {
                Title = "Save json text file",
                Filter = string.Format("{0} (*.{1})|*.{1}|All files (*.*)|*.*", "Text file", "txt")
            };

            dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult == null || !dialogResult.HasValue || !dialogResult.Value)
            {
                return;
            }

            string path_GISModelJson = saveFileDialog.FileName;

            GISModel? gISModel = null;
            using (GISModelFile gISModelFile = new(path_GISModelFile))
            {
                gISModelFile.Open();
                gISModel = gISModelFile.Value;
            }

            if (gISModel is null)
            {
                return;
            }

            Core.Convert.ToSystem_FileInfo(gISModel, path_GISModelJson);
        }

        private static void SaveOrtoDatasToJsonFile()
        {
            bool? dialogResult;

            OpenFileDialog openFileDialog = new()
            {
                Title = $"Select {FileTypeName.OrtoDatasFile}",
                Filter = string.Format("{0} (*.{1})|*.{1}|All files (*.*)|*.*", FileTypeName.OrtoDatasFile, FileExtension.OrtoDatasFile)
            };
            dialogResult = openFileDialog.ShowDialog();
            if (dialogResult == null || !dialogResult.HasValue || !dialogResult.Value)
            {
                return;
            }

            string path_OrtoDatasFile = openFileDialog.FileName;

            SaveFileDialog saveFileDialog = new()
            {
                Title = "Save json text file",
                Filter = string.Format("{0} (*.{1})|*.{1}|All files (*.*)|*.*", "Text file", "txt")
            };

            dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult == null || !dialogResult.HasValue || !dialogResult.Value)
            {
                return;
            }

            string path_OrtoDataJson = saveFileDialog.FileName;

            OrtoDatas? ortoDatas = null;

            using (OrtoDatasFile ortoDataFiles = new(path_OrtoDatasFile))
            {
                ortoDataFiles.Open();

                HashSet<UniqueReference>? uniqueReferences = ortoDataFiles.GetUniqueReferences();
                if (uniqueReferences is null || uniqueReferences.Count == 0)
                {
                    return;
                }

                ortoDatas = ortoDataFiles.GetValue(uniqueReferences.ElementAt(0));
            }

            if (ortoDatas is null)
            {
                return;
            }

            Core.Convert.ToSystem_FileInfo((ISerializableObject)ortoDatas, path_OrtoDataJson);
        }

        private void AppendBuildingModels()
        {
            DateTime dateTime = DateTime.Now;

            TextBlock_Progress.Text = "Append Building Models...";

            Modify.AppendBuildingModels(this);

            TimeSpan timeSpan = new((DateTime.Now - dateTime).Ticks);

            TextBlock_Progress.Text = string.Format("Done Appending Building Models! [{0}]", string.Format("{0}d:{1}h:{2}m:{3}s", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds));
        }

        private void AppendPredictionYearBuilts()
        {
            DateTime dateTime = DateTime.Now;

            TextBlock_Progress.Text = "Appending Prediction Year Builts...";

            Modify.AppendPredictionYearBuilts(this);

            TimeSpan timeSpan = new((DateTime.Now - dateTime).Ticks);

            TextBlock_Progress.Text = string.Format("Done Appending Prediction Year Builts! [{0}]", string.Format("{0}d:{1}h:{2}m:{3}s", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds));
        }

        private void AppendTable()
        {
            DateTime dateTime = DateTime.Now;

            TextBlock_Progress.Text = "Appending...";

            Modify.AppendPredictionTable(this);

            TimeSpan timeSpan = new((DateTime.Now - dateTime).Ticks);

            TextBlock_Progress.Text = string.Format("Done Appending Table! [{0}]", string.Format("{0}d:{1}h:{2}m:{3}s", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds));
        }

        private void AppendVoTTModel_OrtoRange()
        {
            DateTime dateTime = DateTime.Now;

            TextBlock_Progress.Text = "Appending VoTT Model...";

            Modify.AppendVoTTModel_OrtoRange(this);

            TimeSpan timeSpan = new((DateTime.Now - dateTime).Ticks);

            TextBlock_Progress.Text = string.Format("Done Appending VoTT Model! [{0}]", string.Format("{0}d:{1}h:{2}m:{3}s", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds));
        }

        private void AppendYOLOModel()
        {
            bool includeOrtoRange = false;

            DateTime dateTime = DateTime.Now;

            TextBlock_Progress.Text = "Appending YOLO Model...";

            YOLOConversionOptions yOLOConversionOptions = new()
            {
                ClearData = true
            };

            yOLOConversionOptions[YOLO.Enums.Category.Train] = 0.9;
            yOLOConversionOptions[YOLO.Enums.Category.Validate] = 0.1;
            yOLOConversionOptions[YOLO.Enums.Category.Test] = 0;

            Modify.AppendYOLOModel_Building2D(this, yOLOConversionOptions);

            if (includeOrtoRange)
            {
                yOLOConversionOptions.ClearData = false;
                Modify.AppendYOLOModel_OrtoRange(this, yOLOConversionOptions);
            }

            TimeSpan timeSpan = new((DateTime.Now - dateTime).Ticks);

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

            TimeSpan timeSpan = new((DateTime.Now - dateTime).Ticks);

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

        private void Button_CalculateTypologies_Click(object sender, RoutedEventArgs e)
        {
            DateTime dateTime = DateTime.Now;

            TextBlock_Progress.Text = "Calculating Typologies...";

            Modify.CalculateTypologies(this);

            TimeSpan timeSpan = new((DateTime.Now - dateTime).Ticks);

            TextBlock_Progress.Text = string.Format("Done Calculating! [{0}]", string.Format("{0}d:{1}h:{2}m:{3}s", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds));
        }

        private void Button_CompareJsons_Click(object sender, RoutedEventArgs e)
        {
            bool? dialogResult;

            OpenFileDialog openFileDialog;

            openFileDialog = new()
            {
                Title = "Select json text file",
                Filter = string.Format("{0} (*.{1})|*.{1}|{2} (*.{3})|*.{3}|All files (*.*)|*.*", "Text file", "txt", "JSON file", "json")
            };

            dialogResult = openFileDialog.ShowDialog();
            if (dialogResult == null || !dialogResult.HasValue || !dialogResult.Value)
            {
                return;
            }

            string path_1 = openFileDialog.FileName;

            openFileDialog = new()
            {
                Title = "Select json text file",
                Filter = string.Format("{0} (*.{1})|*.{1}|{2} (*.{3})|*.{3}|All files (*.*)|*.*", "Text file", "txt", "JSON file", "json")
            };

            dialogResult = openFileDialog.ShowDialog();
            if (dialogResult == null || !dialogResult.HasValue || !dialogResult.Value)
            {
                return;
            }

            string path_2 = openFileDialog.FileName;

            ISerializableObject? serializableObject_1 = Core.Convert.ToDiGi<ISerializableObject>(new Core.Classes.Path(path_1))?.FirstOrDefault();
            ISerializableObject? serializableObject_2 = Core.Convert.ToDiGi<ISerializableObject>(new Core.Classes.Path(path_2))?.FirstOrDefault();

            bool result = serializableObject_1?.ToString() == serializableObject_2?.ToString();

            MessageBox.Show(result ? "The same objects" : "Objects are different");
        }

        private void Button_ConvertOrtoDatasToFiles_Click(object sender, RoutedEventArgs e)
        {
            Convert_ToFiles();
        }

        private void Button_OrtoDatas_Click(object sender, RoutedEventArgs e)
        {
            Hide();

            UI.Windows.OrtoDatasWindow yearBuiltsWindow = new()
            {
                WindowState = WindowState.Maximized
            };

            yearBuiltsWindow.ShowDialog();

            Close();
        }

        private void Button_RecalculateOrtoDatas_Building2D_Click(object sender, RoutedEventArgs e)
        {
            RecalculateOrtoDatas_Building2D(100);
        }

        private void Button_Reduce_Click(object sender, RoutedEventArgs e)
        {
            Reduce();
        }

        private async void Button_RefreshAdministrativeAreal2Ds_Click(object sender, RoutedEventArgs e)
        {
            DateTime dateTime = DateTime.Now;

            TextBlock_Progress.Text = "Refreshing...";

            PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter? administrativeAreal2DPostgreSQLConverter = gISPostgreSQLConverterManager?.GetPostgreSQLConverter<PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter>();
            if (administrativeAreal2DPostgreSQLConverter is null)
            {
                return;
            }

            await administrativeAreal2DPostgreSQLConverter.RefreshAsync();

            TimeSpan timeSpan = new((DateTime.Now - dateTime).Ticks);

            TextBlock_Progress.Text = string.Format("Done Refreshing! [{0}]", string.Format("{0}d:{1}h:{2}m:{3}s", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds));
        }

        private async void Button_RefreshBuiliding2Ds_Click(object sender, RoutedEventArgs e)
        {
            DateTime dateTime = DateTime.Now;

            TextBlock_Progress.Text = "Refreshing...";

            PostgreSQL.Classes.Building2DPostgreSQLConverter? building2DPostgreSQLConverter = gISPostgreSQLConverterManager?.GetPostgreSQLConverter<PostgreSQL.Classes.Building2DPostgreSQLConverter>();
            if (building2DPostgreSQLConverter is null)
            {
                return;
            }

            await building2DPostgreSQLConverter.RefreshAsync();

            TimeSpan timeSpan = new((DateTime.Now - dateTime).Ticks);

            TextBlock_Progress.Text = string.Format("Done Refreshing! [{0}]", string.Format("{0}d:{1}h:{2}m:{3}s", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds));
        }

        private void Button_ResaveGISModels_Click(object sender, RoutedEventArgs e)
        {
            DateTime dateTime = DateTime.Now;

            TextBlock_Progress.Text = "Resaving...";

            //Enum.GetValues<Variable>()
            OpenFolderDialog openFolderDialog = new();
            bool? result = openFolderDialog.ShowDialog(this);
            if (result == null || !result.HasValue || !result.Value)
            {
                return;
            }

            string directory = openFolderDialog.FolderName;
            string[] paths = Directory.GetFiles(directory, "*." + FileExtension.GISModelFile, SearchOption.AllDirectories);
            if (paths is null || paths.Length == 0)
            {
                return;
            }

            foreach (string path in paths)
            {
                string reference = System.IO.Path.GetFileNameWithoutExtension(path);

                using GISModelFile gISModelFile = new(path);
                gISModelFile.Open();

                GISModel? gISModel = gISModelFile.Value;
                if (gISModel is not null)
                {
                    gISModelFile.Value = new GISModel(reference, gISModel);
                    gISModelFile.Save();
                }
            }

            TimeSpan timeSpan = new((DateTime.Now - dateTime).Ticks);

            TextBlock_Progress.Text = string.Format("Done Resaving! [{0}]", string.Format("{0}d:{1}h:{2}m:{3}s", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds));
        }

        private void Button_ResaveOrtoDatasFiles_Click(object sender, RoutedEventArgs e)
        {
            ResaveOrtoDatasFiles();
        }

        private async void Button_Test_Click(object sender, RoutedEventArgs e)
        {
            //SaveOrtoDatasToJsonFile();
            //SaveGISModelToJsonFile();
            //CheckAdministrativeAreal2D();
            //UpdateUpdateAdministrativeAreal2DsCodes();
            //CheckPoint();
            //OrtoDatasTest();

            PostgreSQLTest();
        }

        private async void PostgreSQLTest()
        {
            if (gISPostgreSQLConverterManager is null || !(await gISPostgreSQLConverterManager.TryCreateDatabase<PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter>()))
            {
                return;
            }


            PostgreSQL.Classes.OrtoDatasPostgreSQLConverter? ortoDatasPostgreSQLConverter = gISPostgreSQLConverterManager.GetPostgreSQLConverter<PostgreSQL.Classes.OrtoDatasPostgreSQLConverter>();
            if (ortoDatasPostgreSQLConverter is null)
            {
                return;
            }

            //List<double>? estimatedCoverageFactors = await ortoDatasPostgreSQLConverter.GetEstimatedCoverageFactorsAsync([6, 3854, 19719, 10363, 17509, 28262, 42707, 55639, 57694, 65613, 70825, 75132, 80410, 87574, 92383, 100521]);
            //if(estimatedCoverageFactors is null)
            //{
            //    return;
            //}

            PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter? administrativeAreal2DPostgreSQLConverter = gISPostgreSQLConverterManager.GetPostgreSQLConverter<PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter>();
            if (administrativeAreal2DPostgreSQLConverter is null)
            {
                return;
            }

            List<PostgreSQL.Classes.AdministrativeAreal2DReference>? administrativeAreal2DReferences_All_1 = [];

            List<PostgreSQL.Classes.AdministrativeAreal2DReference>? administrativeAreal2DReferences_Code = await administrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByCodeAsync("02");
            if (administrativeAreal2DReferences_Code is null)
            {
                return;
            }

            foreach (PostgreSQL.Classes.AdministrativeAreal2DReference administrativeAreal2DReference_Code in administrativeAreal2DReferences_Code)
            {
                List<PostgreSQL.Classes.AdministrativeAreal2DReference>? administrativeAreal2DReferences_Temp = await administrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync(PostgreSQL.Enums.AdministrativeArealType.Municipality, administrativeAreal2DReference_Code.Id);
                if (administrativeAreal2DReferences_Temp is null)
                {
                    continue;
                }

                administrativeAreal2DReferences_All_1.AddRange(administrativeAreal2DReferences_Temp);
            }

            List<PostgreSQL.Classes.AdministrativeAreal2DReference>? administrativeAreal2DReferences_All_2 = await administrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByCodeAsync("02", PostgreSQL.Enums.AdministrativeArealType.Municipality);
            if (administrativeAreal2DReferences_All_2 is null)
            {
                return;
            }

            if (administrativeAreal2DReferences_All_1.Count != administrativeAreal2DReferences_All_2.Count)
            {
                throw new Exception();
            }

            PostgreSQL.Classes.AdministrativeAreal2DReference? administrativeAreal2DReference = await administrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferenceByCodeAsync("02");
            if (administrativeAreal2DReference is null)
            {
                return;
            }

            PostgreSQL.Classes.Building2DPostgreSQLConverter? building2DPostgreSQLConverter = gISPostgreSQLConverterManager.GetPostgreSQLConverter<PostgreSQL.Classes.Building2DPostgreSQLConverter>();
            if (building2DPostgreSQLConverter is null)
            {
                return;
            }

#pragma warning disable IDE0059 // Unnecessary assignment of a value
            List<PostgreSQL.Classes.Building2DReference>? building2DReferences = await building2DPostgreSQLConverter.GetBuilding2DReferencesByAdministrativeAreal2DIdsAsync([2]);
#pragma warning restore IDE0059 // Unnecessary assignment of a value

            List<PostgreSQL.Classes.AdministrativeAreal2DReference>? administrativeAreal2DReferences = await administrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync(PostgreSQL.Enums.AdministrativeArealType.Voivodeship, null, true);
            if (administrativeAreal2DReferences is null)
            {
                return;
            }

            PostgreSQL.Classes.GISPostgreSQLConverterManager? gISPostgreSQLConverterManager_Temp = PostgreSQL.Create.GISPostgreSQLConverterManager();
            if (gISPostgreSQLConverterManager_Temp is not null)
            {
                List<PostgreSQL.Interfaces.IGISPostgreSQLConverter> gISPostgreSQLConverters = gISPostgreSQLConverterManager_Temp.GetPostgreSQLConverters<PostgreSQL.Interfaces.IGISPostgreSQLConverter>();
                if (gISPostgreSQLConverters is not null)
                {
#pragma warning disable IDE0059 // Unnecessary assignment of a value
                    foreach (PostgreSQL.Interfaces.IGISPostgreSQLConverter gISPostgreSQLConverter in gISPostgreSQLConverters)
                    {
                        //serviceCollection.AddSingleton(gISPostgreSQLConverter);
                    }
#pragma warning restore IDE0059 // Unnecessary assignment of a value
                }
            }
        }

        private void Button_ToDiGiGISModelFiles_Click(object sender, RoutedEventArgs e)
        {
            ToDiGiGISModelFiles();
        }

        private void Button_Typology_Click(object sender, RoutedEventArgs e)
        {
            Hide();

            UI.Windows.TypologyWindow typologyWindow = new()
            {
                WindowState = WindowState.Maximized
            };

            typologyWindow.ShowDialog();

            Close();
        }

        private async void Button_UpdateAdministrativeAreal2Ds_Click(object sender, RoutedEventArgs e)
        {
            bool clear = false;

            DateTime dateTime = DateTime.Now;

            TextBlock_Progress.Text = "Updating...";

            if (gISPostgreSQLConverterManager is null || !(await gISPostgreSQLConverterManager.TryCreateDatabase<PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter>()))
            {
                return;
            }

            bool? result;

            OpenFolderDialog openFolderDialog = new();
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
            if (paths_Input == null || paths_Input.Length == 0)
            {
                return;
            }

            PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter? administrativeAreal2DPostgreSQLConverter = gISPostgreSQLConverterManager.GetPostgreSQLConverter<PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter>();
            if (administrativeAreal2DPostgreSQLConverter is null)
            {
                return;
            }

            if (clear)
            {
                await administrativeAreal2DPostgreSQLConverter.ClearAsync();
            }

            foreach (string path_Input in paths_Input)
            {
                GISModel? gISModel_Input = null;

                using (GISModelFile gISModelFile = new(path_Input))
                {
                    gISModelFile.Open();
                    gISModel_Input = gISModelFile.Value;
                }

                if (gISModel_Input == null)
                {
                    continue;
                }

                List<AdministrativeAreal2D>? administrativeAreal2Ds_GIS = gISModel_Input.GetObjects<AdministrativeAreal2D>();
                if (administrativeAreal2Ds_GIS is null || administrativeAreal2Ds_GIS.Count == 0)
                {
                    continue;
                }

                //File.WriteAllText(@"C:\Users\jakub\Downloads\GIS\administrativeAreal2D.json", Core.Convert.ToSystem_String(administrativeAreal2Ds_GIS[0]));

                List<PostgreSQL.Classes.AdministrativeAreal2D>? administrativeAreal2Ds_PostgreSQL = [];
                foreach (AdministrativeAreal2D administrativeAreal2D_GIS in administrativeAreal2Ds_GIS)
                {
                    if (PostgreSQL.Convert.ToPostgreSQL(administrativeAreal2D_GIS) is not PostgreSQL.Classes.AdministrativeAreal2D administrativeAreal2D_PostgreSQL)
                    {
                        continue;
                    }

                    administrativeAreal2Ds_PostgreSQL.Add(administrativeAreal2D_PostgreSQL);
                }

                await administrativeAreal2DPostgreSQLConverter.UpdateAsync(administrativeAreal2Ds_PostgreSQL);
            }

            TimeSpan timeSpan = new((DateTime.Now - dateTime).Ticks);

            TextBlock_Progress.Text = string.Format("Done Updating! [{0}]", string.Format("{0}d:{1}h:{2}m:{3}s", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds));
        }

        private async void Button_UpdateBuiliding2Ds_Click(object sender, RoutedEventArgs e)
        {
            bool clear = true;

            DateTime dateTime = DateTime.Now;

            TextBlock_Progress.Text = "Updating...";

            if (gISPostgreSQLConverterManager is null || !(await gISPostgreSQLConverterManager.TryCreateDatabase<PostgreSQL.Classes.Building2DPostgreSQLConverter>()))
            {
                return;
            }

            bool? result;

            OpenFolderDialog openFolderDialog = new();
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
            if (paths_Input == null || paths_Input.Length == 0)
            {
                return;
            }

            PostgreSQL.Classes.Building2DPostgreSQLConverter? building2DPostgreSQLConverter = gISPostgreSQLConverterManager.GetPostgreSQLConverter<PostgreSQL.Classes.Building2DPostgreSQLConverter>();
            if (building2DPostgreSQLConverter is null)
            {
                return;
            }

            if (clear)
            {
                await building2DPostgreSQLConverter.ClearAsync();
            }

            foreach (string path_Input in paths_Input)
            {
                GISModel? gISModel_Input = null;

                using (GISModelFile gISModelFile = new(path_Input))
                {
                    gISModelFile.Open();
                    gISModel_Input = gISModelFile.Value;
                }

                if (gISModel_Input == null)
                {
                    continue;
                }

                List<Building2D>? building2Ds_GIS = gISModel_Input.GetObjects<Building2D>();
                if (building2Ds_GIS is null || building2Ds_GIS.Count == 0)
                {
                    continue;
                }

                //string? code = null;
                string? code = gISModel_Input.Reference;
                if (!string.IsNullOrWhiteSpace(code))
                {
                    code = code.ToUpper();
                    int index = code.IndexOf('_');
                    if (index != -1)
                    {
                        code = code[..index];
                    }
                }

                List<PostgreSQL.Classes.Building2D>? building2Ds_PostgreSQL = [];
                foreach (Building2D building2D_GIS in building2Ds_GIS)
                {
                    if (PostgreSQL.Convert.ToPostgreSQL(building2D_GIS, code) is not PostgreSQL.Classes.Building2D building2D_PostgreSQL)
                    {
                        continue;
                    }

                    building2Ds_PostgreSQL.Add(building2D_PostgreSQL);
                }

                await building2DPostgreSQLConverter.UpdateAsync(building2Ds_PostgreSQL);
            }

            TimeSpan timeSpan = new((DateTime.Now - dateTime).Ticks);

            TextBlock_Progress.Text = string.Format("Done Updating! [{0}]", string.Format("{0}d:{1}h:{2}m:{3}s", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds));
        }

        private async void Button_UpdateOrtoDatas_FromFile_Click(object sender, RoutedEventArgs e)
        {
            if (gISPostgreSQLConverterManager is null || !(await gISPostgreSQLConverterManager.TryCreateDatabase<PostgreSQL.Classes.OrtoDatasPostgreSQLConverter>()))
            {
                return;
            }

            PostgreSQL.Classes.OrtoDatasPostgreSQLConverter? ortoDatasPostgreSQLConverter = gISPostgreSQLConverterManager.GetPostgreSQLConverter<PostgreSQL.Classes.OrtoDatasPostgreSQLConverter>();
            if (ortoDatasPostgreSQLConverter is null)
            {
                return;
            }

            PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter? administrativeAreal2DPostgreSQLConverter = gISPostgreSQLConverterManager.GetPostgreSQLConverter<PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter>();
            if (administrativeAreal2DPostgreSQLConverter is null)
            {
                return;
            }

            bool clear = true;

            DateTime dateTime = DateTime.Now;

            TextBlock_Progress.Text = "Updating...";

            bool? result;

            OpenFolderDialog openFolderDialog = new();
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
            if (paths_Input == null || paths_Input.Length == 0)
            {
                return;
            }

            if (clear)
            {
                await ortoDatasPostgreSQLConverter.ClearAsync();
            }

            foreach (string path_Input in paths_Input)
            {
                string? directory_GISModel = System.IO.Path.GetDirectoryName(path_Input);
                if (!Directory.Exists(directory_GISModel))
                {
                    continue;
                }

                string? directory_OrtoDatas = GIS.Query.OrtoDatasDirectory_Building2D(directory_GISModel);
                if (!Directory.Exists(directory_OrtoDatas))
                {
                    continue;
                }

                GISModel? gISModel_Input = null;

                using (GISModelFile gISModelFile = new(path_Input))
                {
                    gISModelFile.Open();
                    gISModel_Input = gISModelFile.Value;
                }

                if (gISModel_Input == null)
                {
                    continue;
                }

                List<Building2D>? building2Ds_GIS = gISModel_Input.GetObjects<Building2D>();
                if (building2Ds_GIS is null || building2Ds_GIS.Count == 0)
                {
                    continue;
                }

                //string? code = null;
                string? code = gISModel_Input.Reference;
                if (!string.IsNullOrWhiteSpace(code))
                {
                    code = code.ToUpper();
                    int index = code.IndexOf('_');
                    if (index != -1)
                    {
                        code = code[..index];
                    }
                }

                if (string.IsNullOrWhiteSpace(code))
                {
                    continue;
                }

                int? countyId = await administrativeAreal2DPostgreSQLConverter.GetIdByCodeAsync(code, PostgreSQL.Enums.AdministrativeArealType.County);
                if (countyId is null || !countyId.HasValue)
                {
                    continue;
                }

                List<Building2D>? building2Ds_Split;

                SizeSplitter<Building2D> memorySizeSplitter = new(building2Ds_GIS);
                while ((building2Ds_Split = memorySizeSplitter.Next(200)) is not null)
                {
                    IEnumerable<OrtoDatas>? ortoDatas_GIS = GIS.Query.OrtoDatasDictionary(directory_OrtoDatas, building2Ds_Split)?.Values;
                    if (ortoDatas_GIS is null)
                    {
                        continue;
                    }

                    List<PostgreSQL.Classes.OrtoDatas>? ortoDatas_PostgreSQL = [];
                    foreach (OrtoDatas ortoDatas_GIS_Temp in ortoDatas_GIS)
                    {
                        PostgreSQL.Classes.OrtoDatas? ortoDatas_PostgreSQL_Temp = PostgreSQL.Convert.ToPostgreSQL(ortoDatas_GIS_Temp, countyId);
                        if (ortoDatas_PostgreSQL_Temp is null)
                        {
                            continue;
                        }

                        ortoDatas_PostgreSQL.Add(ortoDatas_PostgreSQL_Temp);
                    }

                    await ortoDatasPostgreSQLConverter.UpdateAsync(ortoDatas_PostgreSQL);
                }
            }

            TimeSpan timeSpan = new((DateTime.Now - dateTime).Ticks);

            TextBlock_Progress.Text = string.Format("Done Updating! [{0}]", string.Format("{0}d:{1}h:{2}m:{3}s", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds));
        }

        private void Button_WriteStatisticalDataCollections_Click(object sender, RoutedEventArgs e)
        {
            WriteStatisticalDataCollections();
        }

        private void Button_YearBuilts_Click(object sender, RoutedEventArgs e)
        {
            Hide();

            UI.Windows.YearBuiltsWindow yearBuiltsWindow = new()
            {
                WindowState = WindowState.Maximized
            };

            yearBuiltsWindow.ShowDialog();

            Close();
        }

        private void CalculateAdministrativeAreal2DStatisticalUnits()
        {
            DateTime dateTime = DateTime.Now;

            TextBlock_Progress.Text = "Calculating...";

            Modify.CalculateAdministrativeAreal2DStatisticalUnits(this, true, "_StatisticalUnits");

            TimeSpan timeSpan = new((DateTime.Now - dateTime).Ticks);

            TextBlock_Progress.Text = string.Format("Done Calculating! [{0}]", string.Format("{0}d:{1}h:{2}m:{3}s", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds));
        }

        private void CalculateGISModelFiles()
        {
            DateTime dateTime = DateTime.Now;

            TextBlock_Progress.Text = "Calculating...";

            Modify.CalculateGISModelFiles(this, determinateWindowWorker);

            TimeSpan timeSpan = new((DateTime.Now - dateTime).Ticks);

            TextBlock_Progress.Text = string.Format("Done Calculating! [{0}]", string.Format("{0}d:{1}h:{2}m:{3}s", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds));
        }

        private async void CalculateOrtoDatas_Building2D(int count = 100)
        {
            DateTime dateTime = DateTime.Now;

            TextBlock_Progress.Text = "Calculating...";

            await Modify.CalculateOrtoDatas(this, Create.OrtoDatasBuilding2DOptions(), count, true);

            TimeSpan timeSpan = new((DateTime.Now - dateTime).Ticks);

            TextBlock_Progress.Text = string.Format("Done Calculating! [{0}]", string.Format("{0}d:{1}h:{2}m:{3}s", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds));
        }

        private async void CalculateOrtoDatas_OrtoRange(int count = 100)
        {
            DateTime dateTime = DateTime.Now;

            TextBlock_Progress.Text = "Calculating...";

            OrtoDatasOrtoRangeOptions ortoDatasOrtoRangeOptions = new()
            {
                MaxFileSize = (1024UL * 1024UL * 1024UL * 5) / 10
            };

            await Modify.CalculateOrtoDatas(this, ortoDatasOrtoRangeOptions, count);

            TimeSpan timeSpan = new((DateTime.Now - dateTime).Ticks);

            TextBlock_Progress.Text = string.Format("Done Calculating! [{0}]", string.Format("{0}d:{1}h:{2}m:{3}s", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds));
        }

        private void CalculateOrtoDatasComparisons()
        {
            DateTime dateTime = DateTime.Now;

            TextBlock_Progress.Text = "Calculating...";

            OrtoDatasComparisonOptions ortoDatasComparisonOptions = new()
            {
                DirectoryNames = GIS.Query.OrtoDatasDirectoryNames_Building2D()
            };
            //ortoDatasComparisonOptions.OrtoDatasOptions.MaxFileSize = (1024UL * 1024UL * 1024UL * 5) / 10;

            Modify.CalculateOrtoDatasComparisons(this, ortoDatasComparisonOptions);

            TimeSpan timeSpan = new((DateTime.Now - dateTime).Ticks);

            TextBlock_Progress.Text = string.Format("Done Calculating! [{0}]", string.Format("{0}d:{1}h:{2}m:{3}s", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds));
        }

        private void CalculateOrtoRanges()
        {
            DateTime dateTime = DateTime.Now;

            TextBlock_Progress.Text = "Calculating...";

            OrtoRangeOptions ortoRangeOptions = new()
            {
            };

            Modify.CalculateOrtoRanges(this, ortoRangeOptions);

            TimeSpan timeSpan = new((DateTime.Now - dateTime).Ticks);

            TextBlock_Progress.Text = string.Format("Done Calculating! [{0}]", string.Format("{0}d:{1}h:{2}m:{3}s", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds));
        }

        private void CheckAdministrativeAreal2D()
        {
            OpenFolderDialog openFolderDialog = new();
            bool? openFolderDialogResult = openFolderDialog.ShowDialog(this);
            if (openFolderDialogResult == null || !openFolderDialogResult.HasValue || !openFolderDialogResult.Value)
            {
                return;
            }

            SaveFileDialog saveFileDialog = new();
            bool? saveFileDialogResult = saveFileDialog.ShowDialog(this);
            if (saveFileDialogResult == null || !saveFileDialogResult.HasValue || !saveFileDialogResult.Value)
            {
                return;
            }

            string path = saveFileDialog.FileName;

            string directory = openFolderDialog.FolderName;
            if (string.IsNullOrWhiteSpace(directory) || !Directory.Exists(directory))
            {
                return;
            }

            List<string>? paths_Input = Directory.GetFiles(directory, "*." + FileExtension.GISModelFile, SearchOption.AllDirectories)?.ToList();
            if (paths_Input == null || paths_Input.Count == 0)
            {
                return;
            }

            ConcurrentBag<GuidReference> concurrentBag = [];

            ParallelOptions parallelOptions = new()
            {
                MaxDegreeOfParallelism = 20,
            };

            //Parallel.ForEach(paths_Input, parallelOptions, (string path) =>
            foreach (string path_Temp in paths_Input)
            {
                using GISModelFile gISModelFile = new(path_Temp);

                gISModelFile.Open();

                if (gISModelFile.Value is not GISModel gISModel)
                {
                    return;
                }

                List<Building2D>? building2Ds = gISModel.GetObjects<Building2D>();
                if (building2Ds is null)
                {
                    return;
                }

                Dictionary<GuidReference, List<AdministrativeAreal2D>?>? dictionary = gISModel.AdministrativeAreal2DsDictionary<AdministrativeAreal2D>(building2Ds);
                if (dictionary is null)
                {
                    return;
                }

                List<AdministrativeDivision>? administrativeDivisions_All = gISModel.GetObjects<AdministrativeDivision>(x => x?.AdministrativeDivisionType == Enums.AdministrativeDivisionType.municipality);

                foreach (Building2D building2D in building2Ds)
                {
                    if (building2D is null)
                    {
                        continue;
                    }

                    GuidReference guidReference = new(building2D);

                    if (!dictionary.TryGetValue(guidReference, out List<AdministrativeAreal2D>? administrativeDivisions) || administrativeDivisions is null)
                    {
                        continue;
                    }

                    AdministrativeAreal2D? administrativeAreal2D = administrativeDivisions.Find(div => div is AdministrativeDivision administrativeDivision && administrativeDivision.AdministrativeDivisionType == Enums.AdministrativeDivisionType.municipality);
                    if (administrativeAreal2D is not null)
                    {
                        continue;
                    }

                    Point2D? point2D = building2D?.PolygonalFace2D?.GetInternalPoint();
                    if (point2D is not null)
                    {
                        AdministrativeAreal2D? administrativeAreal2D_Inside = administrativeDivisions_All?.Find(x => x.PolygonalFace2D is PolygonalFace2D polygonalFace2D && polygonalFace2D.Inside(point2D));

                        List<Tuple<AdministrativeAreal2D, double>>? tuples = administrativeDivisions_All?.ConvertAll(x => new Tuple<AdministrativeAreal2D, double>(x, x.PolygonalFace2D is not PolygonalFace2D polygonalFace2D ? double.MaxValue : Geometry.Planar.Query.Distance(point2D, polygonalFace2D.ExternalEdge)));
                        tuples?.Sort((x, y) => x.Item2.CompareTo(y.Item2));
                    }

                    concurrentBag.Add(guidReference);
                }
            }//);

            File.WriteAllLines(path, concurrentBag.Cast<GuidReference>().ToList().ConvertAll(x => x.ToString() is string value ? value : string.Empty));
        }

        private async void CheckPoint()
        {
            DateTime dateTime = DateTime.Now;

            TextBlock_Progress.Text = "Checking...";

            PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter? administrativeAreal2DPostgreSQLConverter = gISPostgreSQLConverterManager?.GetPostgreSQLConverter<PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter>();
            if (administrativeAreal2DPostgreSQLConverter is null)
            {
                return;
            }

            PostgreSQL.Enums.AdministrativeArealType administrativeArealType = Core.Query.Enum<PostgreSQL.Enums.AdministrativeArealType>("subdivision");

            _ = await administrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByPoint2DAsync(new Point2D(338301.58, 397037.55), administrativeArealType);

            TimeSpan timeSpan = new((DateTime.Now - dateTime).Ticks);

            TextBlock_Progress.Text = string.Format("Done Checking! [{0}]", string.Format("{0}d:{1}h:{2}m:{3}s:{4}ms", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds));
        }

        private void Convert_ToFiles(int count = 10)
        {
            OpenFolderDialog openFolderDialog;

            bool? result;

            openFolderDialog = new()
            {
                Title = "Select OrtoDatasFiles directory"
            };

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

            openFolderDialog = new()
            {
                Title = "Select destination directory"
            };

            result = openFolderDialog.ShowDialog(this);
            if (result == null || !result.HasValue || !result.Value)
            {
                return;
            }

            string directory_Destination = openFolderDialog.FolderName;
            if (string.IsNullOrWhiteSpace(directory) || !Directory.Exists(directory))
            {
                return;
            }

            TextBlock_Progress.Text = "Converting...";

            string[] paths_Input = Directory.GetFiles(directory, "*." + FileExtension.OrtoDatasFile, SearchOption.AllDirectories);
            for (int i = 0; i < paths_Input.Length; i++)
            {
                string path_Input = paths_Input[i];

                using OrtoDatasFile ortoDatasFile = new(path_Input);
                ortoDatasFile.Open();

                List<UniqueReference>? uniqueReferences = ortoDatasFile.GetUniqueReferences()?.ToList();
                if (uniqueReferences == null)
                {
                    continue;
                }

                //string directory_Temp = System.IO.Path.Combine(directory, "OrtoData");
                //if (!Directory.Exists(directory_Temp))
                //{
                //    Directory.CreateDirectory(directory_Temp);
                //}

                while (uniqueReferences.Count > 0)
                {
                    int count_Temp = Math.Max(count, uniqueReferences.Count);

                    List<UniqueReference> uniqueReferences_Temp = uniqueReferences.GetRange(0, count_Temp);
                    uniqueReferences.RemoveRange(0, count_Temp);

                    IEnumerable<OrtoDatas?>? ortoDatasList = ortoDatasFile.GetValues<OrtoDatas>(uniqueReferences_Temp);
                    if (ortoDatasList != null)
                    {
                        foreach (OrtoDatas? ortoDatas in ortoDatasList)
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

                                using Image image = Image.FromStream(new MemoryStream(ortoData.Bytes));
                                image.Save(System.IO.Path.Combine(directory_Destination, fileName), ImageFormat.Jpeg);
                            }
                        }
                    }
                }
            }
            ;

            TextBlock_Progress.Text = "Done Converting!";

            MessageBox.Show("Finished!");
        }

        private void MainWindow_Closed(object? sender, EventArgs e)
        {
        }

        private void Read_FromZip()
        {
            bool? result;

            OpenFolderDialog openFolderDialog = new();
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
                GISModel? gISModel_Input = null;

                if (System.IO.Path.GetDirectoryName(path_Input) is not string directory_Temp)
                {
                    continue;
                }

                using (GISModelFile gISModelFile = new(path_Input))
                {
                    gISModelFile.Open();
                    gISModel_Input = gISModelFile.Value;
                }

                if (gISModel_Input == null)
                {
                    continue;
                }

                Building2D? building2D = gISModel_Input.GetObject<Building2D>();

                string path_Output = System.IO.Path.Combine(directory_Temp, System.IO.Path.GetFileNameWithoutExtension(path_Input) + "_Out." + FileExtension.GISModelFile);
                using (GISModelFile gISModelFile = new(path_Output))
                {
                    GISModel gISModel_Output = new();
                    gISModel_Output.Update(building2D);
                    gISModel_Output.CalculateBuilding2DGeometries();

                    gISModel_Output.CalculateBuilding2DGeometries();

                    gISModelFile.Value = gISModel_Output;
                    gISModelFile.Save();
                }

                using (GISModelFile gISModelFile = new(path_Output))
                {
                    gISModelFile.Open();
                    GISModel? gISModel_Output = gISModelFile.Value;
                }
            }

            TextBlock_Progress.Text = "Done Reading!";
        }

        private async void RecalculateOrtoDatas_Building2D(int count = 100)
        {
            DateTime dateTime = DateTime.Now;

            TextBlock_Progress.Text = "Recalculating...";

            await Modify.CalculateOrtoDatas(this, Create.OrtoDatasBuilding2DOptions(), count, false);

            TimeSpan timeSpan = new((DateTime.Now - dateTime).Ticks);

            TextBlock_Progress.Text = string.Format("Done Recalculating! [{0}]", string.Format("{0}d:{1}h:{2}m:{3}s", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds));
        }

        private void Reduce()
        {
            OpenFolderDialog openFolderDialog = new();
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

                using OrtoDatasFile ortoDatasFile = new(path_Input);

                ortoDatasFile.Open();

                string directory_Temp = @"C:\Users\jakub\Downloads\GIS Test\OrtoData";

                IEnumerable<OrtoDatas?>? ortoDatasList = ortoDatasFile.Values;
                if (ortoDatasList != null)
                {
                    foreach (OrtoDatas? ortoDatas in ortoDatasList)
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

                            using Image image = Image.FromStream(new MemoryStream(ortoData.Bytes));
                            image.Save(System.IO.Path.Combine(directory_Temp_Before, fileName), ImageFormat.Jpeg);
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

                            using Image image = Image.FromStream(new MemoryStream(ortoData.Bytes));
                            image.Save(System.IO.Path.Combine(directory_Temp_After, fileName), ImageFormat.Jpeg);
                        }

                        ortoDatasFile.AddValue(ortoDatas);
                    }
                }

                ortoDatasFile.Save();
            }
            ;

            TextBlock_Progress.Text = "Done reducing...";

            MessageBox.Show("Finished!");
        }

        private void Reorganize()
        {
            OpenFolderDialog openFolderDialog = new();
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

            TimeSpan timeSpan = new((DateTime.Now - dateTime).Ticks);

            TextBlock_Progress.Text = string.Format("Done Resaving! [{0}]", string.Format("{0}d:{1}h:{2}m:{3}s", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds));

            MessageBox.Show("Finished!");
        }

        private void Test_1()
        {
            OpenFolderDialog openFolderDialog;
            bool? result;

            openFolderDialog = new OpenFolderDialog
            {
                Title = "Select EPW files directory"
            };
            result = openFolderDialog.ShowDialog(this);
            if (result == null || !result.HasValue || !result.Value)
            {
                return;
            }

            Dictionary<string, EPWFile>? dictionary_EPWFile = EPW.Create.EPWFiles(openFolderDialog.FolderName, SearchOption.AllDirectories);

            openFolderDialog = new OpenFolderDialog
            {
                Title = "Select GISModel directory"
            };
            result = openFolderDialog.ShowDialog(this);
            if (result == null || !result.HasValue || !result.Value)
            {
                return;
            }

            string[] paths_GISModel = Directory.GetFiles(openFolderDialog.FolderName, "*." + FileExtension.GISModelFile, SearchOption.AllDirectories);
            for (int i = 0; i < paths_GISModel.Length; i++)
            {
                string path_GISModel = paths_GISModel[i];

                if (System.IO.Path.GetDirectoryName(path_GISModel) is not string directory)
                {
                    continue;
                }

                string path_BuidlingModelsFile = System.IO.Path.Combine(directory, System.IO.Path.GetFileNameWithoutExtension(path_GISModel) + "." + Analytical.Constants.FileExtension.BuildingModelsFile);

                if (!File.Exists(path_BuidlingModelsFile))
                {
                    continue;
                }

                List<Building2D>? building2Ds = null;
                using (GISModelFile gISModelFile = new(path_GISModel))
                {
                    gISModelFile.Open();

                    building2Ds = gISModelFile?.Value?.GetObjects<Building2D>();
                }

                if (building2Ds == null || building2Ds.Count == 0)
                {
                    continue;
                }

                if (dictionary_EPWFile != null)
                {
                    foreach (Building2D building2D in building2Ds)
                    {
                        EPWFile? ePWFile = GIS.Query.EPWFile(building2D.PolygonalFace2D.Centroid(), dictionary_EPWFile.Values, out double distance);
                    }
                }

                //List<Building2D> building2Ds_Invalid = [];

                //Dictionary<Analytical.Enums.LOD, List<BuildingModel>> dictionary = new Dictionary<Analytical.Enums.LOD, List<BuildingModel>>();
                //using (BuildingModelsFile buildingModelsFile = new BuildingModelsFile(path_BuidlingModelsFile))
                //{
                //    buildingModelsFile.Open();

                //    foreach(Building2D building2D in building2Ds)
                //    {
                //        UniqueReference uniqueReference = BuildingModelsFile.GetUniqueReference(building2D?.Reference);
                //        if(uniqueReference == null)
                //        {
                //            continue;
                //        }

                //        BuildingModel buildingModel = buildingModelsFile.GetValue<BuildingModel>(uniqueReference);
                //        if(buildingModel == null)
                //        {
                //            building2Ds_Invalid.Add(building2D);
                //        }

                //        if (!buildingModel.TryGetValue(Analytical.Enums.BuildingModelParameter.LOD, out Analytical.Enums.LOD lOD, new Core.Parameter.Classes.GetValueSettings(true, false)))
                //        {
                //            lOD = Analytical.Enums.LOD.Undefined;
                //        }

                //        if (!dictionary.TryGetValue(lOD, out List<BuildingModel> buildingModels))
                //        {
                //            buildingModels = new List<BuildingModel>();
                //            dictionary[lOD] = buildingModels;
                //        }

                //        buildingModels.Add(buildingModel);

                //        Point3D point3D = GIS.Convert.ToEPSG4326(building2D.PolygonalFace2D.ExternalEdge.GetInternalPoint());

                //    }

                //}
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

        private void Test_CalculateConstructionDate()
        {
            OpenFolderDialog openFolderDialog = new();
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

                GISModel? gISModel = null;

                using (GISModelFile gISModelFile = new(path_Input))
                {
                    gISModelFile.Open();
                    gISModel = gISModelFile.Value;
                }

                if (gISModel == null)
                {
                    continue;
                }

                List<Building2D>? buidling2Ds = gISModel.GetObjects<Building2D>();
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

            OpenFolderDialog openFolderDialog = new();
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
                if (System.IO.Path.GetDirectoryName(path_Input) is not string directory_Temp)
                {
                    continue;
                }

                GISModel? gISModel_Input = null;

                using (GISModelFile gISModelFile = new(path_Input))
                {
                    gISModelFile.Open();
                    gISModel_Input = gISModelFile.Value;
                }

                if (gISModel_Input == null)
                {
                    continue;
                }

                Building2D? building2D = gISModel_Input.GetObject<Building2D>();

                string path_Output = System.IO.Path.Combine(directory_Temp, System.IO.Path.GetFileNameWithoutExtension(path_Input) + "_Out." + FileExtension.GISModelFile);
                using (GISModelFile gISModelFile = new(path_Output))
                {
                    GISModel gISModel_Output = new();
                    gISModel_Output.Update(building2D);
                    gISModelFile.Value = gISModel_Output;
                    gISModelFile.Save();
                }

                using (GISModelFile gISModelFile = new(path_Output))
                {
                    gISModelFile.Open();
                    GISModel? gISModel_Output = gISModelFile.Value;
                }
            }
        }

        private void ToDiGiGISModelFiles()
        {
            DateTime dateTime = DateTime.Now;

            TextBlock_Progress.Text = "Converting...";

            Convert.ToDiGi_GISModelFiles(this);

            TimeSpan timeSpan = new((DateTime.Now - dateTime).Ticks);

            TextBlock_Progress.Text = string.Format("Done Converting! [{0}]", string.Format("{0}d:{1}h:{2}m:{3}s", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds));
        }

        private void UpdateUpdateAdministrativeAreal2DsCodes()
        {
            OpenFolderDialog? openFolderDialog = null;
            bool? openFolderDialogResult = null;
            string? directory = null;

            openFolderDialog = new()
            {
                Title = "Source"
            };

            openFolderDialogResult = openFolderDialog.ShowDialog(this);
            if (openFolderDialogResult == null || !openFolderDialogResult.HasValue || !openFolderDialogResult.Value)
            {
                return;
            }

            directory = openFolderDialog.FolderName;
            if (string.IsNullOrWhiteSpace(directory) || !Directory.Exists(directory))
            {
                return;
            }

            List<string>? paths_Source = Directory.GetFiles(directory, "*." + FileExtension.GISModelFile, SearchOption.AllDirectories)?.ToList();
            if (paths_Source == null || paths_Source.Count == 0)
            {
                return;
            }

            openFolderDialog = new()
            {
                Title = "Destination"
            };

            openFolderDialogResult = openFolderDialog.ShowDialog(this);
            if (openFolderDialogResult == null || !openFolderDialogResult.HasValue || !openFolderDialogResult.Value)
            {
                return;
            }

            directory = openFolderDialog.FolderName;
            if (string.IsNullOrWhiteSpace(directory) || !Directory.Exists(directory))
            {
                return;
            }

            List<string>? paths_Destination = Directory.GetFiles(directory, "*." + FileExtension.GISModelFile, SearchOption.AllDirectories)?.ToList();
            if (paths_Destination == null || paths_Destination.Count == 0)
            {
                return;
            }

            foreach (string path_Destination in paths_Destination)
            {
                if (string.IsNullOrWhiteSpace(path_Destination))
                {
                    continue;
                }

                string fileName = System.IO.Path.GetFileName(path_Destination);
                if (string.IsNullOrWhiteSpace(fileName))
                {
                    continue;
                }

                string? path_Source = paths_Source.Find(x => System.IO.Path.GetFileName(x) == fileName);
                if (string.IsNullOrWhiteSpace(path_Source))
                {
                    continue;
                }

                using GISModelFile gISModelFile_Destination = new(path_Destination);

                gISModelFile_Destination.Open();
                GISModel? gISModel_Destination = gISModelFile_Destination.Value;
                if (gISModel_Destination is null)
                {
                    continue;
                }

                using GISModelFile gISModelFile_Source = new(path_Source);

                gISModelFile_Source.Open();
                GISModel? gISModel_Source = gISModelFile_Source.Value;
                if (gISModel_Source is null)
                {
                    continue;
                }

                if (!gISModel_Destination.TryGetObjects(out List<AdministrativeAreal2D>? administrativeAreal2Ds_Destination) || administrativeAreal2Ds_Destination is null)
                {
                    continue;
                }

                foreach (AdministrativeAreal2D administrativeAreal2D_Destination in administrativeAreal2Ds_Destination)
                {
                    if (administrativeAreal2D_Destination == null)
                    {
                        continue;
                    }

                    List<AdministrativeAreal2D>? administrativeAreal2Ds_Source = gISModel_Source.GetObjects<AdministrativeAreal2D>(x => x?.Reference == administrativeAreal2D_Destination.Reference && administrativeAreal2D_Destination.GetType() == x?.GetType());
                    if (administrativeAreal2Ds_Source == null || administrativeAreal2Ds_Source.Count != 1)
                    {
                        continue;
                    }

                    AdministrativeAreal2D administrativeAreal2D_Source = administrativeAreal2Ds_Source[0];

                    if (administrativeAreal2D_Destination is AdministrativeDivision administrativeDivision)
                    {
                        gISModel_Destination.Update(new AdministrativeDivision(administrativeDivision, administrativeAreal2D_Source.Code));
                    }
                    else if (administrativeAreal2D_Destination is AdministrativeSubdivision administrativeSubdivision)
                    {
                        gISModel_Destination.Update(new AdministrativeSubdivision(administrativeSubdivision, administrativeAreal2D_Source.Code));
                    }
                    else
                    {
                        continue;
                    }
                }

                gISModelFile_Destination.Value = gISModel_Destination;
                gISModelFile_Destination.Save();
            }
        }

        private void WriteImages()
        {
            DateTime dateTime = DateTime.Now;

            TextBlock_Progress.Text = "Writing...";

            Modify.WriteImages(this, false, new Range<int>(0, 10));

            TimeSpan timeSpan = new((DateTime.Now - dateTime).Ticks);

            TextBlock_Progress.Text = string.Format("Done Writing! [{0}]", string.Format("{0}d:{1}h:{2}m:{3}s", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds));
        }

        private async void WriteStatisticalDataCollections()
        {
            DateTime dateTime = DateTime.Now;

            TextBlock_Progress.Text = "Writing...";

            //Enum.GetValues<Variable>()

            await Modify.WriteStatisticalDataCollections(Enum.GetValues<Variable>(), new Range<int>(2008, DateTime.Now.Year));

            TimeSpan timeSpan = new((DateTime.Now - dateTime).Ticks);

            TextBlock_Progress.Text = string.Format("Done Writing! [{0}]", string.Format("{0}d:{1}h:{2}m:{3}s", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds));
        }

        private async void Button_RefreshOrtoDatas_Click(object sender, RoutedEventArgs e)
        {
            bool result = await PostgreSQL.Modify.RefreshOrtoDatas(gISPostgreSQLConverterManager, new PostgreSQL.Classes.PostgreSQLOrtoDatasRefreshOptions());
            if(result)
            {
                
            }
        }

        private async void Button_UpdateOrtoDatas_FromDatabase_Click(object sender, RoutedEventArgs e)
        {
            PostgreSQL.Classes.OrtoDatasPostgreSQLConverter? ortoDatasPostgreSQLConverter = gISPostgreSQLConverterManager?.GetPostgreSQLConverter<PostgreSQL.Classes.OrtoDatasPostgreSQLConverter>();
            if(ortoDatasPostgreSQLConverter is null)
            {
                return;
            }

            PostgreSQL.Classes.Building2DPostgreSQLConverter? building2DPostgreSQLConverter = gISPostgreSQLConverterManager!.GetPostgreSQLConverter<PostgreSQL.Classes.Building2DPostgreSQLConverter>();
            if (building2DPostgreSQLConverter is null)
            {
                return;
            }

            List<PostgreSQL.Classes.Building2DReference>? building2DReferences = await ortoDatasPostgreSQLConverter.GetNextBuilding2DReferencesAsync(10);
            if(building2DReferences is not null)
            {
                while (building2DReferences.Count > 0)
                {
                    int? countyId = building2DReferences[0].CountyId;

                    Core.Query.Filter(building2DReferences, x => x?.CountyId == countyId, out List<PostgreSQL.Classes.Building2DReference>? building2DReference_In, out List<PostgreSQL.Classes.Building2DReference>? building2DReferences_Out);
                    building2DReferences = building2DReferences_Out ?? [];

                    if(building2DReference_In is not null)
                    {
                        List<PostgreSQL.Classes.Building2D>? building2Ds = await building2DPostgreSQLConverter.GetBuilding2DsByBuilding2DReferences(building2DReference_In);
                        if(building2Ds != null)
                        {
                            OrtoDatasBuilding2DOptions ortoDatasBuilding2DOptions = new();

                            List<PostgreSQL.Classes.OrtoDatas> ortoDatas_PostgreSQL = [];
                            foreach (PostgreSQL.Classes.Building2D building2D in building2Ds)
                            {
                                GIS.Classes.OrtoDatas? ortoDatas = await GIS.Create.OrtoDatas(building2D.ToDiGi(), ortoDatasBuilding2DOptions.Years, ortoDatasBuilding2DOptions.Offset, ortoDatasBuilding2DOptions.Width, ortoDatasBuilding2DOptions.Reduce, squared: true);
                                if (ortoDatas?.ToPostgreSQL(countyId) is not PostgreSQL.Classes.OrtoDatas ortoDatas_PostgreSQL_Temp)
                                {
                                    continue;
                                }

                                ortoDatas_PostgreSQL.Add(ortoDatas_PostgreSQL_Temp);
                            }

                            HashSet<long>? ids = await ortoDatasPostgreSQLConverter.UpdateAsync(ortoDatas_PostgreSQL);
                        }
                    }
                }
            }
        }

        private async void Button_UpdateYearBuiltDatas_Click(object sender, RoutedEventArgs e)
        {
            if (gISPostgreSQLConverterManager is null || !(await gISPostgreSQLConverterManager.TryCreateDatabase<PostgreSQL.Classes.YearBuiltDataPostgreSQLConverter>()))
            {
                return;
            }

            PostgreSQL.Classes.YearBuiltDataPostgreSQLConverter? yearBuiltDataPostgreSQLConverter = gISPostgreSQLConverterManager.GetPostgreSQLConverter<PostgreSQL.Classes.YearBuiltDataPostgreSQLConverter>();
            if (yearBuiltDataPostgreSQLConverter is null)
            {
                return;
            }

            PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter? administrativeAreal2DPostgreSQLConverter = gISPostgreSQLConverterManager.GetPostgreSQLConverter<PostgreSQL.Classes.AdministrativeAreal2DPostgreSQLConverter>();
            if (administrativeAreal2DPostgreSQLConverter is null)
            {
                return;
            }

            //bool clear = true;

            DateTime dateTime = DateTime.Now;

            TextBlock_Progress.Text = "Updating...";

            bool? result;

            OpenFolderDialog openFolderDialog = new();
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
            if (paths_Input == null || paths_Input.Length == 0)
            {
                return;
            }

            //if (clear)
            //{
            //    await yearBuiltDataPostgreSQLConverter.ClearAsync();
            //}

            foreach (string path_Input in paths_Input)
            {
                string? directory_GISModel = System.IO.Path.GetDirectoryName(path_Input);
                if (!Directory.Exists(directory_GISModel))
                {
                    continue;
                }

                GISModel? gISModel_Input = null;

                using (GISModelFile gISModelFile = new(path_Input))
                {
                    gISModelFile.Open();
                    gISModel_Input = gISModelFile.Value;

                    if (gISModel_Input == null)
                    {
                        continue;
                    }

                    List<Building2D>? building2Ds_GIS = gISModel_Input.GetObjects<Building2D>();
                    if (building2Ds_GIS is null || building2Ds_GIS.Count == 0)
                    {
                        continue;
                    }

                    //string? code = null;
                    string? code = gISModel_Input.Reference;
                    if (!string.IsNullOrWhiteSpace(code))
                    {
                        code = code.ToUpper();
                        int index = code.IndexOf('_');
                        if (index != -1)
                        {
                            code = code[..index];
                        }
                    }

                    if (string.IsNullOrWhiteSpace(code))
                    {
                        continue;
                    }

                    int? countyId = await administrativeAreal2DPostgreSQLConverter.GetIdByCodeAsync(code, PostgreSQL.Enums.AdministrativeArealType.County);
                    if (countyId is null || !countyId.HasValue)
                    {
                        continue;
                    }

                    List<Building2D>? building2Ds_Split;

                    SizeSplitter<Building2D> memorySizeSplitter = new(building2Ds_GIS);
                    while ((building2Ds_Split = memorySizeSplitter.Next(200)) is not null)
                    {
                        List<string> references = [];
                        foreach (Building2D building2D in building2Ds_Split)
                        {
                            if (building2D?.Reference is string reference && !string.IsNullOrWhiteSpace(reference))
                            {
                                references.Add(reference);
                            }
                        }

                        Dictionary<string, YearBuiltData>? dictionary = GIS.Query.YearBuiltDataDictionary<YearBuiltData>(gISModelFile, references);
                        if (dictionary is null || dictionary.Count == 0)
                        {
                            continue;
                        }

                        List<PostgreSQL.Classes.YearBuiltData> yearBuiltDatas_PostgreSQL = [];
                        foreach(YearBuiltData yearBuiltData in dictionary.Values)
                        {
                            PostgreSQL.Classes.YearBuiltData? yearBuiltData_PostgreSQL = yearBuiltData.ToPostgreSQL(countyId);
                            if(yearBuiltData_PostgreSQL is not null)
                            {
                                yearBuiltDatas_PostgreSQL.Add(yearBuiltData_PostgreSQL);
                            }

                        }

                        await yearBuiltDataPostgreSQLConverter.UpdateAsync(yearBuiltDatas_PostgreSQL);

                    }
                }


            }

            TimeSpan timeSpan = new((DateTime.Now - dateTime).Ticks);

            TextBlock_Progress.Text = string.Format("Done Updating! [{0}]", string.Format("{0}d:{1}h:{2}m:{3}s", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds));
        }
    }
}