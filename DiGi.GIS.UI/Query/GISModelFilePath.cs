namespace DiGi.GIS.UI
{
    public static partial class Query
    {
        public static string? GISModelFilePath(System.Windows.Window window)
        {
            return DiGi.UI.WPF.Query.Path(window, Constants.FileFilter.GISModelFile);
        }
    }
}