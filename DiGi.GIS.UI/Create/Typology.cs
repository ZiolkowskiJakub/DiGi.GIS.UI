using DiGi.Geometry.Planar.Classes;
using DiGi.Geometry.Planar.Interfaces;
using DiGi.GIS.Classes;
using DiGi.GIS.Enums;
using System.IO;

namespace DiGi.GIS.UI
{
    public static partial class Create
    {
        public static Typology.Classes.Typology? Typology_Residential_1(GISModel? gISModel, string? description, double thinnesRatio = 0.95, double offset = 1)
        {
            if(gISModel is null)
            {
                return null;
            }

            List<Building2D>? building2Ds = gISModel.GetObjects<Building2D>();
            if (building2Ds is null || building2Ds.Count == 0)
            {
                return null;
            }

            List<AdministrativeDivision>? administrativeDivisions = Query.AdministrativeAreal2Ds<AdministrativeDivision>(gISModel, building2Ds);

            AdministrativeDivision? administrativeDivision = null;
            if (administrativeDivisions != null && administrativeDivisions.Count != 0)
            {
                administrativeDivisions.Sort((x, y) => ((int)y.AdministrativeDivisionType).CompareTo((int)x.AdministrativeDivisionType));
                administrativeDivision = administrativeDivisions[0];
            }
            else
            {
                administrativeDivision = gISModel.GetObjects<AdministrativeDivision>(x => x?.AdministrativeDivisionType == AdministrativeDivisionType.county)?.FirstOrDefault();
            }

            Dictionary<Core.Classes.GuidReference, List<AdministrativeSubdivision>?>? dictionary = GIS.Query.AdministrativeAreal2DsDictionary<AdministrativeSubdivision>(gISModel, building2Ds);
            if (dictionary is null || dictionary.Count == 0)
            {
                return null;
            }

            Typology.Classes.Typology result = new("46fea9c0-595b-416a-b636-6b36ee1d4fbc", description);

            if (!Typology.Modify.TryUpdateByName(result, null, string.Format("{0} [{1}]", description ?? "???", administrativeDivision == null ? "???" : administrativeDivision.Name), "Location", out Typology.Classes.Typology? typology_GISModel) || typology_GISModel is null)
            {
                return null;
            }

            foreach (Building2D building2D in building2Ds)
            {
                PolygonalFace2D? polygonalFace2D = building2D.PolygonalFace2D;
                if (polygonalFace2D is null)
                {
                    continue;
                }

                double area = polygonalFace2D.GetArea();
                if (Core.Query.AlmostEquals(area, 0))
                {
                    continue;
                }

                IPolygonal2D? polygonal2D = polygonalFace2D.ExternalEdge;
                if (polygonal2D is null)
                {
                    continue;
                }

                List<AdministrativeSubdivision>? administrativeSubdivisions = dictionary[new Core.Classes.GuidReference(building2D)];
                if(administrativeSubdivisions != null && administrativeSubdivisions.Count > 1)
                {
                    administrativeSubdivisions?.RemoveAll(x => x.PolygonalFace2D is null || x.PolygonalFace2D.GetArea() < area);
                    administrativeSubdivisions?.Sort((x, y) => x.PolygonalFace2D!.GetArea().CompareTo(y.PolygonalFace2D!.GetArea()));
                }

                if (administrativeSubdivisions is null || administrativeSubdivisions.Count == 0)
                {
                    continue;
                }

                AdministrativeSubdivision administrativeSubdivision = administrativeSubdivisions[0];

                BuildingGeneralFunction? buildingGeneralFunction = building2D.BuildingGeneralFunction;
                if (buildingGeneralFunction is null || buildingGeneralFunction.Value != BuildingGeneralFunction.residential_buildings)
                {
                    continue;
                }

                int storeys = building2D.Storeys;

                bool isUrban = administrativeSubdivision.IsCity();

                if (!Typology.Modify.TryUpdateByName(typology_GISModel, isUrban ? [1] : [2], isUrban.ToString(), "Urban", out Typology.Classes.Typology? typology_Building) || typology_Building == null)
                {
                    continue;
                }

                area = Core.Query.Round(area, Core.Constans.Tolerance.MacroDistance);

                double totalArea = Core.Query.Round(area * storeys, Core.Constans.Tolerance.MacroDistance);

                string totalAreaName = "???";
                int totalAreaIndex = 0;
                if (totalArea <= 100)
                {
                    totalAreaName = "<min, 100>";
                    totalAreaIndex = 1;
                }
                else if (totalArea <= 200)
                {
                    totalAreaName = "(100, 200>";
                    totalAreaIndex = 2;
                }
                else if (totalArea <= 300)
                {
                    totalAreaName = "(200, 300>";
                    totalAreaIndex = 3;
                }
                else if (totalArea <= 400)
                {
                    totalAreaName = "(300, 400>";
                    totalAreaIndex = 4;
                }
                else if (totalArea <= 500)
                {
                    totalAreaName = "(400, 500>";
                    totalAreaIndex = 5;
                }
                else
                {
                    totalAreaName = "(500, max>";
                    totalAreaIndex = 6;
                }

                if (!Typology.Modify.TryUpdateByName(typology_Building, [totalAreaIndex], totalAreaName, "Area [m2]", out typology_Building) || typology_Building is null)
                {
                    continue;
                }

                BuildingShapeSolver buildingShapeSolver = new(offset, thinnesRatio)
                {
                    Input = building2D
                };

                BuildingShape buildingShape = BuildingShape.Undefined;
                if (buildingShapeSolver.Solve())
                {
                    buildingShape = buildingShapeSolver.Output;
                }

                if (!Typology.Modify.TryUpdateByName(typology_Building, [(int)buildingShape], Core.Query.Description(buildingShape), "Shape", out typology_Building) || typology_Building is null)
                {
                    continue;
                }

                uint? occupancy = null;
                if (gISModel!.TryGetRelatedObjects<OccupancyCalculationResult, Building2DOccupancyCalculationResultRelation>(building2D, out List<OccupancyCalculationResult>? occupancyCalculationResults) && occupancyCalculationResults != null)
                {
                    OccupancyCalculationResult? occupancyCalculationResult = occupancyCalculationResults.FirstOrDefault();
                    if (occupancyCalculationResult?.Occupancy is uint occupancy_Temp)
                    {
                        occupancy = occupancy_Temp;
                    }
                }

                string occupancyName = "???";
                int occupancyIndex = 0;
                if (occupancy is null || occupancy.Value == 0)
                {
                    occupancyName = "Unoccupied";
                    occupancyIndex = 1;
                }
                else if (occupancy <= 5)
                {
                    occupancyName = "<min, 5>";
                    occupancyIndex = 2;
                }
                else if (occupancy <= 10)
                {
                    occupancyName = "(5, 10>";
                    occupancyIndex = 3;
                }
                else if (occupancy <= 15)
                {
                    occupancyName = "(10, 15>";
                    occupancyIndex = 4;
                }
                else if (occupancy <= 20)
                {
                    occupancyName = "(15, 20>";
                    occupancyIndex = 5;
                }
                else if (occupancy <= 30)
                {
                    occupancyName = "(20, 30>";
                    occupancyIndex = 6;
                }
                else if (occupancy <= 40)
                {
                    occupancyName = "(30, 40>";
                    occupancyIndex = 7;
                }
                else if (occupancy <= 50)
                {
                    occupancyName = "(40, 50>";
                    occupancyIndex = 8;
                }
                else if (occupancy <= 100)
                {
                    occupancyName = "(50, 100>";
                    occupancyIndex = 9;
                }
                else if (occupancy <= 150)
                {
                    occupancyName = "(100, 150>";
                    occupancyIndex = 10;
                }
                else if (occupancy <= 200)
                {
                    occupancyName = "(150, 200>";
                    occupancyIndex = 11;
                }
                else if (occupancy <= 250)
                {
                    occupancyName = "(200, 250>";
                    occupancyIndex = 12;
                }
                else if (occupancy <= 300)
                {
                    occupancyName = "(250, 300>";
                    occupancyIndex = 13;
                }
                else if (occupancy <= 350)
                {
                    occupancyName = "(300, 350>";
                    occupancyIndex = 14;
                }
                else if (occupancy <= 400)
                {
                    occupancyName = "(350, 400>";
                    occupancyIndex = 15;
                }
                else if (occupancy <= 450)
                {
                    occupancyName = "(400, 450>";
                    occupancyIndex = 16;
                }
                else if (occupancy <= 500)
                {
                    occupancyName = "(450, 500>";
                    occupancyIndex = 17;
                }
                else
                {
                    occupancyName = "(500, max>";
                    occupancyIndex = 18;
                }

                if (!Typology.Modify.TryUpdateByName(typology_Building, [occupancyIndex], occupancyName, "Occupancy [p]", out typology_Building) || typology_Building is null)
                {
                    continue;
                }

                typology_Building.AddReference(building2D.Guid.ToString());
            }

            return result;
        }

        public static Typology.Classes.Typology? Typology_Residential_2(GISModel? gISModel, string? description, double thinnesRatio = 0.95, double offset = 1)
        {
            if (gISModel is null)
            {
                return null;
            }

            List<Building2D>? building2Ds = gISModel.GetObjects<Building2D>();
            if (building2Ds is null || building2Ds.Count == 0)
            {
                return null;
            }

            List<AdministrativeDivision>? administrativeDivisions = Query.AdministrativeAreal2Ds<AdministrativeDivision>(gISModel, building2Ds);

            AdministrativeDivision? administrativeDivision = null;
            if (administrativeDivisions != null && administrativeDivisions.Count != 0)
            {
                administrativeDivisions.Sort((x, y) => ((int)y.AdministrativeDivisionType).CompareTo((int)x.AdministrativeDivisionType));
                administrativeDivision = administrativeDivisions[0];
            }
            else
            {
                administrativeDivision = gISModel.GetObjects<AdministrativeDivision>(x => x?.AdministrativeDivisionType == AdministrativeDivisionType.county)?.FirstOrDefault();
            }

            Dictionary<Core.Classes.GuidReference, List<AdministrativeSubdivision>?>? dictionary = GIS.Query.AdministrativeAreal2DsDictionary<AdministrativeSubdivision>(gISModel, building2Ds);
            if (dictionary is null || dictionary.Count == 0)
            {
                return null;
            }

            Typology.Classes.Typology result = new("fac3c860-5e65-4c5f-9966-ed6e4753b0e0", description);

            if (!Typology.Modify.TryUpdateByName(result, null, string.Format("{0} [{1}]", description ?? "???", administrativeDivision == null ? "???" : administrativeDivision.Name), "Location", out Typology.Classes.Typology? typology_GISModel) || typology_GISModel is null)
            {
                return null;
            }

            foreach (Building2D building2D in building2Ds)
            {
                PolygonalFace2D? polygonalFace2D = building2D.PolygonalFace2D;
                if (polygonalFace2D is null)
                {
                    continue;
                }

                double area = polygonalFace2D.GetArea();
                if (Core.Query.AlmostEquals(area, 0))
                {
                    continue;
                }

                IPolygonal2D? polygonal2D = polygonalFace2D.ExternalEdge;
                if (polygonal2D is null)
                {
                    continue;
                }

                List<AdministrativeSubdivision>? administrativeSubdivisions = dictionary[new Core.Classes.GuidReference(building2D)];
                if (administrativeSubdivisions != null && administrativeSubdivisions.Count > 1)
                {
                    administrativeSubdivisions?.RemoveAll(x => x.PolygonalFace2D is null || x.PolygonalFace2D.GetArea() < area);
                    administrativeSubdivisions?.Sort((x, y) => x.PolygonalFace2D!.GetArea().CompareTo(y.PolygonalFace2D!.GetArea()));
                }

                if (administrativeSubdivisions is null || administrativeSubdivisions.Count == 0)
                {
                    continue;
                }

                AdministrativeSubdivision administrativeSubdivision = administrativeSubdivisions[0];

                BuildingGeneralFunction? buildingGeneralFunction = building2D.BuildingGeneralFunction;
                if (buildingGeneralFunction is null || buildingGeneralFunction.Value != BuildingGeneralFunction.residential_buildings)
                {
                    continue;
                }

                bool isUrban = administrativeSubdivision.IsCity();

                if (!Typology.Modify.TryUpdateByName(typology_GISModel, isUrban ? [1] : [2], isUrban.ToString(), "Urban", out Typology.Classes.Typology? typology_Building) || typology_Building == null)
                {
                    continue;
                }

                area = Core.Query.Round(area, Core.Constans.Tolerance.MacroDistance);

                string areaName = "???";
                int areaIndex = 0;
                if (area <= 100)
                {
                    areaName = "<min, 100>";
                    areaIndex = 1;
                }
                else if (area <= 200)
                {
                    areaName = "(100, 200>";
                    areaIndex = 2;
                }
                else if (area <= 300)
                {
                    areaName = "(200, 300>";
                    areaIndex = 3;
                }
                else if (area <= 400)
                {
                    areaName = "(300, 400>";
                    areaIndex = 4;
                }
                else if (area <= 500)
                {
                    areaName = "(400, 500>";
                    areaIndex = 5;
                }
                else
                {
                    areaName = "(500, max>";
                    areaIndex = 6;
                }

                if (!Typology.Modify.TryUpdateByName(typology_Building, [areaIndex], areaName, "Area [m2]", out typology_Building) || typology_Building is null)
                {
                    continue;
                }

                ushort storeys = building2D.Storeys;

                string storeysName = "???";
                int storeysIndex = 0;
                if (storeys <= 1)
                {
                    storeysName = "<min, 1>";
                    storeysIndex = 1;
                }
                else if (storeys <= 2)
                {
                    storeysName = "<1, 2>";
                    storeysIndex = 2;
                }
                else if (storeys <= 3)
                {
                    storeysName = "<2, 3>";
                    storeysIndex = 3;
                }
                else if (storeys <= 4)
                {
                    storeysName = "<3, 4>";
                    storeysIndex = 4;
                }
                else if (storeys <= 5)
                {
                    storeysName = "<4, 5>";
                    storeysIndex = 5;
                }
                else
                {
                    storeysName = "(5, max>";
                    storeysIndex = 6;
                }

                if (!Typology.Modify.TryUpdateByName(typology_Building, [storeysIndex], storeysName, "Storeys", out typology_Building) || typology_Building is null)
                {
                    continue;
                }


                BuildingShapeSolver buildingShapeSolver = new(offset, thinnesRatio)
                {
                    Input = building2D
                };

                BuildingShape buildingShape = BuildingShape.Undefined;
                if (buildingShapeSolver.Solve())
                {
                    buildingShape = buildingShapeSolver.Output;
                }

                if (!Typology.Modify.TryUpdateByName(typology_Building, [(int)buildingShape], Core.Query.Description(buildingShape), "Shape", out typology_Building) || typology_Building is null)
                {
                    continue;
                }

                uint? occupancy = null;
                if (gISModel!.TryGetRelatedObjects<OccupancyCalculationResult, Building2DOccupancyCalculationResultRelation>(building2D, out List<OccupancyCalculationResult>? occupancyCalculationResults) && occupancyCalculationResults != null)
                {
                    OccupancyCalculationResult? occupancyCalculationResult = occupancyCalculationResults.FirstOrDefault();
                    if (occupancyCalculationResult?.Occupancy is uint occupancy_Temp)
                    {
                        occupancy = occupancy_Temp;
                    }
                }

                string occupancyName = "???";
                int occupancyIndex = 0;
                if (occupancy is null || occupancy.Value == 0)
                {
                    occupancyName = "Unoccupied";
                    occupancyIndex = 1;
                }
                else if (occupancy <= 5)
                {
                    occupancyName = "<min, 5>";
                    occupancyIndex = 2;
                }
                else if (occupancy <= 10)
                {
                    occupancyName = "(5, 10>";
                    occupancyIndex = 3;
                }
                else if (occupancy <= 15)
                {
                    occupancyName = "(10, 15>";
                    occupancyIndex = 4;
                }
                else if (occupancy <= 20)
                {
                    occupancyName = "(15, 20>";
                    occupancyIndex = 5;
                }
                else if (occupancy <= 30)
                {
                    occupancyName = "(20, 30>";
                    occupancyIndex = 6;
                }
                else if (occupancy <= 40)
                {
                    occupancyName = "(30, 40>";
                    occupancyIndex = 7;
                }
                else if (occupancy <= 50)
                {
                    occupancyName = "(40, 50>";
                    occupancyIndex = 8;
                }
                else if (occupancy <= 100)
                {
                    occupancyName = "(50, 100>";
                    occupancyIndex = 9;
                }
                else if (occupancy <= 150)
                {
                    occupancyName = "(100, 150>";
                    occupancyIndex = 10;
                }
                else if (occupancy <= 200)
                {
                    occupancyName = "(150, 200>";
                    occupancyIndex = 11;
                }
                else if (occupancy <= 250)
                {
                    occupancyName = "(200, 250>";
                    occupancyIndex = 12;
                }
                else if (occupancy <= 300)
                {
                    occupancyName = "(250, 300>";
                    occupancyIndex = 13;
                }
                else if (occupancy <= 350)
                {
                    occupancyName = "(300, 350>";
                    occupancyIndex = 14;
                }
                else if (occupancy <= 400)
                {
                    occupancyName = "(350, 400>";
                    occupancyIndex = 15;
                }
                else if (occupancy <= 450)
                {
                    occupancyName = "(400, 450>";
                    occupancyIndex = 16;
                }
                else if (occupancy <= 500)
                {
                    occupancyName = "(450, 500>";
                    occupancyIndex = 17;
                }
                else
                {
                    occupancyName = "(500, max>";
                    occupancyIndex = 18;
                }

                if (!Typology.Modify.TryUpdateByName(typology_Building, [occupancyIndex], occupancyName, "Occupancy [p]", out typology_Building) || typology_Building is null)
                {
                    continue;
                }

                typology_Building.AddReference(building2D.Guid.ToString());
            }

            return result;
        }

        public static Typology.Classes.Typology? Typology_Residential_3(GISModel? gISModel, string? description, double thinnesRatio = 0.95)
        {
            if (gISModel is null)
            {
                return null;
            }

            List<Building2D>? building2Ds = gISModel.GetObjects<Building2D>();
            if (building2Ds is null || building2Ds.Count == 0)
            {
                return null;
            }

            List<AdministrativeDivision>? administrativeDivisions = Query.AdministrativeAreal2Ds<AdministrativeDivision>(gISModel, building2Ds);

            AdministrativeDivision? administrativeDivision = null;
            if(administrativeDivisions != null && administrativeDivisions.Count != 0)
            {
                administrativeDivisions.Sort((x, y) => ((int)y.AdministrativeDivisionType).CompareTo((int)x.AdministrativeDivisionType));
                administrativeDivision = administrativeDivisions[0];
            }
            else
            {
                administrativeDivision = gISModel.GetObjects<AdministrativeDivision>(x => x?.AdministrativeDivisionType == AdministrativeDivisionType.county)?.FirstOrDefault();
            }

            Dictionary<Core.Classes.GuidReference, List<AdministrativeSubdivision>?>? dictionary = GIS.Query.AdministrativeAreal2DsDictionary<AdministrativeSubdivision>(gISModel, building2Ds);
            if (dictionary is null || dictionary.Count == 0)
            {
                return null;
            }

            Typology.Classes.Typology result = new("9167966d-0bde-431b-ac35-a5689ff096e7", description);

            if (!Typology.Modify.TryUpdateByName(result, null, string.Format("{0} [{1}]", description ?? "???", administrativeDivision == null ? "???" : administrativeDivision.Name), "Location", out Typology.Classes.Typology? typology_GISModel) || typology_GISModel is null)
            {
                return null;
            }

            foreach (Building2D building2D in building2Ds)
            {
                PolygonalFace2D? polygonalFace2D = building2D.PolygonalFace2D;
                if (polygonalFace2D is null)
                {
                    continue;
                }

                double area = polygonalFace2D.GetArea();
                if (Core.Query.AlmostEquals(area, 0))
                {
                    continue;
                }

                IPolygonal2D? polygonal2D = polygonalFace2D.ExternalEdge;
                if (polygonal2D is null)
                {
                    continue;
                }

                List<AdministrativeSubdivision>? administrativeSubdivisions = dictionary[new Core.Classes.GuidReference(building2D)];
                if (administrativeSubdivisions != null && administrativeSubdivisions.Count > 1)
                {
                    administrativeSubdivisions?.RemoveAll(x => x.PolygonalFace2D is null || x.PolygonalFace2D.GetArea() < area);
                    administrativeSubdivisions?.Sort((x, y) => x.PolygonalFace2D!.GetArea().CompareTo(y.PolygonalFace2D!.GetArea()));
                }

                if (administrativeSubdivisions is null || administrativeSubdivisions.Count == 0)
                {
                    continue;
                }

                AdministrativeSubdivision administrativeSubdivision = administrativeSubdivisions[0];

                BuildingGeneralFunction? buildingGeneralFunction = building2D.BuildingGeneralFunction;
                if (buildingGeneralFunction is null || buildingGeneralFunction.Value != BuildingGeneralFunction.residential_buildings)
                {
                    continue;
                }


                HashSet<BuildingSpecificFunction> buildingSpecificFunctions = building2D.BuildingSpecificFunctions;

                int storeys = building2D.Storeys;

                bool isUrban = administrativeSubdivision.IsCity();

                if (!Typology.Modify.TryUpdateByName(typology_GISModel, isUrban ? [1] : [2], isUrban.ToString(), "Urban", out Typology.Classes.Typology? typology_Building) || typology_Building == null)
                {
                    continue;
                }

                area = Core.Query.Round(area, Core.Constans.Tolerance.MacroDistance);

                double totalArea = Core.Query.Round(area * storeys, Core.Constans.Tolerance.MacroDistance);

                string totalAreaName = "???";
                int totalAreaIndex = 0;
                if (totalArea <= 100)
                {
                    totalAreaName = "<min, 100>";
                    totalAreaIndex = 1;
                }
                else if (totalArea <= 200)
                {
                    totalAreaName = "(100, 200>";
                    totalAreaIndex = 2;
                }
                else if (totalArea <= 300)
                {
                    totalAreaName = "(200, 300>";
                    totalAreaIndex = 3;
                }
                else if (totalArea <= 400)
                {
                    totalAreaName = "(300, 400>";
                    totalAreaIndex = 4;
                }
                else if (totalArea <= 500)
                {
                    totalAreaName = "(400, 500>";
                    totalAreaIndex = 5;
                }
                else
                {
                    totalAreaName = "(500, max>";
                    totalAreaIndex = 6;
                }

                if (!Typology.Modify.TryUpdateByName(typology_Building, [totalAreaIndex], totalAreaName, "Area [m2]", out typology_Building) || typology_Building is null)
                {
                    continue;
                }

                double isoperimetricRatio = Geometry.Planar.Query.IsoperimetricRatio(polygonal2D);
                double rectangularThinnessRatio = Geometry.Planar.Query.RectangularThinnessRatio(polygonal2D);
                double squareThinnessRatio = Geometry.Planar.Query.SquareThinnessRatio(polygonal2D);

                double ratio = 0;
                string ratioName = "???";
                int ratioIndex = 0;
                if(isoperimetricRatio > thinnesRatio || (isoperimetricRatio > rectangularThinnessRatio && isoperimetricRatio > squareThinnessRatio))
                {
                    ratio = isoperimetricRatio;
                    ratioName = "Circural";
                    ratioIndex = 1;
                }
                else if(squareThinnessRatio > thinnesRatio || (squareThinnessRatio > isoperimetricRatio && squareThinnessRatio > rectangularThinnessRatio))
                {
                    ratio = squareThinnessRatio;
                    ratioName = "Square";
                    ratioIndex = 2;
                }
                else
                {
                    ratio = rectangularThinnessRatio;
                    ratioName = "Rectangular";
                    ratioIndex = 3;
                }

                if (!Typology.Modify.TryUpdateByName(typology_Building, [ratioIndex], ratioName, "Typical Shape", out typology_Building) || typology_Building is null)
                {
                    continue;
                }

                string ratioValueName = "???";
                int ratioValueIndex = 0;
                if (ratio <= 0.25)
                {
                    ratioValueName = "<0, 0.25>";
                    ratioValueIndex = 1;
                }
                else if (ratio <= 0.5)
                {
                    ratioValueName = "(0.25, 0.5>";
                    ratioValueIndex = 2;
                }
                else if (ratio <= 0.75)
                {
                    ratioValueName = "(0.5, 0.75>";
                    ratioValueIndex = 3;
                }
                else
                {
                    ratioValueName = "(0.75, 1>";
                    ratioValueIndex = 4;
                }

                if (!Typology.Modify.TryUpdateByName(typology_Building, [ratioValueIndex], ratioValueName, "Ratio [0-1]", out typology_Building) || typology_Building is null)
                {
                    continue;
                }


                uint? occupancy = null;
                if (gISModel!.TryGetRelatedObjects<OccupancyCalculationResult, Building2DOccupancyCalculationResultRelation>(building2D, out List<OccupancyCalculationResult>? occupancyCalculationResults) && occupancyCalculationResults != null)
                {
                    OccupancyCalculationResult? occupancyCalculationResult = occupancyCalculationResults.FirstOrDefault();
                    if (occupancyCalculationResult?.Occupancy is uint occupancy_Temp)
                    {
                        occupancy = occupancy_Temp;
                    }
                }

                string occupancyName = "???";
                int occupancyIndex = 0;
                if (occupancy is null || occupancy.Value == 0)
                {
                    occupancyName = "Unoccupied";
                    occupancyIndex = 1;
                }
                else if (occupancy <= 5)
                {
                    occupancyName = "<min, 5>";
                    occupancyIndex = 2;
                }
                else if (occupancy <= 10)
                {
                    occupancyName = "(5, 10>";
                    occupancyIndex = 3;
                }
                else if (occupancy <= 15)
                {
                    occupancyName = "(10, 15>";
                    occupancyIndex = 4;
                }
                else if (occupancy <= 20)
                {
                    occupancyName = "(15, 20>";
                    occupancyIndex = 5;
                }
                else if (occupancy <= 30)
                {
                    occupancyName = "(20, 30>";
                    occupancyIndex = 6;
                }
                else if (occupancy <= 40)
                {
                    occupancyName = "(30, 40>";
                    occupancyIndex = 7;
                }
                else if (occupancy <= 50)
                {
                    occupancyName = "(40, 50>";
                    occupancyIndex = 8;
                }
                else if (occupancy <= 100)
                {
                    occupancyName = "(50, 100>";
                    occupancyIndex = 9;
                }
                else if (occupancy <= 150)
                {
                    occupancyName = "(100, 150>";
                    occupancyIndex = 10;
                }
                else if (occupancy <= 200)
                {
                    occupancyName = "(150, 200>";
                    occupancyIndex = 11;
                }
                else if (occupancy <= 250)
                {
                    occupancyName = "(200, 250>";
                    occupancyIndex = 12;
                }
                else if (occupancy <= 300)
                {
                    occupancyName = "(250, 300>";
                    occupancyIndex = 13;
                }
                else if (occupancy <= 350)
                {
                    occupancyName = "(300, 350>";
                    occupancyIndex = 14;
                }
                else if (occupancy <= 400)
                {
                    occupancyName = "(350, 400>";
                    occupancyIndex = 15;
                }
                else if (occupancy <= 450)
                {
                    occupancyName = "(400, 450>";
                    occupancyIndex = 16;
                }
                else if (occupancy <= 500)
                {
                    occupancyName = "(450, 500>";
                    occupancyIndex = 17;
                }
                else
                {
                    occupancyName = "(500, max>";
                    occupancyIndex = 18;
                }

                if (!Typology.Modify.TryUpdateByName(typology_Building, [occupancyIndex], occupancyName, "Occupancy [p]", out typology_Building) || typology_Building is null)
                {
                    continue;
                }

                typology_Building.AddReference(building2D.Guid.ToString());
            }

            return result;
        }

        public static Typology.Classes.Typology? Typology_Residential_4(GISModel? gISModel, string? description, double thinnesRatio = 0.95, double offset = 1)
        {
            if (gISModel is null)
            {
                return null;
            }

            List<Building2D>? building2Ds = gISModel.GetObjects<Building2D>();
            if (building2Ds is null || building2Ds.Count == 0)
            {
                return null;
            }

            List<AdministrativeDivision>? administrativeDivisions = Query.AdministrativeAreal2Ds<AdministrativeDivision>(gISModel, building2Ds);

            AdministrativeDivision? administrativeDivision = null;
            if (administrativeDivisions != null && administrativeDivisions.Count != 0)
            {
                administrativeDivisions.Sort((x, y) => ((int)y.AdministrativeDivisionType).CompareTo((int)x.AdministrativeDivisionType));
                administrativeDivision = administrativeDivisions[0];
            }
            else
            {
                administrativeDivision = gISModel.GetObjects<AdministrativeDivision>(x => x?.AdministrativeDivisionType == AdministrativeDivisionType.county)?.FirstOrDefault();
            }

            Dictionary<Core.Classes.GuidReference, List<AdministrativeSubdivision>?>? dictionary = GIS.Query.AdministrativeAreal2DsDictionary<AdministrativeSubdivision>(gISModel, building2Ds);
            if (dictionary is null || dictionary.Count == 0)
            {
                return null;
            }

            Typology.Classes.Typology result = new("7d80653e-a9bc-416f-a1ee-450113357469", description);

            if (!Typology.Modify.TryUpdateByName(result, null, string.Format("{0} [{1}]", description ?? "???", administrativeDivision == null ? "???" : administrativeDivision.Name), "Location", out Typology.Classes.Typology? typology_GISModel) || typology_GISModel is null)
            {
                return null;
            }

            foreach (Building2D building2D in building2Ds)
            {
                PolygonalFace2D? polygonalFace2D = building2D.PolygonalFace2D;
                if (polygonalFace2D is null)
                {
                    continue;
                }

                double area = polygonalFace2D.GetArea();
                if (Core.Query.AlmostEquals(area, 0))
                {
                    continue;
                }

                IPolygonal2D? polygonal2D = polygonalFace2D.ExternalEdge;
                if (polygonal2D is null)
                {
                    continue;
                }

                List<AdministrativeSubdivision>? administrativeSubdivisions = dictionary[new Core.Classes.GuidReference(building2D)];
                if (administrativeSubdivisions != null && administrativeSubdivisions.Count > 1)
                {
                    administrativeSubdivisions?.RemoveAll(x => x.PolygonalFace2D is null || x.PolygonalFace2D.GetArea() < area);
                    administrativeSubdivisions?.Sort((x, y) => x.PolygonalFace2D!.GetArea().CompareTo(y.PolygonalFace2D!.GetArea()));
                }

                if (administrativeSubdivisions is null || administrativeSubdivisions.Count == 0)
                {
                    continue;
                }

                AdministrativeSubdivision administrativeSubdivision = administrativeSubdivisions[0];

                BuildingGeneralFunction? buildingGeneralFunction = building2D.BuildingGeneralFunction;
                if (buildingGeneralFunction is null || buildingGeneralFunction.Value != BuildingGeneralFunction.residential_buildings)
                {
                    continue;
                }

                int storeys = building2D.Storeys;

                bool isUrban = administrativeSubdivision.IsCity();

                if (!Typology.Modify.TryUpdateByName(typology_GISModel, isUrban ? [1] : [2], isUrban.ToString(), "Urban", out Typology.Classes.Typology? typology_Building) || typology_Building == null)
                {
                    continue;
                }

                area = Core.Query.Round(area, Core.Constans.Tolerance.MacroDistance);

                double totalArea = Core.Query.Round(area * storeys, Core.Constans.Tolerance.MacroDistance);

                string totalAreaName = "???";
                int totalAreaIndex = 0;
                if (totalArea <= 100)
                {
                    totalAreaName = "<min, 100>";
                    totalAreaIndex = 1;
                }
                else if (totalArea <= 200)
                {
                    totalAreaName = "(100, 200>";
                    totalAreaIndex = 2;
                }
                else if (totalArea <= 300)
                {
                    totalAreaName = "(200, 300>";
                    totalAreaIndex = 3;
                }
                else if (totalArea <= 400)
                {
                    totalAreaName = "(300, 400>";
                    totalAreaIndex = 4;
                }
                else if (totalArea <= 500)
                {
                    totalAreaName = "(400, 500>";
                    totalAreaIndex = 5;
                }
                else
                {
                    totalAreaName = "(500, max>";
                    totalAreaIndex = 6;
                }

                if (!Typology.Modify.TryUpdateByName(typology_Building, [totalAreaIndex], totalAreaName, "Area [m2]", out typology_Building) || typology_Building is null)
                {
                    continue;
                }

                BuildingShapeSolver buildingShapeSolver = new(offset, thinnesRatio)
                {
                    Input = building2D
                };

                BuildingShape buildingShape = BuildingShape.Undefined;
                if (buildingShapeSolver.Solve())
                {
                    buildingShape = buildingShapeSolver.Output;
                }

                if (!Typology.Modify.TryUpdateByName(typology_Building, [(int)buildingShape], Core.Query.Description(buildingShape), "Shape", out typology_Building) || typology_Building is null)
                {
                    continue;
                }

                uint? occupancy = null;
                if (gISModel!.TryGetRelatedObjects<OccupancyCalculationResult, Building2DOccupancyCalculationResultRelation>(building2D, out List<OccupancyCalculationResult>? occupancyCalculationResults) && occupancyCalculationResults != null)
                {
                    OccupancyCalculationResult? occupancyCalculationResult = occupancyCalculationResults.FirstOrDefault();
                    if (occupancyCalculationResult?.Occupancy is uint occupancy_Temp)
                    {
                        occupancy = occupancy_Temp;
                    }
                }

                string occupancyName = "???";
                int occupancyIndex = 0;
                if (occupancy is null || occupancy.Value == 0)
                {
                    occupancyName = "Unoccupied";
                    occupancyIndex = 1;
                }
                else if (occupancy <= 5)
                {
                    occupancyName = "<min, 5>";
                    occupancyIndex = 2;
                }
                else if (occupancy <= 10)
                {
                    occupancyName = "(5, 10>";
                    occupancyIndex = 3;
                }
                else if (occupancy <= 15)
                {
                    occupancyName = "(10, 15>";
                    occupancyIndex = 4;
                }
                else if (occupancy <= 20)
                {
                    occupancyName = "(15, 20>";
                    occupancyIndex = 5;
                }
                else if (occupancy <= 30)
                {
                    occupancyName = "(20, 30>";
                    occupancyIndex = 6;
                }
                else if (occupancy <= 40)
                {
                    occupancyName = "(30, 40>";
                    occupancyIndex = 7;
                }
                else if (occupancy <= 50)
                {
                    occupancyName = "(40, 50>";
                    occupancyIndex = 8;
                }
                else if (occupancy <= 100)
                {
                    occupancyName = "(50, 100>";
                    occupancyIndex = 9;
                }
                else if (occupancy <= 150)
                {
                    occupancyName = "(100, 150>";
                    occupancyIndex = 10;
                }
                else if (occupancy <= 200)
                {
                    occupancyName = "(150, 200>";
                    occupancyIndex = 11;
                }
                else if (occupancy <= 250)
                {
                    occupancyName = "(200, 250>";
                    occupancyIndex = 12;
                }
                else if (occupancy <= 300)
                {
                    occupancyName = "(250, 300>";
                    occupancyIndex = 13;
                }
                else if (occupancy <= 350)
                {
                    occupancyName = "(300, 350>";
                    occupancyIndex = 14;
                }
                else if (occupancy <= 400)
                {
                    occupancyName = "(350, 400>";
                    occupancyIndex = 15;
                }
                else if (occupancy <= 450)
                {
                    occupancyName = "(400, 450>";
                    occupancyIndex = 16;
                }
                else if (occupancy <= 500)
                {
                    occupancyName = "(450, 500>";
                    occupancyIndex = 17;
                }
                else
                {
                    occupancyName = "(500, max>";
                    occupancyIndex = 18;
                }

                if (!Typology.Modify.TryUpdateByName(typology_Building, [occupancyIndex], occupancyName, "Occupancy [p]", out typology_Building) || typology_Building is null)
                {
                    continue;
                }

                CardinalDirection cardinalDirection = GIS.Query.CardinalDirection(building2D.Azimuth());

                if (!Typology.Modify.TryUpdateByName(typology_Building, [(int)cardinalDirection], Core.Query.Description(cardinalDirection), "Direction", out typology_Building) || typology_Building is null)
                {
                    continue;
                }

                typology_Building.AddReference(building2D.Guid.ToString());
            }

            return result;
        }

        public static Typology.Classes.Typology? Typology_Residential_5(GISModel? gISModel, string? description, double thinnesRatio = 0.95)
        {
            if (gISModel is null)
            {
                return null;
            }

            List<Building2D>? building2Ds = gISModel.GetObjects<Building2D>();
            if (building2Ds is null || building2Ds.Count == 0)
            {
                return null;
            }

            List<AdministrativeDivision>? administrativeDivisions = Query.AdministrativeAreal2Ds<AdministrativeDivision>(gISModel, building2Ds);

            AdministrativeDivision? administrativeDivision = null;
            if (administrativeDivisions != null && administrativeDivisions.Count != 0)
            {
                administrativeDivisions.Sort((x, y) => ((int)y.AdministrativeDivisionType).CompareTo((int)x.AdministrativeDivisionType));
                administrativeDivision = administrativeDivisions[0];
            }
            else
            {
                administrativeDivision = gISModel.GetObjects<AdministrativeDivision>(x => x?.AdministrativeDivisionType == AdministrativeDivisionType.county)?.FirstOrDefault();
            }

            Dictionary<Core.Classes.GuidReference, List<AdministrativeSubdivision>?>? dictionary = GIS.Query.AdministrativeAreal2DsDictionary<AdministrativeSubdivision>(gISModel, building2Ds);
            if (dictionary is null || dictionary.Count == 0)
            {
                return null;
            }

            Typology.Classes.Typology result = new("33249d82-93bc-4a7a-9113-e6579703f3c3", description);

            if (!Typology.Modify.TryUpdateByName(result, null, string.Format("{0} [{1}]", description ?? "???", administrativeDivision == null ? "???" : administrativeDivision.Name), "Location", out Typology.Classes.Typology? typology_GISModel) || typology_GISModel is null)
            {
                return null;
            }

            foreach (Building2D building2D in building2Ds)
            {
                PolygonalFace2D? polygonalFace2D = building2D.PolygonalFace2D;
                if (polygonalFace2D is null)
                {
                    continue;
                }

                double area = polygonalFace2D.GetArea();
                if (Core.Query.AlmostEquals(area, 0))
                {
                    continue;
                }

                IPolygonal2D? polygonal2D = polygonalFace2D.ExternalEdge;
                if (polygonal2D is null)
                {
                    continue;
                }

                List<AdministrativeSubdivision>? administrativeSubdivisions = dictionary[new Core.Classes.GuidReference(building2D)];
                if (administrativeSubdivisions != null && administrativeSubdivisions.Count > 1)
                {
                    administrativeSubdivisions?.RemoveAll(x => x.PolygonalFace2D is null || x.PolygonalFace2D.GetArea() < area);
                    administrativeSubdivisions?.Sort((x, y) => x.PolygonalFace2D!.GetArea().CompareTo(y.PolygonalFace2D!.GetArea()));
                }

                if (administrativeSubdivisions is null || administrativeSubdivisions.Count == 0)
                {
                    continue;
                }

                AdministrativeSubdivision administrativeSubdivision = administrativeSubdivisions[0];

                BuildingGeneralFunction? buildingGeneralFunction = building2D.BuildingGeneralFunction;
                if (buildingGeneralFunction is null || buildingGeneralFunction.Value != BuildingGeneralFunction.residential_buildings)
                {
                    continue;
                }


                HashSet<BuildingSpecificFunction> buildingSpecificFunctions = building2D.BuildingSpecificFunctions;

                int storeys = building2D.Storeys;

                bool isUrban = administrativeSubdivision.IsCity();

                if (!Typology.Modify.TryUpdateByName(typology_GISModel, isUrban ? [1] : [2], isUrban.ToString(), "Urban", out Typology.Classes.Typology? typology_Building) || typology_Building == null)
                {
                    continue;
                }

                area = Core.Query.Round(area, Core.Constans.Tolerance.MacroDistance);

                double totalArea = Core.Query.Round(area * storeys, Core.Constans.Tolerance.MacroDistance);

                string totalAreaName = "???";
                int totalAreaIndex = 0;
                if (totalArea <= 100)
                {
                    totalAreaName = "<min, 100>";
                    totalAreaIndex = 1;
                }
                else if (totalArea <= 200)
                {
                    totalAreaName = "(100, 200>";
                    totalAreaIndex = 2;
                }
                else if (totalArea <= 300)
                {
                    totalAreaName = "(200, 300>";
                    totalAreaIndex = 3;
                }
                else if (totalArea <= 400)
                {
                    totalAreaName = "(300, 400>";
                    totalAreaIndex = 4;
                }
                else if (totalArea <= 500)
                {
                    totalAreaName = "(400, 500>";
                    totalAreaIndex = 5;
                }
                else
                {
                    totalAreaName = "(500, max>";
                    totalAreaIndex = 6;
                }

                if (!Typology.Modify.TryUpdateByName(typology_Building, [totalAreaIndex], totalAreaName, "Area [m2]", out typology_Building) || typology_Building is null)
                {
                    continue;
                }

                double isoperimetricRatio = Geometry.Planar.Query.IsoperimetricRatio(polygonal2D);
                double rectangularThinnessRatio = Geometry.Planar.Query.RectangularThinnessRatio(polygonal2D);
                double squareThinnessRatio = Geometry.Planar.Query.SquareThinnessRatio(polygonal2D);

                double ratio = 0;
                string ratioName = "???";
                int ratioIndex = 0;
                if (isoperimetricRatio > thinnesRatio || (isoperimetricRatio > rectangularThinnessRatio && isoperimetricRatio > squareThinnessRatio))
                {
                    ratio = isoperimetricRatio;
                    ratioName = "Circural";
                    ratioIndex = 1;
                }
                else if (squareThinnessRatio > thinnesRatio || (squareThinnessRatio > isoperimetricRatio && squareThinnessRatio > rectangularThinnessRatio))
                {
                    ratio = squareThinnessRatio;
                    ratioName = "Square";
                    ratioIndex = 2;
                }
                else
                {
                    ratio = rectangularThinnessRatio;
                    ratioName = "Rectangular";
                    ratioIndex = 3;
                }

                if (!Typology.Modify.TryUpdateByName(typology_Building, [ratioIndex], ratioName, "Typical Shape", out typology_Building) || typology_Building is null)
                {
                    continue;
                }

                string ratioValueName = "???";
                int ratioValueIndex = 0;
                if (ratio <= 0.25)
                {
                    ratioValueName = "<0, 0.25>";
                    ratioValueIndex = 1;
                }
                else if (ratio <= 0.5)
                {
                    ratioValueName = "(0.25, 0.5>";
                    ratioValueIndex = 2;
                }
                else if (ratio <= 0.75)
                {
                    ratioValueName = "(0.5, 0.75>";
                    ratioValueIndex = 3;
                }
                else
                {
                    ratioValueName = "(0.75, 1>";
                    ratioValueIndex = 4;
                }

                if (!Typology.Modify.TryUpdateByName(typology_Building, [ratioValueIndex], ratioValueName, "Ratio [0-1]", out typology_Building) || typology_Building is null)
                {
                    continue;
                }


                uint? occupancy = null;
                if (gISModel!.TryGetRelatedObjects<OccupancyCalculationResult, Building2DOccupancyCalculationResultRelation>(building2D, out List<OccupancyCalculationResult>? occupancyCalculationResults) && occupancyCalculationResults != null)
                {
                    OccupancyCalculationResult? occupancyCalculationResult = occupancyCalculationResults.FirstOrDefault();
                    if (occupancyCalculationResult?.Occupancy is uint occupancy_Temp)
                    {
                        occupancy = occupancy_Temp;
                    }
                }

                string occupancyName = "???";
                int occupancyIndex = 0;
                if (occupancy is null || occupancy.Value == 0)
                {
                    occupancyName = "Unoccupied";
                    occupancyIndex = 1;
                }
                else if (occupancy <= 5)
                {
                    occupancyName = "<min, 5>";
                    occupancyIndex = 2;
                }
                else if (occupancy <= 10)
                {
                    occupancyName = "(5, 10>";
                    occupancyIndex = 3;
                }
                else if (occupancy <= 15)
                {
                    occupancyName = "(10, 15>";
                    occupancyIndex = 4;
                }
                else if (occupancy <= 20)
                {
                    occupancyName = "(15, 20>";
                    occupancyIndex = 5;
                }
                else if (occupancy <= 30)
                {
                    occupancyName = "(20, 30>";
                    occupancyIndex = 6;
                }
                else if (occupancy <= 40)
                {
                    occupancyName = "(30, 40>";
                    occupancyIndex = 7;
                }
                else if (occupancy <= 50)
                {
                    occupancyName = "(40, 50>";
                    occupancyIndex = 8;
                }
                else if (occupancy <= 100)
                {
                    occupancyName = "(50, 100>";
                    occupancyIndex = 9;
                }
                else if (occupancy <= 150)
                {
                    occupancyName = "(100, 150>";
                    occupancyIndex = 10;
                }
                else if (occupancy <= 200)
                {
                    occupancyName = "(150, 200>";
                    occupancyIndex = 11;
                }
                else if (occupancy <= 250)
                {
                    occupancyName = "(200, 250>";
                    occupancyIndex = 12;
                }
                else if (occupancy <= 300)
                {
                    occupancyName = "(250, 300>";
                    occupancyIndex = 13;
                }
                else if (occupancy <= 350)
                {
                    occupancyName = "(300, 350>";
                    occupancyIndex = 14;
                }
                else if (occupancy <= 400)
                {
                    occupancyName = "(350, 400>";
                    occupancyIndex = 15;
                }
                else if (occupancy <= 450)
                {
                    occupancyName = "(400, 450>";
                    occupancyIndex = 16;
                }
                else if (occupancy <= 500)
                {
                    occupancyName = "(450, 500>";
                    occupancyIndex = 17;
                }
                else
                {
                    occupancyName = "(500, max>";
                    occupancyIndex = 18;
                }

                if (!Typology.Modify.TryUpdateByName(typology_Building, [occupancyIndex], occupancyName, "Occupancy [p]", out typology_Building) || typology_Building is null)
                {
                    continue;
                }

                CardinalDirection cardinalDirection = GIS.Query.CardinalDirection(building2D.Azimuth());

                if (!Typology.Modify.TryUpdateByName(typology_Building, [(int)cardinalDirection], Core.Query.Description(cardinalDirection), "Direction", out typology_Building) || typology_Building is null)
                {
                    continue;
                }

                typology_Building.AddReference(building2D.Guid.ToString());
            }

            return result;
        }


        public static Typology.Classes.Typology? Typology_NonResidential_1(GISModel? gISModel, string? description, double thinnesRatio = 0.95, double offset = 1)
        {
            if (gISModel is null)
            {
                return null;
            }

            List<Building2D>? building2Ds = gISModel.GetObjects<Building2D>();
            if (building2Ds is null || building2Ds.Count == 0)
            {
                return null;
            }

            List<AdministrativeDivision>? administrativeDivisions = Query.AdministrativeAreal2Ds<AdministrativeDivision>(gISModel, building2Ds);

            AdministrativeDivision? administrativeDivision = null;
            if (administrativeDivisions != null && administrativeDivisions.Count != 0)
            {
                administrativeDivisions.Sort((x, y) => ((int)y.AdministrativeDivisionType).CompareTo((int)x.AdministrativeDivisionType));
                administrativeDivision = administrativeDivisions[0];
            }
            else
            {
                administrativeDivision = gISModel.GetObjects<AdministrativeDivision>(x => x?.AdministrativeDivisionType == AdministrativeDivisionType.county)?.FirstOrDefault();
            }

            Dictionary<Core.Classes.GuidReference, List<AdministrativeSubdivision>?>? dictionary = GIS.Query.AdministrativeAreal2DsDictionary<AdministrativeSubdivision>(gISModel, building2Ds);
            if (dictionary is null || dictionary.Count == 0)
            {
                return null;
            }

            Typology.Classes.Typology result = new("5507f608-5960-49c7-9f92-e5234d05319f", description);

            if (!Typology.Modify.TryUpdateByName(result, null, string.Format("{0} [{1}]", description ?? "???", administrativeDivision == null ? "???" : administrativeDivision.Name), "Location", out Typology.Classes.Typology? typology_GISModel) || typology_GISModel is null)
            {
                return null;
            }

            foreach (Building2D building2D in building2Ds)
            {
                PolygonalFace2D? polygonalFace2D = building2D.PolygonalFace2D;
                if (polygonalFace2D is null)
                {
                    continue;
                }

                double area = polygonalFace2D.GetArea();
                if (Core.Query.AlmostEquals(area, 0))
                {
                    continue;
                }

                IPolygonal2D? polygonal2D = polygonalFace2D.ExternalEdge;
                if (polygonal2D is null)
                {
                    continue;
                }

                List<AdministrativeSubdivision>? administrativeSubdivisions = dictionary[new Core.Classes.GuidReference(building2D)];
                if (administrativeSubdivisions != null && administrativeSubdivisions.Count > 1)
                {
                    administrativeSubdivisions?.RemoveAll(x => x.PolygonalFace2D is null || x.PolygonalFace2D.GetArea() < area);
                    administrativeSubdivisions?.Sort((x, y) => x.PolygonalFace2D!.GetArea().CompareTo(y.PolygonalFace2D!.GetArea()));
                }

                if (administrativeSubdivisions is null || administrativeSubdivisions.Count == 0)
                {
                    continue;
                }

                AdministrativeSubdivision administrativeSubdivision = administrativeSubdivisions[0];

                BuildingGeneralFunction? buildingGeneralFunction = building2D.BuildingGeneralFunction;
                if (buildingGeneralFunction is null || buildingGeneralFunction.Value == BuildingGeneralFunction.residential_buildings)
                {
                    continue;
                }

                int storeys = building2D.Storeys;

                bool isUrban = administrativeSubdivision.IsCity();

                if (!Typology.Modify.TryUpdateByName(typology_GISModel, isUrban ? [1] : [2], isUrban.ToString(), "Urban", out Typology.Classes.Typology? typology_Building) || typology_Building == null)
                {
                    continue;
                }

                if (!Typology.Modify.TryUpdateByName(typology_Building, [(int)buildingGeneralFunction], Core.Query.Description(buildingGeneralFunction), "Function", out typology_Building) || typology_Building is null)
                {
                    continue;
                }

                area = Core.Query.Round(area, Core.Constans.Tolerance.MacroDistance);

                double totalArea = Core.Query.Round(area * storeys, Core.Constans.Tolerance.MacroDistance);

                string totalAreaName = "???";
                int totalAreaIndex = 0;
                if (totalArea <= 100)
                {
                    totalAreaName = "<min, 100>";
                    totalAreaIndex = 1;
                }
                else if (totalArea <= 200)
                {
                    totalAreaName = "(100, 200>";
                    totalAreaIndex = 2;
                }
                else if (totalArea <= 300)
                {
                    totalAreaName = "(200, 300>";
                    totalAreaIndex = 3;
                }
                else if (totalArea <= 400)
                {
                    totalAreaName = "(300, 400>";
                    totalAreaIndex = 4;
                }
                else if (totalArea <= 500)
                {
                    totalAreaName = "(400, 500>";
                    totalAreaIndex = 5;
                }
                else
                {
                    totalAreaName = "(500, max>";
                    totalAreaIndex = 6;
                }

                if (!Typology.Modify.TryUpdateByName(typology_Building, [totalAreaIndex], totalAreaName, "Area [m2]", out typology_Building) || typology_Building is null)
                {
                    continue;
                }

                BuildingShapeSolver buildingShapeSolver = new(offset, thinnesRatio)
                {
                    Input = building2D
                };

                BuildingShape buildingShape = BuildingShape.Undefined;
                if (buildingShapeSolver.Solve())
                {
                    buildingShape = buildingShapeSolver.Output;
                }

                if (!Typology.Modify.TryUpdateByName(typology_Building, [(int)buildingShape], Core.Query.Description(buildingShape), "Shape", out typology_Building) || typology_Building is null)
                {
                    continue;
                }

                typology_Building.AddReference(building2D.Guid.ToString());
            }

            return result;
        }

        public static Typology.Classes.Typology? Typology_NonResidential_2(GISModel? gISModel, string? description, double thinnesRatio = 0.95)
        {
            if (gISModel is null)
            {
                return null;
            }

            List<Building2D>? building2Ds = gISModel.GetObjects<Building2D>();
            if (building2Ds is null || building2Ds.Count == 0)
            {
                return null;
            }

            List<AdministrativeDivision>? administrativeDivisions = Query.AdministrativeAreal2Ds<AdministrativeDivision>(gISModel, building2Ds);

            AdministrativeDivision? administrativeDivision = null;
            if (administrativeDivisions != null && administrativeDivisions.Count != 0)
            {
                administrativeDivisions.Sort((x, y) => ((int)y.AdministrativeDivisionType).CompareTo((int)x.AdministrativeDivisionType));
                administrativeDivision = administrativeDivisions[0];
            }
            else
            {
                administrativeDivision = gISModel.GetObjects<AdministrativeDivision>(x => x?.AdministrativeDivisionType == AdministrativeDivisionType.county)?.FirstOrDefault();
            }

            Dictionary<Core.Classes.GuidReference, List<AdministrativeSubdivision>?>? dictionary = GIS.Query.AdministrativeAreal2DsDictionary<AdministrativeSubdivision>(gISModel, building2Ds);
            if (dictionary is null || dictionary.Count == 0)
            {
                return null;
            }

            Typology.Classes.Typology result = new("78ae1eb5-98eb-4c8a-be3c-e8d5259147fd", description);

            if (!Typology.Modify.TryUpdateByName(result, null, string.Format("{0} [{1}]", description ?? "???", administrativeDivision == null ? "???" : administrativeDivision.Name), "Location", out Typology.Classes.Typology? typology_GISModel) || typology_GISModel is null)
            {
                return null;
            }

            foreach (Building2D building2D in building2Ds)
            {
                PolygonalFace2D? polygonalFace2D = building2D.PolygonalFace2D;
                if (polygonalFace2D is null)
                {
                    continue;
                }

                double area = polygonalFace2D.GetArea();
                if (Core.Query.AlmostEquals(area, 0))
                {
                    continue;
                }

                IPolygonal2D? polygonal2D = polygonalFace2D.ExternalEdge;
                if (polygonal2D is null)
                {
                    continue;
                }

                List<AdministrativeSubdivision>? administrativeSubdivisions = dictionary[new Core.Classes.GuidReference(building2D)];
                if (administrativeSubdivisions != null && administrativeSubdivisions.Count > 1)
                {
                    administrativeSubdivisions?.RemoveAll(x => x.PolygonalFace2D is null || x.PolygonalFace2D.GetArea() < area);
                    administrativeSubdivisions?.Sort((x, y) => x.PolygonalFace2D!.GetArea().CompareTo(y.PolygonalFace2D!.GetArea()));
                }

                if (administrativeSubdivisions is null || administrativeSubdivisions.Count == 0)
                {
                    continue;
                }

                AdministrativeSubdivision administrativeSubdivision = administrativeSubdivisions[0];

                BuildingGeneralFunction? buildingGeneralFunction = building2D.BuildingGeneralFunction;
                if (buildingGeneralFunction is null || buildingGeneralFunction.Value == BuildingGeneralFunction.residential_buildings)
                {
                    continue;
                }

                int storeys = building2D.Storeys;

                bool isUrban = administrativeSubdivision.IsCity();

                if (!Typology.Modify.TryUpdateByName(typology_GISModel, isUrban ? [1] : [2], isUrban.ToString(), "Urban", out Typology.Classes.Typology? typology_Building) || typology_Building == null)
                {
                    continue;
                }

                if (!Typology.Modify.TryUpdateByName(typology_Building, [(int)buildingGeneralFunction], Core.Query.Description(buildingGeneralFunction), "Function", out typology_Building) || typology_Building is null)
                {
                    continue;
                }

                area = Core.Query.Round(area, Core.Constans.Tolerance.MacroDistance);

                double totalArea = Core.Query.Round(area * storeys, Core.Constans.Tolerance.MacroDistance);

                string totalAreaName = "???";
                int totalAreaIndex = 0;
                if (totalArea <= 100)
                {
                    totalAreaName = "<min, 100>";
                    totalAreaIndex = 1;
                }
                else if (totalArea <= 200)
                {
                    totalAreaName = "(100, 200>";
                    totalAreaIndex = 2;
                }
                else if (totalArea <= 300)
                {
                    totalAreaName = "(200, 300>";
                    totalAreaIndex = 3;
                }
                else if (totalArea <= 400)
                {
                    totalAreaName = "(300, 400>";
                    totalAreaIndex = 4;
                }
                else if (totalArea <= 500)
                {
                    totalAreaName = "(400, 500>";
                    totalAreaIndex = 5;
                }
                else
                {
                    totalAreaName = "(500, max>";
                    totalAreaIndex = 6;
                }

                if (!Typology.Modify.TryUpdateByName(typology_Building, [totalAreaIndex], totalAreaName, "Area [m2]", out typology_Building) || typology_Building is null)
                {
                    continue;
                }

                double isoperimetricRatio = Geometry.Planar.Query.IsoperimetricRatio(polygonal2D);
                double rectangularThinnessRatio = Geometry.Planar.Query.RectangularThinnessRatio(polygonal2D);
                double squareThinnessRatio = Geometry.Planar.Query.SquareThinnessRatio(polygonal2D);

                double ratio = 0;
                string ratioName = "???";
                int ratioIndex = 0;
                if (isoperimetricRatio > thinnesRatio || (isoperimetricRatio > rectangularThinnessRatio && isoperimetricRatio > squareThinnessRatio))
                {
                    ratio = isoperimetricRatio;
                    ratioName = "Circural";
                    ratioIndex = 1;
                }
                else if (squareThinnessRatio > thinnesRatio || (squareThinnessRatio > isoperimetricRatio && squareThinnessRatio > rectangularThinnessRatio))
                {
                    ratio = squareThinnessRatio;
                    ratioName = "Square";
                    ratioIndex = 2;
                }
                else
                {
                    ratio = rectangularThinnessRatio;
                    ratioName = "Rectangular";
                    ratioIndex = 3;
                }

                if (!Typology.Modify.TryUpdateByName(typology_Building, [ratioIndex], ratioName, "Typical Shape", out typology_Building) || typology_Building is null)
                {
                    continue;
                }

                string ratioValueName = "???";
                int ratioValueIndex = 0;
                if (ratio <= 0.25)
                {
                    ratioValueName = "<0, 0.25>";
                    ratioValueIndex = 1;
                }
                else if (ratio <= 0.5)
                {
                    ratioValueName = "(0.25, 0.5>";
                    ratioValueIndex = 2;
                }
                else if (ratio <= 0.75)
                {
                    ratioValueName = "(0.5, 0.75>";
                    ratioValueIndex = 3;
                }
                else
                {
                    ratioValueName = "(0.75, 1>";
                    ratioValueIndex = 4;
                }

                if (!Typology.Modify.TryUpdateByName(typology_Building, [ratioValueIndex], ratioValueName, "Ratio [0-1]", out typology_Building) || typology_Building is null)
                {
                    continue;
                }

                typology_Building.AddReference(building2D.Guid.ToString());
            }

            return result;
        }

        public static Typology.Classes.Typology? Typology_NonResidential_3(GISModel? gISModel, string? description, double thinnesRatio = 0.95)
        {
            if (gISModel is null)
            {
                return null;
            }

            List<Building2D>? building2Ds = gISModel.GetObjects<Building2D>();
            if (building2Ds is null || building2Ds.Count == 0)
            {
                return null;
            }

            List<AdministrativeDivision>? administrativeDivisions = Query.AdministrativeAreal2Ds<AdministrativeDivision>(gISModel, building2Ds);

            AdministrativeDivision? administrativeDivision = null;
            if (administrativeDivisions != null && administrativeDivisions.Count != 0)
            {
                administrativeDivisions.Sort((x, y) => ((int)y.AdministrativeDivisionType).CompareTo((int)x.AdministrativeDivisionType));
                administrativeDivision = administrativeDivisions[0];
            }
            else
            {
                administrativeDivision = gISModel.GetObjects<AdministrativeDivision>(x => x?.AdministrativeDivisionType == AdministrativeDivisionType.county)?.FirstOrDefault();
            }

            Dictionary<Core.Classes.GuidReference, List<AdministrativeSubdivision>?>? dictionary = GIS.Query.AdministrativeAreal2DsDictionary<AdministrativeSubdivision>(gISModel, building2Ds);
            if (dictionary is null || dictionary.Count == 0)
            {
                return null;
            }

            Typology.Classes.Typology result = new("0131b910-f112-4748-93ce-e341aa56a9c2", description);

            if (!Typology.Modify.TryUpdateByName(result, null, string.Format("{0} [{1}]", description ?? "???", administrativeDivision == null ? "???" : administrativeDivision.Name), "Location", out Typology.Classes.Typology? typology_GISModel) || typology_GISModel is null)
            {
                return null;
            }

            foreach (Building2D building2D in building2Ds)
            {
                PolygonalFace2D? polygonalFace2D = building2D.PolygonalFace2D;
                if (polygonalFace2D is null)
                {
                    continue;
                }

                double area = polygonalFace2D.GetArea();
                if (Core.Query.AlmostEquals(area, 0))
                {
                    continue;
                }

                IPolygonal2D? polygonal2D = polygonalFace2D.ExternalEdge;
                if (polygonal2D is null)
                {
                    continue;
                }

                List<AdministrativeSubdivision>? administrativeSubdivisions = dictionary[new Core.Classes.GuidReference(building2D)];
                if (administrativeSubdivisions != null && administrativeSubdivisions.Count > 1)
                {
                    administrativeSubdivisions?.RemoveAll(x => x.PolygonalFace2D is null || x.PolygonalFace2D.GetArea() < area);
                    administrativeSubdivisions?.Sort((x, y) => x.PolygonalFace2D!.GetArea().CompareTo(y.PolygonalFace2D!.GetArea()));
                }

                if (administrativeSubdivisions is null || administrativeSubdivisions.Count == 0)
                {
                    continue;
                }

                AdministrativeSubdivision administrativeSubdivision = administrativeSubdivisions[0];

                BuildingGeneralFunction? buildingGeneralFunction = building2D.BuildingGeneralFunction;
                if (buildingGeneralFunction is null || buildingGeneralFunction.Value == BuildingGeneralFunction.residential_buildings)
                {
                    continue;
                }

                int storeys = building2D.Storeys;

                bool isUrban = administrativeSubdivision.IsCity();

                if (!Typology.Modify.TryUpdateByName(typology_GISModel, isUrban ? [1] : [2], isUrban.ToString(), "Urban", out Typology.Classes.Typology? typology_Building) || typology_Building == null)
                {
                    continue;
                }

                if (!Typology.Modify.TryUpdateByName(typology_Building, [(int)buildingGeneralFunction], Core.Query.Description(buildingGeneralFunction), "Function", out typology_Building) || typology_Building is null)
                {
                    continue;
                }

                area = Core.Query.Round(area, Core.Constans.Tolerance.MacroDistance);

                double totalArea = Core.Query.Round(area * storeys, Core.Constans.Tolerance.MacroDistance);

                string totalAreaName = "???";
                int totalAreaIndex = 0;
                if (totalArea <= 100)
                {
                    totalAreaName = "<min, 100>";
                    totalAreaIndex = 1;
                }
                else if (totalArea <= 200)
                {
                    totalAreaName = "(100, 200>";
                    totalAreaIndex = 2;
                }
                else if (totalArea <= 300)
                {
                    totalAreaName = "(200, 300>";
                    totalAreaIndex = 3;
                }
                else if (totalArea <= 400)
                {
                    totalAreaName = "(300, 400>";
                    totalAreaIndex = 4;
                }
                else if (totalArea <= 500)
                {
                    totalAreaName = "(400, 500>";
                    totalAreaIndex = 5;
                }
                else
                {
                    totalAreaName = "(500, max>";
                    totalAreaIndex = 6;
                }

                if (!Typology.Modify.TryUpdateByName(typology_Building, [totalAreaIndex], totalAreaName, "Area [m2]", out typology_Building) || typology_Building is null)
                {
                    continue;
                }

                double isoperimetricRatio = Geometry.Planar.Query.IsoperimetricRatio(polygonal2D);
                double rectangularThinnessRatio = Geometry.Planar.Query.RectangularThinnessRatio(polygonal2D);
                double squareThinnessRatio = Geometry.Planar.Query.SquareThinnessRatio(polygonal2D);

                double ratio = 0;
                string ratioName = "???";
                int ratioIndex = 0;
                if (isoperimetricRatio > thinnesRatio || (isoperimetricRatio > rectangularThinnessRatio && isoperimetricRatio > squareThinnessRatio))
                {
                    ratio = isoperimetricRatio;
                    ratioName = "Circural";
                    ratioIndex = 1;
                }
                else if (squareThinnessRatio > thinnesRatio || (squareThinnessRatio > isoperimetricRatio && squareThinnessRatio > rectangularThinnessRatio))
                {
                    ratio = squareThinnessRatio;
                    ratioName = "Square";
                    ratioIndex = 2;
                }
                else
                {
                    ratio = rectangularThinnessRatio;
                    ratioName = "Rectangular";
                    ratioIndex = 3;
                }

                if (!Typology.Modify.TryUpdateByName(typology_Building, [ratioIndex], ratioName, "Typical Shape", out typology_Building) || typology_Building is null)
                {
                    continue;
                }

                string ratioValueName = "???";
                int ratioValueIndex = 0;
                if (ratio <= 0.25)
                {
                    ratioValueName = "<0, 0.25>";
                    ratioValueIndex = 1;
                }
                else if (ratio <= 0.5)
                {
                    ratioValueName = "(0.25, 0.5>";
                    ratioValueIndex = 2;
                }
                else if (ratio <= 0.75)
                {
                    ratioValueName = "(0.5, 0.75>";
                    ratioValueIndex = 3;
                }
                else
                {
                    ratioValueName = "(0.75, 1>";
                    ratioValueIndex = 4;
                }

                if (!Typology.Modify.TryUpdateByName(typology_Building, [ratioValueIndex], ratioValueName, "Ratio [0-1]", out typology_Building) || typology_Building is null)
                {
                    continue;
                }

                CardinalDirection cardinalDirection = GIS.Query.CardinalDirection(building2D.Azimuth());

                if (!Typology.Modify.TryUpdateByName(typology_Building, [(int)cardinalDirection], Core.Query.Description(cardinalDirection), "Direction", out typology_Building) || typology_Building is null)
                {
                    continue;
                }

                typology_Building.AddReference(building2D.Guid.ToString());
            }

            return result;
        }

    }
}
