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
using System.Diagnostics;

namespace FootballKit1
{
    public partial class fDoiHinh : Form
    {
        public string whatTeam { get; set; }
        public string name { get; set; }
        public Color color { get; set; }

        private Font font;

        private const string tagLbCauThu = "tagLbCauThu";
        private const string tagLbHLV = "tagLbHLV";
        private const string tagLbTenDoi = "tagLbTenDoi";
        private const string tagLbSoDo = "tagLbSoDo";
        private const int dist = 55;
        private const int deltaFontSize = 2;

        ContextMenuStrip contextMenuStrip1 = new ContextMenuStrip();
        ToolStripMenuItem changeForeColorMenuItem = new ToolStripMenuItem("Đổi màu CHỮ");
        ToolStripMenuItem changeBackColorMenuItem = new ToolStripMenuItem("Đổi màu NỀN");

        ColorDialog colorDialogGlobal = new ColorDialog();

        private Label localLbSoDo;
        //private class Distance
        //{
        //    public string title;
        //    public int dist;

        //    public Distance (string title, int dist)
        //    {
        //        this.title = title;
        //        this.dist = dist;
        //    }
        //}
        //private List<Distance> listDist = new List<Distance>()
        //{
        //    new Distance( "0.5 ô", 23),
        //    new Distance( "1 ô", 45),
        //    new Distance( "1.5 ô", 68),
        //    new Distance( "2 ô", 90),
        //    new Distance( "2.5 ô", 112),
        //    new Distance( "3 ô", 135),
        //};

        public fDoiHinh()
        {
            InitializeComponent();
        }
        public void fDoiHinh_Load()
        {
            //List<string> listTitle = new List<string>();
            //foreach (Distance dist in listDist)
            //{
            //    listTitle.Add(dist.title);
            //}
            //cbbDist.DataSource = listTitle;
            //cbbDist.SelectedIndex = 2;

            //fDoiHinh_Resize();

            //
            // Add the ToolStripMenuItems to the ContextMenuStrip
            contextMenuStrip1.Items.Add(changeBackColorMenuItem);
            contextMenuStrip1.Items.Add(changeForeColorMenuItem);
            // Attach the same event handlers to each ToolStripMenuItem
            changeForeColorMenuItem.Click += new EventHandler(changeForeColorMenuItem_Click);
            changeBackColorMenuItem.Click += new EventHandler(changeBackColorMenuItem_Click);

            font = AppDataController.appData.font;

            if (whatTeam == "A")
            {
                if (DoiHinhController.image.imageDoiA.backColor == Color.Ivory)
                {
                    DoiHinhController.image.imageDoiA.backColor = color;
                }
                if (DoiHinhController.image.imageDoiA.backColor == Color.White
                    || DoiHinhController.image.imageDoiA.backColor.Name == "ffffffff")
                    DoiHinhController.image.imageDoiA.foreColor = Color.Black;
                lbBackColor.BackColor = DoiHinhController.image.imageDoiA.backColor;
                lbForeColor.BackColor = DoiHinhController.image.imageDoiA.foreColor;

                initializeLabels(MatchController.doiHinhChinhA, DoiHinhController.image.imageDoiA);
                if (!string.IsNullOrWhiteSpace(MatchController.hlvA))
                {
                    initializeLabelHLV("HLV: " + MatchController.hlvA, DoiHinhController.image.imageDoiA.labelHLV);
                }
                else
                {
                    initializeLabelHLV(string.Empty, DoiHinhController.image.imageDoiA.labelHLV);
                }
                initializeLabelTenDoi("Đội hình chính " + name, DoiHinhController.image.imageDoiA.labelTenDoi);
                initializeLabelSoDo(DoiHinhController.image.imageDoiA.labelSoDo);
                txbSoDo.Text = DoiHinhController.image.imageDoiA.soDo;
            }
            else if ((whatTeam == "B"))
            {
                if (DoiHinhController.image.imageDoiB.backColor == Color.Ivory)
                {
                    DoiHinhController.image.imageDoiB.backColor = color;
                }
                if (DoiHinhController.image.imageDoiB.backColor == Color.White
                    || DoiHinhController.image.imageDoiA.backColor.Name == "ffffffff")
                    DoiHinhController.image.imageDoiB.foreColor = Color.Black;
                lbBackColor.BackColor = DoiHinhController.image.imageDoiB.backColor;
                lbForeColor.BackColor = DoiHinhController.image.imageDoiB.foreColor;

                initializeLabels(MatchController.doiHinhChinhB, DoiHinhController.image.imageDoiB);
                if (!string.IsNullOrWhiteSpace(MatchController.hlvB))
                {
                    initializeLabelHLV("HLV: " + MatchController.hlvB, DoiHinhController.image.imageDoiB.labelHLV);
                }
                else
                {
                    initializeLabelHLV(string.Empty, DoiHinhController.image.imageDoiB.labelHLV);
                }
                initializeLabelTenDoi("Đội hình chính " + name, DoiHinhController.image.imageDoiB.labelTenDoi);
                initializeLabelSoDo(DoiHinhController.image.imageDoiB.labelSoDo);
                txbSoDo.Text = DoiHinhController.image.imageDoiB.soDo;
            }
        }
        private void fDoiHinh_Load(object sender, EventArgs e)
        {
            fDoiHinh_Load();
        }
        //
        // Initial Labels
        //
        private void initializeLabels(List<string> listName, ImageDoiHinh imageDoi)
        {
            try
            {
                int x = 30;
                int y = 80;
                List<LabelDoiHinh> listDoiHinh = imageDoi.listLabelsCauThu;
                Color backColor = imageDoi.backColor;
                Color foreColor = imageDoi.foreColor;

                List<LabelDoiHinh> tmpListDoiHinh = new List<LabelDoiHinh>(listDoiHinh);
                List<Label> listLbRemain = new List<Label>();

                foreach (string name in listName)
                {
                    Label lb = new Label
                    {
                        AutoSize = true,
                        ForeColor = foreColor,
                        BackColor = backColor,
                        Font = font,
                        Padding = new Padding(3),
                        Tag = tagLbCauThu,
                        Cursor = Cursors.Hand,
                        ContextMenuStrip = contextMenuStrip1,
                        Text = name,
                        Location = new Point(x, y)
                    };
                    lb.LocationChanged += new EventHandler(label_LocationChanged);
                    ControlExtension.Draggable(lb, true);
                    y += 50;
                    listLbRemain.Add(lb);

                    foreach (LabelDoiHinh lbDH in tmpListDoiHinh)
                    {
                        if (name == lbDH.text)
                        {
                            lb.Text = name;
                            lb.Location = lbDH.location;
                            lb.ForeColor = lbDH.foreColor;
                            lb.BackColor = lbDH.backColor;
                            tmpListDoiHinh.Remove(lbDH);
                            listLbRemain.Remove(lb);
                            break;
                        }
                    }
                    ptbPitch.Controls.Add(lb);
                }
                int emptyRemain = Math.Min(listLbRemain.Count, tmpListDoiHinh.Count);
                for (int i = 0; i < emptyRemain; i++)
                {
                    listLbRemain[i].Location = tmpListDoiHinh[i].location;
                    listLbRemain[i].ForeColor = tmpListDoiHinh[i].foreColor;
                    listLbRemain[i].BackColor = tmpListDoiHinh[i].backColor;
                }
                ptbPitch.SendToBack();
            }
            catch { }
        }
        private void initializeLabelHLV(string hlv, LabelDoiHinh lbHLV)
        {
            Label lb = new Label
            {
                AutoSize = true,
                BackColor = Color.Transparent,
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                Text = hlv,
                Font = increaseFontSize(font),
                Tag = tagLbHLV,
                Location = lbHLV.location
            };
            ControlExtension.Draggable(lb, true);
            ptbPitch.Controls.Add(lb);
            ptbPitch.SendToBack();
        }
        private void initializeLabelTenDoi(string tenDoi, LabelDoiHinh lbTenDoi)
        {
            Label lb = new Label
            {
                AutoSize = true,
                BackColor = Color.Transparent,
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                Text = tenDoi,
                Font = increaseFontSize(font),
                Tag = tagLbTenDoi,
                Location = lbTenDoi.location
            };
            ControlExtension.Draggable(lb, true);
            ptbPitch.Controls.Add(lb);
            ptbPitch.SendToBack();


        }
        private void initializeLabelSoDo(LabelDoiHinh lbSoDo)
        {
            Label lb = new Label
            {
                AutoSize = true,
                BackColor = Color.Transparent,
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                Text = lbSoDo.text,
                Font = increaseFontSize(font),
                Tag = tagLbSoDo,
                Location = lbSoDo.location
            };
            ControlExtension.Draggable(lb, true);
            ptbPitch.Controls.Add(lb);
            localLbSoDo = lb;
            ptbPitch.SendToBack();
        }
        private void btnTaoAnh_Click(object sender, EventArgs e)
        {
            btnTaoAnh_Click();
        }
        private void btnTaoAnh_Click()
        {
            if (Directory.Exists(SaveController.folderPath))
            {
                try
                {
                    Bitmap bmp = new Bitmap(ptbPitch.Width, ptbPitch.Height);
                    //Graphics g = Graphics.FromImage(bmp);
                    ptbPitch.DrawToBitmap(bmp, ptbPitch.ClientRectangle);

                    foreach (Control control in ptbPitch.Controls)
                    {
                        try
                        {
                            Label label = (Label)control;
                            Point location = ptbPitch.PointToClient(label.Parent.PointToScreen(label.Location));
                            label.DrawToBitmap(bmp, new Rectangle(location, label.Size));
                        }
                        catch { }
                    }
                    string filePath = SaveController.folderPath + "\\DoiHinh" + whatTeam + ".png";
                    bmp.Save(filePath, ImageFormat.Png);
                    //showNotifySuccess("Đã lưu ảnh đội hình " + name);
                    DialogResult diaRes = MessageBox.Show("Đã lưu ảnh\nXem ảnh vừa lưu?",
                    "Xem ảnh", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (diaRes == DialogResult.Yes)
                    {
                        try
                        {
                            Process.Start(filePath);
                        }
                        catch
                        {
                            MessageBox.Show("Chưa thể xem ngay.\r\nBạn có thể vào đường dẫn " + filePath +
                                " để xem.",
                           "Lỗi: Không xem được ảnh", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch { }
            }
            else
            {
                MessageBox.Show("Chưa có thư mục để lưu ảnh.\r\nVui lòng tạo thư mục và lưu dự án trước.",
                    "Lỗi lưu ảnh", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void funcTaoAnh()
        {
            if (Directory.Exists(SaveController.folderPath))
            {
                try
                {
                    Bitmap bmp = new Bitmap(ptbPitch.Width, ptbPitch.Height);
                    //Graphics g = Graphics.FromImage(bmp);
                    ptbPitch.DrawToBitmap(bmp, ptbPitch.ClientRectangle);

                    foreach (Control control in ptbPitch.Controls)
                    {
                        try
                        {
                            Label label = (Label)control;
                            Point location = ptbPitch.PointToClient(label.Parent.PointToScreen(label.Location));
                            label.DrawToBitmap(bmp, new Rectangle(location, label.Size));
                        }
                        catch { }
                    }
                    string filePath = SaveController.folderPath + "\\DoiHinh" + whatTeam + ".png";
                    bmp.Save(filePath, ImageFormat.Png);                 
                }
                catch { }
            }
        }
        // Notify
        private void showNotifySuccess(string title)
        {
            if (Default.iconSuccess != null) FootballKit.Icon = Default.iconSuccess;
            FootballKit.ShowBalloonTip(Default.timeShowNotify, title, " ", ToolTipIcon.None);
        }
        
        //
        // Save Label
        //
        private void saveLabels(ImageDoiHinh image)
        {
            int x = 20;
            int y = 60;
            image.listLabelsCauThu.Clear();

            foreach (Control control in ptbPitch.Controls)
            {
                try
                {
                    Label lb = (Label)control;
                    if (lb.Location.X < 10 || lb.Location.Y < 10
                        || lb.Location.X > ptbPitch.Width - 10 || lb.Location.Y > ptbPitch.Height - 10 )
                    {
                        lb.Location = new Point(x, y);
                        y += 50;
                    }
                    switch (lb.Tag)
                    {
                        case tagLbCauThu:
                            image.listLabelsCauThu.Add(new LabelDoiHinh(lb.Text, lb.Location, lb.ForeColor, lb.BackColor));
                            break;
                        case tagLbHLV:
                            image.labelHLV = new LabelDoiHinh(lb.Text, lb.Location, lb.ForeColor, lb.BackColor);
                            break;
                        case tagLbTenDoi:
                            image.labelTenDoi = new LabelDoiHinh(lb.Text, lb.Location, lb.ForeColor, lb.BackColor);
                            break;
                        case tagLbSoDo:
                            image.labelSoDo = new LabelDoiHinh(lb.Text, lb.Location, lb.ForeColor, lb.BackColor);
                            break;
                        default:
                            break;
                    }
                }
                catch { }
            }
        }
        private void fDoiHinh_FormClosing(object sender, FormClosingEventArgs e)
        {
            fDoiHinh_FormClosing();
        }
        private void fDoiHinh_FormClosing()
        {
            saveImageDoiHinh();
            DoiHinhController.listfDoiHinhs.Remove(this);
        }
        public void saveImageDoiHinh()
        {
            if (whatTeam == "A")
            {
                saveLabels(DoiHinhController.image.imageDoiA);
                DoiHinhController.image.imageDoiA.foreColor = lbForeColor.BackColor;
                DoiHinhController.image.imageDoiA.backColor = lbBackColor.BackColor;
                DoiHinhController.image.imageDoiA.soDo = txbSoDo.Text;
            }
            else if ((whatTeam == "B"))
            {
                saveLabels(DoiHinhController.image.imageDoiB);
                DoiHinhController.image.imageDoiB.foreColor = lbForeColor.BackColor;
                DoiHinhController.image.imageDoiB.backColor = lbBackColor.BackColor;
                DoiHinhController.image.imageDoiB.soDo = txbSoDo.Text;
            }
            AppDataController.appData.font = font;
        }
        //
        // Change ForeColor
        // 
        // ForeColor
        private void lbForeColor_Click(object sender, EventArgs e)
        {
            lbForeColor_Click();
        }
        private void lbForeColor_Click()
        {
            colorDialogGlobal.Color = lbForeColor.BackColor ;

            if (colorDialogGlobal.ShowDialog() == DialogResult.OK)
            {
                lbForeColor.BackColor = colorDialogGlobal.Color;
                foreach (Control control in ptbPitch.Controls)
                {
                    try
                    {
                        Label lb = (Label)control;
                        if ((string)lb.Tag == tagLbCauThu) lb.ForeColor = lbForeColor.BackColor;
                    }
                    catch { }
                }
                MatchController.fMain.isSaved = false;
            }
        }
        // BackColor
        private void lbBackColor_Click(object sender, EventArgs e)
        {
            lbBackColor_Click();
        }
        private void lbBackColor_Click()
        {

            colorDialogGlobal.Color = lbBackColor.BackColor;

            if (colorDialogGlobal.ShowDialog() == DialogResult.OK)
            {
                lbBackColor.BackColor = colorDialogGlobal.Color;
                foreach (Control control in ptbPitch.Controls)
                {
                    try
                    {
                        Label lb = (Label)control;
                        if ((string)lb.Tag == tagLbCauThu) lb.BackColor = lbBackColor.BackColor;
                    }
                    catch { }
                }
                MatchController.fMain.isSaved = false;
            }        
        }
        //
        // Change font
        //
        // Increase font size
        private Font increaseFontSize(Font font)
        {
            Font newFont = new Font(font.FontFamily, font.Size + deltaFontSize, font.Style);
            return newFont;
        }
        private void btnFont_Click(object sender, EventArgs e)
        {
            btnFont_Click();
        }
        private void btnFont_Click()
        {
            try
            {
                FontDialog fontDialog = new FontDialog { Font = font };

                if (fontDialog.ShowDialog() == DialogResult.OK)
                {
                    AppDataController.appData.font = font = fontDialog.Font;
                    foreach (Control control in ptbPitch.Controls)
                    {
                        try
                        {
                            Label lb = (Label)control;
                            float oldWidth = lb.Size.Width;
                            float oldHeight = lb.Size.Height;

                            float oldX = lb.Location.X;
                            float oldY = lb.Location.Y;

                            if ((string)lb.Tag == tagLbCauThu)
                            {
                                lb.Font = font;
                            }
                            else
                            {
                                lb.Font = increaseFontSize(font);
                            }

                            float newWidth = lb.Size.Width;
                            float newHeight = lb.Size.Height;

                            int newX = Convert.ToInt32(oldX - (newWidth - oldWidth) / 2);
                            int newY = Convert.ToInt32(oldY - (newHeight - oldHeight) / 2);

                            lb.Location = new Point(newX, newY);

                        }
                        catch { }
                    }
                    MatchController.fMain.isSaved = false;
                }
            }
            catch { }
        }
        //
        // Sơ đồ chiến thuật
        //
        private void txbSoDo_TextChanged(object sender, EventArgs e)
        {
            txbSoDo_TextChanged();
        }
        private void txbSoDo_TextChanged()
        {
            switch (txbSoDo.Text.Length)
            {
                case 0:
                    localLbSoDo.Text = string.Empty;
                    break;
                case 1:
                    localLbSoDo.Text = "Sơ đồ: " + txbSoDo.Text;
                    break;
                default:
                    string soDo = "Sơ đồ: ";
                    foreach (char c in txbSoDo.Text)
                    {
                        soDo += c + "-";
                    }
                    soDo = soDo.Substring(0, soDo.Length - 1);
                    localLbSoDo.Text = soDo;
                    break;
            }
            MatchController.fMain.isSaved = false;
        }
        // Tự tạo sơ đồ
        private void btnAutoGenerateSoDo_Click(object sender, EventArgs e)
        {
            btnAutoGenerateSoDo_Click();
        }
        private void btnAutoGenerateSoDo_Click()
        {
            autoGenerateSoDo(dist);
            //autoGenerateSoDo(listDist[cbbDist.SelectedIndex].dist);
        }
        private void autoGenerateSoDo(int dist)
        {
            try
            {
                List<int> listY = new List<int>();
                List<int> countRow = new List<int>();

                foreach (Control control in ptbPitch.Controls)
                {
                    try
                    {
                        Label lb = (Label)control;
                        if ((string)lb.Tag == tagLbCauThu)
                        {
                            listY.Add(lb.Location.Y);
                        }
                    }
                    catch { }
                }
                listY.Sort();
                listY[listY.Count - 1] += dist;
                int count = 1;
                for (int i = 0; i < listY.Count - 1; i++)
                {
                    int currY = listY[i];
                    int nextY = listY[i + 1];
                    if (nextY - currY < dist) // Khoảng cách 2 label cạnh nhau < dist
                    {
                        count++;
                    }
                    else
                    {
                        countRow.Insert(0, count);
                        count = 1;
                    }
                }
                string soDo = string.Empty;
                foreach (int i in countRow)
                {
                    soDo += i.ToString();
                }
                txbSoDo.Text = soDo;
            }
            catch { }
        }

        //private void fDoiHinh_Resize(object sender, EventArgs e)
        //{
        //    fDoiHinh_Resize();
        //}
        private void fDoiHinh_Resize()
        {
            float oldSize = ptbPitch.Width;

            int minSize = Math.Min(Width + 120, Height);
            int newSizePtb = minSize - 240;

            ptbPitch.Size = new Size(new Point(newSizePtb, newSizePtb));
            int newX = (Width - ptbPitch.Width) / 2;
            ptbPitch.Location = new Point(newX, ptbPitch.Location.Y);
            float newSize = newSizePtb;

            float scale = newSize / oldSize;
            //if (scale > 1)
            //{
            //    scale *= 1.1f;
            //}
            //else
            //{
            //    scale *= 0.9f;
            //}
            if (scale != 1)
            {
                foreach (Control control in ptbPitch.Controls)
                {
                    try
                    {
                        Label lb = (Label)control;

                        //int newLbX = Convert.ToInt32(scale > 1 ? scale * lb.Location.X * 1.15 : scale * lb.Location.X * 0.85);
                        int newLbX = Convert.ToInt32(scale * lb.Location.X);
                        int newLbY = Convert.ToInt32(scale * lb.Location.Y);

                        lb.Location = new Point(newLbX, newLbY);
                    }
                    catch { }
                }
            }
        }
        //
        // Switch view
        //
        private void ptbSwitchView_Click(object sender, EventArgs e)
        {
            ptbSwitchView_Click();
        }

        private void ptbSwitchView_Click()
        {
            //float centerX = 342f;
            //float centerY = 308f;

            //float smallDeltaX = 194f;
            //float bigDeltaX = 282f;

            //float smallDeltaY = 165f;
            //float bigDeltaY = 237;
            try
            {
                float centerX = 342f;
                float centerY = 300f;

                float smallDeltaX = 194f;
                float bigDeltaX = 210f;

                float smallDeltaY = 165f;
                float bigDeltaY = 237;

                foreach (Control control in ptbPitch.Controls)
                {
                    try
                    {
                        Label lb = (Label)control;
                        if ((string)lb.Tag == tagLbCauThu)
                        {
                            float oldX = lb.Location.X;
                            float oldY = lb.Location.Y;
                            float width = lb.Size.Width;
                            float height = lb.Size.Height;

                            float oldMidX = oldX + width / 2;
                            float oldMidY = oldY + height / 2;

                            float newMidX;
                            float newMidY;

                            if (oldMidY > centerY)
                            {
                                newMidX = centerX - (oldMidX - centerX) / bigDeltaX * smallDeltaX;
                                newMidY = centerY - (oldMidY - centerY) / bigDeltaY * smallDeltaY;
                            }
                            else
                            {
                                newMidX = centerX - (oldMidX - centerX) / smallDeltaX * bigDeltaX;
                                newMidY = centerY - (oldMidY - centerY) / smallDeltaY * bigDeltaY;
                            }

                            int newX = Convert.ToInt32(newMidX - width / 2);
                            int newY = Convert.ToInt32(newMidY - height / 2);

                            lb.Location = new Point(newX, newY);
                        }
                    }
                    catch { }
                }
                MatchController.fMain.isSaved = false;
            }
            catch { }
        }
        //
        //  Change Color selected label
        //
        private void changeForeColorMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
                ContextMenuStrip contextMenu = (ContextMenuStrip)menuItem.Owner;
                Label label = (Label)contextMenu.SourceControl;
                colorDialogGlobal.Color = label.ForeColor;
                if (colorDialogGlobal.ShowDialog() == DialogResult.OK)
                {
                    label.ForeColor = colorDialogGlobal.Color;
                    MatchController.fMain.isSaved = false;
                }
            }
            catch { }
        }
        private void changeBackColorMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
                ContextMenuStrip contextMenu = (ContextMenuStrip)menuItem.Owner;
                Label label = (Label)contextMenu.SourceControl;
                colorDialogGlobal.Color = label.BackColor;
                if (colorDialogGlobal.ShowDialog() == DialogResult.OK)
                {
                    label.BackColor = colorDialogGlobal.Color;
                    MatchController.fMain.isSaved = false;
                }
            }
            catch { }
        }
        //
        // LocalChanged
        //
        private void label_LocationChanged(object sender, EventArgs e)
        {
            MatchController.fMain.isSaved = false;
        }
    }
}
