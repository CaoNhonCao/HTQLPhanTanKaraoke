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
    public partial class frmSuaHoatDong : Form
    {
        string connection = ConfigurationManager.ConnectionStrings["HTQLKaraoke.Properties.Settings.KaraokeConnectionString"].ConnectionString;
        private string maHoatDong;
        public frmSuaHoatDong(string maHoatDong)
        {
            InitializeComponent();
            this.maHoatDong = maHoatDong;
        }

        private void frmSuaHoatDong_Load(object sender, EventArgs e)
        {
            LoadData();
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
                        hd.MaHoatDong, 
                        hd.TenNhanVien, 
                        hd.MoTaHoatDong, 
                        hd.NgayThucHien, 
                        hd.MaChiNhanh, 
                        cn.TenChiNhanh
                    FROM 
                        HoatDongHeThong hd
                    INNER JOIN 
                        ChiNhanh cn ON hd.MaChiNhanh = cn.MaChiNhanh
                    WHERE 
                        hd.MaHoatDong = @MaHoatDong";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaHoatDong", maHoatDong);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtMaHoatDong.Text = reader["MaHoatDong"].ToString();
                                txtTenNhanVien.Text = reader["TenNhanVien"].ToString();
                                txtMoTa.Text = reader["MoTaHoatDong"].ToString();
                                dtpNgayThucHien.Value = Convert.ToDateTime(reader["NgayThucHien"]);
                                lblTenChiNhanh.Text = "Chi Nhánh " + reader["TenChiNhanh"].ToString();
                                lblTenChiNhanh.Tag = reader["MaChiNhanh"].ToString();
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

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu nhập vào
            if (string.IsNullOrWhiteSpace(txtTenNhanVien.Text) || string.IsNullOrWhiteSpace(txtMoTa.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtTenNhanVien.Text.Any(char.IsDigit))
            {
                MessageBox.Show("Tên nhân viên không được chứa số!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        TenNhanVien = @TenNhanVien,
                        MoTaHoatDong = @MoTaHoatDong,
                        NgayThucHien = @NgayThucHien,
                        MaChiNhanh = @MaChiNhanh
                    WHERE 
                        MaHoatDong = @MaHoatDong";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaHoatDong", maHoatDong);
                        cmd.Parameters.AddWithValue("@TenNhanVien", txtTenNhanVien.Text);
                        cmd.Parameters.AddWithValue("@MoTaHoatDong", txtMoTa.Text);
                        cmd.Parameters.AddWithValue("@NgayThucHien", dtpNgayThucHien.Value);
                        cmd.Parameters.AddWithValue("@MaChiNhanh", lblTenChiNhanh.Tag.ToString());

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

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
