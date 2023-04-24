using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballKit1
{
    public class MatchController
    {
        public static fMain fMain { get; set; }

        public static string tenA { get; set; } = Default.tenA;
        public static string tenB { get; set; } = Default.tenB;

        public static string vietTatA { get; set; } = Default.vietTatA;
        public static string vietTatB { get; set; } = Default.vietTatB;

        public static Color mauA { get; set; } = Color.Red;
        public static Color mauB { get; set; } = Color.Blue;

        public static TimeSpan restartTime { get; set; }
        public static int toTime { get; set; } = 0;

        public static DateTime startTime { get; set; }

        public static List<string> doiHinhChinhA { get; set; } = new List<string>();
        public static List<string> doiHinhDuBiA { get; set; } = new List<string>();

        public static List<string> doiHinhChinhB { get; set; } = new List<string>();
        public static List<string> doiHinhDuBiB { get; set; } = new List<string>();

        public static string hlvA { get; set; } = string.Empty;
        public static string hlvB { get; set; } = string.Empty;

        public delegate void changeNudLuanLuu();
    }
}
