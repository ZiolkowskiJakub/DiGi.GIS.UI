using DiGi.Core;
using DiGi.Core.Classes;
using DiGi.Core.Interfaces;
using DiGi.Core.IO.Table.Classes;
using DiGi.Geometry.Core.Enums;
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
        private DiGi.UI.WPF.Core.Classes.DeterminateWindowWorker determinateWindowWorker;

        public MainWindow()
        {
            InitializeComponent();

            determinateWindowWorker = new DiGi.UI.WPF.Core.Classes.DeterminateWindowWorker(this);

            this.Closed += MainWindow_Closed;
        }

        private void MainWindow_Closed(object? sender, EventArgs e)
        {

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
            }
            ;

            MessageBox.Show("Finished!");
        }

        private void Button_Calculate_Click(object sender, RoutedEventArgs e)
        {
            //Calculate();



            //Test_CalculateConstructionDate();
            //Calculate_OrtoDatas();
        }

        private void Button_CalculateGISModelFiles_Click(object sender, RoutedEventArgs e)
        {
            CalculateGISModelFiles();
        }

        private void Button_CalculateOrtoDatas_Click(object sender, RoutedEventArgs e)
        {
            CalculateOrtoDatas(100);
        }

        private void Button_Reduce_Click(object sender, RoutedEventArgs e)
        {
            Reduce();
        }

        private void Button_ResaveOrtoDatasFiles_Click(object sender, RoutedEventArgs e)
        {
            ResaveOrtoDatasFiles();
        }

        private void Button_Test_Click(object sender, RoutedEventArgs e)
        {
            GISFileModelTest();
        }

        private void Button_ToDiGiGISModelFiles_Click(object sender, RoutedEventArgs e)
        {
            ToDiGiGISModelFiles();
        }

        private void Button_YearBuilts_Click(object sender, RoutedEventArgs e)
        {
            Hide();

            UI.Windows.YearBuiltsWindow yearBuiltsWindow = new UI.Windows.YearBuiltsWindow();
            yearBuiltsWindow.WindowState = WindowState.Maximized;

            yearBuiltsWindow.ShowDialog();

            Close();
        }

        private async void CalculateGISModelFiles()
        {
            DateTime dateTime = DateTime.Now;

            TextBlock_Progress.Text = "Calculating...";

            await Modify.CalculateGISModelFilesAsync(this, determinateWindowWorker);

            TimeSpan timeSpan = new TimeSpan((DateTime.Now - dateTime).Ticks);

            TextBlock_Progress.Text = string.Format("Done Calculating! [{0}]", string.Format("{0}d:{1}h:{2}m:{3}s", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds));
        }

        private async void CalculateOrtoDatas(int count = 100)
        {
            DateTime dateTime = DateTime.Now;

            TextBlock_Progress.Text = "Calculating...";

            OrtoDatasOptions ortoDatasOptions = new OrtoDatasOptions()
            {
                MaxFileSize = (1024UL * 1024UL * 1024UL * 5) / 10
            };

            bool result = await Modify.CalculateOrtoDatas(this, ortoDatasOptions, count);

            TimeSpan timeSpan = new TimeSpan((DateTime.Now - dateTime).Ticks);

            TextBlock_Progress.Text = string.Format("Done Calculating! [{0}]", string.Format("{0}d:{1}h:{2}m:{3}s", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds));
        }

        private async void CalculateOrtoDatasComparisons()
        {
            OrtoDatasComparisonOptions ortoDatasComparisonOptions = new OrtoDatasComparisonOptions();
            //ortoDatasComparisonOptions.OrtoDatasOptions.MaxFileSize = (1024UL * 1024UL * 1024UL * 5) / 10;

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

            Modify.ResaveOrtoDatasFiles(this);

            TimeSpan timeSpan = new TimeSpan((DateTime.Now - dateTime).Ticks);

            TextBlock_Progress.Text = string.Format("Done Resaving! [{0}]", string.Format("{0}d:{1}h:{2}m:{3}s", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds));
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
                if(building2Ds != null && building2Ds.Count > 0)
                {
                    Building2D building2D = building2Ds[0];

                    List<AdministrativeAreal2D> administrativeAreal2Ds = GIS.Query.AdministrativeAreal2Ds<AdministrativeAreal2D>(gISModel, building2D);


                }

            }
        }
    }
}