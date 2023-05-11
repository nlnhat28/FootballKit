using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballKit1
{
    class ListDataSource
    {
        public static readonly List<string> fromTime = new List<string>() { "0", "10", "15", "20", "25","30", "35", "40", "45", "50", "60", "70", "80", "90", "105", "100", "120"};
        public static readonly List<string> toTime = new List<string>() { "0", "10", "15", "20", "25", "30", "35", "40", "45", "50", "60", "70", "80", "90", "100", "105", "120" };
        public static readonly List<string> overTime = new List<string>() { "0","1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "12", "15","20" };
        public static readonly List<string> speed = new List<string>() { "0.25", "0.5", "0.75", "1", "1.25", "1.5", "1.75", "2" };

        public static readonly List<string> periodName = new List<string>() { "Hiệp 1", "Hết hiệp 1", "Hiệp 2", "Hết hiệp 2", "Hết trận",
                                                "Hiệp phụ 1", "Hết hiệp phụ 1", "Hiệp phụ 2", "Hết hiệp phụ 2", "Luân lưu" };
        public static readonly List<string> headerPen = new List<string>() { "01   02   03   04   05", "06   07   08   09   10",
                                             "11   12   13   14   15", "16   17   18   19   20",
                                             "21   22   23   24   25"};

        public static readonly List<string> buoiTrongNgay = new List<string>() { "Sáng", "Trưa", "Chiều", "Tối"};
        public static readonly List<string> nangMua = new List<string>() { "Trời đẹp", "Nắng gắt", "Có nắng", "Nắng nhẹ", "Mưa lớn", "Có mưa", "Mưa nhẹ", "Âm u" };
        public static readonly List<string> gio = new List<string>() { "Gió lớn", "Gió nhẹ", "Không có gió"};
        public static readonly List<string> nhietDo = new List<string>() { "Nóng", "Mát mẻ", "Lạnh" };
    }
}
