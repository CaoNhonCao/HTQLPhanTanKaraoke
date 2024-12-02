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

namespace HTQLKaraoke.QLDV
{
    public partial class frmQLDV : Form
    {
        string connection = ConfigurationManager.ConnectionStrings["HTQLKaraoke.Properties.Settings.KaraokeConnectionString"].ConnectionString;
        public frmQLDV()
        {
            InitializeComponent();
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
                            DateTime ngayTao = Convert.ToDateTime(reader["NgayTao"]);
                            DateTime ngayCapNhat = Convert.ToDateTime(reader["NgayCapNhat"]);

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
                                ShowServiceDetails(maDichVu, tenDichVu, giaDichVu, ghiChu, ngayTao, ngayCapNhat);
                                if(!string.IsNullOrEmpty(maDichVu))
                                {
                                    btnSuaDichVu.Enabled = true;
                                    btnXoaDichVu.Enabled = true;
                                }
                            };

                            // Chọn hình ảnh minh họa dịch vụ (có thể thay đổi theo ý thích)
                            try
                            {
                                    btnService.Image = Image.FromFile(@"\HTQLKaraoke\HTQLKaraoke\Image\dichvu.gif");
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

        private void ShowServiceDetails(string maDichVu, string tenDichVu, decimal giaDichVu, string ghiChu, DateTime ngayTao, DateTime ngayCapNhat)
        {
            // Hiển thị thông tin dịch vụ trong các TextBox
            groupBoxServiceDetails.Text = string.Format("Thông Tin Dịch Vụ: {0}", tenDichVu);
            txtMaDichVu.Text = maDichVu;
            txtTenDichVu.Text = tenDichVu;
            txtGiaDichVu.Text = giaDichVu.ToString("N0") + "₫";
            txtGhiChu.Text = ghiChu; 
            txtNgayTao.Text = ngayTao.ToString("dd/MM/yyyy HH:mm:ss");
            txtNgayCapNhat.Text = ngayCapNhat.ToString("dd/MM/yyyy HH:mm:ss");


            // Cập nhật thông tin trong groupbox
            groupBoxServiceDetails.Visible = true;
        }

        private void frmQLDV_Load(object sender, EventArgs e)
        {
            LoadServiceData();
        }

        private void btnThemDichVu_Click(object sender, EventArgs e)
        {
            frmThemDichVu frmThemDichVu = new frmThemDichVu();
            frmThemDichVu.FormClosed += (s, args) =>
            {
                LoadServiceData();
            };

            frmThemDichVu.ShowDialog();
        }

        private void btnXoaDichVu_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa dịch vụ này không?",
                                                  "Xác nhận xóa",
                                                  MessageBoxButtons.OKCancel,
                                                  MessageBoxIcon.Warning);

            if (result == DialogResult.OK)
            {
                string maDichVu = txtMaDichVu.Text.Trim();

                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();

                    try
                    {
                        // Tạo danh sách lưu mã phòng đang sử dụng dịch vụ và chưa thanh toán
                        List<string> maPhongChuaThanhToan = new List<string>();

                        // Kiểm tra trạng thái của dịch vụ trong bảng DichVuChoPhong
                        string checkDichVuStatus = @"SELECT MaPhong FROM DichVuChoPhong WHERE MaDichVu = @MaDichVu AND TrangThai = 0";
                        using (SqlCommand cmdCheck = new SqlCommand(checkDichVuStatus, conn, transaction))
                        {
                            cmdCheck.Parameters.AddWithValue("@MaDichVu", maDichVu);

                            using (SqlDataReader reader = cmdCheck.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    maPhongChuaThanhToan.Add(reader["MaPhong"].ToString());
                                }
                                reader.Close();
                            }
                        }

                        // Nếu có phòng chưa thanh toán, hiển thị thông báo và hủy quá trình xóa
                        if (maPhongChuaThanhToan.Count > 0)
                        {
                            string danhSachPhong = string.Join(", ", maPhongChuaThanhToan);
                            MessageBox.Show("Dịch vụ này đang được sử dụng tại các phòng " + danhSachPhong + " và chưa được thanh toán, không thể xóa.");
                            transaction.Rollback();
                            return;
                        }

                        // Xóa dịch vụ từ bảng DichVuChoPhong trước
                        string deleteFromDichVuChoPhong = @"DELETE FROM DichVuChoPhong WHERE MaDichVu = @MaDichVu";
                        using (SqlCommand cmdDeleteFromDichVuChoPhong = new SqlCommand(deleteFromDichVuChoPhong, conn, transaction))
                        {
                            cmdDeleteFromDichVuChoPhong.Parameters.AddWithValue("@MaDichVu", maDichVu);
                            cmdDeleteFromDichVuChoPhong.ExecuteNonQuery();
                        }

                        // Xóa dịch vụ từ bảng DichVu
                        string deleteDichVu = @"DELETE FROM DichVu WHERE MaDichVu = @MaDichVu";
                        using (SqlCommand cmdDeleteDichVu = new SqlCommand(deleteDichVu, conn, transaction))
                        {
                            cmdDeleteDichVu.Parameters.AddWithValue("@MaDichVu", maDichVu);
                            cmdDeleteDichVu.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        MessageBox.Show("Xóa dịch vụ thành công.");
                    }
                    catch
                    {
                        transaction.Rollback();
                        MessageBox.Show("Có lỗi xảy ra trong quá trình xóa dịch vụ.");
                    }
                }

                LoadServiceData();
            }
        }


        private void btnSuaDichVu_Click(object sender, EventArgs e)
        {
            frmSuaDichVu frmSuaDichVu = new frmSuaDichVu(txtMaDichVu.Text);
            frmSuaDichVu.FormClosed += (s, args) =>
            {
                LoadServiceData();
            };
                frmSuaDichVu.ShowDialog();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
