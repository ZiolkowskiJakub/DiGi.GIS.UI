namespace DiGi.GIS.UI
{
    public static partial class Query
    {
        public static string? GISModelFilePath(System.Windows.Window window)
        {
            return DiGi.UI.WPF.Core.Query.Path(window, Constans.FileTypeName.GISModelFile, Constans.FileExtension.GISModelFile);
        }
    }
}
