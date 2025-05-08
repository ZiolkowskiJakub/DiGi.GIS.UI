using DiGi.GIS.Classes;
using DiGi.GIS.UI.Delegates;
using System.Windows.Controls;


namespace DiGi.GIS.UI.Controls
{
    /// <summary>
    /// Interaction logic for OrtoDatasListControl.xaml
    /// </summary>
    public partial class OrtoDatasListControl : UserControl
    {
        public OrtoDatasListControl()
        {
            InitializeComponent();

            OrtoDatasControl_Main.OrtoDataSelectionChanged += OrtoDatasControl_Main_OrtoDataSelectionChanged;
        }

        public event OrtoDataSelectionChangedEventHandler OrtoDataSelectionChanged;

        public List<OrtoDatas> OrtoDatasList
        {
            get
            {
                return GetOrtoDatasList();
            }

            set
            {
                SetOrtoDatasList(value);
            }
        }

        public OrtoDatas SelectedOrtoDatas
        {
            get
            {
                if (ListBox_Main.SelectedItems != null)
                {
                    foreach (object @object in ListBox_Main.SelectedItems)
                    {
                        ListBoxItem listBoxItem = @object as ListBoxItem;
                        if (listBoxItem == null)
                        {
                            break;
                        }

                        OrtoDatas result = listBoxItem.Tag as OrtoDatas;
                        if (result != null)
                        {
                            return result;
                        }
                    }
                }

                return null;
            }
        }

        private List<OrtoDatas> GetOrtoDatasList()
        {
            if (ListBox_Main.Items == null)
            {
                return null;
            }

            List<OrtoDatas> result = new List<OrtoDatas>();
            foreach (object @object in ListBox_Main.Items)
            {
                ListBoxItem listBoxItem = @object as ListBoxItem;
                if (listBoxItem == null)
                {
                    continue;
                }

                OrtoDatas ortoDatas = listBoxItem.Tag as OrtoDatas;
                if (ortoDatas == null)
                {
                    continue;
                }

                result.Add(ortoDatas);
            }

            return result;
        }

        private void ListBox_Main_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OrtoDatasControl_Main.OrtoDatas = SelectedOrtoDatas;
        }

        private void OrtoDatasControl_Main_OrtoDataSelectionChanged(object sender, DiGi.UI.WPF.Core.Classes.OrtoDataSelectionChangedEventArgs e)
        {
            OrtoDataSelectionChanged?.Invoke(this, e);
        }
        
        private void SetOrtoDatasList(IEnumerable<OrtoDatas> ortoDatasList)
        {
            ListBox_Main.Items.Clear();

            if(ortoDatasList == null)
            {
                return;
            }

            for(int i =0; i < ortoDatasList.Count(); i++)
            {
                OrtoDatas ortoDatas = ortoDatasList.ElementAt(i);

                ListBox_Main.Items.Add(new ListBoxItem() { Content = string.Format("[{0}] {1}", i + 1, ortoDatas.Reference), Tag = ortoDatas });
            }
        }
    }
}
