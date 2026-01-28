using DiGi.BDL.Enums;
using DiGi.GIS.Classes;
using DiGi.GIS.Constans;
using Microsoft.Win32;

namespace DiGi.GIS.UI
{
    public static partial class Modify
    {
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