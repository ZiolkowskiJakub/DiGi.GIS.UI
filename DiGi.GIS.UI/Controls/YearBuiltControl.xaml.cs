using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DiGi.GIS.UI.Controls
{
    /// <summary>
    /// Interaction logic for YearBuiltControl.xaml
    /// </summary>
    public partial class YearBuiltControl : UserControl
    {
        public YearBuiltControl()
        {
            InitializeComponent();
        }

        public int Year
        {
            get
            {
                return GetYear();
            }

            set
            {
                SetYear(value);
            }
        }

        private bool SetYear(int year)
        {
            TextBlock_Main.Text = year > 0 ? year.ToString() : null;

            return !string.IsNullOrWhiteSpace(TextBlock_Main.Text);
        }

        private int GetYear()
        {
            string value = TextBlock_Main.Text;
            if(string.IsNullOrWhiteSpace(value))
            {
                return -1;
            }

            if(!int.TryParse(value, out int result))
            {
                return -1;
            }

            return result;

        }
    }
}
