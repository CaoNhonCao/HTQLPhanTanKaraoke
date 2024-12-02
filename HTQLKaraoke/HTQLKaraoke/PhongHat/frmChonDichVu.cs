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
    public partial class frmChonDichVu : Form
    {

        string connection = ConfigurationManager.ConnectionStrings["HTQLKaraoke.Properties.Settings.KaraokeConnectionString"].ConnectionString;
        private string maDichVu, maPhong, tenDichVu;
        private decimal giaDichVu;
        public frmChonDichVu(string maDichVu, string maPhong, decimal giaDichVu, string tenDichVu)
        {
            InitializeComponent();
            this.maDichVu = maDichVu;
            this.maPhong = maPhong;
            this.tenDichVu = tenDichVu;
            this.giaDichVu = giaDichVu;

            txtMaDichVu.Text = maDichVu;
            txtMaPhong.Text = maPhong;
            txtTenDichVu.Text = tenDichVu;
            txtGiaDichVu.Text = giaDichVu.ToString("N0");

            numSoLuong.ValueChanged += new EventHandler(this.numSoLuong_ValueChanged);
        }

        private void numSoLuong_ValueChanged(object sender, EventArgs e)
        {
            decimal soLuong = numSoLuong.Value;
            decimal thanhTien = soLuong * giaDichVu;
            txtThanhTien.Text = thanhTien.ToString("N0");
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            decimal soLuong = numSoLuong.Value;
            decimal thanhTien = soLuong * giaDichVu;

            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();

                string queryCheck = "SELECT SoLuong FROM DichVuChoPhong WHERE MaDichVu = @MaDichVu AND MaPhong = @MaPhong and TrangThai = 0";
                using (SqlCommand cmdCheck = new SqlCommand(queryCheck, conn))
                {
                    cmdCheck.Parameters.AddWithValue("@MaDichVu", maDichVu);
                    cmdCheck.Parameters.AddWithValue("@MaPhong", maPhong);
                    object result = cmdCheck.ExecuteScalar();

                    if (result != null)
                    {
                        decimal soLuongHienTai = Convert.ToDecimal(result);
                        decimal soLuongMoi = soLuongHienTai + soLuong;
                        decimal thanhTienMoi = soLuongMoi * giaDichVu;

                        string queryUpdate = "UPDATE DichVuChoPhong SET SoLuong = @SoLuong, ThanhTien = @ThanhTien, NgaySuDung = GETDATE() WHERE MaDichVu = @MaDichVu AND MaPhong = @MaPhong";
                        using (SqlCommand cmdUpdate = new SqlCommand(queryUpdate, conn))
                        {
                            cmdUpdate.Parameters.AddWithValue("@SoLuong", soLuongMoi);
                            cmdUpdate.Parameters.AddWithValue("@ThanhTien", thanhTienMoi);
                            cmdUpdate.Parameters.AddWithValue("@MaDichVu", maDichVu);
                            cmdUpdate.Parameters.AddWithValue("@MaPhong", maPhong);

                            cmdUpdate.ExecuteNonQuery();
                        }

                        MessageBox.Show("Số lượng đã được cập nhật.", "Thông báo");
                    }
                    else
                    {
                        // Tạo ID ngẫu nhiên
                        string randomID = GenerateRandomID(10);

                        string queryChiTiet = "INSERT INTO DichVuChoPhong (ID, MaDichVu, MaPhong, SoLuong, NgaySuDung, TrangThai, ThanhTien) " +
                                              "VALUES (@ID, @MaDichVu, @MaPhong, @SoLuong, GETDATE(), 0, @ThanhTien)";
                        using (SqlCommand cmdInsert = new SqlCommand(queryChiTiet, conn))
                        {
                            cmdInsert.Parameters.AddWithValue("@ID", randomID);
                            cmdInsert.Parameters.AddWithValue("@MaDichVu", maDichVu);
                            cmdInsert.Parameters.AddWithValue("@MaPhong", maPhong);
                            cmdInsert.Parameters.AddWithValue("@SoLuong", soLuong);
                            cmdInsert.Parameters.AddWithValue("@ThanhTien", thanhTien);

                            cmdInsert.ExecuteNonQuery();
                        }

                        MessageBox.Show("Dịch vụ " + tenDichVu + " đã được thêm với số lượng là " + soLuong, "Thông báo");
                    }
                }
            }

            this.Close();
        }

        private string GenerateRandomID(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            StringBuilder result = new StringBuilder(length);
            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                result.Append(chars[random.Next(chars.Length)]);
            }
            return result.ToString();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
