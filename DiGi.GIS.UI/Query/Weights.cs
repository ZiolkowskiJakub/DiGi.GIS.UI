namespace DiGi.GIS.UI
{
    public static partial class Query
    {
        /// <summary>
        /// Initializes and returns a dictionary containing the default weights for all available YOLO categories.
        /// </summary>
        /// <returns>A <see cref="Dictionary{TKey, TValue}"/> where each <see cref="YOLO.Enums.Category"/> is mapped to its default weight value.</returns>
        public static Dictionary<YOLO.Enums.Category, double> Weights()
        {
            Dictionary<YOLO.Enums.Category, double> result = [];

            foreach (YOLO.Enums.Category category in Enum.GetValues<YOLO.Enums.Category>())
            {
                result[category] = 1;
            }

            return result;
        }
    }
}