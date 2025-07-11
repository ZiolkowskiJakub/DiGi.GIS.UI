﻿using DiGi.GIS.Classes;
using System.Windows.Controls;
using System.Windows.Input;

namespace DiGi.GIS.UI.Controls
{
    /// <summary>
    /// Interaction logic for YearBuiltsControl.xaml
    /// </summary>
    public partial class YearBuiltsControl : UserControl
    {
        private GISModelFile gISModelFile;

        public YearBuiltsControl()
        {
            InitializeComponent();
        }

        private void LoadGISModelFile()
        {
            ListBox_Main.Items.Clear();
            WrapPanel_Main.Children.Clear();

            ListBox_Main.SelectionChanged -= ListBox_Main_SelectionChanged;

            GISModel gISModel = gISModelFile.Value;
            if(gISModel == null)
            {
                return;
            }

            List<Building2D> building2Ds = gISModel.GetObjects<Building2D>();
            if(building2Ds == null || building2Ds.Count == 0)
            {
                return;
            }

            for (int i = 0; i < building2Ds.Count; i++)
            {
                Building2D building2D = building2Ds[i];

                ListBox_Main.Items.Add(new ListBoxItem() { Content = string.Format("[{0}] {1}", i + 1, building2D.Reference), Tag = building2D });
            }

            ListBox_Main.SelectionChanged += ListBox_Main_SelectionChanged;
        }

        private void ListBox_Main_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WrapPanel_Main.Children.Clear();

            Building2D building2D = Building2D;

            Building2DControl_Main.Building2D = building2D;

            string path = gISModelFile.Path;
            if(string.IsNullOrWhiteSpace(path))
            {
                return;
            }

            string directory = System.IO.Path.GetDirectoryName(path);
            if(!System.IO.Directory.Exists(directory))
            {
                return;
            }

            directory = GIS.Query.OrtoDatasDirectory_Building2D(directory);

            OrtoDatas ortoDatas = GIS.Query.OrtoDatas(building2D, directory);
            if(ortoDatas == null || ortoDatas.Count() == 0)
            {
                return;
            }

            short? userYear = GIS.Query.UserYearBuilt(gISModelFile, building2D);

            //TODO: Implement predicted year
            short? predictedYear = GIS.Query.LatestPredictedYearBuilt(gISModelFile, building2D);

            SortedDictionary<short, OrtoDataControl> dictionary = new SortedDictionary<short, OrtoDataControl>();

            OrtoDataControl ortoDataControl;

            foreach (OrtoData ortoData in ortoDatas)
            {
                short year_Temp = System.Convert.ToInt16(ortoData.DateTime.Year);

                ortoDataControl = new OrtoDataControl()
                {
                    Year = year_Temp,
                    BitmapImage = ortoData.BitmapImage(),
                    Tag = ortoData,
                    Active = year_Temp == userYear,
                    Width = 300,
                    Height = 300,
                    Margin = new System.Windows.Thickness(5, 5, 5, 5),
                };

                ortoDataControl.MouseDown += OrtoDataControl_MouseDown;

                dictionary[year_Temp] = ortoDataControl;   
            }

            short max = System.Convert.ToInt16(Math.Min(dictionary.Keys.Max() + 1, DateTime.Now.Year));

            if (!dictionary.TryGetValue(max, out ortoDataControl) || ortoDataControl == null)
            {
                ortoDataControl = new OrtoDataControl()
                {
                    Year = max,
                    BitmapImage = null,
                    Tag = null,
                    Active = max == userYear,
                    Width = 300,
                    Height = 300,
                    Margin = new System.Windows.Thickness(5, 5, 5, 5),
                };

                ortoDataControl.MouseDown += OrtoDataControl_MouseDown;

                dictionary[max] = ortoDataControl;
            }

            ortoDataControl.BitmapImage = Create.BitmapImage(building2D, directory, DateTime.Now.Year);

            foreach (OrtoDataControl ortoDataControl_Temp in dictionary.Values)
            {
                WrapPanel_Main.Children.Add(ortoDataControl_Temp);
            }

            if(predictedYear != null && predictedYear.HasValue && dictionary.Count !=0)
            {
                List<short> years = dictionary.Keys.ToList();
                if(years.Count == 1)
                {
                    dictionary.First().Value.PredictedYear = predictedYear.Value;
                }

                years.Sort();

                if(years.First() >= predictedYear.Value)
                {
                    dictionary[years.First()].PredictedYear = predictedYear.Value;
                }
                else if (years.Last() <= predictedYear.Value)
                {
                    dictionary[years.Last()].PredictedYear = predictedYear.Value;
                }
                else
                {
                    for (int i = 0; i < years.Count - 1; i++)
                    {
                        if (years[i].Equals(predictedYear.Value))
                        {
                            dictionary[years[i]].PredictedYear = predictedYear.Value;
                            break;
                        }
                        else if(years[i] < predictedYear.Value && years[i + 1] > predictedYear.Value)
                        {
                            if(predictedYear.Value - years[i] > years[i + 1] - predictedYear.Value)
                            {
                                dictionary[years[i + 1]].PredictedYear = predictedYear.Value;
                            }
                            else
                            {
                                dictionary[years[i]].PredictedYear = predictedYear.Value;
                            }
                            break;
                        }
                    }
                }
            }
        }

        private void OrtoDataControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
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

            GIS.Modify.UpdateUserYearBuilt(gISModelFile, Building2D, year);

            //ListBox_Main.Focus();
            e.Handled = true;
        }

        public GISModelFile GISModelFile
        {
            get
            {
                return gISModelFile;
            }

            set
            {
                gISModelFile = value;
                LoadGISModelFile();
            }
        }

        public Building2D Building2D
        {
            get
            {
                ListBoxItem listBoxItem = ListBox_Main.SelectedValue as ListBoxItem;
                if (listBoxItem == null)
                {
                    return null;
                }

                return listBoxItem.Tag as Building2D;
            }
        }

        private void Button_Next_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            int count = ListBox_Main.Items.Count;
            if(count <= 0)
            {
                return;
            }

            int index = ListBox_Main.SelectedIndex;
            index++;

            if(index >= count)
            {
                return;
            }

            ListBox_Main.SelectedIndex = index;

            ListBox_Main.ScrollIntoView(ListBox_Main.SelectedItem);
        }
    }
}
