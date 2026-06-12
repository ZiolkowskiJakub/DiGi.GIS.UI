using DiGi.GIS.Classes;

namespace DiGi.GIS.UI
{
    public static partial class Create
    {
        /// <summary>
        /// Creates a new instance of <see cref="OrtoDatasBuilding2DOptions"/> with default configuration settings.
        /// </summary>
        /// <returns>An instance of <see cref="OrtoDatasBuilding2DOptions"/> containing the default options.</returns>
        public static OrtoDatasBuilding2DOptions? OrtoDatasBuilding2DOptions()
        {
            return new()
            {
                MaxFileSize = (1024UL * 1024UL * 1024UL * 5) / 10
            };
        }
    }
}