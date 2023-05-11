using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballKit1
{
    public class City
    {
        public int id {get; set;}
        public string city { get; set; }
        public string province { get; set; }
        public Coord coord { get; set; }
        public class Coord
        {
            public double lon { get; set; }
            public double lat { get; set; }
        }

        public string getTitle()
        {
            string title = string.Empty;

            title = $"{province} (Tp.{city})";

            return title;
        }
    }
}
