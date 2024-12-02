using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Configuration;
using System.IO;

namespace HTQLKaraoke.PhongHat
{
    public partial class frmDonHangHienTai : Form
    {
        string connection = ConfigurationManager.ConnectionStrings["HTQLKaraoke.Properties.Settings.KaraokeConnectionString"].ConnectionString;
        string maDonHang;
        public frmDonHangHienTai(string maDonHang)
        {
            InitializeComponent();
            this.maDonHang = maDonHang;
        }

        private void frmDonHangHienTai_Load(object sender, EventArgs e)
        {
            LoadDonHang();
        }

        private void LoadDonHang()
        {
            string queryChiTietDonHang = @"
                SELECT 
                    ChiTietDonHang.MaDonHang, 
                    ChiTietDonHang.MaSanPham, 
                    SanPham.TenSanPham, 
                    SanPham.LoaiSanPham, 
                    SanPham.DonViTinh,
                    ChiTietDonHang.SoLuong, 
                    ChiTietDonHang.DonGia, 
                    ChiTietDonHang.ThanhTien 
                FROM ChiTietDonHang
                INNER JOIN SanPham ON ChiTietDonHang.MaSanPham = SanPham.MaSanPham
                INNER JOIN DonHang ON ChiTietDonHang.MaDonHang = DonHang.MaDonHang
                WHERE ChiTietDonHang.MaDonHang = @MaDonHang AND DonHang.TrangThai = 0";


            string queryDonHang = "SELECT NgayTao, TongTien, TrangThai, GhiChu FROM DonHang WHERE MaDonHang = @MaDonHang";

            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(queryChiTietDonHang, conn))
                {
                    cmd.Parameters.AddWithValue("@MaDonHang", maDonHang);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dgvCTDH.DataSource = dataTable;
                }

                using (SqlCommand cmd = new SqlCommand(queryDonHang, conn))
                {
                    cmd.Parameters.AddWithValue("@MaDonHang", maDonHang);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txtNgayTao.Text = reader["NgayTao"].ToString();
                        txtTongTien.Text = reader["TongTien"].ToString();
                        txtTrangThai.Text = (bool)reader["TrangThai"] ? "Đã thanh toán" : "Chưa thanh toán";
                        txtGhiChu.Text = reader["GhiChu"].ToString();
                    }
                    reader.Close();
                }

                // Cập nhật tiêu đề các cột trong DataGridView
                dgvCTDH.Columns["MaDonHang"].HeaderText = "Mã Đơn Hàng";
                dgvCTDH.Columns["MaSanPham"].HeaderText = "Mã Sản Phẩm";
                dgvCTDH.Columns["TenSanPham"].HeaderText = "Tên Sản Phẩm";
                dgvCTDH.Columns["LoaiSanPham"].HeaderText = "Loại Sản Phẩm";
                dgvCTDH.Columns["DonViTinh"].HeaderText = "Đơn Vị Tính";
                dgvCTDH.Columns["SoLuong"].HeaderText = "Số Lượng";
                dgvCTDH.Columns["DonGia"].HeaderText = "Đơn Giá";
                dgvCTDH.Columns["ThanhTien"].HeaderText = "Thành Tiền";
                dgvCTDH.AllowUserToAddRows = false;
            }

            TinhTongTien();
        }

        private void TinhTongTien()
        {
            decimal tongTien = 0;
            foreach (DataGridViewRow row in dgvCTDH.Rows)
            {
                tongTien += Convert.ToDecimal(row.Cells["ThanhTien"].Value);
            }

            // Định dạng số tiền theo kiểu tiền tệ của Việt Nam với đơn vị "VND"
            txtTongTien.Text = tongTien.ToString("N0") + " VND";
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            string ghiChuMoi = txtGhiChu.Text;

            string queryUpdateGhiChu = "UPDATE DonHang SET GhiChu = @GhiChu WHERE MaDonHang = @MaDonHang";

            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(queryUpdateGhiChu, conn))
                {
                    cmd.Parameters.AddWithValue("@GhiChu", ghiChuMoi);
                    cmd.Parameters.AddWithValue("@MaDonHang", maDonHang);

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Ghi chú đã được cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTraHang_Click(object sender, EventArgs e)
        {
            if (dgvCTDH.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm để thực hiện trả hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = dgvCTDH.SelectedRows[0];
            string maSanPham = selectedRow.Cells["MaSanPham"].Value.ToString();
            decimal soLuongHienTai = Convert.ToDecimal(selectedRow.Cells["SoLuong"].Value);
            decimal donGia = Convert.ToDecimal(selectedRow.Cells["DonGia"].Value);
            decimal soLuongTra = numSoLuongTra.Value;

            if (soLuongTra > soLuongHienTai)
            {
                MessageBox.Show("Số lượng trả hàng lớn hơn số lượng order, vui lòng chọn lại số lượng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal thanhTienTra = soLuongTra * donGia;

            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();

                // Nếu số lượng sau khi trả bằng 0 thì xoá sản phẩm khỏi ChiTietDonHang
                if (soLuongHienTai - soLuongTra == 0)
                {
                    string queryDelete = "DELETE FROM ChiTietDonHang WHERE MaDonHang = @MaDonHang AND MaSanPham = @MaSanPham";
                    using (SqlCommand cmd = new SqlCommand(queryDelete, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaDonHang", maDonHang);
                        cmd.Parameters.AddWithValue("@MaSanPham", maSanPham);
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    // Cập nhật số lượng và thành tiền nếu sản phẩm vẫn còn sau khi trả
                    string queryUpdateChiTiet = "UPDATE ChiTietDonHang SET SoLuong = SoLuong - @SoLuongTra, ThanhTien = ThanhTien - @ThanhTienTra WHERE MaDonHang = @MaDonHang AND MaSanPham = @MaSanPham";
                    using (SqlCommand cmd = new SqlCommand(queryUpdateChiTiet, conn))
                    {
                        cmd.Parameters.AddWithValue("@SoLuongTra", soLuongTra);
                        cmd.Parameters.AddWithValue("@ThanhTienTra", thanhTienTra);
                        cmd.Parameters.AddWithValue("@MaDonHang", maDonHang);
                        cmd.Parameters.AddWithValue("@MaSanPham", maSanPham);
                        cmd.ExecuteNonQuery();
                    }
                }

                // Cập nhật lại tổng tiền trong bảng DonHang
                decimal tongTienMoi = Convert.ToDecimal(txtTongTien.Text.Replace(" VND", "").Replace(",", "")) - thanhTienTra;
                string queryUpdateDonHang = "UPDATE DonHang SET TongTien = @TongTien WHERE MaDonHang = @MaDonHang";
                using (SqlCommand cmd = new SqlCommand(queryUpdateDonHang, conn))
                {
                    cmd.Parameters.AddWithValue("@TongTien", tongTienMoi);
                    cmd.Parameters.AddWithValue("@MaDonHang", maDonHang);
                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Cập nhật trả hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadDonHang();
        }
    }
}
