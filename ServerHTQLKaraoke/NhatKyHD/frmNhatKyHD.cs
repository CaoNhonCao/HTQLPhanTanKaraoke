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

namespace ServerHTQLKaraoke.NhatKyHD
{
    public partial class frmNhatKyHD : Form
    {
        string connection = ConfigurationManager.ConnectionStrings["ServerHTQLKaraoke.Properties.Settings.KaraokeConnectionString"].ConnectionString;
        private string selectedMaHoatDong = null;
        private string selectedTenChiNhanh = null;
        public frmNhatKyHD()
        {
            InitializeComponent();
        }
        private void frmNhatKyHD_Load(object sender, EventArgs e)
        {
            LoadChiNhanh();
            LoadData();
        }

        private void LoadData(string chiNhanh = null)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                try
                {
                    conn.Open();

                    // Truy vấn lấy dữ liệu từ bảng HoatDongHeThong và join với bảng ChiNhanh
                    string query = @"
                SELECT 
                    hd.MaHoatDong,
                    hd.TenNhanVien,
                    hd.MoTaHoatDong,
                    hd.NgayThucHien,
                    cn.TenChiNhanh
                FROM 
                    HoatDongHeThong hd
                INNER JOIN 
                    ChiNhanh cn ON hd.MaChiNhanh = cn.MaChiNhanh";

                    // Nếu có chi nhánh, thêm điều kiện lọc
                    if (!string.IsNullOrEmpty(chiNhanh))
                    {
                        query += " WHERE cn.TenChiNhanh = @TenChiNhanh";
                    }

                    query += " ORDER BY hd.NgayThucHien DESC";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    if (!string.IsNullOrEmpty(chiNhanh))
                    {
                        cmd.Parameters.AddWithValue("@TenChiNhanh", chiNhanh);
                    }

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Gán dữ liệu cho DataGridView
                    dtgNhatKy.DataSource = dt;

                    // Đặt header là tiếng Việt
                    dtgNhatKy.Columns["MaHoatDong"].HeaderText = "Mã Hoạt Động";
                    dtgNhatKy.Columns["TenNhanVien"].HeaderText = "Tên Nhân Viên";
                    dtgNhatKy.Columns["MoTaHoatDong"].HeaderText = "Mô Tả Hoạt Động";
                    dtgNhatKy.Columns["NgayThucHien"].HeaderText = "Ngày Thực Hiện";
                    dtgNhatKy.Columns["TenChiNhanh"].HeaderText = "Tên Chi Nhánh";

                    // Định dạng cột "Mô Tả Hoạt Động"
                    dtgNhatKy.Columns["MoTaHoatDong"].Width = 220;

                    // Đưa cột "Mô Tả Hoạt Động" về cuối cùng
                    dtgNhatKy.Columns["MoTaHoatDong"].DisplayIndex = dtgNhatKy.Columns.Count - 1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void LoadChiNhanh()
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT TenChiNhanh FROM ChiNhanh";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    // Thêm tùy chọn "Tất cả chi nhánh"
                    cbxChiNhanh.Items.Clear();
                    cbxChiNhanh.Items.Add("Tất cả chi nhánh");

                    while (reader.Read())
                    {
                        cbxChiNhanh.Items.Add(reader["TenChiNhanh"].ToString());
                    }

                    // Chọn mặc định là "Tất cả chi nhánh"
                    cbxChiNhanh.SelectedIndex = 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải danh sách chi nhánh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void cbxChiNhanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Nếu chọn "Tất cả chi nhánh", tải lại toàn bộ dữ liệu
            if (cbxChiNhanh.SelectedIndex == 0)
            {
                LoadData();
            }
            else
            {
                // Lấy tên chi nhánh được chọn và tải dữ liệu
                string selectedChiNhanh = cbxChiNhanh.SelectedItem.ToString();
                LoadData(selectedChiNhanh);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            frmThemHoatDong frm = new frmThemHoatDong();
            frm.FormClosed += (s, args) =>
            {
                LoadData();
            };
            frm.ShowDialog();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (selectedMaHoatDong == null)
            {
                MessageBox.Show("Vui lòng chọn một hoạt động để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Hỏi xác nhận trước khi xóa
            DialogResult confirmResult = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa hoạt động này không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connection))
                    {
                        conn.Open();

                        string query = "DELETE FROM HoatDongHeThong WHERE MaHoatDong = @MaHoatDong";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@MaHoatDong", selectedMaHoatDong);
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Xóa hoạt động thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadData(); // Tải lại dữ liệu sau khi xóa
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy hoạt động cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa hoạt động: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void dtgNhatKy_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra nếu nhấp vào dòng cuối cùng hoặc cột ngoài vùng dữ liệu
            if (e.RowIndex == dtgNhatKy.NewRowIndex || e.RowIndex < 0)
            {
                selectedMaHoatDong = null; // Đặt lại giá trị
                MessageBox.Show("Vui lòng chọn một hoạt động hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy giá trị của MaHoatDong từ dòng được chọn
            selectedMaHoatDong = dtgNhatKy.Rows[e.RowIndex].Cells["MaHoatDong"].Value.ToString();
            selectedTenChiNhanh = dtgNhatKy.Rows[e.RowIndex].Cells["TenChiNhanh"].Value.ToString();

            // Kiểm tra nếu không lấy được giá trị MaHoatDong
            if (string.IsNullOrEmpty(selectedMaHoatDong))
            {
                MessageBox.Show("Không thể lấy thông tin hoạt động. Vui lòng chọn dòng hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedMaHoatDong))
            {
                MessageBox.Show("Vui lòng chọn một hoạt động cần chỉnh sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy tên nhân viên từ DataGridView
            string tenNhanVien = dtgNhatKy.Rows[dtgNhatKy.CurrentCell.RowIndex].Cells["TenNhanVien"].Value.ToString();

            // Kiểm tra nếu tên nhân viên không phải là "Ông chủ"
            if (tenNhanVien != "Ông chủ")
            {
                MessageBox.Show("Chỉ có thể sửa hoạt động của chính bạn, không thể sửa của nhân viên khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Mở form sửa
            frmSuaHoatDong frm = new frmSuaHoatDong(selectedMaHoatDong, selectedTenChiNhanh);
            frm.FormClosed += (s, args) =>
            {
                LoadData();
            };
            frm.ShowDialog();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
