using DiGi.Geometry.Planar.Classes;
using DiGi.GIS.Classes;
using DiGi.GIS.UI.Classes;
using DiGi.YOLO.Classes;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace DiGi.GIS.UI
{
    public static partial class Modify
    {
        public static bool AppendYOLOModel_Building2D(Window owner, YOLOConversionOptions yOLOConversionOptions = null)
        {
            if (yOLOConversionOptions == null)
            {
                yOLOConversionOptions = new YOLOConversionOptions();
            }

            if (string.IsNullOrWhiteSpace(yOLOConversionOptions.GISModelFilesDirectory) || !Directory.Exists(yOLOConversionOptions.GISModelFilesDirectory))
            {
                OpenFolderDialog openFolderDialog = new OpenFolderDialog();
                openFolderDialog.Title = "Select GIS Model Files directory";
                bool? result = openFolderDialog.ShowDialog(owner);
                if (result == null || !result.HasValue || !result.Value)
                {
                    return false;
                }

                yOLOConversionOptions.GISModelFilesDirectory = openFolderDialog.FolderName;
            }

            string directory_GISModelFiles = yOLOConversionOptions.GISModelFilesDirectory;
            if (string.IsNullOrWhiteSpace(directory_GISModelFiles) || !Directory.Exists(directory_GISModelFiles))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(yOLOConversionOptions.ConfigurationFilePath) || !File.Exists(yOLOConversionOptions.ConfigurationFilePath))
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = string.Format("{0} (*.{1})|*.{1}|All files (*.*)|*.*", "YOLO yaml File", "yaml");
                saveFileDialog.FileName = "conf.yaml";
                saveFileDialog.Title = "Select YOLO yaml file";
                saveFileDialog.OverwritePrompt = false;
                bool? result = saveFileDialog.ShowDialog(owner);
                if (result == null || !result.HasValue || !result.Value)
                {
                    return false;
                }

                yOLOConversionOptions.ConfigurationFilePath = saveFileDialog.FileName;
            }

            string path_YOLO = yOLOConversionOptions.ConfigurationFilePath;
            if(string.IsNullOrWhiteSpace(path_YOLO))
            {
                return false;
            }

            YOLOModel yOLOModel = YOLO.Modify.Read(path_YOLO);
            if (yOLOModel == null)
            {
                yOLOModel = new YOLOModel(Path.GetDirectoryName(path_YOLO));
            }

            if(yOLOConversionOptions.ClearData)
            {
                YOLO.Modify.ClearData(yOLOModel);
            }

            string[] paths_Input = Directory.GetFiles(directory_GISModelFiles, "*." + Constans.FileExtension.GISModelFile, SearchOption.AllDirectories);
            for (int i = 0; i < paths_Input.Length; i++)
            {
                string path_Input = paths_Input[i];

                string directory_OrtoDatas = GIS.Query.Directory(Path.GetDirectoryName(path_Input), GIS.Query.OrtoDatasDirectoryNames_Building2D());

                GISModel gISModel = null;
                List<Tuple<Building2D, short>> tuples = new List<Tuple<Building2D, short>>();
                using (GISModelFile gISModelFile = new GISModelFile(path_Input))
                {
                    gISModelFile.Open();

                    gISModel = gISModelFile.Value;
                    List<Building2D> building2Ds = gISModel?.GetObjects<Building2D>();
                    if(building2Ds != null)
                    {
                        foreach(Building2D building2D in building2Ds)
                        {
                            short? yearBuilt = gISModelFile.UserYearBuilt(building2D);
                            if(yearBuilt == null || !yearBuilt.HasValue)
                            {
                                continue;
                            }

                            tuples.Add(new Tuple<Building2D, short>(building2D, yearBuilt.Value));
                        }
                    }
                }

                if (tuples == null || tuples.Count == 0)
                {
                    continue;
                }

                Random random = new Random(tuples.Count);

                foreach (Tuple<Building2D, short> tuple in tuples)
                {
                    YOLO.Enums.Category? category = yOLOConversionOptions.Category(random);
                    if(category == null || !category.HasValue)
                    {
                        category = YOLO.Enums.Category.Train;
                    }

                    Building2D building2D = tuple.Item1;

                    int yearBuilt = tuple.Item2;

                    Building2DGeometryCalculationResult building2DGeometryCalculationResult = gISModel.GetRelatedObject<Building2DGeometryCalculationResult>(building2D);
                    if(building2DGeometryCalculationResult == null)
                    {
                        building2DGeometryCalculationResult = building2D.Building2DGeometryCalculationResult();
                    }

                    OrtoDatas ortoDatas = building2D.OrtoDatas(directory_OrtoDatas);
                    if (ortoDatas == null)
                    {
                        continue;
                    }

                    string directory = yOLOModel.GetDirectory_Images(category.Value);
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    string pathPrefix = Path.Combine(directory, building2D.Reference);

                    foreach (OrtoData ortoData in ortoDatas)
                    {
                        int year = ortoData.DateTime.Year;

                        BitmapImage bitmapImage = ortoData.BitmapImage();
                        if (bitmapImage == null)
                        {
                            continue;
                        }

                        string path_Image = string.Format("{0}_{1}.jpeg", pathPrefix, year);

                        JpegBitmapEncoder jpegBitmapEncoder = new JpegBitmapEncoder();
                        jpegBitmapEncoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                        using (FileStream fileStream = new FileStream(path_Image, FileMode.Create))
                        {
                            jpegBitmapEncoder.Save(fileStream);
                        }

                        yOLOModel.Add(path_Image, category.Value);
                        if (year < yearBuilt)
                        {
                            continue;
                        }

                        BoundingBox2D boundingBox2D = building2DGeometryCalculationResult.BoundingBox;

                        if (yOLOConversionOptions != null)
                        {
                            boundingBox2D.Offset(yOLOConversionOptions.Offset);
                        }

                        Point2D topLeft = ortoData.ToOrto(boundingBox2D.TopLeft);
                        Point2D bottomRight = ortoData.ToOrto(boundingBox2D.BottomRight);

                        yOLOModel.Add(path_Image, "Building", YOLO.Create.BoundingBox(bitmapImage.Width, bitmapImage.Height, topLeft.X, topLeft.Y, bottomRight.X - topLeft.X, bottomRight.Y - topLeft.Y));
                    }
                }

                YOLO.Modify.Write(yOLOModel);
            }

            return true;
        }

        public static bool AppendYOLOModel_OrtoRange(Window owner, YOLOConversionOptions yOLOConversionOptions = null)
        {
            if (yOLOConversionOptions == null)
            {
                yOLOConversionOptions = new YOLOConversionOptions();
            }

            if (string.IsNullOrWhiteSpace(yOLOConversionOptions.GISModelFilesDirectory) || !Directory.Exists(yOLOConversionOptions.GISModelFilesDirectory))
            {
                OpenFolderDialog openFolderDialog = new OpenFolderDialog();
                openFolderDialog.Title = "Select GIS Model Files directory";
                bool? result = openFolderDialog.ShowDialog(owner);
                if (result == null || !result.HasValue || !result.Value)
                {
                    return false;
                }

                yOLOConversionOptions.GISModelFilesDirectory = openFolderDialog.FolderName;
            }

            string directory_GISModelFiles = yOLOConversionOptions.GISModelFilesDirectory;
            if (string.IsNullOrWhiteSpace(directory_GISModelFiles) || !Directory.Exists(directory_GISModelFiles))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(yOLOConversionOptions.ConfigurationFilePath) || !File.Exists(yOLOConversionOptions.ConfigurationFilePath))
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = string.Format("{0} (*.{1})|*.{1}|All files (*.*)|*.*", "YOLO yaml File", "yaml");
                saveFileDialog.FileName = "conf.yaml";
                saveFileDialog.Title = "Select YOLO yaml file";
                saveFileDialog.OverwritePrompt = false;
                bool? result = saveFileDialog.ShowDialog(owner);
                if (result == null || !result.HasValue || !result.Value)
                {
                    return false;
                }

                yOLOConversionOptions.ConfigurationFilePath = saveFileDialog.FileName;
            }



            string path_YOLO = yOLOConversionOptions.ConfigurationFilePath;
            if (string.IsNullOrWhiteSpace(path_YOLO))
            {
                return false;
            }

            YOLOModel yOLOModel = YOLO.Modify.Read(path_YOLO);
            if (yOLOModel == null)
            {
                yOLOModel = new YOLOModel(Path.GetDirectoryName(path_YOLO));
            }

            if (yOLOConversionOptions.ClearData)
            {
                YOLO.Modify.ClearData(yOLOModel);
            }

            string[] paths_Input = Directory.GetFiles(directory_GISModelFiles, "*." + Constans.FileExtension.GISModelFile, SearchOption.AllDirectories);
            for (int i = 0; i < paths_Input.Length; i++)
            {
                string path_Input = paths_Input[i];

                string directory_Input = Path.GetDirectoryName(path_Input);

                string directory_OrtoDatas = GIS.Query.Directory(directory_Input, GIS.Query.OrtoDatasDirectoryNames_OrtoRange());

                GISModel gISModel = null;
                List<Tuple<Building2D, short>> tuples = new List<Tuple<Building2D, short>>();
                using (GISModelFile gISModelFile = new GISModelFile(path_Input))
                {
                    gISModelFile.Open();

                    gISModel = gISModelFile.Value;
                    List<Building2D> building2Ds = gISModel?.GetObjects<Building2D>();
                    if (building2Ds != null)
                    {
                        foreach (Building2D building2D in building2Ds)
                        {
                            short? yearBuilt = gISModelFile.UserYearBuilt(building2D);
                            if (yearBuilt == null || !yearBuilt.HasValue)
                            {
                                continue;
                            }

                            tuples.Add(new Tuple<Building2D, short>(building2D, yearBuilt.Value));
                        }
                    }

                }

                if (tuples == null || tuples.Count == 0)
                {
                    continue;
                }

                string path_OrtoRange = Path.Combine(directory_Input, string.Format("{0}.{1}", Path.GetFileNameWithoutExtension(path_Input), Constans.FileExtension.OrtoRangeFile));
                if (!File.Exists(path_OrtoRange))
                {
                    continue;
                }

                IEnumerable<OrtoRange> ortoRanges = null;
                using (OrtoRangeFile ortoRangeFile = new OrtoRangeFile(path_OrtoRange))
                {
                    ortoRanges = ortoRangeFile.Values;
                }

                Random random = new Random(tuples.Count);

                for (int j = 0; j < ortoRanges.Count(); j++)
                {
                    OrtoRange ortoRange = ortoRanges.ElementAt(j);

                    HashSet<string> references_Inside = ortoRange?.References_Inside;
                    if (references_Inside == null || references_Inside.Count == 0)
                    {
                        continue;
                    }

                    List<Tuple<Building2D, short>> tuples_OrtoRange = tuples.FindAll(x => references_Inside.Contains(x.Item1.Reference));
                    if (tuples_OrtoRange == null || tuples_OrtoRange.Count == 0)
                    {
                        continue;
                    }

                    if(references_Inside.Count != tuples_OrtoRange.Count)
                    {
                        continue;
                    }

                    OrtoDatas ortoDatas = ortoRange.OrtoDatas(directory_OrtoDatas);
                    if (ortoDatas == null)
                    {
                        continue;
                    }

                    YOLO.Enums.Category? category = yOLOConversionOptions.Category(random);
                    if (category == null || !category.HasValue)
                    {
                        category = YOLO.Enums.Category.Train;
                    }

                    string directory = yOLOModel.GetDirectory_Images(category.Value);
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    string pathPrefix = Path.Combine(directory, ortoRange.UniqueId);

                    foreach (OrtoData ortoData in ortoDatas)
                    {
                        BitmapImage bitmapImage = ortoData.BitmapImage();
                        if (bitmapImage == null)
                        {
                            continue;
                        }

                        int year = ortoData.DateTime.Year;

                        string path_Image = string.Format("{0}_{1}.jpeg", pathPrefix, year);

                        JpegBitmapEncoder jpegBitmapEncoder = new JpegBitmapEncoder();
                        jpegBitmapEncoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                        using (FileStream fileStream = new FileStream(path_Image, FileMode.Create))
                        {
                            jpegBitmapEncoder.Save(fileStream);
                        }

                        yOLOModel.Add(path_Image, category.Value);

                        foreach (Tuple<Building2D, short> tuple in tuples_OrtoRange)
                        {
                            int yearBuilt = tuple.Item2;

                            if (year < yearBuilt)
                            {
                                continue;
                            }

                            Building2D building2D = tuple.Item1;

                            Building2DGeometryCalculationResult building2DGeometryCalculationResult = gISModel.GetRelatedObject<Building2DGeometryCalculationResult>(building2D);
                            if (building2DGeometryCalculationResult == null)
                            {
                                building2DGeometryCalculationResult = building2D.Building2DGeometryCalculationResult();
                            }

                            BoundingBox2D boundingBox2D = building2DGeometryCalculationResult.BoundingBox;

                            if (yOLOConversionOptions != null)
                            {
                                boundingBox2D.Offset(yOLOConversionOptions.Offset);
                            }

                            Point2D topLeft = ortoData.ToOrto(boundingBox2D.TopLeft);
                            Point2D bottomRight = ortoData.ToOrto(boundingBox2D.BottomRight);

                            yOLOModel.Add(path_Image, "Building", YOLO.Create.BoundingBox(bitmapImage.Width, bitmapImage.Height, topLeft.X, topLeft.Y, bottomRight.X - topLeft.X, bottomRight.Y - topLeft.Y));
                        }
                    }
                }

                YOLO.Modify.Write(yOLOModel);
            }

            return true;
        }
    }
}
