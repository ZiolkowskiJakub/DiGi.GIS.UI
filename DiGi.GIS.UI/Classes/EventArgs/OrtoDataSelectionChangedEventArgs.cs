using DiGi.GIS.Classes;

namespace DiGi.UI.WPF.Core.Classes
{
    public class OrtoDataSelectionChangedEventArgs : EventArgs
    {
        private OrtoDatas ortoDatas;
        private short? year;

        public OrtoDataSelectionChangedEventArgs(OrtoDatas ortoDatas, short? year)
            : base()
        {
            this.ortoDatas = ortoDatas;
            this.year = year;
        }

        public short? Year
        {
            get
            {
                return year;
            }
        }

        public OrtoDatas OrtoDatas
        {
            get
            {
                return ortoDatas;
            }
        }
    }
}
