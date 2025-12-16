using DiGi.GIS.Classes;

namespace DiGi.GIS.UI
{
    public static partial class Create
    {
        public static OrtoDatasBuilding2DOptions? OrtoDatasBuilding2DOptions()
        {
            return new()
            {
                MaxFileSize = (1024UL * 1024UL * 1024UL * 5) / 10
            };
        }
    }
}
