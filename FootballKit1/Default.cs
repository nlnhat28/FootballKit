using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace FootballKit1
{
    class Default
    {
        public static string resourcesFolderPath = Application.StartupPath + "\\Resources";
        public static string appDataFolderPath = Application.StartupPath + "\\AppData";

        public static string czhFootballKitFolderPath
            = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\CZH\\FootballKit";

        public static string newAppDataFolderPath
            = czhFootballKitFolderPath + "\\AppData";

        public static readonly string appData = appDataFolderPath + "\\AppData.json";
        public static readonly string recentPaths = appDataFolderPath + "\\RecentPaths.txt";

        public static readonly string newAppData = newAppDataFolderPath + "\\AppData.json";
        public static readonly string newRecentPaths = newAppDataFolderPath + "\\RecentPaths.txt";

        public const string tenA = "Đội A";
        public const string tenB = "Đội B";

        public const string vietTatA = "Đội A";
        public const string vietTatB = "Đội B";

        public static readonly Color mauA = Color.Red;
        public static readonly Color mauB = Color.Blue;

        public static readonly Font fontRoboto16 = new Font("Roboto Condensed", 16, FontStyle.Bold);
        public static readonly Font fontRoboto12 = new Font("Roboto Condensed", 12, FontStyle.Regular);

        public static readonly Font fontNeutra14 = new Font("SVN-Neutraface 2", 14, FontStyle.Bold);
        public static readonly Font fontIcielCubano16 = new Font("iCielBC Cubano Normal", 16, FontStyle.Bold);

        public const string footballKit = "\\FootballKit.json";
        public const string penalties = "\\Penalties.json";
        public const string doiHinhImage = "\\DoiHinhImage.json";

        public const int timeInterval = 1000;

        public const string btnChay = "Chạy ▶";
        public const string btnTamDung = "Tạm dừng ❚❚";

        public const string tenGoiY = "LiveBongDa";

        public const int timeShowNotify = 750;

        public const string titleGhiBan = "Bàn thắng";
        public const string titlePhanLuoi = "Phản lưới";
        public const string titleTheVang = "Thẻ vàng";
        public const string titleTheDo = "Thẻ đỏ";
        public const string titleRaSan = "Ra sân";
        public const string titleVaoSan = "Vào sân";

        // Notify Icon
        public static Icon iconGhiBan = getIconFileInResources("Goal.ico");
        public static Icon iconTheVang = getIconFileInResources("YellowCard.ico");
        public static Icon iconTheDo = getIconFileInResources("RedCard.ico");
        public static Icon iconRaSan = getIconFileInResources("Out.ico");
        public static Icon iconVaoSan = getIconFileInResources("In.ico");
        public static Icon iconSuccess = getIconFileInResources("Success.ico");

        // Pen State Image
        public static Image imgVao = getImageFileInResources("Vao.png");
        public static Image imgTach = getImageFileInResources("Tach.png");
        public static Image imgReset = getImageFileInResources("Reset.png");
        public static List<int> listPenState = new List<int>(5) { 2, 2, 2, 2, 2 };
        // In Resources
        public static string imgPathVao = resourcesFolderPath + "\\Vao.png";
        public static string imgPathTach = resourcesFolderPath + "\\Tach.png";
        public static string imgPathReset = resourcesFolderPath + "\\Reset.png";

        // In Penalties Folder
        public const string imgA1 = "\\Penalties\\A1.png";
        public const string imgA2 = "\\Penalties\\A2.png";
        public const string imgA3 = "\\Penalties\\A3.png";
        public const string imgA4 = "\\Penalties\\A4.png";
        public const string imgA5 = "\\Penalties\\A5.png";
        public static List<string> listImgA = new List<string>(5) { imgA1, imgA2, imgA3, imgA4, imgA5 };

        public const string imgB1 = "\\Penalties\\B1.png";
        public const string imgB2 = "\\Penalties\\B2.png";
        public const string imgB3 = "\\Penalties\\B3.png";
        public const string imgB4 = "\\Penalties\\B4.png";
        public const string imgB5 = "\\Penalties\\B5.png";
        public static List<string> listImgB = new List<string>(5) { imgB1, imgB2, imgB3, imgB4, imgB5 };

        // Pen State ForeColor
        public static readonly Color colorVao = Color.Lime;
        public static readonly Color colorTach = Color.Red;
        public static readonly Color colorReset = Color.DarkGray;

        public PenFrame penFrame = new PenFrame()
        {
            listPenStateA = listPenState,
            listPenStateB = listPenState
        };

        // Ảnh đội hình
        public const string imageDoiHinhA = "\\DoiHinhA.png";
        public const string imageDoiHinhB = "\\DoiHinhB.png";

        // Ảnh winrate
        public const string coHoiChienThang = "\\CoHoiChienThang.png";

        // Thời tiết image
        public const string thoiTiet = "\\ThoiTiet.png";

        // Color checkbox
        public static readonly Color checkedColor = Color.White;
        public static readonly Color uncheckedColor = Color.LightGray;

        private static Icon getIconFileInResources(string fileName)
        {
            //string fullPath = Directory.GetParent(Application.StartupPath).FullName;
            //fullPath = Directory.GetParent(fullPath).FullName;
            //fullPath = string.Format("{0}\\Resources\\{1}", fullPath,fileName);
            //return fullPath;
            string path = string.Empty;
            path = Application.StartupPath;
            path = string.Format("{0}\\Resources\\{1}", path, fileName);
            Icon icon;
            if (File.Exists(path))
            {
                icon = new Icon(path);
                return icon;
            }
            else return null;
        }
        private static Image getImageFileInResources(string fileName)
        {
            //string fullPath = Directory.GetParent(Application.StartupPath).FullName;
            //fullPath = Directory.GetParent(fullPath).FullName;
            //fullPath = string.Format("{0}\\Resources\\{1}", fullPath,fileName);
            //return fullPath;
            string path = string.Empty;
            path = Application.StartupPath;
            path = string.Format("{0}\\Resources\\{1}", path, fileName);
            Image image;
            if (File.Exists(path))
            {
                image = Image.FromFile(path);
                return image;
            }
            else return null;
        }
    }
}

