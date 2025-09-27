using System.Reflection;

namespace DiGi.GIS.UI
{
    public static partial class Settings
    {
        public static string? DefaultDirectory { get; }  = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    }
}
