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

namespace ServerHTQLKaraoke.DanhGia
{
    public partial class frmTTDanhGia : Form
    {
        string connection = ConfigurationManager.ConnectionStrings["ServerHTQLKaraoke.Properties.Settings.KaraokeConnectionString"].ConnectionString;
        public frmTTDanhGia()
        {
            InitializeComponent();
            LoadLocDiem();
        }
        private void LoadLocDiem()
        {
            cbxLocDiem.Items.Clear(); 

            cbxLocDiem.Items.Add("Lọc điểm tăng dần");
            cbxLocDiem.Items.Add("Lọc điểm giảm dần");
            cbxLocDiem.Items.Add("Lọc điểm dưới 3");
            cbxLocDiem.Items.Add("Lọc điểm trên 3");
            cbxLocDiem.Items.Add("Top 10 phòng có điểm thấp nhất");
            cbxLocDiem.Items.Add("Top 10 phòng có điểm cao nhất");
            cbxLocDiem.Items.Add("10 phòng có điểm thấp nhất tháng");
            cbxLocDiem.Items.Add("10 phòng có điểm cao nhất tháng");

            cbxLocDiem.SelectedIndex = 0;
        }

        private void frmTTDanhGia_Load(object sender, EventArgs e)
        {
            LoadChiNhanh();
            LoadDanhGia();
            LoadPhongVaLoaiPhong();
            EnableFilterButton();
        }
        private void LoadChiNhanh()
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string query = "SELECT DISTINCT TenChiNhanh FROM ChiNhanh"; // Lấy danh sách chi nhánh
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                cbxChiNhanh.Items.Clear();
                cbxChiNhanh.Items.Add("Tất cả"); // Thêm tùy chọn "Tất cả"
                while (reader.Read())
                {
                    cbxChiNhanh.Items.Add(reader["TenChiNhanh"].ToString());
                }
                reader.Close();
                cbxChiNhanh.SelectedIndex = 0; // Mặc định chọn "Tất cả"
            }
        }

        private void LoadDanhGia(string chiNhanh = null)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                string query = @"
                SELECT 
                    KhachHang.HoTen, 
                    PhongHat.SoPhong, 
                    PhongHat.LoaiPhong, 
                    DanhGiaKhachHang.DiemDanhGia, 
                    DanhGiaKhachHang.NgayDanhGia, 
                    DanhGiaKhachHang.NoiDung, 
                    ChiNhanh.TenChiNhanh
                FROM 
                    DanhGiaKhachHang
                JOIN 
                    KhachHang ON DanhGiaKhachHang.MaKhachHang = KhachHang.MaKhachHang
                JOIN 
                    PhongHat ON DanhGiaKhachHang.MaPhong = PhongHat.MaPhong
                JOIN 
                    ChiNhanh ON PhongHat.MaChiNhanh = ChiNhanh.MaChiNhanh";

                if (!string.IsNullOrEmpty(chiNhanh) && chiNhanh != "Tất cả")
                {
                    query += " WHERE ChiNhanh.TenChiNhanh = @TenChiNhanh";
                }

                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                if (!string.IsNullOrEmpty(chiNhanh) && chiNhanh != "Tất cả")
                {
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@TenChiNhanh", chiNhanh);
                }

                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                dtgDanhGia.DataSource = dataTable;

                dtgDanhGia.Columns["HoTen"].HeaderText = "Họ Tên";
                dtgDanhGia.Columns["SoPhong"].HeaderText = "Số Phòng";
                dtgDanhGia.Columns["LoaiPhong"].HeaderText = "Loại Phòng";
                dtgDanhGia.Columns["DiemDanhGia"].HeaderText = "Điểm Đánh Giá";
                dtgDanhGia.Columns["NgayDanhGia"].HeaderText = "Ngày Đánh Giá";
                dtgDanhGia.Columns["NoiDung"].HeaderText = "Nội Dung";
                dtgDanhGia.Columns["TenChiNhanh"].HeaderText = "Chi Nhánh";
            }
        }

        private void cbxChiNhanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedChiNhanh = cbxChiNhanh.SelectedItem.ToString();
            LoadDanhGia(selectedChiNhanh); 
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
                DanhGiaKhachHang.NoiDung,
                ChiNhanh.TenChiNhanh
            FROM 
                DanhGiaKhachHang
            JOIN 
                KhachHang ON DanhGiaKhachHang.MaKhachHang = KhachHang.MaKhachHang
            JOIN 
                PhongHat ON DanhGiaKhachHang.MaPhong = PhongHat.MaPhong
            JOIN
                ChiNhanh ON PhongHat.MaChiNhanh = ChiNhanh.MaChiNhanh
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
                DanhGiaKhachHang.NoiDung,
                ChiNhanh.TenChiNhanh
            FROM 
                DanhGiaKhachHang
            JOIN 
                KhachHang ON DanhGiaKhachHang.MaKhachHang = KhachHang.MaKhachHang
            JOIN 
                PhongHat ON DanhGiaKhachHang.MaPhong = PhongHat.MaPhong
            JOIN
                ChiNhanh ON PhongHat.MaChiNhanh = ChiNhanh.MaChiNhanh
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
                DanhGiaKhachHang.NoiDung,
                ChiNhanh.TenChiNhanh
            FROM 
                DanhGiaKhachHang
            JOIN 
                KhachHang ON DanhGiaKhachHang.MaKhachHang = KhachHang.MaKhachHang
            JOIN 
                PhongHat ON DanhGiaKhachHang.MaPhong = PhongHat.MaPhong
            JOIN
                ChiNhanh ON PhongHat.MaChiNhanh = ChiNhanh.MaChiNhanh
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
                DanhGiaKhachHang.NoiDung,
                ChiNhanh.TenChiNhanh
            FROM 
                DanhGiaKhachHang
            JOIN 
                KhachHang ON DanhGiaKhachHang.MaKhachHang = KhachHang.MaKhachHang
            JOIN 
                PhongHat ON DanhGiaKhachHang.MaPhong = PhongHat.MaPhong
            JOIN
                ChiNhanh ON PhongHat.MaChiNhanh = ChiNhanh.MaChiNhanh
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
                DanhGiaKhachHang.NoiDung,
                ChiNhanh.TenChiNhanh
            FROM 
                DanhGiaKhachHang
            JOIN 
                KhachHang ON DanhGiaKhachHang.MaKhachHang = KhachHang.MaKhachHang
            JOIN 
                PhongHat ON DanhGiaKhachHang.MaPhong = PhongHat.MaPhong
            JOIN
                ChiNhanh ON PhongHat.MaChiNhanh = ChiNhanh.MaChiNhanh
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
                DanhGiaKhachHang.NoiDung,
                ChiNhanh.TenChiNhanh
            FROM 
                DanhGiaKhachHang
            JOIN 
                KhachHang ON DanhGiaKhachHang.MaKhachHang = KhachHang.MaKhachHang
            JOIN 
                PhongHat ON DanhGiaKhachHang.MaPhong = PhongHat.MaPhong
            JOIN
                ChiNhanh ON PhongHat.MaChiNhanh = ChiNhanh.MaChiNhanh
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
                DanhGiaKhachHang.NoiDung,
                ChiNhanh.TenChiNhanh
            FROM 
                DanhGiaKhachHang
            JOIN 
                KhachHang ON DanhGiaKhachHang.MaKhachHang = KhachHang.MaKhachHang
            JOIN 
                PhongHat ON DanhGiaKhachHang.MaPhong = PhongHat.MaPhong
            JOIN
                ChiNhanh ON PhongHat.MaChiNhanh = ChiNhanh.MaChiNhanh
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
                DanhGiaKhachHang.NoiDung,
                ChiNhanh.TenChiNhanh
            FROM 
                DanhGiaKhachHang
            JOIN 
                KhachHang ON DanhGiaKhachHang.MaKhachHang = KhachHang.MaKhachHang
            JOIN 
                PhongHat ON DanhGiaKhachHang.MaPhong = PhongHat.MaPhong
            JOIN
                ChiNhanh ON PhongHat.MaChiNhanh = ChiNhanh.MaChiNhanh
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
                dtgDanhGia.Columns["TenChiNhanh"].HeaderText = "Tên Chi Nhánh"; // Thêm tên chi nhánh vào DataGridView
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
            EnableFilterButton();
        }
        private void EnableFilterButton()
        {
            if (cbxLocTheoPhong.SelectedIndex != -1 && cbxLocTheoLoaiPhong.SelectedIndex != -1)
            {
                btnLoc.Enabled = true;  
            }
            else
            {
                btnLoc.Enabled = false; 
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDanhGia();
        }
    }
}
