using DiGi.Core;
using DiGi.Geometry.Planar;
using DiGi.Geometry.Planar.Classes;
using DiGi.GIS.Classes;
using DiGi.GIS.Constans;
using Microsoft.Win32;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;

namespace DiGi.GIS.UI
{
    public static partial class Modify
    {
        public static bool WriteImages(this Building2D building2D, string directory_OrtoDatasFiles, string directory_Output)
        {
            if(directory_OrtoDatasFiles == null || building2D == null || string.IsNullOrWhiteSpace(directory_Output))
            {
                return false;
            }

            if(!Directory.Exists(directory_OrtoDatasFiles) || !Directory.Exists(directory_Output))
            {
                return false;
            }

            OrtoDatas ortoDatas = building2D.OrtoDatas(directory_OrtoDatasFiles);
            if (ortoDatas == null)
            {
                return false;
            }

            Polygon2D polygon2D = building2D?.PolygonalFace2D?.ExternalEdge as Polygon2D;

            List<Point2D> point2Ds = polygon2D?.GetPoints();
            if (point2Ds == null)
            {
                return false;
            }

            Polygon2D polygon2D_Offset = polygon2D.Offset(0.5)?.FirstOrDefault();

            List<Point2D> point2Ds_Offset = polygon2D_Offset?.GetPoints();

            bool result = false;

            foreach (OrtoData ortoData in ortoDatas)
            {
                byte[] bytes = ortoData?.Bytes;
                if (bytes == null)
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
                    if (point2Ds_Offset != null)
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

                        Geometry.Drawing.Modify.Draw(graphics, polygon2D_OrtoData, new Pen(Color.Black.ToDiGi(), 3), false);
                        Geometry.Drawing.Modify.Draw(graphics, polygon2D_OrtoData.GetBoundingBox(), new Pen(Color.Gray.ToDiGi(), 1), false);

                        if (point2Ds_Offset_Temp != null)
                        {
                            Polygon2D polygon2D_OrtoData_Offset = new Polygon2D(point2Ds_Offset_Temp);

                            Geometry.Drawing.Modify.Draw(graphics, polygon2D_OrtoData_Offset, new Pen(Color.Red.ToDiGi(), 3), false);
                            Geometry.Drawing.Modify.Draw(graphics, polygon2D_OrtoData_Offset.GetBoundingBox(), new Pen(Color.Gray.ToDiGi(), 1), false);
                        }
                    }

                    image.Save(Path.Combine(directory_Output, fileName), ImageFormat.Jpeg);
                    result = true;
                }
            }

            return result;
        }

        public static bool WriteImages(Window owner, bool drawGeometry = false, Core.Classes.Range<int> range = null)
        {
            bool? dialogResult;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select GIS Model file";
            openFileDialog.Filter = string.Format("{0} (*.{1})|*.{1}|All files (*.*)|*.*", FileTypeName.GISModelFile, FileExtension.GISModelFile);
            dialogResult = openFileDialog.ShowDialog();
            if (dialogResult == null || !dialogResult.HasValue || !dialogResult.Value)
            {
                return false;
            }

            string path_GISModel = openFileDialog.FileName;

            OpenFolderDialog openFolderDialog = new OpenFolderDialog();
            openFolderDialog.Title = "Select destination directory";
            dialogResult = openFolderDialog.ShowDialog(owner);
            if (dialogResult == null || !dialogResult.HasValue || !dialogResult.Value)
            {
                return false;
            }

            string directory_Output = openFolderDialog.FolderName;

            List<Building2D> building2Ds = new List<Building2D>();
            using (GISModelFile gISModelFile = new GISModelFile(path_GISModel))
            {
                gISModelFile.Open();

                building2Ds = gISModelFile.Value?.GetObjects<Building2D>();
            }

            if (range != null)
            {
                building2Ds = building2Ds.GetRange(range.Min, range.Max - range.Min);
            }

            string directory_OrtoDatas = GIS.Query.OrtoDatasDirectory_Building2D(Path.GetDirectoryName(path_GISModel));

            Dictionary<Core.Classes.GuidReference, OrtoDatas> dictionary = GIS.Query.OrtoDatasDictionary(directory_OrtoDatas, building2Ds);

            bool result = false;

            foreach(Building2D building2D in building2Ds)
            {
                if(building2D == null || !dictionary.TryGetValue(new Core.Classes.GuidReference(building2D), out OrtoDatas ortoDatas) || ortoDatas == null)
                {
                    continue;
                }

                Polygon2D polygon2D = building2D?.PolygonalFace2D?.ExternalEdge as Polygon2D;

                List<Point2D> point2Ds = polygon2D?.GetPoints();
                if (point2Ds == null)
                {
                    return false;
                }

                Polygon2D polygon2D_Offset = polygon2D.Offset(0.5)?.FirstOrDefault();

                List<Point2D> point2Ds_Offset = polygon2D_Offset?.GetPoints();

                foreach (OrtoData ortoData in ortoDatas)
                {
                    byte[] bytes = ortoData?.Bytes;
                    if (bytes == null)
                    {
                        continue;
                    }

                    string fileName = string.Format("{0}_{1}.{2}", ortoDatas.Reference, ortoData.DateTime.Year.ToString(), "jpeg");

                    using (Image image = Image.FromStream(new MemoryStream(bytes)))
                    {
                        if(drawGeometry)
                        {
                            List<Point2D> point2Ds_Temp = new List<Point2D>();
                            for (int j = 0; j < point2Ds.Count; j++)
                            {
                                point2Ds_Temp.Add(ortoData.ToOrto(point2Ds[j]));
                            }

                            List<Point2D> point2Ds_Offset_Temp = null;
                            if (point2Ds_Offset != null)
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

                                Geometry.Drawing.Modify.Draw(graphics, polygon2D_OrtoData, new Pen(Color.Black.ToDiGi(), 3), false);
                                Geometry.Drawing.Modify.Draw(graphics, polygon2D_OrtoData.GetBoundingBox(), new Pen(Color.Gray.ToDiGi(), 1), false);

                                if (point2Ds_Offset_Temp != null)
                                {
                                    Polygon2D polygon2D_OrtoData_Offset = new Polygon2D(point2Ds_Offset_Temp);

                                    Geometry.Drawing.Modify.Draw(graphics, polygon2D_OrtoData_Offset, new Pen(Color.Red.ToDiGi(), 3), false);
                                    Geometry.Drawing.Modify.Draw(graphics, polygon2D_OrtoData_Offset.GetBoundingBox(), new Pen(Color.Gray.ToDiGi(), 1), false);
                                }
                            }
                        }

                        image.Save(Path.Combine(directory_Output, fileName), ImageFormat.Jpeg);
                        result = true;
                    }
                }
            }

            return result;

        }
    }
}
