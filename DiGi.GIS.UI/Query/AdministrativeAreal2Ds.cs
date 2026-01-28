using DiGi.Core.Classes;
using DiGi.GIS.Classes;

namespace DiGi.GIS.UI
{
    public static partial class Query
    {
        /// <summary>
        /// Gets common AdministrativeAreal2Ds for given building2Ds
        /// </summary>
        /// <typeparam name="TAdministrativeAreal2D">Subtype of AdministrativeAreal2D</typeparam>
        /// <param name="gISModel">GISModel</param>
        /// <param name="building2Ds">Buidling2Ds</param>
        /// <returns>List of common AdministrativeAreal2Ds</returns>
        public static List<TAdministrativeAreal2D>? AdministrativeAreal2Ds<TAdministrativeAreal2D>(this GISModel? gISModel, IEnumerable<Building2D>? building2Ds) where TAdministrativeAreal2D : AdministrativeAreal2D
        {
            if (gISModel == null || building2Ds == null || !building2Ds.Any())
            {
                return null;
            }

            Dictionary<GuidReference, HashSet<GuidReference>?> dictionary = [];

            List<AdministrativeAreal2DBuilding2DsRelation>? administrativeAreal2DBuilding2DsRelations = gISModel.GetRelations<AdministrativeAreal2DBuilding2DsRelation>();
            if (administrativeAreal2DBuilding2DsRelations == null || administrativeAreal2DBuilding2DsRelations.Count == 0)
            {
                return null;
            }

            foreach (Building2D building2D in building2Ds)
            {
                if (building2D == null)
                {
                    continue;
                }

                dictionary[new GuidReference(building2D)] = null;
            }

            Parallel.ForEach(building2Ds, building2D =>
            {
                GuidReference guidReference_To = new(building2D);

                foreach (AdministrativeAreal2DBuilding2DsRelation administrativeAreal2DBuilding2DsRelation in administrativeAreal2DBuilding2DsRelations)
                {
                    if (administrativeAreal2DBuilding2DsRelation == null || !administrativeAreal2DBuilding2DsRelation.Contains(Core.Relation.Enums.RelationSide.To, guidReference_To))
                    {
                        continue;
                    }

                    if (administrativeAreal2DBuilding2DsRelation.UniqueReference_From is not GuidReference guidReference_From)
                    {
                        continue;
                    }

                    if (!dictionary.TryGetValue(guidReference_To, out HashSet<GuidReference>? guidReferences_From) || guidReferences_From == null)
                    {
                        guidReferences_From = [];
                        dictionary[guidReference_To] = guidReferences_From;
                    }

                    guidReferences_From.Add(guidReference_From);
                }
            });

            HashSet<GuidReference>? guidReferences = null;

            foreach (HashSet<GuidReference>? guidReferences_Temp in dictionary.Values)
            {
                if (guidReferences_Temp == null || guidReferences_Temp.Count == 0)
                {
                    continue;
                }

                guidReferences = guidReferences_Temp;
                break;
            }

            if (guidReferences == null)
            {
                return null;
            }

            List<TAdministrativeAreal2D> result = [];

            foreach (HashSet<GuidReference>? guidReferences_Temp in dictionary.Values)
            {
                if (guidReferences_Temp is null || guidReferences_Temp.Count == 0)
                {
                    continue;
                }

                List<GuidReference> guidReferences_ToRemove = [.. guidReferences];

                foreach (GuidReference guidReference_Temp in guidReferences_Temp)
                {
                    guidReferences_ToRemove.Remove(guidReference_Temp);
                }

                foreach (GuidReference guidReference_ToRemove in guidReferences_ToRemove)
                {
                    guidReferences.Remove(guidReference_ToRemove);
                }
            }

            foreach (GuidReference guidReference in guidReferences)
            {
                if (gISModel.GetObject<TAdministrativeAreal2D>(guidReference) is not TAdministrativeAreal2D administrativeAreal2D)
                {
                    continue;
                }

                result.Add(administrativeAreal2D);
            }

            return result;
        }
    }
}