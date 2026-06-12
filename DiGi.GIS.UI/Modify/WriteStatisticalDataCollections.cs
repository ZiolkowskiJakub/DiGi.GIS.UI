using DiGi.BDL.Classes;
using DiGi.GIS.Classes;
using DiGi.GIS.Constants;
using Microsoft.Win32;

namespace DiGi.GIS.UI
{
    public static partial class Modify
    {
        /// <summary>
        /// Asynchronously prompts the user to save statistical unit data using a file save dialog.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains <see langword="true"/> if the file was successfully saved; otherwise, <see langword="false"/>.</returns>
        public static async Task<bool> WriteStatisticalUnit()
        {
            SaveFileDialog saveFileDialog = new()
            {
                Filter = string.Format("{0} (*.{1})|*.{1}|All files (*.*)|*.*", FileTypeName.StatisticalUnitFile, FileExtension.StatisticalUnitFile)
            };
            bool? saveFileDialog_Result = saveFileDialog.ShowDialog();
            if (saveFileDialog_Result == null || !saveFileDialog_Result.HasValue || !saveFileDialog_Result.Value)
            {
                return false;
            }

            List<Unit>? units = await BDL.Create.Units();
            if (units == null)
            {
                return false;
            }

            StatisticalUnit? statisticalUnit = GIS.Create.StatisticalUnit(units);
            if (statisticalUnit == null)
            {
                return false;
            }

            bool result = false;

            using (StatisticalUnitFile statisticalUnitFile = new(saveFileDialog.FileName))
            {
                statisticalUnitFile.Value = statisticalUnit;
                result = statisticalUnitFile.Save();
            }

            return result;
        }
    }
}