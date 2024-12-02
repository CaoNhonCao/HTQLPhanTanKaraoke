using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServerHTQLKaraoke.NhatKyHD
{
    public partial class frmThemHoatDong : Form
    {
        string connection = ConfigurationManager.ConnectionStrings["ServerHTQLKaraoke.Properties.Settings.KaraokeConnectionString"].ConnectionString;
        public frmThemHoatDong()
        {
            InitializeComponent();
        }
        private void btnThemHoatDong_Click(object sender, EventArgs e)
        {
            // Kiểm tra các trường bắt buộc
            if (cbxChiNhanh.SelectedIndex <= 0)
            {
                MessageBox.Show("Vui lòng chọn chi nhánh để thêm hoạt động.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tạo mã hoạt động ngẫu nhiên
            string maHoatDong = GenerateRandomString(10);
            txtMaHoatDong.Text = maHoatDong;

            // Lấy mã chi nhánh dựa vào tên chi nhánh đã chọn
            string maChiNhanh = GetMaChiNhanh(cbxChiNhanh.SelectedItem.ToString());
            if (string.IsNullOrEmpty(maChiNhanh))
            {
                MessageBox.Show("Không tìm thấy mã chi nhánh tương ứng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Lưu thông tin vào cơ sở dữ liệu
            using (SqlConnection conn = new SqlConnection(connection))
            {
                try
                {
                    conn.Open();
                    string query = @"INSERT INTO HoatDongHeThong (MaHoatDong, TenNhanVien, MoTaHoatDong, NgayThucHien, MaChiNhanh)
                                     VALUES (@MaHoatDong, @TenNhanVien, @MoTaHoatDong, @NgayThucHien, @MaChiNhanh)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaHoatDong", maHoatDong);
                    cmd.Parameters.AddWithValue("@TenNhanVien", txtTenNhanVien.Text);
                    cmd.Parameters.AddWithValue("@MoTaHoatDong", txtMoTa.Text);
                    cmd.Parameters.AddWithValue("@NgayThucHien", dtpNgayThucHien.Value);
                    cmd.Parameters.AddWithValue("@MaChiNhanh", maChiNhanh);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Thêm hoạt động thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thêm hoạt động: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private string GetMaChiNhanh(string tenChiNhanh)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT MaChiNhanh FROM ChiNhanh WHERE TenChiNhanh = @TenChiNhanh";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@TenChiNhanh", tenChiNhanh);
                    object result = cmd.ExecuteScalar();
                    return result?.ToString();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        private string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            char[] buffer = new char[length];

            for (int i = 0; i < length; i++)
            {
                buffer[i] = chars[random.Next(chars.Length)];
            }

            return new string(buffer);
        }

        private void frmNhatKyHD_Load(object sender, EventArgs e)
        {
            // Mặc định giá trị cho các ô nhập liệu
            txtTenNhanVien.Text = "Ông chủ";
            dtpNgayThucHien.Value = DateTime.Now;
            txtMaHoatDong.Text = GenerateRandomString(10);

            // Tải danh sách chi nhánh vào ComboBox
            LoadChiNhanh();
        }

        private void LoadChiNhanh()
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT TenChiNhanh FROM ChiNhanh";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    cbxChiNhanh.Items.Clear();
                    cbxChiNhanh.Items.Add("Chọn chi nhánh");

                    while (reader.Read())
                    {
                        cbxChiNhanh.Items.Add(reader["TenChiNhanh"].ToString());
                    }

                    cbxChiNhanh.SelectedIndex = 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải danh sách chi nhánh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
