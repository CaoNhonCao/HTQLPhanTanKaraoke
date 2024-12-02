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
using System.Configuration;
using HTQLKaraoke.DMKhachHang;

namespace HTQLKaraoke.PhongHat
{
    public partial class frmDatPhong : Form
    {
        string connection = ConfigurationManager.ConnectionStrings["HTQLKaraoke.Properties.Settings.KaraokeConnectionString"].ConnectionString;
        string maPhong;
        string LoaiPhong;
        private TimeSpan thoiGianDaSuDung;
        public frmDatPhong(int soPhong, string maPhong, string loaiPhong)
        {
            InitializeComponent();
            InitializeDateTimePickers();
            txtSoPhong.Text = soPhong.ToString();
            txtMaPhong.Text = maPhong.ToString();
            LoaiPhong = loaiPhong;
            this.maPhong = maPhong;
        }

        private void InitializeDateTimePickers()
        {
            // Thời gian bắt đầu
            dtpThoiGianBatDau.Format = DateTimePickerFormat.Custom;
            dtpThoiGianBatDau.CustomFormat = "dd/MM/yyyy HH:mm:ss";

            // Thời gian kết thúc
            dtpThoiGianKetThuc.Format = DateTimePickerFormat.Custom;
            dtpThoiGianKetThuc.CustomFormat = "dd/MM/yyyy HH:mm:ss";

            // Đặt giá trị mặc định là hiện tại
            dtpThoiGianBatDau.Value = DateTime.Now;
            dtpThoiGianKetThuc.Value = DateTime.Now.AddHours(1); // Thời gian kết thúc sau 1 giờ
        }

        private void frmDatPhong_Load(object sender, EventArgs e)
        {
            // Kiểm tra thời điểm hiện tại so với thời gian bắt đầu
            DateTime currentTime = DateTime.Now;

            if (dtpThoiGianBatDau.Value <= currentTime)
            {
                // Nếu thời gian bắt đầu là hiện tại hoặc trong quá khứ
                dtpThoiGianKetThuc.Value = currentTime; // Thời gian kết thúc là ngay lúc này
                dtpThoiGianKetThuc.Enabled = false; // Vô hiệu hóa DateTimePicker
            }
            else
            {
                // Nếu thời gian bắt đầu là trong tương lai
                dtpThoiGianKetThuc.Value = dtpThoiGianBatDau.Value.AddHours(1); // Đặt thời gian kết thúc là 1 giờ sau thời gian bắt đầu
                dtpThoiGianKetThuc.Enabled = true; // Kích hoạt DateTimePicker
            }
        }

        private void dtpThoiGianBatDau_ValueChanged(object sender, EventArgs e)
        {
            DateTime currentTime = DateTime.Now;
            if (dtpThoiGianBatDau.Value <= currentTime)
            {
                dtpThoiGianKetThuc.Value = currentTime;
                dtpThoiGianKetThuc.Enabled = false;
            }
            else
            {
                dtpThoiGianKetThuc.Enabled = true;
                dtpThoiGianKetThuc.Value = dtpThoiGianBatDau.Value.AddHours(1);
            }
        }


        private void ShowSuggestions(string searchText)
        {
            listBoxSuggestions.Items.Clear(); // Xóa các gợi ý cũ

            // Xác định tìm kiếm theo số hay chữ
            bool isNumeric = searchText.All(char.IsDigit);

            string query;

            if (string.IsNullOrWhiteSpace(searchText))
            {
                // Nếu không nhập gì, hiển thị toàn bộ danh sách
                query = "SELECT SoDienThoai, HoTen, Email FROM KhachHang";
            }
            else if (isNumeric)
            {
                // Nếu là số, tìm kiếm theo số điện thoại
                query = "SELECT SoDienThoai FROM KhachHang WHERE SoDienThoai LIKE @SearchText";
            }
            else
            {
                // Nếu là chữ, tìm kiếm theo họ tên hoặc email
                query = "SELECT HoTen FROM KhachHang WHERE HoTen LIKE @SearchText UNION SELECT Email FROM KhachHang WHERE Email LIKE @SearchText";
            }

            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();

                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    if (!string.IsNullOrWhiteSpace(searchText))
                    {
                        command.Parameters.AddWithValue("@SearchText", "%" + searchText + "%");
                    }

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Chỉ hiển thị giá trị gợi ý theo kiểu tìm kiếm
                            string suggestion = reader[0].ToString();
                            listBoxSuggestions.Items.Add(suggestion);
                        }
                    }
                }
            }

            // Hiển thị gợi ý nếu có ít nhất một kết quả
            listBoxSuggestions.Visible = listBoxSuggestions.Items.Count > 0;
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string searchPhone = txtTimKiem.Text.Trim(); // Lấy số điện thoại từ TextBox

            if (string.IsNullOrEmpty(searchPhone) || searchPhone == "Nhập vào số điện thoại")
            {
                MessageBox.Show("Vui lòng nhập số điện thoại để tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "SELECT * FROM KhachHang WHERE SoDienThoai = @SDT or HoTen = @SDT or Email = @SDT";

            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@SDT", searchPhone);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Kiểm tra xem có kết quả hay không
                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0]; // Lấy dòng đầu tiên

                        // Cập nhật thông tin vào các TextBox
                        txtMaKhach.Text = row["MaKhachHang"].ToString();
                        txtHoTen.Text = row["HoTen"].ToString();
                        txtEmail.Text = row["Email"].ToString();
                        txtLoaiKhach.Text = row["LoaiKhachHang"].ToString();
                    }
                    else
                    {
                        DialogResult result = MessageBox.Show("Khách hàng chưa tồn tại. Vui lòng thêm khách hàng.", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                        if (result == DialogResult.OK)
                        {
                            // Mở form thêm khách hàng
                            frmThemKhach frmThemKhach = new frmThemKhach();
                            frmThemKhach.ShowDialog();

                            // Sau khi thêm khách hàng, tự động nạp lại thông tin khách hàng mới thêm
                            ReloadCustomerInfo(searchPhone, searchPhone, searchPhone);
                        }
                    }
                }
            }
        }

        // Hàm Reload thông tin khách hàng sau khi thêm mới
        private void ReloadCustomerInfo(string soDienThoai, string hoTen, string email)
        {
            string query = "SELECT * FROM KhachHang WHERE SoDienThoai = @SDT or HoTen = @HoTen or Email = @Email";

            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@SDT", soDienThoai);
                    command.Parameters.AddWithValue("@HoTen", hoTen);
                    command.Parameters.AddWithValue("@Email", email);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0]; // Lấy dòng đầu tiên

                        // Cập nhật thông tin vào các TextBox
                        txtMaKhach.Text = row["MaKhachHang"].ToString();
                        txtHoTen.Text = row["HoTen"].ToString();
                        txtEmail.Text = row["Email"].ToString();
                        txtLoaiKhach.Text = row["LoaiKhachHang"].ToString();
                    }
                }
            }
        }

        private void txtTimKiem_TextChanged_1(object sender, EventArgs e)
        {
            string searchText = txtTimKiem.Text.Trim();


            // Hiển thị gợi ý chỉ khi có ít nhất 2 ký tự
            if (searchText.Length >= 1)
            {
                ShowSuggestions(searchText);
            }
            else
            {
                listBoxSuggestions.Visible = false; // Ẩn nếu không đủ ký tự
            }
        }

        private void listBoxSuggestions_MouseClick(object sender, MouseEventArgs e)
        {
            if (listBoxSuggestions.SelectedItem != null)
            {
                txtTimKiem.Text = listBoxSuggestions.SelectedItem.ToString();
                listBoxSuggestions.Visible = false; // Ẩn gợi ý sau khi chọn
            }
        }

        private void txtTimKiem_Enter(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "Nhập SĐT, Tên, Email...")
            {
                txtTimKiem.Text = ""; // Xóa placeholder
                txtTimKiem.ForeColor = Color.Black; // Đặt lại màu chữ
            }
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            // Kiểm tra thông tin nhập
            if (string.IsNullOrWhiteSpace(txtMaKhach.Text) || dtpThoiGianBatDau.Value >= dtpThoiGianKetThuc.Value)
            {
                MessageBox.Show("Vui lòng kiểm tra lại thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy thông tin từ form
            string maKhachHang = txtMaKhach.Text;
            string maPhong = txtMaPhong.Text;
            DateTime thoiGianBatDau = dtpThoiGianBatDau.Value;
            DateTime? thoiGianKetThuc = null; // Sử dụng nullable DateTime

            // Kiểm tra trạng thái của dtpThoiGianKetThuc
            if (dtpThoiGianKetThuc.Enabled)
            {
                thoiGianKetThuc = dtpThoiGianKetThuc.Value; // Nếu enabled, lấy giá trị
            }
            // Nếu disabled, thoiGianKetThuc sẽ vẫn là null

            // Đặt trạng thái phòng
            string trangThai;
            if (thoiGianBatDau <= DateTime.Now)
            {
                trangThai = "Đang sử dụng";
                StartRoomUsage(maPhong); // Bắt đầu tính giờ sử dụng nếu đặt ngay lập tức
                MessageBox.Show("Phòng đã bắt đầu sử dụng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                trangThai = "Đã đặt phòng";
                MessageBox.Show("Đặt phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // Lấy mã đặt phòng tiếp theo
            string maDatPhong = GetNextBookingCode(connection);
            string TinhTrang = "Chưa thanh toán";

            // Lưu thông tin đặt phòng vào CSDL
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();

                // Lưu thông tin vào bảng DatPhong
                SqlCommand cmdDatPhong = new SqlCommand("INSERT INTO DatPhong (MaDatPhong, MaKhachHang, MaPhong, ThoiGianBatDau, ThoiGianKetThuc, TinhTrang, GhiChu) VALUES (@MaDatPhong, @MaKhachHang, @MaPhong, @ThoiGianBatDau, @ThoiGianKetThuc, @TinhTrang, @GhiChu)", conn);
                cmdDatPhong.Parameters.AddWithValue("@MaDatPhong", maDatPhong);
                cmdDatPhong.Parameters.AddWithValue("@MaKhachHang", maKhachHang);
                cmdDatPhong.Parameters.AddWithValue("@ThoiGianBatDau", thoiGianBatDau);
                cmdDatPhong.Parameters.AddWithValue("@ThoiGianKetThuc", (object)thoiGianKetThuc ?? DBNull.Value); // Set to null if thoiGianKetThuc is null
                cmdDatPhong.Parameters.AddWithValue("@TinhTrang", TinhTrang);
                cmdDatPhong.Parameters.AddWithValue("@MaPhong", maPhong);
                cmdDatPhong.Parameters.AddWithValue("@GhiChu", txtGhiChu.Text);
                cmdDatPhong.ExecuteNonQuery();

                // Cập nhật trạng thái phòng
                UpdateRoomStatus(maPhong, trangThai);
            }

            // Đóng form đặt phòng
            this.DialogResult = DialogResult.OK; // Trả về OK để thông báo rằng đã đặt thành công
            this.Close(); // Đóng form đặt phòng
        }

        private string GetNextBookingCode(string connectionString)
        {
            string newBookingCode;
            bool isDuplicate;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                do
                {
                    // Tạo mã đặt phòng ngẫu nhiên gồm 10 ký tự (chữ và số)
                    newBookingCode = GenerateRandomCode(10);

                    // Kiểm tra xem mã này đã tồn tại trong cơ sở dữ liệu chưa
                    string query = "SELECT COUNT(*) FROM DatPhong WHERE MaDatPhong = @MaDatPhong";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaDatPhong", newBookingCode);
                        int count = (int)cmd.ExecuteScalar();

                        // Nếu mã đã tồn tại, đặt isDuplicate = true để tiếp tục vòng lặp
                        isDuplicate = count > 0;
                    }

                } while (isDuplicate); // Tiếp tục tạo mã mới nếu mã đã tồn tại
            }

            return newBookingCode; // Trả về mã đặt phòng mới
        }

        // Hàm tạo chuỗi ngẫu nhiên với độ dài bất kỳ
        private string GenerateRandomCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        private void UpdateRoomStatus(string maPhong, string trangThai)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE PhongHat SET TrangThai = @TrangThai WHERE MaPhong = @MaPhong", conn);
                cmd.Parameters.AddWithValue("@TrangThai", trangThai);
                cmd.Parameters.AddWithValue("@MaPhong", maPhong);
                cmd.ExecuteNonQuery();
            }
        }
        private void StartRoomUsage(string maPhong)
        {
            // Khởi tạo Timer
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000; // Đặt khoảng thời gian là 1 giây
            timer.Tick += timer_Tick; // Gán sự kiện tick cho Timer
            timer.Start(); // Bắt đầu Timer

            thoiGianDaSuDung = TimeSpan.Zero; // Khởi tạo thời gian đã sử dụng
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            thoiGianDaSuDung = thoiGianDaSuDung.Add(TimeSpan.FromSeconds(1)); // Tăng thêm 1 giây

            // Hiển thị thời gian đã sử dụng trên giao diện người dùng
            lblThoiGianSuDung.Text = string.Format("{0:D2}:{1:D2}:{2:D2}",
                (int)thoiGianDaSuDung.TotalHours,
                thoiGianDaSuDung.Minutes,
                thoiGianDaSuDung.Seconds);
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
