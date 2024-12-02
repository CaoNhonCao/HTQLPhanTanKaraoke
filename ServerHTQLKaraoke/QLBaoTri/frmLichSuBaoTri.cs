using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServerHTQLKaraoke.QLBaoTri
{
    public partial class frmLichSuBaoTri : Form
    {
        string connection = ConfigurationManager.ConnectionStrings["ServerHTQLKaraoke.Properties.Settings.KaraokeConnectionString"].ConnectionString;
        public frmLichSuBaoTri()
        {
            InitializeComponent();
        }

        private void frmLichSuBaoTri_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            LoadThangComboBox();
            LoadChiNhanhComboBox();
            LoadLichSuBaoTri(DateTime.Now.Month);
        }
        private void SetupDataGridView()
        {
            dataGridViewLichSu.Columns.Clear();
            dataGridViewLichSu.Columns.Add("MaBaoTri", "Mã Bảo Trì");
            dataGridViewLichSu.Columns.Add("MaPhong", "Mã Phòng");
            dataGridViewLichSu.Columns.Add("SoPhong", "Số Phòng");
            dataGridViewLichSu.Columns.Add("LoaiPhong", "Loại Phòng");
            dataGridViewLichSu.Columns.Add("NgayBaoTri", "Ngày Bảo Trì");
            dataGridViewLichSu.Columns.Add("MoTaBaoTri", "Mô Tả");
            dataGridViewLichSu.Columns.Add("ChiPhiBaoTri", "Chi Phí Bảo Trì");
            dataGridViewLichSu.Columns.Add("TenChiNhanh", "Tên Chi Nhánh"); // Thêm cột Tên Chi Nhánh
        }

        private void LoadLichSuBaoTri(int month)
        {
            dataGridViewLichSu.Rows.Clear();

            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string query = @"
            SELECT L.MaBaoTri, L.MaPhong, P.SoPhong, P.LoaiPhong, L.NgayBaoTri, L.MoTaBaoTri, L.ChiPhiBaoTri, C.TenChiNhanh
            FROM LichSuBaoTri L
            INNER JOIN PhongHat P ON L.MaPhong = P.MaPhong
            INNER JOIN ChiNhanh C ON P.MaChiNhanh = C.MaChiNhanh
            WHERE MONTH(L.NgayBaoTri) = @Month 
              AND YEAR(L.NgayBaoTri) = YEAR(GETDATE())";

                // Kiểm tra nếu không chọn "Tất cả chi nhánh"
                if (comboBoxChiNhanh.SelectedIndex > 0)
                {
                    query += " AND C.TenChiNhanh = @TenChiNhanh";
                }

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Month", month);

                    // Nếu có bộ lọc chi nhánh, thêm tham số
                    if (comboBoxChiNhanh.SelectedIndex > 0)
                    {
                        cmd.Parameters.AddWithValue("@TenChiNhanh", comboBoxChiNhanh.SelectedItem.ToString());
                    }

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        dataGridViewLichSu.Rows.Add(
                            reader["MaBaoTri"].ToString(),
                            reader["MaPhong"].ToString(),
                            reader["SoPhong"].ToString(),
                            reader["LoaiPhong"].ToString(),
                            Convert.ToDateTime(reader["NgayBaoTri"]).ToString("dd/MM/yyyy"),
                            reader["MoTaBaoTri"] != DBNull.Value ? reader["MoTaBaoTri"].ToString() : "null",
                            reader["ChiPhiBaoTri"] != DBNull.Value ? reader["ChiPhiBaoTri"].ToString() : "null",
                            reader["TenChiNhanh"].ToString() // Thêm tên chi nhánh
                        );
                    }
                }
            }
            CalculateTotalCost();
        }

        private void comboBoxChiNhanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedMonth = Convert.ToInt32(comboBoxThang.SelectedItem); // Lấy tháng hiện tại
            LoadLichSuBaoTri(selectedMonth); // Tải lại dữ liệu với bộ lọc mới
        }

        private void LoadThangComboBox()
        {
            for (int i = 1; i <= 12; i++)
            {
                comboBoxThang.Items.Add(i.ToString());
            }
            comboBoxThang.SelectedIndex = DateTime.Now.Month - 1;
            comboBoxThang.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void LoadChiNhanhComboBox()
        {
            comboBoxChiNhanh.Items.Clear(); // Xóa các item cũ
            comboBoxChiNhanh.Items.Add("Tất cả"); // Thêm tùy chọn mặc định

            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string query = "SELECT TenChiNhanh FROM ChiNhanh"; // Truy vấn lấy tên chi nhánh
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        comboBoxChiNhanh.Items.Add(reader["TenChiNhanh"].ToString());
                    }
                }
            }
            comboBoxChiNhanh.SelectedIndex = 0; // Chọn mặc định là "Tất cả chi nhánh"
        }

        private void comboBoxThang_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedMonth = Convert.ToInt32(comboBoxThang.SelectedItem);
            LoadLichSuBaoTri(selectedMonth);
        }

        private void CalculateTotalCost()
        {
            decimal totalCost = 0; // Khởi tạo tổng chi phí

            foreach (DataGridViewRow row in dataGridViewLichSu.Rows)
            {
                // Kiểm tra xem ô có giá trị không null và không rỗng
                if (row.Cells["ChiPhiBaoTri"].Value != null && !string.IsNullOrEmpty(row.Cells["ChiPhiBaoTri"].Value.ToString()))
                {
                    string costString = row.Cells["ChiPhiBaoTri"].Value.ToString();

                    // Kiểm tra nếu giá trị không phải là ký tự "null" và có thể chuyển đổi thành decimal
                    if (costString.Trim().ToLower() != "null")
                    {
                        // Định nghĩa cost
                        decimal cost;
                        // Kiểm tra nếu có thể chuyển đổi
                        if (decimal.TryParse(costString, NumberStyles.Currency, CultureInfo.CurrentCulture, out cost))
                        {
                            totalCost += cost; // Nếu chuyển đổi thành công, cộng vào tổng chi phí
                        }
                    }
                }
            }

            // Hiển thị tổng chi phí vào txtTongTien
            txtTongTien.Text = totalCost.ToString("C0", CultureInfo.CurrentCulture); // Định dạng tiền
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadLichSuBaoTri(DateTime.Now.Month);
        }
    }
}
