using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballKit1
{
    public class FootballKit
    {
        
        public string giaiDau { get; set; }
        public string vongDau { get; set; }
        public string diaDiem { get; set; }
        public string san { get; set; }
        public string thoiGian { get; set; }
        public int idThoiTiet { get; set; }

        public Team teamA { get; set; }
        public Team teamB { get; set; }

        public string binhLuanVien { get; set; }
        public string trongTai { get; set; }

        public int fromTime { get; set; }
        public int toTime { get; set; }
        public string thoiGianChinhThuc { get; set; }
        public string thoiGianBuGio { get; set; }

        public string hiepDau { get; set; }
        public int soPhutBuGio { get; set; }

        public void setDefault()
        {
            giaiDau = string.Empty;
            vongDau = string.Empty;
            diaDiem = string.Empty;
            san = string.Empty;
            binhLuanVien = string.Empty;
            trongTai = string.Empty;
            hiepDau = string.Empty;

            teamA = new Team()
            {
                ten = "Đội A",
                vietTat = "Đội A",
                mauAo = Default.mauA,
                mauQuan = Default.mauA,
                tiSo = 0,
                tiSoLuanLuu = 0,
            };
            teamB = new Team()
            {
                ten = "Đội B",
                vietTat = "Đội B",
                mauAo = Default.mauB,
                mauQuan = Default.mauB,
                tiSo = 0,
                tiSoLuanLuu = 0,
            };


        }
    }
}
