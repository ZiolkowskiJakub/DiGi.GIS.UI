using DiGi.GIS.Classes;
using Microsoft.Win32;
using DiGi.GIS.Constans;
using DiGi.BDL.Enums;

namespace DiGi.GIS.UI
{
    public static partial class Modify
    {
        public static async Task<bool> WriteStatisticalDataCollections(IEnumerable<Variable> variables, Core.Classes.Range<int> years)
        {
            bool? dialogResult;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = string.Format("{0} (*.{1})|*.{1}|All files (*.*)|*.*", FileTypeName.StatisticalUnitFile, FileExtension.StatisticalUnitFile);
            dialogResult = openFileDialog.ShowDialog();
            if (dialogResult == null || !dialogResult.HasValue || !dialogResult.Value)
            {
                return false;
            }

            StatisticalUnit statisticalUnit = null;

            using (StatisticalUnitFile statisticalUnitFile = new StatisticalUnitFile(openFileDialog.FileName))
            {
                statisticalUnitFile.Open();
                statisticalUnit = statisticalUnitFile.Value;
            }

            if (statisticalUnit == null)
            {
                return false;
            }

            IEnumerable<StatisticalUnit> statisticalUnits = statisticalUnit.GetStatisticalUnits(true);
            if (statisticalUnits == null || statisticalUnits.Count() == 0)
            {
                return false;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = string.Format("{0} (*.{1})|*.{1}|All files (*.*)|*.*", FileTypeName.StatisticalDataCollectionFile, FileExtension.StatisticalDataCollectionFile);
            saveFileDialog.FileName = string.Format("{0}.{1}", "StatisticalDataCollections", FileExtension.StatisticalDataCollectionFile);
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
