using DiGi.BDL.Enums;
using DiGi.GIS.Classes;
using DiGi.GIS.Constants;
using Microsoft.Win32;

namespace DiGi.GIS.UI
{
    public static partial class Modify
    {
        /// <summary>
        /// Asynchronously writes the specified statistical data collections to a file selected via an open file dialog.
        /// </summary>
        /// <param name="variables">The collection of variables to be written to the file.</param>
        /// <param name="years">The range of years for which the statistical data should be recorded.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains <see langword="true"/> if the data was successfully written; otherwise, <see langword="false"/>.</returns>
        public static async Task<bool> WriteStatisticalDataCollections(IEnumerable<Variable>? variables, Core.Classes.Range<int>? years)
        {
            bool? dialogResult;

            OpenFileDialog openFileDialog = new()
            {
                Filter = string.Format("{0} (*.{1})|*.{1}|All files (*.*)|*.*", FileTypeName.StatisticalUnitFile, FileExtension.StatisticalUnitFile)
            };
            dialogResult = openFileDialog.ShowDialog();
            if (dialogResult == null || !dialogResult.HasValue || !dialogResult.Value)
            {
                return false;
            }

            StatisticalUnit? statisticalUnit = null;

            using (StatisticalUnitFile statisticalUnitFile = new(openFileDialog.FileName))
            {
                statisticalUnitFile.Open();
                statisticalUnit = statisticalUnitFile.Value;
            }

            if (statisticalUnit == null)
            {
                return false;
            }

            IEnumerable<StatisticalUnit>? statisticalUnits = statisticalUnit.GetStatisticalUnits(true);
            if (statisticalUnits == null || !statisticalUnits.Any())
            {
                return false;
            }

            SaveFileDialog saveFileDialog = new()
            {
                Filter = string.Format("{0} (*.{1})|*.{1}|All files (*.*)|*.*", FileTypeName.StatisticalDataCollectionFile, FileExtension.StatisticalDataCollectionFile),
                FileName = string.Format("{0}.{1}", "StatisticalDataCollections", FileExtension.StatisticalDataCollectionFile)
            };
            dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult == null || !dialogResult.HasValue || !dialogResult.Value)
            {
                return false;
            }

            bool result = false;
            foreach (StatisticalUnit statisticalUnit_Temp in statisticalUnits)
            {
                bool succeeded = await GIS.Modify.Write(statisticalUnit_Temp, saveFileDialog.FileName, variables, years);
                if (succeeded)
                {
                    result = true;
                }
            }

            return result;
        }
    }
}