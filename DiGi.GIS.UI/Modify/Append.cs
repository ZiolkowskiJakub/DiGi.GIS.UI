using DiGi.GIS.Classes;
using DiGi.YOLO.Classes;
using System.Collections.Generic;

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

            List<Building2DYearBuiltPredictions>? building2DYearBuiltPredictions = GIS.YOLO.Create.Building2DYearBuiltPredictions(boundingBoxResultFile);
            if (building2DYearBuiltPredictions == null || building2DYearBuiltPredictions.Count == 0)
            {
                return false;
            }

            foreach (Building2DYearBuiltPredictions item in building2DYearBuiltPredictions)
            {
                building2DYearBuiltPredictionsFile.AddValue(item);
            }

            return true;
        }
    }
}