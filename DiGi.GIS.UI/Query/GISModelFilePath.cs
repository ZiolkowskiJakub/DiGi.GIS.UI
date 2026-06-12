namespace DiGi.GIS.UI
{
    public static partial class Query
    {
        /// <summary>
        /// Opens a file dialog to select the path of a GIS model file.
        /// </summary>
        /// <param name="window">The owner window for the file selection dialog.</param>
        /// <returns>The full path of the selected GIS model file, or <c>null</c> if the operation was canceled.</returns>
        public static string? GISModelFilePath(System.Windows.Window window)
        {
            return DiGi.UI.WPF.Query.Path(window, Constants.FileFilter.GISModelFile);
        }
    }
}