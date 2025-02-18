using DiGi.GIS.Classes;
using System.IO;
using System.Drawing;
using DiGi.Geometry.Planar;
using DiGi.Geometry.Planar.Classes;
using System.Drawing.Imaging;
using DiGi.Core;

namespace DiGi.GIS.UI
{
    public static partial class Modify
    {
        public static bool Save(this GISModelFile gISModelFile, Building2D building2D, string directory)
        {
            if(gISModelFile == null || building2D == null || string.IsNullOrWhiteSpace(directory))
            {
                return false;
            }

            OrtoDatas ortoDatas = gISModelFile.OrtoDatas(building2D);
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

                    image.Save(Path.Combine(directory, fileName), ImageFormat.Jpeg);
                    result = true;
                }
            }

            return result;
        }
    }
}
