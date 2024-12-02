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

namespace HTQLKaraoke.DanhGia
{
    public partial class frmTTDanhGia : Form
    {
        string connection = ConfigurationManager.ConnectionStrings["HTQLKaraoke.Properties.Settings.KaraokeConnectionString"].ConnectionString;
        public frmTTDanhGia()
        {
            InitializeComponent();
            LoadLocDiem();
        }

        private void LoadDanhGia()
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                // Câu truy vấn SQL để lấy dữ liệu
                string query = @"
            SELECT 
                KhachHang.HoTen, 
                PhongHat.SoPhong, 
                PhongHat.LoaiPhong, 
                DanhGiaKhachHang.DiemDanhGia, 
                DanhGiaKhachHang.NgayDanhGia, 
                DanhGiaKhachHang.NoiDung
            FROM 
                DanhGiaKhachHang
            JOIN 
                KhachHang ON DanhGiaKhachHang.MaKhachHang = KhachHang.MaKhachHang
            JOIN 
                PhongHat ON DanhGiaKhachHang.MaPhong = PhongHat.MaPhong";

                // Sử dụng SqlDataAdapter để fill dữ liệu vào DataTable
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                // Đặt DataSource cho DataGridView
                dtgDanhGia.DataSource = dataTable;

                // Thiết lập các header bằng tiếng Việt
                dtgDanhGia.Columns["HoTen"].HeaderText = "Họ Tên";
                dtgDanhGia.Columns["SoPhong"].HeaderText = "Số Phòng";
                dtgDanhGia.Columns["LoaiPhong"].HeaderText = "Loại Phòng";
                dtgDanhGia.Columns["DiemDanhGia"].HeaderText = "Điểm Đánh Giá";
                dtgDanhGia.Columns["NgayDanhGia"].HeaderText = "Ngày Đánh Giá";
                dtgDanhGia.Columns["NoiDung"].HeaderText = "Nội Dung";
                dtgDanhGia.AllowUserToAddRows = false;
            }
        }

        private void frmTTDanhGia_Load(object sender, EventArgs e)
        {
            LoadDanhGia();
            LoadPhongVaLoaiPhong();
            EnableFilterButton();
        }
        private void LoadLocDiem()
        {
            cbxLocDiem.Items.Clear(); // Xóa các mục cũ trong ComboBox

            // Thêm các mục lọc điểm vào ComboBox
            cbxLocDiem.Items.Add("Lọc điểm tăng dần");
            cbxLocDiem.Items.Add("Lọc điểm giảm dần");
            cbxLocDiem.Items.Add("Lọc điểm dưới 3");
            cbxLocDiem.Items.Add("Lọc điểm trên 3");
            cbxLocDiem.Items.Add("Top 10 phòng có điểm thấp nhất");
            cbxLocDiem.Items.Add("Top 10 phòng có điểm cao nhất");
            cbxLocDiem.Items.Add("10 phòng có điểm thấp nhất tháng");
            cbxLocDiem.Items.Add("10 phòng có điểm cao nhất tháng");

            // Chọn mặc định một mục (có thể chọn cái đầu tiên hoặc bỏ trống)
            cbxLocDiem.SelectedIndex = 0;
        }        
        private void cbxLocDiem_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedOption = cbxLocDiem.SelectedItem.ToString();

            using (SqlConnection conn = new SqlConnection(connection))
            {
                // Mở kết nối
                conn.Open();

                // Câu truy vấn SQL theo từng lựa chọn
                string query = "";

                switch (selectedOption)
                {
                    case "Lọc điểm tăng dần":
                        query = @"
                    SELECT 
                        KhachHang.HoTen, 
                        PhongHat.SoPhong, 
                        PhongHat.LoaiPhong, 
                        DanhGiaKhachHang.DiemDanhGia, 
                        DanhGiaKhachHang.NgayDanhGia, 
                        DanhGiaKhachHang.NoiDung
                    FROM 
                        DanhGiaKhachHang
                    JOIN 
                        KhachHang ON DanhGiaKhachHang.MaKhachHang = KhachHang.MaKhachHang
                    JOIN 
                        PhongHat ON DanhGiaKhachHang.MaPhong = PhongHat.MaPhong
                    ORDER BY 
                        DanhGiaKhachHang.DiemDanhGia ASC";
                        break;

                    case "Lọc điểm giảm dần":
                        query = @"
                    SELECT 
                        KhachHang.HoTen, 
                        PhongHat.SoPhong, 
                        PhongHat.LoaiPhong, 
                        DanhGiaKhachHang.DiemDanhGia, 
                        DanhGiaKhachHang.NgayDanhGia, 
                        DanhGiaKhachHang.NoiDung
                    FROM 
                        DanhGiaKhachHang
                    JOIN 
                        KhachHang ON DanhGiaKhachHang.MaKhachHang = KhachHang.MaKhachHang
                    JOIN 
                        PhongHat ON DanhGiaKhachHang.MaPhong = PhongHat.MaPhong
                    ORDER BY 
                        DanhGiaKhachHang.DiemDanhGia DESC";
                        break;

                    case "Lọc điểm dưới 3":
                        query = @"
                    SELECT 
                        KhachHang.HoTen, 
                        PhongHat.SoPhong, 
                        PhongHat.LoaiPhong, 
                        DanhGiaKhachHang.DiemDanhGia, 
                        DanhGiaKhachHang.NgayDanhGia, 
                        DanhGiaKhachHang.NoiDung
                    FROM 
                        DanhGiaKhachHang
                    JOIN 
                        KhachHang ON DanhGiaKhachHang.MaKhachHang = KhachHang.MaKhachHang
                    JOIN 
                        PhongHat ON DanhGiaKhachHang.MaPhong = PhongHat.MaPhong
                    WHERE 
                        DanhGiaKhachHang.DiemDanhGia < 3";
                        break;

                    case "Lọc điểm trên 3":
                        query = @"
                    SELECT 
                        KhachHang.HoTen, 
                        PhongHat.SoPhong, 
                        PhongHat.LoaiPhong, 
                        DanhGiaKhachHang.DiemDanhGia, 
                        DanhGiaKhachHang.NgayDanhGia, 
                        DanhGiaKhachHang.NoiDung
                    FROM 
                        DanhGiaKhachHang
                    JOIN 
                        KhachHang ON DanhGiaKhachHang.MaKhachHang = KhachHang.MaKhachHang
                    JOIN 
                        PhongHat ON DanhGiaKhachHang.MaPhong = PhongHat.MaPhong
                    WHERE 
                        DanhGiaKhachHang.DiemDanhGia > 3";
                        break;

                    case "Top 10 phòng có điểm thấp nhất":
                        query = @"
                    SELECT TOP 10
                        KhachHang.HoTen, 
                        PhongHat.SoPhong, 
                        PhongHat.LoaiPhong, 
                        DanhGiaKhachHang.DiemDanhGia, 
                        DanhGiaKhachHang.NgayDanhGia, 
                        DanhGiaKhachHang.NoiDung
                    FROM 
                        DanhGiaKhachHang
                    JOIN 
                        KhachHang ON DanhGiaKhachHang.MaKhachHang = KhachHang.MaKhachHang
                    JOIN 
                        PhongHat ON DanhGiaKhachHang.MaPhong = PhongHat.MaPhong
                    ORDER BY 
                        DanhGiaKhachHang.DiemDanhGia ASC";
                        break;

                    case "Top 10 phòng có điểm cao nhất":
                        query = @"
                    SELECT TOP 10
                        KhachHang.HoTen, 
                        PhongHat.SoPhong, 
                        PhongHat.LoaiPhong, 
                        DanhGiaKhachHang.DiemDanhGia, 
                        DanhGiaKhachHang.NgayDanhGia, 
                        DanhGiaKhachHang.NoiDung
                    FROM 
                        DanhGiaKhachHang
                    JOIN 
                        KhachHang ON DanhGiaKhachHang.MaKhachHang = KhachHang.MaKhachHang
                    JOIN 
                        PhongHat ON DanhGiaKhachHang.MaPhong = PhongHat.MaPhong
                    ORDER BY 
                        DanhGiaKhachHang.DiemDanhGia DESC";
                        break;

                    case "10 phòng có điểm thấp nhất tháng":
                        query = @"
                    SELECT TOP 10
                        KhachHang.HoTen, 
                        PhongHat.SoPhong, 
                        PhongHat.LoaiPhong, 
                        DanhGiaKhachHang.DiemDanhGia, 
                        DanhGiaKhachHang.NgayDanhGia, 
                        DanhGiaKhachHang.NoiDung
                    FROM 
                        DanhGiaKhachHang
                    JOIN 
                        KhachHang ON DanhGiaKhachHang.MaKhachHang = KhachHang.MaKhachHang
                    JOIN 
                        PhongHat ON DanhGiaKhachHang.MaPhong = PhongHat.MaPhong
                    WHERE 
                        MONTH(DanhGiaKhachHang.NgayDanhGia) = MONTH(GETDATE()) 
                    ORDER BY 
                        DanhGiaKhachHang.DiemDanhGia ASC";
                        break;

                    case "10 phòng có điểm cao nhất tháng":
                        query = @"
                    SELECT TOP 10
                        KhachHang.HoTen, 
                        PhongHat.SoPhong, 
                        PhongHat.LoaiPhong, 
                        DanhGiaKhachHang.DiemDanhGia, 
                        DanhGiaKhachHang.NgayDanhGia, 
                        DanhGiaKhachHang.NoiDung
                    FROM 
                        DanhGiaKhachHang
                    JOIN 
                        KhachHang ON DanhGiaKhachHang.MaKhachHang = KhachHang.MaKhachHang
                    JOIN 
                        PhongHat ON DanhGiaKhachHang.MaPhong = PhongHat.MaPhong
                    WHERE 
                        MONTH(DanhGiaKhachHang.NgayDanhGia) = MONTH(GETDATE()) 
                    ORDER BY 
                        DanhGiaKhachHang.DiemDanhGia DESC";
                        break;
                }

                // Sử dụng SqlDataAdapter để load dữ liệu vào DataGridView
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                // Cập nhật dữ liệu cho DataGridView
                dtgDanhGia.DataSource = dataTable;

                // Cập nhật tiêu đề cho các cột trong DataGridView
                dtgDanhGia.Columns["HoTen"].HeaderText = "Họ Tên";
                dtgDanhGia.Columns["SoPhong"].HeaderText = "Số Phòng";
                dtgDanhGia.Columns["LoaiPhong"].HeaderText = "Loại Phòng";
                dtgDanhGia.Columns["DiemDanhGia"].HeaderText = "Điểm Đánh Giá";
                dtgDanhGia.Columns["NgayDanhGia"].HeaderText = "Ngày Đánh Giá";
                dtgDanhGia.Columns["NoiDung"].HeaderText = "Nội Dung";
            }
        }

        private void LoadPhongVaLoaiPhong()
        {
            // Load số phòng vào ComboBox cbxLocTheoPhong
            string queryPhong = "SELECT DISTINCT SoPhong FROM PhongHat";
            using (SqlConnection conn = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand(queryPhong, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                cbxLocTheoPhong.Items.Clear();
                while (reader.Read())
                {
                    cbxLocTheoPhong.Items.Add(reader["SoPhong"].ToString());
                }
            }

            // Load loại phòng vào ComboBox cbxLocTheoLoaiPhong
            string queryLoaiPhong = "SELECT DISTINCT LoaiPhong FROM PhongHat";
            using (SqlConnection conn = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand(queryLoaiPhong, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                cbxLocTheoLoaiPhong.Items.Clear();
                while (reader.Read())
                {
                    cbxLocTheoLoaiPhong.Items.Add(reader["LoaiPhong"].ToString());
                }
            }

            // Disable cbxLocTheoLoaiPhong ban đầu
            cbxLocTheoLoaiPhong.Enabled = false;
        }

        private void cbxLocTheoPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kiểm tra nếu có phòng được chọn
            if (cbxLocTheoPhong.SelectedIndex != -1)
            {
                // Enable cbxLocTheoLoaiPhong khi chọn phòng
                cbxLocTheoLoaiPhong.Enabled = true;
            }
            else
            {
                // Nếu không chọn phòng thì disable cbxLocTheoLoaiPhong
                cbxLocTheoLoaiPhong.Enabled = false;
            }

            // Kiểm tra nếu cả 2 combobox đều có lựa chọn thì enable nút lọc
            EnableFilterButton();
        }

        private void cbxLocTheoLoaiPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kiểm tra nếu cả 2 combobox đều có lựa chọn thì enable nút lọc
            EnableFilterButton();
        }
        private void EnableFilterButton()
        {
            // Kiểm tra xem cả 2 combobox đều có lựa chọn
            if (cbxLocTheoPhong.SelectedIndex != -1 && cbxLocTheoLoaiPhong.SelectedIndex != -1)
            {
                btnLoc.Enabled = true;  // Kích hoạt nút lọc
            }
            else
            {
                btnLoc.Enabled = false; // Tắt nút lọc nếu chưa chọn đủ
            }
        }


        private void btnLoc_Click(object sender, EventArgs e)
        {
            if (cbxLocTheoPhong.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn phòng.");
                return;
            }

            if (cbxLocTheoLoaiPhong.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn loại phòng.");
                return;
            }

            string selectedRoom = cbxLocTheoPhong.SelectedItem.ToString();
            string selectedRoomType = cbxLocTheoLoaiPhong.SelectedItem.ToString();

            // Lấy tổng điểm trung bình của phòng
            string query = @"SELECT AVG(DG.DiemDanhGia) AS DiemTrungBinh
                     FROM DanhGiaKhachHang DG
                     INNER JOIN PhongHat PH ON DG.MaPhong = PH.MaPhong
                     WHERE PH.SoPhong = @SoPhong AND PH.LoaiPhong = @LoaiPhong";

            using (SqlConnection conn = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SoPhong", selectedRoom);
                cmd.Parameters.AddWithValue("@LoaiPhong", selectedRoomType);
                conn.Open();

                object result = cmd.ExecuteScalar();
                if (result != DBNull.Value && result != null)
                {
                    double averageScore = Convert.ToDouble(result);
                    txtTongDiem.Text = averageScore.ToString();
                }
                else
                {
                    txtTongDiem.Text = "Không có dữ liệu";
                }
            }
        }
    }
}
