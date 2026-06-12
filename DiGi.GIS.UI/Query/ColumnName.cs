using DiGi.BDL.Enums;

namespace DiGi.GIS.UI
{
    public static partial class Query
    {
        /// <summary>
        /// Gets the column name associated with the specified variable.
        /// </summary>
        /// <param name="variable">The <see cref="Variable"/> for which to retrieve the column name.</param>
        /// <returns>The column name as a string, or <c>null</c> if no corresponding name is found.</returns>
        public static string? ColumnName(this Variable variable)
        {
            if (variable == Variable.population_thousand_persons)
            {
                return "Polpulation";
            }

            return Core.Query.Description(variable);
        }
    }
}