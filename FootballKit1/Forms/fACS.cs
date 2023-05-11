using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FootballKit1
{
    public partial class fACS : Form
    {
        private int lastSelectedId = 0;
        private string lastContent = string.Empty;

        public fACS()
        {
            lastContent = listStringToTxb(ACSList.listACS[lastSelectedId].listString);
            InitializeComponent();
        }

        private void fACS_Load(object sender, EventArgs e)
        {
            fACS_Load();
        }
        private void fACS_Load()
        {
            // Load combobox
            List<string> listCbbTitle = new List<string>();
            foreach (ACS item in ACSList.listACS)
            {
                listCbbTitle.Add(item.title);
            }
            cbbACS.DataSource = listCbbTitle;

            // Resize
            fACS_Resize();
        }

        private void fACS_FormClosing(object sender, FormClosingEventArgs e)
        {
            fACS_FormClosing();
        }
        private void fACS_FormClosing()
        {
            saveACS(cbbACS.SelectedIndex, txbACS.Text);
        }
        //
        // Cbb Selected Index Changed
        //
        private void cbbACS_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbbACS_SelectedIndexChanged();
        }
        private void cbbACS_SelectedIndexChanged()
        {
            saveACS(lastSelectedId, lastContent);
            int i = cbbACS.SelectedIndex;
            txbACS.Text = listStringToTxb(ACSList.listACS[i].listString);
            lastSelectedId = i;
        }
        private string listStringToTxb(List<string> listString)
        {
            string strACS = string.Empty;
            foreach (string item in listString)
            {
                strACS += item + "\r\n";
            }
            return strACS;
        }
        private void fACS_Resize(object sender, EventArgs e)
        {
            fACS_Resize();
        }
        private void fACS_Resize()
        {
            float fontSize = Math.Max(Width / 50f, 14f);
            Font newFont = new Font(txbACS.Font.FontFamily, fontSize, txbACS.Font.Style);
            txbACS.Font = newFont;
        }
        //
        // Load txt file
        //
        private void ptbLoadFromTxtACS_Click(object sender, EventArgs e)
        {
            ptbLoadFromTxtACS_Click();
        }
        private void ptbLoadFromTxtACS_Click()
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
                        txbACS.Text = File.ReadAllText(filePath);
                    }
                    catch
                    {
                        MessageBox.Show("File không tồn tại hoặc không đọc được nội dung file.",
                            "Lỗi: Load file txt", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    };
                }
            }
        }
        //
        // Lưu ACS
        //
        private void saveACS(int i, string content)
        {
            try
            {
                string path = ACSList.listACS[i].txtFile;
                ACSList.listACS[i].listString = addLinesTxbToList(content);
                if (File.Exists(path)) File.WriteAllText(path, content);
                //showNotifySuccess("Đã lưu");
            }
            catch { }
        }
        private void showNotifySuccess(string title)
        {
            if (Default.iconSuccess != null) FootballKit.Icon = Default.iconSuccess;
            FootballKit.ShowBalloonTip(Default.timeShowNotify, title, " ", ToolTipIcon.None);
        }
        //
        // Add lines in Textbox to list
        //
        private List<string> addLinesTxbToList(string content)
        {
            List<string> newList = new List<string>();
            string[] linesArray = content.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            foreach (string line in linesArray)
            {
                if (!newList.Contains(line) && !string.IsNullOrWhiteSpace(line))
                    newList.Add(line);
            }
            return newList;
        }
        // 
        // Textbox Changed
        //
        private void txbACS_TextChanged(object sender, EventArgs e)
        {
            lastContent = txbACS.Text;
        }
    }
}
