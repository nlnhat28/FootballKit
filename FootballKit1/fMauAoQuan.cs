using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FootballKit1
{
    public partial class fMauAoQuan : Form
    {
        public fMauAoQuan()
        {
            InitializeComponent();
        }
        private void btnLuuAnh_Click(object sender, EventArgs e)
        {
            btnLuuAnh_Click();
        }
        private void btnLuuAnh_Click()
        {
            //if (Directory.Exists(SaveController.folderPath))
            //{
            try
            {
                Bitmap bmp = new Bitmap(pnlMau.Width, pnlMau.Height);
                //Graphics g = Graphics.FromImage(bmp);
                pnlMau.DrawToBitmap(bmp, pnlMau.ClientRectangle);

                foreach (Control control in pnlMau.Controls)
                {
                    try
                    {
                        Label label = (Label)control;
                        Point location = pnlMau.PointToClient(label.Parent.PointToScreen(label.Location));
                        label.DrawToBitmap(bmp, new Rectangle(location, label.Size));
                    }
                    catch { }
                }
                string filePath = SaveController.folderPath + "\\MauAoQuan.png";// + whatTeam + ".png";
                bmp.Save(filePath, ImageFormat.Png);
                //showNotifySuccess("Đã lưu ảnh đội hình " + name);
                //DialogResult diaRes = MessageBox.Show("Xem ảnh vừa lưu?",
                //"Xem ảnh", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //if (diaRes == DialogResult.Yes)
                //{
                //    try
                //    {
                //        Process.Start(filePath);
                //    }
                //    catch
                //    {
                //        MessageBox.Show("Chưa thể xem ngay.\r\nBạn có thể vào đường dẫn " + filePath +
                //            " để xem.",
                //        "Lỗi: Không xem được ảnh", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    }
                //}
            }
            catch { }
        }
        //else
        //{
        //    MessageBox.Show("Chưa có thư mục để lưu ảnh.\r\nVui lòng tạo thư mục và lưu dự án trước.",
        //        "Lỗi lưu ảnh", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //}
    }
}

