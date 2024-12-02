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
using System.Globalization;

namespace HTQLKaraoke.BaoTri
{
    public partial class frmLichSuBaoTri : Form
    {
        string connection = ConfigurationManager.ConnectionStrings["HTQLKaraoke.Properties.Settings.KaraokeConnectionString"].ConnectionString;
        public frmLichSuBaoTri()
        {
            InitializeComponent();
        }

        private void frmLichSuBaoTri_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            LoadThangComboBox();
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
            dataGridViewLichSu.AllowUserToAddRows = false;
        }

        private void LoadLichSuBaoTri(int month)
        {
            dataGridViewLichSu.Rows.Clear();

            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string query = @"
                SELECT L.MaBaoTri, L.MaPhong, P.SoPhong, P.LoaiPhong, L.NgayBaoTri, L.MoTaBaoTri AS MoTaBaoTri, L.ChiPhiBaoTri
                FROM LichSuBaoTri L
                INNER JOIN PhongHat P ON L.MaPhong = P.MaPhong
                WHERE MONTH(L.NgayBaoTri) = @Month AND YEAR(L.NgayBaoTri) = YEAR(GETDATE())";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Month", month);
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
                            reader["ChiPhiBaoTri"] != DBNull.Value ? reader["ChiPhiBaoTri"].ToString() : "null"
                        );
                    }
                }
            }
            CalculateTotalCost();
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

        private void comboBoxThang_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedMonth = Convert.ToInt32(comboBoxThang.SelectedItem);
            LoadLichSuBaoTri(selectedMonth);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu có hàng hiện tại và ô "MaBaoTri" không phải là null hoặc rỗng
            if (dataGridViewLichSu.CurrentRow != null &&
                dataGridViewLichSu.CurrentRow.Cells["MaBaoTri"].Value != null &&
                !string.IsNullOrWhiteSpace(dataGridViewLichSu.CurrentRow.Cells["MaBaoTri"].Value.ToString()))
            {
                string maBaoTri = dataGridViewLichSu.CurrentRow.Cells["MaBaoTri"].Value.ToString();

                // Tạo form chỉnh sửa và hiển thị
                frmEditBaoTri editForm = new frmEditBaoTri(maBaoTri);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    LoadLichSuBaoTri(DateTime.Now.Month);
                }
            }
            else
            {
                // Thông báo khi không có thông tin bảo trì
                MessageBox.Show("Vui lòng chọn thông tin bảo trì cần chỉnh sửa.");
            }
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

        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu có hàng hiện tại và ô "MaBaoTri" không phải là null hoặc rỗng
            if (dataGridViewLichSu.CurrentRow != null &&
                dataGridViewLichSu.CurrentRow.Cells["MaBaoTri"].Value != null &&
                !string.IsNullOrWhiteSpace(dataGridViewLichSu.CurrentRow.Cells["MaBaoTri"].Value.ToString()))
            {
                string maBaoTri = dataGridViewLichSu.CurrentRow.Cells["MaBaoTri"].Value.ToString();

                // Xác nhận xóa
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa bảo trì này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(connection))
                    {
                        conn.Open();
                        string query = "DELETE FROM LichSuBaoTri WHERE MaBaoTri = @MaBaoTri";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@MaBaoTri", maBaoTri);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    // Tải lại dữ liệu
                    LoadLichSuBaoTri(DateTime.Now.Month);
                    // Thông báo xóa thành công
                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                // Thông báo khi không có mục bảo trì nào được chọn
                MessageBox.Show("Vui lòng chọn thông tin bảo trì cần xóa.");
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
