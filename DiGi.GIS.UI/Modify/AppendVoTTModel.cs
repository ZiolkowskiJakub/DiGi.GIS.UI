using System.IO;
using Microsoft.Win32;
using System.Windows;
using DiGi.GIS.Classes;
using DiGi.VoTT.Classes;
using System.Windows.Media.Imaging;
using DiGi.VoTT;
using DiGi.Geometry.Planar.Classes;

namespace DiGi.GIS.UI
{
    public static partial class Modify
    {
        public static void AppendVoTTModel_Building2D(Window owner, VoTTConversionOptions voTTConversionOptions = null)
        {
            bool? result;

            OpenFolderDialog openFolderDialog;

            openFolderDialog = new OpenFolderDialog();
            openFolderDialog.Title = "Select GIS Model Files directory";
            result = openFolderDialog.ShowDialog(owner);
            if (result == null || !result.HasValue || !result.Value)
            {
                return;
            }

            string directory_GISModelFiles = openFolderDialog.FolderName;
            if (string.IsNullOrWhiteSpace(directory_GISModelFiles) || !Directory.Exists(directory_GISModelFiles))
            {
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = string.Format("{0} (*.{1})|*.{1}|All files (*.*)|*.*", "VoTT json File", "json");
            saveFileDialog.FileName = "VoTT.json";
            saveFileDialog.Title = "Select VoTT File";
            saveFileDialog.OverwritePrompt = false;
            result = saveFileDialog.ShowDialog(owner);
            if (result == null || !result.HasValue || !result.Value)
            {
                return;
            }

            if(voTTConversionOptions == null)
            {
                voTTConversionOptions = new VoTTConversionOptions();
            }

            string path_VoTT = saveFileDialog.FileName;

            VoTTModel voTTModel = VoTT.Modify.Read(path_VoTT);
            if (voTTModel == null)
            {
                voTTModel = new VoTTModel()
                {
                    name = "Building2D VoTT",
                };
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
                            short? yearBuilt = gISModelFile.YearBuilt(building2D);
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

                string directory_VoTT = Path.GetDirectoryName(path_VoTT);
                if (!Directory.Exists(directory_VoTT))
                {
                    Directory.CreateDirectory(directory_VoTT);
                }

                foreach (Tuple<Building2D, short> tuple in tuples)
                {
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

                    string pathPrefix = Path.Combine(directory_VoTT, building2D.Reference);

                    foreach (OrtoData ortoData in ortoDatas)
                    {
                        int year = ortoData.DateTime.Year;

                        if (year < yearBuilt)
                        {
                            continue;
                        }

                        BitmapImage bitmapImage = ortoData.BitmapImage();
                        if (bitmapImage == null)
                        {
                            continue;
                        }

                        string path_OrtoData = string.Format("{0}_{1}.jpeg", pathPrefix, year);

                        JpegBitmapEncoder jpegBitmapEncoder = new JpegBitmapEncoder();
                        jpegBitmapEncoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                        using (FileStream fileStream = new FileStream(path_OrtoData, FileMode.Create))
                        {
                            jpegBitmapEncoder.Save(fileStream);
                        }

                        BoundingBox2D boundingBox2D = building2DGeometryCalculationResult.BoundingBox;

                        if (voTTConversionOptions != null)
                        {
                            boundingBox2D.Offset(voTTConversionOptions.Offset);
                        }

                        Point2D min = ortoData.ToOrto(boundingBox2D.Min);
                        Point2D max = ortoData.ToOrto(boundingBox2D.Max);

                        Asset asset = VoTT.Create.Asset(path_OrtoData);

                        voTTModel.Add(asset);

                        voTTModel.Add(asset.id, VoTT.Create.Region(min.X, min.Y, max.X, max.Y, year.ToString()));

                    }
                }

                VoTT.Modify.Write(voTTModel, path_VoTT);
            }
        }

        public static void AppendVoTTModel_OrtoRange(Window owner, VoTTConversionOptions voTTConversionOptions = null)
        {
            bool? result;

            OpenFolderDialog openFolderDialog;

            openFolderDialog = new OpenFolderDialog();
            openFolderDialog.Title = "Select GIS Model Files directory";
            result = openFolderDialog.ShowDialog(owner);
            if (result == null || !result.HasValue || !result.Value)
            {
                return;
            }

            string directory_GISModelFiles = openFolderDialog.FolderName;
            if (string.IsNullOrWhiteSpace(directory_GISModelFiles) || !Directory.Exists(directory_GISModelFiles))
            {
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = string.Format("{0} (*.{1})|*.{1}|All files (*.*)|*.*", "VoTT json File", "json");
            saveFileDialog.FileName = "VoTT.json";
            saveFileDialog.Title = "Select VoTT File";
            saveFileDialog.OverwritePrompt = false;
            result = saveFileDialog.ShowDialog(owner);
            if (result == null || !result.HasValue || !result.Value)
            {
                return;
            }

            if (voTTConversionOptions == null)
            {
                voTTConversionOptions = new VoTTConversionOptions();
            }

            string path_VoTT = saveFileDialog.FileName;

            VoTTModel voTTModel = VoTT.Modify.Read(path_VoTT);
            if (voTTModel == null)
            {
                voTTModel = new VoTTModel()
                {
                    name = "OrtoRange VoTT",
                };
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
                            short? yearBuilt = gISModelFile.YearBuilt(building2D);
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
                if(!File.Exists(path_OrtoRange))
                {
                    continue;
                }

                IEnumerable<OrtoRange> ortoRanges = null;
                using (OrtoRangeFile ortoRangeFile = new OrtoRangeFile(path_OrtoRange))
                {
                    ortoRanges = ortoRangeFile.Values;
                }

                string directory_VoTT = Path.GetDirectoryName(path_VoTT);
                if (!Directory.Exists(directory_VoTT))
                {
                    Directory.CreateDirectory(directory_VoTT);
                }

                for(int j = 0; j < ortoRanges.Count(); j++)
                {
                    OrtoRange ortoRange = ortoRanges.ElementAt(j);

                    HashSet<string> references_Inside = ortoRange?.References_Inside;
                    if(references_Inside == null || references_Inside.Count == 0)
                    {
                        continue;
                    }

                    List<Tuple<Building2D, short>> tuples_OrtoRange = tuples.FindAll(x => references_Inside.Contains(x.Item1.Reference));
                    if (tuples_OrtoRange == null || tuples_OrtoRange.Count == 0)
                    {
                        continue;
                    }

                    OrtoDatas ortoDatas = ortoRange.OrtoDatas(directory_OrtoDatas);
                    if(ortoDatas == null)
                    {
                        continue;
                    }

                    string pathPrefix = Path.Combine(directory_VoTT, ortoRange.UniqueId);

                    foreach (OrtoData ortoData in ortoDatas)
                    {
                        BitmapImage bitmapImage = ortoData.BitmapImage();
                        if (bitmapImage == null)
                        {
                            continue;
                        }

                        int year = ortoData.DateTime.Year;

                        string path_OrtoData = string.Format("{0}_{1}.jpeg", pathPrefix, year);

                        JpegBitmapEncoder jpegBitmapEncoder = new JpegBitmapEncoder();
                        jpegBitmapEncoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                        using (FileStream fileStream = new FileStream(path_OrtoData, FileMode.Create))
                        {
                            jpegBitmapEncoder.Save(fileStream);
                        }

                        Asset asset = VoTT.Create.Asset(path_OrtoData);

                        voTTModel.Add(asset);

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

                            if(voTTConversionOptions != null)
                            {
                                boundingBox2D.Offset(voTTConversionOptions.Offset);
                            }

                            Point2D min = ortoData.ToOrto(boundingBox2D.Min);
                            Point2D max = ortoData.ToOrto(boundingBox2D.Max);

                            voTTModel.Add(asset.id, VoTT.Create.Region(min.X, min.Y, max.X, max.Y, year.ToString()));
                        }
                    }
                }

                VoTT.Modify.Write(voTTModel, path_VoTT);
            }
        }
    }
}
