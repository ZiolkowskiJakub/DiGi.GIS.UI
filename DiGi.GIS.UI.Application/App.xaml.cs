using DiGi.GIS.UI.Application.Windows;
using System.Windows;

namespace DiGi.GIS.UI.Application
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        void App_Startup(object sender, StartupEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.ShowDialog();

            //UI.Windows.GISWindow gISWindow = new UI.Windows.GISWindow();
            //gISWindow.Show();

            Shutdown();
        }
    }

}
