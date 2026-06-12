using DiGi.GIS.UI.Classes;

namespace DiGi.GIS.UI
{
    public static partial class Query
    {
        /// <summary>
        /// Selects a category from the provided weight distribution using a weighted random selection process.
        /// </summary>
        /// <param name="weights">A dictionary containing categories as keys and their corresponding weights as values.</param>
        /// <param name="random">An optional <see cref="Random"/> instance to use for the selection; if null, a new instance is created.</param>
        /// <returns>The selected <see cref="YOLO.Enums.Category"/>, or <c>null</c> if the weights dictionary is null or empty.</returns>
        public static YOLO.Enums.Category? Category(this Dictionary<YOLO.Enums.Category, double>? weights, Random? random = null)
        {
            if (weights == null || weights.Count == 0)
            {
                return null;
            }

            random ??= new Random();

            double sum = 0;
            foreach (double weight in weights.Values)
            {
                sum += weight;
            }

            double value = sum * (System.Convert.ToDouble(random.Next(1, 100)) / 100.0);

            double current = 0;
            foreach (KeyValuePair<YOLO.Enums.Category, double> keyValuePair in weights)
            {
                current += keyValuePair.Value;
                if (current >= value)
                {
                    return keyValuePair.Key;
                }
            }

            return null;
        }

        /// <summary>
        /// Determines the YOLO category based on the provided conversion options.
        /// </summary>
        /// <param name="yOLOConversionOptions">The YOLO conversion options used to derive the category.</param>
        /// <param name="random">An optional random number generator for selection logic.</param>
        /// <returns>The determined <see cref="YOLO.Enums.Category"/>, or <c>null</c> if it cannot be determined.</returns>
        public static YOLO.Enums.Category? Category(this YOLOConversionOptions? yOLOConversionOptions, Random? random = null)
        {
            return Category(yOLOConversionOptions?.Weights, random);
        }
    }
}