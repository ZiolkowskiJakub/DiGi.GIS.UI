using System.Reflection;

namespace DiGi.GIS.UI
{
    public static partial class Settings
    {
        public static string DefaultDirectory = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    }
}
