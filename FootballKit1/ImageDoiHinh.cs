using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballKit1
{
    public class ImageDoiHinh
    {
        public List<LabelDoiHinh> listLabelsCauThu { get; set; } = new List<LabelDoiHinh>();
        public LabelDoiHinh labelHLV { get; set; } = new LabelDoiHinh(string.Empty, new Point(36, 626), Color.White, Color.Ivory);
        public LabelDoiHinh labelTenDoi { get; set; } = new LabelDoiHinh(string.Empty, new Point(215, 40), Color.White, Color.Ivory);
        public LabelDoiHinh labelSoDo { get; set; } = new LabelDoiHinh(string.Empty, new Point(468, 626), Color.White, Color.Ivory);
        public string soDo { get; set; } = string.Empty;

        public Color foreColor { get; set; } = Color.White;
        public Color backColor { get; set; } = Color.Ivory;

        //public bool IsColorChanged { get; set; } = false;

        public void changeBackColor(Color backColor)
        {
            foreach(LabelDoiHinh lb in listLabelsCauThu)
            {
                lb.backColor = backColor;
            }
        }
    }
}
