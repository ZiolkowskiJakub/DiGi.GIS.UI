using DiGi.BDL.Enums;

namespace DiGi.GIS.UI
{
    public static partial class Query
    {
        public static string? ColumnName(this Variable variable)
        {
            if(variable == Variable.population_thousand_persons)
            {
                return "Polpulation";
            }

            return Core.Query.Description(variable);
        }
    }
}
