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

namespace HTQLKaraoke.NhaCungCap
{
    public partial class frmNhaCungCap : Form
    {
        string connection = ConfigurationManager.ConnectionStrings["HTQLKaraoke.Properties.Settings.KaraokeConnectionString"].ConnectionString;
        string maNhaCungCap = "";
        public frmNhaCungCap()
        {
            InitializeComponent();
        }

        private void frmNhaCungCap_Load(object sender, EventArgs e)
        {
            LoadNhaCungCapData();
        }

        private void LoadNhaCungCapData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();
                    string query = "SELECT MaNhaCungCap, TenNhaCungCap, DiaChi, SoDienThoai, MaChiNhanh FROM NhaCungCap";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dtgNhaCungCap.DataSource = dt;

                    // Đổi header sang tiếng Việt
                    dtgNhaCungCap.Columns["MaNhaCungCap"].HeaderText = "ID";
                    dtgNhaCungCap.Columns["TenNhaCungCap"].HeaderText = "Nơi Cung Cấp"; 
                    dtgNhaCungCap.Columns["TenNhaCungCap"].Width = 125;
                    dtgNhaCungCap.Columns["DiaChi"].HeaderText = "Địa Chỉ";
                    dtgNhaCungCap.Columns["SoDienThoai"].HeaderText = "Số Điện Thoại";
                    dtgNhaCungCap.Columns["MaChiNhanh"].HeaderText = "Mã Chi Nhánh";
                    dtgNhaCungCap.AllowUserToAddRows = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            frmThem frm = new frmThem();
            frm.FormClosed += (s, args) =>
            {
                LoadNhaCungCapData();
            };
            frm.ShowDialog();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            frmSua frm = new frmSua(maNhaCungCap);

            frm.FormClosed += (s, args) =>
            {
                LoadNhaCungCapData(); // Gọi lại phương thức load dữ liệu sau khi form sửa đóng
            };

            frm.ShowDialog();
        }


        private void dtgNhaCungCap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                maNhaCungCap = dtgNhaCungCap.Rows[e.RowIndex].Cells["MaNhaCungCap"].Value.ToString();
                if(!string.IsNullOrEmpty(maNhaCungCap))
                {
                    btnSua.Enabled = true;
                    btnXoa.Enabled = true;
                }
                else
                {
                    btnSua.Enabled = false;
                    btnXoa.Enabled = false;
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa nhà cung cấp này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    try
                    {
                        // Mở kết nối
                        conn.Open();

                        // Xóa dữ liệu trong bảng NhapHang liên quan đến mã nhà cung cấp
                        string deleteNhapHangQuery = "DELETE FROM NhapHang WHERE MaNhaCungCap = @MaNhaCungCap";
                        using (SqlCommand cmd = new SqlCommand(deleteNhapHangQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@MaNhaCungCap", maNhaCungCap);
                            cmd.ExecuteNonQuery();
                        }

                        // Xóa nhà cung cấp trong bảng NhaCungCap
                        string deleteNhaCungCapQuery = "DELETE FROM NhaCungCap WHERE MaNhaCungCap = @MaNhaCungCap";
                        using (SqlCommand cmd = new SqlCommand(deleteNhaCungCapQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@MaNhaCungCap", maNhaCungCap);
                            cmd.ExecuteNonQuery();
                        }

                        // Cập nhật lại dữ liệu trong DataGridView
                        LoadNhaCungCapData();

                        MessageBox.Show("Xóa nhà cung cấp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Có lỗi xảy ra khi xóa nhà cung cấp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
