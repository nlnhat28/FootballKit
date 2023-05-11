using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FootballKit1
{
    public class WeatherController
    {
        public static string weatherFolderPath = Default.resourcesFolderPath + "\\Weather";
        public static string citiesFilePath = Default.resourcesFolderPath + "\\cities.json";
        public static int id { get; set; } = 0;
        public static List<City> listCities { get; set; } = new List<City>();
        public static List<string> listTitles { get; set; } = new List<string>();
        public static int cbbCityId { get; set; } = 38;

        public static List<WeatherImage> listWeatherImages = new List<WeatherImage>()
        {
            new WeatherImage("Nhiều nắng", weatherFolderPath + "\\1.png"),
            new WeatherImage("Có nắng", weatherFolderPath + "\\2.png"),
            new WeatherImage("Đẹp trời", weatherFolderPath + "\\3.png"),
            new WeatherImage( "Nhiều mây", weatherFolderPath + "\\4.png"),
            new WeatherImage( "Âm u", weatherFolderPath + "\\5.png"),
            new WeatherImage( "Mưa bóng mây", weatherFolderPath + "\\6.png"),
            new WeatherImage( "Mưa bay", weatherFolderPath + "\\7.png"),
            new WeatherImage( "Mưa nặng hạt", weatherFolderPath + "\\8.png"),
            new WeatherImage("Mưa giông", weatherFolderPath + "\\9.png"),
            new WeatherImage( "Mát mẻ", weatherFolderPath + "\\10.png"),
            new WeatherImage( "Gió mạnh", weatherFolderPath + "\\11.png"),
            new WeatherImage( "Sương mù", weatherFolderPath + "\\12.png"),
            new WeatherImage( "Trời tối", weatherFolderPath + "\\13.png"),
            new WeatherImage( "Trời tối mát mẻ", weatherFolderPath + "\\14.png"),
            new WeatherImage( "Trời tối nhiều mây", weatherFolderPath + "\\15.png"),
            new WeatherImage( "Trời mưa tối", weatherFolderPath + "\\16.png"),
            new WeatherImage( "Trời tối mưa giông", weatherFolderPath + "\\17.png"),
            new WeatherImage( "Lạnh giá", weatherFolderPath + "\\18.png")
        };

        public static void getListCitiesFromJson()
        {
            try
            {
                string json = File.ReadAllText(citiesFilePath);
                List<City> list = JsonConvert.DeserializeObject<List<City>>(json);
                list = list.OrderBy(c => c.province).ToList();
                listCities = new List<City>(list);

                foreach (City c in listCities)
                {
                    listTitles.Add(c.getTitle());
                    if (c.id == AppDataController.appData.idCity)
                    {
                        cbbCityId = listCities.IndexOf(c);
                    }
                }       
            }
            catch
            { };
        }
    }
}
