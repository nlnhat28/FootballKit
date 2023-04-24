namespace FootballKit1
{
    partial class fDoiHinh
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fDoiHinh));
            this.btnTaoAnh = new System.Windows.Forms.Button();
            this.FootballKit = new System.Windows.Forms.NotifyIcon(this.components);
            this.btnFont = new System.Windows.Forms.Button();
            this.lbForeColor = new System.Windows.Forms.Label();
            this.ttDoiHinh = new System.Windows.Forms.ToolTip(this.components);
            this.lbBackColor = new System.Windows.Forms.Label();
            this.ptbSwitchView = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txbSoDo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnAutoGenerateSoDo = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.ptbPitch = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ptbSwitchView)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbPitch)).BeginInit();
            this.SuspendLayout();
            // 
            // btnTaoAnh
            // 
            this.btnTaoAnh.BackColor = System.Drawing.Color.Chartreuse;
            this.btnTaoAnh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTaoAnh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTaoAnh.Font = new System.Drawing.Font("Roboto Condensed", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTaoAnh.ForeColor = System.Drawing.Color.Black;
            this.btnTaoAnh.Location = new System.Drawing.Point(427, 70);
            this.btnTaoAnh.Name = "btnTaoAnh";
            this.btnTaoAnh.Size = new System.Drawing.Size(132, 39);
            this.btnTaoAnh.TabIndex = 10;
            this.btnTaoAnh.Text = "Lưu ảnh";
            this.btnTaoAnh.UseVisualStyleBackColor = false;
            this.btnTaoAnh.Click += new System.EventHandler(this.btnTaoAnh_Click);
            // 
            // FootballKit
            // 
            this.FootballKit.Text = "FootballKit";
            this.FootballKit.Visible = true;
            // 
            // btnFont
            // 
            this.btnFont.BackColor = System.Drawing.Color.White;
            this.btnFont.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFont.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFont.Font = new System.Drawing.Font("Roboto Condensed", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFont.ForeColor = System.Drawing.Color.Black;
            this.btnFont.Location = new System.Drawing.Point(19, 70);
            this.btnFont.Name = "btnFont";
            this.btnFont.Size = new System.Drawing.Size(91, 39);
            this.btnFont.TabIndex = 12;
            this.btnFont.Text = "Font chữ";
            this.btnFont.UseVisualStyleBackColor = false;
            this.btnFont.Click += new System.EventHandler(this.btnFont_Click);
            // 
            // lbForeColor
            // 
            this.lbForeColor.BackColor = System.Drawing.Color.Yellow;
            this.lbForeColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbForeColor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbForeColor.Font = new System.Drawing.Font("Roboto Condensed", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbForeColor.Location = new System.Drawing.Point(207, 76);
            this.lbForeColor.Name = "lbForeColor";
            this.lbForeColor.Size = new System.Drawing.Size(25, 25);
            this.lbForeColor.TabIndex = 13;
            this.ttDoiHinh.SetToolTip(this.lbForeColor, "Click để đổi màu CHỮ");
            this.lbForeColor.Click += new System.EventHandler(this.lbForeColor_Click);
            // 
            // ttDoiHinh
            // 
            this.ttDoiHinh.IsBalloon = true;
            // 
            // lbBackColor
            // 
            this.lbBackColor.BackColor = System.Drawing.Color.Yellow;
            this.lbBackColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbBackColor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbBackColor.Font = new System.Drawing.Font("Roboto Condensed", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbBackColor.Location = new System.Drawing.Point(333, 76);
            this.lbBackColor.Name = "lbBackColor";
            this.lbBackColor.Size = new System.Drawing.Size(51, 25);
            this.lbBackColor.TabIndex = 15;
            this.ttDoiHinh.SetToolTip(this.lbBackColor, "Click để đổi màu NỀN");
            this.lbBackColor.Click += new System.EventHandler(this.lbBackColor_Click);
            // 
            // ptbSwitchView
            // 
            this.ptbSwitchView.BackColor = System.Drawing.Color.Orange;
            this.ptbSwitchView.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ptbSwitchView.Image = global::FootballKit1.Properties.Resources.Upward_arrow_23;
            this.ptbSwitchView.Location = new System.Drawing.Point(525, 17);
            this.ptbSwitchView.Name = "ptbSwitchView";
            this.ptbSwitchView.Size = new System.Drawing.Size(34, 34);
            this.ptbSwitchView.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ptbSwitchView.TabIndex = 24;
            this.ptbSwitchView.TabStop = false;
            this.ttDoiHinh.SetToolTip(this.ptbSwitchView, "Xoay view ngược lại");
            this.ptbSwitchView.Click += new System.EventHandler(this.ptbSwitchView_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Roboto Condensed", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(122, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 23);
            this.label2.TabIndex = 14;
            this.label2.Text = "Màu chữ:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Roboto Condensed", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(248, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 23);
            this.label3.TabIndex = 16;
            this.label3.Text = "Màu nền:";
            // 
            // txbSoDo
            // 
            this.txbSoDo.Font = new System.Drawing.Font("Roboto Condensed", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbSoDo.Location = new System.Drawing.Point(116, 20);
            this.txbSoDo.Name = "txbSoDo";
            this.txbSoDo.Size = new System.Drawing.Size(100, 30);
            this.txbSoDo.TabIndex = 17;
            this.txbSoDo.TextChanged += new System.EventHandler(this.txbSoDo_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Roboto Condensed", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(14, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 23);
            this.label4.TabIndex = 18;
            this.label4.Text = "Nhập sơ đồ:";
            // 
            // btnAutoGenerateSoDo
            // 
            this.btnAutoGenerateSoDo.BackColor = System.Drawing.Color.Aqua;
            this.btnAutoGenerateSoDo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAutoGenerateSoDo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAutoGenerateSoDo.Font = new System.Drawing.Font("Roboto Condensed", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAutoGenerateSoDo.ForeColor = System.Drawing.Color.Black;
            this.btnAutoGenerateSoDo.Location = new System.Drawing.Point(316, 17);
            this.btnAutoGenerateSoDo.Name = "btnAutoGenerateSoDo";
            this.btnAutoGenerateSoDo.Size = new System.Drawing.Size(125, 34);
            this.btnAutoGenerateSoDo.TabIndex = 20;
            this.btnAutoGenerateSoDo.Text = "Tự điền sơ đồ";
            this.btnAutoGenerateSoDo.UseVisualStyleBackColor = false;
            this.btnAutoGenerateSoDo.Click += new System.EventHandler(this.btnAutoGenerateSoDo_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Roboto Condensed", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(224, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 23);
            this.label1.TabIndex = 21;
            this.label1.Text = "hoặc click";
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.ptbSwitchView);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.btnFont);
            this.panel1.Controls.Add(this.btnAutoGenerateSoDo);
            this.panel1.Controls.Add(this.btnTaoAnh);
            this.panel1.Controls.Add(this.lbForeColor);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.lbBackColor);
            this.panel1.Controls.Add(this.txbSoDo);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(78, 716);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(579, 128);
            this.panel1.TabIndex = 23;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Roboto Condensed", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(471, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 23);
            this.label5.TabIndex = 25;
            this.label5.Text = "Xoay:";
            // 
            // ptbPitch
            // 
            this.ptbPitch.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ptbPitch.Image = global::FootballKit1.Properties.Resources.Pitch_12;
            this.ptbPitch.Location = new System.Drawing.Point(24, 25);
            this.ptbPitch.Name = "ptbPitch";
            this.ptbPitch.Size = new System.Drawing.Size(685, 685);
            this.ptbPitch.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ptbPitch.TabIndex = 2;
            this.ptbPitch.TabStop = false;
            // 
            // fDoiHinh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(736, 851);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ptbPitch);
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "fDoiHinh";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tạo ảnh đội hình chính";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fDoiHinh_FormClosing);
            this.Load += new System.EventHandler(this.fDoiHinh_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ptbSwitchView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbPitch)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox ptbPitch;
        private System.Windows.Forms.Button btnTaoAnh;
        private System.Windows.Forms.NotifyIcon FootballKit;
        private System.Windows.Forms.Button btnFont;
        private System.Windows.Forms.Label lbForeColor;
        private System.Windows.Forms.ToolTip ttDoiHinh;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbBackColor;
        private System.Windows.Forms.TextBox txbSoDo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnAutoGenerateSoDo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox ptbSwitchView;
    }
}