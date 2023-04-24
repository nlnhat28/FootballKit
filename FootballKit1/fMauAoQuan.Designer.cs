namespace FootballKit1
{
    partial class fMauAoQuan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fMauAoQuan));
            this.lbMauAo = new System.Windows.Forms.Label();
            this.lbMauQuan = new System.Windows.Forms.Label();
            this.btnLuuAnh = new System.Windows.Forms.Button();
            this.pnlMau = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlMau.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbMauAo
            // 
            this.lbMauAo.BackColor = System.Drawing.Color.Red;
            this.lbMauAo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbMauAo.Location = new System.Drawing.Point(5, 5);
            this.lbMauAo.Margin = new System.Windows.Forms.Padding(0);
            this.lbMauAo.Name = "lbMauAo";
            this.lbMauAo.Size = new System.Drawing.Size(450, 25);
            this.lbMauAo.TabIndex = 0;
            // 
            // lbMauQuan
            // 
            this.lbMauQuan.BackColor = System.Drawing.Color.Black;
            this.lbMauQuan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbMauQuan.Location = new System.Drawing.Point(455, 5);
            this.lbMauQuan.Margin = new System.Windows.Forms.Padding(0);
            this.lbMauQuan.Name = "lbMauQuan";
            this.lbMauQuan.Size = new System.Drawing.Size(200, 25);
            this.lbMauQuan.TabIndex = 1;
            // 
            // btnLuuAnh
            // 
            this.btnLuuAnh.BackColor = System.Drawing.Color.Chartreuse;
            this.btnLuuAnh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuuAnh.Font = new System.Drawing.Font("Roboto Condensed", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuuAnh.Location = new System.Drawing.Point(312, 123);
            this.btnLuuAnh.Name = "btnLuuAnh";
            this.btnLuuAnh.Size = new System.Drawing.Size(105, 38);
            this.btnLuuAnh.TabIndex = 3;
            this.btnLuuAnh.Text = "Lưu ảnh";
            this.btnLuuAnh.UseVisualStyleBackColor = false;
            this.btnLuuAnh.Click += new System.EventHandler(this.btnLuuAnh_Click);
            // 
            // pnlMau
            // 
            this.pnlMau.AutoSize = true;
            this.pnlMau.BackColor = System.Drawing.Color.Black;
            this.pnlMau.Controls.Add(this.lbMauAo);
            this.pnlMau.Controls.Add(this.lbMauQuan);
            this.pnlMau.Location = new System.Drawing.Point(50, 65);
            this.pnlMau.Margin = new System.Windows.Forms.Padding(0);
            this.pnlMau.Name = "pnlMau";
            this.pnlMau.Padding = new System.Windows.Forms.Padding(5);
            this.pnlMau.Size = new System.Drawing.Size(660, 35);
            this.pnlMau.TabIndex = 5;
            // 
            // fMauAoQuan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(763, 183);
            this.Controls.Add(this.pnlMau);
            this.Controls.Add(this.btnLuuAnh);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "fMauAoQuan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Màu áo quần";
            this.pnlMau.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbMauAo;
        private System.Windows.Forms.Label lbMauQuan;
        private System.Windows.Forms.Button btnLuuAnh;
        private System.Windows.Forms.FlowLayoutPanel pnlMau;
    }
}