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

namespace HTQLKaraoke.NhatKyHD
{
    public partial class frmThemHoatDong : Form
    {
        string connection = ConfigurationManager.ConnectionStrings["HTQLKaraoke.Properties.Settings.KaraokeConnectionString"].ConnectionString;
        public frmThemHoatDong()
        {
            InitializeComponent();
        }

        private void frmThemHoatDong_Load(object sender, EventArgs e)
        {
            txtMaHoatDong.Text = GenerateRandomCode(10);

            dtpNgayThucHien.Value = DateTime.Now;

            LoadChiNhanh();
        }

        private string GenerateRandomCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void LoadChiNhanh()
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                try
                {
                    conn.Open();

                    string query = "SELECT TOP 1 MaChiNhanh, TenChiNhanh FROM ChiNhanh";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            lblTenChiNhanh.Text = "Chi Nhánh " + reader["TenChiNhanh"].ToString();
                            lblTenChiNhanh.Tag = reader["MaChiNhanh"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải chi nhánh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Kiểm tra đầu vào
            if (string.IsNullOrWhiteSpace(txtTenNhanVien.Text) || string.IsNullOrWhiteSpace(txtMoTa.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!IsValidName(txtTenNhanVien.Text))
            {
                MessageBox.Show("Tên nhân viên không được chứa số.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lưu vào cơ sở dữ liệu
            using (SqlConnection conn = new SqlConnection(connection))
            {
                try
                {
                    conn.Open();

                    string query = @"
                        INSERT INTO HoatDongHeThong (MaHoatDong, TenNhanVien, MoTaHoatDong, NgayThucHien, MaChiNhanh)
                        VALUES (@MaHoatDong, @TenNhanVien, @MoTaHoatDong, @NgayThucHien, @MaChiNhanh)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaHoatDong", txtMaHoatDong.Text);
                        cmd.Parameters.AddWithValue("@TenNhanVien", txtTenNhanVien.Text);
                        cmd.Parameters.AddWithValue("@MoTaHoatDong", txtMoTa.Text);
                        cmd.Parameters.AddWithValue("@NgayThucHien", dtpNgayThucHien.Value);
                        cmd.Parameters.AddWithValue("@MaChiNhanh", lblTenChiNhanh.Tag.ToString());

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Thêm hoạt động thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close(); // Đóng form sau khi thêm thành công
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thêm hoạt động: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool IsValidName(string name)
        {
            // Kiểm tra tên không chứa số
            foreach (char c in name)
            {
                if (char.IsDigit(c))
                    return false;
            }
            return true;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
