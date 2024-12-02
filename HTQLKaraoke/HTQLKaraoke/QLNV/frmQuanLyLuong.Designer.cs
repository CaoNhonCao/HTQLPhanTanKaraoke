namespace HTQLKaraoke.QLNV
{
    partial class frmQuanLyLuong
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmQuanLyLuong));
            this.label1 = new System.Windows.Forms.Label();
            this.dgvLuongNhanVien = new System.Windows.Forms.DataGridView();
            this.txtTongGioLam = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtLuongThuong = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTienPhat = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLuongLanh = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnNhanLuong = new System.Windows.Forms.Button();
            this.btnLichSu = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLuongNhanVien)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label1.Location = new System.Drawing.Point(152, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(472, 29);
            this.label1.TabIndex = 62;
            this.label1.Text = "Quản Lý Lương Nhân Viên Trong Tháng";
            // 
            // dgvLuongNhanVien
            // 
            this.dgvLuongNhanVien.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLuongNhanVien.Location = new System.Drawing.Point(38, 89);
            this.dgvLuongNhanVien.Name = "dgvLuongNhanVien";
            this.dgvLuongNhanVien.ReadOnly = true;
            this.dgvLuongNhanVien.Size = new System.Drawing.Size(699, 190);
            this.dgvLuongNhanVien.TabIndex = 63;
            // 
            // txtTongGioLam
            // 
            this.txtTongGioLam.Enabled = false;
            this.txtTongGioLam.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtTongGioLam.Location = new System.Drawing.Point(134, 319);
            this.txtTongGioLam.Name = "txtTongGioLam";
            this.txtTongGioLam.ReadOnly = true;
            this.txtTongGioLam.Size = new System.Drawing.Size(99, 26);
            this.txtTongGioLam.TabIndex = 65;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label13.Location = new System.Drawing.Point(34, 325);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(94, 17);
            this.label13.TabIndex = 64;
            this.label13.Text = "Tổng giờ làm:";
            // 
            // txtLuongThuong
            // 
            this.txtLuongThuong.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtLuongThuong.ForeColor = System.Drawing.Color.Green;
            this.txtLuongThuong.Location = new System.Drawing.Point(134, 363);
            this.txtLuongThuong.Name = "txtLuongThuong";
            this.txtLuongThuong.ReadOnly = true;
            this.txtLuongThuong.Size = new System.Drawing.Size(99, 26);
            this.txtLuongThuong.TabIndex = 67;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(34, 369);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 17);
            this.label2.TabIndex = 66;
            this.label2.Text = "Lương thưởng:";
            // 
            // txtTienPhat
            // 
            this.txtTienPhat.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtTienPhat.ForeColor = System.Drawing.Color.Red;
            this.txtTienPhat.Location = new System.Drawing.Point(356, 319);
            this.txtTienPhat.Name = "txtTienPhat";
            this.txtTienPhat.ReadOnly = true;
            this.txtTienPhat.Size = new System.Drawing.Size(99, 26);
            this.txtTienPhat.TabIndex = 69;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.Location = new System.Drawing.Point(265, 325);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 17);
            this.label3.TabIndex = 68;
            this.label3.Text = "Lương Phạt:";
            // 
            // txtLuongLanh
            // 
            this.txtLuongLanh.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtLuongLanh.ForeColor = System.Drawing.SystemColors.Highlight;
            this.txtLuongLanh.Location = new System.Drawing.Point(356, 363);
            this.txtLuongLanh.Name = "txtLuongLanh";
            this.txtLuongLanh.ReadOnly = true;
            this.txtLuongLanh.Size = new System.Drawing.Size(99, 26);
            this.txtLuongLanh.TabIndex = 71;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label4.Location = new System.Drawing.Point(265, 369);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 17);
            this.label4.TabIndex = 70;
            this.label4.Text = "Lương lãnh:";
            // 
            // btnNhanLuong
            // 
            this.btnNhanLuong.Image = ((System.Drawing.Image)(resources.GetObject("btnNhanLuong.Image")));
            this.btnNhanLuong.Location = new System.Drawing.Point(490, 285);
            this.btnNhanLuong.Name = "btnNhanLuong";
            this.btnNhanLuong.Size = new System.Drawing.Size(134, 67);
            this.btnNhanLuong.TabIndex = 72;
            this.btnNhanLuong.UseVisualStyleBackColor = true;
            this.btnNhanLuong.Click += new System.EventHandler(this.btnNhanLuong_Click);
            // 
            // btnLichSu
            // 
            this.btnLichSu.Image = ((System.Drawing.Image)(resources.GetObject("btnLichSu.Image")));
            this.btnLichSu.Location = new System.Drawing.Point(644, 285);
            this.btnLichSu.Name = "btnLichSu";
            this.btnLichSu.Size = new System.Drawing.Size(134, 67);
            this.btnLichSu.TabIndex = 73;
            this.btnLichSu.UseVisualStyleBackColor = true;
            this.btnLichSu.Click += new System.EventHandler(this.btnLichSu_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHuy.Image = ((System.Drawing.Image)(resources.GetObject("btnHuy.Image")));
            this.btnHuy.Location = new System.Drawing.Point(568, 358);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(134, 67);
            this.btnHuy.TabIndex = 74;
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // frmQuanLyLuong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 431);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnLichSu);
            this.Controls.Add(this.btnNhanLuong);
            this.Controls.Add(this.txtLuongLanh);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtTienPhat);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtLuongThuong);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTongGioLam);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.dgvLuongNhanVien);
            this.Controls.Add(this.label1);
            this.Name = "frmQuanLyLuong";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý lương nhân viên";
            ((System.ComponentModel.ISupportInitialize)(this.dgvLuongNhanVien)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvLuongNhanVien;
        private System.Windows.Forms.TextBox txtTongGioLam;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtLuongThuong;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTienPhat;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLuongLanh;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnNhanLuong;
        private System.Windows.Forms.Button btnLichSu;
        private System.Windows.Forms.Button btnHuy;
    }
}