using DiGi.GIS.Classes;

namespace DiGi.UI.WPF.Core.Classes
{
    /// <summary>
    /// Provides data for the event that is raised when the ortho data selection changes.
    /// </summary>
    public class OrtoDataSelectionChangedEventArgs : EventArgs
    {
        private readonly OrtoDatas? ortoDatas;
        private readonly short? year;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrtoDataSelectionChangedEventArgs"/> class.
        /// </summary>
        /// <param name="ortoDatas">The orthophoto data associated with the selection change.</param>
        /// <param name="year">The year associated with the selected orthophoto data.</param>
        public OrtoDataSelectionChangedEventArgs(OrtoDatas? ortoDatas, short? year)
            : base()
        {
            this.ortoDatas = ortoDatas;
            this.year = year;
        }

        /// <summary>
        /// Gets the year associated with the ortho data selection change.
        /// </summary>
        public short? Year
        {
            get
            {
                return year;
            }
        }

        /// <summary> Gets the orthophoto data associated with the selection change event. </summary>
        public OrtoDatas? OrtoDatas
        {
            get
            {
                return ortoDatas;
            }
        }
    }
}
