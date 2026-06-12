using DiGi.Typology.Classes;
using DiGi.UI.WPF.Classes;

namespace DiGi.GIS.UI
{
    public static partial class Create
    {
        /// <summary>
        /// Creates an <see cref="ItemPath"/> based on the provided typology and typology path.
        /// </summary>
        /// <param name="typology">The typology instance to search within.</param>
        /// <param name="typologyPath">The hierarchical path used to locate the item within the typology.</param>
        /// <param name="name">When this method returns, contains the name of the item found at the specified path; otherwise, <c>null</c>.</param>
        /// <param name="func">An optional function used to resolve a string value from the typology object.</param>
        /// <returns>An <see cref="ItemPath"/> if the item is successfully located; otherwise, <c>null</c>.</returns>
        public static ItemPath? ItemPath(this Typology.Classes.Typology? typology, TypologyPath? typologyPath, out string? name, Func<Typology.Classes.Typology?, string>? func = null)
        {
            name = null;

            if (typology == null || typologyPath == null)
            {
                return null;
            }

            Typology.Classes.Typology? typology_Temp = typology.GetTypology(typologyPath);
            if (typology_Temp is null)
            {
                return null;
            }

            func ??= new Func<Typology.Classes.Typology?, string>(x =>
                {
                    if (x is null)
                    {
                        return "???";
                    }

                    return string.Format("{0} : {1} -> {2}", x.TypologyPath?.ToString() ?? "[???]", x.Description ?? "???", x.Name ?? "???");
                });

            List<string> names = [func.Invoke(typology_Temp)];

            TypologyPath? typologyPath_Temp = typologyPath.Parent;
            while (typologyPath_Temp is not null)
            {
                typology_Temp = typology.GetTypology(typologyPath_Temp);
                if (typology_Temp is null)
                {
                    break;
                }

                names.Add(func.Invoke(typology_Temp));

                typologyPath_Temp = typologyPath_Temp.Parent;
            }

            names.Reverse();

            if (names.Count > 0)
            {
                name = names.Last();
                names.RemoveAt(names.Count - 1);
            }

            return names.Count == 0 ? null : new ItemPath(names);
        }
    }
}