namespace DiGi.GIS.UI
{
    public static partial class Query
    {
        public static Dictionary<YOLO.Enums.Category, double> Weights()
        {
            Dictionary<YOLO.Enums.Category, double> result = new Dictionary<YOLO.Enums.Category, double>();

            foreach(YOLO.Enums.Category category in Enum.GetValues(typeof(YOLO.Enums.Category)))
            {
                result[category] = 1;
            }

            return result;
        }
    }
}
