namespace DiGi.GIS.UI
{
    public static partial class Query
    {
        /// <summary>
        /// Initializes and returns a dictionary containing the default weights for all available YOLO categories.
        /// </summary>
        /// <returns>A <see cref="Dictionary{TKey, TValue}"/> where each <see cref="DiGi.YOLO.Enums.Category"/> is mapped to its default weight value.</returns>
        public static Dictionary<DiGi.YOLO.Enums.Category, double> Weights()
        {
            Dictionary<DiGi.YOLO.Enums.Category, double> result = [];

            foreach (DiGi.YOLO.Enums.Category category in Enum.GetValues<DiGi.YOLO.Enums.Category>())
            {
                result[category] = 1;
            }

            return result;
        }
    }
}