namespace FootballKit1
{
    partial class ucPenState
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cmsPen = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiVao = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTach = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiReset = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.resetTatCaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.lbP5 = new System.Windows.Forms.Label();
            this.lbP4 = new System.Windows.Forms.Label();
            this.lbP3 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.lbP1 = new System.Windows.Forms.Label();
            this.lbP2 = new System.Windows.Forms.Label();
            this.ttUcPenState = new System.Windows.Forms.ToolTip(this.components);
            this.cmsPen.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmsPen
            // 
            this.cmsPen.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmsPen.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiVao,
            this.tsmiTach,
            this.toolStripSeparator1,
            this.tsmiReset,
            this.toolStripSeparator2,
            this.resetTatCaToolStripMenuItem});
            this.cmsPen.Name = "cmsPen";
            this.cmsPen.Size = new System.Drawing.Size(154, 112);
            this.cmsPen.Text = "Kết quả cú sút";
            // 
            // tsmiVao
            // 
            this.tsmiVao.Image = global::FootballKit1.Properties.Resources.Vao;
            this.tsmiVao.Name = "tsmiVao";
            this.tsmiVao.Size = new System.Drawing.Size(153, 24);
            this.tsmiVao.Text = "Vào";
            this.tsmiVao.Click += new System.EventHandler(this.tsmiVao_Click);
            // 
            // tsmiTach
            // 
            this.tsmiTach.Image = global::FootballKit1.Properties.Resources.Tach;
            this.tsmiTach.Name = "tsmiTach";
            this.tsmiTach.Size = new System.Drawing.Size(153, 24);
            this.tsmiTach.Text = "Tạch";
            this.tsmiTach.Click += new System.EventHandler(this.tsmiTach_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(150, 6);
            // 
            // tsmiReset
            // 
            this.tsmiReset.Image = global::FootballKit1.Properties.Resources.ChuaSut;
            this.tsmiReset.Name = "tsmiReset";
            this.tsmiReset.Size = new System.Drawing.Size(153, 24);
            this.tsmiReset.Text = "Reset";
            this.tsmiReset.Click += new System.EventHandler(this.tsmiReset_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(150, 6);
            // 
            // resetTatCaToolStripMenuItem
            // 
            this.resetTatCaToolStripMenuItem.Image = global::FootballKit1.Properties.Resources.Reset;
            this.resetTatCaToolStripMenuItem.Name = "resetTatCaToolStripMenuItem";
            this.resetTatCaToolStripMenuItem.Size = new System.Drawing.Size(153, 24);
            this.resetTatCaToolStripMenuItem.Text = "Reset tất cả";
            this.resetTatCaToolStripMenuItem.Click += new System.EventHandler(this.resetTatCaToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "label1";
            // 
            // lbP5
            // 
            this.lbP5.ContextMenuStrip = this.cmsPen;
            this.lbP5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbP5.Font = new System.Drawing.Font("Roboto Condensed", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbP5.ForeColor = System.Drawing.Color.DarkGray;
            this.lbP5.Location = new System.Drawing.Point(155, 0);
            this.lbP5.Name = "lbP5";
            this.lbP5.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.lbP5.Size = new System.Drawing.Size(32, 36);
            this.lbP5.TabIndex = 9;
            this.lbP5.Tag = "4";
            this.lbP5.Text = "⚫";
            this.lbP5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ttUcPenState.SetToolTip(this.lbP5, "Nhấp chuột phải để chọn kết quả");
            this.lbP5.MouseLeave += new System.EventHandler(this.lbP_MouseLeave);
            this.lbP5.MouseHover += new System.EventHandler(this.lbP_MouseHover);
            // 
            // lbP4
            // 
            this.lbP4.ContextMenuStrip = this.cmsPen;
            this.lbP4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbP4.Font = new System.Drawing.Font("Roboto Condensed", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbP4.ForeColor = System.Drawing.Color.DarkGray;
            this.lbP4.Location = new System.Drawing.Point(117, 0);
            this.lbP4.Name = "lbP4";
            this.lbP4.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.lbP4.Size = new System.Drawing.Size(32, 36);
            this.lbP4.TabIndex = 10;
            this.lbP4.Tag = "3";
            this.lbP4.Text = "⚫";
            this.lbP4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ttUcPenState.SetToolTip(this.lbP4, "Nhấp chuột phải để chọn kết quả");
            this.lbP4.MouseLeave += new System.EventHandler(this.lbP_MouseLeave);
            this.lbP4.MouseHover += new System.EventHandler(this.lbP_MouseHover);
            // 
            // lbP3
            // 
            this.lbP3.ContextMenuStrip = this.cmsPen;
            this.lbP3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbP3.Font = new System.Drawing.Font("Roboto Condensed", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbP3.ForeColor = System.Drawing.Color.DarkGray;
            this.lbP3.Location = new System.Drawing.Point(79, 0);
            this.lbP3.Name = "lbP3";
            this.lbP3.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.lbP3.Size = new System.Drawing.Size(32, 36);
            this.lbP3.TabIndex = 7;
            this.lbP3.Tag = "2";
            this.lbP3.Text = "⚫";
            this.lbP3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ttUcPenState.SetToolTip(this.lbP3, "Nhấp chuột phải để chọn kết quả");
            this.lbP3.MouseLeave += new System.EventHandler(this.lbP_MouseLeave);
            this.lbP3.MouseHover += new System.EventHandler(this.lbP_MouseHover);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Black;
            this.flowLayoutPanel1.Controls.Add(this.lbP1);
            this.flowLayoutPanel1.Controls.Add(this.lbP2);
            this.flowLayoutPanel1.Controls.Add(this.lbP3);
            this.flowLayoutPanel1.Controls.Add(this.lbP4);
            this.flowLayoutPanel1.Controls.Add(this.lbP5);
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(194, 37);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // lbP1
            // 
            this.lbP1.ContextMenuStrip = this.cmsPen;
            this.lbP1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbP1.Font = new System.Drawing.Font("Roboto Condensed", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbP1.ForeColor = System.Drawing.Color.DarkGray;
            this.lbP1.Location = new System.Drawing.Point(3, 0);
            this.lbP1.Name = "lbP1";
            this.lbP1.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.lbP1.Size = new System.Drawing.Size(32, 36);
            this.lbP1.TabIndex = 11;
            this.lbP1.Tag = "0";
            this.lbP1.Text = "⚫";
            this.lbP1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ttUcPenState.SetToolTip(this.lbP1, "Nhấp chuột phải để chọn kết quả");
            this.lbP1.MouseLeave += new System.EventHandler(this.lbP_MouseLeave);
            this.lbP1.MouseHover += new System.EventHandler(this.lbP_MouseHover);
            // 
            // lbP2
            // 
            this.lbP2.ContextMenuStrip = this.cmsPen;
            this.lbP2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbP2.Font = new System.Drawing.Font("Roboto Condensed", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbP2.ForeColor = System.Drawing.Color.DarkGray;
            this.lbP2.Location = new System.Drawing.Point(41, 0);
            this.lbP2.Name = "lbP2";
            this.lbP2.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.lbP2.Size = new System.Drawing.Size(32, 36);
            this.lbP2.TabIndex = 8;
            this.lbP2.Tag = "1";
            this.lbP2.Text = "⚫";
            this.lbP2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ttUcPenState.SetToolTip(this.lbP2, "Nhấp chuột phải để chọn kết quả");
            this.lbP2.MouseLeave += new System.EventHandler(this.lbP_MouseLeave);
            this.lbP2.MouseHover += new System.EventHandler(this.lbP_MouseHover);
            // 
            // ttUcPenState
            // 
            this.ttUcPenState.Active = false;
            this.ttUcPenState.IsBalloon = true;
            // 
            // ucPenState
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Orange;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "ucPenState";
            this.Size = new System.Drawing.Size(200, 44);
            this.cmsPen.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip cmsPen;
        private System.Windows.Forms.ToolStripMenuItem tsmiVao;
        private System.Windows.Forms.ToolStripMenuItem tsmiTach;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiReset;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem resetTatCaToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbP5;
        private System.Windows.Forms.Label lbP4;
        private System.Windows.Forms.Label lbP3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label lbP2;
        private System.Windows.Forms.Label lbP1;
        private System.Windows.Forms.ToolTip ttUcPenState;
    }
}
