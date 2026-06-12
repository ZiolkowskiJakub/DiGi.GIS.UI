using DiGi.GIS.Classes;
using System.Windows;

namespace DiGi.GIS.UI.Windows
{
    /// <summary>
    /// Interaction logic for Building2DWindow.xaml
    /// </summary>
    public partial class Building2DWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Building2DWindow"/> class.
        /// </summary>
        public Building2DWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Building2DWindow"/> class for the specified 2D building.
        /// </summary>
        /// <param name="building2D">The <see cref="Building2D"/> entity to be associated with this window.</param>
        public Building2DWindow(Building2D building2D)
            : this()
        {
            InitializeComponent();

            Building2D = building2D;
        }

        /// <summary>
        /// Gets or sets the <see cref="Building2D"/> entity associated with this window.
        /// </summary>
        public Building2D? Building2D
        {
            get
            {
                return Building2DControl_Main.Building2D;
            }

            set
            {
                Building2DControl_Main.Building2D = value;
            }
        }

        private void Button_OK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}