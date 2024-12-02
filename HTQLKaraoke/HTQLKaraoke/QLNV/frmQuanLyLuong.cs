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

namespace HTQLKaraoke.QLNV
{
    public partial class frmQuanLyLuong : Form
    {
        string connection = ConfigurationManager.ConnectionStrings["HTQLKaraoke.Properties.Settings.KaraokeConnectionString"].ConnectionString;
        private string maNhanVien;
        public frmQuanLyLuong(string maNhanVien)
        {
            InitializeComponent();

            this.maNhanVien = maNhanVien;
            LoadLuongNhanVien();
        }

        private void LoadLuongNhanVien()
        {
            // Kết nối cơ sở dữ liệu và lấy dữ liệu nhân viên, điểm danh
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();

                // Lấy tháng và năm hiện tại
                int thangHienTai = DateTime.Now.Month;
                int namHienTai = DateTime.Now.Year;

                // Cập nhật truy vấn SQL để lọc theo tháng và năm hiện tại
                string query = @"SELECT NV.MaNhanVien, NV.HoTen, DD.NgayDiemDanh, DD.ThoiGianDiLam, DD.ThoiGianVeLam, NV.LuongCoBan
                         FROM NhanVien NV
                         JOIN DiemDanh DD ON NV.MaNhanVien = DD.MaNhanVien
                         WHERE NV.MaNhanVien = @MaNhanVien 
                         AND MONTH(DD.NgayDiemDanh) = @Thang 
                         AND YEAR(DD.NgayDiemDanh) = @Nam
                         ORDER BY DD.NgayDiemDanh";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaNhanVien", maNhanVien);
                cmd.Parameters.AddWithValue("@Thang", thangHienTai);
                cmd.Parameters.AddWithValue("@Nam", namHienTai);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Kiểm tra nếu có bất kỳ ThoiGianVeLam nào là null
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu cho tháng hiện tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Tính giờ làm hàng ngày và thêm vào DataTable
                dt.Columns.Add("GioLamTrongNgay", typeof(decimal));

                foreach (DataRow row in dt.Rows)
                {
                    if (row["ThoiGianVeLam"] == DBNull.Value)
                    {
                        MessageBox.Show("Vui lòng kết thúc ca trước khi xem lương của nhân viên: " + row["HoTen"].ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    DateTime thoiGianVao = Convert.ToDateTime(row["ThoiGianDiLam"]);
                    DateTime thoiGianRa = Convert.ToDateTime(row["ThoiGianVeLam"]);
                    TimeSpan gioLam = thoiGianRa - thoiGianVao;

                    // Làm tròn 2 số thập phân cho giờ làm
                    row["GioLamTrongNgay"] = Math.Round((decimal)gioLam.TotalHours, 2);
                }

                // Hiển thị DataTable lên DataGridView
                dgvLuongNhanVien.DataSource = dt;

                // Đặt tên tiêu đề cột cho DataGridView bằng tiếng Việt
                dgvLuongNhanVien.Columns["MaNhanVien"].HeaderText = "Mã Nhân Viên";
                dgvLuongNhanVien.Columns["HoTen"].HeaderText = "Họ Tên";
                dgvLuongNhanVien.Columns["NgayDiemDanh"].HeaderText = "Ngày Điểm Danh";
                dgvLuongNhanVien.Columns["ThoiGianDiLam"].HeaderText = "Thời Gian Đi Làm";
                dgvLuongNhanVien.Columns["ThoiGianVeLam"].HeaderText = "Thời Gian Về Làm";
                dgvLuongNhanVien.Columns["LuongCoBan"].HeaderText = "Lương Cơ Bản";
                dgvLuongNhanVien.Columns["GioLamTrongNgay"].HeaderText = "Giờ Làm Trong Ngày";
                dgvLuongNhanVien.AllowUserToAddRows = false;

                // Tự động điều chỉnh độ rộng cột dựa trên nội dung của cột và tiêu đề
                dgvLuongNhanVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                TinhTongGioLam(dt);
            }
        }


        private void TinhTongGioLam(DataTable dt)
        {
            // Tính tổng giờ làm trong tháng
            decimal tongGioLam = 0;
            foreach (DataRow row in dt.Rows)
            {
                tongGioLam += Convert.ToDecimal(row["GioLamTrongNgay"]);
            }

            txtTongGioLam.Text = tongGioLam.ToString("N2") + " giờ";

            // Tính lương thưởng và lương lãnh
            TinhLuong(tongGioLam, Convert.ToDecimal(dt.Rows[0]["LuongCoBan"]));
        }

        private void TinhLuong(decimal tongGioLam, decimal luongCoBan)
        {
            decimal luongThuong = 0;
            decimal tienPhat = 0;
            decimal luongLanh = 0;

            // Tính lương thưởng
            if (tongGioLam > 100)
            {
                luongThuong = 500000;
            }
            else if (tongGioLam > 80)
            {
                luongThuong = 300000;
            }
            else if (tongGioLam > 70)
            {
                luongThuong = 100000;
            }
            else if (tongGioLam < 60)
            {
                // Tính phạt nếu dưới 60 tiếng
                tienPhat = (60 - tongGioLam) * 5000;

                // Giới hạn tiền phạt tối đa là 300,000
                if (tienPhat > 300000)
                {
                    tienPhat = 300000;
                }
            }

            // Tính lương lãnh
            luongLanh = (tongGioLam * luongCoBan) + luongThuong - tienPhat;

            txtLuongThuong.Text = luongThuong.ToString("N0") + "₫";
            txtLuongThuong.ReadOnly = true; 
            txtLuongThuong.BackColor = SystemColors.Window; 
            txtLuongThuong.ForeColor = Color.Green; 

            txtTienPhat.Text = tienPhat > 0 ? "-" + tienPhat.ToString("N0") + "₫" : "0₫";
            txtTienPhat.ReadOnly = true;
            txtTienPhat.BackColor = SystemColors.Window;
            txtTienPhat.ForeColor = tienPhat > 0 ? Color.Red : Color.Black; 

            txtLuongLanh.Text = luongLanh.ToString("N0") + "₫";
            txtLuongLanh.ReadOnly = true;
            txtLuongLanh.BackColor = SystemColors.Window;
            txtLuongLanh.ForeColor = Color.Blue; 
        }

        private void btnNhanLuong_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();

                // Kiểm tra xem lương tháng này đã được nhận chưa
                string queryCheck = @"SELECT COUNT(*) FROM LichSuNhanLuong 
                              WHERE MaNhanVien = @MaNhanVien AND Thang = @Thang AND Nam = @Nam AND TrangThaiNhanLuong = 1";
                SqlCommand cmdCheck = new SqlCommand(queryCheck, conn);
                cmdCheck.Parameters.AddWithValue("@MaNhanVien", maNhanVien);
                cmdCheck.Parameters.AddWithValue("@Thang", DateTime.Now.Month);
                cmdCheck.Parameters.AddWithValue("@Nam", DateTime.Now.Year);

                int isLuongNhan = (int)cmdCheck.ExecuteScalar();

                if (isLuongNhan > 0)
                {
                    MessageBox.Show("Lương tháng này đã được nhận. Bạn không thể nhận thêm lương.", "Thông báo");
                    return;
                }

                // Nếu chưa nhận, thì cập nhật trạng thái đã nhận lương vào bảng LichSuNhanLuong
                string queryInsert = @"INSERT INTO LichSuNhanLuong (MaNhanVien, Thang, Nam, TongGioLam, TongLuong, TrangThaiNhanLuong)
                               VALUES (@MaNhanVien, @Thang, @Nam, @TongGioLam, @TongLuong, @TrangThaiNhanLuong)";
                SqlCommand cmdInsert = new SqlCommand(queryInsert, conn);
                cmdInsert.Parameters.AddWithValue("@MaNhanVien", maNhanVien);
                cmdInsert.Parameters.AddWithValue("@Thang", DateTime.Now.Month);
                cmdInsert.Parameters.AddWithValue("@Nam", DateTime.Now.Year);

                // Chuyển đổi giá trị từ TextBox thành kiểu số (Decimal)
                decimal tongGioLam;
                decimal tongLuong;

                // Kiểm tra và chuyển đổi giá trị
                if (!decimal.TryParse(txtTongGioLam.Text.Replace(" giờ", "").Trim(), out tongGioLam))
                {
                    MessageBox.Show("Giá trị tổng giờ làm không hợp lệ.", "Lỗi");
                    return;
                }

                if (!decimal.TryParse(txtLuongLanh.Text.Replace("₫", "").Replace(",", "").Trim(), out tongLuong))
                {
                    MessageBox.Show("Giá trị tổng lương không hợp lệ.", "Lỗi");
                    return;
                }

                cmdInsert.Parameters.AddWithValue("@TongGioLam", tongGioLam);
                cmdInsert.Parameters.AddWithValue("@TongLuong", tongLuong);
                cmdInsert.Parameters.AddWithValue("@TrangThaiNhanLuong", 1);

                cmdInsert.ExecuteNonQuery();

                MessageBox.Show("Lương đã được nhận thành công!", "Thông báo");
            }
        }

        private void btnLichSu_Click(object sender, EventArgs e)
        {
            frmLichSuNhanLuong frmLichSu = new frmLichSuNhanLuong(maNhanVien);
            frmLichSu.ShowDialog();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
