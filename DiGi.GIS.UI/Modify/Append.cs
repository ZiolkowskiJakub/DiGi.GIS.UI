// TODO [YearBuiltPipelineFileSource]: Superseded by the automated Year Built Prediction pipeline (ZiolkowskiJakub/DiGi.YOLO#1). Do not remove without: 1. A successful prediction parity run on deployed PostgreSQL data (#1 §Verification), 2. Full database coverage established, 3. An explicit decision and developer opt-in.
using DiGi.GIS.Classes;
using DiGi.YOLO.Classes;
using System;
using System.Collections.Generic;

namespace DiGi.GIS.UI
{
    public static partial class Modify
    {
        /// <summary>
        /// [TEMPORARY] Appends the data from a bounding box result file to the specified building 2D year built predictions file.
        /// </summary>
        /// <param name="building2DYearBuiltPredictionsFile">The <see cref="Building2DYearBuiltPredictionsFile"/> instance to which the results will be appended.</param>
        /// <param name="boundingBoxResultFile">The <see cref="BoundingBoxResultFile"/> containing the bounding box results to append.</param>
        /// <returns><c>true</c> if the data was successfully appended; otherwise, <c>false</c>.</returns>
        // TODO [YearBuiltPipelineFileSource]: Superseded by DiGi.GIS.YOLO.Create.Building2DYearBuiltPredictions.
        [Obsolete("Superseded by DiGi.GIS.YOLO.Create.Building2DYearBuiltPredictions. TODO [YearBuiltPipelineFileSource]")]
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