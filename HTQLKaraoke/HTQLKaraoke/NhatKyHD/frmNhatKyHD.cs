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

namespace HTQLKaraoke.NhatKyHD
{
    public partial class frmNhatKyHD : Form
    {
        string connection = ConfigurationManager.ConnectionStrings["HTQLKaraoke.Properties.Settings.KaraokeConnectionString"].ConnectionString;
        private string selectedMaHoatDong = null;
        public frmNhatKyHD()
        {
            InitializeComponent();
        }

        private void frmNhatKyHD_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
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
                            ChiNhanh cn ON hd.MaChiNhanh = cn.MaChiNhanh
                        ORDER BY 
                            hd.NgayThucHien DESC";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
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
                    dtgNhatKy.AllowUserToAddRows = false;

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
                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["HTQLKaraoke.Properties.Settings.KaraokeConnectionString"].ConnectionString))
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
            if (e.RowIndex >= 0)
            {
                selectedMaHoatDong = dtgNhatKy.Rows[e.RowIndex].Cells["MaHoatDong"].Value.ToString();
                if (!string.IsNullOrEmpty(selectedMaHoatDong))
                {
                    btnXoa.Enabled = true;
                    btnSua.Enabled = true;
                }
                else
                {
                    btnSua.Enabled = false;
                    btnXoa.Enabled = false;
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedMaHoatDong))
            {
                MessageBox.Show("Vui lòng chọn một hoạt động cần chỉnh sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Mở form sửa
            frmSuaHoatDong frm = new frmSuaHoatDong(selectedMaHoatDong);
            frm.FormClosed += (s, args) =>
            {
                LoadData(); // Tải lại dữ liệu sau khi đóng form sửa
            };
            frm.ShowDialog();
        }
    }
}
