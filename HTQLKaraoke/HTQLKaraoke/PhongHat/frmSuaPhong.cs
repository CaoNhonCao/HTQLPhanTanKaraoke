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

namespace HTQLKaraoke.PhongHat
{
    public partial class frmSuaPhong : Form
    {
        string connectionString = ConfigurationManager.ConnectionStrings["HTQLKaraoke.Properties.Settings.KaraokeConnectionString"].ConnectionString;
        private string maPhong;
        private string trangThaiHienTai;

        public frmSuaPhong(string maPhong, int soPhong, string loaiPhong, string trangThai, int sucChua, decimal giaTheoGio)
        {
            InitializeComponent();
            SetupComboBox();

            
            this.maPhong = maPhong;
            txtMaPhong.Text = maPhong; 
            txtMaPhong.ReadOnly = true; 
            txtSoPhong.Text = soPhong.ToString(); 
            txtSoPhong.ReadOnly = true;
            txtLoaiPhong.Text = loaiPhong;
            cbxTrangThai.SelectedItem = trangThai; 
            trangThaiHienTai = trangThai; 
            txtSucChua.Text = sucChua.ToString(); 
            txtGia.Text = giaTheoGio.ToString();
        }

        private void SetupComboBox()
        {
            cbxTrangThai.Items.AddRange(new string[] { "Chưa đặt phòng", "Đang bảo trì" });
            cbxTrangThai.SelectedIndex = 0;
            cbxTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string newTrangThai = cbxTrangThai.SelectedItem.ToString();
            int newSucChua;
            if (!int.TryParse(txtSucChua.Text, out newSucChua) || newSucChua <= 1 || newSucChua >= 20)
            {
                MessageBox.Show("Sức chứa phải lớn hơn 1 và nhỏ hơn 20.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal newGiaTheoGio;
            if (!decimal.TryParse(txtGia.Text, out newGiaTheoGio) || newGiaTheoGio < 0)
            {
                MessageBox.Show("Giá thuê không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string currentGhiChu = null;

                // Lấy ghi chú hiện tại từ LichSuBaoTri
                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 GhiChu FROM LichSuBaoTri WHERE MaPhong = @MaPhong ORDER BY NgayBaoTri DESC", conn))
                {
                    cmd.Parameters.AddWithValue("@MaPhong", maPhong);
                    object result = cmd.ExecuteScalar();
                    currentGhiChu = result != null ? result.ToString() : null;
                }

                // Kiểm tra điều kiện thay đổi trạng thái
                if (trangThaiHienTai == "Đang bảo trì" && newTrangThai == "Chưa đặt phòng" && currentGhiChu == "Đang bảo trì")
                {
                    UpdateLichSuBaoTri("Đã xong", conn);
                }
                else if (trangThaiHienTai == "Chưa đặt phòng" && newTrangThai == "Đang bảo trì")
                {
                    if (currentGhiChu == null || currentGhiChu == "Đã xong")
                    {
                        UpdateLichSuBaoTri("Đang bảo trì", conn);
                    }
                }

                UpdateRoom(maPhong, newTrangThai, newSucChua, newGiaTheoGio, conn);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void UpdateLichSuBaoTri(string ghiChu, SqlConnection conn)
        {
            // Kiểm tra xem phòng đã có lịch sử bảo trì hay chưa
            string queryCheck = "SELECT TOP 1 GhiChu FROM LichSuBaoTri WHERE MaPhong = @MaPhong ORDER BY NgayBaoTri DESC";
            string currentGhiChu = null;
            using (SqlCommand cmdCheck = new SqlCommand(queryCheck, conn))
            {
                cmdCheck.Parameters.AddWithValue("@MaPhong", maPhong);
                object result = cmdCheck.ExecuteScalar();
                currentGhiChu = result != null ? result.ToString() : null;
            }

            if (ghiChu == "Đang bảo trì")
            {
                // Nếu trạng thái chuyển sang "Đang bảo trì"
                if (currentGhiChu == null || currentGhiChu == "Đã xong")
                {
                    string maBaoTri = GenerateRandomCode(10);

                    string maChiNhanh;
                    using (SqlCommand cmdBranch = new SqlCommand("SELECT TOP 1 MaChiNhanh FROM ChiNhanh", conn))
                    {
                        object result = cmdBranch.ExecuteScalar();
                        maChiNhanh = result != null ? result.ToString() : string.Empty;
                    }

                    string queryInsert = "INSERT INTO LichSuBaoTri (MaBaoTri, MaPhong, GhiChu, MaChiNhanh, NgayBaoTri) VALUES (@MaBaoTri, @MaPhong, @GhiChu, @MaChiNhanh, @NgayBaoTri)";
                    using (SqlCommand cmdInsert = new SqlCommand(queryInsert, conn))
                    {
                        cmdInsert.Parameters.AddWithValue("@MaBaoTri", maBaoTri);
                        cmdInsert.Parameters.AddWithValue("@MaPhong", maPhong);
                        cmdInsert.Parameters.AddWithValue("@GhiChu", ghiChu);
                        cmdInsert.Parameters.AddWithValue("@MaChiNhanh", maChiNhanh);
                        cmdInsert.Parameters.AddWithValue("@NgayBaoTri", DateTime.Now);
                        cmdInsert.ExecuteNonQuery();
                    }
                }
            }
            else if (ghiChu == "Đã xong" && currentGhiChu == "Đang bảo trì")
            {
                // Nếu chuyển trạng thái từ "Đang bảo trì" sang "Chưa đặt phòng"
                string queryUpdate = "UPDATE LichSuBaoTri SET GhiChu = @GhiChu WHERE MaPhong = @MaPhong";
                using (SqlCommand cmdUpdate = new SqlCommand(queryUpdate, conn))
                {
                    cmdUpdate.Parameters.AddWithValue("@MaPhong", maPhong);
                    cmdUpdate.Parameters.AddWithValue("@GhiChu", ghiChu);
                    int rowsAffected = cmdUpdate.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        MessageBox.Show("Không tìm thấy ghi chú nào để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void UpdateRoom(string maPhong, string trangThai, int sucChua, decimal giaThue, SqlConnection conn)
        {
            string query = "UPDATE PhongHat SET TrangThai = @TrangThai, SucChua = @SucChua, GiaTheoGio = @GiaTheoGio, NgayCapNhat = @NgayCapNhat WHERE MaPhong = @MaPhong";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@MaPhong", maPhong);
                cmd.Parameters.AddWithValue("@TrangThai", trangThai);
                cmd.Parameters.AddWithValue("@SucChua", sucChua);
                cmd.Parameters.AddWithValue("@GiaTheoGio", giaThue);
                cmd.Parameters.AddWithValue("@NgayCapNhat", DateTime.Now);
                cmd.ExecuteNonQuery();
            }
        }

        private string GenerateRandomCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var stringBuilder = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                stringBuilder.Append(chars[random.Next(chars.Length)]);
            }
            return stringBuilder.ToString();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
