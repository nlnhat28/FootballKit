using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace FootballKit1
{

    class ACSList
    {
        //public const string ACSFolderPath = Default.appDataFolderPath + "\\ACS";
        public static readonly string ACSFolderPath = Default.newAppDataFolderPath + "\\ACS";

        public static readonly ACS tenThuMuc = new ACS(new List<string>(), ACSFolderPath + "\\TenThuMuc.txt", "Tên thư mục");

        public static readonly ACS tenDoi = new ACS(new List<string>(), ACSFolderPath + "\\TenDoi.txt", "Tên đội");
        public static readonly ACS vietTat = new ACS(new List<string>(), ACSFolderPath + "\\VietTat.txt", "Tên viết tắt");

        public static readonly ACS giaiDau = new ACS(new List<string>(), ACSFolderPath + "\\GiaiDau.txt", "Giải đấu");
        public static readonly ACS vongDau = new ACS(new List<string>(), ACSFolderPath + "\\VongDau.txt", "Vòng đấu");

        public static readonly ACS diaDiem = new ACS(new List<string>(), ACSFolderPath + "\\DiaDiem.txt", "Địa điểm");
        public static readonly ACS san = new ACS(new List<string>(), ACSFolderPath + "\\San.txt", "Sân");
        public static readonly ACS thoiGian = new ACS(new List<string>(), ACSFolderPath + "\\ThoiGian.txt", "Thời gian");

        public static readonly ACS binhLuanVien = new ACS(new List<string>(), ACSFolderPath + "\\BinhLuanVien.txt", "Bình luận viên");
        public static readonly ACS trongTai = new ACS(new List<string>(), ACSFolderPath + "\\TrongTai.txt", "Trọng tài");

        public static readonly ACS hlv = new ACS(new List<string>(), ACSFolderPath + "\\HLV.txt", "Huấn luyện viên");

        public static List<ACS> listACS = new List<ACS>() {
            tenThuMuc, tenDoi, vietTat, giaiDau, vongDau, diaDiem, san, thoiGian, binhLuanVien, trongTai, hlv
        };

        private static readonly List<string> listACSTxtFiles = new List<string>() {
            tenThuMuc.txtFile, tenDoi.txtFile, vietTat.txtFile, giaiDau.txtFile, vongDau.txtFile,
            diaDiem.txtFile, san.txtFile, thoiGian.txtFile, binhLuanVien.txtFile, trongTai.txtFile, hlv.txtFile};

        //public static void createACSTxtFiles()
        //{
        //    if (!Directory.Exists(ACSPath)) Directory.CreateDirectory(ACSPath);
        //    foreach (string filePath in listACSTxtFiles)
        //    {
        //        if (!File.Exists(filePath)) File.Create(filePath);
        //    }
        //}
        
        public static void saveACSToTxtFile(ACS aCS)
        {
            try
            {
                string filePath = aCS.txtFile;
                List<string> listACS = aCS.listString;
                string strTmpACS = string.Empty;
                foreach (string item in listACS)
                {
                    strTmpACS += item + "\r\n";
                }
                File.WriteAllText(filePath, strTmpACS);
            }
            catch { }
        }
        public static void saveAllACSs()
        {
            saveACSToTxtFile(tenThuMuc);

            saveACSToTxtFile(tenDoi);

            saveACSToTxtFile(vietTat);

            saveACSToTxtFile(giaiDau);
            saveACSToTxtFile(vongDau);

            saveACSToTxtFile(diaDiem);
            saveACSToTxtFile(san);
            saveACSToTxtFile(thoiGian);

            saveACSToTxtFile(binhLuanVien);
            saveACSToTxtFile(trongTai);

            saveACSToTxtFile(hlv);
        }
        public static void loadACSTxtFileToACSList(ACS aCS)
        {
            try
            {
                string filePath = aCS.txtFile;
                string content = string.Empty;
                List<string> list = aCS.listString;

                if (File.Exists(filePath)) content = File.ReadAllText(filePath);
                string[] linesArray = content.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                foreach (string line in linesArray)
                {
                    if (!list.Contains(line) && !string.IsNullOrWhiteSpace(line)) list.Add(line);
                }
            }
            catch { }
        }

        public static void loadAllACSs()
        {
            loadACSTxtFileToACSList(tenThuMuc);

            loadACSTxtFileToACSList(tenDoi);

            loadACSTxtFileToACSList(vietTat);

            loadACSTxtFileToACSList(giaiDau);
            loadACSTxtFileToACSList(vongDau);

            loadACSTxtFileToACSList(diaDiem);
            loadACSTxtFileToACSList(san);
            loadACSTxtFileToACSList(thoiGian);

            loadACSTxtFileToACSList(binhLuanVien);
            loadACSTxtFileToACSList(trongTai);

            loadACSTxtFileToACSList(hlv);
        }
    }
}
