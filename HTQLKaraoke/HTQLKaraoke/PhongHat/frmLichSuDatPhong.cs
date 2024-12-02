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

namespace HTQLKaraoke.PhongHat
{
    public partial class frmLichSuDatPhong : Form
    {
        string connection = ConfigurationManager.ConnectionStrings["HTQLKaraoke.Properties.Settings.KaraokeConnectionString"].ConnectionString;
        string loaiPhong;
        public frmLichSuDatPhong(string loaiPhong)
        {
            InitializeComponent();
            InitializeDataGridView();
            this.loaiPhong = loaiPhong;
            InitializeComboBoxMonths();
            LoadBookingHistoryByDate(DateTime.Now.Date);
        }
        private void InitializeComboBoxMonths()
        {
            cmbThang.Items.Clear(); // Xóa các mục cũ trong ComboBox
            for (int month = 1; month <= 12; month++)
            {
                cmbThang.Items.Add("Tháng " + month);
            }
            cmbThang.SelectedIndex = DateTime.Now.Month - 1; // Chọn tháng hiện tại
        }

        private void LoadBookingHistoryByMonth(int month)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();

                string query = @"
SELECT 
    ISNULL(k.MaKhachHang, 'null') AS [ID], 
    ISNULL(k.HoTen, N'Khách vãng lai') AS [Họ Tên], 
    ISNULL(k.SoDienThoai, 'null') AS [SDT],
    ISNULL(d.MaPhong, 'null') AS [Mã Phòng], 
    ISNULL(p.SoPhong, 'null') AS [Số Phòng],
    ISNULL(p.LoaiPhong, 'null') AS [Loại Phòng],
    ISNULL(k.LoaiKhachHang, 'null') AS [Loại Khách Hàng], 
    d.ThoiGianBatDau AS [Thời Gian Bắt Đầu], 
    d.ThoiGianKetThuc AS [Thời Gian Kết Thúc]
FROM 
    DatPhong d 
LEFT JOIN 
    KhachHang k ON d.MaKhachHang = k.MaKhachHang 
JOIN 
    PhongHat p ON d.MaPhong = p.MaPhong 
WHERE 
    MONTH(d.ThoiGianBatDau) = @Month
    AND YEAR(d.ThoiGianBatDau) = @Year
    AND d.TinhTrang = N'Đã thanh toán'
    AND p.LoaiPhong = @LoaiPhong";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Month", month);
                    cmd.Parameters.AddWithValue("@Year", DateTime.Now.Year);
                    cmd.Parameters.AddWithValue("@LoaiPhong", loaiPhong);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        dataGridViewHistory.Rows.Clear();

                        while (reader.Read())
                        {
                            string thoiGianBatDau = reader["Thời Gian Bắt Đầu"] == DBNull.Value
                                ? "null"
                                : Convert.ToDateTime(reader["Thời Gian Bắt Đầu"]).ToString("HH:mm:ss, dd/MM/yyyy");

                            string thoiGianKetThuc = reader["Thời Gian Kết Thúc"] == DBNull.Value
                                ? "null"
                                : Convert.ToDateTime(reader["Thời Gian Kết Thúc"]).ToString("HH:mm:ss, dd/MM/yyyy");

                            dataGridViewHistory.Rows.Add(
                                       reader["ID"].ToString(),
                                       reader["Họ Tên"].ToString(),
                                       reader["SDT"].ToString(),
                                       reader["Mã Phòng"].ToString(),
                                       reader["Số Phòng"].ToString(),
                                       reader["Loại Phòng"].ToString(),
                                       reader["Loại Khách Hàng"].ToString(),
                                       Convert.ToDateTime(reader["Thời Gian Bắt Đầu"]).ToString("HH:mm:ss, dd/MM/yyyy"),
                                       Convert.ToDateTime(reader["Thời Gian Kết Thúc"]).ToString("HH:mm:ss, dd/MM/yyyy")
                                   );
                        }

                        if (dataGridViewHistory.Rows.Count == 0)
                        {
                            MessageBox.Show($"Không có lịch sử đặt phòng nào trong tháng {month}.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
        }

        private void ShowSuggestions(string searchText)
        {
            listBoxSuggestions.Items.Clear(); // Xóa các gợi ý cũ
            string query = "SELECT SoDienThoai FROM KhachHang WHERE SoDienThoai LIKE @SearchText";

            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();

                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@SearchText", "%" + searchText + "%");
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string soDienThoai = reader["SoDienThoai"].ToString();
                        listBoxSuggestions.Items.Add(soDienThoai);
                    }

                    reader.Close();
                }
            }

            // Hiển thị gợi ý nếu có ít nhất một kết quả
            listBoxSuggestions.Visible = listBoxSuggestions.Items.Count > 0;
        }

        private void listBoxSuggestions_MouseClick(object sender, MouseEventArgs e)
        {
            if (listBoxSuggestions.SelectedItem != null)
            {
                txtTimKiem.Text = listBoxSuggestions.SelectedItem.ToString();
                listBoxSuggestions.Visible = false; // Ẩn gợi ý sau khi chọn
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtTimKiem.Text.Trim();

            // Hiển thị gợi ý chỉ khi có ít nhất 2 ký tự
            if (searchText.Length >= 2)
            {
                ShowSuggestions(searchText);
            }
            else
            {
                listBoxSuggestions.Visible = false; // Ẩn nếu không đủ ký tự
            }
        }

        private void txtTimKiem_Enter(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "Nhập vào số điện thoại...")
            {
                txtTimKiem.Text = ""; // Xóa placeholder
                txtTimKiem.ForeColor = Color.Black; // Đặt lại màu chữ
            }
        }

        private void listBoxSuggestions_MouseClick_1(object sender, MouseEventArgs e)
        {
            if (listBoxSuggestions.SelectedItem != null)
            {
                txtTimKiem.Text = listBoxSuggestions.SelectedItem.ToString();
                listBoxSuggestions.Visible = false; // Ẩn gợi ý sau khi chọn
            }
        }

        private void InitializeDataGridView()
        {
            // Đặt các thuộc tính cho DataGridView
            dataGridViewHistory.ColumnCount = 9;
            dataGridViewHistory.Columns[0].Name = "ID";
            dataGridViewHistory.Columns[1].Name = "Họ Tên";
            dataGridViewHistory.Columns[2].Name = "SDT";
            dataGridViewHistory.Columns[3].Name = "Mã Phòng";
            dataGridViewHistory.Columns[4].Name = "Số Phòng";
            dataGridViewHistory.Columns[5].Name = "Loại Phòng";
            dataGridViewHistory.Columns[6].Name = "Loại Khách Hàng";
            dataGridViewHistory.Columns[7].Name = "Thời Gian Bắt Đầu";
            dataGridViewHistory.Columns[7].Width = 150;
            dataGridViewHistory.Columns[8].Name = "Thời Gian Kết Thúc";
            dataGridViewHistory.Columns[8].Width = 150; 

            dataGridViewHistory.AllowUserToAddRows = false;
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string soDienThoai = txtTimKiem.Text.Trim();

            // Kiểm tra số điện thoại có hợp lệ không
            if (string.IsNullOrEmpty(soDienThoai) || soDienThoai == "Nhập vào số điện thoại...")
            {
                MessageBox.Show("Vui lòng nhập số điện thoại để tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kết nối tới cơ sở dữ liệu
            using (SqlConnection conn = new SqlConnection(connection))
            {
                try
                {
                    conn.Open();

                    // Câu truy vấn để lấy thông tin đặt phòng
                    string query = @"
            SELECT 
                k.MaKhachHang AS [ID], 
                k.HoTen AS [Họ Tên], 
                k.SoDienThoai AS [SDT],
                d.MaPhong AS [Mã Phòng], 
                p.SoPhong AS [Số Phòng],
                p.LoaiPhong AS [Loại Phòng], 
                k.LoaiKhachHang AS [Loại Khách Hàng], 
                d.ThoiGianBatDau AS [Thời Gian Bắt Đầu], 
                d.ThoiGianKetThuc AS [Thời Gian Kết Thúc]
            FROM 
                DatPhong d 
            JOIN 
                KhachHang k ON d.MaKhachHang = k.MaKhachHang 
            JOIN 
                PhongHat p ON d.MaPhong = p.MaPhong 
            WHERE 
                k.SoDienThoai = @SoDienThoai";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Thêm tham số bảo mật vào câu truy vấn
                        cmd.Parameters.AddWithValue("@SoDienThoai", soDienThoai);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Xóa các dòng trước khi thêm mới
                            dataGridViewHistory.Rows.Clear();

                            // Nếu có kết quả
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    // Thêm dòng mới vào DataGridView
                                    dataGridViewHistory.Rows.Add(
                                        reader["ID"].ToString(),
                                        reader["Họ Tên"].ToString(),
                                        reader["SDT"].ToString(),
                                        reader["Mã Phòng"].ToString(),
                                        reader["Số Phòng"].ToString(),
                                        reader["Loại Phòng"].ToString(),
                                        reader["Loại Khách Hàng"].ToString(),
                                        Convert.ToDateTime(reader["Thời Gian Bắt Đầu"]).ToString("HH:mm:ss, dd/MM/yyyy"),
                                        Convert.ToDateTime(reader["Thời Gian Kết Thúc"]).ToString("HH:mm:ss, dd/MM/yyyy")
                                    );
                                }
                            }
                            else
                            {
                                // Nếu không có kết quả
                                MessageBox.Show("Không tìm thấy lịch sử đặt phòng cho số điện thoại này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi nếu có
                    MessageBox.Show("Có lỗi xảy ra trong quá trình tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dtpLocTheoNgay_ValueChanged(object sender, EventArgs e)
        {
            LoadBookingHistoryByDate(dtpLoctheoNgay.Value.Date);
        }
        private void LoadBookingHistoryByDate(DateTime selectedDate)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();

                string query = @"
        SELECT 
            ISNULL(k.MaKhachHang, 'null') AS [ID], 
            ISNULL(k.HoTen, N'Khách vãng lai') AS [Họ Tên], 
            ISNULL(k.SoDienThoai, 'null') AS [SDT],
            ISNULL(d.MaPhong, 'null') AS [Mã Phòng], 
            ISNULL(p.SoPhong, 'null') AS [Số Phòng],
            ISNULL(p.LoaiPhong, 'null') AS [Loại Phòng],
            ISNULL(k.LoaiKhachHang, 'null') AS [Loại Khách Hàng], 
            d.ThoiGianBatDau AS [Thời Gian Bắt Đầu], 
            d.ThoiGianKetThuc AS [Thời Gian Kết Thúc]
        FROM 
            DatPhong d 
        LEFT JOIN 
            KhachHang k ON d.MaKhachHang = k.MaKhachHang 
        JOIN 
            PhongHat p ON d.MaPhong = p.MaPhong 
        WHERE 
            CAST(d.ThoiGianBatDau AS DATE) = @SelectedDate
            AND d.TinhTrang = N'Đã thanh toán'
            And p.LoaiPhong = @LoaiPhong";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SelectedDate", selectedDate);  // Thêm tham số ngày chọn
                    cmd.Parameters.AddWithValue("@LoaiPhong", loaiPhong);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        dataGridViewHistory.Rows.Clear();

                        while (reader.Read())
                        {
                            string thoiGianBatDau = reader["Thời Gian Bắt Đầu"] == DBNull.Value
                                ? "null"
                                : Convert.ToDateTime(reader["Thời Gian Bắt Đầu"]).ToString("HH:mm:ss, dd/MM/yyyy");

                            string thoiGianKetThuc = reader["Thời Gian Kết Thúc"] == DBNull.Value
                                ? "null"
                                : Convert.ToDateTime(reader["Thời Gian Kết Thúc"]).ToString("HH:mm:ss, dd/MM/yyyy");

                            dataGridViewHistory.Rows.Add(
                                reader["ID"].ToString(),
                                reader["Họ Tên"].ToString(),
                                reader["SDT"].ToString(),
                                reader["Mã Phòng"].ToString(),
                                reader["Số Phòng"].ToString(),
                                reader["Loại Phòng"].ToString(),
                                reader["Loại Khách Hàng"].ToString(),
                                thoiGianBatDau,
                                thoiGianKetThuc
                            );
                        }
                    }
                }
            }
        }

        private void cmbThang_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedMonth = cmbThang.SelectedIndex + 1;
            LoadBookingHistoryByMonth(selectedMonth);
        }
    }
}
