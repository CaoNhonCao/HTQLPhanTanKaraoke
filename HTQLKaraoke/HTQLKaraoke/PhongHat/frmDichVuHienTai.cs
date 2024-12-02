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
    public partial class frmDichVuHienTai : Form
    {
        string connection = ConfigurationManager.ConnectionStrings["HTQLKaraoke.Properties.Settings.KaraokeConnectionString"].ConnectionString;
        string maPhong;
        public frmDichVuHienTai(string maPhong)
        {
            InitializeComponent();
            this.maPhong = maPhong;
        }

        private void frmDichVuHienTai_Load(object sender, EventArgs e)
        {
            LoadDichVu();
        }
        private void LoadDichVu()
        {
            string queryDichVuChoPhong = @"
        SELECT 
            DichVuChoPhong.MaDichVu, 
            DichVuChoPhong.MaPhong, 
            DichVu.TenDichVu, 
            DichVu.GiaDichVu,
            DichVuChoPhong.SoLuong, 
            DichVuChoPhong.NgaySuDung, 
            DichVuChoPhong.ThanhTien, 
            DichVuChoPhong.TrangThai
        FROM 
            DichVuChoPhong
        INNER JOIN 
            DichVu ON DichVuChoPhong.MaDichVu = DichVu.MaDichVu
        WHERE 
            DichVuChoPhong.MaPhong = @MaPhong AND 
            DichVuChoPhong.TrangThai = 0";

            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(queryDichVuChoPhong, conn))
                {
                    cmd.Parameters.AddWithValue("@MaPhong", maPhong);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        dgvCTDH.DataSource = dataTable;
                        txtNgayDung.Text = dataTable.Rows[0]["NgaySuDung"].ToString();

                        bool trangThai = (bool)dataTable.Rows[0]["TrangThai"];
                        txtTrangThai.Text = trangThai ? "Đã thanh toán" : "Chưa thanh toán";
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy dữ liệu nào cho phòng này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtNgayDung.Text = "";
                        txtTrangThai.Text = "";
                        this.Close();
                        return;
                    }
                }

                // Cập nhật tiêu đề các cột trong DataGridView
                dgvCTDH.Columns["MaDichVu"].HeaderText = "Mã Dịch Vụ";
                dgvCTDH.Columns["MaPhong"].HeaderText = "Mã Phòng";
                dgvCTDH.Columns["TenDichVu"].HeaderText = "Tên Dịch Vụ";
                dgvCTDH.Columns["GiaDichVu"].HeaderText = "Giá Dịch Vụ";
                dgvCTDH.Columns["SoLuong"].HeaderText = "Số Lượng";
                dgvCTDH.Columns["ThanhTien"].HeaderText = "Thành Tiền";
                dgvCTDH.Columns["NgaySuDung"].HeaderText = "Ngày Sử Dụng";
                dgvCTDH.Columns["TrangThai"].HeaderText = "Trạng Thái";

                dgvCTDH.Columns["NgaySuDung"].Width = 120;
                dgvCTDH.Columns["TrangThai"].Width = 70;
                dgvCTDH.AllowUserToAddRows = false;
            }

            TinhTongTien();
        }

        private void TinhTongTien()
        {
            decimal tongTien = 0;
            foreach (DataGridViewRow row in dgvCTDH.Rows)
            {
                tongTien += Convert.ToDecimal(row.Cells["ThanhTien"].Value);
            }

            txtTongTien.Text = tongTien.ToString("N0") + " VND";
        }

        private void btnGiamSoLuong_Click(object sender, EventArgs e)
        {
            if (dgvCTDH.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn dịch vụ để giảm số lượng dịch vụ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = dgvCTDH.SelectedRows[0];
            string maDichVu = selectedRow.Cells["MaDichVu"].Value.ToString();
            string maPhong = selectedRow.Cells["MaPhong"].Value.ToString();
            decimal soLuongHienTai = Convert.ToDecimal(selectedRow.Cells["SoLuong"].Value);
            decimal giaDichVu = Convert.ToDecimal(selectedRow.Cells["GiaDichVu"].Value);
            decimal soLuongGiam = numSoLuongGiam.Value;

            if (soLuongGiam > soLuongHienTai)
            {
                MessageBox.Show("Số lượng giảm của dịch vụ lớn hơn số lượng đang sử dụng, vui lòng chọn lại số lượng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal thanhTienGiam = soLuongGiam * giaDichVu;

            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();

                if (soLuongHienTai - soLuongGiam == 0)
                {
                    string queryDelete = "DELETE FROM DichVuChoPhong WHERE MaDichVu = @MaDichVu AND MaPhong = @MaPhong";
                    using (SqlCommand cmd = new SqlCommand(queryDelete, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaDichVu", maDichVu);
                        cmd.Parameters.AddWithValue("@MaPhong", maPhong);
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    string queryUpdateDVCP = "UPDATE DichVuChoPhong SET SoLuong = SoLuong - @SoLuongGiam, ThanhTien = ThanhTien - @ThanhTienGiam WHERE MaDichVu = @MaDichVu AND MaPhong = @MaPhong";
                    using (SqlCommand cmd = new SqlCommand(queryUpdateDVCP, conn))
                    {
                        cmd.Parameters.AddWithValue("@SoLuongGiam", soLuongGiam);
                        cmd.Parameters.AddWithValue("@ThanhTienGiam", thanhTienGiam);
                        cmd.Parameters.AddWithValue("@MaDichVu", maDichVu);
                        cmd.Parameters.AddWithValue("@MaPhong", maPhong);
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            MessageBox.Show("Cập nhật giảm số lượng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadDichVu();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
