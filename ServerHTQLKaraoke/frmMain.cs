using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ServerHTQLKaraoke.DMKH;
using ServerHTQLKaraoke.QLNV;
using ServerHTQLKaraoke.QLHD;
using ServerHTQLKaraoke.QLChiPhi;
using ServerHTQLKaraoke.QLBaoTri;
using ServerHTQLKaraoke.NhatKyHD;
using ServerHTQLKaraoke.DanhGia;
using ServerHTQLKaraoke.ThongKe;
using ServerHTQLKaraoke.TroGiup;

namespace ServerHTQLKaraoke
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            toolStripTrangChu.Visible = true;
            btnQLChiPhi.Visible = false;
            QLBaoTri.Visible = false;
            btnThongKe.Visible = false;
            btnNhatKy.Visible = false;
            btnHuongDan.Visible = false;
            btnLienHe.Visible = false;
            btnThongKe.Visible = true;
            using (frmDangNhap frmDangNhap = new frmDangNhap())
            {
                if (frmDangNhap.ShowDialog() != DialogResult.OK)
                {
                    Application.Exit();
                }
            }
        }

        private void trangChuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnQLKH.Visible = true;
            btnQLNV.Visible = true;
            btnQLHoaDon.Visible = true;
            btnQLChiPhi.Visible = false;
            QLBaoTri.Visible = false;
            btnThongKe.Visible = true;
            btnNhatKy.Visible = false;
            btnDanhGia.Visible = true;
            btnHuongDan.Visible = false;
            btnLienHe.Visible = false;
            btnThoat.Visible = true;
        }

        private void heThongToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            btnQLKH.Visible = false;
            btnQLNV.Visible = false;
            btnQLHoaDon.Visible = false;
            btnQLChiPhi.Visible = false;
            QLBaoTri.Visible = false;
            btnThongKe.Visible = true;
            btnNhatKy.Visible = true;
            btnDanhGia.Visible = false;
            btnHuongDan.Visible = false;
            btnLienHe.Visible = false;
            btnThoat.Visible = true;
        }

        private void quanTriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnQLKH.Visible = true;
            btnQLNV.Visible = true;
            btnQLHoaDon.Visible = true;
            btnQLChiPhi.Visible = true;
            QLBaoTri.Visible = true;
            btnThongKe.Visible = false;
            btnNhatKy.Visible = false;
            btnDanhGia.Visible = false;
            btnHuongDan.Visible = false;
            btnLienHe.Visible = false;
            btnThoat.Visible = true;
        }

        private void troGiupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnQLKH.Visible = false;
            btnQLNV.Visible = false;
            btnQLHoaDon.Visible = false;
            btnQLChiPhi.Visible = false;
            QLBaoTri.Visible = false;
            btnThongKe.Visible = false;
            btnNhatKy.Visible = false;
            btnDanhGia.Visible = false;
            btnHuongDan.Visible = true;
            btnLienHe.Visible = true;
            btnThoat.Visible = true;
        }

        private void btnQLKH_Click(object sender, EventArgs e)
        {
            frmKhachHang frmKhachHang = new frmKhachHang();
            frmKhachHang.ShowDialog();
        }

        private void btnQLNV_Click(object sender, EventArgs e)
        {
            frmQLNV frmQLNV = new frmQLNV();
            frmQLNV.ShowDialog();
        }

        private void btnQLHoaDon_Click(object sender, EventArgs e)
        {
            frmHoaDon frm = new frmHoaDon();
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

        private void btnNhatKy_Click(object sender, EventArgs e)
        {
            frmNhatKyHD frm = new frmNhatKyHD();
            frm.ShowDialog();
        }

        private void btnDanhGia_Click(object sender, EventArgs e)
        {
            frmTTDanhGia frm = new frmTTDanhGia();
            frm.ShowDialog();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            frmThongKe frm = new frmThongKe();
            frm.ShowDialog();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn thoát ứng dụng không?",
                "Xác nhận thoát",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnLienHe_Click(object sender, EventArgs e)
        {
            frmLienHe frm = new frmLienHe();
            frm.ShowDialog();
        }
    }
}
