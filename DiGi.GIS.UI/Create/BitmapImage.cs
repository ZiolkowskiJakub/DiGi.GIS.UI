using DiGi.GIS.Classes;
using System.Windows.Media.Imaging;

namespace DiGi.GIS.UI
{
    public static partial class Create
    {
        public static BitmapImage BitmapImage(this OrtoData ortoData)
        {
            if (ortoData == null)
            {
                return null;
            }

            return DiGi.UI.WPF.Core.Create.BitmapImage(ortoData.Bytes);
        }
    }
}
