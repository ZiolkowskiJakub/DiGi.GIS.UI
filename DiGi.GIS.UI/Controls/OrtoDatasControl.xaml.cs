using DiGi.Core;
using DiGi.GIS.Classes;
using DiGi.GIS.UI.Delegates;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace DiGi.GIS.UI.Controls
{
    /// <summary>
    /// Interaction logic for OrtoDatasControl.xaml
    /// </summary>
    public partial class OrtoDatasControl : UserControl
    {
        private Core.Classes.Size imageSize = new Core.Classes.Size(300, 300);
        private int margin = 5;
        
        private OrtoDatas ortoDatas;

        public OrtoDatasControl()
        {
            InitializeComponent();
        }

        public OrtoDatasControl(OrtoDatas ortoDatas, short? year = null)
        {
            InitializeComponent();

            OrtoDatas = ortoDatas;
            Year = year;
        }

        public event OrtoDataSelectionChangedEventHandler OrtoDataSelectionChanged;

        public Core.Classes.Size ImageSize
        {
            get
            {
                return imageSize?.Clone<Core.Classes.Size>();
            }

            set
            {
                if (value == null)
                {
                    return;
                }

                imageSize = value.Clone<Core.Classes.Size>();

                if (WrapPanel_Main.Children == null)
                {
                    return;
                }

                foreach (OrtoDataControl ortoDataControl_Temp in WrapPanel_Main.Children)
                {
                    if (ortoDataControl_Temp == null)
                    {
                        continue;
                    }

                    ortoDataControl_Temp.Width = imageSize.Width;
                    ortoDataControl_Temp.Height = imageSize.Height;
                }

            }
        }

        public int Margin
        {
            get
            {
                return margin;
            }

            set
            {
                margin = value;

                if (WrapPanel_Main.Children == null)
                {
                    return;
                }

                foreach (OrtoDataControl ortoDataControl_Temp in WrapPanel_Main.Children)
                {
                    if (ortoDataControl_Temp == null)
                    {
                        continue;
                    }

                    ortoDataControl_Temp.Margin = new System.Windows.Thickness(margin, margin, margin, margin);
                }

            }
        }

        public OrtoDatas OrtoDatas
        {
            get
            {
                return GetOrtoDatas();
            }

            set
            {
                SetOrtoDatas(value);
            }
        }

        public short? Year
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

        public List<short> Years
        {
            get
            {
                List<short> result = null;

                if (WrapPanel_Main?.Children == null)
                {
                    return result;
                }

                result = new List<short>();
                foreach (OrtoDataControl ortoDataControl_Temp in WrapPanel_Main.Children)
                {
                    if (ortoDataControl_Temp != null)
                    {
                        result.Add(ortoDataControl_Temp.Year);
                    }
                }

                return result;
            }
        }

        public bool Update(BitmapImage bitmapImage, short year)
        {
            return Update(bitmapImage, year, null);
        }

        private OrtoDataControl GetOrtoDataControl(short year)
        {
            if (WrapPanel_Main?.Children == null)
            {
                return null;
            }

            foreach (OrtoDataControl ortoDataControl_Temp in WrapPanel_Main.Children)
            {
                if (ortoDataControl_Temp.Year == year)
                {
                    return ortoDataControl_Temp;
                }
            }

            return null;
        }

        private OrtoDatas GetOrtoDatas()
        {
            return ortoDatas;
        }

        private short? GetYear()
        {
            short? result = null;

            if (WrapPanel_Main?.Children == null)
            {
                return result;
            }

            foreach (OrtoDataControl ortoDataControl_Temp in WrapPanel_Main.Children)
            {
                if (ortoDataControl_Temp != null && ortoDataControl_Temp.Active)
                {
                    result = ortoDataControl_Temp.Year;
                    break;
                }
            }

            return result;
        }

        private void OrtoDataControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton != MouseButton.Left)
            {
                return;
            }

            OrtoDataControl ortoDataControl = sender as OrtoDataControl;

            bool active = ortoDataControl == null ? false : ortoDataControl.Active;

            foreach (OrtoDataControl ortoDataControl_Temp in WrapPanel_Main.Children)
            {
                ortoDataControl_Temp.Active = false;
            }

            if (ortoDataControl == null)
            {
                return;
            }

            short? year = active ? null : ortoDataControl.Year;
            ortoDataControl.Active = !active;

            OrtoDataSelectionChanged?.Invoke(this, new DiGi.UI.WPF.Core.Classes.OrtoDataSelectionChangedEventArgs(ortoDatas, year));

            e.Handled = true;
        }

        private void SetOrtoDatas(OrtoDatas ortoDatas)
        {
            this.ortoDatas = ortoDatas;

            WrapPanel_Main.Children.Clear();

            if (ortoDatas == null)
            {
                return;
            }

            foreach (OrtoData ortoData in ortoDatas)
            {
                Update(ortoData);
            }
        }

        private void SetYear(short? year)
        {
            if (WrapPanel_Main?.Children == null)
            {
                return;
            }

            OrtoDataControl ortoDataControl_Active = null;

            foreach (OrtoDataControl ortoDataControl_Temp in WrapPanel_Main.Children)
            {
                if (ortoDataControl_Temp == null)
                {
                    continue;
                }

                if (year != ortoDataControl_Temp.Year || ortoDataControl_Active != null)
                {
                    ortoDataControl_Temp.Active = false;
                }
                else
                {
                    ortoDataControl_Active = ortoDataControl_Temp;
                }
            }

            if (ortoDataControl_Active != null)
            {
                ortoDataControl_Active.Active = true;
            }

        }

        private bool Update(OrtoData ortoData)
        {
            if(ortoData == null)
            {
                return false;
            }

            return Update(ortoData.BitmapImage(), System.Convert.ToInt16(ortoData.DateTime.Year), ortoData);
        }

        private bool Update(BitmapImage bitmapImage, short year, OrtoData ortoData)
        {
            OrtoDataControl ortoDataControl = GetOrtoDataControl(year);
            if(ortoDataControl != null)
            {
                ortoDataControl.BitmapImage = bitmapImage;
                return true;
            }

            ortoDataControl = new OrtoDataControl()
            {
                Year = year,
                BitmapImage = bitmapImage,
                Tag = ortoData,
                Active = false,
                Width = imageSize.Width,
                Height = imageSize.Height,
                Margin = new System.Windows.Thickness(margin, margin, margin, margin),
            };

            ortoDataControl.MouseDown += OrtoDataControl_MouseDown;

            WrapPanel_Main.Children.Add(ortoDataControl);
            return true;
        }
    }
}
