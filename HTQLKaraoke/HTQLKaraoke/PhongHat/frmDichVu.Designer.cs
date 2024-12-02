namespace HTQLKaraoke.PhongHat
{
    partial class frmDichVu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDichVu));
            this.lblTitle = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBoxServiceDetails = new System.Windows.Forms.GroupBox();
            this.txtMaPhong = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMaDichVu = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtGiaDichVu = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtTenDichVu = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnChonDichVu = new System.Windows.Forms.Button();
            this.btnDichVuHienTai = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBoxServiceDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTitle.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblTitle.Location = new System.Drawing.Point(394, 32);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(311, 29);
            this.lblTitle.TabIndex = 12;
            this.lblTitle.Text = "Lựa Chọn Dịch Vụ Đi Kèm";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.flowLayoutPanel);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.groupBox1.Location = new System.Drawing.Point(34, 87);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(615, 447);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Danh Sách Dịch Vụ";
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.AutoScroll = true;
            this.flowLayoutPanel.Location = new System.Drawing.Point(4, 27);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(605, 415);
            this.flowLayoutPanel.TabIndex = 9;
            // 
            // groupBoxServiceDetails
            // 
            this.groupBoxServiceDetails.Controls.Add(this.txtMaPhong);
            this.groupBoxServiceDetails.Controls.Add(this.label1);
            this.groupBoxServiceDetails.Controls.Add(this.txtMaDichVu);
            this.groupBoxServiceDetails.Controls.Add(this.label10);
            this.groupBoxServiceDetails.Controls.Add(this.txtGhiChu);
            this.groupBoxServiceDetails.Controls.Add(this.label9);
            this.groupBoxServiceDetails.Controls.Add(this.txtGiaDichVu);
            this.groupBoxServiceDetails.Controls.Add(this.label6);
            this.groupBoxServiceDetails.Controls.Add(this.txtTenDichVu);
            this.groupBoxServiceDetails.Controls.Add(this.label3);
            this.groupBoxServiceDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.groupBoxServiceDetails.Location = new System.Drawing.Point(655, 87);
            this.groupBoxServiceDetails.Name = "groupBoxServiceDetails";
            this.groupBoxServiceDetails.Size = new System.Drawing.Size(424, 328);
            this.groupBoxServiceDetails.TabIndex = 13;
            this.groupBoxServiceDetails.TabStop = false;
            this.groupBoxServiceDetails.Enter += new System.EventHandler(this.groupBoxServiceDetails_Enter);
            // 
            // txtMaPhong
            // 
            this.txtMaPhong.Enabled = false;
            this.txtMaPhong.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtMaPhong.Location = new System.Drawing.Point(114, 70);
            this.txtMaPhong.Name = "txtMaPhong";
            this.txtMaPhong.Size = new System.Drawing.Size(136, 26);
            this.txtMaPhong.TabIndex = 35;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(23, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 17);
            this.label1.TabIndex = 34;
            this.label1.Text = "Mã Phòng:";
            // 
            // txtMaDichVu
            // 
            this.txtMaDichVu.Enabled = false;
            this.txtMaDichVu.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtMaDichVu.Location = new System.Drawing.Point(114, 27);
            this.txtMaDichVu.Name = "txtMaDichVu";
            this.txtMaDichVu.Size = new System.Drawing.Size(136, 26);
            this.txtMaDichVu.TabIndex = 31;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label10.Location = new System.Drawing.Point(23, 33);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(80, 17);
            this.label10.TabIndex = 30;
            this.label10.Text = "Mã dịch vụ:";
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Enabled = false;
            this.txtGhiChu.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtGhiChu.Location = new System.Drawing.Point(95, 214);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.ReadOnly = true;
            this.txtGhiChu.Size = new System.Drawing.Size(288, 83);
            this.txtGhiChu.TabIndex = 29;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label9.Location = new System.Drawing.Point(26, 223);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 17);
            this.label9.TabIndex = 28;
            this.label9.Text = "Ghi Chú:";
            // 
            // txtGiaDichVu
            // 
            this.txtGiaDichVu.Enabled = false;
            this.txtGiaDichVu.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtGiaDichVu.Location = new System.Drawing.Point(129, 165);
            this.txtGiaDichVu.Name = "txtGiaDichVu";
            this.txtGiaDichVu.Size = new System.Drawing.Size(103, 29);
            this.txtGiaDichVu.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label6.Location = new System.Drawing.Point(23, 174);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 17);
            this.label6.TabIndex = 8;
            this.label6.Text = "Giá dịch vụ:";
            // 
            // txtTenDichVu
            // 
            this.txtTenDichVu.Enabled = false;
            this.txtTenDichVu.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtTenDichVu.Location = new System.Drawing.Point(129, 119);
            this.txtTenDichVu.Name = "txtTenDichVu";
            this.txtTenDichVu.Size = new System.Drawing.Size(192, 29);
            this.txtTenDichVu.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.Location = new System.Drawing.Point(23, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Tên dịch vụ:";
            // 
            // btnChonDichVu
            // 
            this.btnChonDichVu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChonDichVu.Image = ((System.Drawing.Image)(resources.GetObject("btnChonDichVu.Image")));
            this.btnChonDichVu.Location = new System.Drawing.Point(656, 421);
            this.btnChonDichVu.Name = "btnChonDichVu";
            this.btnChonDichVu.Size = new System.Drawing.Size(129, 74);
            this.btnChonDichVu.TabIndex = 49;
            this.btnChonDichVu.UseVisualStyleBackColor = true;
            this.btnChonDichVu.Click += new System.EventHandler(this.btnChonDichVu_Click);
            // 
            // btnDichVuHienTai
            // 
            this.btnDichVuHienTai.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDichVuHienTai.Image = ((System.Drawing.Image)(resources.GetObject("btnDichVuHienTai.Image")));
            this.btnDichVuHienTai.Location = new System.Drawing.Point(803, 423);
            this.btnDichVuHienTai.Name = "btnDichVuHienTai";
            this.btnDichVuHienTai.Size = new System.Drawing.Size(128, 74);
            this.btnDichVuHienTai.TabIndex = 53;
            this.btnDichVuHienTai.UseVisualStyleBackColor = true;
            this.btnDichVuHienTai.Click += new System.EventHandler(this.btnDichVuHienTai_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHuy.Image = ((System.Drawing.Image)(resources.GetObject("btnHuy.Image")));
            this.btnHuy.Location = new System.Drawing.Point(951, 423);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(129, 72);
            this.btnHuy.TabIndex = 54;
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // frmDichVu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(1114, 548);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnDichVuHienTai);
            this.Controls.Add(this.btnChonDichVu);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBoxServiceDetails);
            this.Controls.Add(this.lblTitle);
            this.Name = "frmDichVu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lựa chọn dịch vụ";
            this.Load += new System.EventHandler(this.frmDichVu_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBoxServiceDetails.ResumeLayout(false);
            this.groupBoxServiceDetails.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.GroupBox groupBoxServiceDetails;
        private System.Windows.Forms.TextBox txtMaDichVu;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtGiaDichVu;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtTenDichVu;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMaPhong;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnChonDichVu;
        private System.Windows.Forms.Button btnDichVuHienTai;
        private System.Windows.Forms.Button btnHuy;
    }
}