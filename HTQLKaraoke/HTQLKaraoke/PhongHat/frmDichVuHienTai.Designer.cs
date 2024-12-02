namespace HTQLKaraoke.PhongHat
{
    partial class frmDichVuHienTai
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDichVuHienTai));
            this.numSoLuongGiam = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.btnGiamSoLuong = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.txtTrangThai = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTongTien = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNgayDung = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvCTDH = new System.Windows.Forms.DataGridView();
            this.lblTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuongGiam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCTDH)).BeginInit();
            this.SuspendLayout();
            // 
            // numSoLuongGiam
            // 
            this.numSoLuongGiam.Location = new System.Drawing.Point(580, 349);
            this.numSoLuongGiam.Name = "numSoLuongGiam";
            this.numSoLuongGiam.Size = new System.Drawing.Size(53, 20);
            this.numSoLuongGiam.TabIndex = 84;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label5.Location = new System.Drawing.Point(485, 348);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 17);
            this.label5.TabIndex = 83;
            this.label5.Text = "Số lượng trả:";
            // 
            // btnGiamSoLuong
            // 
            this.btnGiamSoLuong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGiamSoLuong.Image = ((System.Drawing.Image)(resources.GetObject("btnGiamSoLuong.Image")));
            this.btnGiamSoLuong.Location = new System.Drawing.Point(256, 394);
            this.btnGiamSoLuong.Name = "btnGiamSoLuong";
            this.btnGiamSoLuong.Size = new System.Drawing.Size(112, 57);
            this.btnGiamSoLuong.TabIndex = 82;
            this.btnGiamSoLuong.UseVisualStyleBackColor = true;
            this.btnGiamSoLuong.Click += new System.EventHandler(this.btnGiamSoLuong_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHuy.Image = ((System.Drawing.Image)(resources.GetObject("btnHuy.Image")));
            this.btnHuy.Location = new System.Drawing.Point(518, 394);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(115, 57);
            this.btnHuy.TabIndex = 81;
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // txtTrangThai
            // 
            this.txtTrangThai.Enabled = false;
            this.txtTrangThai.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtTrangThai.Location = new System.Drawing.Point(566, 306);
            this.txtTrangThai.Name = "txtTrangThai";
            this.txtTrangThai.Size = new System.Drawing.Size(172, 26);
            this.txtTrangThai.TabIndex = 80;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(483, 311);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 17);
            this.label2.TabIndex = 79;
            this.label2.Text = "Trạng thái:";
            // 
            // txtTongTien
            // 
            this.txtTongTien.Enabled = false;
            this.txtTongTien.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtTongTien.Location = new System.Drawing.Point(173, 343);
            this.txtTongTien.Name = "txtTongTien";
            this.txtTongTien.Size = new System.Drawing.Size(214, 26);
            this.txtTongTien.TabIndex = 78;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(90, 349);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 17);
            this.label1.TabIndex = 77;
            this.label1.Text = "Tổng tiền:";
            // 
            // txtNgayDung
            // 
            this.txtNgayDung.Enabled = false;
            this.txtNgayDung.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtNgayDung.Location = new System.Drawing.Point(173, 306);
            this.txtNgayDung.Name = "txtNgayDung";
            this.txtNgayDung.Size = new System.Drawing.Size(214, 26);
            this.txtNgayDung.TabIndex = 76;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.Location = new System.Drawing.Point(90, 312);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 17);
            this.label3.TabIndex = 75;
            this.label3.Text = "Ngày dùng:";
            // 
            // dgvCTDH
            // 
            this.dgvCTDH.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCTDH.Location = new System.Drawing.Point(28, 69);
            this.dgvCTDH.Name = "dgvCTDH";
            this.dgvCTDH.Size = new System.Drawing.Size(833, 212);
            this.dgvCTDH.TabIndex = 74;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTitle.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblTitle.Location = new System.Drawing.Point(251, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(382, 29);
            this.lblTitle.TabIndex = 85;
            this.lblTitle.Text = "Dịch Vụ Đang Sử Dụng Hiện Tại";
            // 
            // frmDichVuHienTai
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.HotPink;
            this.ClientSize = new System.Drawing.Size(889, 470);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.numSoLuongGiam);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnGiamSoLuong);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.txtTrangThai);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTongTien);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNgayDung);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dgvCTDH);
            this.Name = "frmDichVuHienTai";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dịch Vụ Đang Sử Dụng";
            this.Load += new System.EventHandler(this.frmDichVuHienTai_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuongGiam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCTDH)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numSoLuongGiam;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnGiamSoLuong;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.TextBox txtTrangThai;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTongTien;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNgayDung;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvCTDH;
        private System.Windows.Forms.Label lblTitle;

    }
}