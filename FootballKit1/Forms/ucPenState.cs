using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace FootballKit1
{
    public partial class ucPenState : UserControl
    {
        List<Label> listLbPenState;

        public ucPenState()
        {
            InitializeComponent();

            listLbPenState = new List<Label>() {lbP1, lbP2, lbP3, lbP4, lbP5 };
            cmsPen.RightToLeft = RightToLeft.No;

        }

        public void convertListPenStateToImage(List<int> listPenState)
        {
            int i = 0;
            foreach (int penState in listPenState)
            {
                switch (penState)
                {
                    case 1: listLbPenState[i].ForeColor = Default.colorVao; break; //Vào
                    case 0: listLbPenState[i].ForeColor = Default.colorTach; break; //Tạch
                    case 2: listLbPenState[i].ForeColor = Default.colorReset; break; //Reset
                    default: listLbPenState[i].ForeColor = Default.colorReset; break;
                }
                i++;
            }
        }
        //
        public void resetAUcPenState()
        {
            foreach (Label lb in listLbPenState)
            {
                lb.ForeColor = Default.colorReset;
            }
        }
        // Vào
        private void tsmiVao_Click(object sender, EventArgs e)
        {
            tsmiVaoClick(sender);
        }
        private void tsmiVaoClick(object sender)
        {
            ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
            ContextMenuStrip cms = (ContextMenuStrip)tsmi.Owner;
            Label lb = ((Label)cms.SourceControl);
            if ((string)Tag == "A")
            {
                if (lb.ForeColor != Default.colorVao)
                {
                    PensController.tiSoLuanLuuA++;
                    PensController.fPenalties.changeTiSoA();
                }
                PensController.listPenFrame[PensController.currentIdPenFrame].listPenStateA[Convert.ToInt32(lb.Tag)] = 1;
                copyImg(Default.imgPathVao, Default.listImgA[Convert.ToInt32(lb.Tag)]);

            }
            else if ((string)Tag == "B")
            {
                if (lb.ForeColor != Default.colorVao)
                {
                    PensController.tiSoLuanLuuB++;
                    PensController.fPenalties.changeTiSoB();
                }
                PensController.listPenFrame[PensController.currentIdPenFrame].listPenStateB[Convert.ToInt32(lb.Tag)] = 1;
                copyImg(Default.imgPathVao, Default.listImgB[Convert.ToInt32(lb.Tag)]);
            }
            lb.ForeColor = Default.colorVao;
            MatchController.fMain.isSaved = false;
        }
        // Tạch
        private void tsmiTach_Click(object sender, EventArgs e)
        {
            tsmiTachClick(sender);
        }
        private void tsmiTachClick(object sender)
        {
            ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
            ContextMenuStrip cms = (ContextMenuStrip)tsmi.Owner;
            Label lb = ((Label)cms.SourceControl);

            if ((string)Tag == "A")
            {
                if (lb.ForeColor == Default.colorVao)
                {
                    PensController.tiSoLuanLuuA--;
                    PensController.fPenalties.changeTiSoA();
                }
                PensController.listPenFrame[PensController.currentIdPenFrame].listPenStateA[Convert.ToInt32(lb.Tag)] = 0;
                copyImg(Default.imgPathTach, Default.listImgA[Convert.ToInt32(lb.Tag)]);
            }
            else if ((string)Tag == "B")
            {
                if (lb.ForeColor == Default.colorVao)
                {
                    PensController.tiSoLuanLuuB--;
                    PensController.fPenalties.changeTiSoB();
                }
                PensController.listPenFrame[PensController.currentIdPenFrame].listPenStateB[Convert.ToInt32(lb.Tag)] = 0;
                copyImg(Default.imgPathTach, Default.listImgB[Convert.ToInt32(lb.Tag)]);
            }
            lb.ForeColor = Default.colorTach;
            MatchController.fMain.isSaved = false;
        }
        private void tsmiReset_Click(object sender, EventArgs e)
        {
            tsmiResetClick(sender);
        }
        private void tsmiResetClick(object sender)
        {
            ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
            ContextMenuStrip cms = (ContextMenuStrip)tsmi.Owner;
            Label lb = ((Label)cms.SourceControl);

            if ((string)Tag == "A")
            {
                if (lb.ForeColor == Default.colorVao)
                {
                    PensController.tiSoLuanLuuA--;
                    PensController.fPenalties.changeTiSoA();
                }
                PensController.listPenFrame[PensController.currentIdPenFrame].listPenStateA[Convert.ToInt32(lb.Tag)] = 2;
                copyImg(Default.imgPathReset, Default.listImgA[Convert.ToInt32(lb.Tag)]);
            }
            else if ((string)Tag == "B")
            {
                if (lb.ForeColor == Default.colorVao)
                {
                    PensController.tiSoLuanLuuB--;
                    PensController.fPenalties.changeTiSoB();
                }
                PensController.listPenFrame[PensController.currentIdPenFrame].listPenStateB[Convert.ToInt32(lb.Tag)] = 2;
                copyImg(Default.imgPathReset, Default.listImgB[Convert.ToInt32(lb.Tag)]);
            }
            lb.ForeColor = Default.colorReset;
            MatchController.fMain.isSaved = false;
        }
        // Reset All
        private void resetTatCaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            resetTatCaToolStripMenuItemClick();
        }
        private void resetTatCaToolStripMenuItemClick()
        {
            DialogResult diaRes = MessageBox.Show("Reset kết quả luân lưu 2 đội?",
                        "Reset luân lưu", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (diaRes == DialogResult.OK)
            {
                resetPenalties2Doi();
            }
        }
        private void resetPenalties2Doi()
        {
            PensController.fPenalties.resetAll();
            MatchController.fMain.isSaved = false;
        }
        // UI/UX
        private void lbP_MouseHover(object sender, EventArgs e)
        {
            Label lb = (Label)sender;
            lb.BackColor = Color.Gold;
        }

        private void lbP_MouseLeave(object sender, EventArgs e)
        {
            Label lb = (Label)sender;
            lb.BackColor = Color.Black;
        }
        private void copyImg(string srcFile, string desFile)
        {
            if (SaveController.folderPath != null & Directory.Exists(SaveController.folderPath))
            {
                if (File.Exists(srcFile))
                {
                    File.Copy(srcFile, SaveController.folderPath + desFile, true);
                }
            }
        }
    }
}
