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
using iTextSharp.text;
using iTextSharp.text.pdf;
using DrawingRectangle = System.Drawing.Rectangle;
using PdfRectangle = iTextSharp.text.Rectangle;
using System.Drawing.Printing; 

namespace HTQLKaraoke.PhongHat
{
    public partial class frmHoaDon : Form
    {
        string connection = ConfigurationManager.ConnectionStrings["HTQLKaraoke.Properties.Settings.KaraokeConnectionString"].ConnectionString;
        string maPhong, soPhong, tenKhachHang, giaThue, loaiPhong, gioBatDau, tenChiNhanh;
        decimal tongTien, tongThanhToan, giamGia;
        public frmHoaDon(string maPhong, string soPhong, string tenKhachHang, string giaThue, string loaiPhong, string gioBatDau)
        {
            InitializeComponent();
            this.soPhong = soPhong;
            this.tenKhachHang = tenKhachHang;
            this.giaThue = giaThue;
            this.loaiPhong = loaiPhong;
            this.maPhong = maPhong;
            this.gioBatDau = gioBatDau;
        }
        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            lblHoaDonPhong.Text = lblHoaDonPhong.Text + " " + soPhong;
            lblTenKhachHang.Text = tenKhachHang;
            lblLoaiPhong.Text = loaiPhong;
            lblGiaGio.Text = giaThue + "/Giờ";
            lblGioBatDau.Text = gioBatDau;

            DateTime? endTime = GetEndTime(maPhong);
            lblTenChiNhanh.Text = lblTenChiNhanh.Text + " " + getTenChiNhanh();
            if (endTime.HasValue)
            {
                lblGioKetThuc.Text = endTime.Value.ToString("HH:mm:ss, dd/MM/yyyy");

                // Tính tổng thời gian
                DateTime thoiGianBatDau = DateTime.Parse(gioBatDau);
                DateTime thoiGianKetThuc = endTime.Value;
                TimeSpan tongThoiGianSpan = thoiGianKetThuc - thoiGianBatDau;

                // Tính số giờ, phút, giây
                int hours = (int)tongThoiGianSpan.TotalHours; // Lấy số giờ
                int minutes = tongThoiGianSpan.Minutes; // Lấy số phút
                int seconds = tongThoiGianSpan.Seconds; // Lấy số giây

                // Hiển thị tổng thời gian lên lblTongThoiGian
                lblTongThoiGian.Text = hours+" giờ, "+ +minutes+" phút, "+seconds+" giây"; // Cập nhật label
            }
            else
            {
                lblGioKetThuc.Text = "Không tìm thấy thời gian kết thúc.";
            }

            if (dgvDichVu_SanPham.Columns.Count == 0)
            {
                dgvDichVu_SanPham.Columns.Add("Ten", "Tên");
                dgvDichVu_SanPham.Columns.Add("SoLuong", "SL");
                dgvDichVu_SanPham.Columns.Add("Gia", "Giá");
                dgvDichVu_SanPham.Columns.Add("Tong", "Tổng tiền");
            }

            dgvDichVu_SanPham.Columns["Ten"].Width = 150;
            dgvDichVu_SanPham.Columns["SoLuong"].Width = 50;
            dgvDichVu_SanPham.Columns["Gia"].Width = 100;
            dgvDichVu_SanPham.Columns["Tong"].Width = 100;
            dgvDichVu_SanPham.AllowUserToAddRows = false;

            LoadDichVuVaSanPham();

            if(string.IsNullOrWhiteSpace(tenKhachHang))
            {
                UpdateDiscount(GetMaKhachHangByTen(tenKhachHang));
            }
        }

        private DateTime? GetEndTime(string maPhong)
        {
            DateTime? endTime = null; // Khởi tạo biến thời gian kết thúc là null

            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();

                // Truy vấn để lấy mã khách hàng dựa trên tên khách hàng
                string query = @"
            SELECT ThoiGianKetThuc 
            FROM DatPhong WHERE MaPhong = @maPhong 
              AND TinhTrang = N'Chưa thanh toán'";

                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@maPhong", maPhong);

                    // Thực thi truy vấn và lấy thời gian kết thúc
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        endTime = Convert.ToDateTime(result);
                    }
                }
            }

            return endTime; 
        }
        private string getTenChiNhanh()
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();

                string query = @"select top 1 TenChiNhanh from ChiNhanh";

                using (SqlCommand command = new SqlCommand(query, conn))
                {

                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        tenChiNhanh = result.ToString(); ;
                    }
                }
            }

            return tenChiNhanh; 
        }

        private void LoadDichVuVaSanPham()
        {
            decimal tongDichVu = 0;
            decimal tongSanPham = 0;

            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();

                // Lấy danh sách dịch vụ
                string queryDichVu = @"
        SELECT dv.TenDichVu AS Ten, dvcp.SoLuong, dv.GiaDichVu AS Gia, dvcp.SoLuong * dv.GiaDichVu AS Tong
        FROM DichVuChoPhong dvcp
        JOIN DichVu dv ON dvcp.MaDichVu = dv.MaDichVu
        WHERE dvcp.MaPhong = @maPhong and dvcp.TrangThai = 0";

                using (SqlCommand cmdDichVu = new SqlCommand(queryDichVu, conn))
                {
                    cmdDichVu.Parameters.AddWithValue("@maPhong", maPhong);
                    using (SqlDataReader reader = cmdDichVu.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string ten = reader["Ten"].ToString();
                            int soLuong = Convert.ToInt32(reader["SoLuong"]);
                            decimal gia = Convert.ToDecimal(reader["Gia"]);
                            decimal tong = Convert.ToDecimal(reader["Tong"]);

                            tongDichVu += tong;

                            dgvDichVu_SanPham.Rows.Add(ten, soLuong, gia.ToString("N0"), tong.ToString("N0")); // Hiển thị không có chữ số thập phân
                        }
                    }
                }

                // Lấy danh sách sản phẩm
                string querySanPham = @"
        SELECT sp.TenSanPham AS Ten, ctdh.SoLuong, sp.GiaBan AS Gia, ctdh.ThanhTien AS Tong
        FROM ChiTietDonHang ctdh
        JOIN SanPham sp ON ctdh.MaSanPham = sp.MaSanPham
        JOIN DonHang dh ON ctdh.MaDonHang = dh.MaDonHang
        WHERE dh.MaPhong = @maPhong and dh.TrangThai = 0";

                using (SqlCommand cmdSanPham = new SqlCommand(querySanPham, conn))
                {
                    cmdSanPham.Parameters.AddWithValue("@maPhong", maPhong);
                    using (SqlDataReader reader = cmdSanPham.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string ten = reader["Ten"].ToString();
                            int soLuong = Convert.ToInt32(reader["SoLuong"]);
                            decimal gia = Convert.ToDecimal(reader["Gia"]);
                            decimal tong = Convert.ToDecimal(reader["Tong"]);

                            tongSanPham += tong;

                            dgvDichVu_SanPham.Rows.Add(ten, soLuong, gia.ToString("N0"), tong.ToString("N0")); // Hiển thị không có chữ số thập phân
                        }
                    }
                }
            }

            // Gán tổng tiền dịch vụ và sản phẩm lên các Label
            lblTongDichVu.Text = tongDichVu.ToString("N0") + " VND";
            lblTongSanPham.Text = tongSanPham.ToString("N0") + " VND";

            DateTime thoiGianBatDau = DateTime.Parse(gioBatDau);
            DateTime thoiGianKetThuc = DateTime.Parse(lblGioKetThuc.Text);
            TimeSpan tongThoiGian = thoiGianKetThuc - thoiGianBatDau;
            decimal tongThoiGianGio = (decimal)tongThoiGian.TotalHours;

            decimal giaThueGio = ConvertToDecimal(giaThue);
            decimal tienGio = tongThoiGianGio * giaThueGio;

            lblTienGio.Text = tienGio.ToString("N0") + " VND";

            tongTien = tongDichVu + tongSanPham + tienGio;
            giamGia = IsKhachHangVIP(tenKhachHang) ? tongTien * 0.05m : 0;
            lblGiamGia.Text = giamGia.ToString("N0") + " VND";

            lblTongTien.Text = tongTien.ToString("N0") + " VND";
            // Tính tổng số tiền phải thanh toán
            tongThanhToan = tongTien- giamGia;
            lblThanhToan.Text = tongThanhToan.ToString("N0") + " VND";
        }

        private decimal ConvertToDecimal(string amount)
        {
            string cleanedAmount = amount.Replace(".", "").Replace("₫", "").Trim();

            decimal parsedAmount;

            if (decimal.TryParse(cleanedAmount, out parsedAmount))
            {
                return parsedAmount;
            }
            else
            {
                return 0;
            }
        }

        private void btnXuatHoaDon_Click(object sender, EventArgs e)
        {
            LoadbtnXuatHoaDon();
        }

        private void LoadbtnXuatHoaDon()
        {
            DialogResult result = MessageBox.Show("Bạn có muốn xuất hóa đơn để thanh toán không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                // Tạo mã hóa đơn ngẫu nhiên
                string maHoaDon = GenerateRandomCode(10);

                // Lấy mã khách hàng từ tên khách hàng
                string tenKhachHang = lblTenKhachHang.Text;
                string maKhachHang = GetMaKhachHangByTen(tenKhachHang);

                // Lấy thông tin hóa đơn
                DateTime ngayLapHoaDon = DateTime.Now;
                string ghiChu = "Đã thanh toán";

              
                    SaveHoaDonToDatabase(maHoaDon, maKhachHang, maPhong, ngayLapHoaDon, tongTien, tongThanhToan, giamGia, ghiChu);

                    ExportBillToPDF();

                    UpdateRoomStatus(maPhong);
                    UpdateServiceStatus(maPhong);
                    UpdateBookingStatus(maKhachHang, maPhong);
                    UpdateDonHangStatus(maPhong);
                    UpdateStockQuantity();

                if (!string.IsNullOrEmpty(maKhachHang))
                {
                    frmDanhGiaKH frmDanhGia = new frmDanhGiaKH(maKhachHang, maPhong, tenKhachHang, soPhong);
                    frmDanhGia.ShowDialog();

                    CheckAndUpdateVIPStatus(maKhachHang);
                }

                MessageBox.Show("Xuất hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void UpdateRoomStatus(string maPhong)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string query = "UPDATE PhongHat SET TrangThai = N'Chưa đặt phòng' WHERE MaPhong = @MaPhong";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaPhong", maPhong);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        private void UpdateServiceStatus(string maPhong)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                string query = "UPDATE DichVuChoPhong SET TrangThai = 1 WHERE MaPhong = @MaPhong AND TrangThai = 0";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaPhong", maPhong);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UpdateBookingStatus(string maKhachHang, string maPhong)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                string query;

                // Kiểm tra nếu maKhachHang có giá trị
                if (string.IsNullOrEmpty(maKhachHang))
                {
                    // Nếu maKhachHang rỗng, chỉ kiểm tra MaPhong và TinhTrang
                    query = "UPDATE DatPhong SET TinhTrang = N'Đã thanh toán' WHERE MaPhong = @MaPhong AND TinhTrang = N'Chưa thanh toán'";
                }
                else
                {
                    // Nếu maKhachHang có giá trị, thêm điều kiện cho MaKhachHang
                    query = "UPDATE DatPhong SET TinhTrang = N'Đã thanh toán' WHERE MaKhachHang = @MaKhachHang AND MaPhong = @MaPhong AND TinhTrang = N'Chưa thanh toán'";
                }

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Thêm tham số cho câu lệnh SQL
                    if (!string.IsNullOrEmpty(maKhachHang))
                    {
                        cmd.Parameters.AddWithValue("@MaKhachHang", maKhachHang);
                    }
                    cmd.Parameters.AddWithValue("@MaPhong", maPhong);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UpdateDonHangStatus(string maPhong)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                string query = "UPDATE DonHang SET TrangThai = 1 WHERE MaPhong = @MaPhong AND TrangThai = 0"; // Chỉ cập nhật trạng thái thành true cho các đơn hàng có trạng thái = 0
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaPhong", maPhong);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UpdateStockQuantity()
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();

                foreach (DataGridViewRow row in dgvDichVu_SanPham.Rows)
                {
                    if (row.Cells["Ten"].Value != null) // Kiểm tra xem ô tên không trống
                    {
                        string tenSanPham = row.Cells["Ten"].Value.ToString();
                        int soLuong = Convert.ToInt32(row.Cells["SoLuong"].Value);

                        // Lấy mã sản phẩm từ tên sản phẩm (bạn có thể cần điều chỉnh truy vấn nếu cần)
                        string maSanPham = GetMaSanPhamByTen(tenSanPham, conn);

                        if (!string.IsNullOrEmpty(maSanPham))
                        {
                            // Cập nhật số lượng tồn kho
                            string updateQuery = "UPDATE QuanLyKho SET SoLuongTon = SoLuongTon - @SoLuong WHERE MaSanPham = @MaSanPham";
                            using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@SoLuong", soLuong);
                                cmd.Parameters.AddWithValue("@MaSanPham", maSanPham);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
        }

        private string GetMaSanPhamByTen(string tenSanPham, SqlConnection conn)
        {
            string maSanPham = null;
            string query = "SELECT MaSanPham FROM SanPham WHERE TenSanPham = @TenSanPham";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@TenSanPham", tenSanPham);
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    maSanPham = result.ToString();
                }
            }
            return maSanPham;
        }

        private void CheckAndUpdateVIPStatus(string maKhachHang)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();

                // Kiểm tra số lượng hóa đơn
                string queryCountHoaDon = "SELECT COUNT(*) FROM HoaDon WHERE MaKhachHang = @MaKhachHang";
                using (SqlCommand cmdCountHoaDon = new SqlCommand(queryCountHoaDon, conn))
                {
                    cmdCountHoaDon.Parameters.AddWithValue("@MaKhachHang", maKhachHang);
                    int countHoaDon = (int)cmdCountHoaDon.ExecuteScalar();

                    // Kiểm tra số lượng đánh giá khách hàng
                    string queryCountDanhGia = "SELECT COUNT(*) FROM DanhGiaKhachHang WHERE MaKhachHang = @MaKhachHang";
                    using (SqlCommand cmdCountDanhGia = new SqlCommand(queryCountDanhGia, conn))
                    {
                        cmdCountDanhGia.Parameters.AddWithValue("@MaKhachHang", maKhachHang);
                        int countDanhGia = (int)cmdCountDanhGia.ExecuteScalar();

                        // Nếu có từ 10 hóa đơn và hơn 5 đánh giá, cập nhật khách hàng là VIP
                        if (countHoaDon >= 10 && countDanhGia >= 5)
                        {
                            string updateVIP = "UPDATE KhachHang SET LoaiKhachHang = 'VIP' WHERE MaKhachHang = @MaKhachHang";
                            using (SqlCommand cmdUpdate = new SqlCommand(updateVIP, conn))
                            {
                                cmdUpdate.Parameters.AddWithValue("@MaKhachHang", maKhachHang);
                                cmdUpdate.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
        }

        private void UpdateDiscount(string maKhachHang)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string query = @"
            SELECT km.PhanTramGiam
            FROM KhachHang kh
            JOIN KhuyenMai km ON km.TenKhuyenMai = N'Khuyến mãi khách hàng VIP'
            WHERE kh.MaKhachHang = @MaKhachHang AND kh.LoaiKhachHang = 'VIP'";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaKhachHang", maKhachHang);
                    var result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        decimal phanTramGiam = Convert.ToDecimal(result);
                        giamGia = tongTien * (phanTramGiam / 100);
                    }
                }
            }

            lblGiamGia.Text = giamGia.ToString("N0") + " VND"; // Cập nhật lại label giảm giá
            tongThanhToan = tongTien - giamGia;
            lblThanhToan.Text = tongThanhToan.ToString("N0") + " VND"; // Cập nhật lại số tiền phải thanh toán
        }

        private void SaveHoaDonToDatabase(string maHoaDon, string maKhachHang, string maPhong, DateTime ngayLapHoaDon, decimal tongTien, decimal thanhToan, decimal giamGia, string ghiChu)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                string query = "INSERT INTO HoaDon (MaHoaDon, MaKhachHang, MaPhong, NgayLapHoaDon, TongTien, ThanhToan, GiamGia, GhiChu, NgayTao, NgayCapNhat) " +
                               "VALUES (@MaHoaDon, @MaKhachHang, @MaPhong, @NgayLapHoaDon, @TongTien, @ThanhToan, @GiamGia, @GhiChu, GETDATE(), GETDATE())";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                // Sử dụng DBNull.Value nếu maKhachHang là null
                cmd.Parameters.AddWithValue("@MaKhachHang", string.IsNullOrEmpty(maKhachHang) ? DBNull.Value : (object)maKhachHang);
                cmd.Parameters.AddWithValue("@MaPhong", maPhong);
                cmd.Parameters.AddWithValue("@NgayLapHoaDon", ngayLapHoaDon);
                cmd.Parameters.AddWithValue("@TongTien", Convert.ToDecimal(tongTien));
                cmd.Parameters.AddWithValue("@ThanhToan", thanhToan.ToString("N0"));
                cmd.Parameters.AddWithValue("@GiamGia", Convert.ToDecimal(giamGia));
                cmd.Parameters.AddWithValue("@GhiChu", ghiChu);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }


        private bool IsKhachHangVIP(string tenKhachHang)
        {
            // Thực hiện truy vấn để kiểm tra loại khách hàng
            using (SqlConnection conn = new SqlConnection(connection))
            {
                string query = "SELECT LoaiKhachHang FROM KhachHang WHERE HoTen = @TenKhachHang";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TenKhachHang", tenKhachHang);
                conn.Open();

                // Thực hiện truy vấn và kiểm tra null
                var result = cmd.ExecuteScalar();
                conn.Close();

                // Nếu result là null, trả về false vì khách hàng không tồn tại
                if (result == null)
                {
                    return false;
                }

                string loaiKhachHang = result.ToString();
                return loaiKhachHang == "VIP";
            }
        }

        private string GenerateRandomCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private string GetMaKhachHangByTen(string tenKhachHang)
        {
            string maKhachHang = null;

            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string query = "SELECT MaKhachHang FROM KhachHang WHERE HoTen = @HoTen";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@HoTen", tenKhachHang);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        maKhachHang = result.ToString();
                    }
                }
            }
            return maKhachHang;
        }
        private Bitmap billImage; 
        private void ExportBillToPDF()
        {
            try
            {
                using (FolderBrowserDialog folderBrowser = new FolderBrowserDialog())
                {
                    folderBrowser.Description = "Chọn thư mục để lưu hóa đơn PDF";
                    folderBrowser.ShowNewFolderButton = true;

                    if (folderBrowser.ShowDialog() == DialogResult.OK)
                    {
                        string folderPath = folderBrowser.SelectedPath;

                        // Tạo tên file PDF
                        string fileName = "HoaDon_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".pdf";
                        string filePath = Path.Combine(folderPath, fileName);

                        // Tạo tài liệu PDF
                        Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                        PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
                        document.Open();

                        // Chụp nội dung GroupBox và lưu vào file ảnh Bitmap
                        billImage = CaptureGroupBox(grpHoaDon, 2.0f); // Tăng tỷ lệ để ảnh rõ hơn
                        AddImageToPDF(document, billImage);

                        // Đóng tài liệu
                        document.Close();

                        MessageBox.Show("Đã in hóa đơn ra file PDF", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi xuất hóa đơn: " + ex.Message + "\n" + ex.StackTrace, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            try
            {
                LoadbtnXuatHoaDon();
                if (billImage == null)
                {
                    MessageBox.Show("Vui lòng xuất ra file trước khi in", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Khởi tạo đối tượng PrintDocument
                PrintDocument printDocument = new PrintDocument();

                // Sự kiện để vẽ ảnh hóa đơn lên trang in
                printDocument.PrintPage += new PrintPageEventHandler(PrintBillImage);

                // Hiển thị hộp thoại để chọn máy in
                PrintDialog printDialog = new PrintDialog();
                printDialog.Document = printDocument;

                // Kiểm tra nếu người dùng chọn máy in và nhấn in
                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    // Cập nhật máy in được chọn
                    printDocument.PrinterSettings = printDialog.PrinterSettings;

                    // In hóa đơn ra giấy
                    printDocument.Print();

                    MessageBox.Show("Hóa đơn đã được in thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi in hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void PrintBillImage(object sender, PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;

            if (billImage != null)
            {
                // Tính toán kích thước ảnh sao cho phù hợp với trang in
                float x = 100;
                float y = 100;
                float width = e.PageBounds.Width - 2 * x;
                float height = e.PageBounds.Height - 2 * y;

                // Vẽ ảnh lên trang in
                graphics.DrawImage(billImage, x, y, width, height);

                // Đặt giá trị HasMorePages nếu muốn in nhiều trang (nếu có)
                e.HasMorePages = false;  // Chỉ có một trang trong ví dụ này
            }
        }


        /// <summary>
        /// Chụp nội dung của GroupBox với tỷ lệ tùy chỉnh để tăng độ rõ nét.
        /// </summary>
        private Bitmap CaptureGroupBox(GroupBox groupBox, float scale = 2.0f)
        {
            int width = (int)(groupBox.Width * scale);
            int height = (int)(groupBox.Height * scale);

            Bitmap bmp = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                g.ScaleTransform(scale, scale);

                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, groupBox.Width, groupBox.Height);
                groupBox.DrawToBitmap(bmp, rect);
            }
            return bmp;
        }

        /// <summary>
        /// Thêm ảnh Bitmap vào tài liệu PDF.
        /// </summary>
        private void AddImageToPDF(Document document, Bitmap bitmap)
        {
            using (var ms = new MemoryStream())
            {
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                iTextSharp.text.Image pdfImage = iTextSharp.text.Image.GetInstance(ms.ToArray());

                pdfImage.ScaleToFit(document.PageSize.Width - document.LeftMargin - document.RightMargin, document.PageSize.Height - document.TopMargin - document.BottomMargin);
                pdfImage.Alignment = Element.ALIGN_CENTER;

                document.Add(pdfImage);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
