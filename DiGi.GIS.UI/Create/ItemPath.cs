using DiGi.UI.WPF.Core.Classes;
using DiGi.Typology.Classes;

namespace DiGi.GIS.UI
{
    public static partial class Create
    {
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

            if(func == null)
            {
                func = new Func<Typology.Classes.Typology?, string>(x => 
                { 
                    if(x is null)
                    {
                        return "???";
                    }

                    return string.Format("{0} : {1} -> {2}", x.TypologyPath?.ToString() ?? "[???]" , x.Description ?? "???", x.Name ?? "???"); 
                });
            }

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

            if(names.Count > 0)
            {
                name = names.Last();
                names.RemoveAt(names.Count - 1);
            }

            return names.Count == 0 ? null : new ItemPath(names);
        }
    }
}
