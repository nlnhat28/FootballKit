namespace FootballKit1
{
    partial class fPenalties
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fPenalties));
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmsResetTiSoLuanLuu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.resetTiSoLuanLuu2DoiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lbPrevious = new System.Windows.Forms.Label();
            this.lbNext = new System.Windows.Forms.Label();
            this.lbHeader = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lbLuanLuuMauB = new System.Windows.Forms.Label();
            this.lbLuanLuuTenB = new System.Windows.Forms.Label();
            this.lbTiSoLuanLuuB2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbLuanLuuMauA = new System.Windows.Forms.Label();
            this.lbLuanLuuTenA = new System.Windows.Forms.Label();
            this.lbTiSoLuanLuuA2 = new System.Windows.Forms.Label();
            this.ttPenalties = new System.Windows.Forms.ToolTip(this.components);
            this.ucPenStateB = new FootballKit1.ucPenState();
            this.ucPenStateA = new FootballKit1.ucPenState();
            this.panel1.SuspendLayout();
            this.cmsResetTiSoLuanLuu.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.BlueViolet;
            this.panel1.ContextMenuStrip = this.cmsResetTiSoLuanLuu;
            this.panel1.Controls.Add(this.lbPrevious);
            this.panel1.Controls.Add(this.lbNext);
            this.panel1.Controls.Add(this.lbHeader);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(6, 7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(538, 314);
            this.panel1.TabIndex = 0;
            // 
            // cmsResetTiSoLuanLuu
            // 
            this.cmsResetTiSoLuanLuu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetTiSoLuanLuu2DoiToolStripMenuItem});
            this.cmsResetTiSoLuanLuu.Name = "cmsResetTiSoLuanLuu";
            this.cmsResetTiSoLuanLuu.Size = new System.Drawing.Size(203, 26);
            this.cmsResetTiSoLuanLuu.Text = "Reset tất cả";
            // 
            // resetTiSoLuanLuu2DoiToolStripMenuItem
            // 
            this.resetTiSoLuanLuu2DoiToolStripMenuItem.Image = global::FootballKit1.Properties.Resources.Reset;
            this.resetTiSoLuanLuu2DoiToolStripMenuItem.Name = "resetTiSoLuanLuu2DoiToolStripMenuItem";
            this.resetTiSoLuanLuu2DoiToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.resetTiSoLuanLuu2DoiToolStripMenuItem.Text = "Reset tỉ số luân lưu 2 đội";
            this.resetTiSoLuanLuu2DoiToolStripMenuItem.Click += new System.EventHandler(this.resetTiSoLuanLuu2DoiToolStripMenuItem_Click);
            // 
            // lbPrevious
            // 
            this.lbPrevious.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbPrevious.AutoSize = true;
            this.lbPrevious.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbPrevious.Font = new System.Drawing.Font("Roboto Condensed", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPrevious.ForeColor = System.Drawing.Color.Cyan;
            this.lbPrevious.Location = new System.Drawing.Point(119, 34);
            this.lbPrevious.Name = "lbPrevious";
            this.lbPrevious.Size = new System.Drawing.Size(33, 44);
            this.lbPrevious.TabIndex = 38;
            this.lbPrevious.Text = "-";
            this.lbPrevious.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ttPenalties.SetToolTip(this.lbPrevious, "Trở về 5 lượt trước");
            this.lbPrevious.Click += new System.EventHandler(this.lbPrevious_Click);
            // 
            // lbNext
            // 
            this.lbNext.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbNext.AutoSize = true;
            this.lbNext.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbNext.Font = new System.Drawing.Font("Roboto Condensed", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNext.ForeColor = System.Drawing.Color.Cyan;
            this.lbNext.Location = new System.Drawing.Point(411, 34);
            this.lbNext.Name = "lbNext";
            this.lbNext.Size = new System.Drawing.Size(38, 44);
            this.lbNext.TabIndex = 35;
            this.lbNext.Text = "+";
            this.lbNext.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ttPenalties.SetToolTip(this.lbNext, "Chuyển sang 5 lượt tiếp");
            this.lbNext.Click += new System.EventHandler(this.lbNext_Click);
            // 
            // lbHeader
            // 
            this.lbHeader.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbHeader.AutoSize = true;
            this.lbHeader.Font = new System.Drawing.Font("Roboto Condensed", 21.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHeader.ForeColor = System.Drawing.Color.White;
            this.lbHeader.Location = new System.Drawing.Point(158, 40);
            this.lbHeader.Name = "lbHeader";
            this.lbHeader.Size = new System.Drawing.Size(249, 35);
            this.lbHeader.TabIndex = 35;
            this.lbHeader.Text = "01   02   03   04   05";
            // 
            // panel3
            // 
            this.panel3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.Controls.Add(this.ucPenStateB);
            this.panel3.Controls.Add(this.lbLuanLuuMauB);
            this.panel3.Controls.Add(this.lbLuanLuuTenB);
            this.panel3.Controls.Add(this.lbTiSoLuanLuuB2);
            this.panel3.Location = new System.Drawing.Point(6, 184);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(520, 84);
            this.panel3.TabIndex = 37;
            // 
            // lbLuanLuuMauB
            // 
            this.lbLuanLuuMauB.AutoSize = true;
            this.lbLuanLuuMauB.BackColor = System.Drawing.Color.Blue;
            this.lbLuanLuuMauB.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbLuanLuuMauB.Font = new System.Drawing.Font("Roboto Condensed", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLuanLuuMauB.Location = new System.Drawing.Point(18, 21);
            this.lbLuanLuuMauB.Name = "lbLuanLuuMauB";
            this.lbLuanLuuMauB.Size = new System.Drawing.Size(23, 35);
            this.lbLuanLuuMauB.TabIndex = 30;
            this.lbLuanLuuMauB.Text = " ";
            // 
            // lbLuanLuuTenB
            // 
            this.lbLuanLuuTenB.Font = new System.Drawing.Font("Roboto Condensed", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLuanLuuTenB.ForeColor = System.Drawing.Color.White;
            this.lbLuanLuuTenB.Location = new System.Drawing.Point(51, 24);
            this.lbLuanLuuTenB.Name = "lbLuanLuuTenB";
            this.lbLuanLuuTenB.Size = new System.Drawing.Size(222, 29);
            this.lbLuanLuuTenB.TabIndex = 27;
            this.lbLuanLuuTenB.Text = "Đội B";
            this.lbLuanLuuTenB.MouseHover += new System.EventHandler(this.lbB_MouseHover);
            // 
            // lbTiSoLuanLuuB2
            // 
            this.lbTiSoLuanLuuB2.AutoSize = true;
            this.lbTiSoLuanLuuB2.Font = new System.Drawing.Font("Roboto Condensed", 39.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTiSoLuanLuuB2.ForeColor = System.Drawing.Color.Gold;
            this.lbTiSoLuanLuuB2.Location = new System.Drawing.Point(420, 8);
            this.lbTiSoLuanLuuB2.Name = "lbTiSoLuanLuuB2";
            this.lbTiSoLuanLuuB2.Size = new System.Drawing.Size(54, 63);
            this.lbTiSoLuanLuuB2.TabIndex = 34;
            this.lbTiSoLuanLuuB2.Text = "0";
            this.lbTiSoLuanLuuB2.TextChanged += new System.EventHandler(this.lbTiSoLuanLuuB2_TextChanged);
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.ucPenStateA);
            this.panel2.Controls.Add(this.lbLuanLuuMauA);
            this.panel2.Controls.Add(this.lbLuanLuuTenA);
            this.panel2.Controls.Add(this.lbTiSoLuanLuuA2);
            this.panel2.Location = new System.Drawing.Point(6, 94);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(520, 84);
            this.panel2.TabIndex = 36;
            // 
            // lbLuanLuuMauA
            // 
            this.lbLuanLuuMauA.AutoSize = true;
            this.lbLuanLuuMauA.BackColor = System.Drawing.Color.Red;
            this.lbLuanLuuMauA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbLuanLuuMauA.Font = new System.Drawing.Font("Roboto Condensed", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLuanLuuMauA.Location = new System.Drawing.Point(18, 21);
            this.lbLuanLuuMauA.Name = "lbLuanLuuMauA";
            this.lbLuanLuuMauA.Size = new System.Drawing.Size(23, 35);
            this.lbLuanLuuMauA.TabIndex = 30;
            this.lbLuanLuuMauA.Text = " ";
            // 
            // lbLuanLuuTenA
            // 
            this.lbLuanLuuTenA.Font = new System.Drawing.Font("Roboto Condensed", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLuanLuuTenA.ForeColor = System.Drawing.Color.White;
            this.lbLuanLuuTenA.Location = new System.Drawing.Point(51, 24);
            this.lbLuanLuuTenA.Name = "lbLuanLuuTenA";
            this.lbLuanLuuTenA.Size = new System.Drawing.Size(222, 29);
            this.lbLuanLuuTenA.TabIndex = 27;
            this.lbLuanLuuTenA.Text = "Đội A";
            this.lbLuanLuuTenA.MouseHover += new System.EventHandler(this.lbA_MouseHover);
            // 
            // lbTiSoLuanLuuA2
            // 
            this.lbTiSoLuanLuuA2.AutoSize = true;
            this.lbTiSoLuanLuuA2.Font = new System.Drawing.Font("Roboto Condensed", 39.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTiSoLuanLuuA2.ForeColor = System.Drawing.Color.Gold;
            this.lbTiSoLuanLuuA2.Location = new System.Drawing.Point(420, 8);
            this.lbTiSoLuanLuuA2.Name = "lbTiSoLuanLuuA2";
            this.lbTiSoLuanLuuA2.Size = new System.Drawing.Size(54, 63);
            this.lbTiSoLuanLuuA2.TabIndex = 34;
            this.lbTiSoLuanLuuA2.Text = "0";
            this.lbTiSoLuanLuuA2.TextChanged += new System.EventHandler(this.lbTiSoLuanLuuA2_TextChanged);
            // 
            // ttPenalties
            // 
            this.ttPenalties.IsBalloon = true;
            // 
            // ucPenStateB
            // 
            this.ucPenStateB.BackColor = System.Drawing.Color.Orange;
            this.ucPenStateB.Location = new System.Drawing.Point(143, 5);
            this.ucPenStateB.Margin = new System.Windows.Forms.Padding(5, 8, 5, 8);
            this.ucPenStateB.Name = "ucPenStateB";
            this.ucPenStateB.Size = new System.Drawing.Size(267, 63);
            this.ucPenStateB.TabIndex = 36;
            this.ucPenStateB.Tag = "B";
            // 
            // ucPenStateA
            // 
            this.ucPenStateA.BackColor = System.Drawing.Color.Orange;
            this.ucPenStateA.Location = new System.Drawing.Point(143, 5);
            this.ucPenStateA.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ucPenStateA.Name = "ucPenStateA";
            this.ucPenStateA.Size = new System.Drawing.Size(267, 63);
            this.ucPenStateA.TabIndex = 35;
            this.ucPenStateA.Tag = "A";
            // 
            // fPenalties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(552, 328);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(538, 302);
            this.Name = "fPenalties";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chi tiết luân lưu";
            this.Load += new System.EventHandler(this.fPenalties_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.cmsResetTiSoLuanLuu.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbLuanLuuMauA;
        private System.Windows.Forms.Label lbLuanLuuTenA;
        private System.Windows.Forms.Label lbTiSoLuanLuuA2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lbLuanLuuMauB;
        private System.Windows.Forms.Label lbLuanLuuTenB;
        private System.Windows.Forms.Label lbTiSoLuanLuuB2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lbHeader;
        private System.Windows.Forms.Label lbNext;
        private System.Windows.Forms.Label lbPrevious;
        private ucPenState ucPenStateA;
        private ucPenState ucPenStateB;
        private System.Windows.Forms.ContextMenuStrip cmsResetTiSoLuanLuu;
        private System.Windows.Forms.ToolStripMenuItem resetTiSoLuanLuu2DoiToolStripMenuItem;
        private System.Windows.Forms.ToolTip ttPenalties;
    }
}