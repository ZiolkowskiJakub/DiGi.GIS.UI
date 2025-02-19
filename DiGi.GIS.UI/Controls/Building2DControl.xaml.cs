using DiGi.GIS.Classes;
using System.Windows.Controls;

namespace DiGi.GIS.UI.Controls
{
    /// <summary>
    /// Interaction logic for Building2DControl.xaml
    /// </summary>
    public partial class Building2DControl : UserControl
    {
        private Building2D building2D;

        public Building2DControl()
        {
            InitializeComponent();
        }

        private void LoadBuilding2D()
        {
            TextBox_Guid.Text = null;
            TextBox_Reference.Text = null;
            TextBox_Storeys.Text = null;
            TextBox_BuildingGeneralFunction.Text = null;
            TextBox_BuildingSpecificFunctions.Text = null;
            TextBox_BuildingPhase.Text = null;

            if (building2D != null)
            {
                TextBox_Guid.Text = building2D.Guid.ToString();
                TextBox_Reference.Text = building2D.Reference;
                TextBox_Storeys.Text = building2D.Storeys.ToString();

                if(building2D.BuildingGeneralFunction != null)
                {
                    TextBox_BuildingGeneralFunction.Text = Core.Query.Description(building2D.BuildingGeneralFunction.Value);
                }

                if(building2D.BuildingSpecificFunctions != null)
                {
                    TextBox_BuildingSpecificFunctions.Text = string.Join(", ", building2D.BuildingSpecificFunctions.ToList().ConvertAll(x => Core.Query.Description(x)));
                }

                if (building2D.BuildingPhase != null)
                {
                    TextBox_BuildingPhase.Text = Core.Query.Description(building2D.BuildingPhase.Value);
                }
            }
        }

        public Building2D Building2D
        {
            get
            {
                return building2D;
            }

            set
            {
                building2D = value;
                LoadBuilding2D();
            }
        }
    }
}
