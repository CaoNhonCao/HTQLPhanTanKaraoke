namespace ServerHTQLKaraoke.QLHD
{
    partial class frmHoaDon
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHoaDon));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtpFilterDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbMonthFilter = new System.Windows.Forms.ComboBox();
            this.BtnFilterThisYear = new System.Windows.Forms.Button();
            this.BtnFilterThisMonth = new System.Windows.Forms.Button();
            this.BtnFilterToday = new System.Windows.Forms.Button();
            this.dataGridViewHoaDon = new System.Windows.Forms.DataGridView();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnThoat = new System.Windows.Forms.Button();
            this.cbxChiNhanh = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHoaDon)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtpFilterDate);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbMonthFilter);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.groupBox1.Location = new System.Drawing.Point(89, 385);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(323, 101);
            this.groupBox1.TabIndex = 91;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lọc Thời Điểm Khác";
            // 
            // dtpFilterDate
            // 
            this.dtpFilterDate.Location = new System.Drawing.Point(128, 69);
            this.dtpFilterDate.Name = "dtpFilterDate";
            this.dtpFilterDate.Size = new System.Drawing.Size(185, 24);
            this.dtpFilterDate.TabIndex = 3;
            this.dtpFilterDate.ValueChanged += new System.EventHandler(this.DtpFilterDate_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(18, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "Chọn ngày:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.ForeColor = System.Drawing.Color.DarkCyan;
            this.label1.Location = new System.Drawing.Point(18, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Chọn Tháng:";
            // 
            // cbMonthFilter
            // 
            this.cbMonthFilter.FormattingEnabled = true;
            this.cbMonthFilter.Location = new System.Drawing.Point(128, 28);
            this.cbMonthFilter.Name = "cbMonthFilter";
            this.cbMonthFilter.Size = new System.Drawing.Size(121, 26);
            this.cbMonthFilter.TabIndex = 0;
            this.cbMonthFilter.SelectedIndexChanged += new System.EventHandler(this.CbMonthFilter_SelectedIndexChanged);
            // 
            // BtnFilterThisYear
            // 
            this.BtnFilterThisYear.BackColor = System.Drawing.Color.DarkOrange;
            this.BtnFilterThisYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.BtnFilterThisYear.ForeColor = System.Drawing.Color.Indigo;
            this.BtnFilterThisYear.Location = new System.Drawing.Point(559, 416);
            this.BtnFilterThisYear.Name = "BtnFilterThisYear";
            this.BtnFilterThisYear.Size = new System.Drawing.Size(118, 42);
            this.BtnFilterThisYear.TabIndex = 90;
            this.BtnFilterThisYear.Text = "Năm Nay";
            this.BtnFilterThisYear.UseVisualStyleBackColor = false;
            this.BtnFilterThisYear.Click += new System.EventHandler(this.BtnFilterThisYear_Click);
            // 
            // BtnFilterThisMonth
            // 
            this.BtnFilterThisMonth.BackColor = System.Drawing.Color.DarkCyan;
            this.BtnFilterThisMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.BtnFilterThisMonth.ForeColor = System.Drawing.Color.BlanchedAlmond;
            this.BtnFilterThisMonth.Location = new System.Drawing.Point(435, 442);
            this.BtnFilterThisMonth.Name = "BtnFilterThisMonth";
            this.BtnFilterThisMonth.Size = new System.Drawing.Size(118, 42);
            this.BtnFilterThisMonth.TabIndex = 89;
            this.BtnFilterThisMonth.Text = "Tháng Này";
            this.BtnFilterThisMonth.UseVisualStyleBackColor = false;
            this.BtnFilterThisMonth.Click += new System.EventHandler(this.BtnFilterThisMonth_Click);
            // 
            // BtnFilterToday
            // 
            this.BtnFilterToday.BackColor = System.Drawing.Color.DarkKhaki;
            this.BtnFilterToday.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.BtnFilterToday.ForeColor = System.Drawing.Color.DarkBlue;
            this.BtnFilterToday.Location = new System.Drawing.Point(435, 394);
            this.BtnFilterToday.Name = "BtnFilterToday";
            this.BtnFilterToday.Size = new System.Drawing.Size(118, 42);
            this.BtnFilterToday.TabIndex = 88;
            this.BtnFilterToday.Text = "Hôm Nay";
            this.BtnFilterToday.UseVisualStyleBackColor = false;
            this.BtnFilterToday.Click += new System.EventHandler(this.BtnFilterToday_Click);
            // 
            // dataGridViewHoaDon
            // 
            this.dataGridViewHoaDon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewHoaDon.Location = new System.Drawing.Point(49, 105);
            this.dataGridViewHoaDon.Name = "dataGridViewHoaDon";
            this.dataGridViewHoaDon.Size = new System.Drawing.Size(831, 262);
            this.dataGridViewHoaDon.TabIndex = 87;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTitle.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblTitle.Location = new System.Drawing.Point(371, 39);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(216, 29);
            this.lblTitle.TabIndex = 86;
            this.lblTitle.Text = "Quản Lý Hóa Đơn";
            // 
            // btnThoat
            // 
            this.btnThoat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThoat.Image = ((System.Drawing.Image)(resources.GetObject("btnThoat.Image")));
            this.btnThoat.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnThoat.Location = new System.Drawing.Point(683, 371);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(151, 140);
            this.btnThoat.TabIndex = 92;
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // cbxChiNhanh
            // 
            this.cbxChiNhanh.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.cbxChiNhanh.FormattingEnabled = true;
            this.cbxChiNhanh.Location = new System.Drawing.Point(210, 68);
            this.cbxChiNhanh.Name = "cbxChiNhanh";
            this.cbxChiNhanh.Size = new System.Drawing.Size(155, 28);
            this.cbxChiNhanh.TabIndex = 93;
            this.cbxChiNhanh.SelectedIndexChanged += new System.EventHandler(this.CbxChiNhanh_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.ForeColor = System.Drawing.Color.DarkCyan;
            this.label3.Location = new System.Drawing.Point(56, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(144, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Chọn Chi Nhánh:";
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.White;
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.Location = new System.Drawing.Point(12, 12);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(48, 43);
            this.btnRefresh.TabIndex = 94;
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // frmHoaDon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.BlanchedAlmond;
            this.ClientSize = new System.Drawing.Size(928, 530);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbxChiNhanh);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.BtnFilterThisYear);
            this.Controls.Add(this.BtnFilterThisMonth);
            this.Controls.Add(this.BtnFilterToday);
            this.Controls.Add(this.dataGridViewHoaDon);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnThoat);
            this.Name = "frmHoaDon";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản Lý Hóa Đơn";
            this.Load += new System.EventHandler(this.frmHoaDon_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHoaDon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dtpFilterDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbMonthFilter;
        private System.Windows.Forms.Button BtnFilterThisYear;
        private System.Windows.Forms.Button BtnFilterThisMonth;
        private System.Windows.Forms.Button BtnFilterToday;
        private System.Windows.Forms.DataGridView dataGridViewHoaDon;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.ComboBox cbxChiNhanh;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnRefresh;
    }
}