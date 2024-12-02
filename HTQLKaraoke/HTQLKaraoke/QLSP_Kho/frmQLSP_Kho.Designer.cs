namespace HTQLKaraoke.QLSP_Kho
{
    partial class frmQLSP_Kho
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmQLSP_Kho));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.MenuThucUong = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuDoAn = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuCombo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTrangChu = new System.Windows.Forms.ToolStrip();
            this.btnBia = new System.Windows.Forms.ToolStripButton();
            this.btnRuou = new System.Windows.Forms.ToolStripButton();
            this.btnNuocNgot = new System.Windows.Forms.ToolStripButton();
            this.btnNuocSuoi = new System.Windows.Forms.ToolStripButton();
            this.btnDoAnNhe = new System.Windows.Forms.ToolStripButton();
            this.btnTraiCay = new System.Windows.Forms.ToolStripButton();
            this.btnCanhBao = new System.Windows.Forms.ToolStripButton();
            this.btnCombo = new System.Windows.Forms.ToolStripButton();
            this.btnThoat = new System.Windows.Forms.ToolStripButton();
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBoxProductDetails = new System.Windows.Forms.GroupBox();
            this.txtSoLuong = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMaSanPham = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtGiaBan = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDonViTinh = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtLoaiSanPham = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTenSanPham = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSuaSanPham = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnXoaSanPham = new System.Windows.Forms.Button();
            this.btnThemSanPham = new System.Windows.Forms.Button();
            this.btnThongKe = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.toolStripTrangChu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBoxProductDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.AutoSize = false;
            this.menuStrip1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuThucUong,
            this.MenuDoAn,
            this.MenuCombo});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1011, 50);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // MenuThucUong
            // 
            this.MenuThucUong.Name = "MenuThucUong";
            this.MenuThucUong.Size = new System.Drawing.Size(112, 46);
            this.MenuThucUong.Text = "Menu Thức Uống";
            this.MenuThucUong.Click += new System.EventHandler(this.MenuThucUong_Click);
            // 
            // MenuDoAn
            // 
            this.MenuDoAn.Name = "MenuDoAn";
            this.MenuDoAn.Size = new System.Drawing.Size(86, 46);
            this.MenuDoAn.Text = "Menu Đồ Ăn";
            this.MenuDoAn.Click += new System.EventHandler(this.MenuDoAn_Click);
            // 
            // MenuCombo
            // 
            this.MenuCombo.Name = "MenuCombo";
            this.MenuCombo.Size = new System.Drawing.Size(59, 46);
            this.MenuCombo.Text = "Combo";
            this.MenuCombo.Click += new System.EventHandler(this.MenuCombo_Click);
            // 
            // toolStripTrangChu
            // 
            this.toolStripTrangChu.BackColor = System.Drawing.Color.White;
            this.toolStripTrangChu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnBia,
            this.btnRuou,
            this.btnNuocNgot,
            this.btnNuocSuoi,
            this.btnDoAnNhe,
            this.btnTraiCay,
            this.btnCanhBao,
            this.btnCombo,
            this.btnThoat});
            this.toolStripTrangChu.Location = new System.Drawing.Point(0, 50);
            this.toolStripTrangChu.Name = "toolStripTrangChu";
            this.toolStripTrangChu.Size = new System.Drawing.Size(1011, 155);
            this.toolStripTrangChu.TabIndex = 4;
            this.toolStripTrangChu.Text = "toolStrip1";
            // 
            // btnBia
            // 
            this.btnBia.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnBia.Image = ((System.Drawing.Image)(resources.GetObject("btnBia.Image")));
            this.btnBia.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnBia.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBia.Name = "btnBia";
            this.btnBia.Size = new System.Drawing.Size(132, 152);
            this.btnBia.Text = "Bia";
            this.btnBia.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnBia.Click += new System.EventHandler(this.btnBia_Click);
            // 
            // btnRuou
            // 
            this.btnRuou.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnRuou.Image = ((System.Drawing.Image)(resources.GetObject("btnRuou.Image")));
            this.btnRuou.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnRuou.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRuou.Name = "btnRuou";
            this.btnRuou.Size = new System.Drawing.Size(132, 152);
            this.btnRuou.Text = "Rượu";
            this.btnRuou.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRuou.Visible = false;
            this.btnRuou.Click += new System.EventHandler(this.btnRuou_Click);
            // 
            // btnNuocNgot
            // 
            this.btnNuocNgot.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnNuocNgot.Image = ((System.Drawing.Image)(resources.GetObject("btnNuocNgot.Image")));
            this.btnNuocNgot.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnNuocNgot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuocNgot.Name = "btnNuocNgot";
            this.btnNuocNgot.Size = new System.Drawing.Size(132, 152);
            this.btnNuocNgot.Text = "Nước Ngọt";
            this.btnNuocNgot.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNuocNgot.Visible = false;
            this.btnNuocNgot.Click += new System.EventHandler(this.btnNuocNgot_Click);
            // 
            // btnNuocSuoi
            // 
            this.btnNuocSuoi.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnNuocSuoi.Image = ((System.Drawing.Image)(resources.GetObject("btnNuocSuoi.Image")));
            this.btnNuocSuoi.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnNuocSuoi.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuocSuoi.Name = "btnNuocSuoi";
            this.btnNuocSuoi.Size = new System.Drawing.Size(132, 152);
            this.btnNuocSuoi.Text = "Nước Suối";
            this.btnNuocSuoi.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNuocSuoi.Visible = false;
            this.btnNuocSuoi.Click += new System.EventHandler(this.btnNuocSuoi_Click);
            // 
            // btnDoAnNhe
            // 
            this.btnDoAnNhe.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnDoAnNhe.Image = ((System.Drawing.Image)(resources.GetObject("btnDoAnNhe.Image")));
            this.btnDoAnNhe.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnDoAnNhe.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDoAnNhe.Name = "btnDoAnNhe";
            this.btnDoAnNhe.Size = new System.Drawing.Size(132, 152);
            this.btnDoAnNhe.Text = "Đồ Ăn Nhẹ";
            this.btnDoAnNhe.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDoAnNhe.Visible = false;
            this.btnDoAnNhe.Click += new System.EventHandler(this.btnDoAnNhe_Click);
            // 
            // btnTraiCay
            // 
            this.btnTraiCay.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnTraiCay.Image = ((System.Drawing.Image)(resources.GetObject("btnTraiCay.Image")));
            this.btnTraiCay.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnTraiCay.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTraiCay.Name = "btnTraiCay";
            this.btnTraiCay.Size = new System.Drawing.Size(132, 152);
            this.btnTraiCay.Text = "Trái Cây";
            this.btnTraiCay.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnTraiCay.Click += new System.EventHandler(this.btnTraiCay_Click);
            // 
            // btnCanhBao
            // 
            this.btnCanhBao.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnCanhBao.Image = ((System.Drawing.Image)(resources.GetObject("btnCanhBao.Image")));
            this.btnCanhBao.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnCanhBao.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCanhBao.Name = "btnCanhBao";
            this.btnCanhBao.Size = new System.Drawing.Size(132, 152);
            this.btnCanhBao.Text = "Cảnh báo";
            this.btnCanhBao.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCanhBao.Click += new System.EventHandler(this.btnCanhBao_Click);
            // 
            // btnCombo
            // 
            this.btnCombo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnCombo.Image = ((System.Drawing.Image)(resources.GetObject("btnCombo.Image")));
            this.btnCombo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnCombo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCombo.Name = "btnCombo";
            this.btnCombo.Size = new System.Drawing.Size(132, 152);
            this.btnCombo.Text = "Combo Phổ Biến";
            this.btnCombo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCombo.Click += new System.EventHandler(this.btnCombo_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.AutoSize = false;
            this.btnThoat.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnThoat.Image = ((System.Drawing.Image)(resources.GetObject("btnThoat.Image")));
            this.btnThoat.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnThoat.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnThoat.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(132, 152);
            this.btnThoat.Text = "Thoát";
            this.btnThoat.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnThoat.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.AutoScroll = true;
            this.flowLayoutPanel.Location = new System.Drawing.Point(6, 28);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(585, 390);
            this.flowLayoutPanel.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.flowLayoutPanel);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.groupBox1.Location = new System.Drawing.Point(0, 208);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(597, 425);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Danh Sách Sản Phẩm";
            // 
            // groupBoxProductDetails
            // 
            this.groupBoxProductDetails.Controls.Add(this.txtSoLuong);
            this.groupBoxProductDetails.Controls.Add(this.label1);
            this.groupBoxProductDetails.Controls.Add(this.txtMaSanPham);
            this.groupBoxProductDetails.Controls.Add(this.label11);
            this.groupBoxProductDetails.Controls.Add(this.txtGiaBan);
            this.groupBoxProductDetails.Controls.Add(this.label6);
            this.groupBoxProductDetails.Controls.Add(this.txtDonViTinh);
            this.groupBoxProductDetails.Controls.Add(this.label5);
            this.groupBoxProductDetails.Controls.Add(this.txtLoaiSanPham);
            this.groupBoxProductDetails.Controls.Add(this.label4);
            this.groupBoxProductDetails.Controls.Add(this.txtTenSanPham);
            this.groupBoxProductDetails.Controls.Add(this.label3);
            this.groupBoxProductDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.groupBoxProductDetails.Location = new System.Drawing.Point(603, 208);
            this.groupBoxProductDetails.Name = "groupBoxProductDetails";
            this.groupBoxProductDetails.Size = new System.Drawing.Size(399, 255);
            this.groupBoxProductDetails.TabIndex = 6;
            this.groupBoxProductDetails.TabStop = false;
            // 
            // txtSoLuong
            // 
            this.txtSoLuong.Enabled = false;
            this.txtSoLuong.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtSoLuong.Location = new System.Drawing.Point(104, 169);
            this.txtSoLuong.Name = "txtSoLuong";
            this.txtSoLuong.Size = new System.Drawing.Size(85, 29);
            this.txtSoLuong.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(30, 178);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 20;
            this.label1.Text = "Số lượng:";
            // 
            // txtMaSanPham
            // 
            this.txtMaSanPham.Enabled = false;
            this.txtMaSanPham.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtMaSanPham.Location = new System.Drawing.Point(142, 38);
            this.txtMaSanPham.Name = "txtMaSanPham";
            this.txtMaSanPham.Size = new System.Drawing.Size(150, 26);
            this.txtMaSanPham.TabIndex = 19;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label11.Location = new System.Drawing.Point(30, 44);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(100, 17);
            this.label11.TabIndex = 18;
            this.label11.Text = "Mã Sản Phẩm:";
            // 
            // txtGiaBan
            // 
            this.txtGiaBan.Enabled = false;
            this.txtGiaBan.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtGiaBan.Location = new System.Drawing.Point(274, 169);
            this.txtGiaBan.Name = "txtGiaBan";
            this.txtGiaBan.Size = new System.Drawing.Size(103, 29);
            this.txtGiaBan.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label6.Location = new System.Drawing.Point(206, 178);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 17);
            this.label6.TabIndex = 8;
            this.label6.Text = "Giá bán:";
            // 
            // txtDonViTinh
            // 
            this.txtDonViTinh.Enabled = false;
            this.txtDonViTinh.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtDonViTinh.Location = new System.Drawing.Point(115, 212);
            this.txtDonViTinh.Name = "txtDonViTinh";
            this.txtDonViTinh.Size = new System.Drawing.Size(92, 29);
            this.txtDonViTinh.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label5.Location = new System.Drawing.Point(30, 221);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 17);
            this.label5.TabIndex = 6;
            this.label5.Text = "Đơn vị tính:";
            // 
            // txtLoaiSanPham
            // 
            this.txtLoaiSanPham.Enabled = false;
            this.txtLoaiSanPham.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtLoaiSanPham.Location = new System.Drawing.Point(142, 124);
            this.txtLoaiSanPham.Name = "txtLoaiSanPham";
            this.txtLoaiSanPham.Size = new System.Drawing.Size(99, 29);
            this.txtLoaiSanPham.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label4.Location = new System.Drawing.Point(30, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "Loại Sản Phẩm:";
            // 
            // txtTenSanPham
            // 
            this.txtTenSanPham.Enabled = false;
            this.txtTenSanPham.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtTenSanPham.Location = new System.Drawing.Point(142, 77);
            this.txtTenSanPham.Name = "txtTenSanPham";
            this.txtTenSanPham.Size = new System.Drawing.Size(235, 29);
            this.txtTenSanPham.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.Location = new System.Drawing.Point(30, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Tên Sản Phẩm:";
            // 
            // btnSuaSanPham
            // 
            this.btnSuaSanPham.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSuaSanPham.Enabled = false;
            this.btnSuaSanPham.Image = ((System.Drawing.Image)(resources.GetObject("btnSuaSanPham.Image")));
            this.btnSuaSanPham.Location = new System.Drawing.Point(877, 476);
            this.btnSuaSanPham.Name = "btnSuaSanPham";
            this.btnSuaSanPham.Size = new System.Drawing.Size(125, 73);
            this.btnSuaSanPham.TabIndex = 50;
            this.btnSuaSanPham.UseVisualStyleBackColor = true;
            this.btnSuaSanPham.Click += new System.EventHandler(this.btnSuaSanPham_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHuy.Image = ((System.Drawing.Image)(resources.GetObject("btnHuy.Image")));
            this.btnHuy.Location = new System.Drawing.Point(812, 563);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(128, 71);
            this.btnHuy.TabIndex = 49;
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnXoaSanPham
            // 
            this.btnXoaSanPham.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXoaSanPham.Enabled = false;
            this.btnXoaSanPham.Image = ((System.Drawing.Image)(resources.GetObject("btnXoaSanPham.Image")));
            this.btnXoaSanPham.Location = new System.Drawing.Point(742, 476);
            this.btnXoaSanPham.Name = "btnXoaSanPham";
            this.btnXoaSanPham.Size = new System.Drawing.Size(126, 73);
            this.btnXoaSanPham.TabIndex = 48;
            this.btnXoaSanPham.UseVisualStyleBackColor = true;
            this.btnXoaSanPham.Click += new System.EventHandler(this.btnXoaSanPham_Click);
            // 
            // btnThemSanPham
            // 
            this.btnThemSanPham.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThemSanPham.Image = ((System.Drawing.Image)(resources.GetObject("btnThemSanPham.Image")));
            this.btnThemSanPham.Location = new System.Drawing.Point(604, 476);
            this.btnThemSanPham.Name = "btnThemSanPham";
            this.btnThemSanPham.Size = new System.Drawing.Size(129, 73);
            this.btnThemSanPham.TabIndex = 47;
            this.btnThemSanPham.UseVisualStyleBackColor = true;
            this.btnThemSanPham.Click += new System.EventHandler(this.btnThemSanPham_Click);
            // 
            // btnThongKe
            // 
            this.btnThongKe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThongKe.Image = ((System.Drawing.Image)(resources.GetObject("btnThongKe.Image")));
            this.btnThongKe.Location = new System.Drawing.Point(672, 564);
            this.btnThongKe.Name = "btnThongKe";
            this.btnThongKe.Size = new System.Drawing.Size(129, 71);
            this.btnThongKe.TabIndex = 51;
            this.btnThongKe.UseVisualStyleBackColor = true;
            this.btnThongKe.Click += new System.EventHandler(this.btnThongKe_Click);
            // 
            // frmQLSP_Kho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(1011, 646);
            this.Controls.Add(this.btnThongKe);
            this.Controls.Add(this.btnSuaSanPham);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnXoaSanPham);
            this.Controls.Add(this.btnThemSanPham);
            this.Controls.Add(this.groupBoxProductDetails);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripTrangChu);
            this.Controls.Add(this.menuStrip1);
            this.Name = "frmQLSP_Kho";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý sản phẩm và kho hàng";
            this.Load += new System.EventHandler(this.frmQLSP_Kho_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStripTrangChu.ResumeLayout(false);
            this.toolStripTrangChu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBoxProductDetails.ResumeLayout(false);
            this.groupBoxProductDetails.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MenuThucUong;
        private System.Windows.Forms.ToolStripMenuItem MenuDoAn;
        private System.Windows.Forms.ToolStripMenuItem MenuCombo;
        private System.Windows.Forms.ToolStrip toolStripTrangChu;
        private System.Windows.Forms.ToolStripButton btnBia;
        private System.Windows.Forms.ToolStripButton btnRuou;
        private System.Windows.Forms.ToolStripButton btnNuocNgot;
        private System.Windows.Forms.ToolStripButton btnNuocSuoi;
        private System.Windows.Forms.ToolStripButton btnDoAnNhe;
        private System.Windows.Forms.ToolStripButton btnTraiCay;
        private System.Windows.Forms.ToolStripButton btnCombo;
        private System.Windows.Forms.ToolStripButton btnThoat;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBoxProductDetails;
        private System.Windows.Forms.TextBox txtSoLuong;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMaSanPham;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtGiaBan;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDonViTinh;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtLoaiSanPham;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTenSanPham;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSuaSanPham;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.Button btnXoaSanPham;
        private System.Windows.Forms.Button btnThemSanPham;
        private System.Windows.Forms.ToolStripButton btnCanhBao;
        private System.Windows.Forms.Button btnThongKe;


    }
}