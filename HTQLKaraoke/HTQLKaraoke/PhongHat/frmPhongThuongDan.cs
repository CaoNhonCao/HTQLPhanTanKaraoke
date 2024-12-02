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
    public partial class frmPhongThuongDan : Form
    {
        string connection = ConfigurationManager.ConnectionStrings["HTQLKaraoke.Properties.Settings.KaraokeConnectionString"].ConnectionString;
        string LoaiPhong;
        private TimeSpan elapsedTime;
        private bool isClockRunning = false; 

        public frmPhongThuongDan(string loaiPhong)
        {
            LoaiPhong = loaiPhong;
            InitializeComponent();
            flowLayoutPanel.Scroll += flowLayoutPanel_Scroll;
        }

        private void btnThemPhong_Click(object sender, EventArgs e)
        {
            frmThemPhongChoThuongDan frmThuongDan = new frmThemPhongChoThuongDan(LoaiPhong);


            frmThuongDan.FormClosed += (s, args) =>
            {
                LoadRoomData();
            };
            // Mở form sửa thông tin khách hàng và truyền mã khách hàng
            frmThuongDan.ShowDialog();
        }

        private void frmPhongThuongDan_Load(object sender, EventArgs e)
        {
            lblTitle.Text = "Quản Lý Đặt Phòng Cho " + LoaiPhong.ToString();
            LoadRoomData(); 
        }
        private void LoadRoomData()
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string query = "SELECT MaPhong, SoPhong, TrangThai, SucChua, GiaTheoGio, LoaiPhong FROM PhongHat WHERE LoaiPhong = @LoaiPhong ORDER BY SoPhong ASC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Đặt tham số trước khi thực hiện câu lệnh
                    cmd.Parameters.AddWithValue("@LoaiPhong", LoaiPhong);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        flowLayoutPanel.Controls.Clear();

                        while (reader.Read())
                        {
                            int soPhong = Convert.ToInt32(reader["SoPhong"]);
                            string maPhong = reader["MaPhong"].ToString();
                            string trangThai = reader["TrangThai"].ToString();
                            int sucChua = Convert.ToInt32(reader["SucChua"]);
                            decimal giaTheoGio = Convert.ToDecimal(reader["GiaTheoGio"]);
                            string loaiPhong = reader["LoaiPhong"].ToString();

                            Button btnRoom = new Button();
                            btnRoom.Text = string.Format("Phòng {0}", soPhong);
                            btnRoom.Size = new Size(135, 146);
                            btnRoom.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);
                            btnRoom.TextAlign = ContentAlignment.BottomCenter;
                            btnRoom.ImageAlign = ContentAlignment.MiddleCenter;

                            btnRoom.Click += (s, e) =>
                            {
                                ShowRoomDetails(maPhong, soPhong, trangThai, sucChua, giaTheoGio, loaiPhong);
                                if (txtTrangThai.Text == "Chưa đặt phòng")
                                {
                                    btnSua.Enabled = true;
                                    btnXoa.Enabled = true;
                                    btnDatPhong.Enabled = true;
                                    btnDung.Enabled = false;
                                    btnOrder.Enabled = false;
                                    btnDichVu.Enabled = false;
                                    btnXuatHoaDon.Enabled = false;
                                }
                                if (txtTrangThai.Text == "Đã đặt phòng" || txtTrangThai.Text == "Đang sử dụng")
                                {
                                    btnSua.Enabled = false;
                                    btnXoa.Enabled = false;
                                    btnDatPhong.Enabled = false;
                                    btnDung.Enabled = true;
                                    btnOrder.Enabled = true;
                                    btnDichVu.Enabled = true;
                                    btnXuatHoaDon.Enabled = true;
                                }
                                if (txtTrangThai.Text == "Đang bảo trì")
                                {
                                    btnSua.Enabled = true;
                                    btnXoa.Enabled = true;
                                    btnDatPhong.Enabled = false;
                                    btnDung.Enabled = true;
                                    btnOrder.Enabled = false;
                                    btnDichVu.Enabled = false;
                                    btnXuatHoaDon.Enabled = false;
                                }
                            };

                            try
                            {
                                string imagePath = string.Empty;
                                switch (trangThai)
                                {
                                    case "Chưa đặt phòng":
                                        imagePath = @"D:\HTQLKaraoke\HTQLKaraoke\Image\ChuaDat.gif";
                                        break;
                                    case "Đã đặt phòng":
                                        imagePath = @"D:\HTQLKaraoke\HTQLKaraoke\Image\DaDat.gif";
                                        break;
                                    case "Đang sử dụng":
                                        imagePath = @"D:\HTQLKaraoke\HTQLKaraoke\Image\DangSuDung.gif";
                                        break;
                                    case "Đang bảo trì":
                                        imagePath = @"D:\HTQLKaraoke\HTQLKaraoke\Image\BaoTri.gif";
                                        break;
                                }
                                btnRoom.Image = Image.FromFile(imagePath);
                            }
                            catch (FileNotFoundException ex)
                            {
                                MessageBox.Show("Không tìm thấy file hình ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Đã xảy ra lỗi khi tải hình ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            flowLayoutPanel.Controls.Add(btnRoom);
                        }
                    }
                }
            }
        }
        string maKhach;
        // Định nghĩa một Dictionary để lưu trữ timer cho từng phòng
        private Dictionary<string, Timer> roomTimers = new Dictionary<string, Timer>();

        private void ShowRoomDetails(string maPhong, int soPhong, string trangThai, int sucChua, decimal giaTheoGio, string loaiPhong)
        {
            // Kiểm tra thông tin cơ bản của phòng
            if (soPhong < 101 || soPhong > 199)
            {
                MessageBox.Show("Số phòng không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (sucChua < 1 || sucChua > 20)
            {
                MessageBox.Show("Sức chứa không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Hiển thị thông tin phòng
            groupBoxRoomDetails.Text = $"Thông Tin Phòng {soPhong}";
            txtMaPhong.Text = maPhong;
            txtSoPhong.Text = soPhong.ToString();
            txtTrangThai.Text = trangThai;
            txtSucChua.Text = sucChua.ToString();
            txtGiaThue.Text = giaTheoGio.ToString("N0") + "₫";
            txtLoaiPhong.Text = loaiPhong;

            // Trạng thái phòng
            if (trangThai == "Chưa đặt phòng" || trangThai == "Đang bảo trì")
            {
                txtKhachDangDat.Text = string.Empty;
                txtBatDau.Text = string.Empty;
                txtTongThoiGian.Text = string.Empty;
                txtGhiChu.Text = string.Empty;

                // Tắt đồng hồ nếu có
                if (roomTimers.ContainsKey(maPhong))
                {
                    roomTimers[maPhong].Stop();
                }
                return;
            }

            // Kết nối cơ sở dữ liệu
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();

                string query = "SELECT d.ThoiGianBatDau, d.ThoiGianKetThuc, d.GhiChu, k.HoTen " +
                               "FROM DatPhong d " +
                               "LEFT JOIN KhachHang k ON d.MaKhachHang = k.MaKhachHang " +
                               "WHERE d.MaPhong = @MaPhong AND d.TinhTrang = N'Chưa thanh toán'";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaPhong", maPhong);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string tenKhachHang = reader["HoTen"] != DBNull.Value ? reader["HoTen"].ToString() : "Khách vãng lai";
                            txtKhachDangDat.Text = tenKhachHang;

                            DateTime thoiGianBatDau = reader["ThoiGianBatDau"] != DBNull.Value
                                ? Convert.ToDateTime(reader["ThoiGianBatDau"])
                                : DateTime.MinValue;

                            txtBatDau.Text = thoiGianBatDau != DateTime.MinValue
                                ? thoiGianBatDau.ToString("HH:mm:ss, dd/MM/yyyy")
                                : "Chưa bắt đầu";

                            if (reader["ThoiGianKetThuc"] == DBNull.Value)
                            {
                                if (!roomTimers.ContainsKey(maPhong))
                                {
                                    // Tính thời gian trôi qua
                                    elapsedTime = DateTime.Now - thoiGianBatDau;

                                    // Tạo Timer riêng cho phòng
                                    Timer roomTimer = new Timer();
                                    roomTimer.Interval = 1000; // 1 giây
                                    roomTimer.Tick += (s, e) =>
                                    {
                                        // Tính toán lại thời gian trôi qua
                                        elapsedTime = DateTime.Now - thoiGianBatDau;

                                        // Cập nhật thông tin thời gian của đúng phòng
                                        if (txtMaPhong.Text == maPhong)
                                        {
                                            txtTongThoiGian.Invoke((MethodInvoker)(() =>
                                            {
                                                txtTongThoiGian.Text = string.Format("{0:D2}:{1:D2}:{2:D2}",
                                                    (int)elapsedTime.TotalHours,
                                                    elapsedTime.Minutes,
                                                    elapsedTime.Seconds);
                                            }));
                                        }
                                    };

                                    // Lưu Timer vào danh sách và khởi chạy
                                    roomTimers[maPhong] = roomTimer;
                                    roomTimer.Start();
                                    isClockRunning = true;
                                }
                                else
                                {
                                    // Nếu Timer đã tồn tại, đảm bảo chỉ chạy lại Timer cho phòng cụ thể
                                    roomTimers[maPhong].Start();
                                    isClockRunning = true;
                                }
                            }
                            else
                            {
                                // Tính tổng thời gian nếu có thời gian kết thúc
                                DateTime thoiGianKetThuc = Convert.ToDateTime(reader["ThoiGianKetThuc"]);
                                TimeSpan tongThoiGian = thoiGianKetThuc - thoiGianBatDau;
                                txtTongThoiGian.Text = string.Format("{0:D2}:{1:D2}:{2:D2}",
                                    (int)tongThoiGian.TotalHours,
                                    tongThoiGian.Minutes,
                                    tongThoiGian.Seconds);
                            }

                            txtGhiChu.Text = reader["GhiChu"] != DBNull.Value ? reader["GhiChu"].ToString() : "Không có ghi chú";
                        }
                        else
                        {
                            // Xử lý khi không tìm thấy thông tin đặt phòng
                            txtKhachDangDat.Text = "Không có khách hàng";
                            txtBatDau.Text = "Không có thông tin";
                            txtTongThoiGian.Text = string.Empty;
                            txtGhiChu.Text = "Không có ghi chú";
                        }
                    }
                }
            }

            groupBoxRoomDetails.Visible = true; // Hiển thị thông tin phòng
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int soPhong = int.Parse(txtSoPhong.Text.Replace("Số Phòng: ", ""));
            string trangThai = txtTrangThai.Text.Replace("Trạng Thái: ", "");
            string maPhong = txtMaPhong.Text;

            // Danh sách các bảng cần kiểm tra dữ liệu liên quan
            string[] tables = { "DichVuChoPhong", "HoaDon", "LichSuBaoTri", "DanhGiaKhachHang", "DatPhong", "DonHang" };
            List<string> affectedTables = new List<string>();

            try
            {
                DialogResult initialResult = MessageBox.Show("Bạn có chắc chắn muốn xóa phòng này?", "Xác nhận xóa phòng", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (initialResult != DialogResult.Yes)
                {
                    return;
                }

                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();

                    // Kiểm tra dữ liệu trong các bảng phụ
                    foreach (string table in tables)
                    {
                        string checkQuery = "SELECT COUNT(*) FROM " + table + " WHERE MaPhong = @MaPhong";

                        using (SqlCommand cmd = new SqlCommand(checkQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@MaPhong", maPhong);
                            int count = (int)cmd.ExecuteScalar();

                            if (count > 0)
                            {
                                affectedTables.Add(table);
                            }
                        }
                    }
                }

                // Hiển thị thông báo nếu có dữ liệu liên quan
                if (affectedTables.Count > 0)
                {string message = "Phòng này có một số dữ liệu liên quan trong các bảng sau:\n" +
                 string.Join("\n", affectedTables) +
                 "\n\nLưu ý: Nếu bạn tiếp tục, tất cả dữ liệu liên quan trong các bảng trên sẽ bị xóa.\n" +
                 "Bạn có chắc chắn muốn thực hiện hành động này?";

DialogResult result = MessageBox.Show(message, "Xác nhận xóa phòng", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result != DialogResult.OK)
                    {
                        return; // Hủy xóa nếu người dùng chọn Cancel
                    }
                }

                // Thực hiện xóa dữ liệu
                PerformDelete(maPhong, soPhong);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi kiểm tra dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PerformDelete(string maPhong, int soPhong)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();

                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // Lấy danh sách mã đơn hàng liên quan đến mã phòng
                            string getOrderIdsQuery = "SELECT MaDonHang FROM DonHang WHERE MaPhong = @MaPhong";
                            List<string> orderIds = new List<string>();

                            using (SqlCommand cmd = new SqlCommand(getOrderIdsQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@MaPhong", maPhong);
                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        orderIds.Add(reader["MaDonHang"].ToString());
                                    }
                                }
                            }

                            // Xóa dữ liệu trong bảng ChiTietDonHang theo danh sách mã đơn hàng
                            foreach (string orderId in orderIds)
                            {
                                string deleteOrderDetailsQuery = "DELETE FROM ChiTietDonHang WHERE MaDonHang = @MaDonHang";
                                using (SqlCommand cmd = new SqlCommand(deleteOrderDetailsQuery, conn, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@MaDonHang", orderId);
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            // Xóa dữ liệu trong bảng DonHang liên quan đến mã phòng
                            string deleteDonHangQuery = "DELETE FROM DonHang WHERE MaPhong = @MaPhong";
                            using (SqlCommand cmd = new SqlCommand(deleteDonHangQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@MaPhong", maPhong);
                                cmd.ExecuteNonQuery();
                            }

                            // Xóa dữ liệu trong các bảng phụ khác
                            string[] deleteQueries =
                    {
                        "DELETE FROM DatPhong WHERE MaPhong = @MaPhong",
                        "DELETE FROM DichVuChoPhong WHERE MaPhong = @MaPhong",
                        "DELETE FROM HoaDon WHERE MaPhong = @MaPhong",
                        "DELETE FROM LichSuBaoTri WHERE MaPhong = @MaPhong",
                        "DELETE FROM DanhGiaKhachHang WHERE MaPhong = @MaPhong"
                    };

                            foreach (string query in deleteQueries)
                            {
                                ExecuteDeleteQuery(query, maPhong, conn, transaction);
                            }

                            // Xóa dữ liệu trong bảng PhongHat
                            string deletePhongHatQuery = "DELETE FROM PhongHat WHERE MaPhong = @MaPhong";
                            using (SqlCommand cmd = new SqlCommand(deletePhongHatQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@MaPhong", maPhong);
                                cmd.ExecuteNonQuery();
                            }

                            // Xác nhận giao dịch
                            transaction.Commit();
                            MessageBox.Show("Xóa phòng và các dữ liệu liên quan thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Cập nhật lại giao diện sau khi xóa
                            LoadRoomData();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Lỗi khi xóa dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thực hiện xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Hàm thực thi câu lệnh DELETE
        private void ExecuteDeleteQuery(string query, string maPhong, SqlConnection conn, SqlTransaction transaction)
        {
            using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@MaPhong", maPhong);
                cmd.ExecuteNonQuery();
            }
        }


        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            int soPhong = Convert.ToInt32(txtSoPhong.Text);
            string trangThai = txtTrangThai.Text;

            // Lấy sức chứa và giá thuê
            int sucChua = Convert.ToInt32(txtSucChua.Text);
            decimal giaThue = Convert.ToDecimal(txtGiaThue.Text
                .Replace("₫", "")  // Bỏ ký hiệu tiền
                .Replace(",", "")   // Bỏ dấu phẩy
                .Trim());

            // Lấy mã phòng từ cơ sở dữ liệu dựa trên số phòng
            string maPhong;
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string query = "SELECT MaPhong FROM PhongHat WHERE SoPhong = @SoPhong and LoaiPhong = @LoaiPhong";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SoPhong", soPhong);
                    cmd.Parameters.AddWithValue("@LoaiPhong", LoaiPhong);
                    object result = cmd.ExecuteScalar();
                    maPhong = result.ToString();
                }
            }

            // Mở form sửa phòng và truyền dữ liệu vào form đó
            frmSuaPhong editRoomForm = new frmSuaPhong(maPhong, soPhong, LoaiPhong, trangThai, sucChua, giaThue);

            if (editRoomForm.ShowDialog() == DialogResult.OK)
            {
                LoadRoomData();
            }
        }

        private void btnDatPhong_Click(object sender, EventArgs e)
        {

            int soPhong = Convert.ToInt32(txtSoPhong.Text);
            string trangThaiPhong = txtTrangThai.Text;
            string maPhong = txtMaPhong.Text;

            frmLuaChon luaChon = new frmLuaChon();
            DialogResult result = luaChon.ShowDialog();

            if (result == DialogResult.Yes)
            {
                frmDatPhong DatPhong = new frmDatPhong(soPhong, maPhong, LoaiPhong);
                DatPhong.FormClosed += (s, args) =>
                {
                    LoadRoomData();
                };
                DatPhong.ShowDialog(); 
            }
            else if (result == DialogResult.No)
            {
                
                string maDatPhong = GetNextBookingCode(connection);

                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();

                    // Lưu thông tin vào bảng DatPhong
                    SqlCommand cmdDatPhong = new SqlCommand("INSERT INTO DatPhong (MaDatPhong, MaPhong, ThoiGianBatDau, TinhTrang) VALUES (@MaDatPhong, @MaPhong, GetDate(), N'Chưa thanh toán')", conn);
                    cmdDatPhong.Parameters.AddWithValue("@MaDatPhong", maDatPhong);
                    cmdDatPhong.Parameters.AddWithValue("@MaPhong", maPhong);
                    cmdDatPhong.ExecuteNonQuery();

                    // Cập nhật trạng thái phòng
                    UpdateRoomStatus(maPhong);
                }

                MessageBox.Show("Phòng số " + soPhong + " đã bắt đầu sử dụng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadRoomData(); 
            }
        }
        private void UpdateRoomStatus(string maPhong)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE PhongHat SET TrangThai = N'Đang sử dụng' WHERE MaPhong = @MaPhong", conn);
                cmd.Parameters.AddWithValue("@MaPhong", maPhong);
                cmd.ExecuteNonQuery();
            }
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
                    newBookingCode = GenerateRandomCode(10);

                    // Kiểm tra xem mã này đã tồn tại trong cơ sở dữ liệu chưa
                    string query = "SELECT COUNT(*) FROM DatPhong WHERE MaDatPhong = @MaDatPhong";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaDatPhong", newBookingCode);
                        int count = (int)cmd.ExecuteScalar();

                        isDuplicate = count > 0;
                    }

                } while (isDuplicate); // Tiếp tục tạo mã mới nếu mã đã tồn tại
            }

            return newBookingCode; // Trả về mã đặt phòng mới
        }
        private string GenerateRandomCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void btnDung_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem người dùng đã chọn phòng hay chưa
            if (string.IsNullOrWhiteSpace(txtMaPhong.Text))
            {
                MessageBox.Show("Vui lòng chọn phòng cần dừng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maPhong = txtMaPhong.Text;
            int soPhong = Convert.ToInt32(txtSoPhong.Text);
            string trangThai = txtTrangThai.Text;
            string thongBao = "";

            // Kiểm tra trạng thái và xác nhận dừng phòng
            if (trangThai == "Đang bảo trì")
            {
                thongBao = "Bạn có muốn dừng bảo trì phòng không?";
            }
            else if (trangThai == "Đang sử dụng")
            {
                thongBao = "Bạn có muốn dừng phòng để thanh toán không?";
            }
            else if (trangThai == "Đã đặt phòng")
            {
                thongBao = "Bạn có muốn hủy đặt phòng này không?";
            }

            DialogResult result = MessageBox.Show(thongBao, "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();

                if (trangThai == "Đang bảo trì" && result == DialogResult.Yes)
                {
                    // Kiểm tra và cập nhật lịch sử bảo trì
                    string checkLichSuBaoTriQuery = "SELECT COUNT(*) FROM LichSuBaoTri WHERE MaPhong = @MaPhong AND GhiChu = N'Đang bảo trì'";
                    using (SqlCommand cmdCheck = new SqlCommand(checkLichSuBaoTriQuery, conn))
                    {
                        cmdCheck.Parameters.AddWithValue("@MaPhong", maPhong);
                        int count = Convert.ToInt32(cmdCheck.ExecuteScalar());

                        if (count > 0)
                        {
                            // Cập nhật ghi chú trong lịch sử bảo trì
                            string updateLichSuBaoTriQuery = "UPDATE LichSuBaoTri SET GhiChu = N'Đã xong' WHERE MaPhong = @MaPhong AND GhiChu = N'Đang bảo trì'";
                            using (SqlCommand cmdUpdate = new SqlCommand(updateLichSuBaoTriQuery, conn))
                            {
                                cmdUpdate.Parameters.AddWithValue("@MaPhong", maPhong);
                                cmdUpdate.ExecuteNonQuery();
                            }
                        }
                    }

                    // Cập nhật trạng thái phòng trong bảng PhongHat
                    string updatePhongHatQuery = "UPDATE PhongHat SET TrangThai = N'Chưa đặt phòng' WHERE MaPhong = @MaPhong";
                    using (SqlCommand cmdUpdatePhong = new SqlCommand(updatePhongHatQuery, conn))
                    {
                        cmdUpdatePhong.Parameters.AddWithValue("@MaPhong", maPhong);
                        cmdUpdatePhong.ExecuteNonQuery();
                    }

                    MessageBox.Show("Đã dừng bảo trì phòng và cập nhật lịch sử bảo trì.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (trangThai == "Đang sử dụng" && result == DialogResult.Yes)
                {
                    // Cập nhật thời gian kết thúc trong bảng DatPhong
                    string updateDatPhongQuery = "UPDATE DatPhong SET ThoiGianKetThuc = @ThoiGianKetThuc WHERE MaPhong = @MaPhong AND TinhTrang = N'Chưa thanh toán'";
                    using (SqlCommand cmd = new SqlCommand(updateDatPhongQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@ThoiGianKetThuc", DateTime.Now);
                        cmd.Parameters.AddWithValue("@MaPhong", maPhong);
                        cmd.ExecuteNonQuery();
                    }


                    // Dừng timer riêng của phòng
                    if (roomTimers.ContainsKey(maPhong))
                    {
                        roomTimers[maPhong].Stop();
                        roomTimers.Remove(maPhong);
                        isClockRunning = false;
                    }
                    MessageBox.Show("Đã dừng phòng và cập nhật thời gian kết thúc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (trangThai == "Đã đặt phòng" && result == DialogResult.Yes)
                {
                    // Xóa dữ liệu đặt phòng và cập nhật trạng thái phòng
                    string deleteDatPhongQuery = "DELETE FROM DatPhong WHERE MaPhong = @MaPhong AND MaKhachHang = @MaKhachHang AND TinhTrang = N'Chưa thanh toán'";
                    using (SqlCommand cmd = new SqlCommand(deleteDatPhongQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaPhong", maPhong);
                        cmd.Parameters.AddWithValue("@MaKhachHang", maKhach);
                        cmd.ExecuteNonQuery();
                    }

                    string updatePhongHatQuery = "UPDATE PhongHat SET TrangThai = N'Chưa đặt phòng' WHERE MaPhong = @MaPhong";
                    using (SqlCommand cmd = new SqlCommand(updatePhongHatQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaPhong", maPhong);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Đã hủy phòng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            // Load lại dữ liệu phòng
            LoadRoomData();
        }

        private void flowLayoutPanel_Scroll(object sender, ScrollEventArgs e)
        {
                // Kiểm tra vị trí cuộn
            int scrollPosition = flowLayoutPanel.VerticalScroll.Value;
    
            // Thực hiện hành động khi cuộn
            Console.WriteLine("Vị trí cuộn hiện tại: " + scrollPosition);

            // Ví dụ: Cập nhật tiêu đề form với vị trí cuộn
            this.Text = "Vị trí cuộn: " + scrollPosition;
        }

        private void btnLichSu_Click(object sender, EventArgs e)
        {
            frmLichSuDatPhong frmLichSu = new frmLichSuDatPhong(LoaiPhong);
            frmLichSu.ShowDialog(); 
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            string maDonHang = null;
            bool trangThaiDonHang = false;
            // Truy vấn kiểm tra đơn hàng có mã phòng và trạng thái là false
            string checkOrderQuery = @"
    SELECT MaDonHang, TrangThai
    FROM DonHang
    WHERE MaPhong = @MaPhong AND TrangThai = 0"; // Chỉ lấy đơn hàng chưa thanh toán

            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(checkOrderQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@MaPhong", txtMaPhong.Text);

                    try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Lấy mã đơn hàng nếu có trạng thái là false
                                maDonHang = reader["MaDonHang"].ToString();
                                trangThaiDonHang = Convert.ToBoolean(reader["TrangThai"]);
                            }
                            else
                            {
                                // Nếu không có dữ liệu trả về (không có mã phòng nào với trạng thái là false)
                                maDonHang = null;
                                trangThaiDonHang = false;
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Lỗi khi kiểm tra mã đơn hàng: " + ex.Message);
                        return;
                    }
                }
            }

            // Kiểm tra trạng thái đơn hàng
            if (maDonHang != null && !trangThaiDonHang)
            {
                // Update đơn hàng hiện có nếu trạng thái là false
                UpdateExistingOrder(maDonHang);
            }
            else if (maDonHang == null)
            {
                // Tạo đơn hàng mới nếu không tìm thấy đơn hàng nào với mã phòng và trạng thái là false
                maDonHang = GenerateOrderCode();
                InsertNewOrder(maDonHang, txtMaPhong.Text);
            }

            // Chuyển mã đơn hàng hợp lệ sang form khác
            frmOrder frmOrder = new frmOrder(maDonHang, txtMaPhong.Text, txtSoPhong.Text, LoaiPhong);
            frmOrder.ShowDialog();
        }

        private void InsertNewOrder(string maDonHang, string maPhong)
        {
            string insertDonHangQuery = "INSERT INTO DonHang (MaDonHang, MaPhong, NgayTao, TrangThai) VALUES (@MaDonHang, @MaPhong, GETDATE(), 0)";

            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(insertDonHangQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@MaDonHang", maDonHang);
                    cmd.Parameters.AddWithValue("@MaPhong", maPhong);

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Lỗi khi thêm đơn hàng mới: " + ex.Message);
                    }
                }
            }
        }

        private void UpdateExistingOrder(string maDonHang)
        {
            string updateDonHangQuery = "UPDATE DonHang SET NgayTao = GETDATE() WHERE MaDonHang = @MaDonHang AND TrangThai = 0";

            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(updateDonHangQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@MaDonHang", maDonHang);

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Lỗi khi cập nhật đơn hàng: " + ex.Message);
                    }
                }
            }
        }

        private string GenerateOrderCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            string randomString = new string(Enumerable.Repeat(chars, 10)
                                                  .Select(s => s[random.Next(s.Length)]).ToArray());
            return randomString;
        }

        private void btnDichVu_Click(object sender, EventArgs e)
        {
            frmDichVu frmDichVu = new frmDichVu(txtMaPhong.Text);
            frmDichVu.ShowDialog();
        }

        private void btnXuatHoaDon_Click(object sender, EventArgs e)
        {
            if (isClockRunning)
            {
                MessageBox.Show("Vui lòng dừng phòng trước khi in hóa đơn.");
                return;
            }

            // Nếu tất cả điều kiện đã thỏa mãn, hiển thị form hóa đơn
            frmHoaDon frm = new frmHoaDon(txtMaPhong.Text, txtSoPhong.Text, txtKhachDangDat.Text, txtGiaThue.Text, LoaiPhong, txtBatDau.Text);
            frm.FormClosed += (s, args) =>
            {
                LoadRoomData(); // Tải lại dữ liệu phòng sau khi đặt
            };
            frm.ShowDialog();
        }
    }
}
