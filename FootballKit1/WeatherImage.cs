using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballKit1
{
    public class WeatherImage
    {
        public string title { get; set; }
        public string path { get; set; }

        public WeatherImage(string title, string path)
        {
            this.title = title;
            this.path = path;
        }
    }
}
