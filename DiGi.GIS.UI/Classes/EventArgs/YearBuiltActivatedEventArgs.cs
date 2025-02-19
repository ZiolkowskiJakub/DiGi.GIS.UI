using DiGi.GIS.Classes;

namespace DiGi.GIS.UI.Classes
{
    public class YearBuiltActivatedEventArgs
    {
        public int Year { get; }
        
        public Building2D Building2D { get; }

        public YearBuiltActivatedEventArgs(Building2D building2D, int year)
        {
            Year = year;
            Building2D = building2D;
        }
    }
}
