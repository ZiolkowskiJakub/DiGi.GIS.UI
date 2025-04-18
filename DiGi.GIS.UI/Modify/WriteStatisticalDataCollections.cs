using DiGi.GIS.Classes;
using Microsoft.Win32;
using DiGi.GIS.Constans;
using DiGi.BDL.Classes;

namespace DiGi.GIS.UI
{
    public static partial class Modify
    {
        public static async Task<bool> WriteStatisticalUnit()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = string.Format("{0} (*.{1})|*.{1}|All files (*.*)|*.*", FileTypeName.StatisticalUnitFile, FileExtension.StatisticalUnitFile);
            bool? saveFileDialog_Result = saveFileDialog.ShowDialog();
            if (saveFileDialog_Result == null || !saveFileDialog_Result.HasValue || !saveFileDialog_Result.Value)
            {
                return false;
            }

            List<Unit> units = await BDL.Create.Units();
            if(units == null)
            {
                return false;
            }

            StatisticalUnit statisticalUnit = GIS.Create.StatisticalUnit(units);
            if(statisticalUnit == null)
            {
                return false;
            }

            bool result = false;

            using(StatisticalUnitFile statisticalUnitFile = new StatisticalUnitFile(saveFileDialog.FileName))
            {
                statisticalUnitFile.Value = statisticalUnit;
                result = statisticalUnitFile.Save();
            }

            return result;
        }
    }
}
