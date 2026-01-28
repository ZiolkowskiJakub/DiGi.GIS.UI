using DiGi.Core;
using DiGi.Geometry.Planar;
using DiGi.Geometry.Planar.Classes;
using DiGi.GIS.Classes;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace DiGi.GIS.UI
{
    public static partial class Create
    {
        public static BitmapImage? BitmapImage(this OrtoData? ortoData)
        {
            if (ortoData == null)
            {
                return null;
            }

            return DiGi.UI.WPF.Core.Create.BitmapImage(ortoData?.Bytes);
        }

        public static BitmapImage? BitmapImage(this Building2D? building2D, string? directory, int year)
        {
            if (directory == null || building2D == null || year <= 0)
            {
                return null;
            }

            OrtoDatas? ortoDatas = building2D.OrtoDatas(directory);
            if (ortoDatas == null)
            {
                return null;
            }

            Polygon2D? polygon2D = building2D?.PolygonalFace2D?.ExternalEdge as Polygon2D;

            List<Point2D>? point2Ds = polygon2D?.GetPoints();
            if (point2Ds == null)
            {
                return null;
            }

            Polygon2D? polygon2D_Offset = polygon2D.Offset(0.5)?.FirstOrDefault();

            List<Point2D>? point2Ds_Offset = polygon2D_Offset?.GetPoints();

            BitmapImage? result = null;

            OrtoData? ortoData = ortoDatas.GetOrtoData(new DateTime(year, 1, 1));
            byte[]? bytes = ortoData?.Bytes;
            if (bytes == null)
            {
                return null;
            }

            using (Image image = Image.FromStream(new MemoryStream(bytes)))
            {
                List<Point2D> point2Ds_Temp = [];
                for (int j = 0; j < point2Ds.Count; j++)
                {
                    if (ortoData!.ToOrto(point2Ds[j]) is Point2D point2D)
                    {
                        point2Ds_Temp.Add(point2D);
                    }
                }

                List<Point2D>? point2Ds_Offset_Temp = null;
                if (point2Ds_Offset != null)
                {
                    point2Ds_Offset_Temp = [];
                    for (int j = 0; j < point2Ds_Offset.Count; j++)
                    {
                        if (ortoData!.ToOrto(point2Ds_Offset[j]) is Point2D point2D)
                        {
                            point2Ds_Offset_Temp.Add(point2D);
                        }
                    }
                }

                using (Graphics graphics = Graphics.FromImage(image))
                {
                    Polygon2D polygon2D_OrtoData = new(point2Ds_Temp);

                    Geometry.Drawing.Modify.Draw(graphics, polygon2D_OrtoData, new Pen(Color.Black.ToDiGi(), 3), false);
                    Geometry.Drawing.Modify.Draw(graphics, polygon2D_OrtoData.GetBoundingBox(), new Pen(Color.Gray.ToDiGi(), 1), false);

                    if (point2Ds_Offset_Temp != null)
                    {
                        Polygon2D polygon2D_OrtoData_Offset = new(point2Ds_Offset_Temp);

                        Geometry.Drawing.Modify.Draw(graphics, polygon2D_OrtoData_Offset, new Pen(Color.Red.ToDiGi(), 3), false);
                        Geometry.Drawing.Modify.Draw(graphics, polygon2D_OrtoData_Offset.GetBoundingBox(), new Pen(Color.Gray.ToDiGi(), 1), false);
                    }
                }

                result = DiGi.UI.WPF.Core.Create.BitmapImage(image);
            }

            return result;
        }

        public static BitmapImage? BitmapImage(this Building2D? building2D, int width, int height, double offset = 0.1)
        {
            if (building2D == null || width == -1 || height == -1)
            {
                return null;
            }

            Polygon2D? polygon2D = building2D?.PolygonalFace2D?.ExternalEdge as Polygon2D;

            polygon2D = new BoundingBox2D(Geometry.Planar.Constans.Point2D.Zero, new Point2D(width, height)).Fit(polygon2D, offset);
            if (polygon2D is null)
            {
                return null;
            }

            BitmapImage? result = null;

            using (Image image = new Bitmap(width, height))
            {
                using (Graphics graphics = Graphics.FromImage(image))
                {
                    Geometry.Drawing.Modify.Draw(graphics, polygon2D, new Pen(Color.Black.ToDiGi(), 3), false);
                    Geometry.Drawing.Modify.Draw(graphics, polygon2D.GetBoundingBox(), new Pen(Color.Gray.ToDiGi(), 1), false);
                }

                result = DiGi.UI.WPF.Core.Create.BitmapImage(image);
            }

            return result;
        }
    }
}