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
    public partial class frmSuaHoatDong : Form
    {
        string connection = ConfigurationManager.ConnectionStrings["ServerHTQLKaraoke.Properties.Settings.KaraokeConnectionString"].ConnectionString;
        private string maHoatDong;
        public frmSuaHoatDong(string maHoatDong, string tenChiNhanh)
        {
            InitializeComponent();
            this.maHoatDong = maHoatDong;
            LoadChiNhanh();
            cbxChiNhanh.SelectedItem = tenChiNhanh;
        }

        private void frmSuaHoatDong_Load(object sender, EventArgs e)
        {
            // Mặc định giá trị cho các ô nhập liệu
            txtTenNhanVien.Text = "Ông chủ";
            dtpNgayThucHien.Value = DateTime.Now;
            txtMaHoatDong.Text = maHoatDong;

            LoadData();
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
        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();

                    // Lấy thông tin hoạt động cần sửa
                    string query = @"
                    SELECT 
                        hd.MoTaHoatDong
                    FROM 
                        HoatDongHeThong hd
                    WHERE 
                        hd.MaHoatDong = @MaHoatDong";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaHoatDong", maHoatDong);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtMoTa.Text = reader["MoTaHoatDong"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Kiểm tra các trường bắt buộc
            if (cbxChiNhanh.SelectedIndex <= 0)
            {
                MessageBox.Show("Vui lòng chọn chi nhánh để thêm hoạt động.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maChiNhanh = GetMaChiNhanh(cbxChiNhanh.SelectedItem.ToString());
            if (string.IsNullOrEmpty(maChiNhanh))
            {
                MessageBox.Show("Không tìm thấy mã chi nhánh tương ứng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();

                    // Update thông tin hoạt động
                    string query = @"
                    UPDATE HoatDongHeThong
                    SET 
                        MoTaHoatDong = @MoTaHoatDong,
                        NgayThucHien = @NgayThucHien,
                        MaChiNhanh = @MaChiNhanh
                    WHERE 
                        MaHoatDong = @MaHoatDong";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaHoatDong", maHoatDong);
                        cmd.Parameters.AddWithValue("@MoTaHoatDong", txtMoTa.Text);
                        cmd.Parameters.AddWithValue("@NgayThucHien", dtpNgayThucHien.Value);
                        cmd.Parameters.AddWithValue("@MaChiNhanh", maChiNhanh);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Sửa hoạt động thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close(); // Đóng form sau khi sửa
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa hoạt động: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
