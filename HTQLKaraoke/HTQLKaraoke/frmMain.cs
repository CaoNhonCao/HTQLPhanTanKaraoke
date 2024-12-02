using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HTQLKaraoke.TrangChu;
using HTQLKaraoke.PhongHat;
using HTQLKaraoke.QLSP_Kho;
using HTQLKaraoke.QLNV;
using HTQLKaraoke.QLDV;
using HTQLKaraoke.HoaDon;
using HTQLKaraoke.QLChiPhi;
using HTQLKaraoke.BaoTri;
using HTQLKaraoke.DanhGia;
using HTQLKaraoke.NhapHang;
using HTQLKaraoke.NhaCungCap;
using HTQLKaraoke.NhatKyHD;
using HTQLKaraoke.ThongKe;
using HTQLKaraoke.TroGiup;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;
using System.Security.Cryptography;

namespace HTQLKaraoke
{
    public partial class frmMain : Form
    {
        private AxWMPLib.AxWindowsMediaPlayer mediaPlayer;
        private Button btnDropdown;
        private Panel leftPanel;
        private Panel rightPanel;
        string loaiPhong;
        CustomToolStripRenderer customRenderer = new CustomToolStripRenderer();

        PictureBox gif1 = new PictureBox();
        PictureBox gif2 = new PictureBox();
        PictureBox gif3 = new PictureBox();
        PictureBox gif4 = new PictureBox();
        PictureBox gif5 = new PictureBox();
        PictureBox gif6 = new PictureBox();
        PictureBox gif7 = new PictureBox();
        PictureBox gif8 = new PictureBox();
        PictureBox gif9 = new PictureBox();
        PictureBox gif10 = new PictureBox();
        PictureBox gif11 = new PictureBox();
        PictureBox gif12 = new PictureBox();
        PictureBox gif13 = new PictureBox();
        PictureBox gif14 = new PictureBox();
        PictureBox gif15 = new PictureBox();
        PictureBox gif16 = new PictureBox();
        PictureBox gif17 = new PictureBox();
        PictureBox gif18 = new PictureBox();
        PictureBox gif19 = new PictureBox();
        PictureBox gif20 = new PictureBox();
        private void ChangeTheme(int themeId)
        {
            switch (themeId)
            {
                case 1:
                    mediaPlayer.URL = @"/HTQLKaraoke/HTQLKaraoke/Video/giangsinh.mp4";
                    mediaPlayer.Visible = true;
                    mediaPlayer.Ctlcontrols.play();
                    mediaPlayer.settings.setMode("loop", true);

                    leftPanel.Controls.Remove(gif11);
                    leftPanel.Controls.Remove(gif12);
                    leftPanel.Controls.Remove(gif13);
                    leftPanel.Controls.Remove(gif14);
                    leftPanel.Controls.Remove(gif15);
                    rightPanel.Controls.Remove(gif16);
                    rightPanel.Controls.Remove(gif17);
                    rightPanel.Controls.Remove(gif18);
                    rightPanel.Controls.Remove(gif19);
                    rightPanel.Controls.Remove(gif20);

                    gif1.Size = new Size(128, 45);
                    gif1.Location = new Point(60, 10);
                    gif1.Image = Image.FromFile(@"/HTQLKaraoke/HTQLKaraoke/Image/giangsinh1.gif");
                    gif1.SizeMode = PictureBoxSizeMode.StretchImage;

                    gif2.Size = new Size(128, 128);
                    gif2.Location = new Point(60, 200);
                    gif2.Image = Image.FromFile(@"/HTQLKaraoke/HTQLKaraoke/Image/giangsinh2.gif");
                    
                    gif3.Size = new Size(83, 128);
                    gif3.Location = new Point(10, 350);
                    gif3.Image = Image.FromFile(@"/HTQLKaraoke/HTQLKaraoke/Image/giangsinh3.gif");

                    gif4.Size = new Size(128, 128);
                    gif4.Location = new Point(100, 370);
                    gif4.Image = Image.FromFile(@"/HTQLKaraoke/HTQLKaraoke/Image/giangsinh4.gif");
                    
                    gif5.Size = new Size(128, 128);
                    gif5.Location = new Point(60, 530);
                    gif5.Image = Image.FromFile(@"/HTQLKaraoke/HTQLKaraoke/Image/giangsinh5.gif");
                    
                    gif6.Size = new Size(128, 45);
                    gif6.Location = new Point(60, 10);
                    gif6.Image = Image.FromFile(@"/HTQLKaraoke/HTQLKaraoke/Image/giangsinh6.gif");
                    gif6.SizeMode = PictureBoxSizeMode.StretchImage;

                    gif7.Size = new Size(128, 128);
                    gif7.Location = new Point(60, 200);
                    gif7.Image = Image.FromFile(@"/HTQLKaraoke/HTQLKaraoke/Image/giangsinh7.gif");
                    
                    gif8.Size = new Size(83, 128);
                    gif8.Location = new Point(10, 350);
                    gif8.Image = Image.FromFile(@"/HTQLKaraoke/HTQLKaraoke/Image/giangsinh8.gif");

                    gif9.Size = new Size(128, 128);
                    gif9.Location = new Point(100, 370);
                    gif9.Image = Image.FromFile(@"/HTQLKaraoke/HTQLKaraoke/Image/giangsinh9.gif");
                    
                    gif10.Size = new Size(128, 128);
                    gif10.Location = new Point(60, 530);
                    gif10.Image = Image.FromFile(@"/HTQLKaraoke/HTQLKaraoke/Image/giangsinh10.gif");

                    leftPanel.Controls.Add(gif1);
                    leftPanel.Controls.Add(gif2);
                    leftPanel.Controls.Add(gif3);
                    leftPanel.Controls.Add(gif4);
                    leftPanel.Controls.Add(gif5);

                    rightPanel.Controls.Add(gif6);
                    rightPanel.Controls.Add(gif7);
                    rightPanel.Controls.Add(gif8);
                    rightPanel.Controls.Add(gif9);
                    rightPanel.Controls.Add(gif10);

                    leftPanel.BackgroundImage = Image.FromFile(@"/HTQLKaraoke/HTQLKaraoke/Image/nengiangsinh.jpg");
                    rightPanel.BackgroundImage = Image.FromFile(@"/HTQLKaraoke/HTQLKaraoke/Image/nengiangsinh1.jpg");

                    leftPanel.BringToFront();
                    rightPanel.BringToFront(); 
                    pictureKar.BringToFront();
                    pictureWelcome.BringToFront();
                    btnVIP.BringToFront();
                    btnNormal.BringToFront();
                    btnNormal1.BringToFront();
                    btnNormal2.BringToFront();
                    btnNormal3.BringToFront();
                    btnNormal4.BringToFront();
                    btnNormal5.BringToFront();
                    btnMaster.BringToFront();

                    toolStripTrangChu.BackgroundImage = Image.FromFile(@"/HTQLKaraoke/HTQLKaraoke/Image/giangsinhtooltrip.png");
                    toolStripTrangChu.ForeColor = Color.Black;
                    toolStripTrangChu.Font = new Font(toolStripTrangChu.Font, FontStyle.Bold);

                    customRenderer.ButtonBackColor = Color.Pink;

                    toolStripTrangChu.Renderer = customRenderer;

                    foreach (ToolStripItem item in toolStripTrangChu.Items)
                    {
                        if (item is ToolStripButton button)
                        {
                            button.BackgroundImage = Image.FromFile(@"/HTQLKaraoke/HTQLKaraoke/Image/buttongiangsinh.png");

                            button.ImageAlign = ContentAlignment.MiddleCenter;
                            button.TextAlign = ContentAlignment.MiddleCenter;

                            button.Font = new Font(button.Font.FontFamily, 10, button.Font.Style);
                            button.Text = button.Text.ToUpper();
                        }
                    }
                    break;
                case 2:
                    mediaPlayer.URL = @"/HTQLKaraoke/HTQLKaraoke/Video/tet.mp4";
                    mediaPlayer.Visible = true;
                    mediaPlayer.Ctlcontrols.play();
                    mediaPlayer.settings.setMode("loop", true);

                    leftPanel.Controls.Remove(gif1);
                    leftPanel.Controls.Remove(gif2);
                    leftPanel.Controls.Remove(gif3);
                    leftPanel.Controls.Remove(gif4);
                    leftPanel.Controls.Remove(gif5);
                    rightPanel.Controls.Remove(gif6);
                    rightPanel.Controls.Remove(gif7);
                    rightPanel.Controls.Remove(gif8);
                    rightPanel.Controls.Remove(gif9);
                    rightPanel.Controls.Remove(gif10);


                    gif11.Size = new Size(128, 52);
                    gif11.Location = new Point(60, 10);
                    gif11.Image = Image.FromFile(@"/HTQLKaraoke/HTQLKaraoke/Image/tet11.gif");

                    gif12.Size = new Size(69, 128);
                    gif12.Location = new Point(10, 200);
                    gif12.Image = Image.FromFile(@"/HTQLKaraoke/HTQLKaraoke/Image/tet12.gif");

                    gif13.Size = new Size(128, 128);
                    gif13.Location = new Point(100, 220);
                    gif13.Image = Image.FromFile(@"/HTQLKaraoke/HTQLKaraoke/Image/tet13.gif");

                    gif14.Size = new Size(128, 128);
                    gif14.Location = new Point(60, 370);
                    gif14.Image = Image.FromFile(@"/HTQLKaraoke/HTQLKaraoke/Image/tet14.gif");

                    gif15.Size = new Size(128, 128);
                    gif15.Location = new Point(70, 510);
                    gif15.Image = Image.FromFile(@"/HTQLKaraoke/HTQLKaraoke/Image/tet15.gif");
                    gif15.SizeMode = PictureBoxSizeMode.StretchImage;

                    gif16.Size = new Size(128, 52);
                    gif16.Location = new Point(60, 10);
                    gif16.Image = Image.FromFile(@"/HTQLKaraoke/HTQLKaraoke/Image/tet16.gif");

                    gif17.Size = new Size(69, 128);
                    gif17.Location = new Point(10, 200);
                    gif17.Image = Image.FromFile(@"/HTQLKaraoke/HTQLKaraoke/Image/tet17.gif");

                    gif18.Size = new Size(128, 128);
                    gif18.Location = new Point(100, 220);
                    gif18.Image = Image.FromFile(@"/HTQLKaraoke/HTQLKaraoke/Image/tet18.gif");

                    gif19.Size = new Size(128, 128);
                    gif19.Location = new Point(60, 370);
                    gif19.Image = Image.FromFile(@"/HTQLKaraoke/HTQLKaraoke/Image/tet19.gif");

                    gif20.Size = new Size(128, 128);
                    gif20.Location = new Point(70, 510);
                    gif20.Image = Image.FromFile(@"/HTQLKaraoke/HTQLKaraoke/Image/tet20.gif");
                    gif20.SizeMode = PictureBoxSizeMode.StretchImage;

                    leftPanel.Controls.Add(gif11);
                    leftPanel.Controls.Add(gif12);
                    leftPanel.Controls.Add(gif13);
                    leftPanel.Controls.Add(gif14);
                    leftPanel.Controls.Add(gif15);

                    rightPanel.Controls.Add(gif16);
                    rightPanel.Controls.Add(gif17);
                    rightPanel.Controls.Add(gif18);
                    rightPanel.Controls.Add(gif19);
                    rightPanel.Controls.Add(gif20);

                    leftPanel.BackgroundImage = Image.FromFile(@"/HTQLKaraoke/HTQLKaraoke/Image/nentet.jpg");
                    rightPanel.BackgroundImage = Image.FromFile(@"/HTQLKaraoke/HTQLKaraoke/Image/nentet1.jpg");


                    leftPanel.BringToFront();
                    rightPanel.BringToFront();
                    pictureKar.BringToFront(); 
                    pictureWelcome.BringToFront();
                    btnVIP.BringToFront();
                    btnNormal.BringToFront();
                    btnNormal1.BringToFront();
                    btnNormal2.BringToFront();
                    btnNormal3.BringToFront();
                    btnNormal4.BringToFront();
                    btnNormal5.BringToFront();
                    btnMaster.BringToFront();

                    toolStripTrangChu.BackgroundImage = Image.FromFile(@"/HTQLKaraoke/HTQLKaraoke/Image/tettoolstrip.jpg");
                    toolStripTrangChu.ForeColor = Color.White;
                    toolStripTrangChu.Font = new Font(toolStripTrangChu.Font, FontStyle.Bold);

                    customRenderer.ButtonBackColor = Color.Orange;

                    toolStripTrangChu.Renderer = customRenderer;

                    foreach (ToolStripItem item in toolStripTrangChu.Items)
                    {
                        if (item is ToolStripButton button)
                        {
                            button.BackgroundImage = Image.FromFile(@"/HTQLKaraoke/HTQLKaraoke/Image/buttontet.png");

                            button.ImageAlign = ContentAlignment.MiddleCenter;
                            button.TextAlign = ContentAlignment.MiddleCenter;

                            button.Font = new Font(button.Font.FontFamily, 10, button.Font.Style);
                            button.Text = button.Text.ToUpper(); 
                        }
                    }
                    break;
                case 3:
                    this.BackColor = Color.White;
                    leftPanel.BackgroundImage = null;
                    rightPanel.BackgroundImage = null;
                    leftPanel.Controls.Remove(gif1);
                    leftPanel.Controls.Remove(gif2);
                    leftPanel.Controls.Remove(gif3);
                    leftPanel.Controls.Remove(gif4);
                    leftPanel.Controls.Remove(gif5);
                    rightPanel.Controls.Remove(gif6);
                    rightPanel.Controls.Remove(gif7);
                    rightPanel.Controls.Remove(gif8);
                    rightPanel.Controls.Remove(gif9);
                    rightPanel.Controls.Remove(gif10);
                    leftPanel.Controls.Remove(gif11);
                    leftPanel.Controls.Remove(gif12);
                    leftPanel.Controls.Remove(gif13);
                    leftPanel.Controls.Remove(gif14);
                    leftPanel.Controls.Remove(gif15);
                    rightPanel.Controls.Remove(gif16);
                    rightPanel.Controls.Remove(gif17);
                    rightPanel.Controls.Remove(gif18);
                    rightPanel.Controls.Remove(gif19);
                    rightPanel.Controls.Remove(gif20);
                    mediaPlayer.Visible = false;
                    mediaPlayer.Ctlcontrols.stop();
                    toolStripTrangChu.BackgroundImage = null;
                    toolStripTrangChu.ForeColor = Color.Black;
                    toolStripTrangChu.Font = new Font(toolStripTrangChu.Font, FontStyle.Bold);

                    customRenderer.ButtonBackColor = Color.Transparent;

                    toolStripTrangChu.Renderer = customRenderer;

                    foreach (ToolStripItem item in toolStripTrangChu.Items)
                    {
                        if (item is ToolStripButton button)
                        {
                            button.BackgroundImage = null;
                            button.BackColor = Color.Transparent;
                        }
                    }
                    break;
            }
        }

        public frmMain()
        {
            InitializeComponent();
            InitializeCustomComponents();
            foreach (ToolStripItem item in toolStripTrangChu.Items)
            {
                if (item is ToolStripButton button)
                {
                    button.BackgroundImage = null;
                    button.BackColor = Color.Transparent;
                }
            }
        }

        private void InitializeCustomComponents()
        {
            // Initialize btnDropdown
            btnDropdown = new Button
            {
                Size = new Size(30, 30),
                Location = new Point(this.Width - 60, 10),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent
            };
            btnDropdown.FlatAppearance.BorderSize = 0;
            btnDropdown.Paint += BtnDropdown_Paint; 
            btnDropdown.Click += BtnDropdown_Click; 
            this.Controls.Add(btnDropdown);
            btnDropdown.BringToFront();

            mediaPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            mediaPlayer.BeginInit();
            this.Controls.Add(mediaPlayer);
            mediaPlayer.EndInit();

            leftPanel = new Panel();
            leftPanel.Size = new Size(250, 800);
            leftPanel.Location = new Point(0, 205);

            rightPanel = new Panel();
            rightPanel.Size = new Size(250, 800);
            rightPanel.Location = new Point(1920 - 250, 205); 

            this.Controls.Add(leftPanel);
            this.Controls.Add(rightPanel);

            mediaPlayer.Size = new Size(1920, 800); 
            mediaPlayer.Location = new Point(0, 205);
            mediaPlayer.uiMode = "none";
            mediaPlayer.Visible = false;

            mediaPlayer.MouseDownEvent += MediaPlayer_MouseDownEvent;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            toolStripTrangChu.Visible = true;
            btnQLSP.Visible = false;
            btnQLChiPhi.Visible = false;
            QLBaoTri.Visible = false;
            btnThongKe.Visible = false;
            btnNhatKy.Visible = false;
            btnNhapHang.Visible = false;
            btnHuongDan.Visible = false;
            btnLienHe.Visible = false;
            btnNhaCungCap.Visible = false;
            btnCaiDatHT.Visible = false;
        }

        private void BtnDropdown_Paint(object sender, PaintEventArgs e)
        {
            Button btn = sender as Button;
            Graphics g = e.Graphics;

            // Vẽ màu nền của nút
            g.FillRectangle(new SolidBrush(Color.CornflowerBlue), 0, 0, btn.Width, btn.Height);

            // Vẽ hình tam giác
            Point[] trianglePoints = {
        new Point(btn.Width / 2, btn.Height - 10),
        new Point(btn.Width - 10, 10),
        new Point(10, 10)
    };
            g.FillPolygon(Brushes.Black, trianglePoints);
        }

        private void BtnDropdown_Click(object sender, EventArgs e)
        {
            ContextMenuStrip menu = new ContextMenuStrip();
            menu.Items.Add("Giao diện giáng sinh", null, (s, ev) => ChangeTheme(1));
            menu.Items.Add("Giao diện tết", null, (s, ev) => ChangeTheme(2));
            menu.Items.Add("Giao diện bình thường", null, (s, ev) => ChangeTheme(3));
            menu.Show(Cursor.Position);
        }

        private void MediaPlayer_MouseDownEvent(object sender, AxWMPLib._WMPOCXEvents_MouseDownEvent e)
        {
            // Chặn người dùng tương tác với MediaPlayer
            mediaPlayer.Enabled = false;
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            if (btnDropdown != null)
            {
                btnDropdown.Location = new Point(this.Width - btnDropdown.Width - 60, 10);
            }
            if (leftPanel != null && rightPanel != null)
            {
                leftPanel.Size = new Size(100, this.ClientSize.Height - 200); 
                rightPanel.Size = new Size(100, this.ClientSize.Height - 200); 
                leftPanel.Location = new Point(0, 200);
                rightPanel.Location = new Point(this.ClientSize.Width - rightPanel.Width, 200);
            }
        }


        private void khoHangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnQLKH.Visible = false;
            btnQLPhongHat.Visible = false;
            btnQLNV.Visible = false;
            btnQLHoaDon.Visible = false;
            btnQLSP.Visible = true;
            btnQLDV.Visible = false;
            btnQLChiPhi.Visible = false;
            QLBaoTri.Visible = false;
            btnThongKe.Visible = false;
            btnNhatKy.Visible = false;
            btnDanhGia.Visible = false;
            btnNhapHang.Visible = true;
            btnHuongDan.Visible = false;
            btnLienHe.Visible = false;
            btnCaiDatHT.Visible = false;
            btnThoat.Visible = true;
            btnNhaCungCap.Visible = true;
        }
        private void trangChuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnQLKH.Visible = true;
            btnQLPhongHat.Visible = true;
            btnQLNV.Visible = true;
            btnQLHoaDon.Visible = true;
            btnQLSP.Visible = false;
            btnQLDV.Visible = true;
            btnQLChiPhi.Visible = false;
            QLBaoTri.Visible = false;
            btnThongKe.Visible = false;
            btnNhatKy.Visible = false;
            btnDanhGia.Visible = true;
            btnNhapHang.Visible = false;
            btnHuongDan.Visible = false;
            btnLienHe.Visible = false;
            btnCaiDatHT.Visible = false;
            btnThoat.Visible = true;
            btnNhaCungCap.Visible = false;
            pictureKar.Visible = false;
            btnVIP.Visible = false;
            btnNormal.Visible = false;
            btnNormal1.Visible = false;
            btnNormal2.Visible = false;
            btnNormal3.Visible = false;
            btnNormal4.Visible = false;
            btnNormal5.Visible = false;
            btnMaster.Visible = false;
            pictureWelcome.Visible = false;
        }

        private void heThongToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            btnQLKH.Visible = false;
            btnQLPhongHat.Visible = false;
            btnQLNV.Visible = false;
            btnQLHoaDon.Visible = false;
            btnQLSP.Visible = false;
            btnQLDV.Visible = false;
            btnQLChiPhi.Visible = false;
            QLBaoTri.Visible = false;
            btnThongKe.Visible = true;
            btnNhatKy.Visible = true;
            btnDanhGia.Visible = false;
            btnNhapHang.Visible = false;
            btnHuongDan.Visible = false;
            btnLienHe.Visible = false;
            btnCaiDatHT.Visible = true;
            btnThoat.Visible = true;
            btnNhaCungCap.Visible = false;
        }
        private void quanTriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnQLKH.Visible = true;
            btnQLPhongHat.Visible = true;
            btnQLNV.Visible = true;
            btnQLHoaDon.Visible = true;
            btnQLSP.Visible = true;
            btnQLDV.Visible = true;
            btnQLChiPhi.Visible = true;
            QLBaoTri.Visible = true;
            btnThongKe.Visible = false;
            btnNhatKy.Visible = false;
            btnDanhGia.Visible = false;
            btnNhapHang.Visible = false;
            btnHuongDan.Visible = false;
            btnLienHe.Visible = false;
            btnCaiDatHT.Visible = false;
            btnNhaCungCap.Visible = false;
            btnThoat.Visible = true;
        }

        private void troGiupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnQLKH.Visible = false;
            btnQLPhongHat.Visible = false;
            btnQLNV.Visible = false;
            btnQLHoaDon.Visible = false;
            btnQLSP.Visible = false;
            btnQLDV.Visible = false;
            btnQLChiPhi.Visible = false;
            QLBaoTri.Visible = false;
            btnThongKe.Visible = false;
            btnNhatKy.Visible = false;
            btnDanhGia.Visible = false;
            btnNhapHang.Visible = false;
            btnHuongDan.Visible = true;
            btnLienHe.Visible = true;
            btnCaiDatHT.Visible = false;
            btnNhaCungCap.Visible = false;
            btnThoat.Visible = true;
        }

        private void btnQLKH_Click(object sender, EventArgs e)
        {
            frmKhachHang frmKhachHang = new frmKhachHang();
            frmKhachHang.ShowDialog();
        }

        private void btnQLPhongHat_Click(object sender, EventArgs e)
        {
            // Hiển thị các control
            btnVIP.Visible = true;
            btnNormal.Visible = true;
            btnNormal1.Visible = true;
            btnNormal2.Visible = true;
            btnNormal3.Visible = true;
            btnNormal4.Visible = true;
            btnNormal5.Visible = true;
            btnMaster.Visible = true;
            pictureWelcome.Visible = true;
            btnNhaCungCap.Visible = false;
            pictureKar.Visible = true;

            // Đưa các control lên trên cùng
            pictureKar.BringToFront();
            pictureWelcome.BringToFront();
            btnVIP.BringToFront();
            btnNormal.BringToFront();
            btnNormal1.BringToFront();
            btnNormal2.BringToFront();
            btnNormal3.BringToFront();
            btnNormal4.BringToFront();
            btnNormal5.BringToFront();
            btnMaster.BringToFront();
        }



        private void btnNormal_Click(object sender, EventArgs e)
        {
            // Khu thường dân dưới đái xã hội
            loaiPhong = "Phòng Thường";
            frmPhongThuongDan frmThuongDan = new frmPhongThuongDan(loaiPhong);
            frmThuongDan.ShowDialog();
        }

        private void btnQLSP_Click(object sender, EventArgs e)
        {
            frmQLSP_Kho frmQLSP_Kho = new frmQLSP_Kho();
            frmQLSP_Kho.ShowDialog();
        }

        private void btnQLNV_Click(object sender, EventArgs e)
        {
            frmQLNV frmQLNV = new frmQLNV();
            frmQLNV.ShowDialog();
        }

        private void btnVIP_Click(object sender, EventArgs e)
        {
            loaiPhong = "Phòng VIP";
            frmPhongThuongDan frmPhongVIP = new frmPhongThuongDan(loaiPhong);
            frmPhongVIP.ShowDialog();
        }

        private void btnMaster_Click(object sender, EventArgs e)
        {
            loaiPhong = "Phòng Master";
            frmPhongThuongDan frmPhongMaster = new frmPhongThuongDan(loaiPhong);
            frmPhongMaster.ShowDialog();
        }

        private void btnQLDV_Click(object sender, EventArgs e)
        {
            frmQLDV frmQLDV = new frmQLDV();
            frmQLDV.ShowDialog();
        }

        private void btnCaiDatHT_Click(object sender, EventArgs e)
        {
            frmCaiDatHeThong frm = new frmCaiDatHeThong();
            frm.ShowDialog();
        }

        private void btnQLHoaDon_Click(object sender, EventArgs e)
        {
            frmQLHoaDon frm = new frmQLHoaDon();
            frm.ShowDialog();
        }

        private void btnQLChiPhi_Click(object sender, EventArgs e)
        {
            frmQLChiPhi frm = new frmQLChiPhi();
            frm.ShowDialog();
        }

        private void QLBaoTri_Click(object sender, EventArgs e)
        {
            frmLichSuBaoTri frm = new frmLichSuBaoTri();
            frm.ShowDialog();
        }

        private void btnDanhGia_Click(object sender, EventArgs e)
        {
            frmTTDanhGia frm = new frmTTDanhGia();
            frm.ShowDialog();
        }

        private void btnNhapHang_Click(object sender, EventArgs e)
        {
            frmQLNhapHang frm = new frmQLNhapHang();
            frm.ShowDialog();
        }

        private void btnNhaCungCap_Click(object sender, EventArgs e)
        {
            frmNhaCungCap frm = new frmNhaCungCap();
            frm.ShowDialog();
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn thoát ứng dụng không?",
                "Xác nhận thoát",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly
            );

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnNhatKy_Click(object sender, EventArgs e)
        {
            frmNhatKyHD frm = new frmNhatKyHD();
            frm.ShowDialog();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            frmThongKe frm = new frmThongKe();
            frm.ShowDialog();
        }

        private void btnLienHe_Click(object sender, EventArgs e)
        {
            frmLienHe frm = new frmLienHe();
            frm.ShowDialog();
        }
    }

    public class CustomToolStripRenderer : ToolStripProfessionalRenderer
    {
        // Thuộc tính để nhận màu nền
        public Color ButtonBackColor { get; set; } = Color.Orange; // Mặc định là cam

        public CustomToolStripRenderer() : base() { }

        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
        {
            base.OnRenderButtonBackground(e);

            // Vẽ màu nền sử dụng ButtonBackColor
            using (var brush = new SolidBrush(ButtonBackColor))
            {
                e.Graphics.FillRectangle(brush, e.Item.ContentRectangle);
            }

            // Vẽ BackgroundImage nếu có
            if (e.Item.BackgroundImage != null)
            {
                e.Graphics.DrawImage(
                    e.Item.BackgroundImage,
                    e.Item.ContentRectangle,
                    new Rectangle(0, 0, e.Item.BackgroundImage.Width, e.Item.BackgroundImage.Height),
                    GraphicsUnit.Pixel);
            }
        }
    }
}
