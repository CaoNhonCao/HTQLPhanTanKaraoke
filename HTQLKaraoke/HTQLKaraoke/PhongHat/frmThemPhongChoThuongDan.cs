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
    public partial class frmThemPhongChoThuongDan : Form
    {
        string connection = ConfigurationManager.ConnectionStrings["HTQLKaraoke.Properties.Settings.KaraokeConnectionString"].ConnectionString;
        String LoaiPhong;
        public frmThemPhongChoThuongDan(string loaiPhong)
        {
            LoaiPhong = loaiPhong;
            InitializeComponent(); 
            LoadTrangThaiComboBox();
        }
        private void LoadTrangThaiComboBox()
        {
            cbxTrangThai.Items.AddRange(new string[] { "Chưa đặt phòng", "Đang bảo trì" });
            cbxTrangThai.SelectedIndex = 0; 
            cbxTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private bool ValidateInput()
        {
            // Khai báo biến soPhong
            int soPhong;

            // Kiểm tra Số Phòng
            if (!int.TryParse(txtSoPhong.Text, out soPhong) || soPhong < 101 || soPhong > 199)
            {
                MessageBox.Show("Số phòng phải là số từ 101 đến 199.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Kiểm tra Sức Chứa
            int sucChua;
            if (!int.TryParse(txtSucChua.Text, out sucChua) || sucChua <= 1 || sucChua >= 20)
            {
                MessageBox.Show("Sức chứa phải lớn hơn 1 và nhỏ hơn 20.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Kiểm tra trùng Số Phòng với cùng loại phòng
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM PhongHat WHERE SoPhong = @SoPhong AND LoaiPhong = @LoaiPhong";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SoPhong", soPhong);
                    cmd.Parameters.AddWithValue("@LoaiPhong", LoaiPhong);
                    int count = (int)cmd.ExecuteScalar();
                    if (count > 0)
                    {
                        MessageBox.Show("Số phòng này đã tồn tại cho loại " + LoaiPhong, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
            }

            return true;
        }

        private void frmThemPhongChoThuongDan_Load(object sender, EventArgs e)
        {
            label1.Text = "Thêm " + LoaiPhong.ToString();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                // Lấy dữ liệu từ form
                int soPhong = int.Parse(txtSoPhong.Text);
                int sucChua = int.Parse(txtSucChua.Text);
                decimal giaTheoGio = decimal.Parse(txtGia.Text);
                string trangThai = cbxTrangThai.SelectedItem.ToString();
                string loaiPhong = LoaiPhong; // Mặc định loại phòng là Phòng Thường
                string maPhong = GenerateNewMaPhong(); // Gọi hàm tạo mã phòng tự động
                string maChiNhanh = GetMaChiNhanh(); // Gọi hàm để lấy mã chi nhánh

                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();

                    // Thêm phòng vào bảng PhongHat
                    string queryPhongHat = "INSERT INTO PhongHat (MaPhong, SoPhong, LoaiPhong, SucChua, GiaTheoGio, TrangThai, MaChiNhanh) " +
                                           "VALUES (@MaPhong, @SoPhong, @LoaiPhong, @SucChua, @GiaTheoGio, @TrangThai, @MaChiNhanh)";
                    using (SqlCommand cmdPhongHat = new SqlCommand(queryPhongHat, conn))
                    {
                        cmdPhongHat.Parameters.AddWithValue("@MaPhong", maPhong);
                        cmdPhongHat.Parameters.AddWithValue("@SoPhong", soPhong);
                        cmdPhongHat.Parameters.AddWithValue("@LoaiPhong", loaiPhong);
                        cmdPhongHat.Parameters.AddWithValue("@SucChua", sucChua);
                        cmdPhongHat.Parameters.AddWithValue("@GiaTheoGio", giaTheoGio);
                        cmdPhongHat.Parameters.AddWithValue("@TrangThai", trangThai);
                        cmdPhongHat.Parameters.AddWithValue("@MaChiNhanh", maChiNhanh);

                        cmdPhongHat.ExecuteNonQuery();
                    }

                    // Nếu trạng thái là "Đang bảo trì," thêm vào bảng LichSuBaoTri
                    if (trangThai == "Đang bảo trì")
                    {
                        string maBaoTri = GenerateRandomCode(10); // Tạo mã bảo trì ngẫu nhiên
                        DateTime ngayBaoTri = DateTime.Now; // Ngày bảo trì là ngày hiện tại

                        string queryLichSuBaoTri = "INSERT INTO LichSuBaoTri (MaBaoTri, MaPhong, NgayBaoTri, GhiChu, MaChiNhanh) " +
                                                   "VALUES (@MaBaoTri, @MaPhong, @NgayBaoTri, N'Đang bảo trì', @MaChiNhanh)";
                        using (SqlCommand cmdLichSuBaoTri = new SqlCommand(queryLichSuBaoTri, conn))
                        {
                            cmdLichSuBaoTri.Parameters.AddWithValue("@MaBaoTri", maBaoTri);
                            cmdLichSuBaoTri.Parameters.AddWithValue("@MaPhong", maPhong);
                            cmdLichSuBaoTri.Parameters.AddWithValue("@NgayBaoTri", ngayBaoTri);
                            cmdLichSuBaoTri.Parameters.AddWithValue("@MaChiNhanh", maChiNhanh);

                            cmdLichSuBaoTri.ExecuteNonQuery();
                        }
                    }
                }

                MessageBox.Show("Thêm phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // Đóng form sau khi lưu
            }
        }

        // Hàm tạo mã phòng tự động
        private string GenerateNewMaPhong()
        {
            string newMaPhong;
            bool isDuplicate;

            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();

                do
                {
                    // Tạo mã phòng ngẫu nhiên gồm 10 ký tự (chữ và số)
                    newMaPhong = GenerateRandomCode(10);

                    // Kiểm tra xem mã này đã tồn tại trong cơ sở dữ liệu chưa
                    string query = "SELECT COUNT(*) FROM PhongHat WHERE MaPhong = @MaPhong";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaPhong", newMaPhong);
                        int count = (int)cmd.ExecuteScalar();

                        // Nếu mã đã tồn tại, đặt isDuplicate = true để tiếp tục vòng lặp
                        isDuplicate = count > 0;
                    }

                } while (isDuplicate); // Tiếp tục tạo mã mới nếu mã đã tồn tại
            }

            return newMaPhong; // Trả về mã phòng mới
        }

        // Hàm tạo chuỗi ngẫu nhiên với độ dài bất kỳ
        private string GenerateRandomCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        // Hàm lấy mã chi nhánh
        private string GetMaChiNhanh()
        {
            string maChiNhanh = string.Empty;
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                // Truy vấn để lấy mã chi nhánh, bạn có thể thay đổi điều kiện cho phù hợp
                string query = "SELECT TOP 1 MaChiNhanh FROM ChiNhanh"; // Lấy mã chi nhánh đầu tiên
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        maChiNhanh = result.ToString(); // Lưu mã chi nhánh
                    }
                }
            }
            return maChiNhanh; // Trả về mã chi nhánh
        }


        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbxTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
