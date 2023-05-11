using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FootballKit1
{
    public class LabelDoiHinh
    {
        public string text { get; set; } = string.Empty;
        public Point location { get; set; } = new Point();
        public Color foreColor { get; set; } = Color.White;
        public Color backColor { get; set; } = Color.Ivory;

        public LabelDoiHinh (string text, Point location, Color foreColor, Color backColor)
        {
            this.text = text;
            this.location = location;
            this.foreColor = foreColor;
            this.backColor = backColor;
        }

    }
}
