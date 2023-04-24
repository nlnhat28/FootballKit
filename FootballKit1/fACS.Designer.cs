namespace FootballKit1
{
    partial class fACS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fACS));
            this.txbACS = new System.Windows.Forms.TextBox();
            this.cbbACS = new System.Windows.Forms.ComboBox();
            this.ptbLoadFromTxtACS = new System.Windows.Forms.PictureBox();
            this.ttACS = new System.Windows.Forms.ToolTip(this.components);
            this.FootballKit = new System.Windows.Forms.NotifyIcon(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ptbLoadFromTxtACS)).BeginInit();
            this.SuspendLayout();
            // 
            // txbACS
            // 
            this.txbACS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbACS.Font = new System.Drawing.Font("Roboto Condensed", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbACS.Location = new System.Drawing.Point(12, 125);
            this.txbACS.Multiline = true;
            this.txbACS.Name = "txbACS";
            this.txbACS.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txbACS.Size = new System.Drawing.Size(313, 348);
            this.txbACS.TabIndex = 0;
            this.txbACS.TextChanged += new System.EventHandler(this.txbACS_TextChanged);
            // 
            // cbbACS
            // 
            this.cbbACS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbACS.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbbACS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbACS.Font = new System.Drawing.Font("Roboto Condensed", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbACS.FormattingEnabled = true;
            this.cbbACS.Location = new System.Drawing.Point(12, 44);
            this.cbbACS.Name = "cbbACS";
            this.cbbACS.Size = new System.Drawing.Size(313, 31);
            this.cbbACS.TabIndex = 1;
            this.cbbACS.SelectedIndexChanged += new System.EventHandler(this.cbbACS_SelectedIndexChanged);
            // 
            // ptbLoadFromTxtACS
            // 
            this.ptbLoadFromTxtACS.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.ptbLoadFromTxtACS.BackColor = System.Drawing.Color.Cyan;
            this.ptbLoadFromTxtACS.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ptbLoadFromTxtACS.Image = global::FootballKit1.Properties.Resources.Import_file1;
            this.ptbLoadFromTxtACS.Location = new System.Drawing.Point(149, 479);
            this.ptbLoadFromTxtACS.Name = "ptbLoadFromTxtACS";
            this.ptbLoadFromTxtACS.Size = new System.Drawing.Size(38, 27);
            this.ptbLoadFromTxtACS.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ptbLoadFromTxtACS.TabIndex = 2;
            this.ptbLoadFromTxtACS.TabStop = false;
            this.ttACS.SetToolTip(this.ptbLoadFromTxtACS, "Load từ file txt");
            this.ptbLoadFromTxtACS.Click += new System.EventHandler(this.ptbLoadFromTxtACS_Click);
            // 
            // ttACS
            // 
            this.ttACS.IsBalloon = true;
            // 
            // FootballKit
            // 
            this.FootballKit.Text = "FootballKit";
            this.FootballKit.Visible = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Roboto Condensed", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(8, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 23);
            this.label1.TabIndex = 44;
            this.label1.Text = "Mục:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Roboto Condensed", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(8, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 23);
            this.label2.TabIndex = 45;
            this.label2.Text = "Các từ gợi ý:";
            // 
            // fACS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(337, 519);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ptbLoadFromTxtACS);
            this.Controls.Add(this.cbbACS);
            this.Controls.Add(this.txbACS);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(203, 232);
            this.Name = "fACS";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Hỗ trợ điền";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fACS_FormClosing);
            this.Load += new System.EventHandler(this.fACS_Load);
            this.Resize += new System.EventHandler(this.fACS_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.ptbLoadFromTxtACS)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txbACS;
        private System.Windows.Forms.ComboBox cbbACS;
        private System.Windows.Forms.PictureBox ptbLoadFromTxtACS;
        private System.Windows.Forms.ToolTip ttACS;
        private System.Windows.Forms.NotifyIcon FootballKit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}