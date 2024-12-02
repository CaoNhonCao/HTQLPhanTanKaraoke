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
    public partial class frmOrderDetails : Form
    {
        string connection = ConfigurationManager.ConnectionStrings["HTQLKaraoke.Properties.Settings.KaraokeConnectionString"].ConnectionString;
        private string maDonHang;
        private string maSanPham;
        private string tenSanPham;
        private string maPhong;
        private decimal donGia;
        public frmOrderDetails(string maDonHang, string maSanPham, string maPhong, string tenSanPham, decimal donGia)
        {
            InitializeComponent();
            this.maDonHang = maDonHang;
            this.maSanPham = maSanPham;
            this.maPhong = maPhong;
            this.tenSanPham = tenSanPham;
            this.donGia = donGia;

            txtMaDonHang.Text = maDonHang;
            txtMaSanPham.Text = maSanPham;
            txtDonGia.Text = donGia.ToString("N0");
            txtTenSanPham.Text = tenSanPham;

            numSoLuong.ValueChanged += new EventHandler(this.numSoLuong_ValueChanged);
        }


        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            decimal soLuong = numSoLuong.Value;
            decimal thanhTien = soLuong * donGia;

            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();

                // Kiểm tra xem sản phẩm đã tồn tại trong chi tiết đơn hàng hay chưa
                string queryCheck = "SELECT SoLuong FROM ChiTietDonHang WHERE MaDonHang = @MaDonHang AND MaSanPham = @MaSanPham";
                using (SqlCommand cmdCheck = new SqlCommand(queryCheck, conn))
                {
                    cmdCheck.Parameters.AddWithValue("@MaDonHang", maDonHang);
                    cmdCheck.Parameters.AddWithValue("@MaSanPham", maSanPham);
                    object result = cmdCheck.ExecuteScalar();

                    if (result != null)
                    {
                        // Sản phẩm đã tồn tại, cập nhật số lượng và thành tiền
                        decimal soLuongHienTai = Convert.ToDecimal(result);
                        decimal soLuongMoi = soLuongHienTai + soLuong;
                        decimal thanhTienMoi = soLuongMoi * donGia;

                        string queryUpdate = "UPDATE ChiTietDonHang SET SoLuong = @SoLuong, ThanhTien = @ThanhTien WHERE MaDonHang = @MaDonHang AND MaSanPham = @MaSanPham";
                        using (SqlCommand cmdUpdate = new SqlCommand(queryUpdate, conn))
                        {
                            cmdUpdate.Parameters.AddWithValue("@SoLuong", soLuongMoi);
                            cmdUpdate.Parameters.AddWithValue("@ThanhTien", thanhTienMoi);
                            cmdUpdate.Parameters.AddWithValue("@MaDonHang", maDonHang);
                            cmdUpdate.Parameters.AddWithValue("@MaSanPham", maSanPham);

                            cmdUpdate.ExecuteNonQuery(); // Cập nhật chi tiết đơn hàng
                        }

                        // Cập nhật tổng tiền đơn hàng
                        decimal tongTien = GetTongTienDonHang(maDonHang, conn);
                        string updateDonHangQuery = "UPDATE DonHang SET TongTien = @TongTien WHERE MaDonHang = @MaDonHang";
                        using (SqlCommand cmdUpdateDonHang = new SqlCommand(updateDonHangQuery, conn))
                        {
                            cmdUpdateDonHang.Parameters.AddWithValue("@TongTien", tongTien);
                            cmdUpdateDonHang.Parameters.AddWithValue("@MaDonHang", maDonHang);
                            cmdUpdateDonHang.ExecuteNonQuery(); // Cập nhật đơn hàng
                        }

                        MessageBox.Show("Số lượng đã được cập nhật.", "Thông báo");
                    }
                    else
                    {
                        // Sản phẩm chưa tồn tại, thêm mới vào ChiTietDonHang
                        string queryChiTiet = "INSERT INTO ChiTietDonHang (MaDonHang, MaSanPham, SoLuong, DonGia, ThanhTien) " +
                                              "VALUES (@MaDonHang, @MaSanPham, @SoLuong, @DonGia, @ThanhTien)";
                        using (SqlCommand cmdInsert = new SqlCommand(queryChiTiet, conn))
                        {
                            cmdInsert.Parameters.AddWithValue("@MaDonHang", maDonHang);
                            cmdInsert.Parameters.AddWithValue("@MaSanPham", maSanPham);
                            cmdInsert.Parameters.AddWithValue("@SoLuong", soLuong);
                            cmdInsert.Parameters.AddWithValue("@DonGia", donGia);
                            cmdInsert.Parameters.AddWithValue("@ThanhTien", thanhTien);

                            cmdInsert.ExecuteNonQuery(); // Thêm chi tiết đơn hàng mới
                        }

                        // Tính tổng tiền đơn hàng
                        decimal tongTien = GetTongTienDonHang(maDonHang, conn) + thanhTien;

                        // Cập nhật hoặc thêm mới đơn hàng nếu cần
                        bool donHangExists = DonHangExists(maDonHang, conn);
                        if (donHangExists)
                        {
                            // Cập nhật tổng tiền của đơn hàng đã tồn tại
                            string updateQuery = "UPDATE DonHang SET TongTien = @TongTien WHERE MaDonHang = @MaDonHang";
                            using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@TongTien", tongTien);
                                cmd.Parameters.AddWithValue("@MaDonHang", maDonHang);
                                cmd.ExecuteNonQuery(); // Cập nhật đơn hàng
                            }
                        }
                        else
                        {
                            // Thêm mới vào bảng DonHang nếu chưa tồn tại
                            string insertQuery = "INSERT INTO DonHang (MaDonHang, MaPhong, NgayTao, TongTien, TrangThai) " +
                                                 "VALUES (@MaDonHang, @MaPhong, @NgayTao, @TongTien, @TrangThai)";
                            using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@MaDonHang", maDonHang);
                                cmd.Parameters.AddWithValue("@MaPhong", maPhong);
                                cmd.Parameters.AddWithValue("@NgayTao", DateTime.Now);
                                cmd.Parameters.AddWithValue("@TongTien", tongTien);
                                cmd.Parameters.AddWithValue("@TrangThai", false);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        MessageBox.Show("Sản phẩm đã được thêm vào đơn hàng thành công.", "Thông báo");
                    }
                }
            }

            this.Close(); 
        }

        // Hàm kiểm tra xem DonHang đã tồn tại chưa
        private bool DonHangExists(string maDonHang, SqlConnection conn)
        {
            string query = "SELECT COUNT(*) FROM DonHang WHERE MaDonHang = @MaDonHang";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@MaDonHang", maDonHang);
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }
        

        private void numSoLuong_ValueChanged(object sender, EventArgs e)
        {
            decimal soLuong = numSoLuong.Value;
            decimal thanhTien = soLuong * donGia;
            txtThanhTien.Text = thanhTien.ToString("N0"); 
        }
        private decimal GetTongTienDonHang(string maDonHang, SqlConnection conn)
        {
            string query = "SELECT SUM(ThanhTien) FROM ChiTietDonHang WHERE MaDonHang = @MaDonHang";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@MaDonHang", maDonHang);
                object result = cmd.ExecuteScalar();
                return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
