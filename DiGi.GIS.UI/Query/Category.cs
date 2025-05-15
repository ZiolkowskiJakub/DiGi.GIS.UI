using DiGi.GIS.UI.Classes;

namespace DiGi.GIS.UI
{
    public static partial class Query
    {
        public static YOLO.Enums.Category? Category(this Dictionary<YOLO.Enums.Category, double> weights, Random random = null)
        {
            if(weights == null || weights.Count == 0)
            {
                return null;
            }

            if(random == null)
            {
                random = new Random();
            }

            double sum = 0;
            foreach(double weight in weights.Values)
            {
                sum += weight;
            }

            double value = sum * (System.Convert.ToDouble(random.Next(1, 100)) / 100.0);

            double current = 0;
            foreach(KeyValuePair<YOLO.Enums.Category, double> keyValuePair in weights)
            {
                current += keyValuePair.Value;
                if(current >= value)
                {
                    return keyValuePair.Key;
                }
            }

            return null;
        }

        public static YOLO.Enums.Category? Category(this YOLOConversionOptions yOLOConversionOptions, Random random = null)
        {
            return Category(yOLOConversionOptions?.Weights, random);
        }
    }
}
