namespace HTQLKaraoke.PhongHat
{
    partial class frmOrder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOrder));
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
            this.btnCombo = new System.Windows.Forms.ToolStripButton();
            this.btnThoat = new System.Windows.Forms.ToolStripButton();
            this.groupBoxProductDetails = new System.Windows.Forms.GroupBox();
            this.txtMaDonHang = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtSoPhong = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtLoaiPhong = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtMaPhong = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
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
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.btnOrder = new System.Windows.Forms.Button();
            this.btnDonHangHienTai = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.toolStripTrangChu.SuspendLayout();
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
            this.menuStrip1.Size = new System.Drawing.Size(1137, 50);
            this.menuStrip1.TabIndex = 4;
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
            this.btnCombo,
            this.btnThoat});
            this.toolStripTrangChu.Location = new System.Drawing.Point(0, 50);
            this.toolStripTrangChu.Name = "toolStripTrangChu";
            this.toolStripTrangChu.Size = new System.Drawing.Size(1137, 155);
            this.toolStripTrangChu.TabIndex = 5;
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
            // groupBoxProductDetails
            // 
            this.groupBoxProductDetails.Controls.Add(this.txtMaDonHang);
            this.groupBoxProductDetails.Controls.Add(this.label10);
            this.groupBoxProductDetails.Controls.Add(this.txtGhiChu);
            this.groupBoxProductDetails.Controls.Add(this.label9);
            this.groupBoxProductDetails.Controls.Add(this.txtSoPhong);
            this.groupBoxProductDetails.Controls.Add(this.label8);
            this.groupBoxProductDetails.Controls.Add(this.txtLoaiPhong);
            this.groupBoxProductDetails.Controls.Add(this.label7);
            this.groupBoxProductDetails.Controls.Add(this.txtMaPhong);
            this.groupBoxProductDetails.Controls.Add(this.label2);
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
            this.groupBoxProductDetails.Location = new System.Drawing.Point(600, 220);
            this.groupBoxProductDetails.Name = "groupBoxProductDetails";
            this.groupBoxProductDetails.Size = new System.Drawing.Size(525, 371);
            this.groupBoxProductDetails.TabIndex = 8;
            this.groupBoxProductDetails.TabStop = false;
            // 
            // txtMaDonHang
            // 
            this.txtMaDonHang.Enabled = false;
            this.txtMaDonHang.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtMaDonHang.Location = new System.Drawing.Point(129, 27);
            this.txtMaDonHang.Name = "txtMaDonHang";
            this.txtMaDonHang.Size = new System.Drawing.Size(136, 26);
            this.txtMaDonHang.TabIndex = 31;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label10.Location = new System.Drawing.Point(23, 33);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(99, 17);
            this.label10.TabIndex = 30;
            this.label10.Text = "Mã Đơn Hàng:";
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Enabled = false;
            this.txtGhiChu.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtGhiChu.Location = new System.Drawing.Point(89, 292);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.ReadOnly = true;
            this.txtGhiChu.Size = new System.Drawing.Size(329, 61);
            this.txtGhiChu.TabIndex = 29;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label9.Location = new System.Drawing.Point(20, 301);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 17);
            this.label9.TabIndex = 28;
            this.label9.Text = "Ghi Chú:";
            // 
            // txtSoPhong
            // 
            this.txtSoPhong.Enabled = false;
            this.txtSoPhong.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtSoPhong.Location = new System.Drawing.Point(367, 73);
            this.txtSoPhong.Name = "txtSoPhong";
            this.txtSoPhong.Size = new System.Drawing.Size(74, 26);
            this.txtSoPhong.TabIndex = 27;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label8.Location = new System.Drawing.Point(282, 79);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 17);
            this.label8.TabIndex = 26;
            this.label8.Text = "Số Phòng:";
            // 
            // txtLoaiPhong
            // 
            this.txtLoaiPhong.Enabled = false;
            this.txtLoaiPhong.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtLoaiPhong.Location = new System.Drawing.Point(105, 119);
            this.txtLoaiPhong.Name = "txtLoaiPhong";
            this.txtLoaiPhong.Size = new System.Drawing.Size(146, 26);
            this.txtLoaiPhong.TabIndex = 25;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label7.Location = new System.Drawing.Point(23, 125);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 17);
            this.label7.TabIndex = 24;
            this.label7.Text = "Loại Phòng:";
            // 
            // txtMaPhong
            // 
            this.txtMaPhong.Enabled = false;
            this.txtMaPhong.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtMaPhong.Location = new System.Drawing.Point(367, 27);
            this.txtMaPhong.Name = "txtMaPhong";
            this.txtMaPhong.Size = new System.Drawing.Size(130, 26);
            this.txtMaPhong.TabIndex = 23;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(282, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 17);
            this.label2.TabIndex = 22;
            this.label2.Text = "Mã Phòng:";
            // 
            // txtSoLuong
            // 
            this.txtSoLuong.Enabled = false;
            this.txtSoLuong.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtSoLuong.Location = new System.Drawing.Point(105, 214);
            this.txtSoLuong.Name = "txtSoLuong";
            this.txtSoLuong.Size = new System.Drawing.Size(92, 29);
            this.txtSoLuong.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(20, 223);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 20;
            this.label1.Text = "Số lượng:";
            // 
            // txtMaSanPham
            // 
            this.txtMaSanPham.Enabled = false;
            this.txtMaSanPham.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtMaSanPham.Location = new System.Drawing.Point(129, 76);
            this.txtMaSanPham.Name = "txtMaSanPham";
            this.txtMaSanPham.Size = new System.Drawing.Size(136, 26);
            this.txtMaSanPham.TabIndex = 19;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label11.Location = new System.Drawing.Point(23, 82);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(100, 17);
            this.label11.TabIndex = 18;
            this.label11.Text = "Mã Sản Phẩm:";
            // 
            // txtGiaBan
            // 
            this.txtGiaBan.Enabled = false;
            this.txtGiaBan.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtGiaBan.Location = new System.Drawing.Point(343, 214);
            this.txtGiaBan.Name = "txtGiaBan";
            this.txtGiaBan.Size = new System.Drawing.Size(147, 29);
            this.txtGiaBan.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label6.Location = new System.Drawing.Point(275, 223);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 17);
            this.label6.TabIndex = 8;
            this.label6.Text = "Giá bán:";
            // 
            // txtDonViTinh
            // 
            this.txtDonViTinh.Enabled = false;
            this.txtDonViTinh.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtDonViTinh.Location = new System.Drawing.Point(105, 257);
            this.txtDonViTinh.Name = "txtDonViTinh";
            this.txtDonViTinh.Size = new System.Drawing.Size(92, 29);
            this.txtDonViTinh.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label5.Location = new System.Drawing.Point(20, 266);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 17);
            this.label5.TabIndex = 6;
            this.label5.Text = "Đơn vị tính:";
            // 
            // txtLoaiSanPham
            // 
            this.txtLoaiSanPham.Enabled = false;
            this.txtLoaiSanPham.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtLoaiSanPham.Location = new System.Drawing.Point(398, 116);
            this.txtLoaiSanPham.Name = "txtLoaiSanPham";
            this.txtLoaiSanPham.Size = new System.Drawing.Size(99, 29);
            this.txtLoaiSanPham.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label4.Location = new System.Drawing.Point(282, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "Loại Sản Phẩm:";
            // 
            // txtTenSanPham
            // 
            this.txtTenSanPham.Enabled = false;
            this.txtTenSanPham.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtTenSanPham.Location = new System.Drawing.Point(139, 165);
            this.txtTenSanPham.Name = "txtTenSanPham";
            this.txtTenSanPham.Size = new System.Drawing.Size(164, 29);
            this.txtTenSanPham.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.Location = new System.Drawing.Point(23, 174);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Tên Sản Phẩm:";
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.AutoScroll = true;
            this.flowLayoutPanel.Location = new System.Drawing.Point(12, 220);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(580, 462);
            this.flowLayoutPanel.TabIndex = 7;
            // 
            // btnOrder
            // 
            this.btnOrder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOrder.Image = ((System.Drawing.Image)(resources.GetObject("btnOrder.Image")));
            this.btnOrder.Location = new System.Drawing.Point(715, 608);
            this.btnOrder.Name = "btnOrder";
            this.btnOrder.Size = new System.Drawing.Size(126, 74);
            this.btnOrder.TabIndex = 51;
            this.btnOrder.UseVisualStyleBackColor = true;
            this.btnOrder.Click += new System.EventHandler(this.btnOrder_Click);
            // 
            // btnDonHangHienTai
            // 
            this.btnDonHangHienTai.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDonHangHienTai.Image = ((System.Drawing.Image)(resources.GetObject("btnDonHangHienTai.Image")));
            this.btnDonHangHienTai.Location = new System.Drawing.Point(916, 608);
            this.btnDonHangHienTai.Name = "btnDonHangHienTai";
            this.btnDonHangHienTai.Size = new System.Drawing.Size(125, 74);
            this.btnDonHangHienTai.TabIndex = 52;
            this.btnDonHangHienTai.UseVisualStyleBackColor = true;
            this.btnDonHangHienTai.Click += new System.EventHandler(this.btnDonHangHienTai_Click);
            // 
            // frmOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(1137, 718);
            this.Controls.Add(this.btnDonHangHienTai);
            this.Controls.Add(this.btnOrder);
            this.Controls.Add(this.groupBoxProductDetails);
            this.Controls.Add(this.flowLayoutPanel);
            this.Controls.Add(this.toolStripTrangChu);
            this.Controls.Add(this.menuStrip1);
            this.Name = "frmOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Order Thực Đơn Cho Phòng";
            this.Load += new System.EventHandler(this.frmOrder_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStripTrangChu.ResumeLayout(false);
            this.toolStripTrangChu.PerformLayout();
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
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.Button btnOrder;
        private System.Windows.Forms.TextBox txtLoaiPhong;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtMaPhong;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtSoPhong;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtMaDonHang;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnDonHangHienTai;
    }
}