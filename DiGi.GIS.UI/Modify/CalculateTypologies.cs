using DiGi.Typology.Classes;
using System.IO;
using System.Windows;

namespace DiGi.GIS.UI
{
    public static partial class Modify
    {
        public static bool CalculateTypologies(Window? owner)
        {
            if (Create.Typologies(owner, out string? directory) is not List<Typology.Classes.Typology> typologies)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(directory) || !Directory.Exists(directory))
            {
                return false;
            }

            string directory_Temp = Path.Combine(directory, "Typology");
            if (!Directory.Exists(directory_Temp))
            {
                Directory.CreateDirectory(directory_Temp);
            }

            bool result = false;

            foreach (Typology.Classes.Typology typology in typologies)
            {
                if (typology?.ToSystem_Strings() is not string[] contents)
                {
                    continue;
                }

                string name = typology.Name ?? Guid.NewGuid().ToString();

                string path;

                path = Path.Combine(directory_Temp, name + ".txt");

                File.AppendAllLines(path, contents);

                path = Path.Combine(directory_Temp, name + "." + Typology.Constans.FileExtension.TypologyFile);

                using TypologyFile typologyFile = new(path);

                typologyFile.Value = typology;

                typologyFile.Save();

                result = true;
            }

            return result;
        }
    }
}