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

namespace HTQLKaraoke.QLDV
{
    public partial class frmThemDichVu : Form
    {
        string connection = ConfigurationManager.ConnectionStrings["HTQLKaraoke.Properties.Settings.KaraokeConnectionString"].ConnectionString;
        public frmThemDichVu()
        {
            InitializeComponent();
        }

        private void frmThemDichVu_Load(object sender, EventArgs e)
        {
            // Tạo mã dịch vụ ngẫu nhiên gồm 10 ký tự
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();

                string newMaDV;
                bool isDuplicate;

                do
                {
                    // Gọi hàm tạo mã ngẫu nhiên
                    newMaDV = GenerateRandomCode(10);

                    // Kiểm tra xem mã dịch vụ đã tồn tại trong cơ sở dữ liệu chưa
                    string query = "SELECT COUNT(*) FROM DichVu WHERE MaDichVu = @MaDichVu";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaDichVu", newMaDV);
                        int count = (int)cmd.ExecuteScalar();

                        // Nếu mã đã tồn tại, đặt isDuplicate = true để tiếp tục vòng lặp
                        isDuplicate = count > 0;
                    }

                } while (isDuplicate); // Tiếp tục tạo mã mới nếu mã đã tồn tại

                txtMaDichVu.Text = newMaDV; // Hiển thị mã dịch vụ mới lên textbox
            }
        }

        // Hàm tạo chuỗi ngẫu nhiên với độ dài bất kỳ
        private string GenerateRandomCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrWhiteSpace(txtTenDichVu.Text) || txtTenDichVu.Text.Length > 20)
            {
                MessageBox.Show("Tên dịch vụ không được rỗng và không quá 20 ký tự.");
                return;
            }

            decimal giaDichVu;
            if (!decimal.TryParse(txtGiaDichVu.Text, out giaDichVu) || giaDichVu <= 1000)
            {
                MessageBox.Show("Giá dịch vụ phải là số lớn hơn 1000.");
                return;
            }

            // Lấy mã chi nhánh duy nhất từ bảng ChiNhanh
            string maChiNhanh = GetMaChiNhanh();
            if (string.IsNullOrEmpty(maChiNhanh)) // Kiểm tra nếu mã chi nhánh không tìm thấy
            {
                return;
            }

            // Thêm dịch vụ vào cơ sở dữ liệu
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();

                // Thêm vào bảng DichVu
                string insertDichVu = @"INSERT INTO DichVu (MaDichVu, TenDichVu, GiaDichVu, MaChiNhanh, GhiChu, NgayTao)
                                        VALUES (@MaDichVu, @TenDichVu, @GiaDichVu, @MaChiNhanh, @GhiChu, @NgayTao)";
                using (SqlCommand cmd = new SqlCommand(insertDichVu, conn))
                {
                    cmd.Parameters.AddWithValue("@MaDichVu", txtMaDichVu.Text);
                    cmd.Parameters.AddWithValue("@TenDichVu", txtTenDichVu.Text);
                    cmd.Parameters.AddWithValue("@GiaDichVu", giaDichVu);
                    cmd.Parameters.AddWithValue("@MaChiNhanh", maChiNhanh);
                    cmd.Parameters.AddWithValue("@GhiChu", txtGhiChu.Text);
                    cmd.Parameters.AddWithValue("@NgayTao", DateTime.Now);

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Thêm dịch vụ thành công!");
            this.Close(); // Đóng form sau khi thêm xong
        }

        private string GetMaChiNhanh()
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string query = "SELECT TOP 1 MaChiNhanh FROM ChiNhanh";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        return result.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy mã chi nhánh.");
                        return string.Empty;
                    }
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
