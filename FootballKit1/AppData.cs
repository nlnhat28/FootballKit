using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballKit1
{
    public class AppData
    {
        public string defaultFolderPath { get; set; }
        public string lastFolderPath { get; set; }
        public bool isShowNotify { get; set; } = true;
        public Font font { get; set; } = Default.fontIcielCubano16;
    }
}
