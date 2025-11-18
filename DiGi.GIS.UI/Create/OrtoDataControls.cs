using DiGi.GIS.Classes;
using DiGi.GIS.UI.Controls;
//using System.Windows.Controls;

namespace DiGi.GIS.UI
{
    public static partial class Create
    {
        public static List<OrtoDataControl>? OrtoDataControls(this GISModelFile? gISModelFile, Building2D? building2D)
        {
            if(gISModelFile is null || building2D == null)
            {
                return null;
            }

            string? path = gISModelFile.Path;
            if (string.IsNullOrWhiteSpace(path))
            {
                return null;
            }

            string? directory = System.IO.Path.GetDirectoryName(path);
            if (!System.IO.Directory.Exists(directory))
            {
                return null;
            }

            directory = GIS.Query.OrtoDatasDirectory_Building2D(directory);

            return OrtoDataControls(gISModelFile.Value, building2D, directory);
        }

        public static List<OrtoDataControl>? OrtoDataControls(this GISModel? gISModel, Building2D? building2D, string? directory_OrtoDatas)
        {
            if (gISModel is null || building2D == null || directory_OrtoDatas == null || string.IsNullOrWhiteSpace(directory_OrtoDatas))
            {
                return null;
            }

            if (!System.IO.Directory.Exists(directory_OrtoDatas))
            {
                return null;
            }

            OrtoDatas? ortoDatas = GIS.Query.OrtoDatas(building2D, directory_OrtoDatas);
            if (ortoDatas == null || !ortoDatas.Any())
            {
                return null;
            }

            short? userYear = GIS.Query.UserYearBuilt(directory_OrtoDatas, building2D);

            short? predictedYear = GIS.Query.LatestPredictedYearBuilt(directory_OrtoDatas, building2D);

            SortedDictionary<short, OrtoDataControl> dictionary = [];

            OrtoDataControl? ortoDataControl;

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

                dictionary[max] = ortoDataControl;
            }

            ortoDataControl.BitmapImage = BitmapImage(building2D, directory_OrtoDatas, DateTime.Now.Year);

            if (predictedYear != null && predictedYear.HasValue && dictionary.Count != 0)
            {
                List<short> years = [.. dictionary.Keys];
                if (years.Count == 1)
                {
                    dictionary.First().Value.PredictedYear = predictedYear.Value;
                }

                years.Sort();

                if (years.First() >= predictedYear.Value)
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
                        else if (years[i] < predictedYear.Value && years[i + 1] > predictedYear.Value)
                        {
                            if (predictedYear.Value - years[i] > years[i + 1] - predictedYear.Value)
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

            return [.. dictionary.Values];
        }
    }
}
