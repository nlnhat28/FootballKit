using Microsoft.WindowsAPICodePack.Dialogs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Threading.Timer;


namespace FootballKit1
{
    public partial class fMain : Form
    {
        // Variables
        Timer timer;
        public AppData appData;
        public bool isSaved;
        private List<string> listRecentPaths = new List<string>();
        ContextMenuStrip contextMenuStripRecentPaths = new ContextMenuStrip();
        ContextMenuStrip contextMenuStripChonDuongDan = new ContextMenuStrip();
        //ContextMenuStrip contextMenuStripGoiYTen = new ContextMenuStrip();
        ColorDialog colorDialogGlobal = new ColorDialog() {FullOpen = true};
        private const int maxCountListRecentPaths = 20;

        double speed = 2;

        public fMain()
        {
            InitializeComponent();
        }
        //
        // Load Main Form
        //
        private void fMain_Load(object sender, EventArgs e)
        {
            fMainLoad();
        }
        private void fMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            formClosing(e);
        }
        private void fMainLoad()
        {
            MatchController.fMain = this;

            addMouseWheelEvents();

            // Load Combobox
            cbbFromTime.DataSource = ListDataSource.fromTime;
            cbbFromTime.SelectedIndex = 0;

            cbbToTime.DataSource = ListDataSource.toTime;
            cbbToTime.SelectedIndex = 5;

            cbbSpeed.DataSource = ListDataSource.speed;
            cbbSpeed.SelectedIndex = 3;

            cbbBuGio.DataSource = ListDataSource.overTime;
            cbbBuGio.SelectedIndex = 2;

            cbbHiepDau.DataSource = ListDataSource.periodName;
            cbbHiepDau.SelectedIndex = 1;

            // time
            btnChay.Text = Default.btnChay;

            // fMain Resize
            //fMain_Resize();

            // AppData
            checkNewAppDataFolder();
            loadNewAppDataFolderPath();
            updateNewAppData();

            // sth changes
            isSaved = true;
        }
        private void updateNewAppData()
        {
            //AppData
            appData = new AppData();
            try
            {
                string json = File.ReadAllText(Default.newAppData);
                appData = JsonConvert.DeserializeObject<AppData>(json);
                if (appData == null) appData = new AppData();

                ckbShowNotify1.Checked = appData.isShowNotify;
                appData.font = appData.font ?? Default.fontIcielCubano16;
                try
                {
                    accessFolderWhenStart(appData.lastFolderPath);
                }
                catch { };

                AppDataController.appData = appData;
            }   
            catch { };
              
            // Weather
            WeatherController.getListCitiesFromJson();

            // ACS  
            try
            {
                ACSList.loadAllACSs();
                setAllACSs();
            }
            catch { };

            // Recent Paths
            loadRecentPathsTxtFileToListRecentPaths();
            setupContextMenuStripRecentPath();
        }
        //
        // Create Folders in AppData
        //
        private void checkNewAppDataFolder()
        {
            // Create some folders AppData
            try
            {
                if (!Directory.Exists(Default.czhFootballKitFolderPath)) Directory.CreateDirectory(Default.czhFootballKitFolderPath);
                if (!Directory.Exists(Default.newAppDataFolderPath)) Directory.CreateDirectory(Default.newAppDataFolderPath);
                if (!Directory.Exists(ACSList.ACSFolderPath)) Directory.CreateDirectory(ACSList.ACSFolderPath);
                //{
                //    DialogResult diaRes = MessageBox.Show("Từ phiên bản 4.x, thư mục AppData sẽ được cố định trong Documents."
                //        + "\nVui lòng Copy thư mục AppData trong thư mục cài đặt của bản cũ."
                //        + " Rồi Paste vào thư mục FootballKit theo đường dẫn: "
                //        + Default.czhFootballKitFolderPath + "."
                //        + "\nSau đó khởi động lại chương trình!"
                //        + "\n\nNếu bạn chưa cài phiên bản nào hoặc không tìm thấy thư mục AppData cũ, " 
                //        + "hãy ấn \"OK\" để tạo mới hoàn toàn.",
                //        "Lưu ý: Thay đổi về thư mục AppData", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                //    if (diaRes == DialogResult.OK)
                //    {
                //        // Create new folders AppData
                //        try
                //        {
                //            if (!Directory.Exists(Default.newAppDataFolderPath)) Directory.CreateDirectory(Default.newAppDataFolderPath);
                //            if (!Directory.Exists(ACSList.ACSFolderPath)) Directory.CreateDirectory(ACSList.ACSFolderPath);

                //            MessageBox.Show("Đã tạo thư mục AppData trong Documents",
                //                "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        }
                //        catch { };
                //    }
                //    else
                //    {
                //        Application.Exit();
                //    }
                //}
            }
            catch { };
        }
        // Load Recent Paths from txt file
        private void loadRecentPathsTxtFileToListRecentPaths()
        {
            try
            {
                string filePath = Default.newRecentPaths;
                string content = string.Empty;
                List<string> list = listRecentPaths;

                if (File.Exists(filePath)) content = File.ReadAllText(filePath);
                string[] linesArray = content.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                foreach (string line in linesArray)
                {
                    if (!list.Contains(line) && !string.IsNullOrWhiteSpace(line))
                        list.Add(line);
                    if (list.Count > maxCountListRecentPaths)
                        break;
                }
            }
            catch { }
        }
        // Save Recent Paths to txt flie
        private void saveListRecentPathsToTxtFile()
        {
            try
            {
                string filePath = Default.newRecentPaths;
                List<string> list = listRecentPaths;
                string content = string.Empty;
                foreach (string item in listRecentPaths)
                {
                    content += item + "\r\n";
                }
                File.WriteAllText(filePath, content);
            }
            catch { }
        }
        private void setupContextMenuStripRecentPath()
        {
            try
            {
                contextMenuStripRecentPaths.Items.Clear();
                contextMenuStripChonDuongDan.Items.Clear();

                foreach (string path in listRecentPaths)
                {
                    try
                    {
                        ToolStripMenuItem menuItemRecentPaths = new ToolStripMenuItem();
                        menuItemRecentPaths.Text = path;    
                        menuItemRecentPaths.Click += new EventHandler(menuRecentPathsItem_Click);
                        menuItemRecentPaths.Font = Default.fontRoboto12;

                        ToolStripMenuItem menuItemChonDuongDan = new ToolStripMenuItem();
                        menuItemChonDuongDan.Text = path;
                        menuItemChonDuongDan.Click += new EventHandler(menuChonDuongDanItem_Click);
                        menuItemChonDuongDan.Font = Default.fontRoboto12;

                        contextMenuStripRecentPaths.Items.Add(menuItemRecentPaths);
                        contextMenuStripChonDuongDan.Items.Add(menuItemChonDuongDan);

                    }
                    catch { }
                }
                ptbChonTuMayTinh1.ContextMenuStrip = contextMenuStripRecentPaths;
                ptbChonDuongDan1.ContextMenuStrip = contextMenuStripChonDuongDan;
            }
            catch { };
        }
        private void menuRecentPathsItem_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
                string path = menuItem.Text;
                accessFolder(path);
            }
            catch { };
        }
        private void menuChonDuongDanItem_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
                string path = menuItem.Text;
                funcChonDuongDan(path);
            }
            catch { };
        }
        private void updateListRecentPathsAndCMS(string newPath)
        {
            try
            {
                if (listRecentPaths.Contains(newPath))
                {
                    listRecentPaths.Remove(newPath);
                }
                listRecentPaths.Insert(0, newPath);
                if (listRecentPaths.Count > maxCountListRecentPaths)
                {
                    listRecentPaths.RemoveAt(listRecentPaths.Count - 1);
                }
                setupContextMenuStripRecentPath();
            }
            catch { };
        }
        //
        // Form Closing
        //
        private void formClosing(FormClosingEventArgs e)
        {
            if (!isSaved)
            {
                DialogResult diaRes = MessageBox.Show("Lưu trước khi thoát?",
                        "Lưu", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (diaRes == DialogResult.Yes)
                {
                    try
                    {
                        saveProject();
                        if (isSaved)
                        {
                            try
                            {
                                saveAppDatas();
                            }
                            catch { e.Cancel = true; }
                        }
                        else e.Cancel = true;
                    }
                    catch
                    {
                        DialogResult diaRes2 = MessageBox.Show("Vẫn thoát mà không lưu?",
                        "Lỗi: Lưu trữ không thành công", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                        if (diaRes2 == DialogResult.Yes)
                        {
                            saveAppDatas();
                            e.Cancel = false;
                        }
                        else e.Cancel = true;
                    }
                }
                else if (diaRes == DialogResult.No)
                {
                    saveAppDatas();
                }
                else e.Cancel = true;
            }
            else
            {
                saveAppDatas();
                e.Cancel = false;
            }
        }
        private void saveAppDatas() //do it after exit application
        {
            try
            {
                // Create some folders AppData
                //try
                //{
                //    if (!Directory.Exists(Default.appDataFolderPath)) Directory.CreateDirectory(Default.appDataFolderPath);
                //    if (!Directory.Exists(ACSList.ACSFolderPath)) Directory.CreateDirectory(ACSList.ACSFolderPath);
                //}
                //catch { };
                saveFileAppData();
                ACSList.saveAllACSs();
                saveListRecentPathsToTxtFile();
            }
            catch { }
        }
        private void saveFileAppData()
        {
            try
            {
                appData.isShowNotify = ckbShowNotify1.Checked;
                string filePath = Default.newAppData;
                File.WriteAllText(filePath, JsonConvert.SerializeObject(appData, Formatting.Indented));
            }
            catch { }
        }
        //
        // Chạy thời gian ------------------------------------------------------------------------------------------
        //    
        // Check Validation Cbb Time
        private void cbbFromTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            cbbValidKeyPress(e);
        }

        private void cbbToTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            cbbValidKeyPress(e);
        }

        private void cbbBuGio_KeyPress(object sender, KeyPressEventArgs e)
        {
            cbbValidKeyPress(e);
        }
        private void cbbValidKeyPress(KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        // Time Values Changed
        private void cbbFromTime_TextChanged(object sender, EventArgs e)
        {
            cbbFromTimeTextChanged();
        }

        private void cbbToTime_TextChanged(object sender, EventArgs e)
        {
            //if (!string.IsNullOrWhiteSpace(cbbToTime.Text)) MatchController.toTime = Convert.ToInt32(cbbToTime.Text);
            //isSaved = false;
        }
        private void cbbFromTimeTextChanged()
        {
            if (!string.IsNullOrWhiteSpace(cbbFromTime.Text))
            {
                lbThoiGianChinhThuc.Text = string.Format("{0:00}:{1:00}", Convert.ToInt32(cbbFromTime.Text), 0);
            }
            isSaved = false;
        }
        private void cbbBuGio_TextChanged(object sender, EventArgs e)
        {
            timeBuGioChanged();
        }
        private void timeBuGioChanged()
        {
            lbSoPhutBuGio.Text = string.Format("+{0}", cbbBuGio.Text != string.Empty ? cbbBuGio.Text : "0");
            isSaved = false;
        }
        // Hàm chạy
        private void btnChay_MouseClick(object sender, MouseEventArgs e)
        {
            btnChayClick();
        }
        private void btnChay_Click(object sender, EventArgs e)
        {
            btnChayClick();
        }
        private void btnChayClick()
        {
            if (btnChay.Text == Default.btnChay)
            {
                if (cbbFromTime.Text == string.Empty || cbbToTime.Text == string.Empty)
                {
                    MessageBox.Show("Vui lòng không để trống thời gian.",
                        "Lỗi: Cài đặt thời gian", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (cbbFromTime.Text == cbbToTime.Text)
                    {
                        MessageBox.Show("Thời gian bắt đầu phải khác thời gian kết thúc.",
                            "Lỗi: Cài đặt thời gian", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        restartTimer();
                        cbbFromTime.Enabled = false;
                        btnChay.Text = Default.btnTamDung;
                    }
                }
            }
            else // TH timer đã hoạt động
            {
                timer.Dispose();
                btnChay.Text = Default.btnChay;
                ptbIsTimerAlive.Visible = true;
            }
        }
        private TimeSpan labelTextToTimeSpan(Label lb) //80:00
        {
            try
            {
                int minute = getMinuteFromLabel(lb);
                int second = getSecondFromLabel(lb);
                return new TimeSpan(0, minute, second);
            }
            catch { return new TimeSpan(0, Convert.ToInt32(cbbFromTime.Text), 0); }
        }
        private string TimeSpanToStringLabel(TimeSpan tp) //80:00
        {
            string time = "00:00";
            try
            {
                int hour = tp.Hours;
                int minute = tp.Minutes;
                int second = tp.Seconds;
                minute += hour * 60;
                time = string.Format("{0:00}:{1:00}", minute, second);
                return time;
            }
            catch { return time; }
        }
        private int getMinuteTimeSpan(TimeSpan tp)
        {
            try
            {
                int hour = tp.Hours;
                int minute = tp.Minutes;
                int second = tp.Seconds;
                minute += hour * 60;
                return minute;
            }
            catch
            {
                return 0;
            }
        }
        private void restartTimer()
        {
            MatchController.restartTime = labelTextToTimeSpan(lbThoiGianChinhThuc);
            MatchController.toTime = Convert.ToInt32(cbbToTime.Text);
            MatchController.startTime = DateTime.Now;
            runTimer();
        }
        private void runTimer()
        {
            if (getMinuteTimeSpan(MatchController.restartTime) < MatchController.toTime)
            {
                timer = new Timer(new TimerCallback(timerTickAscending), null, 0, Convert.ToInt32(Default.timeInterval / speed));
            }
            else
            {
                timer = new Timer(new TimerCallback(timerTickDescending), null, 0, Convert.ToInt32(Default.timeInterval / speed));
            }
            isSaved = false;
        }
        private void timerTickAscending(object state)
        {
            try
            {
                TimeSpan elapsedTime = DateTime.Now - MatchController.startTime;
                double newElapsedSeconds = elapsedTime.TotalSeconds * speed;
                TimeSpan officialTime = MatchController.restartTime + TimeSpan.FromSeconds(newElapsedSeconds);

                if (getMinuteTimeSpan(officialTime) < MatchController.toTime)
                {
                    Invoke((Action)(() =>
                    {
                        lbThoiGianChinhThuc.Text = TimeSpanToStringLabel(officialTime);
                    }));
                }
                else
                {
                    timer.Dispose();
                    Invoke((Action)(() =>
                    {
                        MatchController.restartTime = labelTextToTimeSpan(lbThoiGianBuGio);
                        lbThoiGianChinhThuc.Text = string.Format("{0:00}:{1:00}", MatchController.toTime, 0);
                        MatchController.startTime = DateTime.Now;
                        timer = new Timer(new TimerCallback(timerTickOverTime), null, 0, Convert.ToInt32(Default.timeInterval / speed));
                    }));
                }
                Invoke((Action)(() => { ptbIsTimerAlive.Visible = !ptbIsTimerAlive.Visible; }));
            }
            catch { }
        }
        private void timerTickDescending(object state)
        {
            try
            {
                TimeSpan elapsedTime = DateTime.Now - MatchController.startTime;
                double newElapsedSeconds = elapsedTime.TotalSeconds * speed;
                TimeSpan officialTime = MatchController.restartTime - TimeSpan.FromSeconds(newElapsedSeconds);

                if (getMinuteTimeSpan(officialTime) >= MatchController.toTime)
                {
                    Invoke((Action)(() =>
                    {
                        lbThoiGianChinhThuc.Text = TimeSpanToStringLabel(officialTime);
                    }));
                    if (getMinuteTimeSpan(officialTime) == MatchController.toTime
                        && officialTime.Seconds == 0)
                    {
                        timer.Dispose();
                        Invoke((Action)(() =>
                        {
                            MatchController.restartTime = labelTextToTimeSpan(lbThoiGianBuGio);
                            MatchController.startTime = DateTime.Now;
                            timer = new Timer(new TimerCallback(timerTickOverTime), null, 0, Convert.ToInt32(Default.timeInterval / speed));
                        }));
                    }
                }
                else
                {
                    timer.Dispose();
                    Invoke((Action)(() =>
                    {
                        MatchController.restartTime = labelTextToTimeSpan(lbThoiGianBuGio);
                        lbThoiGianChinhThuc.Text = string.Format("{0:00}:{1:00}", MatchController.toTime, 0);
                        MatchController.startTime = DateTime.Now;
                        timer = new Timer(new TimerCallback(timerTickOverTime), null, 0, Convert.ToInt32(Default.timeInterval / speed));
                    }));
                }
                Invoke((Action)(() => { ptbIsTimerAlive.Visible = !ptbIsTimerAlive.Visible; }));
            }
            catch { }
        }
        private void timerTickOverTime(object state)
        {
            try
            {
                TimeSpan elapsedTime = DateTime.Now - MatchController.startTime;
                double newElapsedSeconds = elapsedTime.TotalSeconds * speed;
                TimeSpan officialTime = MatchController.restartTime + TimeSpan.FromSeconds(newElapsedSeconds);

                Invoke((Action)(() =>
                {
                    lbThoiGianBuGio.Text = TimeSpanToStringLabel(officialTime);
                }));
                Invoke((Action)(() => { ptbIsTimerAlive.Visible = !ptbIsTimerAlive.Visible; }));
            }
            catch { }
        }
        // Reset time
        private void btnReset_MouseClick(object sender, MouseEventArgs e)
        {
            btnReset_Click();
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            btnReset_Click();
        }
        private void btnReset_Click()
        {
            DialogResult diaRes = MessageBox.Show("Reset thời gian?",
                        "Reset thời gian", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (diaRes == DialogResult.OK)
            {
                resetTime();
            }
        }
        private void resetTime()
        {
            try
            {
                timer.Dispose();
            }
            catch { };
            cbbFromTime.Enabled = cbbToTime.Enabled = true;
            ptbIsTimerAlive.Visible = false;
            btnChay.Text = Default.btnChay;
            lbThoiGianChinhThuc.Text = string.Format("{0:00}:{1:00}", Convert.ToInt32(cbbFromTime.Text), 0);
            lbThoiGianBuGio.Text = string.Format("{0:00}:{1:00}", 0, 0);
            isSaved = false;
        }
        // Khóa nút điều khiển
        private void ptbLock_Click(object sender, EventArgs e)
        {
            lockTimeControl(false);
        }
        private void ptbUnlock_Click(object sender, EventArgs e)
        {
            lockTimeControl(true);

        }
        private void lockTimeControl(bool Enabled)
        {
            cbbSpeed.Enabled = btnChay.Enabled = btnReset.Enabled = ptbUpdateTime.Enabled
                = tsmiChay.Enabled = tsmiResetTime.Enabled = !Enabled;
            ptbLock1.Visible = Enabled;
            ptbUnlock1.Visible = !Enabled;
            ptbUpdateTime.Visible = !Enabled;
            ptbUpdateTimeOff.Visible = Enabled;

            if (Enabled)
            {
                tmsiKhoa.Text = "Mở";
            }
            else
            {
                tmsiKhoa.Text = "Khoá";
            }
        }
        private void tmsiKhoa_Click(object sender, EventArgs e)
        {
            if (tmsiKhoa.Text == "Khoá")
            {
                lockTimeControl(true);
            }
            else
            {
                lockTimeControl(false);
            }
        }
        // btn Chạy TextChanged
        private void btnChay_TextChanged(object sender, EventArgs e)
        {
            tsmiChay.Text = btnChay.Text;
        }
        //
        // Update thời gian kết thúc
        //
        private void ptbUpdateToTime_Click(object sender, EventArgs e)
        {
            ptbUpdateToTimeClick();
        }
        private void ptbUpdateToTimeClick()
        {
            if (string.IsNullOrWhiteSpace(cbbToTime.Text) || string.IsNullOrWhiteSpace(cbbFromTime.Text))
            {
                MessageBox.Show("Vui lòng không trống thời gian.", "Lỗi: Thời gian", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int tmpFromTime = Convert.ToInt32(cbbFromTime.Text);
                int tmpToTime = Convert.ToInt32(cbbToTime.Text);

                if (tmpFromTime == tmpToTime)
                {
                    MessageBox.Show("Thời gian kết thúc phải khác thời gian bắt đầu.",
                            "Lỗi: Cập nhật thời gian", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    int minute = getMinuteFromLabel(lbThoiGianChinhThuc);
                    int second = getSecondFromLabel(lbThoiGianChinhThuc);

                    if (lbThoiGianBuGio.Text != "00:00")
                    {
                        MessageBox.Show("Thời gian chính đã kết thúc.",
                            "Lỗi: Cập nhật thời gian", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if ((minute >= tmpFromTime
                        && minute < tmpToTime) ||
                            (minute < tmpFromTime
                        && minute >= tmpToTime))
                        {
                            MatchController.toTime = tmpToTime;
                            MessageBox.Show("Đã cập nhật thời gian kết thúc đến phút " + MatchController.toTime,
                                "Cập nhật thời gian", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Thời gian hiện tại đã vượt qua thời gian bạn muốn cập nhật.",
                                "Lỗi: Cập nhật thời gian", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }
        //
        // Đội hình ------------------------------------------------------------------------------------------
        //
        // Các nút chức năng
        //Load
        private void loadDoiHinh(TextBox txb)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Load nội dung từ file txt";
                openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string filePath = openFileDialog.FileName;
                        txb.Text = File.ReadAllText(filePath);
                    }
                    catch
                    {
                        MessageBox.Show("File không tồn tại hoặc không đọc được nội dung file.",
                            "Lỗi: Load file txt", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    };
                }
            }
        }
        private void btnLoadChinhThucA_Click(object sender, EventArgs e)
        {
            loadDoiHinh(txbChinhThucA);
        }
        private void btnLoadDuBiA_Click(object sender, EventArgs e)
        {
            loadDoiHinh(txbDuBiA);
        }
        private void btnLoadChinhThucB_Click(object sender, EventArgs e)
        {
            loadDoiHinh(txbChinhThucB);
        }
        private void btnLoadDuBiB_Click(object sender, EventArgs e)
        {
            loadDoiHinh(txbDuBiB);
        }
        // Format + Sắp xếp
        private void formatDoiHinh(TextBox txb, Label lbSoLuong)
        {
            string formatedTxb = Regex.Replace(txb.Text, @"(\r\n)+", "\r\n\r\n",
                RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase);
            const string reduceMultiSpace = @"[ ]{2,}";
            formatedTxb = Regex.Replace(formatedTxb.Replace("\t", " "), reduceMultiSpace, " ");

            string[] arrCauThu = formatedTxb.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            List<string> listDoiHinh = new List<string>();
            foreach (string cauThu in arrCauThu)
            {
                if (cauThu.Trim() != string.Empty)
                {
                    listDoiHinh.Add(cauThu.Trim());
                }
            }
            lbSoLuong.Text = listDoiHinh.Count.ToString();
            if (listDoiHinh.Count > 0)
            {
                listDoiHinh.Sort();
                string tmpDoiHinh = string.Empty;
                foreach (string cauThu in listDoiHinh)
                {
                    tmpDoiHinh += cauThu + "\r\n";
                }
                txb.Text = tmpDoiHinh;
            }
        }
        private void ptbFormatChinhThucA_Click(object sender, EventArgs e)
        {
            formatDoiHinh(txbChinhThucA, lbSoLuongChinhThucA);
        }
        private void ptbFormatDuBiA_Click(object sender, EventArgs e)
        {
            formatDoiHinh(txbDuBiA, lbSoLuongDuBiA);
        }
        private void ptbFormatChinhThucB_Click(object sender, EventArgs e)
        {
            formatDoiHinh(txbChinhThucB, lbSoLuongChinhThucB);
        }
        private void ptbFormatDuBiB_Click(object sender, EventArgs e)
        {
            formatDoiHinh(txbDuBiB, lbSoLuongDuBiB);
        }
        // Xóa nội dung
        private void xoaDoiHinh(TextBox txb, Label lbSoLuong)
        {
            txb.Text = string.Empty;
            lbSoLuong.Text = "0";
        }
        private void ptbXoaChinhThucA_Click(object sender, EventArgs e)
        {
            xoaDoiHinh(txbChinhThucA, lbSoLuongChinhThucA);
            luuChinhThucA();
        }
        private void ptbXoaDuBiA_Click(object sender, EventArgs e)
        {
            xoaDoiHinh(txbDuBiA, lbSoLuongDuBiA);
            luuDuBiA();
        }
        private void ptbXoaChinhThucB_Click(object sender, EventArgs e)
        {
            xoaDoiHinh(txbChinhThucB, lbSoLuongChinhThucB);
            luuChinhThucB();
        }
        private void ptbXoaDuBiB_Click(object sender, EventArgs e)
        {
            xoaDoiHinh(txbDuBiB, lbSoLuongDuBiB);
            luuDuBiB();
        }
        // Lưu đội hình
        private List<string> luuDoiHinh(TextBox txbDoiHinh, Label lbSoLuong)
        {
            List<string> listDoiHinh = new List<string>();
            string formatedTxb = Regex.Replace(txbDoiHinh.Text, @"(\r\n)+", "\r\n\r\n",
                    RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase);
            const string reduceMultiSpace = @"[ ]{2,}";
            formatedTxb = Regex.Replace(formatedTxb.Replace("\t", " "), reduceMultiSpace, " ");

            string[] arrCauThu = formatedTxb.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            foreach (string cauThu in arrCauThu)
            {
                if (cauThu.Trim() != string.Empty)
                {
                    listDoiHinh.Add(cauThu.Trim());
                }
            }
            lbSoLuong.Text = listDoiHinh.Count.ToString();
            if (listDoiHinh.Count > 0)
            {
                string strTmpDoiHinh = string.Empty;
                foreach (string cauThu in listDoiHinh)
                {
                    strTmpDoiHinh += cauThu + "\r\n";
                }
                txbDoiHinh.Text = strTmpDoiHinh;
            }

            return listDoiHinh;
        }
        private List<string> luuDoiHinhChinh(TextBox txbChinhThuc, TextBox txbHLV, List<string> doiHinhDuBi, Label lbSoLuongChinhThuc,
            ComboBox cbbGhiBan, ComboBox cbbTheVang, ComboBox cbbTheDo, ComboBox cbbRaSan, string filePath)
        {

            List<string> doiHinh = new List<string>();
            doiHinh = luuDoiHinh(txbChinhThuc, lbSoLuongChinhThuc);
            List<string> listCbbGhiBan = new List<string>(doiHinh);
            cbbGhiBan.DataSource = listCbbGhiBan;
            List<string> listCbbRaSan = new List<string>(doiHinh);
            cbbRaSan.DataSource = listCbbRaSan;

            List<string> listCbbTheVang = new List<string>(doiHinh);
            List<string> listCbbTheDo = new List<string>(doiHinh);

            listCbbTheVang.AddRange(doiHinhDuBi);
            listCbbTheDo.AddRange(doiHinhDuBi);

            string hlv = txbHLV.Text.Trim();
            if (hlv != string.Empty)
            {
                listCbbTheVang.Add(hlv);
                listCbbTheDo.Add(hlv);
            }

            cbbTheVang.DataSource = listCbbTheVang;
            cbbTheDo.DataSource = listCbbTheDo;

            if (Directory.Exists(SaveController.folderPath))
            {
                File.WriteAllText(SaveController.folderPath + filePath, txbChinhThuc.Text);
            }
            return doiHinh;
        }
        private List<string> luuDoiHinhDuBi(TextBox txbDuBi, TextBox txbHLV, List<string> doiHinhChinh, Label lbSoLuongDuBi,
            ComboBox cbbTheVang, ComboBox cbbTheDo, ComboBox cbbVaoSan, string filePath)
        {
            List<string> doiHinh = new List<string>();
            doiHinh = luuDoiHinh(txbDuBi, lbSoLuongDuBi);

            List<string> listCbbTheVang = new List<string>();
            List<string> listCbbTheDo = new List<string>();

            listCbbTheVang.AddRange(doiHinhChinh);
            listCbbTheDo.AddRange(doiHinhChinh);

            listCbbTheVang.AddRange(doiHinh);
            listCbbTheDo.AddRange(doiHinh);

            string hlv = txbHLV.Text.Trim();
            if (hlv != string.Empty)
            {
                listCbbTheVang.Add(hlv);
                listCbbTheDo.Add(hlv);
            }
            cbbTheVang.DataSource = listCbbTheVang;
            cbbTheDo.DataSource = listCbbTheDo;

            List<string> listCbbVaoSan = new List<string>(doiHinh);
            cbbVaoSan.DataSource = listCbbVaoSan;

            if (Directory.Exists(SaveController.folderPath))
            {
                File.WriteAllText(SaveController.folderPath + filePath, txbDuBi.Text);
            }
            return doiHinh;
        }
        private string luuHLV(TextBox txbHLV, List<string> doiHinhChinh, List<string> doiHinhDuBi,
            ComboBox cbbTheVang, ComboBox cbbTheDo, string filePath)
        {
            List<string> listCbbTheVang = new List<string>();
            List<string> listCbbTheDo = new List<string>();

            listCbbTheVang.AddRange(doiHinhChinh);
            listCbbTheDo.AddRange(doiHinhChinh);
            listCbbTheVang.AddRange(doiHinhDuBi);
            listCbbTheDo.AddRange(doiHinhDuBi);

            string hlv = txbHLV.Text.Trim();
            if (hlv != string.Empty)
            {
                listCbbTheVang.Add(hlv);
                listCbbTheDo.Add(hlv);
            }

            cbbTheVang.DataSource = listCbbTheVang;
            cbbTheDo.DataSource = listCbbTheDo;

            if (Directory.Exists(SaveController.folderPath))
            {
                File.WriteAllText(SaveController.folderPath + filePath, hlv);
            }
            addACSFor2Textbox(ACSList.hlv, txbHLVA, txbHLVB);

            return hlv;
        }
        // Click
        // Đội hình
        private void ptbLuuChinhThucA_Click(object sender, EventArgs e)
        {
            ptbLuuChinhThucA_Click();
        }
        private void ptbLuuChinhThucA_Click()
        {
            luuChinhThucA();
            showNotifySuccess("Đã lưu đội hình chính " + grbDoiA.Text);
        }
        private void luuChinhThucA()
        {
            MatchController.doiHinhChinhA = luuDoiHinhChinh(txbChinhThucA, txbHLVA, MatchController.doiHinhDuBiA, lbSoLuongChinhThucA,
                cbbGhiBanA, cbbTheVangA, cbbTheDoA, cbbRaSanA, TxtFiles.doiHinhChinhThucA);

        }

        private void ptbLuuDuBiA_Click(object sender, EventArgs e)
        {
            ptbLuuDuBiA_Click();
        }
        private void ptbLuuDuBiA_Click()
        {
            luuDuBiA();
            showNotifySuccess("Đã lưu đội hình dự bị " + grbDoiA.Text);
        }
        private void luuDuBiA()
        {
            MatchController.doiHinhDuBiA = luuDoiHinhDuBi(txbDuBiA, txbHLVA, MatchController.doiHinhChinhA, lbSoLuongDuBiA,
                cbbTheVangA, cbbTheDoA, cbbVaoSanA, TxtFiles.doiHinhDuBiA);
        }

        private void ptbLuuChinhThucB_Click(object sender, EventArgs e)
        {
            ptbLuuChinhThucB();
        }
        private void ptbLuuChinhThucB()
        {
            luuChinhThucB();
            showNotifySuccess("Đã lưu đội hình chính " + grbDoiB.Text);
        }
        private void luuChinhThucB()
        {
            MatchController.doiHinhChinhB = luuDoiHinhChinh(txbChinhThucB, txbHLVB, MatchController.doiHinhDuBiB, lbSoLuongChinhThucB,
                cbbGhiBanB, cbbTheVangB, cbbTheDoB, cbbRaSanB, TxtFiles.doiHinhChinhThucB);

        }

        private void ptbLuuDuBiB_Click(object sender, EventArgs e)
        {
            ptbLuuDuBiB_Click();
        }
        private void ptbLuuDuBiB_Click()
        {
            luuDuBiB();
            showNotifySuccess("Đã lưu đội hình dự bị " + grbDoiB.Text);
        }
        private void luuDuBiB()
        {
            MatchController.doiHinhDuBiB = luuDoiHinhDuBi(txbDuBiB, txbHLVB, MatchController.doiHinhChinhB, lbSoLuongDuBiB,
                cbbTheVangB, cbbTheDoB, cbbVaoSanB, TxtFiles.doiHinhDuBiB);
        }
        // HLV
        private void ptbLuuHLVA_Click(object sender, EventArgs e)
        {
            ptbLuuHLVA_Click();
        }
        private void ptbLuuHLVA_Click()
        {
            luuHLVA();
            showNotifySuccess("Đã lưu huấn luyện viên " + grbDoiA.Text);
        }
        private void luuHLVA()
        {
            MatchController.hlvA = luuHLV(txbHLVA, MatchController.doiHinhChinhA, MatchController.doiHinhDuBiA,
                    cbbTheVangA, cbbTheDoA, TxtFiles.hlvA);
        }
        private void txbHLVAFocus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

                ptbLuuHLVA_Click();
            }
        }
        private void ptbLuuHLVB_Click(object sender, EventArgs e)
        {
            ptbLuuHLVB_Click();
        }
        private void ptbLuuHLVB_Click()
        {
            luuHLVB();
            showNotifySuccess("Đã lưu huấn luyện viên " + grbDoiB.Text);
        }
        private void luuHLVB()
        {
            MatchController.hlvB = luuHLV(txbHLVB, MatchController.doiHinhChinhB, MatchController.doiHinhDuBiB,
                    cbbTheVangB, cbbTheDoB, TxtFiles.hlvB);
        }
        private void txbHLVBFocus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

                ptbLuuHLVB_Click();
            }
        }
        // Text changed số lượng
        private void lbSoLuongChinhThucA_TextChanged(object sender, EventArgs e)
        {
            lbSoLuongChinhThucA2.Text = lbSoLuongChinhThucA.Text;
        }
        private void lbSoLuongDuBiA_TextChanged(object sender, EventArgs e)
        {
            lbSoLuongDuBiA2.Text = lbSoLuongDuBiA.Text;
        }
        private void lbSoLuongChinhThucB_TextChanged(object sender, EventArgs e)
        {
            lbSoLuongChinhThucB2.Text = lbSoLuongChinhThucB.Text;

        }
        private void lbSoLuongDuBiB_TextChanged(object sender, EventArgs e)
        {
            lbSoLuongDuBiB2.Text = lbSoLuongDuBiB.Text;
        }
        //
        // Context Menu Strip Đội hình
        // 
        // Đội hình A
        private void formatAllDoiHinhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formatAllDoiHinhToolStripMenuItemClick();
        }
        private void formatAllDoiHinhToolStripMenuItemClick()
        {
            formatDoiHinh(txbChinhThucA, lbSoLuongChinhThucA);
            formatDoiHinh(txbChinhThucB, lbSoLuongChinhThucB);
            formatDoiHinh(txbDuBiA, lbSoLuongDuBiA);
            formatDoiHinh(txbDuBiB, lbSoLuongDuBiB);
        }
        private void hoànThànhNhậpTấtCảToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hoànThànhNhậpTấtCảToolStripMenuItem_Click();
        }
        private void hoànThànhNhậpTấtCảToolStripMenuItem_Click()
        {
            hoanThanhNhapTatCaA();
            hoanThanhNhapTatCaB();
            showNotifySuccess("Đã lưu toàn bộ đội hình");
        }
        private void hoanThanhNhapTatCaA()
        {
            MatchController.doiHinhChinhA = luuDoiHinhChinh(txbChinhThucA, txbHLVA, MatchController.doiHinhDuBiA, lbSoLuongChinhThucA,
                cbbGhiBanA, cbbTheVangA, cbbTheDoA, cbbRaSanA, TxtFiles.doiHinhChinhThucA);

            MatchController.doiHinhDuBiA = luuDoiHinhDuBi(txbDuBiA, txbHLVA, MatchController.doiHinhChinhA, lbSoLuongDuBiA,
                cbbTheVangA, cbbTheDoA, cbbVaoSanA, TxtFiles.doiHinhDuBiA);

            MatchController.hlvA = luuHLV(txbHLVA, MatchController.doiHinhChinhA, MatchController.doiHinhDuBiA,
                        cbbTheVangA, cbbTheDoA, TxtFiles.hlvA);
        }
        private void xóaTấtCảÔNhậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            xoaTatCaONhapToolStripMenuItemClick();
        }
        private void xoaTatCaONhapToolStripMenuItemClick()
        {
            xoaDoiHinh(txbChinhThucA, lbSoLuongChinhThucA);
            xoaDoiHinh(txbChinhThucB, lbSoLuongChinhThucB);

            xoaDoiHinh(txbDuBiA, lbSoLuongDuBiA);
            xoaDoiHinh(txbDuBiB, lbSoLuongDuBiB);

            txbHLVA.Text = string.Empty;
            txbHLVB.Text = string.Empty;

            luuChinhThucA();
            luuChinhThucB();

            luuDuBiA();
            luuDuBiB();

            luuHLVA();
            luuHLVB();
        }
        // Đội hình B
        private void formatAllDoiHinhToolStripMenuItemB_Click(object sender, EventArgs e)
        {
            formatAllDoiHinhToolStripMenuItemBClick();
        }
        private void formatAllDoiHinhToolStripMenuItemBClick()
        {

        }
        private void hoànThànhNhậpTấtCảToolStripMenuItemB_Click(object sender, EventArgs e)
        {
            hoànThànhNhậpTấtCảToolStripMenuItemB_Click();
        }
        private void hoànThànhNhậpTấtCảToolStripMenuItemB_Click()
        {

        }
        private void hoanThanhNhapTatCaB()
        {
            MatchController.doiHinhChinhB = luuDoiHinhChinh(txbChinhThucB, txbHLVB, MatchController.doiHinhDuBiB, lbSoLuongChinhThucB,
                cbbGhiBanB, cbbTheVangB, cbbTheDoB, cbbRaSanB, TxtFiles.doiHinhChinhThucB);

            MatchController.doiHinhDuBiB = luuDoiHinhDuBi(txbDuBiB, txbHLVB, MatchController.doiHinhChinhB, lbSoLuongDuBiB,
                cbbTheVangB, cbbTheDoB, cbbVaoSanB, TxtFiles.doiHinhDuBiB);

            MatchController.hlvB = luuHLV(txbHLVB, MatchController.doiHinhChinhB, MatchController.doiHinhDuBiB,
                        cbbTheVangB, cbbTheDoB, TxtFiles.hlvB);
        }
        private void xóaTấtCảÔNhậpToolStripMenuItemB_Click(object sender, EventArgs e)
        {
            xoaTatCaONhapToolStripMenuItemBClick();
        }
        private void xoaTatCaONhapToolStripMenuItemBClick()
        {

        }
        //
        // Tạo ảnh đội hình chính
        //
        // Đội A
        private void ptbImageDoiHinhA_Click(object sender, EventArgs e)
        {
            luuChinhThucA();
            luuHLVA();
            isSaved = false;
            fDoiHinh f = new fDoiHinh();
            DoiHinhController.listfDoiHinhs.Add(f);
            f.whatTeam = "A";
            f.name = MatchController.tenA;
            f.color = ptbMauAoA.BackColor;
            f.Show();
        }
        // Đội B
        private void ptbImageDoiHinhB_Click(object sender, EventArgs e)
        {
            luuChinhThucB();
            luuHLVB();
            isSaved = false;
            fDoiHinh f = new fDoiHinh();
            DoiHinhController.listfDoiHinhs.Add(f);
            f.whatTeam = "B";
            f.name = MatchController.tenB;
            f.color = ptbMauAoB.BackColor;
            f.Show();
        }
        //
        //
        // Lưu trữ ===============================================================================================================
        //
        // Đồng bộ tên đội và màu
        private void btnDongBo_Click(object sender, EventArgs e)
        {
            btnDongBoClick();
        }
        private void btnDongBoClick()
        {

        }
        private void dongBoTen(TextBox txb, GroupBox grbDoi, Label lbLichSuGhiBan, Label lbLichSuTheVang, Label lbLichSuTheDo, string tenDefault)
        {
            txb.Text = formatName(txb.Text);
            if (txb.Text == string.Empty)
            {
                grbDoi.Text = lbLichSuGhiBan.Text = lbLichSuTheVang.Text = lbLichSuTheDo.Text = tenDefault;
            }
            else
            {
                grbDoi.Text = lbLichSuGhiBan.Text = lbLichSuTheVang.Text = lbLichSuTheDo.Text = txb.Text;
            }
        }
        private void dongBoVietTat(TextBox txb, Label lbTiSo, Label lbGhiBan, Label lbLuanLuu,
            Label lbTheVang, Label lbTheDo, Label lbRaSa, Label lbVaoSan,
            ToolStripItem tsiTiSo, ToolStripItem tsiLuanLuu,
            ToolStripItem tsiGhiBan, ToolStripItem tsiTheVang, ToolStripItem tsiTheDo,
            string tenDefault)
        {
            txb.Text = formatName(txb.Text);
            if (txb.Text == string.Empty)
            {
                lbTiSo.Text = lbGhiBan.Text = lbLuanLuu.Text
                    = lbTheVang.Text = lbTheDo.Text = lbRaSa.Text = lbVaoSan.Text = tenDefault;

                tsiTiSo.Text = "Reset tỉ số " + tenDefault;
                tsiLuanLuu.Text = "Reset luân lưu " + tenDefault;
                tsiGhiBan.Text = "Xóa lịch sử ghi bàn " + tenDefault;
                tsiTheVang.Text = "Xóa lịch sử thẻ vàng " + tenDefault;
                tsiTheDo.Text = "Xóa lịch sử thẻ đỏ " + tenDefault;
            }
            else
            {
                lbTiSo.Text = lbGhiBan.Text = lbLuanLuu.Text
                    = lbTheVang.Text = lbTheDo.Text = lbRaSa.Text = lbVaoSan.Text = txb.Text;
                tsiTiSo.Text = "Reset tỉ số " + txb.Text;
                tsiLuanLuu.Text = "Reset luân lưu " + txb.Text;
                tsiGhiBan.Text = "Xóa lịch sử ghi bàn " + txb.Text;
                tsiTheVang.Text = "Xóa lịch sử thẻ vàng " + txb.Text;
                tsiTheDo.Text = "Xóa lịch sử thẻ đỏ " + txb.Text;
            }
        }
        // Press Enter on Textbox. Focus next textbox
        private void txbFocus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // If the Enter key is pressed
            {
                e.Handled = true; // Stop the Enter key from making a "ding" sound
                e.SuppressKeyPress = true; // Stop the Enter key from creating a new line in the textbox

                SelectNextControl((Control)sender, true, true, true, true); // Select the next control on the form
            }
            else if (e.KeyCode == Keys.Left && string.IsNullOrEmpty(((TextBox)sender).Text))
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

                SelectNextControl((Control)sender, false, true, true, true);
            }
        }
        // Text changed Tên, viết tắt
        private void txbTenA_TextChanged(object sender, EventArgs e)
        {
            txbTenATextChanged();
        }
        private void txbTenATextChanged()
        {
            dongBoTen(txbTenA, grbDoiA, lbLichSuGhiBanA, lbLichSuTheVangA, lbLichSuTheDoA, Default.tenA);
            MatchController.tenA = grbDoiA.Text;
            if (Directory.Exists(SaveController.folderPath))
            {
                File.WriteAllText(SaveController.folderPath + TxtFiles.tenA, txbTenA.Text);
            }
            isSaved = false;
        }
        private void txbTenB_TextChanged(object sender, EventArgs e)
        {
            txbTenBTextChanged();
        }
        private void txbTenBTextChanged()
        {
            dongBoTen(txbTenB, grbDoiB, lbLichSuGhiBanB, lbLichSuTheVangB, lbLichSuTheDoB, Default.tenB);
            MatchController.tenB = grbDoiB.Text;
            if (Directory.Exists(SaveController.folderPath))
            {
                File.WriteAllText(SaveController.folderPath + TxtFiles.tenB, txbTenB.Text);
            }
            isSaved = false;
        }
        private void txbVietTatA_TextChanged(object sender, EventArgs e)
        {
            txbVietTatATextChanged();
        }
        private void txbVietTatATextChanged()
        {
            dongBoVietTat(txbVietTatA, lbTiSoA, lbGhiBanA, lbLuanLuuA, lbTheVangA, lbTheDoA, lbRaSanA, lbVaoSanA,
                resetTiSoDoiAToolStripMenuItem, resetLuanLuuDoiAToolStripMenuItem,
                xoaLichSuGhiBanA, xoaLichSuTheVangA, xoaLichSuTheDoA, Default.tenA);
            MatchController.vietTatA = lbTiSoA.Text;
            if (Directory.Exists(SaveController.folderPath))
            {
                File.WriteAllText(SaveController.folderPath + TxtFiles.vietTatA, txbVietTatA.Text);
            }
            isSaved = false;
        }
        private void txbVietTatB_TextChanged(object sender, EventArgs e)
        {
            txbVietTatBTextChanged();
        }
        private void txbVietTatBTextChanged()
        {
            dongBoVietTat(txbVietTatB, lbTiSoB, lbGhiBanB, lbLuanLuuB, lbTheVangB, lbTheDoB, lbRaSanB, lbVaoSanB,
                resetTiSoDoiBToolStripMenuItem, resetLuanLuuDoiBToolStripMenuItem,
                xoaLichSuGhiBanB, xoaLichSuTheVangB, xoaLichSuTheDoB, Default.tenB);
            MatchController.vietTatB = lbTiSoB.Text;
            if (Directory.Exists(SaveController.folderPath))
            {
                File.WriteAllText(SaveController.folderPath + TxtFiles.vietTatB, txbVietTatB.Text);
            }
            isSaved = false;
        }
        private string formatName(string input)
        {
            string formatedString = input;
            formatedString = Regex.Replace(formatedString, @"(\r\n)+", "\r\n\r\n",
                    RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase);
            const string reduceMultiSpace = @"[ ]{2,}";
            formatedString = Regex.Replace(formatedString.Replace("\t", " "), reduceMultiSpace, " ");
            formatedString = formatedString.Replace(Environment.NewLine, " ");
            return formatedString;
        }
        private void dongBoMau(PictureBox ptbColor, PictureBox ptbColorDoiHinh, Label lbColorTiSo, Label lbColorGhiBan, Label lbColorLuanLuu,
                Label lbColorTheVang, Label lbColorTheDo, Label lbColorRaSan, Label lbColorVaoSan,
                Label lbColorLichSuGhiBan, Label lbColorLichSuTheVang, Label lbColorLichSuTheDo)
        {
            ptbColorDoiHinh.BackColor = lbColorTiSo.BackColor = lbColorGhiBan.BackColor = lbColorLuanLuu.BackColor
                = lbColorTheVang.BackColor = lbColorTheDo.BackColor = lbColorRaSan.BackColor = lbColorVaoSan.BackColor
                = lbColorLichSuGhiBan.BackColor = lbColorLichSuTheVang.BackColor = lbColorLichSuTheDo.BackColor = ptbColor.BackColor;
        }
        private void ptbColorA_Click(object sender, EventArgs e)
        {
            ptbColorAClick();
        }
        private void ptbColorAClick()
        {
            colorDialogGlobal.Color = ptbMauAoA.BackColor;
            if (colorDialogGlobal.ShowDialog() == DialogResult.OK)
            {
                ptbMauAoA.BackColor = colorDialogGlobal.Color;
                dongBoMauA();
                taoAnhMauAoQuan(ptbMauAoA.BackColor, ptbMauQuanA.BackColor, "A");
                isSaved = false;
            };
        }
        private void dongBoMauA()
        {
            dongBoMau(ptbMauAoA, ptbColorDoiHinhA, lbColorTiSoA, lbColorGhiBanA, lbColorLuanLuuA,
                lbColorTheVangA, lbColorTheDoA, lbColorRaSanA, lbColorVaoSanA,
                lbColorLichSuGhiBanA, lbColorLichSuTheVangA, lbColorLichSuTheDoA);
            DoiHinhController.image.imageDoiA.changeBackColor(ptbMauAoA.BackColor);
            MatchController.mauA = DoiHinhController.image.imageDoiA.backColor = ptbMauAoA.BackColor;
            if (PensController.fPenalties != null) PensController.fPenalties.changeColorA();
        }
        private void ptbColorQuanA_Click(object sender, EventArgs e)
        {
            colorDialogGlobal.Color = ptbMauQuanA.BackColor;
            if (colorDialogGlobal.ShowDialog() == DialogResult.OK)
            {
                ptbMauQuanA.BackColor = colorDialogGlobal.Color;
                taoAnhMauAoQuan(ptbMauAoA.BackColor, ptbMauQuanA.BackColor, "A");
                isSaved = false;
            }
        }

        private void ptbColorB_Click(object sender, EventArgs e)
        {
            ptbColorBClick();
        }
        private void ptbColorBClick()
        {
            colorDialogGlobal.Color = ptbMauAoB.BackColor;
            if (colorDialogGlobal.ShowDialog() == DialogResult.OK)
            {
                ptbMauAoB.BackColor = colorDialogGlobal.Color;
                dongBoMauB();
                taoAnhMauAoQuan(ptbMauAoB.BackColor, ptbMauQuanB.BackColor, "B");
                isSaved = false;
            };
        }
        private void dongBoMauB()
        {
            dongBoMau(ptbMauAoB, ptbColorDoiHinhB, lbColorTiSoB, lbColorGhiBanB, lbColorLuanLuuB,
                lbColorTheVangB, lbColorTheDoB, lbColorRaSanB, lbColorVaoSanB,
                lbColorLichSuGhiBanB, lbColorLichSuTheVangB, lbColorLichSuTheDoB);
            DoiHinhController.image.imageDoiB.changeBackColor(ptbMauAoB.BackColor);
            MatchController.mauB = DoiHinhController.image.imageDoiB.backColor = ptbMauAoB.BackColor;
            if (PensController.fPenalties != null) PensController.fPenalties.changeColorB();
        }
        private void ptbColorQuanB_Click(object sender, EventArgs e)
        {
            colorDialogGlobal.Color = ptbMauQuanB.BackColor;
            if (colorDialogGlobal.ShowDialog() == DialogResult.OK)
            {
                ptbMauQuanB.BackColor = colorDialogGlobal.Color;
                taoAnhMauAoQuan(ptbMauAoB.BackColor, ptbMauQuanB.BackColor, "B");
                isSaved = false;
            }
        }
        //
        // Lưu trữ ========================================================================================
        //
        // Chọn đường dẫn
        private void btnChonDuongDan_Click(object sender, EventArgs e)
        {
            lbChonDuongDanClick();
        }
        private void lbChonDuongDanClick()
        {
            CommonOpenFileDialog cOFD = new CommonOpenFileDialog
            {
                Title = "Chọn nơi lưu thư mục lưu trữ",
                IsFolderPicker = true,
                Multiselect = false,
                RestoreDirectory = true
            };

            if (cOFD.ShowDialog() == CommonFileDialogResult.Ok)
            {
                funcChonDuongDan(cOFD.FileName);
            }
        }
        private void funcChonDuongDan(string path)
        {
            if (!ckbTachDuongDan.Checked)
            {
                SaveController.parentPath = path;
            }
            else
            {
                txbDuongDan.Text = SaveController.parentPath = Directory.GetParent(path).FullName;
                txbTenThuMuc.Text = SaveController.folderName = new DirectoryInfo(path).Name;
            }
            txbTenThuMucTextChanged();
        }
        private void ckbTach_CheckedChanged(object sender, EventArgs e)
        {
            ckbTach_CheckedChanged();
        }
        private void ckbTach_CheckedChanged()
        {
            ptbTachDuongDan.Visible = ckbTachDuongDan.Checked;
            ptbKoTachDuongDan.Visible = !ckbTachDuongDan.Checked;
        }
        // Tên thư mục thay đổi
        private void txbTenThuMuc_TextChanged(object sender, EventArgs e)
        {
            txbTenThuMucTextChanged();
        }
        private void txbTenThuMucTextChanged()
        {
            if (string.IsNullOrWhiteSpace(txbTenThuMuc.Text) && string.IsNullOrWhiteSpace(SaveController.parentPath))
            {
                txbDuongDan.Text = string.Empty;
            }
            else
            {
                txbDuongDan.Text = SaveController.folderPath = SaveController.parentPath + "\\" + txbTenThuMuc.Text.Trim();
            }
        }
        // Create valid path
        private string RemoveInvalidChar(string str)
        {
            string outputStr = string.Empty;
            string regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            Regex r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            outputStr = r.Replace(str, "");
            return outputStr;
        }
        private bool formatValidPath(string folderName, string parentPath, TextBox txbFolderName)
        {
            folderName = folderName.Trim();
            string folderPath = parentPath + "\\" + folderName;
            if (string.IsNullOrWhiteSpace(folderName))
            {
                MessageBox.Show("Vui lòng điền tên thư mục.",
                    "Lỗi: Tên thư mục", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                if (!isValidFilename(folderName))
                {
                    DialogResult diaRes = MessageBox.Show("Tên thư mục không được chứa các kí tự đặc biệt \\ / : * ? \" < > |\r\n\r\n" +
                        "Hãy đổi lại tên.\r\nHoặc chọn \"Yes\" để xóa các kí tự đặc biệt.",
                        "Lỗi: Tên thư mục", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if (diaRes == DialogResult.Yes)
                    {
                        txbFolderName.Text = folderName = RemoveInvalidChar(folderName).Trim();
                        folderPath = parentPath + "\\" + folderName;
                    }
                    return false;
                }
                if (string.IsNullOrWhiteSpace(parentPath))
                {
                    parentPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    SaveController.parentPath = parentPath;
                    folderPath = parentPath + "\\" + folderName;
                    txbDuongDan.Text = folderPath;
                    //txbDuongDan.SelectionStart = txbDuongDan.Text.Length;
                    MessageBox.Show("Đường dẫn để trống đã được đổi thành Desktop.\nẤn lưu lại 1 lần nữa để lưu.",
                        "Lưu ý: Thay đổi đường dẫn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                SaveController.folderName = folderName;
                SaveController.parentPath = parentPath;
                SaveController.folderPath = folderPath;
                txbDuongDan.Text = folderPath;
                //txbDuongDan.SelectionStart = txbDuongDan.Text.Length;
                return true;
            }
        }
        private bool isValidFilename(string folderName)
        {
            Regex containsABadCharacter = new Regex("["
                  + Regex.Escape(new string(Path.GetInvalidFileNameChars())) + "]");
            if (containsABadCharacter.IsMatch(folderName)) { return false; };

            // other checks for UNC, drive-path format, etc

            return true;
        }
        bool isValidPath(string pathText)
        {
            Regex containsABadCharacter = new Regex("["
                  + Regex.Escape(new string(Path.GetInvalidPathChars())) + "]");
            if (containsABadCharacter.IsMatch(pathText)) { return false; };

            // other checks for UNC, drive-path format, etc

            return true;
        }
        // Lưu
        private void btnLuu_Click(object sender, EventArgs e)
        {
            btnLuu_Click();
        }
        private void btnLuu_Click()
        {
            saveProject();
            if (btnChay.Text == Default.btnTamDung)
            {
                isSaved = false;
            }
        }
        private void saveProject() // (Will Show MessageBoxs)
        {
            if (formatValidPath(txbTenThuMuc.Text, SaveController.parentPath, txbTenThuMuc))
            {
                try
                {
                    if (!Directory.Exists(SaveController.folderPath)) Directory.CreateDirectory(SaveController.folderPath);
                    if (!Directory.Exists(SaveController.folderPath + "\\Penalties")) Directory.CreateDirectory(SaveController.folderPath + "\\Penalties");
                }
                catch { };
                if (Directory.Exists(SaveController.folderPath))
                {
                    funcSave();
                    updateListRecentPathsAndCMS(SaveController.folderPath);
                    MessageBox.Show("Đã lưu vào thư mục " + SaveController.folderPath,
                        "Lưu thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    isSaved = true;
                }
                else
                {
                    MessageBox.Show("Thư mục lưu trữ không tồn tại hoặc không hợp lệ.",
                        "Lỗi: Thư mục lưu trữ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    isSaved = false;
                }
            }
            else isSaved = false;
        }
        private void funcSave()
        {
            try
            {
                FootballKit kit = savedFootballKit();
                List<PenFrame> listPenFrame = new List<PenFrame>(PensController.listPenFrame);
                serializeObjectToJson(kit, SaveController.folderPath, Default.footballKit);
                serializeObjectToJson(listPenFrame, SaveController.folderPath, Default.penalties);

                foreach (fDoiHinh f in DoiHinhController.listfDoiHinhs)
                {
                    try
                    {
                        f.saveImageDoiHinh();
                    }
                    catch { }
                }
                //try
                //{
                //    luuChinhThucA();
                //    luuHLVA();
                //    fDoiHinh f = new fDoiHinh();
                //    f.name = grbDoiA.Text;
                //    f.color = ptbColorAoA.BackColor;
                //    f.whatTeam = "A";
                //    f.fDoiHinh_Load();
                //    f.funcTaoAnh();
                //    f.Dispose();
                //}
                //catch { }
                //try
                //{ 
                //    luuChinhThucB();
                //    luuHLVB();
                //    fDoiHinh f = new fDoiHinh();
                //    f.name = grbDoiB.Text;
                //    f.color = ptbColorAoB.BackColor;
                //    f.whatTeam = "B";
                //    f.fDoiHinh_Load();
                //    f.funcTaoAnh();
                //    f.Dispose();
                //}
                //catch { }
                serializeObjectToJson(DoiHinhController.image, SaveController.folderPath, Default.doiHinhImage);

                taoAnhMauAoQuan(ptbMauAoA.BackColor, ptbMauQuanA.BackColor, "A");
                taoAnhMauAoQuan(ptbMauAoB.BackColor, ptbMauQuanB.BackColor, "B");
                calcWinRate(false);

                try
                {
                    File.Copy(WeatherController.listWeatherImages[WeatherController.id].path, SaveController.folderPath + Default.thoiTiet, true);
                }
                catch { }

                TxtFiles.createTxtFiles(SaveController.folderPath);
                saveTxtFiles(SaveController.folderPath, kit);

                addAllACSs();

                appData.lastFolderPath = SaveController.folderPath;
            }
            catch { }
        }
        private string convertColorToHex(Color c)
        {
            return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }
        private FootballKit savedFootballKit()
        {
            try
            {
                FootballKit kit = new FootballKit()
                {
                    giaiDau = txbGiaiDau.Text,
                    vongDau = txbVongDau.Text,
                    diaDiem = txbDiaDiem.Text,
                    san = txbSan.Text,
                    thoiGian = txbThoiGian.Text,
                    idThoiTiet = WeatherController.id,

                    teamA = new Team()
                    {
                        ten = txbTenA.Text,
                        vietTat = txbVietTatA.Text,
                        mauAo = ptbMauAoA.BackColor,
                        mauQuan = ptbMauQuanA.BackColor,
                        tiSo = Convert.ToInt32(nudTiSoA.Value),
                        tiSoLuanLuu = Convert.ToInt32(nudLuanLuuA.Value),
                    },
                    teamB = new Team()
                    {
                        ten = txbTenB.Text,
                        vietTat = txbVietTatB.Text,
                        mauAo = ptbMauAoB.BackColor,
                        mauQuan = ptbMauQuanB.BackColor,
                        tiSo = Convert.ToInt32(nudTiSoB.Value),
                        tiSoLuanLuu = Convert.ToInt32(nudLuanLuuB.Value),
                    },

                    binhLuanVien = txbBinhLuanVien.Text,
                    trongTai = txbTrongTai.Text,

                    fromTime = Convert.ToInt32(cbbFromTime.Text),
                    toTime = MatchController.toTime,

                    thoiGianChinhThuc = lbThoiGianChinhThuc.Text,
                    thoiGianBuGio = lbThoiGianBuGio.Text,
                    soPhutBuGio = Convert.ToInt32(cbbBuGio.Text),

                    hiepDau = cbbHiepDau.Text
                };
                return kit;
            }
            catch
            {
                return null;
            }
        }
        private void serializeObjectToJson(object obj, string folderPath, string filePath)
        {
            try
            {
                string filePathJson = folderPath + filePath;
                if (Directory.Exists(folderPath))
                    File.WriteAllText(filePathJson, JsonConvert.SerializeObject(obj, Formatting.Indented));
            }
            catch { }
        }
        private string listDoiHinhToString(List<string> listDoiHinh)
        {
            string strDoiHinh = string.Empty;
            if (listDoiHinh != null && listDoiHinh.Count > 0)
            {
                foreach (string cauThu in listDoiHinh)
                {
                    strDoiHinh += cauThu + "\r\n";
                }
            }
            return strDoiHinh;
        }
        private void saveTxtFiles(string folderPath, FootballKit kit)
        {
            File.WriteAllText(folderPath + TxtFiles.tenA, kit.teamA.ten);
            File.WriteAllText(folderPath + TxtFiles.tenB, kit.teamB.ten);
            File.WriteAllText(folderPath + TxtFiles.vietTatA, kit.teamA.vietTat);
            File.WriteAllText(folderPath + TxtFiles.vietTatB, kit.teamB.vietTat);

            File.WriteAllText(folderPath + TxtFiles.giaiDau, kit.giaiDau);
            File.WriteAllText(folderPath + TxtFiles.vongDau, kit.vongDau);
            File.WriteAllText(folderPath + TxtFiles.diaDiem, kit.diaDiem);
            File.WriteAllText(folderPath + TxtFiles.san, kit.san);
            File.WriteAllText(folderPath + TxtFiles.thoiGian, kit.thoiGian);
            File.WriteAllText(folderPath + TxtFiles.thoiTiet, txbThoiTiet.Text);

            File.WriteAllText(folderPath + TxtFiles.binhLuanVien, kit.binhLuanVien);
            File.WriteAllText(folderPath + TxtFiles.trongTai, kit.trongTai);

            File.WriteAllText(folderPath + TxtFiles.doiHinhChinhThucA, listDoiHinhToString(MatchController.doiHinhChinhA));
            File.WriteAllText(folderPath + TxtFiles.doiHinhChinhThucB, listDoiHinhToString(MatchController.doiHinhChinhB));
            File.WriteAllText(folderPath + TxtFiles.doiHinhDuBiA, listDoiHinhToString(MatchController.doiHinhDuBiA));
            File.WriteAllText(folderPath + TxtFiles.doiHinhDuBiB, listDoiHinhToString(MatchController.doiHinhDuBiB));
            File.WriteAllText(folderPath + TxtFiles.doiHinhTheDoA, txbTheDoA.Text);
            File.WriteAllText(folderPath + TxtFiles.doiHinhTheDoB, txbTheDoB.Text);

            File.WriteAllText(folderPath + TxtFiles.hlvA, MatchController.hlvA);
            File.WriteAllText(folderPath + TxtFiles.hlvB, MatchController.hlvB);

            File.WriteAllText(folderPath + TxtFiles.lichSuGhiBanA, txbLichSuGhiBanA.Text);
            File.WriteAllText(folderPath + TxtFiles.lichSuGhiBanB, txbLichSuGhiBanB.Text);
            File.WriteAllText(folderPath + TxtFiles.lichSuTheVangA, txbLichSuTheVangA.Text);
            File.WriteAllText(folderPath + TxtFiles.lichSuTheVangB, txbLichSuTheVangB.Text);
            File.WriteAllText(folderPath + TxtFiles.lichSuTheDoA, txbLichSuTheDoA.Text);
            File.WriteAllText(folderPath + TxtFiles.lichSuTheDoB, txbLichSuTheDoB.Text);

            File.WriteAllText(folderPath + TxtFiles.officialTime, lbThoiGianChinhThuc.Text);
            File.WriteAllText(folderPath + TxtFiles.overTime, lbThoiGianBuGio.Text);

            File.WriteAllText(folderPath + TxtFiles.tiSoA, nudTiSoA.Value.ToString());
            File.WriteAllText(folderPath + TxtFiles.tiSoB, nudTiSoB.Value.ToString());

            File.WriteAllText(folderPath + TxtFiles.tiSoLuanLuuA, nudLuanLuuA.Value.ToString());
            File.WriteAllText(folderPath + TxtFiles.tiSoLuanLuuB, nudLuanLuuB.Value.ToString());

            File.WriteAllText(folderPath + TxtFiles.hiepDau, cbbHiepDau.Text);
            File.WriteAllText(folderPath + TxtFiles.soPhutBuGio, lbSoPhutBuGio.Text);
        }
        // Chọn từ máy
        private void btnChonTuMayTinh_Click(object sender, EventArgs e)
        {
            btnChonTuMayTinhClick();
        }
        private void btnChonTuMayTinhClick()
        {
            if (!isSaved)
            {
                DialogResult diaRes = MessageBox.Show("Lưu trước khi mở thư mục mới?",
                    "Lưu", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (diaRes == DialogResult.Yes)
                {
                    try
                    {
                        saveProject();
                        if (isSaved)
                        {
                            openAFolder();
                        }
                    }
                    catch
                    {
                        DialogResult diaRes2 = MessageBox.Show("Vẫn mở thư mục mới mà không lưu?",
                        "Lỗi: Lưu trữ không thành công", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                        if (diaRes2 == DialogResult.Yes)
                        {
                            openAFolder();
                        }
                    }
                }
                else if (diaRes == DialogResult.No)
                {
                    openAFolder();
                }
            }
            else
            {
                openAFolder();
            }
        }
        private void openAFolder()
        {
            CommonOpenFileDialog cOFD = new CommonOpenFileDialog
            {
                Title = "Chọn thư mục từ máy tính",
                IsFolderPicker = true,
                Multiselect = false,
                RestoreDirectory = true
            };

            if (cOFD.ShowDialog() == CommonFileDialogResult.Ok)
            {
                accessFolder(cOFD.FileName);
            }
        }

        private void accessFolder(string folderPath) // (Will Show MessageBoxs)
        {
            // Football Kit
            string footballkitFilePath = folderPath + Default.footballKit;
            if (File.Exists(footballkitFilePath))
            {
                FootballKit kit = deserializeJsonToFootballKitObject(footballkitFilePath);
                accessFolderCore(kit, folderPath);

                if (kit == null)
                {
                    MessageBox.Show("Một số dữ liệu không hợp lệ.",
                        "Lưu ý: Load dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("File dữ liệu không tồn tại hoặc không đọc được nội dung file.",
                    "Lỗi: Load dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void accessFolderWhenStart(string folderPath)
        {
            try
            {
                string footballkitFilePath = folderPath + Default.footballKit;
                if (File.Exists(footballkitFilePath))
                {
                    FootballKit kit = deserializeJsonToFootballKitObject(footballkitFilePath);
                    accessFolderCore(kit, folderPath);
                }
            }
            catch { }
        }
        private void accessFolderCore(FootballKit kit, string folderPath)
        {
            try
            {
                btnChay.Text = Default.btnChay;
                clearAllNoSave();

                SaveController.folderPath = folderPath;
                SaveController.parentPath = Directory.GetParent(SaveController.folderPath).FullName;
                SaveController.folderName = new DirectoryInfo(SaveController.folderPath).Name;

                txbTenThuMuc.Text = SaveController.folderName;
                txbTenThuMucTextChanged();

                try
                {
                    if (!Directory.Exists(SaveController.folderPath + "\\Penalties"))
                        Directory.CreateDirectory(SaveController.folderPath + "\\Penalties");
                }
                catch { };

                loadAllDoiHinhFromTxtFiles(folderPath);
                loadAllDoiHinhToListAndCbb();
                loadAllLichSuFromTxtFiles(folderPath);
                loadTxtFilesToTextBox(folderPath + TxtFiles.thoiTiet, txbThoiTiet); //Load thời tiết
                loadObjectToView(kit);

                string doiHinhFilePath = folderPath + Default.doiHinhImage;
                DoiHinhController.image = deserializeJsonToDoiHinh(doiHinhFilePath);
                calcWinRate(true);

                string penFilePath = folderPath + Default.penalties;
                if (File.Exists(penFilePath))
                {
                    try
                    {
                        List<PenFrame> listPF = deserializeJsonToListObject(penFilePath);
                        foreach (PenFrame pf in listPF)
                        {
                            pf.listPenStateA.RemoveRange(0, 5);
                            pf.listPenStateB.RemoveRange(0, 5);
                        }

                        if (listPF != null) PensController.listPenFrame = new List<PenFrame>(listPF);
                    }
                    catch { };
                }
                updateListRecentPathsAndCMS(folderPath);
                appData.lastFolderPath = SaveController.folderPath;
                isSaved = true;
            }
            catch { }
        }
        private FootballKit deserializeJsonToFootballKitObject(string filePath)
        {
            string json = string.Empty;
            try
            {
                json = File.ReadAllText(filePath);
                FootballKit kit = JsonConvert.DeserializeObject<FootballKit>(json);
                return kit;
            }
            catch
            {
                return null;
            };
        }
        private List<PenFrame> deserializeJsonToListObject(string filePath)
        {
            string json = string.Empty;
            try
            {
                json = File.ReadAllText(filePath);
                List<PenFrame> listPF = JsonConvert.DeserializeObject<List<PenFrame>>(json);
                return listPF;
            }
            catch
            {
                return null;
            };
        }
        private DoiHinhImage deserializeJsonToDoiHinh(string filePath)
        {
            string json = string.Empty;
            try
            {
                json = File.ReadAllText(filePath);
                DoiHinhImage image = JsonConvert.DeserializeObject<DoiHinhImage>(json);
                return image;
            }
            catch
            {
                return new DoiHinhImage();
            };
        }
        private void loadObjectToView(FootballKit kit)
        {
            try
            {
                txbGiaiDau.Text = kit.giaiDau;
                txbVongDau.Text = kit.vongDau;
                txbDiaDiem.Text = kit.diaDiem;
                txbSan.Text = kit.san;
                txbThoiGian.Text = kit.thoiGian;
                WeatherController.id = kit.idThoiTiet;

                txbTenA.Text = kit.teamA.ten;
                txbVietTatA.Text = kit.teamA.vietTat;
                if (string.IsNullOrWhiteSpace(MatchController.vietTatA)) MatchController.vietTatA = Default.vietTatA;
                try
                {
                    ptbMauAoA.BackColor = kit.teamA.mauAo;
                }
                catch
                {
                    ptbMauAoA.BackColor = Default.mauA;
                }
                try
                {
                    ptbMauQuanA.BackColor = kit.teamA.mauQuan;
                }
                catch
                {
                    ptbMauQuanA.BackColor = Default.mauA;
                };
                nudTiSoA.Value = kit.teamA.tiSo;
                nudLuanLuuA.Value = kit.teamA.tiSoLuanLuu;
                PensController.tiSoLuanLuuA = kit.teamA.tiSoLuanLuu;

                txbTenB.Text = kit.teamB.ten;
                txbVietTatB.Text = kit.teamB.vietTat;
                if (string.IsNullOrWhiteSpace(MatchController.vietTatB)) MatchController.vietTatB = Default.vietTatB;
                try
                {
                    ptbMauAoB.BackColor = kit.teamB.mauAo;
                }
                catch
                {
                    ptbMauAoB.BackColor = Default.mauB;
                }
                try
                {
                    ptbMauQuanB.BackColor = kit.teamB.mauQuan;
                }
                catch
                {
                    ptbMauQuanB.BackColor = Default.mauB;
                };

                dongBoMauA();
                dongBoMauB();

                nudTiSoB.Value = kit.teamB.tiSo;
                nudLuanLuuB.Value = kit.teamB.tiSoLuanLuu;
                PensController.tiSoLuanLuuB = kit.teamB.tiSoLuanLuu;

                txbBinhLuanVien.Text = kit.binhLuanVien;
                txbTrongTai.Text = kit.trongTai;

                cbbHiepDau.Text = kit.hiepDau;

                cbbFromTime.Text = kit.fromTime.ToString();
                cbbToTime.Text = kit.toTime.ToString();
                MatchController.toTime = kit.toTime;

                lbThoiGianChinhThuc.Text = kit.thoiGianChinhThuc;
                lbThoiGianBuGio.Text = kit.thoiGianBuGio;
                cbbBuGio.Text = kit.soPhutBuGio.ToString();

                formatValidTimer();
                //formatValidTime();

                //if (MatchController.isTimerAlive)
                //{
                //    cbbFromTime.Enabled = false;
                //    ptbIsTimerAlive.Visible = true;
                //}
                //else
                //{
                //    resetTimeOfMatchController();
                //    cbbFromTime.Enabled = cbbToTime.Enabled = true;
                //    ptbIsTimerAlive.Visible = false;
                //}

                //cbbFromTime.Text = MatchController.fromTime.ToString();
                //cbbToTime.Text = MatchController.toTime.ToString();
            }
            catch { }
        }
        private void formatValidTimer()
        {
            if (string.IsNullOrWhiteSpace(lbThoiGianChinhThuc.Text))
                lbThoiGianChinhThuc.Text = string.Format("{0:00}:{1:00}", Convert.ToInt32(cbbFromTime.Text), 0);
            if (string.IsNullOrWhiteSpace(lbThoiGianBuGio.Text)) lbThoiGianBuGio.Text = "00:00";
            if (cbbFromTime.Text == cbbToTime.Text)
            {
                MatchController.toTime = Convert.ToInt32(cbbToTime.Text) + 1;
                cbbToTime.Text = MatchController.toTime.ToString();
            }
        }
        //private void formatValidTime()
        //{
        //    if (MatchController.fromTime == MatchController.toTime)
        //    {
        //        MatchController.toTime++;
        //    }
        //    if (MatchController.fromTime < MatchController.toTime)
        //    {
        //        if (MatchController.currentOfficialMinute < MatchController.fromTime)
        //        {
        //            MatchController.currentOfficialMinute = MatchController.fromTime;
        //            MatchController.currentOfficialSecond = 0;
        //            MatchController.currentOverMinute = MatchController.currentOverSecond = 0;
        //        }
        //        else if (MatchController.currentOfficialMinute >= MatchController.toTime)
        //        {
        //            MatchController.currentOfficialMinute = MatchController.toTime;
        //            MatchController.currentOfficialSecond = 0;
        //        }
        //        else
        //        {
        //            MatchController.currentOverMinute = MatchController.currentOverSecond = 0;
        //        }
        //    }
        //    else if (MatchController.fromTime > MatchController.toTime)
        //    {
        //        if (MatchController.currentOfficialMinute >= MatchController.fromTime)
        //        {
        //            MatchController.currentOfficialMinute = MatchController.fromTime;
        //            MatchController.currentOfficialSecond = 0;
        //            MatchController.currentOverMinute = MatchController.currentOverSecond = 0;
        //        }
        //        else if (MatchController.currentOfficialMinute < MatchController.toTime)
        //        {
        //            MatchController.currentOfficialMinute = MatchController.toTime;
        //            MatchController.currentOfficialSecond = 0;
        //        }
        //        else
        //        {
        //            MatchController.currentOverMinute = MatchController.currentOverSecond = 0;
        //        }
        //    }
        //}
        private void loadTxtFilesToTextBox(string filePath, TextBox txb)
        {
            try
            {
                if (File.Exists(filePath)) txb.Text = File.ReadAllText(filePath);
                else txb.Text = string.Empty;
            }
            catch { }
        }
        private void loadAllDoiHinhFromTxtFiles(string folderPath)
        {
            loadTxtFilesToTextBox(folderPath + TxtFiles.doiHinhChinhThucA, txbChinhThucA);
            loadTxtFilesToTextBox(folderPath + TxtFiles.doiHinhChinhThucB, txbChinhThucB);
            loadTxtFilesToTextBox(folderPath + TxtFiles.doiHinhDuBiA, txbDuBiA);
            loadTxtFilesToTextBox(folderPath + TxtFiles.doiHinhDuBiB, txbDuBiB);
            loadTxtFilesToTextBox(folderPath + TxtFiles.doiHinhTheDoA, txbTheDoA);
            loadTxtFilesToTextBox(folderPath + TxtFiles.doiHinhTheDoB, txbTheDoB);
            loadTxtFilesToTextBox(folderPath + TxtFiles.hlvA, txbHLVA);
            loadTxtFilesToTextBox(folderPath + TxtFiles.hlvB, txbHLVB);
        }
        private void loadAllDoiHinhToListAndCbb()
        {
            MatchController.doiHinhChinhA = luuDoiHinhChinh(txbChinhThucA, txbHLVA, MatchController.doiHinhDuBiA, lbSoLuongChinhThucA,
                    cbbGhiBanA, cbbTheVangA, cbbTheDoA, cbbRaSanA, TxtFiles.doiHinhChinhThucA);
            MatchController.doiHinhDuBiA = luuDoiHinhDuBi(txbDuBiA, txbHLVA, MatchController.doiHinhChinhA, lbSoLuongDuBiA,
                cbbTheVangA, cbbTheDoA, cbbVaoSanA, TxtFiles.doiHinhDuBiA);
            MatchController.hlvA = luuHLV(txbHLVA, MatchController.doiHinhChinhA, MatchController.doiHinhDuBiA,
                        cbbTheVangA, cbbTheDoA, TxtFiles.hlvA);


            MatchController.doiHinhChinhB = luuDoiHinhChinh(txbChinhThucB, txbHLVB, MatchController.doiHinhDuBiB, lbSoLuongChinhThucB,
                    cbbGhiBanB, cbbTheVangB, cbbTheDoB, cbbRaSanB, TxtFiles.doiHinhChinhThucB);
            MatchController.doiHinhDuBiB = luuDoiHinhDuBi(txbDuBiB, txbHLVB, MatchController.doiHinhChinhB, lbSoLuongDuBiB,
                cbbTheVangB, cbbTheDoB, cbbVaoSanB, TxtFiles.doiHinhDuBiB);
            MatchController.hlvB = luuHLV(txbHLVB, MatchController.doiHinhChinhB, MatchController.doiHinhDuBiB,
                        cbbTheVangB, cbbTheDoB, TxtFiles.hlvB);


        }
        private void loadAllLichSuFromTxtFiles(string folderPath)
        {
            loadTxtFilesToTextBox(folderPath + TxtFiles.lichSuGhiBanA, txbLichSuGhiBanA);
            loadTxtFilesToTextBox(folderPath + TxtFiles.lichSuGhiBanB, txbLichSuGhiBanB);
            loadTxtFilesToTextBox(folderPath + TxtFiles.lichSuTheVangA, txbLichSuTheVangA);
            loadTxtFilesToTextBox(folderPath + TxtFiles.lichSuTheVangB, txbLichSuTheVangB);
            loadTxtFilesToTextBox(folderPath + TxtFiles.lichSuTheDoA, txbLichSuTheDoA);
            loadTxtFilesToTextBox(folderPath + TxtFiles.lichSuTheDoB, txbLichSuTheDoB);

        }
        // Xóa lịch sử
        // Ghi bàn
        private void xoaLichSuGhiBanA_Click(object sender, EventArgs e)
        {
            txbLichSuGhiBanA.Text = string.Empty;
        }
        private void xoaLichSuGhiBanB_Click(object sender, EventArgs e)
        {
            txbLichSuGhiBanB.Text = string.Empty;
        }
        private void xoaLichSuGhiBan2Doi_Click(object sender, EventArgs e)
        {
            txbLichSuGhiBanA.Text = txbLichSuGhiBanB.Text = string.Empty;
        }
        // Thẻ vàng
        private void xoaLichSuTheVangA_Click(object sender, EventArgs e)
        {
            txbLichSuTheVangA.Text = string.Empty;
        }

        private void xoaLichSuTheVangB_Click(object sender, EventArgs e)
        {
            txbLichSuTheVangB.Text = string.Empty;
        }

        private void xoaLichSuTheVang2Doi_Click(object sender, EventArgs e)
        {
            txbLichSuTheVangA.Text = txbLichSuTheVangB.Text = string.Empty;
        }
        // Thẻ đỏ
        private void xoaLichSuTheDoA_Click(object sender, EventArgs e)
        {
            txbLichSuTheDoA.Text = string.Empty;
        }
        private void xoaLichSuTheDoB_Click(object sender, EventArgs e)
        {
            txbLichSuTheDoB.Text = string.Empty;
        }
        private void xoaLichSuTheDo2Doi_Click(object sender, EventArgs e)
        {
            txbLichSuTheDoA.Text = txbLichSuTheDoB.Text = string.Empty;
        }
        // Xóa toàn bộ
        private void xoaToanBoLichSu2_Click(object sender, EventArgs e)
        {
            xoaToanBoLichSu();
        }
        private void xoaToanBoLichSu3_Click(object sender, EventArgs e)
        {
            xoaToanBoLichSu();
        }
        private void xoaToanBoLichSu1_Click(object sender, EventArgs e)
        {
            xoaToanBoLichSu();
        }
        private void xoaToanBoLichSu()
        {
            txbLichSuGhiBanA.Text = txbLichSuGhiBanB.Text = string.Empty;
            txbLichSuTheVangA.Text = txbLichSuTheVangB.Text = string.Empty;
            txbLichSuTheDoA.Text = txbLichSuTheDoB.Text = string.Empty;
        }
        //
        // Match Controller =================================================================================================
        //
        // Thời gian
        private void lbThoiGianChinhThuc_TextChanged(object sender, EventArgs e)
        {
            if (Directory.Exists(SaveController.folderPath))
            {
                File.WriteAllText(SaveController.folderPath + TxtFiles.officialTime, lbThoiGianChinhThuc.Text);
            }
        }
        private void lbThoiGianBuGio_TextChanged(object sender, EventArgs e)
        {
            if (Directory.Exists(SaveController.folderPath))
            {
                File.WriteAllText(SaveController.folderPath + TxtFiles.overTime, lbThoiGianBuGio.Text);
            }
        }
        private void lbSoPhutBuGio_TextChanged(object sender, EventArgs e)
        {
            if (Directory.Exists(SaveController.folderPath))
            {
                File.WriteAllText(SaveController.folderPath + TxtFiles.soPhutBuGio, lbSoPhutBuGio.Text);
            }
        }
        private void cbbHiepDau_TextChanged(object sender, EventArgs e)
        {
            if (Directory.Exists(SaveController.folderPath))
            {
                File.WriteAllText(SaveController.folderPath + TxtFiles.hiepDau, cbbHiepDau.Text);
            }

            isSaved = false;
        }
        //
        // Chọn cầu thủ ========================================================================================================
        //
        // Ra/Vào Sân
        private void renderListDoiHinhToTextBox(List<string> doiHinh, TextBox txb, string filePath)
        {
            string strDoiHinh = string.Empty;
            foreach (string cauThu in doiHinh)
            {
                strDoiHinh += cauThu + "\r\n";
            }
            txb.Text = strDoiHinh;
            if (Directory.Exists(SaveController.folderPath))
            {
                File.WriteAllText(SaveController.folderPath + filePath, txb.Text);
            }
        }
        private void btnRaSanClick(ComboBox cbbRaSan, ComboBox cbbGhiBan, ComboBox cbbTheVang, ComboBox cbbTheDo, ComboBox cbbVaoSan,
            List<string> doiHinhChinh, List<string> doiHinhDuBi, string hlv,
            Label lbSoLuongChinhThuc, Label lbSoLuongDuBi,
            TextBox txbChinhThuc, TextBox txbDuBi, string filePathDoiHinhChinh, string filePathDoiHinhDuBi)
        {
            if (doiHinhChinh != null && doiHinhChinh.Count > 0)
            {
                showNotify(Default.titleRaSan, cbbRaSan.Text, Default.iconRaSan, 1);

                int id = cbbRaSan.SelectedIndex;
                doiHinhDuBi.Add(doiHinhChinh[id]);
                doiHinhChinh.RemoveAt(id);

                List<string> listCbbGhiBan = new List<string>(doiHinhChinh);
                cbbGhiBan.DataSource = listCbbGhiBan;
                List<string> listCbbRaSan = new List<string>(doiHinhChinh);
                cbbRaSan.DataSource = listCbbRaSan;
                List<string> listCbbVaoSan = new List<string>(doiHinhDuBi);
                cbbVaoSan.DataSource = listCbbVaoSan;

                List<string> listCbbTheVang = new List<string>(doiHinhChinh);
                List<string> listCbbTheDo = new List<string>(doiHinhChinh);

                listCbbTheVang.AddRange(doiHinhDuBi);
                listCbbTheDo.AddRange(doiHinhDuBi);
                if (hlv != string.Empty)
                {
                    listCbbTheVang.Add(hlv);
                    listCbbTheDo.Add(hlv);
                }
                cbbTheVang.DataSource = listCbbTheVang;
                cbbTheDo.DataSource = listCbbTheDo;

                cbbVaoSan.SelectedIndex = listCbbVaoSan.Count - 1;

                lbSoLuongChinhThuc.Text = doiHinhChinh.Count.ToString();
                lbSoLuongDuBi.Text = doiHinhDuBi.Count.ToString();

                renderListDoiHinhToTextBox(doiHinhChinh, txbChinhThuc, filePathDoiHinhChinh);
                renderListDoiHinhToTextBox(doiHinhDuBi, txbDuBi, filePathDoiHinhDuBi);

            }
        }
        private void btnVaoSanClick(ComboBox cbbRaSan, ComboBox cbbGhiBan, ComboBox cbbTheVang, ComboBox cbbTheDo, ComboBox cbbVaoSan,
            List<string> doiHinhChinh, List<string> doiHinhDuBi, string hlv,
            Label lbSoLuongChinhThuc, Label lbSoLuongDuBi,
            TextBox txbChinhThuc, TextBox txbDuBi, string filePathDoiHinhChinh, string filePathDoiHinhDuBi)
        {
            if (doiHinhDuBi != null && doiHinhDuBi.Count > 0)
            {
                showNotify(Default.titleVaoSan, cbbVaoSan.Text, Default.iconVaoSan, 1);

                int id = cbbVaoSan.SelectedIndex;
                doiHinhChinh.Add(doiHinhDuBi[id]);
                doiHinhDuBi.RemoveAt(id);

                List<string> listCbbGhiBan = new List<string>(doiHinhChinh);
                cbbGhiBan.DataSource = listCbbGhiBan;
                List<string> listCbbRaSan = new List<string>(doiHinhChinh);
                cbbRaSan.DataSource = listCbbRaSan;
                List<string> listCbbVaoSan = new List<string>(doiHinhDuBi);
                cbbVaoSan.DataSource = listCbbVaoSan;

                List<string> listCbbTheVang = new List<string>(doiHinhChinh);
                List<string> listCbbTheDo = new List<string>(doiHinhChinh);

                listCbbTheVang.AddRange(doiHinhDuBi);
                listCbbTheDo.AddRange(doiHinhDuBi);
                if (hlv != string.Empty)
                {
                    listCbbTheVang.Add(hlv);
                    listCbbTheDo.Add(hlv);
                }
                cbbTheVang.DataSource = listCbbTheVang;
                cbbTheDo.DataSource = listCbbTheDo;


                cbbRaSan.SelectedIndex = listCbbRaSan.Count - 1;

                lbSoLuongChinhThuc.Text = doiHinhChinh.Count.ToString();
                lbSoLuongDuBi.Text = doiHinhDuBi.Count.ToString();
                renderListDoiHinhToTextBox(doiHinhChinh, txbChinhThuc, filePathDoiHinhChinh);
                renderListDoiHinhToTextBox(doiHinhDuBi, txbDuBi, filePathDoiHinhDuBi);

            }
        }
        // Ra sân
        private void ptbRaSanA_Click(object sender, EventArgs e)
        {
            ptbRaSanA_Click();
        }
        private void ptbRaSanA_Click()
        {
            if (Directory.Exists(SaveController.folderPath))
            {
                File.WriteAllText(SaveController.folderPath + TxtFiles.raSanA, cbbRaSanA.Text);
            }
            btnRaSanClick(cbbRaSanA, cbbGhiBanA, cbbTheVangA, cbbTheDoA, cbbVaoSanA,
                MatchController.doiHinhChinhA, MatchController.doiHinhDuBiA, MatchController.hlvA,
                lbSoLuongChinhThucA, lbSoLuongDuBiA, txbChinhThucA, txbDuBiA, TxtFiles.doiHinhChinhThucA, TxtFiles.doiHinhDuBiA);
        }
        private void ptbRaSanB_Click(object sender, EventArgs e)
        {
            ptbRaSanB_Click();
        }
        private void ptbRaSanB_Click()
        {
            if (Directory.Exists(SaveController.folderPath))
            {
                File.WriteAllText(SaveController.folderPath + TxtFiles.raSanB, cbbRaSanB.Text);
            }
            btnRaSanClick(cbbRaSanB, cbbGhiBanB, cbbTheVangB, cbbTheDoB, cbbVaoSanB,
                MatchController.doiHinhChinhB, MatchController.doiHinhDuBiB, MatchController.hlvB,
                lbSoLuongChinhThucB, lbSoLuongDuBiB, txbChinhThucB, txbDuBiB, TxtFiles.doiHinhChinhThucB, TxtFiles.doiHinhDuBiB);
        }
        // Vào sân
        private void ptbVaoSanA_Click(object sender, EventArgs e)
        {
            ptbVaoSanAClick();
        }
        private void ptbVaoSanAClick()
        {
            if (Directory.Exists(SaveController.folderPath))
            {
                File.WriteAllText(SaveController.folderPath + TxtFiles.vaoSanA, cbbVaoSanA.Text);
            }
            btnVaoSanClick(cbbRaSanA, cbbGhiBanA, cbbTheVangA, cbbTheDoA, cbbVaoSanA,
                MatchController.doiHinhChinhA, MatchController.doiHinhDuBiA, MatchController.hlvA,
                lbSoLuongChinhThucA, lbSoLuongDuBiA, txbChinhThucA, txbDuBiA, TxtFiles.doiHinhChinhThucA, TxtFiles.doiHinhDuBiA);
        }
        private void ptbVaoSanB_Click(object sender, EventArgs e)
        {
            ptbVaoSanBClick();
        }
        private void ptbVaoSanBClick()
        {
            if (Directory.Exists(SaveController.folderPath))
            {
                File.WriteAllText(SaveController.folderPath + TxtFiles.vaoSanB, cbbVaoSanB.Text);
            }
            btnVaoSanClick(cbbRaSanB, cbbGhiBanB, cbbTheVangB, cbbTheDoB, cbbVaoSanB,
                MatchController.doiHinhChinhB, MatchController.doiHinhDuBiB, MatchController.hlvB,
                lbSoLuongChinhThucB, lbSoLuongDuBiB, txbChinhThucB, txbDuBiB, TxtFiles.doiHinhChinhThucB, TxtFiles.doiHinhDuBiB);
        }
        // Tỉ Số
        private void nudTiSoA_ValueChanged(object sender, EventArgs e)
        {
            if (Directory.Exists(SaveController.folderPath))
            {
                File.WriteAllText(SaveController.folderPath + TxtFiles.tiSoA, nudTiSoA.Value.ToString());
            }
            calcWinRate(true);
            isSaved = false;
        }
        private void nudTiSoB_ValueChanged(object sender, EventArgs e)
        {
            if (Directory.Exists(SaveController.folderPath))
            {
                File.WriteAllText(SaveController.folderPath + TxtFiles.tiSoB, nudTiSoB.Value.ToString());
            }
            calcWinRate(true);
            isSaved = false;
        }
        // Reset tỉ số
        private void resetTiSoDoiAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nudTiSoA.Value = 0;
        }
        private void resetTiSoDoiBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nudTiSoB.Value = 0;
        }
        private void resetTiSo2DoiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nudTiSoA.Value = nudTiSoB.Value = 0;
        }

        // Luân lưu
        public void changeNudValueA(string value)
        {
            nudLuanLuuA.Value = Convert.ToInt32(value);
        }
        public void changeNudValueB(string value)
        {
            nudLuanLuuB.Value = Convert.ToInt32(value);
        }
        private void nudLuanLuuA_ValueChanged(object sender, EventArgs e)
        {
            if (Directory.Exists(SaveController.folderPath))
            {
                File.WriteAllText(SaveController.folderPath + TxtFiles.tiSoLuanLuuA, nudLuanLuuA.Value.ToString());
            }
            isSaved = false;
        }
        private void nudLuanLuuB_ValueChanged(object sender, EventArgs e)
        {
            if (Directory.Exists(SaveController.folderPath))
            {
                File.WriteAllText(SaveController.folderPath + TxtFiles.tiSoLuanLuuB, nudLuanLuuB.Value.ToString());
            }
            isSaved = false;
        }
        // Reset tỉ số luân lưu

        private void resetLuanLuuDoiAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nudLuanLuuA.Value = 0;
        }

        private void resetLuanLuuDoiBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nudLuanLuuB.Value = 0;
        }

        private void resetLuanLuu2DoiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nudLuanLuuA.Value = nudLuanLuuB.Value = 0;
        }
        //
        // Lấy thời gian ghi bàn, thẻ
        private int getMinuteFromLabel(Label lb)
        {
            string s = lb.Text;
            int min = 0;
            min = Convert.ToInt32(s.Split(':')[0]);
            return min;
        }//90:00
        private int getSecondFromLabel(Label lb)
        {
            string s = lb.Text;
            int sec = 0;
            sec = Convert.ToInt32(s.Split(':')[1]);
            return sec;
        }//90:00
        private string getCurrentTime()    //90', 90+1'
        {
            string time = string.Empty;
            int minuteOfficialTime = getMinuteFromLabel(lbThoiGianChinhThuc);
            int secondOfficialTime = getSecondFromLabel(lbThoiGianChinhThuc);
            int minuteOverTime = getMinuteFromLabel(lbThoiGianBuGio);

            if (minuteOfficialTime == MatchController.toTime
                && secondOfficialTime == 0) //Hết thời gian chính thức
            {
                time = minuteOfficialTime.ToString() + "+" + (minuteOverTime + 1).ToString() + "'";
            }
            else //Trong thời gian chính thức
            {
                time = (minuteOfficialTime + 1).ToString() + "'";
            }
            return time;
        }
        // Ghi Bàn
        private void ptbGhiBanClick(ComboBox cbb, TextBox txbLichSuGhiBanTeamMinh, TextBox txbLichSuGhiBanTeamDich, string filePathTeamMinh, string filePathTeamDich)
        {
            if (cbb.SelectedIndex >= 0)
            {
                if (!ckbPhanLuoi1.Checked) // K phản lưới
                {
                    if (Directory.Exists(SaveController.folderPath))
                    {
                        File.WriteAllText(SaveController.folderPath + filePathTeamMinh, cbb.Text);
                    }
                    string lichSu = string.Empty;
                    lichSu = cbb.Text + " " + getCurrentTime() + (ckbPenalty.Checked ? " (P)" : string.Empty) + "\r\n";
                    //txbLichSuGhiBanTeamMinh.Text += lichSu;
                    formatThenWriteLichSuTxb(txbLichSuGhiBanTeamMinh, lichSu);
                    showNotify(Default.titleGhiBan, cbb.Text, Default.iconGhiBan, 0);
                }
                else //Phản lưới
                {
                    if (Directory.Exists(SaveController.folderPath))
                    {
                        File.WriteAllText(SaveController.folderPath + filePathTeamDich, cbb.Text + " (OG)");
                    }
                    string lichSu = string.Empty;
                    lichSu = cbb.Text + " " + getCurrentTime() + " (OG)" + "\r\n";
                    //txbLichSuGhiBanTeamDich.Text += lichSu;
                    formatThenWriteLichSuTxb(txbLichSuGhiBanTeamDich, lichSu);
                    showNotify(Default.titlePhanLuoi, cbb.Text, Default.iconGhiBan, 0);
                }
            }
        }
        private void getLichSuTheVangTheDoClick(ComboBox cbb, TextBox txbLichSu, string filePath)
        {
            if (cbb.SelectedIndex >= 0)
            {
                if (Directory.Exists(SaveController.folderPath))
                {
                    File.WriteAllText(SaveController.folderPath + filePath, cbb.Text);
                }
                string lichSu = string.Empty;
                lichSu = cbb.Text + " " + getCurrentTime() + "\r\n";
                formatThenWriteLichSuTxb(txbLichSu, lichSu);
            }
        }
        private void formatThenWriteLichSuTxb(TextBox txbLichSu, string lichSu)
        {
            if (string.IsNullOrWhiteSpace(txbLichSu.Text) || txbLichSu.Text.EndsWith("\r\n"))  //chưa có chữ hoặc có dấu xuống dòng rồi
            {
                txbLichSu.Text += lichSu;
            }
            else
            {
                txbLichSu.Text += "\r\n" + lichSu;
            }
        }
        private void ptbGhiBanA_Click(object sender, EventArgs e)
        {
            ptbGhiBanAClick();
        }
        private void ptbGhiBanAClick()
        {
            ptbGhiBanClick(cbbGhiBanA, txbLichSuGhiBanA, txbLichSuGhiBanB, TxtFiles.ghiBanA, TxtFiles.ghiBanB);
            if (ckbCapNhatTiSo.Checked)
            {
                if (!ckbPhanLuoi1.Checked) nudTiSoA.Value++;
                else nudTiSoB.Value++;
            }
            ckbPenalty.Checked = ckbPhanLuoi1.Checked = false;
            isSaved = false;
        }
        private void ptbGhiBanB_Click(object sender, EventArgs e)
        {
            ptbGhiBanBClick();
        }
        private void ptbGhiBanBClick()
        {
            ptbGhiBanClick(cbbGhiBanB, txbLichSuGhiBanB, txbLichSuGhiBanA, TxtFiles.ghiBanB, TxtFiles.ghiBanA);
            if (ckbCapNhatTiSo.Checked)
            {
                if (!ckbPhanLuoi1.Checked) nudTiSoB.Value++;
                else nudTiSoA.Value++;
            }
            ckbPenalty.Checked = ckbPhanLuoi1.Checked = false;
            isSaved = false;
        }
        // Thẻ Vàng

        private void ptbTheVangA_Click(object sender, EventArgs e)
        {
            ptbTheVangAClick();
        }
        private void ptbTheVangAClick()
        {
            getLichSuTheVangTheDoClick(cbbTheVangA, txbLichSuTheVangA, TxtFiles.theVangA);
            showNotify(Default.titleTheVang, cbbTheVangA.Text, Default.iconTheVang, 1);
            isSaved = false;
        }
        private void ptbTheVangB_Click(object sender, EventArgs e)
        {
            ptbTheVangBClick();
        }
        private void ptbTheVangBClick()
        {
            getLichSuTheVangTheDoClick(cbbTheVangB, txbLichSuTheVangB, TxtFiles.theVangB);
            showNotify(Default.titleTheVang, cbbTheVangB.Text, Default.iconTheVang, 1);
            isSaved = false;
        }
        // Thẻ Đỏ
        private void ptbTheDoA_Click(object sender, EventArgs e)
        {
            ptbTheDoAClick();
        }
        private void ptbTheDoB_Click(object sender, EventArgs e)
        {
            ptbTheDoBClick();
        }
        private void ptbTheDoAClick()
        {
            getLichSuTheVangTheDoClick(cbbTheDoA, txbLichSuTheDoA, TxtFiles.theDoA);

            funcTheDo(cbbGhiBanA, cbbTheVangA, cbbTheDoA, cbbRaSanA, cbbVaoSanA,
                MatchController.doiHinhChinhA, MatchController.doiHinhDuBiA, MatchController.hlvA,
                lbSoLuongChinhThucA, lbSoLuongDuBiA,
                txbChinhThucA, txbDuBiA, txbHLVA, txbTheDoA,
                TxtFiles.doiHinhChinhThucA, TxtFiles.doiHinhDuBiA, TxtFiles.hlvA, TxtFiles.doiHinhTheDoA);
            MatchController.hlvA = txbHLVA.Text;
            isSaved = false;
        }
        private void ptbTheDoBClick()
        {
            getLichSuTheVangTheDoClick(cbbTheDoB, txbLichSuTheDoB, TxtFiles.theDoB);

            funcTheDo(cbbGhiBanB, cbbTheVangB, cbbTheDoB, cbbRaSanB, cbbVaoSanB,
                MatchController.doiHinhChinhB, MatchController.doiHinhDuBiB, MatchController.hlvB,
                lbSoLuongChinhThucB, lbSoLuongDuBiB,
                txbChinhThucB, txbDuBiB, txbHLVB, txbTheDoB,
                TxtFiles.doiHinhChinhThucB, TxtFiles.doiHinhDuBiB, TxtFiles.hlvB, TxtFiles.doiHinhTheDoB);
            MatchController.hlvB = txbHLVB.Text;
            isSaved = false;
        }
        //
        private void funcTheDo(ComboBox cbbGhiBan, ComboBox cbbTheVang, ComboBox cbbTheDo, ComboBox cbbRaSan, ComboBox cbbVaoSan,
            List<string> doiHinhChinh, List<string> doiHinhDuBi, string hlv,
            Label lbSoLuongChinhThuc, Label lbSoLuongDuBi,
            TextBox txbChinhThuc, TextBox txbDuBi, TextBox txbHLV, TextBox txbTheDo,
            string filePathDoiHinhChinh, string filePathDoiHinhDuBi, string filePathHLV, string filePathDoiHinhTheDo)
        {
            if (doiHinhChinh != null && doiHinhChinh.Count > 0)
            {
                int id = cbbTheDo.SelectedIndex;
                int lenDoiHinhChinh = doiHinhChinh.Count;
                string cauThuTheDo = string.Empty;

                if (id < lenDoiHinhChinh)
                {
                    cauThuTheDo = doiHinhChinh[id];
                    txbTheDo.Text += cauThuTheDo + "\r\n";

                    doiHinhChinh.RemoveAt(id);
                }
                else if (id < (lenDoiHinhChinh + doiHinhDuBi.Count))
                {
                    int idDoiHinhDuBi = id - lenDoiHinhChinh;

                    cauThuTheDo = doiHinhDuBi[idDoiHinhDuBi];
                    txbTheDo.Text += cauThuTheDo + "\r\n";

                    doiHinhDuBi.RemoveAt(idDoiHinhDuBi);
                }
                else
                {
                    cauThuTheDo = hlv;
                    txbTheDo.Text += cauThuTheDo + "\r\n";

                    hlv = string.Empty;
                }

                List<string> listCbbGhiBan = new List<string>(doiHinhChinh);
                cbbGhiBan.DataSource = listCbbGhiBan;
                List<string> listCbbRaSan = new List<string>(doiHinhChinh);
                cbbRaSan.DataSource = listCbbRaSan;
                List<string> listCbbVaoSan = new List<string>(doiHinhDuBi);
                cbbVaoSan.DataSource = listCbbVaoSan;

                List<string> listCbbTheVang = new List<string>(doiHinhChinh);
                List<string> listCbbTheDo = new List<string>(doiHinhChinh);

                listCbbTheVang.AddRange(doiHinhDuBi);
                listCbbTheDo.AddRange(doiHinhDuBi);
                if (hlv != string.Empty)
                {
                    listCbbTheVang.Add(hlv);
                    listCbbTheDo.Add(hlv);
                }
                cbbTheVang.DataSource = listCbbTheVang;
                cbbTheDo.DataSource = listCbbTheDo;

                lbSoLuongChinhThuc.Text = doiHinhChinh.Count.ToString();
                lbSoLuongDuBi.Text = doiHinhDuBi.Count.ToString();

                renderListDoiHinhToTextBox(doiHinhChinh, txbChinhThuc, filePathDoiHinhChinh);
                renderListDoiHinhToTextBox(doiHinhDuBi, txbDuBi, filePathDoiHinhDuBi);
                txbHLV.Text = hlv;
                if (Directory.Exists(SaveController.folderPath))
                {
                    File.WriteAllText(SaveController.folderPath + filePathHLV, txbHLV.Text);
                }
                showNotify(Default.titleTheDo, cauThuTheDo, Default.iconTheDo, 1);
            }
        }
        // TxbTheDo_TextChanged
        private void txbTheDoA_TextChanged(object sender, EventArgs e)
        {
            if (Directory.Exists(SaveController.folderPath))
            {
                File.WriteAllText(SaveController.folderPath + TxtFiles.doiHinhTheDoA, txbTheDoA.Text);
            }
            isSaved = false;
        }
        private void txbTheDoB_TextChanged(object sender, EventArgs e)
        {
            if (Directory.Exists(SaveController.folderPath))
            {
                File.WriteAllText(SaveController.folderPath + TxtFiles.doiHinhTheDoB, txbTheDoB.Text);
            }
            isSaved = false;
        }
        // Lịch sử
        private void txbLichSuGhiBanA_TextChanged(object sender, EventArgs e)
        {
            if (Directory.Exists(SaveController.folderPath))
            {
                File.WriteAllText(SaveController.folderPath + TxtFiles.lichSuGhiBanA, txbLichSuGhiBanA.Text);
            }
            isSaved = false;
        }
        private void txbLichSuGhiBanB_TextChanged(object sender, EventArgs e)
        {
            if (Directory.Exists(SaveController.folderPath))
            {
                File.WriteAllText(SaveController.folderPath + TxtFiles.lichSuGhiBanB, txbLichSuGhiBanB.Text);
            }
            isSaved = false;
        }
        private void txbLichSuTheVangA_TextChanged(object sender, EventArgs e)
        {
            if (Directory.Exists(SaveController.folderPath))
            {
                File.WriteAllText(SaveController.folderPath + TxtFiles.lichSuTheVangA, txbLichSuTheVangA.Text);
            }
            isSaved = false;
        }
        private void txbLichSuTheVangB_TextChanged(object sender, EventArgs e)
        {
            if (Directory.Exists(SaveController.folderPath))
            {
                File.WriteAllText(SaveController.folderPath + TxtFiles.lichSuTheVangB, txbLichSuTheVangB.Text);
            }
            isSaved = false;
        }
        private void txbLichSuTheDoA_TextChanged(object sender, EventArgs e)
        {
            if (Directory.Exists(SaveController.folderPath))
            {
                File.WriteAllText(SaveController.folderPath + TxtFiles.lichSuTheDoA, txbLichSuTheDoA.Text);
            }
            isSaved = false;
        }
        private void txbLichSuTheDoB_TextChanged(object sender, EventArgs e)
        {
            if (Directory.Exists(SaveController.folderPath))
            {
                File.WriteAllText(SaveController.folderPath + TxtFiles.lichSuTheDoB, txbLichSuTheDoB.Text);
            }
            isSaved = false;
        }
        //
        // Chức năng đường dẫn mặc định
        //
        private void ptbGoiYTen_Click(object sender, EventArgs e)
        {
             ptbGoiYTenClick();
        }
        private void ptbGoiYTenClick()
        {
            string fileName = string.Empty;
            fileName += (string.IsNullOrWhiteSpace(txbTenA.Text) ? string.Empty : (txbTenA.Text + "-"))
                + (string.IsNullOrWhiteSpace(txbTenB.Text) ? string.Empty : (txbTenB.Text + "_"))
                + (string.IsNullOrWhiteSpace(txbVongDau.Text) ? string.Empty : (txbVongDau.Text + "_"))
                + (string.IsNullOrWhiteSpace(txbGiaiDau.Text) ? Default.tenGoiY : (txbGiaiDau.Text));
            txbTenThuMuc.Text = fileName;
        }

        private void ptbGoiYTen1_DoubleClick(object sender, EventArgs e)
        {
             ptbGoiYTen1_DoubleClick();
        }
        private void ptbGoiYTen1_DoubleClick()
        {
            string fileName = "Kết quả_";
            fileName += (string.IsNullOrWhiteSpace(txbTenA.Text) ? string.Empty : (txbTenA.Text + "-"))
                + (string.IsNullOrWhiteSpace(txbTenB.Text) ? string.Empty : (txbTenB.Text + "_"))
                + (string.IsNullOrWhiteSpace(txbVongDau.Text) ? string.Empty : (txbVongDau.Text + "_"))
                + (string.IsNullOrWhiteSpace(txbGiaiDau.Text) ? Default.tenGoiY : (txbGiaiDau.Text));

            txbTenThuMuc.Text = fileName;
        }
        private void xoaGoiYTenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txbTenThuMuc.Text = string.Empty;
        }
        //private void setupContextMenuStripGoiYTen()
        //{
        //    string name1 = "Kết quả_"
        //                    + (string.IsNullOrWhiteSpace(txbTenA.Text) ? string.Empty : (txbTenA.Text + "-"))
        //                    + (string.IsNullOrWhiteSpace(txbTenB.Text) ? string.Empty : (txbTenB.Text + "_"))
        //                    + (string.IsNullOrWhiteSpace(txbVongDau.Text) ? string.Empty : (txbVongDau.Text + "_"))
        //                    + (string.IsNullOrWhiteSpace(txbGiaiDau.Text) ? Default.tenGoiY : (txbGiaiDau.Text));

        //    string name2 = (!string.IsNullOrWhiteSpace(txbTenA.Text) ? (txbTenA.Text + "-") : string.Empty)
        //                    + (string.IsNullOrWhiteSpace(txbTenB.Text) ? (txbTenB.Text + "-") : string.Empty);

        //    List<string> listGoiYTen = new List<string>();
        //    try
        //    {

        //        foreach (string path in listRecentPaths)
        //        {
        //            try
        //            {
        //                ToolStripMenuItem menuItemRecentPaths = new ToolStripMenuItem();
        //                menuItemRecentPaths.Text = path;
        //                menuItemRecentPaths.Click += new EventHandler(menuGoiYTenItem_Click);

        //                contextMenuStripRecentPaths.Items.Add(menuItemRecentPaths);

        //            }
        //            catch { }
        //        }
        //        ptbChonTuMayTinh1.ContextMenuStrip = contextMenuStripRecentPaths;
        //        ptbChonDuongDan1.ContextMenuStrip = contextMenuStripChonDuongDan;
        //    }
        //    catch { };
        //}
        //private void menuGoiYTenItem_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
        //        string name = menuItem.Text;
        //        txbTenThuMuc.Text = name;
        //    }
        //    catch { };
        //}

        //
        // Lưu đường dẫn mặc định
        //
        private void ptbLuuDuongDanMacDinh_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txbDuongDan.Text))
            {
                if (Directory.Exists(txbDuongDan.Text))
                {
                    appData.defaultFolderPath = txbDuongDan.Text;
                    MessageBox.Show("Đã ghim " + txbDuongDan.Text + " làm thư mục mặc định", "Thành công: Ghim thư mục mặc định", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Thư mục không tồn tại, không thể ghim làm thư mục mặc định.", "Lỗi: Ghim thư mục mặc định", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Đường dẫn đang trống.", "Lỗi: Ghim thư mục mặc định", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ptbChonDuongDanMacDinh_MouseHover(object sender, EventArgs e)
        {
            ttDuongDanMacDinh.SetToolTip(ptbChonDuongDanMacDinh1, appData.defaultFolderPath);
        }

        private void ptbChonDuongDanMacDinh_MouseLeave(object sender, EventArgs e)
        {
            ttDuongDanMacDinh.SetToolTip(ptbChonDuongDanMacDinh1, null);
        }
        private void ptbChonDuongDanMacDinh_Click(object sender, EventArgs e)
        {
            ptbChonDuongDanMacDinhClick();
        }
        private void ptbChonDuongDanMacDinhClick()
        {
            if (!isSaved)
            {
                DialogResult diaRes = MessageBox.Show("Lưu trước khi sang thư mục khác?",
                    "Lưu", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (diaRes == DialogResult.Yes)
                {
                    try
                    {
                        saveProject();
                        if (isSaved)
                        {
                            ptbChonDuongDanMacDinhClickNoSave();
                        }
                    }
                    catch
                    {
                        DialogResult diaRes2 = MessageBox.Show("Vẫn truy cập thư mục mặc định mà không lưu?",
                           "Lỗi: Lưu trữ không thành công", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                        if (diaRes2 == DialogResult.Yes)
                        {
                            ptbChonDuongDanMacDinhClickNoSave();
                        }
                    };
                }
                else if (diaRes == DialogResult.No)
                {
                    ptbChonDuongDanMacDinhClickNoSave();
                }
            }
            else
            {
                ptbChonDuongDanMacDinhClickNoSave();
            }
        }
        private void ptbChonDuongDanMacDinhClickNoSave()
        {
            if (Directory.Exists(appData.defaultFolderPath))
            {
                accessFolder(appData.defaultFolderPath);
            }
            else
            {
                MessageBox.Show("Thư mục không tồn tại hoặc không truy cập được.",
                    "Lỗi: Chọn thư mục mặc định", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Ghi chú
        private void ptbExpandNote_Click(object sender, EventArgs e)
        {
            ptbExpandNote_Click();
        }
        private void ptbExpandNote_Click()
        {
            fThoiTiet f = new fThoiTiet();
            f.oldContent = txbThoiTiet.Text;
            f.ShowDialog();
            if (f.oldContent != f.newContent)
            {
                txbThoiTiet.Text = f.newContent;
                //showNotifySuccess("Đã lưu thay đổi");
            }
        }
        //
        // Thông báo
        //
        private void showNotify(string title, string content, Icon icon, int i)
        {
            if (ckbShowNotify1.Checked)
            {
                if (icon != null) FootballKit.Icon = icon;
                if (i == 0) //Ghi bàn
                {
                    content = content + " " + getCurrentTime() + (ckbPenalty.Checked ? " (P)" : string.Empty);
                }
                else content = content + " " + getCurrentTime();
                FootballKit.ShowBalloonTip(Default.timeShowNotify, title, content, ToolTipIcon.None);
            }
        }
        private void showNotifySuccess(string title)
        {
            if (ckbShowNotify1.Checked)
            {
                if (Default.iconSuccess != null) FootballKit.Icon = Default.iconSuccess;
                FootballKit.ShowBalloonTip(Default.timeShowNotify, title, " ", ToolTipIcon.None);
            }
        }
        // Copy vào thư mục mặc định và mở
        private void ptbCopyToDefault_Click(object sender, EventArgs e)
        {
            ptbCopyToDefault_Click();
        }
        private void ptbCopyToDefault_Click()
        {
            if (Directory.Exists(appData.defaultFolderPath))
            {
                DialogResult diaRes = MessageBox.Show("Thư mục mặc định có thể chứa dữ liệu của trận khác.\r\nVẫn tiếp tục copy vào thư mục mặc định?",
                    "Copy vào thư mục mặc định", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (diaRes == DialogResult.OK)
                {
                    try
                    {
                        string prevPath = SaveController.folderPath;
                        SaveController.folderPath = appData.defaultFolderPath;

                        if (!Directory.Exists(SaveController.folderPath + "\\Penalties"))
                        {
                            Directory.CreateDirectory(SaveController.folderPath + "\\Penalties");
                        }

                        funcSave();

                        try
                        {
                            File.Copy(prevPath + "\\DoiHinhA.png", SaveController.folderPath + "\\DoiHinhA.png", true);
                        }
                        catch { }
                        try
                        {
                            File.Copy(prevPath + "\\DoiHinhB.png", SaveController.folderPath + "\\DoiHinhB.png", true);
                        }
                        catch { }
                        try
                        {
                            File.Copy(prevPath + "\\ThoiTiet.png", SaveController.folderPath + "\\ThoiTiet.png", true);
                        }
                        catch { }

                        MessageBox.Show("Đã copy vào thư mục mặc định " + SaveController.folderPath,
                            "Copy thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (isSaved == true)
                        {
                            DialogResult diaRes2 = MessageBox.Show("Mở thư mục mặc định ngay?",
                                "Mở thư mục mặc định", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                            if (diaRes2 == DialogResult.OK)
                            {
                                accessFolder(appData.defaultFolderPath);
                            }
                        }
                        else
                        {
                            SaveController.folderPath = prevPath;
                        }
                    }
                    catch { };
                }
            }
            else
            {
                MessageBox.Show("Thư mục mặc định không tồn tại hoặc không truy cập được.",
                    "Lỗi: Copy vào thư mục mặc định", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Tạo mới
        private void ptbTaoMoi_Click(object sender, EventArgs e)
        {
            ptbTaoMoiClick();
        }
        private void ptbTaoMoiClick()
        {
            if (!isSaved)
            {
                DialogResult diaRes = MessageBox.Show("Lưu trước khi tạo mới?",
                    "Lưu", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (diaRes == DialogResult.Yes)
                {
                    try
                    {
                        saveProject();
                        if (isSaved)
                        {
                            clearAllNoSave();
                        }
                    }
                    catch
                    {
                        DialogResult diaRes2 = MessageBox.Show("Vẫn tạo mới mà không lưu?",
                        "Lỗi: Lưu trữ không thành công", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                        if (diaRes2 == DialogResult.Yes)
                        {
                            clearAllNoSave();
                        }
                    }
                }
                else if (diaRes == DialogResult.No)
                {
                    clearAllNoSave();
                }
            }
            else
            {
                clearAllNoSave();
            }
        }
        private void clearAllNoSave()
        {
            try
            {
                clearLuuTru();
                clearMatchController();
                clearDoiHinh();
                clearLichSu();
                clearPenalties();
                clearDoiHinhController();
                isSaved = true;
            }
            catch { };
        }
        private void clearPenalties()
        {
            try
            {
                if (PensController.fPenalties != null)
                {
                    PensController.fPenalties.resetAll();
                    PensController.fPenalties.Close();
                }
            }
            catch { };
        }
        private void clearLuuTru()
        {
            try
            {
                SaveController.folderName = SaveController.parentPath = SaveController.folderPath = null;
                txbTenThuMuc.Text = txbDuongDan.Text = string.Empty;

                txbTenA.Text = txbTenB.Text = txbVietTatA.Text = txbVietTatB.Text = string.Empty;
                ptbMauAoA.BackColor = ptbMauQuanA.BackColor = Default.mauA;
                ptbMauAoB.BackColor = ptbMauQuanB.BackColor = Default.mauB;
                dongBoMauA();
                dongBoMauB();

                txbGiaiDau.Text = txbVongDau.Text = txbDiaDiem.Text = txbSan.Text
                    = txbThoiGian.Text = txbThoiTiet.Text = txbBinhLuanVien.Text = txbTrongTai.Text = string.Empty;

                appData.lastFolderPath = SaveController.folderPath;

                WeatherController.id = 0;
            }
            catch { };
        }
        private void clearMatchController()
        {
            try
            {
                cbbFromTime.Text = "0";
                cbbToTime.Text = "30";
                resetTime();
                lockTimeControl(false);
                nudTiSoA.Value = nudTiSoB.Value = nudLuanLuuA.Value = nudLuanLuuB.Value = 0;
                cbbHiepDau.SelectedIndex = 0;
                cbbBuGio.SelectedIndex = 2;
                lbWinRate.Text = "...";
            }
            catch { };
        }
        private void clearDoiHinh()
        {
            try
            {
                xoaTatCaONhapToolStripMenuItemClick();
                xoaTatCaONhapToolStripMenuItemBClick();
                txbTheDoA.Text = txbTheDoB.Text = string.Empty;
            }
            catch { };
        }
        private void clearLichSu()
        {
            try
            {
                txbLichSuGhiBanA.Text = txbLichSuGhiBanB.Text =
                txbLichSuTheVangA.Text = txbLichSuTheVangB.Text =
                txbLichSuTheDoA.Text = txbLichSuTheDoB.Text = string.Empty;
            }
            catch { };
        }
        private void clearDoiHinhController()
        {
            try
            {
                foreach (fDoiHinh f in DoiHinhController.listfDoiHinhs)
                {
                    try
                    {
                        f.Close();
                    }
                    catch { }
                }
                DoiHinhController.image = new DoiHinhImage();
                DoiHinhController.listfDoiHinhs = new List<fDoiHinh>();
            }
            catch { };
        }
        //
        // Hỏi có lưu hay ko
        //
        private void txbDuongDan_TextChanged(object sender, EventArgs e)
        {
            isSaved = false;
        }

        private void txbGiaiDau_TextChanged(object sender, EventArgs e)
        {
            if (Directory.Exists(SaveController.folderPath))
            {
                File.WriteAllText(SaveController.folderPath + TxtFiles.giaiDau, txbGiaiDau.Text);
            }
            isSaved = false;
        }

        private void txbVongDau_TextChanged(object sender, EventArgs e)
        {
            if (Directory.Exists(SaveController.folderPath))
            {
                File.WriteAllText(SaveController.folderPath + TxtFiles.vongDau, txbVongDau.Text);
            }
            isSaved = false;
        }

        private void txbDiaDiem_TextChanged(object sender, EventArgs e)
        {
            if (Directory.Exists(SaveController.folderPath))
            {
                File.WriteAllText(SaveController.folderPath + TxtFiles.diaDiem, txbDiaDiem.Text);
            }
            isSaved = false;
        }

        private void txbSan_TextChanged(object sender, EventArgs e)
        {
            if (Directory.Exists(SaveController.folderPath))
            {
                File.WriteAllText(SaveController.folderPath + TxtFiles.san, txbSan.Text);
            }
            isSaved = false;
        }

        private void txbThoiGian_TextChanged(object sender, EventArgs e)
        {
            if (Directory.Exists(SaveController.folderPath))
            {
                File.WriteAllText(SaveController.folderPath + TxtFiles.thoiGian, txbThoiGian.Text);
            }
            isSaved = false;
        }

        private void txbGhiChu_TextChanged(object sender, EventArgs e)
        {
            if (Directory.Exists(SaveController.folderPath))
            {
                File.WriteAllText(SaveController.folderPath + TxtFiles.thoiTiet, txbThoiTiet.Text);
            }
            isSaved = false;
        }

        private void txbBinhLuanVien_TextChanged(object sender, EventArgs e)
        {
            if (Directory.Exists(SaveController.folderPath))
            {
                File.WriteAllText(SaveController.folderPath + TxtFiles.binhLuanVien, txbBinhLuanVien.Text);
            }
            isSaved = false;
        }

        private void txbTrongTai_TextChanged(object sender, EventArgs e)
        {
            if (Directory.Exists(SaveController.folderPath))
            {
                File.WriteAllText(SaveController.folderPath + TxtFiles.trongTai, txbTrongTai.Text);
            }
            isSaved = false;
        }

        private void txbChinhThucA_TextChanged(object sender, EventArgs e)
        {
            isSaved = false;
        }
        private void txbDuBiA_TextChanged(object sender, EventArgs e)
        {
            isSaved = false;
        }
        private void txbHLVA_TextChanged(object sender, EventArgs e)
        {
            isSaved = false;
        }

        private void txbChinhThucB_TextChanged(object sender, EventArgs e)
        {
            isSaved = false;

        }
        private void txbDuBiB_TextChanged(object sender, EventArgs e)
        {
            isSaved = false;
        }
        private void txbHLVB_TextChanged(object sender, EventArgs e)
        {
            isSaved = false;
        }
        //
        // Tăng UX Checkboxes
        //
        private void ckbCapNhatTiSo_CheckedChanged(object sender, EventArgs e)
        {
            changeColorCheckbox(ckbCapNhatTiSo);
        }
        private void ckbShowNotify_CheckedChanged(object sender, EventArgs e)
        {
            changeColorCheckbox(ckbShowNotify1);
        }
        private void ckbPenalty_CheckedChanged(object sender, EventArgs e)
        {
            changeColorCheckbox(ckbPenalty);
        }
        private void ckbPhanLuoi_CheckedChanged(object sender, EventArgs e)
        {
            changeColorCheckbox(ckbPhanLuoi1);
        }
        private void changeColorCheckbox(CheckBox ckb)
        {
            ckb.ForeColor = ckb.Checked ? Default.checkedColor : Default.uncheckedColor;
        }

        //
        // Penalty
        //
        private void lbChiTietLuanLuu_Click(object sender, EventArgs e)
        {
            if (PensController.fPenalties != null) PensController.fPenalties.Close();
            fPenalties f = new fPenalties();
            PensController.fPenalties = f;
            f.Show();
        }
        //
        // Speed
        //
        private void cbbSpeed_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbbSpeedSelectedIndexChanged();
        }
        private void cbbSpeedSelectedIndexChanged()
        {
            speed = Convert.ToDouble(cbbSpeed.Text);
            try
            {
                timer.Dispose();
            }
            catch { }
            if (btnChay.Text == Default.btnTamDung) restartTimer();
        }
        //
        // Cập nhật phiên bản mới
        //
        private void btnCapNhatPhienBan_Click(object sender, EventArgs e)
        {
            btnCapNhatPhienBanClick();
        }
        private void btnCapNhatPhienBanClick()
        {
            MessageBox.Show("Chức năng đang được hoàn thiện. Vui lòng quay lại sau.",
                            "Cập nhật chưa hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        //
        // Resize
        //
        //private void fMain_Resize(object sender, EventArgs e)
        //{
        //    fMain_Resize();
        //}
        //private void fMain_Resize()
        //{

        //    //List<TextBox> listTxbDoiHinh = new List<TextBox> {
        //    //    txbChinhThucA, txbDuBiA, txbChinhThucB, txbDuBiB, txbTheDoA, txbTheDoB };

        //    //List<TextBox> listTxbLichSu = new List<TextBox> {
        //    //    txbLichSuGhiBanA, txbLichSuGhiBanB, txbLichSuTheVangA, txbLichSuTheVangB,
        //    //    txbLichSuTheDoA, txbLichSuTheDoB };

        //    List<Label> listLbCaiDat = new List<Label> {lbTen, lbPhienBan, lbNgayPhatHanh};

        //    List<TextBox> listTxbDuongDan = new List<TextBox> { txbTenThuMuc, txbDuongDan };

        //    funcResizeTextBoxDuongDan(listTxbDuongDan);
        //    //funcResizeTextBox(listTxbDoiHinh, 100f, 14f);
        //    //funcResizeTextBox(listTxbLichSu, 60f, 12f);
        //    funcResizeLabel(listLbCaiDat, 60f, 12f);
        //}
        private void funcResizeTextBox(List<TextBox> listTxb, float max_f, float min_f)
        {
            foreach (TextBox txb in listTxb)
            {
                float fontSize = Math.Max(this.Width / max_f, min_f);
                Font newFont = new Font(txb.Font.FontFamily, fontSize, txb.Font.Style);
                txb.Font = newFont;
            }
        }
        private void funcResizeLabel(List<Label> listLb, float max_f, float min_f)
        {
            foreach (Label lb in listLb)
            {
                float fontSize = Math.Max(this.Width / max_f, min_f);
                Font newFont = new Font(lb.Font.FontFamily, fontSize, lb.Font.Style);
                lb.Font = newFont;
            }
        }
        private void funcResizeComboBox(ComboBox cbb, float max_f, float min_f, int max_i, int min_i)
        {
            float fontSize = Math.Max(this.Width / max_f, min_f);
            Font newFont = new Font(cbb.Font.FontFamily, fontSize, cbb.Font.Style);
            cbb.Font = newFont;
            cbb.Width = Math.Max(this.Width / max_i, min_i);
        }
        private void funcResizeTextBoxDuongDan(List<TextBox> listTxb)
        {
            foreach (TextBox txb in listTxb)
            {
                float fontSize = this.Width < 1280 ? 12f : 14f;
                Font newFont = new Font(txb.Font.FontFamily, fontSize, txb.Font.Style);
                txb.Font = newFont;
            }
        }
        private void funcResizeThoiGian()
        {
            //float fontSize = Math.Max(this.Width / max_f, min_f);
            //Font newFont = new Font(lb.Font.FontFamily, fontSize, lb.Font.Style);
            //lb.Font = newFont;
        }
        //
        // Change size by mouse wheel
        //
        private void txbChinhThuc_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0) // scrolling up
            {
                txbChinhThucA.Font = txbChinhThucB.Font = new Font(txbChinhThucA.Font.FontFamily,
                    txbChinhThucA.Font.Size + 1, txbChinhThucA.Font.Style);
            }
            else if (e.Delta < 0) // scrolling down
            {
                if (txbChinhThucA.Font.Size > 1) // don't go below font size 1
                {
                    txbChinhThucA.Font = txbChinhThucB.Font = new Font(txbChinhThucA.Font.FontFamily,
                        txbChinhThucA.Font.Size - 1, txbChinhThucA.Font.Style);
                }
            }
        }
        private void txbDuBi_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0) // scrolling up
            {
                txbDuBiA.Font = txbDuBiB.Font = new Font(txbDuBiA.Font.FontFamily,
                    txbDuBiA.Font.Size + 1, txbDuBiA.Font.Style);
            }
            else if (e.Delta < 0) // scrolling down
            {
                if (txbDuBiA.Font.Size > 1) // don't go below font size 1
                {
                    txbDuBiA.Font = txbDuBiB.Font = new Font(txbDuBiA.Font.FontFamily,
                        txbDuBiA.Font.Size - 1, txbDuBiA.Font.Style);
                }
            }
        }
        private void txbTheDo_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0) // scrolling up
            {
                txbTheDoA.Font = txbTheDoB.Font = new Font(txbTheDoA.Font.FontFamily,
                    txbTheDoA.Font.Size + 1, txbTheDoA.Font.Style);
            }
            else if (e.Delta < 0) // scrolling down
            {
                if (txbTheDoA.Font.Size > 1) // don't go below font size 1
                {
                    txbTheDoA.Font = txbTheDoB.Font = new Font(txbTheDoA.Font.FontFamily,
                        txbTheDoA.Font.Size - 1, txbTheDoA.Font.Style);
                }
            }
        }
        private void txbHLV_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0) // scrolling up
            {
                txbHLVA.Font = txbHLVB.Font = new Font(txbHLVA.Font.FontFamily,
                    txbHLVA.Font.Size + 1, txbHLVA.Font.Style);
            }
            else if (e.Delta < 0) // scrolling down
            {
                if (txbHLVA.Font.Size > 1) // don't go below font size 1
                {
                    txbHLVA.Font = txbHLVB.Font = new Font(txbHLVA.Font.FontFamily,
                        txbHLVA.Font.Size - 1, txbHLVA.Font.Style);
                }
            }
        }
        private void txbLichSu_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0) // scrolling up
            {
                txbLichSuGhiBanA.Font = txbLichSuGhiBanB.Font
                         = txbLichSuTheVangA.Font = txbLichSuTheVangB.Font
                         = txbLichSuTheDoA.Font = txbLichSuTheDoB.Font
                         = new Font(txbLichSuGhiBanA.Font.FontFamily,
                          txbLichSuGhiBanA.Font.Size + 1, txbLichSuGhiBanA.Font.Style);
            }
            else if (e.Delta < 0) // scrolling down
            {
                if (txbChinhThucA.Font.Size > 1) // don't go below font size 1
                {
                    txbLichSuGhiBanA.Font = txbLichSuGhiBanB.Font
                        = txbLichSuTheVangA.Font = txbLichSuTheVangB.Font
                        = txbLichSuTheDoA.Font = txbLichSuTheDoB.Font
                        = new Font(txbLichSuGhiBanA.Font.FontFamily,
                         txbLichSuGhiBanA.Font.Size - 1, txbLichSuGhiBanA.Font.Style);
                }
            }
        }

        private void addMouseWheelEvents()
        {
            txbChinhThucA.MouseWheel += new MouseEventHandler(txbChinhThuc_MouseWheel);
            txbChinhThucB.MouseWheel += new MouseEventHandler(txbChinhThuc_MouseWheel);

            txbDuBiA.MouseWheel += new MouseEventHandler(txbDuBi_MouseWheel);
            txbDuBiB.MouseWheel += new MouseEventHandler(txbDuBi_MouseWheel);

            txbTheDoA.MouseWheel += new MouseEventHandler(txbTheDo_MouseWheel);
            txbTheDoB.MouseWheel += new MouseEventHandler(txbTheDo_MouseWheel);

            //txbHLVA.MouseWheel += new MouseEventHandler(txbHLV_MouseWheel);
            //txbHLVB.MouseWheel += new MouseEventHandler(txbHLV_MouseWheel);

            txbLichSuGhiBanA.MouseWheel += new MouseEventHandler(txbLichSu_MouseWheel);
            txbLichSuGhiBanB.MouseWheel += new MouseEventHandler(txbLichSu_MouseWheel);
            txbLichSuTheVangA.MouseWheel += new MouseEventHandler(txbLichSu_MouseWheel);
            txbLichSuTheVangB.MouseWheel += new MouseEventHandler(txbLichSu_MouseWheel);
            txbLichSuTheDoA.MouseWheel += new MouseEventHandler(txbLichSu_MouseWheel);
            txbLichSuTheDoB.MouseWheel += new MouseEventHandler(txbLichSu_MouseWheel);
        }
        //
        // SetToolTip Mouse Hover
        //
        private void comboBox1_MouseHover(object sender, EventArgs e)
        {
            try
            {
                ComboBox comboBox = (ComboBox)sender;
                if (!string.IsNullOrEmpty(comboBox.Text))
                {
                    ttChung.SetToolTip(comboBox, comboBox.Text);
                }
            }
            catch { }
        }
        private void txb_MouseHover(object sender, EventArgs e)
        {
            try
            {
                TextBox txb = (TextBox)sender;
                if (!string.IsNullOrEmpty(txb.Text))
                {
                    ttChung.SetToolTip(txb, txb.Text);
                }
            }
            catch { }
        }
        private void lb_MouseHover(object sender, EventArgs e)
        {
            try
            {
                Label lb = (Label)sender;
                if (!string.IsNullOrEmpty(lb.Text))
                {
                    ttChung.SetToolTip(lb, lb.Text);
                }
            }
            catch { }
        }
        private void lbA_MouseHover(object sender, EventArgs e)
        {
            try
            {
                Label lb = (Label)sender;
                string caption = grbDoiA.Text;
                ttChung.SetToolTip(lb, caption);
            }
            catch { }
        }
        private void lbB_MouseHover(object sender, EventArgs e)
        {
            try
            {
                Label lb = (Label)sender;
                string caption = grbDoiB.Text;
                ttChung.SetToolTip(lb, caption);
            }
            catch { }
        }
        //
        // Auto Complete Source
        //
        private void setACS(ACS aCS, TextBox txb)
        {
            List<string> list = aCS.listString;
            txb.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txb.AutoCompleteCustomSource = new AutoCompleteStringCollection();
            txb.AutoCompleteCustomSource.AddRange(list.ToArray());
            txb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        }
        private void setAllACSs()
        {
            setACS(ACSList.tenThuMuc, txbTenThuMuc);

            setACS(ACSList.tenDoi, txbTenA);
            setACS(ACSList.tenDoi, txbTenB);

            setACS(ACSList.vietTat, txbVietTatA);
            setACS(ACSList.vietTat, txbVietTatB);

            setACS(ACSList.giaiDau, txbGiaiDau);
            setACS(ACSList.vongDau, txbVongDau);

            setACS(ACSList.diaDiem, txbDiaDiem);
            setACS(ACSList.san, txbSan);
            setACS(ACSList.thoiGian, txbThoiGian);

            setACS(ACSList.binhLuanVien, txbBinhLuanVien);
            setACS(ACSList.trongTai, txbTrongTai);

            setACS(ACSList.hlv, txbHLVA);
            setACS(ACSList.hlv, txbHLVB);
        }
        private void addACS(ACS aCS, TextBox txb)
        {
            try
            {
                List<string> list = aCS.listString;
                string content = txb.Text;
                if (!string.IsNullOrWhiteSpace(content))
                {
                    if (!list.Contains(content)) list.Add(content);
                    if (!txb.AutoCompleteCustomSource.Contains(content)) txb.AutoCompleteCustomSource.Add(content);
                }
            }
            catch { }
        }
        private void addACSFor2Textbox(ACS aCS, TextBox txb1, TextBox txb2)
        {
            try
            {
                List<string> list = aCS.listString;
                string content1 = txb1.Text;
                if (!string.IsNullOrWhiteSpace(content1))
                {
                    if (!list.Contains(content1)) list.Add(content1);
                    if (!txb1.AutoCompleteCustomSource.Contains(content1)) txb1.AutoCompleteCustomSource.Add(content1);
                    if (!txb2.AutoCompleteCustomSource.Contains(content1)) txb2.AutoCompleteCustomSource.Add(content1);

                }
                string content2 = txb2.Text;
                if (!string.IsNullOrWhiteSpace(content2))
                {
                    if (!list.Contains(content2)) list.Add(content2);
                    if (!txb1.AutoCompleteCustomSource.Contains(content2)) txb1.AutoCompleteCustomSource.Add(content2);
                    if (!txb2.AutoCompleteCustomSource.Contains(content2)) txb2.AutoCompleteCustomSource.Add(content2);
                }
            }
            catch { }
        }
        private void addAllACSs()
        {
            addACS(ACSList.tenThuMuc, txbTenThuMuc);

            addACSFor2Textbox(ACSList.tenDoi, txbTenA, txbTenB);

            addACSFor2Textbox(ACSList.vietTat, txbVietTatA, txbVietTatB);

            addACS(ACSList.giaiDau, txbGiaiDau);
            addACS(ACSList.vongDau, txbVongDau);

            addACS(ACSList.diaDiem, txbDiaDiem);
            addACS(ACSList.san, txbSan);
            addACS(ACSList.thoiGian, txbThoiGian);

            addACS(ACSList.binhLuanVien, txbBinhLuanVien);
            addACS(ACSList.trongTai, txbTrongTai);

            addACSFor2Textbox(ACSList.hlv, txbHLVA, txbHLVB);
        }
        private void btnDanhSachACS_Click(object sender, EventArgs e)
        {
            fACS f = new fACS();
            f.ShowDialog();
            try
            {
                setAllACSs();
            }
            catch { }
        }
        //
        // Thu Phóng
        //
        private void ptbThuNho_CheckedChanged(object sender, EventArgs e)
        {
            this.Size = new Size(MinimumSize.Width, 968);
        }

        private void ptbThuVua_CheckedChanged(object sender, EventArgs e)
        {
            this.Size = new Size(800, 968);
        }

        private void ptbThuRong_CheckedChanged(object sender, EventArgs e)
        {
            this.Size = new Size(1920, 1080);
        }
        //
        // Tạo Ảnh Màu Áo Quần
        //
        private void taoAnhMauAoQuan(Color colorAo, Color colorQuan, string whatTeam)
        {
            if (Directory.Exists(SaveController.folderPath))
            {
                try
                {
                    Panel flp = new Panel
                    {
                        BackColor = Color.LightGray,
                        Padding = new Padding(5),
                        Size = new Size(666, 36)
                    };

                    Label lbAo = new Label
                    {
                        BackColor = colorAo,
                        AutoSize = false,
                        Size = new Size(450, 26),
                        FlatStyle = FlatStyle.Flat,
                        BorderStyle = BorderStyle.FixedSingle
                    };

                    Label lbQuan = new Label
                    {
                        BackColor = colorQuan,
                        AutoSize = false,
                        Size = new Size(200, 26),
                        FlatStyle = FlatStyle.Flat,
                        BorderStyle = BorderStyle.FixedSingle
                    };

                    if (whatTeam == "A")
                    {
                        flp.Controls.Add(lbAo);
                        flp.Controls.Add(lbQuan);
                        lbAo.Location = new Point(5, 5);
                        lbQuan.Location = new Point(461, 5);
                    }
                    else
                    {
                        flp.Controls.Add(lbQuan);
                        flp.Controls.Add(lbAo);
                        lbQuan.Location = new Point(5, 5);
                        lbAo.Location = new Point(211, 5);
                    }

                    Bitmap bmp = new Bitmap(flp.Width, flp.Height);
                    flp.DrawToBitmap(bmp, flp.ClientRectangle);

                    Point locationAo = flp.PointToClient(lbAo.Parent.PointToScreen(lbAo.Location));
                    lbAo.DrawToBitmap(bmp, new Rectangle(locationAo, lbAo.Size));

                    Point locationQuan = flp.PointToClient(lbQuan.Parent.PointToScreen(lbQuan.Location));
                    lbQuan.DrawToBitmap(bmp, new Rectangle(locationQuan, lbQuan.Size));

                    string filePath = SaveController.folderPath + "\\MauAoQuan" + whatTeam + ".png";
                    bmp.Save(filePath, ImageFormat.Png);
                }
                catch { }
            }
        }
        //
        // Win rate
        //
        private void btnCalcWinRate_Click(object sender, EventArgs e)
        {
            calcWinRate(true);
        }
        private void calcWinRate(bool writeToLabel)
        {
            string winRate = string.Empty;

            float aWin = 33.3f;
            float bWin = 33.3f;
            float draw = 33.3f;

            float totalSecond = 0;
            float passSecond = 0;

            if (MatchController.toTime >= Convert.ToInt32(cbbFromTime.Text)) // Thời gian tăng
            {
                if (Convert.ToInt32(cbbFromTime.Text) >= (float)(MatchController.toTime / 3)) //Hiệp 2
                {
                    totalSecond = (MatchController.toTime + Convert.ToInt32(cbbBuGio.Text)) * 60;
                }
                else // Đang là Hiệp 1
                {
                    totalSecond = (MatchController.toTime * 2 + Convert.ToInt32(cbbBuGio.Text)) * 60;
                }
                passSecond = getMinuteFromLabel(lbThoiGianChinhThuc) * 60 + getSecondFromLabel(lbThoiGianChinhThuc)
                           + getMinuteFromLabel(lbThoiGianBuGio) * 60 + getSecondFromLabel(lbThoiGianBuGio);
            }
            else // Thời gian giảm
            {
                float totalHalfSecond = (Convert.ToInt32(cbbFromTime.Text) - MatchController.toTime) * 60;
                totalSecond = totalHalfSecond * 2 + Convert.ToInt32(cbbBuGio.Text) * 60;
                passSecond = (Convert.ToInt32(cbbFromTime.Text) - getMinuteFromLabel(lbThoiGianChinhThuc)) * 60
                            - getSecondFromLabel(lbThoiGianChinhThuc)

                            + getMinuteFromLabel(lbThoiGianBuGio) * 60
                            + getSecondFromLabel(lbThoiGianBuGio);

                if (cbbHiepDau.Text != "Hiệp 1") passSecond += totalHalfSecond;
            }

            if (passSecond > totalSecond) passSecond = totalSecond;

            float ratioPassTotal = passSecond / totalSecond;

            float tiSoA = Convert.ToInt32(nudTiSoA.Value);
            float tiSoB = Convert.ToInt32(nudTiSoB.Value);

            float ratioDeltaGoal = (float)Math.Pow(Math.Abs(tiSoA - tiSoB), 0.8)
                / (float)Math.Pow(totalSecond / passSecond, Math.Pow(totalSecond / (totalSecond - passSecond), 0.88));
            if (ratioDeltaGoal > 1) ratioDeltaGoal = 1;

            float ratioAdd = ratioDeltaGoal * 66.7f;

            if (tiSoA > tiSoB)
            {
                aWin += ratioAdd;
                if (ratioPassTotal < 1) aWin = Math.Min(aWin, 99.9f);
                bWin = (100 - aWin) * 1 / 2 * (1 - ratioDeltaGoal * ratioPassTotal);
            }
            else if (tiSoA < tiSoB)
            {
                bWin += ratioAdd;
                if (ratioPassTotal < 1) bWin = Math.Min(bWin, 99.9f);
                aWin = (100 - bWin) * 1 / 2 * (1 - ratioDeltaGoal * ratioPassTotal);
            }
            else
            {
                draw += (float)Math.Pow(ratioPassTotal, 1 + (ratioPassTotal)) * 66.7f;
                aWin = (100 - draw) * 1 / 2;
                bWin = (100 - draw) * 1 / 2;
            }

            aWin = (float)Math.Floor(aWin * 10) / 10;
            bWin = (float)Math.Floor(bWin * 10) / 10;

            draw = 100 - aWin - bWin;
            draw = (float)Math.Round(draw, 1);

            if (writeToLabel) lbWinRate.Text = $"{lbGhiBanA.Text}: {aWin}%.  {lbGhiBanB.Text}: {bWin}%.  Hoà: {draw}%";

            taoAnhWinRate(aWin, bWin, draw);
        }
        private void taoAnhWinRate(float aWin, float bWin, float draw)
        {
            if (Directory.Exists(SaveController.folderPath))
            {
                try
                {
                    Panel pnlBackGround = new Panel
                    {
                        BackColor = Color.Black,
                        Size = new Size(604, 175)
                    };

                    Label lbTitle = new Label
                    {
                        BackColor = Color.Transparent,
                        ForeColor = Color.Yellow,
                        Font = Default.fontRoboto16,
                        Text = "Cơ hội chiến thắng",
                        TextAlign = ContentAlignment.MiddleCenter,
                        Size = new Size(536, 30),
                        Location = new Point(34, 12)
                    };

                    Label lbWinRateBar = new Label
                    {
                        BackColor = Color.White,
                        Size = new Size(536, 22),
                        Location = new Point(34, 100),
                        BorderStyle = BorderStyle.FixedSingle
                    };

                    Label lbABar = new Label
                    {
                        AutoSize = false,
                        FlatStyle = FlatStyle.Flat,
                        BorderStyle = BorderStyle.FixedSingle,
                        Location = new Point(34, 100)
                    };
                    int widthA = (int)Math.Round(aWin / 100 * 536);
                    //lbABar.Size = new Size(widthA, 22);
                    if (aWin < bWin) lbABar.BackColor = Color.Red;
                    else lbABar.BackColor = Color.DeepSkyBlue;


                    Label lbBBar = new Label
                    {
                        AutoSize = false,
                        FlatStyle = FlatStyle.Flat,
                        BorderStyle = BorderStyle.FixedSingle
                    };
                    int widthB = (int)Math.Round(bWin / 100 * 536);
                    //lbBBar.Size = new Size(widthB, 22);
                    //lbBBar.Location = new Point(570 - widthB, 100);
                    if (bWin < aWin) lbBBar.BackColor = Color.Red;
                    else lbBBar.BackColor = Color.DeepSkyBlue;

                    if (widthA < 3 && aWin != 0)
                    {
                        widthA = 3;
                        widthB -= 3;
                    }
                    if (widthB < 3 && bWin != 0)
                    {
                        widthB = 3;
                        widthA -= 3;
                    }
                    lbABar.Size = new Size(widthA, 22);
                    lbBBar.Size = new Size(widthB, 22);
                    lbBBar.Location = new Point(570 - widthB, 100);

                    Label lbTenA = new Label
                    {
                        BackColor = Color.Transparent,
                        ForeColor = Color.White,
                        Font = Default.fontRoboto16,
                        Text = grbDoiA.Text,
                        TextAlign = ContentAlignment.MiddleLeft,
                        Size = new Size(268, 30),
                        Location = new Point(34, 60)
                    };

                    Label lbTenB = new Label
                    {
                        BackColor = Color.Transparent,
                        ForeColor = Color.White,
                        Font = Default.fontRoboto16,
                        Text = grbDoiB.Text,
                        TextAlign = ContentAlignment.MiddleRight,
                        Size = new Size(268, 30),
                        Location = new Point(302, 60)
                    };

                    Label lbWinRateA = new Label
                    {
                        BackColor = Color.Transparent,
                        ForeColor = Color.White,
                        Font = Default.fontRoboto16,
                        Text = aWin.ToString() + "%",
                        TextAlign = ContentAlignment.MiddleLeft,
                        Size = new Size(100, 30),
                        Location = new Point(34, 128)
                    };

                    Label lbWinRateB = new Label
                    {
                        BackColor = Color.Transparent,
                        ForeColor = Color.White,
                        Font = Default.fontRoboto16,
                        Text = bWin.ToString() + "%",
                        TextAlign = ContentAlignment.MiddleRight,
                        Size = new Size(100, 30),
                        Location = new Point(470, 128)
                    };

                    Label lbDraw = new Label
                    {
                        BackColor = Color.Transparent,
                        ForeColor = Color.White,
                        Font = Default.fontRoboto16,
                        Text = "Hoà: " + draw.ToString() + "%",
                        TextAlign = ContentAlignment.MiddleCenter,
                        Size = new Size(140, 30),
                        Location = new Point(232, 138)
                    };

                    pnlBackGround.Controls.Add(lbTitle);

                    pnlBackGround.Controls.Add(lbWinRateBar);
                    pnlBackGround.Controls.Add(lbABar);
                    pnlBackGround.Controls.Add(lbBBar);

                    pnlBackGround.Controls.Add(lbTenA);
                    pnlBackGround.Controls.Add(lbTenB);

                    pnlBackGround.Controls.Add(lbWinRateA);
                    pnlBackGround.Controls.Add(lbWinRateB);
                    pnlBackGround.Controls.Add(lbDraw);

                    Bitmap bmp = new Bitmap(pnlBackGround.Width, pnlBackGround.Height);
                    pnlBackGround.DrawToBitmap(bmp, pnlBackGround.ClientRectangle);

                    foreach (Control control in pnlBackGround.Controls)
                    {
                        try
                        {
                            Label label = (Label)control;
                            Point location = pnlBackGround.PointToClient(label.Parent.PointToScreen(label.Location));
                            label.DrawToBitmap(bmp, new Rectangle(location, label.Size));
                        }
                        catch { }
                    }

                    string filePath = SaveController.folderPath + Default.coHoiChienThang;
                    bmp.Save(filePath, ImageFormat.Png);
                }
                catch { }
            }
        }
        //
        // Cài đặt AppData
        //
        private void btnChonAppDataCu_Click(object sender, EventArgs e)
        {
            btnChonAppDataCu_Click();
        }
        private void btnChonAppDataCu_Click()
        {
            CommonOpenFileDialog cOFD = new CommonOpenFileDialog
            {
                Title = "Chọn thư mục AppData cũ",
                IsFolderPicker = true,
                Multiselect = false,
                RestoreDirectory = true
            };

            if (cOFD.ShowDialog() == CommonFileDialogResult.Ok)
            {
                txbOldAppDataFolderPath.Text = cOFD.FileName;
                btnCopyAppDataFolder.Enabled = true;
            }
        }
        private void loadNewAppDataFolderPath()
        {
            try
            {
                txbNewAppDataFolderPath.Text = Default.newAppDataFolderPath;

                if (!File.Exists(Default.newAppDataFolderPath + "\\AppData.json") || !Directory.Exists(ACSList.ACSFolderPath))
                {
                    try
                    {
                        string pathJson = Directory.GetParent(Application.StartupPath).FullName
                            + "\\FootballKit 3.3 by CZH\\AppData\\AppData.json";
                        string pathACS = Directory.GetParent(Application.StartupPath).FullName
                            + "\\FootballKit 3.3 by CZH\\AppData\\ACS";

                        if (File.Exists(pathJson) && Directory.Exists(pathACS))
                        {
                            txbOldAppDataFolderPath.Text = Directory.GetParent(Application.StartupPath).FullName
                                + "\\FootballKit 3.3 by CZH\\AppData";
                        }
                    }
                    catch
                    {

                    }
                }
                else
                {
                    txbOldAppDataFolderPath.Text = "Đã có thư mục AppData. Không cần Copy!";
                    btnCopyAppDataFolder.Enabled = false;
                }
            }
            catch { }
        }
        private void btnCopyAppDataFolder_MouseClick(object sender, MouseEventArgs e)
        {
            btnCopyAppDataFolder_MouseClick();
        }
        private void btnCopyAppDataFolder_MouseClick()
        {
            try
            {
                string sourceFolderPath = txbOldAppDataFolderPath.Text;
                string destinationFolderPath = txbNewAppDataFolderPath.Text;

                // Copy the folder and its contents recursively
                if (File.Exists(sourceFolderPath + "\\AppData.json") && Directory.Exists(sourceFolderPath + "\\ACS"))
                {
                    if (!Directory.Exists(destinationFolderPath)) Directory.CreateDirectory(destinationFolderPath);
                    foreach (string dirPath in Directory.GetDirectories(sourceFolderPath, "*", SearchOption.AllDirectories))
                    {
                        try
                        {
                            string newDirPath = dirPath.Replace(sourceFolderPath, destinationFolderPath);
                            if (!Directory.Exists(newDirPath))
                                Directory.CreateDirectory(newDirPath);
                        }
                        catch { }
                    }
                    foreach (string filePath in Directory.GetFiles(sourceFolderPath, "*", SearchOption.AllDirectories))
                    {
                        try
                        {
                            File.Copy(filePath, filePath.Replace(sourceFolderPath, destinationFolderPath), true);
                        }
                        catch { }
                    }
                    updateNewAppData();
                    MessageBox.Show("Đã copy thư mục AppData sang thư mục mới.",
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnCopyAppDataFolder.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Đường dẫn không hợp lệ hoặc thư mục AppData cũ không đúng định dạng.",
                        "Lỗi: Thư mục AppData", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
               
            }
        }
    }
}
