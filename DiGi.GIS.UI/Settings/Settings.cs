using System.Reflection;

namespace DiGi.GIS.UI
{
    public static partial class Settings
    {
        /// <summary> Gets the default directory path based on the location of the currently executing assembly. </summary>
        public static string? DefaultDirectory { get; } = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    }
}
