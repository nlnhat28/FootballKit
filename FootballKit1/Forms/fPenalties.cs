using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace FootballKit1
{
    public partial class fPenalties : Form
    {
       
        public fPenalties()
        {
            InitializeComponent();
        }
        private void fPenalties_Load(object sender, EventArgs e)
        {
            loadForm();
        }
        private void loadForm()
        {
            lbLuanLuuTenA.Text = MatchController.vietTatA;
            lbLuanLuuTenB.Text = MatchController.vietTatB;

            lbLuanLuuMauA.BackColor = MatchController.mauA;
            lbLuanLuuMauB.BackColor = MatchController.mauB;

            changeTiSoA();
            changeTiSoB();

            convertDataToViewAndCopyImgToPenFolder();

            if (PensController.currentIdPenFrame == PensController.listPenFrame.Count - 1)
            {
                lbNext.Enabled = false;
            }
            else if (PensController.currentIdPenFrame == 0)
            {
                lbPrevious.Enabled = false;
            }
            else lbNext.Enabled = lbPrevious.Enabled = true;
        }
        // Change Color
        public void changeColorA()
        {
            lbLuanLuuMauA.BackColor = MatchController.mauA;
        }
        public void changeColorB()
        {
            lbLuanLuuMauB.BackColor = MatchController.mauB;
        }
        // Reset
        private void resetTiSoLuanLuu2DoiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            resetTiSoLuanLuu2DoiToolStripMenuItem_Click();
        }
        private void resetTiSoLuanLuu2DoiToolStripMenuItem_Click()
        {
            DialogResult diaRes = MessageBox.Show("Reset kết quả luân lưu 2 đội?",
                        "Reset luân lưu", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (diaRes == DialogResult.OK)
            {
                resetAll();
            }
        }
        public void resetAll()
        {
            PensController.tiSoLuanLuuA = PensController.tiSoLuanLuuB = 0;
            changeTiSoA();
            changeTiSoB();
            PensController.resetListPenFrame();
            convertDataToViewAndCopyImgToPenFolder();
            ucPenStateA.resetAUcPenState();
            ucPenStateB.resetAUcPenState();
        }
        //
        // Switch Pen Frame
        //
        private void lbNext_Click(object sender, EventArgs e)
        {
            lbNextClick();
        }
        private void lbNextClick()
        {
            PensController.currentIdPenFrame++;
            convertDataToViewAndCopyImgToPenFolder();

            // Unable button
            if (PensController.currentIdPenFrame == PensController.listPenFrame.Count - 1)
            {
                lbNext.Enabled = false;
            }
            lbPrevious.Enabled = true;
        }
        private void lbPrevious_Click(object sender, EventArgs e)
        {
            lbPreviousClick();
        }
        private void lbPreviousClick()
        {
            PensController.currentIdPenFrame--;
            convertDataToViewAndCopyImgToPenFolder();

            // Unable button
            if (PensController.currentIdPenFrame == 0)
            {
                lbPrevious.Enabled = false;
            }
            lbNext.Enabled = true;
        }
        private void convertDataToViewAndCopyImgToPenFolder()
        {
            PenFrame penFrame = PensController.listPenFrame[PensController.currentIdPenFrame];
            convertPenFrameToView(penFrame);
            copyDataToPenFolder(penFrame);
        }
        //
        // Convert Pen Frame to view
        //
        public void convertPenFrameToView(PenFrame penFrame)
        {
            lbHeader.Text = ListDataSource.headerPen[PensController.currentIdPenFrame];
            ucPenStateA.convertListPenStateToImage(penFrame.listPenStateA);
            ucPenStateB.convertListPenStateToImage(penFrame.listPenStateB);
        }
        // Copy Image from Res to Pen folder
        public void copyDataToPenFolder(PenFrame penFrame)
        {
            if (SaveController.folderPath != null & Directory.Exists(SaveController.folderPath))
            {
                File.WriteAllText(SaveController.folderPath + TxtFiles.luotPen, lbHeader.Text);
                copyImgResToPenFolder2(penFrame.listPenStateA, Default.listImgA);
                copyImgResToPenFolder2(penFrame.listPenStateB, Default.listImgB);
            }
        }
        private void copyImgResToPenFolder2(List<int> listPenState, List<string> listImg)
        {
            string folderPath = SaveController.folderPath;
            try
            {
                int i = 0;
                foreach (int penState in listPenState)
                {
                    switch (penState)
                    {
                        case 1: //Vào
                            if (File.Exists(Default.imgPathVao))
                            {
                                File.Copy(Default.imgPathVao, folderPath + listImg[i], true);
                            }
                            break;
                        case 0:
                            if (File.Exists(Default.imgPathTach))
                            {
                                File.Copy(Default.imgPathTach, folderPath + listImg[i], true);
                            }
                            break;
                        case 2:
                            if (File.Exists(Default.imgPathReset))
                            {
                                File.Copy(Default.imgPathReset, folderPath + listImg[i], true);
                            }
                            break;
                        default:
                            if (File.Exists(Default.imgPathReset))
                            {
                                File.Copy(Default.imgPathReset, folderPath + listImg[i], true);
                            }
                            break;
                    }
                    i++;
                }
            }
            catch
            {
                MessageBox.Show("Không tìm thấy file ảnh trong Resources.",
                    "Lỗi: Load ảnh", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        //
        // Change tỉ số
        //
        public void changeTiSoA()
        {
            lbTiSoLuanLuuA2.Text = PensController.tiSoLuanLuuA.ToString();
            MatchController.fMain.changeNudValueA(lbTiSoLuanLuuA2.Text);
        }
        public void changeTiSoB()
        {
            lbTiSoLuanLuuB2.Text = PensController.tiSoLuanLuuB.ToString();
            MatchController.fMain.changeNudValueB(lbTiSoLuanLuuB2.Text);
        }

        private void lbTiSoLuanLuuA2_TextChanged(object sender, EventArgs e)
        {
            if (SaveController.folderPath != null & Directory.Exists(SaveController.folderPath))
            {
                File.WriteAllText(SaveController.folderPath + TxtFiles.tiSoLuanLuuA, lbTiSoLuanLuuA2.Text);
            }
        }

        private void lbTiSoLuanLuuB2_TextChanged(object sender, EventArgs e)
        {
            if (SaveController.folderPath != null & Directory.Exists(SaveController.folderPath))
            {
                File.WriteAllText(SaveController.folderPath + TxtFiles.tiSoLuanLuuB, lbTiSoLuanLuuB2.Text);
            }
        }
        // MouseHover
        private void lbA_MouseHover(object sender, EventArgs e)
        {
            try
            {
                ttPenalties.SetToolTip(lbLuanLuuTenA, MatchController.tenA);
            }
            catch { }
        }
        private void lbB_MouseHover(object sender, EventArgs e)
        {
            try
            {
                ttPenalties.SetToolTip(lbLuanLuuTenB, MatchController.tenB);
            }
            catch { }
        }
    }
}
