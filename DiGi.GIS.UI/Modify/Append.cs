using DiGi.GIS.Classes;
using DiGi.YOLO.Classes;

namespace DiGi.GIS.UI
{
    public static partial class Modify
    {
        /// <summary>
        /// Appends the data from a bounding box result file to the specified building 2D year built predictions file.
        /// </summary>
        /// <param name="building2DYearBuiltPredictionsFile">The <see cref="Building2DYearBuiltPredictionsFile"/> instance to which the results will be appended.</param>
        /// <param name="boundingBoxResultFile">The <see cref="BoundingBoxResultFile"/> containing the bounding box results to append.</param>
        /// <returns><c>true</c> if the data was successfully appended; otherwise, <c>false</c>.</returns>
        public static bool Append(this Building2DYearBuiltPredictionsFile building2DYearBuiltPredictionsFile, BoundingBoxResultFile boundingBoxResultFile)
        {
            if (boundingBoxResultFile == null || building2DYearBuiltPredictionsFile == null)
            {
                return false;
            }

            Dictionary<string, List<YearBuiltPrediction>> dictionary = [];
            foreach (BoundingBoxResult boundingBoxResult in boundingBoxResultFile)
            {
                string? name = boundingBoxResult?.Name;
                if (name is null)
                {
                    continue;
                }

                string[] values = name.Split("_");
                if (values == null || values.Length < 2)
                {
                    continue;
                }

                if (!ushort.TryParse(values[1].Trim(), out ushort year))
                {
                    continue;
                }

                string reference = values[0].Trim();

                if (!dictionary.TryGetValue(reference, out List<YearBuiltPrediction>? yearBuiltPredictions) || yearBuiltPredictions == null)
                {
                    yearBuiltPredictions = [];
                    dictionary[reference] = yearBuiltPredictions;
                }

                YearBuiltPrediction YearBuiltPrediction = new(year, new Geometry.Planar.Classes.BoundingBox2D(boundingBoxResult!.X, boundingBoxResult.Y, boundingBoxResult.Width, boundingBoxResult.Height), boundingBoxResult.Confidence);
                yearBuiltPredictions.Add(YearBuiltPrediction);
            }

            bool result = false;
            foreach (KeyValuePair<string, List<YearBuiltPrediction>> keyValuePair in dictionary)
            {
                building2DYearBuiltPredictionsFile.AddValue(new Building2DYearBuiltPredictions(keyValuePair.Key, keyValuePair.Value));
                result = true;
            }

            return result;
        }
    }
}