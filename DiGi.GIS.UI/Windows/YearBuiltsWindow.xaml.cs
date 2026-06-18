using DiGi.GIS.Classes;
using System.Windows;

namespace DiGi.GIS.UI.Windows
{
    /// <summary>
    /// Interaction logic for YearBuiltsWindow.xaml
    /// </summary>
    public partial class YearBuiltsWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="YearBuiltsWindow"/> class.
        /// </summary>
        public YearBuiltsWindow()
        {
            InitializeComponent();
        }

        /// <summary> Gets or sets the GIS model file associated with the Year Builts window. </summary>
        public GISModelFile? GISModelFile
        {
            get
            {
                return YearBuiltsControl_Main?.GISModelFile;
            }

            set
            {
                if (YearBuiltsControl_Main != null)
                {
                    YearBuiltsControl_Main.GISModelFile = value;
                }
            }
        }

        private void MenuItem_About_Click(object sender, RoutedEventArgs e)
        {
        }

        private void MenuItem_Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MenuItem_Load_Click(object sender, RoutedEventArgs e)
        {
            string? path = Query.GISModelFilePath(this);
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            GISModelFile gISModelFile = new(path);
            gISModelFile.Open();

            YearBuiltsControl_Main.GISModelFile = gISModelFile;
        }
    }
}
