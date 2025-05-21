using DiGi.BDL.Enums;
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
        public static Table ToDiGi_Table(this GISModelFile gISModelFile, IEnumerable<string> references = null, ComparisonTableConversionOptions comparisonTableConversionOptions = null)
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

            if (references == null)
            {
                references = gISModelFile.Value.GetObjects<Building2D>()?.ConvertAll(x => x.Reference);
            }

            if (references == null)
            {
                return null;
            }

            if (comparisonTableConversionOptions == null)
            {
                comparisonTableConversionOptions = new ComparisonTableConversionOptions();
            }

            Table result = new Table();

            Dictionary<string, List<Row>> dictionary_Rows = new Dictionary<string, List<Row>>();

            Func<string, int> updateColumn = new Func<string, int>(x =>
            {
                if (string.IsNullOrWhiteSpace(x))
                {
                    return -1;
                }

                if(result.TryGetColumn(x, out Column column))
                {
                    return column.Index;
                }

                column = result.AddColumn(x, typeof(string));
                if(column == null)
                {
                    return -1;
                }

                return column.Index;
            });

            string columnName_Year = "Year";

            Dictionary<string, int> dictionary_YearBuilt = null;
            if (comparisonTableConversionOptions.IncludeYearBuilt)
            {
                string directory = System.IO.Path.GetDirectoryName(path);

                Dictionary<string, YearBuiltData> dictionary_YearBuiltData = GIS.Query.YearBuiltDataDictionary<YearBuiltData>(directory, references);
                if (dictionary_YearBuiltData != null)
                {
                    dictionary_YearBuilt = new Dictionary<string, int>();

                    foreach (KeyValuePair<string, YearBuiltData> keyValuePair in dictionary_YearBuiltData)
                    {
                        dictionary_YearBuilt[keyValuePair.Key] = keyValuePair.Value.Year;
                    }
                }

                if (comparisonTableConversionOptions.YearBuiltOnly)
                {
                    references = references.ToList().FindAll(dictionary_YearBuilt.ContainsKey);
                    if (references.Count() == 0)
                    {
                        return result;
                    }
                }
            }

            GISModel gISModel = gISModelFile.Value;
            List<Building2D> building2Ds = gISModel?.GetObjects<Building2D>();
            if(building2Ds != null)
            {
                for(int i = building2Ds.Count - 1; i >= 0; i--)
                {
                    if (!references.Contains(building2Ds[i]?.Reference))
                    {
                        building2Ds.RemoveAt(i);
                    }
                }
            }

            Dictionary<GuidReference, List<AdministrativeAreal2D>> dictionary = GIS.Query.AdministrativeAreal2DsDictionary<AdministrativeAreal2D>(gISModel, building2Ds);

            if (comparisonTableConversionOptions.IncludeModel)
            {
                if(building2Ds != null && building2Ds.Count != 0)
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

                    foreach(Building2D building2D in building2Ds)
                    {
                        string reference = building2D?.Reference;
                        if(string.IsNullOrWhiteSpace(reference))
                        {
                            continue;
                        }

                        GuidReference guidReference = new GuidReference(building2D);

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

                        if(dictionary.TryGetValue(guidReference, out List<AdministrativeAreal2D> administrativeAreal2Ds) && administrativeAreal2Ds != null)
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

            if (comparisonTableConversionOptions.IncludeStatistical && Directory.Exists(comparisonTableConversionOptions.StatisticalDirectory))
            {
                if (building2Ds != null)
                {
                    List<Tuple<StatisticalUnit, List<Building2D>>> tuples = new List<Tuple<StatisticalUnit, List<Building2D>>>();

                    foreach (Building2D building2D in building2Ds)
                    {
                        if (building2D == null)
                        {
                            continue;
                        }

                        GuidReference guidReference = new GuidReference(building2D);

                        if (!dictionary.TryGetValue(guidReference, out List<AdministrativeAreal2D> administrativeAreal2Ds) || administrativeAreal2Ds == null)
                        {
                            continue;
                        }

                        List<AdministrativeSubdivision> administrativeSubdivisions = administrativeAreal2Ds.OfType<AdministrativeSubdivision>().ToList();
                        if (administrativeSubdivisions == null || administrativeSubdivisions.Count == 0)
                        {
                            continue;
                        }

                        if (administrativeSubdivisions.Count != 1)
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
                        if (statisticalUnits != null)
                        {
                            foreach (StatisticalUnit statisticalUnit in statisticalUnits)
                            {
                                Tuple<StatisticalUnit, List<Building2D>> tuple = tuples.Find(x => x.Item1.Code == statisticalUnit.Code);
                                if (tuple == null)
                                {
                                    tuple = new Tuple<StatisticalUnit, List<Building2D>>(statisticalUnit, new List<Building2D>());
                                    tuples.Add(tuple);
                                }

                                tuple.Item2.Add(building2D);
                            }
                        }
                    }

                    if (tuples != null && tuples.Count != 0)
                    {
                        Range<int> range_Years = comparisonTableConversionOptions?.Years;

                        string[] paths_StatisticalDataCollectionFile = Directory.GetFiles(comparisonTableConversionOptions.StatisticalDirectory, string.Format("*.{0}", Constans.FileExtension.StatisticalDataCollectionFile));
                        if (paths_StatisticalDataCollectionFile != null && paths_StatisticalDataCollectionFile.Length != 0)
                        {
                            foreach (string path_StatisticalDataCollectionFile in paths_StatisticalDataCollectionFile)
                            {
                                using (StatisticalDataCollectionFile statisticalDataCollectionFile = new StatisticalDataCollectionFile(path_StatisticalDataCollectionFile))
                                {
                                    Dictionary<string, StatisticalDataCollection> dictionary_StatisticalDataCollection = GIS.Query.StatisticalDataCollectionDictionary(statisticalDataCollectionFile, tuples.ConvertAll(x => x.Item1.Code));
                                    if (dictionary != null)
                                    {
                                        foreach (KeyValuePair<string, StatisticalDataCollection> keyValuePair in dictionary_StatisticalDataCollection)
                                        {
                                            Tuple<StatisticalUnit, List<Building2D>> tuple = tuples.Find(x => x.Item1.Code == keyValuePair.Key);
                                            if (tuple == null)
                                            {
                                                continue;
                                            }

                                            StatisticalDataCollection statisticalDataCollection = keyValuePair.Value;

                                            Variable[] variables =
                                            {
                                                Variable.population_thousand_persons
                                            };

                                            if (range_Years != null)
                                            {
                                                foreach (Variable variable in variables)
                                                {
                                                    string description = Core.Query.Description(variable);

                                                    StatisticalYearlyDoubleData statisticalYearlyDoubleData = statisticalDataCollection[description] as StatisticalYearlyDoubleData;
                                                    if (statisticalYearlyDoubleData != null)
                                                    {
                                                        string name = Query.ColumnName(variable);

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
                                                                for (int i = range_Years.Min; i <= range_Years.Max; i++)
                                                                {
                                                                    short year = System.Convert.ToInt16(i);

                                                                    double? value = null;
                                                                    if (statisticalYearlyDoubleData.TryGetValue(year, out double value_Temp))
                                                                    {
                                                                        value = value_Temp;
                                                                        if (variable == Variable.population_thousand_persons)
                                                                        {
                                                                            value *= 1000;
                                                                        }
                                                                    }

                                                                    int index_Year = updateColumn.Invoke(string.Format("{0} {1}", name, year));
                                                                    row.SetValue(index_Year, value);
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
                        }
                    }
                }
            }

            if (comparisonTableConversionOptions.IncludeOrtoDatasComparison && comparisonTableConversionOptions.Years != null)
            {
                string directory = System.IO.Path.GetDirectoryName(path);

                Range<int> range_Years = comparisonTableConversionOptions.Years;

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

                        int index_Year = updateColumn.Invoke(columnName_Year);

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
                                    Row row_Template = rows[0];
                                    if(row_Template != null)
                                    {
                                        int count_Template = rows[0].Indexes.Count;
                                        row = new Row(row_Template);
                                        row.SetValue(index_Year, i);

                                        int count = row.Indexes.Count;
                                        if(count_Template != count)
                                        {
                                            rows.RemoveAt(0);
                                        }

                                        rows.Add(row);
                                    }
                                }
                            }

                            for (int j = range_Years.Min; j <= range_Years.Max; j++)
                            {
                                if(j == i)
                                {
                                    continue;
                                }

                                DateTime dateTime_2 = new DateTime(j, 1, 1);

                                Dictionary<string, OrtoImageComparison> dictionary_OrtoImageComparison = Emgu.CV.Query.OrtoImageComparisonDictionary(ortoDatasComparison, dateTime_1, dateTime_2);
                                if(dictionary_OrtoImageComparison == null)
                                {
                                    continue;
                                }

                                foreach(KeyValuePair<string, OrtoImageComparison> keyValuePair_OrtoImageComparison in dictionary_OrtoImageComparison)
                                {
                                    string prefix = string.Format("{0} {1} {2}", keyValuePair_OrtoImageComparison.Key, dateTime_1.Year, dateTime_2.Year);

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

                                    OrtoImageComparison ortoImageComparison = keyValuePair_OrtoImageComparison.Value;

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

            if (dictionary_YearBuilt != null && dictionary_YearBuilt.Count != 0)
            {
                foreach (KeyValuePair<string, int> keyValuePair in dictionary_YearBuilt)
                {
                    if (!dictionary_Rows.TryGetValue(keyValuePair.Key, out List<Row> rows) || rows == null || rows.Count == 0)
                    {
                        continue;
                    }

                    if(rows.Count == 1 || !result.TryGetColumn(columnName_Year, out Column column) || column == null)
                    {
                        int index_YearBuilt = updateColumn.Invoke("Year Built");
                        rows[0].SetValue(index_YearBuilt, keyValuePair.Value);
                    }
                    else
                    {
                        int index_Built = updateColumn.Invoke("Built");

                        foreach (Row row in rows)
                        {
                            if(row.TryGetValue(column.Index, out int year))
                            {
                                row.SetValue(index_Built, keyValuePair.Value <= year);
                            }
                        }
                    }
                }
            }

            foreach(KeyValuePair<string, List<Row>> keyValuePair in dictionary_Rows)
            {
                if(keyValuePair.Value == null)
                {
                    continue;
                }

                foreach(Row row in keyValuePair.Value)
                {
                    result.AddRow(row);
                }
            }

            return result;
        }

        public static Table ToDiGi_Table(this GISModelFile gISModelFile, IEnumerable<string> references = null, PredictionTableConversionOptions predictionTableConversionOptions = null)
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

            if (references == null)
            {
                references = gISModelFile.Value.GetObjects<Building2D>()?.ConvertAll(x => x.Reference);
            }

            if (references == null)
            {
                return null;
            }

            if (predictionTableConversionOptions == null)
            {
                predictionTableConversionOptions = new PredictionTableConversionOptions();
            }

            IndexDataFile indexDataFile = null;
            if(!string.IsNullOrWhiteSpace(predictionTableConversionOptions.AdministrativeAreal2DsIndexDataFilePath) && File.Exists(predictionTableConversionOptions.AdministrativeAreal2DsIndexDataFilePath))
            {
                indexDataFile = new IndexDataFile();
                indexDataFile.Read(predictionTableConversionOptions.AdministrativeAreal2DsIndexDataFilePath);
            }

            Table result = new Table();

            Dictionary<string, Row> dictionary_Row = new Dictionary<string, Row>();

            Func<string, Type, int> updateColumn = new Func<string, Type,int>((name, type) =>
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    return -1;
                }

                if (result.TryGetColumn(name, out Column column))
                {
                    return column.Index;
                }

                column = result.AddColumn(name, type);
                if (column == null)
                {
                    return -1;
                }

                return column.Index;
            });

            Dictionary<string, int> dictionary_YearBuilt = null;
            if (predictionTableConversionOptions.IncludeYearBuilt)
            {
                string directory = System.IO.Path.GetDirectoryName(path);

                Dictionary<string, YearBuiltData> dictionary_YearBuiltData = GIS.Query.YearBuiltDataDictionary<YearBuiltData>(directory, references);
                if (dictionary_YearBuiltData != null)
                {
                    dictionary_YearBuilt = new Dictionary<string, int>();

                    foreach (KeyValuePair<string, YearBuiltData> keyValuePair in dictionary_YearBuiltData)
                    {
                        dictionary_YearBuilt[keyValuePair.Key] = keyValuePair.Value.Year;
                    }
                }

                if (predictionTableConversionOptions.YearBuiltOnly)
                {
                    references = references.ToList().FindAll(dictionary_YearBuilt.ContainsKey);
                    if (references.Count() == 0)
                    {
                        return result;
                    }
                }
            }

            GISModel gISModel = gISModelFile.Value;
            List<Building2D> building2Ds = gISModel?.GetObjects<Building2D>();
            if (building2Ds != null)
            {
                for (int i = building2Ds.Count - 1; i >= 0; i--)
                {
                    if (!references.Contains(building2Ds[i]?.Reference))
                    {
                        building2Ds.RemoveAt(i);
                    }
                }
            }

            Dictionary<string, Building2DYearBuiltPredictions> dictionary_Building2DYearBuiltPredictions = null;
            if (predictionTableConversionOptions.IncludeYearBuiltPredictions)
            {
                string directory = System.IO.Path.GetDirectoryName(path);

                dictionary_Building2DYearBuiltPredictions = GIS.Query.Building2DYearBuiltPredictionsDictionary(directory, building2Ds.ConvertAll(x => x.Reference));
            }

            if (building2Ds != null && dictionary_Building2DYearBuiltPredictions != null)
            {
                for (int i = building2Ds.Count - 1; i >= 0; i--)
                {
                    if (!dictionary_Building2DYearBuiltPredictions.ContainsKey(building2Ds[i]?.Reference))
                    {
                        building2Ds.RemoveAt(i);
                    }
                }
            }

            Dictionary<GuidReference, List<AdministrativeAreal2D>> dictionary = GIS.Query.AdministrativeAreal2DsDictionary<AdministrativeAreal2D>(gISModel, building2Ds);

            if (predictionTableConversionOptions.IncludeModel)
            {
                if (building2Ds != null && building2Ds.Count != 0)
                {
                    int index_Reference = updateColumn.Invoke("Reference", typeof(string));
                    int index_BuildingGeneralFunction = updateColumn.Invoke("Building General Function", typeof(int));
                    int index_BuildingPhase = updateColumn.Invoke("Building Phase", typeof(int));
                    int index_Storeys = updateColumn.Invoke("Storeys", typeof(int));
                    int index_Area = updateColumn.Invoke("Area", typeof(double));
                    int index_Location_X = updateColumn.Invoke("Location X", typeof(double));
                    int index_Location_Y = updateColumn.Invoke("Location Y", typeof(double));
                    
                    int index_Voivodeship = updateColumn.Invoke("Voivodeship", typeof(string));
                    int index_County = updateColumn.Invoke("County", typeof(string));
                    int index_Municipality = updateColumn.Invoke("Municipality", typeof(string));
                    int index_Subdivision = updateColumn.Invoke("Subdivision", typeof(string));
                    
                    int index_SubdivisionCalculatedOccupancy = updateColumn.Invoke("Subdivision Calculated Occupancy", typeof(double));
                    int index_SubdivisionCalculatedOccupancyArea = updateColumn.Invoke("Subdivision Calculated Occupancy Area", typeof(double));

                    int index_BoundingBox_X = updateColumn.Invoke("BoundingBox X", typeof(double));
                    row.SetValue(index, yearBuiltPrediction.BoundingBox.GetCentroid().X);

                    int index_BoundingBox_Y = updateColumn.Invoke("BoundingBox Y", typeof(double));
                    row.SetValue(index, yearBuiltPrediction.BoundingBox.GetCentroid().Y);

                    int index_BoundingBox_Width = updateColumn.Invoke("BoundingBox Width", typeof(double));
                    row.SetValue(index, yearBuiltPrediction.BoundingBox.Width);

                    int index_BoundingBox_Height = updateColumn.Invoke("BoundingBox Height", typeof(double));
                    row.SetValue(index, yearBuiltPrediction.BoundingBox.Height);

                    foreach (Building2D building2D in building2Ds)
                    {
                        string reference = building2D?.Reference;
                        if (string.IsNullOrWhiteSpace(reference))
                        {
                            continue;
                        }

                        GuidReference guidReference = new GuidReference(building2D);

                        PolygonalFace2D polygonalFace2D = building2D.PolygonalFace2D;

                        BuildingGeneralFunction? buildingGeneralFunction = building2D.BuildingGeneralFunction;
                        BuildingPhase? buidlingPhase = building2D.BuildingPhase;
                        ushort storeys = building2D.Storeys;
                        double? area = polygonalFace2D?.GetArea();
                        Point2D location = polygonalFace2D?.GetInternalPoint();
                        string voivodeship = null;
                        string county = null;
                        string municipality = null;
                        string subdivision = null;
                        uint? subdivisionCalculatedOccupancy = null;
                        double? subdivisionCalculatedOccupancyArea = null;

                        BoundingBox2D boundingBox2D = polygonalFace2D?.GetBoundingBox();
                        double? boundingBox_X = boundingBox2D?.GetCentroid().X;
                        double? boundingBox_Y = boundingBox2D?.GetCentroid().Y;
                        double? boundingBox_Width = boundingBox2D?.Width;
                        double? boundingBox_Height = boundingBox2D?.Height;

                        if (dictionary.TryGetValue(guidReference, out List<AdministrativeAreal2D> administrativeAreal2Ds) && administrativeAreal2Ds != null)
                        {
                            List<AdministrativeDivision> administrativeDivisions = administrativeAreal2Ds.OfType<AdministrativeDivision>().ToList();
                            if (administrativeDivisions != null)
                            {
                                AdministrativeDivision administrativeDivision = null;

                                administrativeDivision = administrativeDivisions.Find(x => x.AdministrativeDivisionType == AdministrativeDivisionType.voivodeship);
                                if (administrativeDivision != null)
                                {
                                    if(indexDataFile != null && indexDataFile.TryGetIndex(administrativeDivision.Reference, out int index))
                                    {
                                        voivodeship = index.ToString();
                                    }
                                    else
                                    {
                                        voivodeship = administrativeDivision.Name;
                                    }  
                                }

                                administrativeDivision = administrativeDivisions.Find(x => x.AdministrativeDivisionType == AdministrativeDivisionType.county);
                                if (administrativeDivision != null)
                                {
                                    if (indexDataFile != null && indexDataFile.TryGetIndex(administrativeDivision.Reference, out int index))
                                    {
                                        county = index.ToString();
                                    }
                                    else
                                    {
                                        county = administrativeDivision.Name;
                                    }
                                }

                                administrativeDivision = administrativeDivisions.Find(x => x.AdministrativeDivisionType == AdministrativeDivisionType.municipality);
                                if (administrativeDivision != null)
                                {
                                    if (indexDataFile != null && indexDataFile.TryGetIndex(administrativeDivision.Reference, out int index))
                                    {
                                        municipality = index.ToString();
                                    }
                                    else
                                    {
                                        municipality = administrativeDivision.Name;
                                    }

                                }
                            }

                            AdministrativeSubdivision administrativeSubdivision = administrativeAreal2Ds.OfType<AdministrativeSubdivision>().FirstOrDefault();
                            if (administrativeSubdivision != null)
                            {
                                if (indexDataFile != null && indexDataFile.TryGetIndex(administrativeSubdivision.Reference, out int index))
                                {
                                    subdivision = index.ToString();
                                }
                                else
                                {
                                    subdivision = administrativeSubdivision.Name;
                                }


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
                        row.SetValue(index_BuildingGeneralFunction, buildingGeneralFunction == null || !buildingGeneralFunction.HasValue ? -1 : ((int)buildingGeneralFunction.Value).ToString());
                        row.SetValue(index_BuildingPhase, buidlingPhase == null || !buidlingPhase.HasValue ? -1 : ((int)buidlingPhase.Value).ToString());
                        row.SetValue(index_Storeys, storeys.ToString());
                        row.SetValue(index_Area, area);
                        row.SetValue(index_Location_X, location?.X);
                        row.SetValue(index_Location_Y, location?.Y);
                        row.SetValue(index_Voivodeship, voivodeship);
                        row.SetValue(index_County, county);
                        row.SetValue(index_Municipality, municipality);
                        row.SetValue(index_Subdivision, subdivision);
                        row.SetValue(index_SubdivisionCalculatedOccupancy, subdivisionCalculatedOccupancy);
                        row.SetValue(index_SubdivisionCalculatedOccupancyArea, subdivisionCalculatedOccupancyArea);

                        row.SetValue(index_BoundingBox_X, boundingBox_X);
                        row.SetValue(index_BoundingBox_Y, boundingBox_Y);
                        row.SetValue(index_BoundingBox_Width, boundingBox_Width);
                        row.SetValue(index_BoundingBox_Height, boundingBox_Height);

                        dictionary_Row[reference] = row;
                    }
                }
            }

            if (predictionTableConversionOptions.IncludeStatistical && Directory.Exists(predictionTableConversionOptions.StatisticalDirectory))
            {
                if (building2Ds != null)
                {
                    List<Tuple<StatisticalUnit, List<Building2D>>> tuples = new List<Tuple<StatisticalUnit, List<Building2D>>>();

                    foreach (Building2D building2D in building2Ds)
                    {
                        if (building2D == null)
                        {
                            continue;
                        }

                        GuidReference guidReference = new GuidReference(building2D);

                        if (!dictionary.TryGetValue(guidReference, out List<AdministrativeAreal2D> administrativeAreal2Ds) || administrativeAreal2Ds == null)
                        {
                            continue;
                        }

                        List<AdministrativeSubdivision> administrativeSubdivisions = administrativeAreal2Ds.OfType<AdministrativeSubdivision>().ToList();
                        if (administrativeSubdivisions == null || administrativeSubdivisions.Count == 0)
                        {
                            continue;
                        }

                        if (administrativeSubdivisions.Count != 1)
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
                        if (statisticalUnits != null)
                        {
                            foreach (StatisticalUnit statisticalUnit in statisticalUnits)
                            {
                                Tuple<StatisticalUnit, List<Building2D>> tuple = tuples.Find(x => x.Item1.Code == statisticalUnit.Code);
                                if (tuple == null)
                                {
                                    tuple = new Tuple<StatisticalUnit, List<Building2D>>(statisticalUnit, new List<Building2D>());
                                    tuples.Add(tuple);
                                }

                                tuple.Item2.Add(building2D);
                            }
                        }
                    }

                    if (tuples != null && tuples.Count != 0)
                    {
                        Range<int> range_Years = predictionTableConversionOptions?.Years;

                        string[] paths_StatisticalDataCollectionFile = Directory.GetFiles(predictionTableConversionOptions.StatisticalDirectory, string.Format("*.{0}", Constans.FileExtension.StatisticalDataCollectionFile));
                        if (paths_StatisticalDataCollectionFile != null && paths_StatisticalDataCollectionFile.Length != 0)
                        {
                            foreach (string path_StatisticalDataCollectionFile in paths_StatisticalDataCollectionFile)
                            {
                                using (StatisticalDataCollectionFile statisticalDataCollectionFile = new StatisticalDataCollectionFile(path_StatisticalDataCollectionFile))
                                {
                                    Dictionary<string, StatisticalDataCollection> dictionary_StatisticalDataCollection = GIS.Query.StatisticalDataCollectionDictionary(statisticalDataCollectionFile, tuples.ConvertAll(x => x.Item1.Code));
                                    if (dictionary != null)
                                    {
                                        foreach (KeyValuePair<string, StatisticalDataCollection> keyValuePair in dictionary_StatisticalDataCollection)
                                        {
                                            Tuple<StatisticalUnit, List<Building2D>> tuple = tuples.Find(x => x.Item1.Code == keyValuePair.Key);
                                            if (tuple == null)
                                            {
                                                continue;
                                            }

                                            StatisticalDataCollection statisticalDataCollection = keyValuePair.Value;

                                            Variable[] variables =
                                            {
                                                Variable.population_thousand_persons
                                            };

                                            if (range_Years != null)
                                            {
                                                foreach (Variable variable in variables)
                                                {
                                                    string description = Core.Query.Description(variable);

                                                    StatisticalYearlyDoubleData statisticalYearlyDoubleData = statisticalDataCollection[description] as StatisticalYearlyDoubleData;
                                                    if (statisticalYearlyDoubleData != null)
                                                    {
                                                        string name = Query.ColumnName(variable);

                                                        foreach (Building2D building2D in tuple.Item2)
                                                        {
                                                            string reference = building2D.Reference;

                                                            if (!dictionary_Row.TryGetValue(reference, out Row row) || row == null)
                                                            {
                                                                row = new Row(-1);
                                                                dictionary_Row[reference] = row;
                                                            }

                                                            for (int i = range_Years.Min; i <= range_Years.Max; i++)
                                                            {
                                                                short year = System.Convert.ToInt16(i);

                                                                double? value = null;
                                                                if (statisticalYearlyDoubleData.TryGetValue(year, out double value_Temp))
                                                                {
                                                                    value = value_Temp;
                                                                    if (variable == Variable.population_thousand_persons)
                                                                    {
                                                                        value *= 1000;
                                                                    }
                                                                }

                                                                int index_Year = updateColumn.Invoke(string.Format("{0} {1}", name, year), typeof(double));
                                                                row.SetValue(index_Year, value == null || !value.HasValue ? null : value.Value);
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
                    }
                }
            }

            if (dictionary_Building2DYearBuiltPredictions != null && dictionary_Building2DYearBuiltPredictions.Count != 0)
            {
                Range<int> range_Years = predictionTableConversionOptions?.Years;
                if(range_Years != null)
                {
                    foreach (KeyValuePair<string, Row> keyValuePair in dictionary_Row)
                    {
                        if (!dictionary_Building2DYearBuiltPredictions.TryGetValue(keyValuePair.Key, out Building2DYearBuiltPredictions building2DYearBuiltPredictions) || building2DYearBuiltPredictions == null)
                        {
                            continue;
                        }

                        Row row = keyValuePair.Value;

                        for (int i = range_Years.Min; i <= range_Years.Max; i++)
                        {
                            YearBuiltPrediction yearBuiltPrediction = building2DYearBuiltPredictions.GetYearBuiltPrediction((ushort)i);
                            if (yearBuiltPrediction == null)
                            {
                                continue;
                            }

                            int index = -1;

                            index = updateColumn.Invoke(string.Format("Prediction Confidence {0}", i), typeof(double));
                            row.SetValue(index, yearBuiltPrediction.Confidence);

                            index = updateColumn.Invoke(string.Format("Prediction BoundingBox X {0}", i), typeof(double));
                            row.SetValue(index, yearBuiltPrediction.BoundingBox.GetCentroid().X);

                            index = updateColumn.Invoke(string.Format("Prediction BoundingBox Y {0}", i), typeof(double));
                            row.SetValue(index, yearBuiltPrediction.BoundingBox.GetCentroid().Y);

                            index = updateColumn.Invoke(string.Format("Prediction BoundingBox Width {0}", i), typeof(double));
                            row.SetValue(index, yearBuiltPrediction.BoundingBox.Width);

                            index = updateColumn.Invoke(string.Format("Prediction BoundingBox Height {0}", i), typeof(double));
                            row.SetValue(index, yearBuiltPrediction.BoundingBox.Height);
                        }
                    }
                }
            }

            if (dictionary_YearBuilt != null && dictionary_YearBuilt.Count != 0)
            {
                int index_YearBuilt = updateColumn.Invoke("Year Built", typeof(int));

                foreach (KeyValuePair<string, int> keyValuePair in dictionary_YearBuilt)
                {
                    if (!dictionary_Row.TryGetValue(keyValuePair.Key, out Row row) || row == null)
                    {
                        continue;
                    }

                    row.SetValue(index_YearBuilt, keyValuePair.Value);
                }
            }

            IEnumerable<Column> columns = result.Columns;
            foreach (KeyValuePair<string, Row> keyValuePair in dictionary_Row)
            {
                if (keyValuePair.Value == null)
                {
                    continue;
                }

                Row row = result.AddRow(keyValuePair.Value);
                if(row == null)
                {
                    continue;
                }

                if (row.Count != columns.Count())
                {
                    foreach(Column column in columns)
                    {
                        if(column.Type == typeof(string))
                        {
                            if (!row.TryGetValue(column.Index, out string value) || value == null)
                            {
                                row.SetValue(column.Index, "null");
                            }
                        }
                        else if (column.Type == typeof(double))
                        {
                            if (!row.TryGetValue(column.Index, out double value))
                            {
                                row.SetValue(column.Index, 0.0);
                            }
                        }
                        else if (column.Type == typeof(int))
                        {
                            if (!row.TryGetValue(column.Index, out int value))
                            {
                                row.SetValue(column.Index, -1);
                            }
                        }
                    }

                    result.AddRow(row, false);
                }
            }

            return result;
        }
    }
}
