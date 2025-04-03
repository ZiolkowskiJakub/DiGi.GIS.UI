using DiGi.Core.Classes;
using DiGi.Core.IO.Table.Classes;
using DiGi.Geometry.Planar.Classes;
using DiGi.GIS.Classes;
using DiGi.GIS.Emgu.CV.Classes;
using DiGi.GIS.Enums;
using System.IO;

namespace DiGi.GIS.UI
{
    public static partial class Convert
    {
        public static Table ToDiGi_Table(this GISModelFile gISModelFile, IEnumerable<string> references = null, TableConversionOptions tableConversionOptions = null)
        {
            if (gISModelFile == null)
            {
                return null;
            }

            string path = gISModelFile.Path;
            if (string.IsNullOrWhiteSpace(path))
            {
                return null;
            }

            if(references == null)
            {
                references = gISModelFile.Value.GetObjects<Building2D>()?.ConvertAll(x => x.Reference);
            }

            if(references == null)
            {
                return null;
            }

            if (tableConversionOptions == null)
            {
                tableConversionOptions = new TableConversionOptions();
            }

            Table result = new Table();

            Dictionary<string, List<Row>> dictionary_Rows = new Dictionary<string, List<Row>>();
            Dictionary<string, Column> dictionary_Column = new Dictionary<string, Column>();

            Func<string, int> updateColumn = new Func<string, int>(x =>
            {
                if (string.IsNullOrWhiteSpace(x))
                {
                    return -1;
                }

                if (!dictionary_Column.TryGetValue(x, out Column column) || column == null)
                {
                    int index = 0;

                    if (dictionary_Column.Count > 0)
                    {
                        index = dictionary_Column.Values.ToList().ConvertAll(x => x.Index).Max();
                        index++;
                    }

                    column = new Column(index, x, typeof(string));
                    dictionary_Column.Add(x, column);
                }

                return column.Index;
            });

            if (tableConversionOptions.IncludeModel)
            {
                GISModel gISModel = gISModelFile.Value;
                if (gISModel != null)
                {
                    int index_Reference = updateColumn.Invoke("Reference");
                    int index_BuildingGeneralFunction = updateColumn.Invoke("Building General Function");
                    int index_BuildingPhase = updateColumn.Invoke("Building Phase");
                    int index_Storeys = updateColumn.Invoke("Storeys");
                    int index_Area = updateColumn.Invoke("Area");
                    int index_Location_X = updateColumn.Invoke("Location X");
                    int index_Location_Y = updateColumn.Invoke("Location Y");
                    int index_VoivodeshipName = updateColumn.Invoke("Voivodeship Name");
                    int index_CountyName = updateColumn.Invoke("County Name");
                    int index_MunicipalityName = updateColumn.Invoke("Municipality Name");
                    int index_SubdivisionName = updateColumn.Invoke("Subdivision Name");
                    int index_SubdivisionCalculatedOccupancy = updateColumn.Invoke("Subdivision Calculated Occupancy");
                    int index_SubdivisionCalculatedOccupancyArea = updateColumn.Invoke("Subdivision Calculated Occupancy Area");

                    foreach (string reference in references)
                    {
                        Building2D building2D = gISModel.GetObject<Building2D>(x => x?.Reference == reference);
                        if (building2D == null)
                        {
                            continue;
                        }

                        PolygonalFace2D polygonalFace2D = building2D.PolygonalFace2D;

                        BuildingGeneralFunction? buildingGeneralFunction = building2D.BuildingGeneralFunction;
                        BuildingPhase? buidlingPhase = building2D.BuildingPhase;
                        ushort storeys = building2D.Storeys;
                        double? area = polygonalFace2D?.GetArea();
                        Point2D location = polygonalFace2D?.GetInternalPoint();
                        string voivodeshipName = null;
                        string countyName = null;
                        string municipalityName = null;
                        string subdivisionName = null;
                        uint? subdivisionCalculatedOccupancy = null;
                        double? subdivisionCalculatedOccupancyArea = null;

                        List<AdministrativeAreal2D> administrativeAreal2Ds = gISModel.AdministrativeAreal2Ds<AdministrativeAreal2D>(building2D);
                        if (administrativeAreal2Ds != null)
                        {
                            List<AdministrativeDivision> administrativeDivisions = administrativeAreal2Ds.OfType<AdministrativeDivision>().ToList();
                            if (administrativeDivisions != null)
                            {
                                AdministrativeDivision administrativeDivision = null;

                                administrativeDivision = administrativeDivisions.Find(x => x.AdministrativeDivisionType == AdministrativeDivisionType.voivodeship);
                                if (administrativeDivision != null)
                                {
                                    voivodeshipName = administrativeDivision.Name;
                                }

                                administrativeDivision = administrativeDivisions.Find(x => x.AdministrativeDivisionType == AdministrativeDivisionType.county);
                                if (administrativeDivision != null)
                                {
                                    countyName = administrativeDivision.Name;
                                }

                                administrativeDivision = administrativeDivisions.Find(x => x.AdministrativeDivisionType == AdministrativeDivisionType.municipality);
                                if (administrativeDivision != null)
                                {
                                    municipalityName = administrativeDivision.Name;
                                }
                            }

                            AdministrativeSubdivision administrativeSubdivision = administrativeAreal2Ds.OfType<AdministrativeSubdivision>().FirstOrDefault();
                            if (administrativeSubdivision != null)
                            {
                                subdivisionName = administrativeSubdivision.Name;

                                if (gISModel.TryGetRelatedObjects<OccupancyCalculationResult, AdministrativeAreal2DOccupancyCalculationResultRelation>(administrativeSubdivision, out List<OccupancyCalculationResult> occupancyCalculationResults) && occupancyCalculationResults != null)
                                {
                                    OccupancyCalculationResult occupancyCalculationResult = occupancyCalculationResults.FirstOrDefault();
                                    if (occupancyCalculationResult != null)
                                    {
                                        subdivisionCalculatedOccupancy = occupancyCalculationResult.Occupancy;
                                        subdivisionCalculatedOccupancyArea = occupancyCalculationResult.OccupancyArea;
                                    }
                                }

                            }
                        }

                        Row row = new Row(-1);
                        row.SetValue(index_Reference, reference);
                        row.SetValue(index_BuildingGeneralFunction, buildingGeneralFunction?.ToString());
                        row.SetValue(index_BuildingPhase, buidlingPhase?.ToString());
                        row.SetValue(index_Storeys, storeys.ToString());
                        row.SetValue(index_Area, area == null || !area.HasValue ? null : Core.Query.Round(area.Value, Core.Constans.Tolerance.MacroDistance).ToString());
                        row.SetValue(index_Location_X, location == null ? null : Core.Query.Round(location.X, Core.Constans.Tolerance.MacroDistance).ToString());
                        row.SetValue(index_Location_Y, location == null ? null : Core.Query.Round(location.Y, Core.Constans.Tolerance.MacroDistance).ToString());
                        row.SetValue(index_VoivodeshipName, voivodeshipName);
                        row.SetValue(index_CountyName, countyName);
                        row.SetValue(index_MunicipalityName, municipalityName);
                        row.SetValue(index_SubdivisionName, subdivisionName);
                        row.SetValue(index_SubdivisionCalculatedOccupancy, subdivisionCalculatedOccupancy);
                        row.SetValue(index_SubdivisionCalculatedOccupancyArea, subdivisionCalculatedOccupancyArea);

                        if (!dictionary_Rows.TryGetValue(reference, out List<Row> rows) || rows == null)
                        {
                            rows = new List<Row>();
                            dictionary_Rows[reference] = rows;
                        }

                        rows.Add(row);
                    }
                }
            }

            if (tableConversionOptions.IncludeOrtoDatasComparison && tableConversionOptions.Years != null)
            {
                string directory = System.IO.Path.GetDirectoryName(path);

                Range<int> range_Years = tableConversionOptions.Years;

                Dictionary<string, OrtoDatasComparison> dictionary_OrtoDatasComparison = Emgu.CV.Query.OrtoDatasComparisonDictionary(directory, references);
                if (dictionary_OrtoDatasComparison != null)
                {
                    foreach (KeyValuePair<string, OrtoDatasComparison> keyValuePair in dictionary_OrtoDatasComparison)
                    {
                        string reference = keyValuePair.Key;
                        OrtoDatasComparison ortoDatasComparison = keyValuePair.Value;

                        if (!dictionary_Rows.TryGetValue(reference, out List<Row> rows) || rows == null)
                        {
                            rows = new List<Row>();
                            dictionary_Rows[reference] = rows;
                        }

                        int index_Year = updateColumn.Invoke("Year");

                        for (int i = range_Years.Min; i <= range_Years.Max; i++)
                        {
                            DateTime dateTime_1 = new DateTime(i, 1, 1);

                            Row row = null;
                            if (rows.Count == 0)
                            {
                                row = new Row(-1);
                                row.SetValue(index_Year, i);
                                rows.Add(row);
                            }
                            else
                            {
                                row = rows.Find(x => x.TryGetValue(index_Year, out int year) && year == i);
                                if (row == null)
                                {
                                    row = new Row(rows[0]);
                                    row.SetValue(index_Year, i);
                                    rows.Add(row);
                                }
                            }

                            OrtoDataComparison ortoDataComparison = ortoDatasComparison.GetOrtoDataComparison(dateTime_1);
                            if (ortoDataComparison == null)
                            {
                                continue;
                            }

                            foreach (OrtoImageComparisonGroup ortoImageComparisonGroup in ortoDataComparison.OrtoImageComparisonGroups)
                            {
                                string name = ortoImageComparisonGroup.Name;

                                for (int j = range_Years.Min; j <= range_Years.Max; j++)
                                {
                                    DateTime dateTime_2 = new DateTime(j, 1, 1);

                                    OrtoImageComparison ortoImageComparison = ortoImageComparisonGroup.GetOrtoImageComparison(dateTime_2);
                                    if (ortoImageComparison == null)
                                    {
                                        continue;
                                    }

                                    string prefix = string.Format("{0} {1}", name, j);

                                    int index_AverageColorSimilarity = updateColumn.Invoke(string.Format("{0} {1}", prefix, "Average Color Similarity"));
                                    int index_ColorDistributionShift = updateColumn.Invoke(string.Format("{0} {1}", prefix, "Color Distribution Shift"));
                                    int index_GrayHistogramsFactor = updateColumn.Invoke(string.Format("{0} {1}", prefix, "Gray Histograms Factor"));
                                    int index_HammingDistance = updateColumn.Invoke(string.Format("{0} {1}", prefix, "Hamming Distance"));
                                    int index_HistogramCorrelation = updateColumn.Invoke(string.Format("{0} {1}", prefix, "Histogram Correlation"));
                                    int index_ShapeComparisonFactor = updateColumn.Invoke(string.Format("{0} {1}", prefix, "Shape Comparison Factor"));
                                    int index_StructuralSimilarityIndex_AbsoluteDifference = updateColumn.Invoke(string.Format("{0} {1}", prefix, "Structural Similarity Index (Absolute Difference)"));
                                    int index_StructuralSimilarityIndex_MatchTemplate = updateColumn.Invoke(string.Format("{0} {1}", prefix, "Structural Similarity Index (Match Template)"));
                                    int index_MeanLaplacianFactor = updateColumn.Invoke(string.Format("{0} {1}", prefix, "Mean Laplacian Factor"));
                                    int index_StandardDeviationLaplacianFactor = updateColumn.Invoke(string.Format("{0} {1}", prefix, "Standard Deviation Laplacian Factor"));
                                    int index_OpticalFlowAverageMagnitude = updateColumn.Invoke(string.Format("{0} {1}", prefix, "Optical Flow Average Magnitude"));
                                    int index_ORBFeatureMatchingFactor = updateColumn.Invoke(string.Format("{0} {1}", prefix, "ORB Feature Matching Factor"));


                                    row.SetValue(index_AverageColorSimilarity, double.IsNaN(ortoImageComparison.AverageColorSimilarity) ? 0 : ortoImageComparison.AverageColorSimilarity);
                                    row.SetValue(index_ColorDistributionShift, double.IsNaN(ortoImageComparison.ColorDistributionShift) ? 0 : ortoImageComparison.ColorDistributionShift);
                                    row.SetValue(index_GrayHistogramsFactor, double.IsNaN(ortoImageComparison.GrayHistogramsFactor) ? 0 : ortoImageComparison.GrayHistogramsFactor);
                                    row.SetValue(index_HammingDistance, ortoImageComparison.HammingDistance);
                                    row.SetValue(index_HistogramCorrelation, double.IsNaN(ortoImageComparison.HistogramCorrelation) ? 0 : ortoImageComparison.HistogramCorrelation);
                                    row.SetValue(index_ShapeComparisonFactor, double.IsNaN(ortoImageComparison.ShapeComparisonFactor) ? 0 : ortoImageComparison.ShapeComparisonFactor);
                                    row.SetValue(index_StructuralSimilarityIndex_AbsoluteDifference, double.IsNaN(ortoImageComparison.StructuralSimilarityIndex_AbsoluteDifference) ? 0 : ortoImageComparison.StructuralSimilarityIndex_AbsoluteDifference);
                                    row.SetValue(index_StructuralSimilarityIndex_MatchTemplate, double.IsNaN(ortoImageComparison.StructuralSimilarityIndex_MatchTemplate) ? 0 : ortoImageComparison.StructuralSimilarityIndex_MatchTemplate);
                                    row.SetValue(index_MeanLaplacianFactor, double.IsNaN(ortoImageComparison.MeanLaplacianFactor) ? 0 : ortoImageComparison.MeanLaplacianFactor);
                                    row.SetValue(index_StandardDeviationLaplacianFactor, double.IsNaN(ortoImageComparison.StandardDeviationLaplacianFactor) ? 0 : ortoImageComparison.StandardDeviationLaplacianFactor);
                                    row.SetValue(index_OpticalFlowAverageMagnitude, double.IsNaN(ortoImageComparison.OpticalFlowAverageMagnitude) ? 0 : ortoImageComparison.OpticalFlowAverageMagnitude);
                                    row.SetValue(index_ORBFeatureMatchingFactor, double.IsNaN(ortoImageComparison.ORBFeatureMatchingFactor) ? 0 : ortoImageComparison.ORBFeatureMatchingFactor);
                                }
                            }
                        }
                    }
                }
            }

            if (tableConversionOptions.IncludeStatistical && Directory.Exists(tableConversionOptions.StatisticalDirectory))
            {
                GISModel gISModel = gISModelFile.Value;
                if (gISModel != null)
                {
                    List<Tuple<StatisticalUnit, List<Building2D>>> tuples = new List<Tuple<StatisticalUnit, List<Building2D>>>();

                    foreach (string reference in references)
                    {
                        Building2D building2D = gISModel.GetObject<Building2D>(x => x?.Reference == reference);
                        if (building2D == null)
                        {
                            continue;
                        }

                        List<AdministrativeSubdivision> administrativeSubdivisions = gISModel.AdministrativeAreal2Ds<AdministrativeSubdivision>(building2D);
                        if(administrativeSubdivisions == null || administrativeSubdivisions.Count == 0)
                        {
                            continue;
                        }

                        if(administrativeSubdivisions.Count != 1)
                        {
                            List<Tuple<double, AdministrativeSubdivision>> tuples_AdministrativeSubdivision = new List<Tuple<double, AdministrativeSubdivision>>();
                            foreach (AdministrativeSubdivision administrativeSubdivision_Temp in administrativeSubdivisions)
                            {
                                AdministrativeAreal2DGeometryCalculationResult administrativeAreal2DGeometryCalculationResult = gISModel.GetRelatedObject<AdministrativeAreal2DGeometryCalculationResult>(administrativeSubdivision_Temp);
                                double area = administrativeAreal2DGeometryCalculationResult != null ? administrativeAreal2DGeometryCalculationResult.Area : administrativeSubdivision_Temp.PolygonalFace2D.GetArea();
                                tuples_AdministrativeSubdivision.Add(new Tuple<double, AdministrativeSubdivision>(area, administrativeSubdivision_Temp));
                            }

                            tuples_AdministrativeSubdivision.Sort((x, y) => x.Item1.CompareTo(y.Item1));
                            administrativeSubdivisions = tuples_AdministrativeSubdivision.ConvertAll(x => x.Item2);
                        }

                        IEnumerable<StatisticalUnit> statisticalUnits = gISModel.GetRelatedObject<AdministrativeAreal2DStatisticalUnitsCalculcationResult>(administrativeSubdivisions[0])?.StatisticalUnits;
                        if(statisticalUnits != null)
                        {
                            foreach(StatisticalUnit statisticalUnit in statisticalUnits)
                            {
                                Tuple<StatisticalUnit, List<Building2D>> tuple = tuples.Find(x => x.Item1.Code == statisticalUnit.Code);
                                if(tuple == null)
                                {
                                    tuple = new Tuple<StatisticalUnit, List<Building2D>>(statisticalUnit, new List<Building2D>());
                                    tuples.Add(tuple);
                                }

                                tuple.Item2.Add(building2D);
                            }
                        }
                    }

                    if(tuples != null && tuples.Count != 0)
                    {
                        string[] paths_StatisticalDataCollectionFile = Directory.GetFiles(tableConversionOptions.StatisticalDirectory, string.Format("*.{0}", Constans.FileExtension.StatisticalDataCollectionFile));
                        if (paths_StatisticalDataCollectionFile != null && paths_StatisticalDataCollectionFile.Length != 0)
                        {
                            foreach (string path_StatisticalDataCollectionFile in paths_StatisticalDataCollectionFile)
                            {
                                using (StatisticalDataCollectionFile statisticalDataCollectionFile = new StatisticalDataCollectionFile(path_StatisticalDataCollectionFile))
                                {
                                    Dictionary<string, StatisticalDataCollection> dictionary = GIS.Query.StatisticalDataCollectionDictionary(statisticalDataCollectionFile, tuples.ConvertAll(x => x.Item1.Code));
                                    if (dictionary != null)
                                    {
                                        foreach (KeyValuePair<string, StatisticalDataCollection> keyValuePair in dictionary)
                                        {
                                            Tuple<StatisticalUnit, List<Building2D>> tuple = tuples.Find(x => x.Item1.Code == keyValuePair.Key);
                                            if(tuple == null)
                                            {
                                                continue;
                                            }

                                            StatisticalDataCollection statisticalDataCollection = keyValuePair.Value;



                                            foreach (Building2D building2D in tuple.Item2)
                                            {
                                                string reference = building2D.Reference;

                                                if (!dictionary_Rows.TryGetValue(reference, out List<Row> rows) || rows == null)
                                                {
                                                    rows = new List<Row>();
                                                    dictionary_Rows[reference] = rows;
                                                }

                                                if (rows.Count == 0)
                                                {
                                                    Row row = new Row(-1);
                                                    rows.Add(row);
                                                }

                                                foreach (Row row in rows)
                                                {

                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }
    }
}
