namespace HTQLKaraoke.DanhGia
{
    partial class frmTTDanhGia
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
            this.dtgDanhGia = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLoc = new System.Windows.Forms.Button();
            this.cbxLocDiem = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTongDiem = new System.Windows.Forms.TextBox();
            this.cbxLocTheoLoaiPhong = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxLocTheoPhong = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dtgDanhGia)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtgDanhGia
            // 
            this.dtgDanhGia.BackgroundColor = System.Drawing.Color.Cornsilk;
            this.dtgDanhGia.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgDanhGia.Location = new System.Drawing.Point(12, 85);
            this.dtgDanhGia.Name = "dtgDanhGia";
            this.dtgDanhGia.ReadOnly = true;
            this.dtgDanhGia.Size = new System.Drawing.Size(643, 329);
            this.dtgDanhGia.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label1.Location = new System.Drawing.Point(261, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(371, 27);
            this.label1.TabIndex = 5;
            this.label1.Text = "Khách Hàng Đánh Giá Phòng Hát";
            // 
            // btnLoc
            // 
            this.btnLoc.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnLoc.Enabled = false;
            this.btnLoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnLoc.Location = new System.Drawing.Point(57, 159);
            this.btnLoc.Name = "btnLoc";
            this.btnLoc.Size = new System.Drawing.Size(154, 44);
            this.btnLoc.TabIndex = 6;
            this.btnLoc.Text = "Lọc";
            this.btnLoc.UseVisualStyleBackColor = false;
            this.btnLoc.Click += new System.EventHandler(this.btnLoc_Click);
            // 
            // cbxLocDiem
            // 
            this.cbxLocDiem.BackColor = System.Drawing.Color.LavenderBlush;
            this.cbxLocDiem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxLocDiem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.cbxLocDiem.ForeColor = System.Drawing.Color.Chocolate;
            this.cbxLocDiem.FormattingEnabled = true;
            this.cbxLocDiem.Location = new System.Drawing.Point(10, 53);
            this.cbxLocDiem.Name = "cbxLocDiem";
            this.cbxLocDiem.Size = new System.Drawing.Size(254, 24);
            this.cbxLocDiem.TabIndex = 7;
            this.cbxLocDiem.SelectedIndexChanged += new System.EventHandler(this.cbxLocDiem_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Aquamarine;
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbxLocDiem);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(669, 85);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(270, 87);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lọc Danh Sách";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.ForeColor = System.Drawing.Color.Brown;
            this.label2.Location = new System.Drawing.Point(7, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 17);
            this.label2.TabIndex = 9;
            this.label2.Text = "Lọc Theo Điểm:";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.Desktop;
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtTongDiem);
            this.groupBox2.Controls.Add(this.cbxLocTheoLoaiPhong);
            this.groupBox2.Controls.Add(this.btnLoc);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cbxLocTheoPhong);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.groupBox2.ForeColor = System.Drawing.Color.ForestGreen;
            this.groupBox2.Location = new System.Drawing.Point(669, 187);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(270, 227);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Lọc Theo Phòng";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label5.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label5.Location = new System.Drawing.Point(7, 118);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 17);
            this.label5.TabIndex = 12;
            this.label5.Text = "Tổng Điểm:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label4.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label4.Location = new System.Drawing.Point(6, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 17);
            this.label4.TabIndex = 11;
            this.label4.Text = "Loại Phòng:";
            // 
            // txtTongDiem
            // 
            this.txtTongDiem.BackColor = System.Drawing.Color.LavenderBlush;
            this.txtTongDiem.Enabled = false;
            this.txtTongDiem.ForeColor = System.Drawing.Color.Chocolate;
            this.txtTongDiem.Location = new System.Drawing.Point(114, 112);
            this.txtTongDiem.Name = "txtTongDiem";
            this.txtTongDiem.Size = new System.Drawing.Size(137, 26);
            this.txtTongDiem.TabIndex = 11;
            // 
            // cbxLocTheoLoaiPhong
            // 
            this.cbxLocTheoLoaiPhong.BackColor = System.Drawing.Color.LavenderBlush;
            this.cbxLocTheoLoaiPhong.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxLocTheoLoaiPhong.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.cbxLocTheoLoaiPhong.ForeColor = System.Drawing.Color.Chocolate;
            this.cbxLocTheoLoaiPhong.FormattingEnabled = true;
            this.cbxLocTheoLoaiPhong.Location = new System.Drawing.Point(114, 72);
            this.cbxLocTheoLoaiPhong.Name = "cbxLocTheoLoaiPhong";
            this.cbxLocTheoLoaiPhong.Size = new System.Drawing.Size(137, 24);
            this.cbxLocTheoLoaiPhong.TabIndex = 10;
            this.cbxLocTheoLoaiPhong.SelectedIndexChanged += new System.EventHandler(this.cbxLocTheoLoaiPhong_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label3.Location = new System.Drawing.Point(7, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "Chọn Phòng:";
            // 
            // cbxLocTheoPhong
            // 
            this.cbxLocTheoPhong.BackColor = System.Drawing.Color.LavenderBlush;
            this.cbxLocTheoPhong.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxLocTheoPhong.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.cbxLocTheoPhong.ForeColor = System.Drawing.Color.Chocolate;
            this.cbxLocTheoPhong.FormattingEnabled = true;
            this.cbxLocTheoPhong.Location = new System.Drawing.Point(114, 28);
            this.cbxLocTheoPhong.Name = "cbxLocTheoPhong";
            this.cbxLocTheoPhong.Size = new System.Drawing.Size(137, 24);
            this.cbxLocTheoPhong.TabIndex = 7;
            this.cbxLocTheoPhong.SelectedIndexChanged += new System.EventHandler(this.cbxLocTheoPhong_SelectedIndexChanged);
            // 
            // frmTTDanhGia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.ClientSize = new System.Drawing.Size(951, 426);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtgDanhGia);
            this.Name = "frmTTDanhGia";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thông tin đánh giá khách hàng";
            this.Load += new System.EventHandler(this.frmTTDanhGia_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgDanhGia)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dtgDanhGia;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnLoc;
        private System.Windows.Forms.ComboBox cbxLocDiem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbxLocTheoLoaiPhong;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbxLocTheoPhong;
        private System.Windows.Forms.TextBox txtTongDiem;
        private System.Windows.Forms.Label label5;
    }
}