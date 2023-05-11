using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballKit1
{
    class TxtFiles
    {
        public const string tenA = "\\TenA.txt";
        public const string tenB = "\\TenB.txt";

        public const string vietTatA = "\\VietTatA.txt";
        public const string vietTatB = "\\VietTatB.txt";

        public const string tiSoA = "\\TiSoA.txt";
        public const string tiSoB = "\\TiSoB.txt";

        public const string officialTime = "\\ThoiGianChinhThuc.txt";
        public const string overTime = "\\ThoiGianBuGio.txt";

        public const string hiepDau = "\\HiepDau.txt";
        public const string soPhutBuGio = "\\SoPhutBuGio.txt";

        public const string doiHinhChinhThucA = "\\DoiHinhChinhA.txt";
        public const string doiHinhChinhThucB = "\\DoiHinhChinhB.txt";

        public const string doiHinhDuBiA = "\\DoiHinhDuBiA.txt";
        public const string doiHinhDuBiB = "\\DoiHinhDuBiB.txt";

        public const string hlvA = "\\HLVA.txt";
        public const string hlvB = "\\HLVB.txt";

        public const string doiHinhTheDoA = "\\DoiHinhTheDoA.txt";
        public const string doiHinhTheDoB = "\\DoiHinhTheDoB.txt";

        public const string ghiBanA = "\\GhiBanA.txt";
        public const string ghiBanB = "\\GhiBanB.txt";

        public const string theVangA = "\\TheVangA.txt";
        public const string theVangB = "\\TheVangB.txt";

        public const string theDoA = "\\TheDoA.txt";
        public const string theDoB = "\\TheDoB.txt";

        public const string raSanA = "\\RaSanA.txt";
        public const string raSanB = "\\RaSanB.txt";

        public const string vaoSanA = "\\VaoSanA.txt";
        public const string vaoSanB = "\\VaoSanB.txt";

        public const string lichSuGhiBanA = "\\LichSuGhiBanA.txt";
        public const string lichSuGhiBanB = "\\LichSuGhiBanB.txt";

        public const string lichSuTheVangA = "\\LichSuTheVangA.txt";
        public const string lichSuTheVangB = "\\LichSuTheVangB.txt";

        public const string lichSuTheDoA = "\\LichSuTheDoA.txt";
        public const string lichSuTheDoB = "\\LichSuTheDoB.txt";

        public const string giaiDau = "\\GiaiDau.txt";
        public const string vongDau = "\\VongDau.txt";
        public const string diaDiem = "\\DiaDiem.txt";
        public const string san = "\\San.txt";

        public const string thoiGian = "\\ThoiGian.txt";
        public const string thoiTiet = "\\ThoiTiet.txt";

        public const string binhLuanVien = "\\BinhLuanVien.txt";
        public const string trongTai = "\\TrongTai.txt";

        //Penalties
        public const string tiSoLuanLuuA = "\\Penalties\\TiSoLuanLuuA.txt";
        public const string tiSoLuanLuuB = "\\Penalties\\TiSoLuanLuuB.txt";
        public const string luotPen = "\\Penalties\\LuotPen.txt";



        public static List<string> listTxtFilesPath = new List<string>() {ghiBanA, ghiBanB, theVangA, theVangB, theDoA, theDoB,
            raSanA, raSanB, vaoSanA, vaoSanB, luotPen};

        public static void createTxtFiles(string folderPath)
        {
            if (Directory.Exists(folderPath))
            {
                foreach (string filePath in listTxtFilesPath)
                {
                    if (!File.Exists(folderPath + filePath)) File.Create(folderPath + filePath);
                }
            }
        }
    }
}
