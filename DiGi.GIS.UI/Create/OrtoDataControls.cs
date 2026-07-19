using DiGi.GIS.Classes;
using DiGi.GIS.UI.Controls;

namespace DiGi.GIS.UI
{
    public static partial class Create
    {
        /// <summary>
        /// Creates a list of Orto data controls for the specified GIS model file and 2D building entity.
        /// </summary>
        /// <param name="gISModelFile">The GIS model file that stores the GIS model data.</param>
        /// <param name="building2D">The 2D building entity associated with the controls.</param>
        /// <returns>A list of <see cref="OrtoDataControl"/> instances if both parameters are non-null; otherwise, <c>null</c>.</returns>
        public static List<OrtoDataControl>? OrtoDataControls(this GISModelFile? gISModelFile, Building2D? building2D)
        {
            if (gISModelFile is null || building2D == null)
            {
                return null;
            }

            return OrtoDataControls(gISModelFile.Value, gISModelFile.Path, building2D);
        }

        /// <summary>
        /// Creates or retrieves a list of orthodata controls associated with the specified GIS model and 2D building entity based on the provided file path.
        /// </summary>
        /// <param name="gISModel">The GIS model that manages unique objects such as Building2D and AdministrativeAreal2D.</param>
        /// <param name="path_GISModel">The file path to the GIS model source.</param>
        /// <param name="building2D">The 2D building entity for which orthodata controls are being generated.</param>
        /// <returns>A list of <see cref="OrtoDataControl"/> objects if all parameters are valid and the directory exists; otherwise, <c>null</c>.</returns>
        public static List<OrtoDataControl>? OrtoDataControls(this GISModel? gISModel, string? path_GISModel, Building2D? building2D)
        {
            if (gISModel is null || building2D == null || path_GISModel == null || string.IsNullOrWhiteSpace(path_GISModel))
            {
                return null;
            }

            string? directory_GISModel = System.IO.Path.GetDirectoryName(path_GISModel);
            if (!System.IO.Directory.Exists(directory_GISModel))
            {
                return null;
            }

            string? directory_OrtoDatas = GIS.Query.OrtoDatasDirectory_Building2D(directory_GISModel);
            if (!System.IO.Directory.Exists(directory_OrtoDatas))
            {
                return null;
            }

            short max = System.Convert.ToInt16(DateTime.Now.Year);

            OrtoDataControl? ortoDataControl;

            OrtoDatas? ortoDatas = GIS.Query.OrtoDatas(building2D, directory_OrtoDatas);
            if (ortoDatas == null || !ortoDatas.Any())
            {
                ortoDataControl = new OrtoDataControl()
                {
                    Year = max,
                    BitmapImage = building2D.BitmapImage(300, 300, 2),
                    Tag = null,
                    Active = false,
                    Width = 300,
                    Height = 300,
                    Margin = new System.Windows.Thickness(5, 5, 5, 5),
                };

                return [ortoDataControl];
            }

            short? userYear = GIS.Query.UserYearBuilt(directory_GISModel, building2D);

            SortedDictionary<short, OrtoDataControl> dictionary = [];

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

            max = System.Convert.ToInt16(Math.Min(dictionary.Keys.Max() + 1, DateTime.Now.Year));

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

            ortoDataControl?.BitmapImage = BitmapImage(building2D, directory_OrtoDatas, DateTime.Now.Year);

            short? predictedYear = GIS.Query.LatestPredictedYearBuilt(directory_GISModel, building2D);

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