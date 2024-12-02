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
    public partial class frmDichVu : Form
    {
        string connection = ConfigurationManager.ConnectionStrings["HTQLKaraoke.Properties.Settings.KaraokeConnectionString"].ConnectionString;
        private string maPhong;
        public frmDichVu(string maPhong)
        {
            InitializeComponent();
            this.maPhong = maPhong;
        }

        private void LoadServiceData()
        {
            // Kết nối tới cơ sở dữ liệu và lấy danh sách dịch vụ
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                // Truy vấn để lấy thông tin dịch vụ từ bảng DichVu
                string query = @"SELECT dv.MaDichVu, dv.TenDichVu, dv.GiaDichVu, dv.GhiChu, dv.NgayTao, dv.NgayCapNhat
                         FROM DichVu dv";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        flowLayoutPanel.Controls.Clear(); // Xóa các button cũ trước khi thêm mới

                        while (reader.Read())
                        {
                            // Lấy thông tin dịch vụ
                            string maDichVu = reader["MaDichVu"].ToString();
                            string tenDichVu = reader["TenDichVu"].ToString();
                            decimal giaDichVu = Convert.ToDecimal(reader["GiaDichVu"]);
                            string ghiChu = reader["GhiChu"].ToString();

                            // Tạo button cho mỗi dịch vụ
                            Button btnService = new Button();
                            btnService.Text = string.Format("{0}\n{1:N0}₫", tenDichVu, giaDichVu);
                            btnService.Size = new Size(140, 180);
                            btnService.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);
                            btnService.TextAlign = ContentAlignment.BottomCenter;
                            btnService.ImageAlign = ContentAlignment.TopCenter;
                            btnService.Tag = maDichVu; // Lưu mã dịch vụ vào tag để dùng sau này

                            // Sự kiện click để hiển thị chi tiết dịch vụ
                            btnService.Click += (s, e) =>
                            {
                                ShowServiceDetails(maDichVu, tenDichVu, giaDichVu, ghiChu);
                            };

                            // Chọn hình ảnh minh họa dịch vụ (có thể thay đổi theo ý thích)
                            try
                            {
                                btnService.Image = Image.FromFile(@"D:\HTQLKaraoke\HTQLKaraoke\Image\dichvu.gif");
                            }
                            catch (FileNotFoundException ex)
                            {
                                MessageBox.Show("Không tìm thấy file hình ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Đã xảy ra lỗi khi tải hình ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            flowLayoutPanel.Controls.Add(btnService);
                        }
                    }
                }
            }
        }

        private void ShowServiceDetails(string maDichVu, string tenDichVu, decimal giaDichVu, string ghiChu)
        {
            // Hiển thị thông tin dịch vụ trong các TextBox
            groupBoxServiceDetails.Text = string.Format("Thông Tin Dịch Vụ: {0}", tenDichVu);
            txtMaDichVu.Text = maDichVu;
            txtTenDichVu.Text = tenDichVu;
            txtGiaDichVu.Text = giaDichVu.ToString("N0") + "₫";
            txtGhiChu.Text = ghiChu;

            // Cập nhật thông tin trong groupbox
            groupBoxServiceDetails.Visible = true;
        }

        private void frmDichVu_Load(object sender, EventArgs e)
        {
            LoadServiceData();
            txtMaPhong.Text = maPhong;
        }

        private void btnChonDichVu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaDichVu.Text))
            {
                MessageBox.Show("Vui lòng chọn dịch vụ cho phòng.");
                return;
            }
            decimal giaDichVu = Convert.ToDecimal(txtGiaDichVu.Text.Replace("₫", "").Replace(",", ""));
            frmChonDichVu frm = new frmChonDichVu(txtMaDichVu.Text, maPhong, giaDichVu, txtTenDichVu.Text);
            frm.ShowDialog();
        }

        private void btnDichVuHienTai_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(maPhong))
            {
                MessageBox.Show("Không thể tìm thấy phòng.");
                return;
            }
            frmDichVuHienTai frmDichVuHienTai = new frmDichVuHienTai(maPhong);

            frmDichVuHienTai.FormClosed += (s, args) =>
            {
                LoadServiceData();
            };

            frmDichVuHienTai.ShowDialog();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBoxServiceDetails_Enter(object sender, EventArgs e)
        {

        }
    }
}
