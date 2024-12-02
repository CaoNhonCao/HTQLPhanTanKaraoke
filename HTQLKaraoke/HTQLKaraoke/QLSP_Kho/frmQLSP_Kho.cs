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

namespace HTQLKaraoke.QLSP_Kho
{
    public partial class frmQLSP_Kho : Form
    {
        string connection = ConfigurationManager.ConnectionStrings["HTQLKaraoke.Properties.Settings.KaraokeConnectionString"].ConnectionString;
        string maSanPham;
        public frmQLSP_Kho()
        {
            InitializeComponent();
        }

        private void MenuThucUong_Click(object sender, EventArgs e)
        {
            btnBia.Visible = true;
            btnNuocNgot.Visible = true;
            btnNuocSuoi.Visible = true;
            btnRuou.Visible = true;
            btnDoAnNhe.Visible = false;
            btnTraiCay.Visible = false;
            btnCombo.Visible = false;
        }

        private void MenuDoAn_Click(object sender, EventArgs e)
        {
            btnBia.Visible = false;
            btnNuocNgot.Visible = false;
            btnNuocSuoi.Visible = false;
            btnRuou.Visible = false;
            btnDoAnNhe.Visible = true;
            btnTraiCay.Visible = true;
            btnCombo.Visible = false;
        }

        private void MenuCombo_Click(object sender, EventArgs e)
        {
            btnBia.Visible = false;
            btnNuocNgot.Visible = false;
            btnNuocSuoi.Visible = false;
            btnRuou.Visible = false;
            btnDoAnNhe.Visible = false;
            btnTraiCay.Visible = false;
            btnCombo.Visible = true;
        }

        private void LoadProductData(string loaiSanPham)
        {
            // Kết nối tới cơ sở dữ liệu và lấy danh sách sản phẩm theo loại
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                // Truy vấn JOIN giữa bảng SanPham và QuanLyKho
                string query = @"SELECT sp.MaSanPham, sp.TenSanPham, sp.GiaBan, qlk.SoLuongTon, sp.DonViTinh
                         FROM SanPham sp
                         JOIN QuanLyKho qlk ON sp.MaSanPham = qlk.MaSanPham
                         WHERE sp.LoaiSanPham = @LoaiSanPham";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@LoaiSanPham", loaiSanPham);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        flowLayoutPanel.Controls.Clear(); // Xóa các button cũ trước khi thêm mới

                        while (reader.Read())
                        {
                            // Lấy thông tin sản phẩm
                            string maSanPham = reader["MaSanPham"].ToString();
                            string tenSanPham = reader["TenSanPham"].ToString();
                            decimal giaBan = Convert.ToDecimal(reader["GiaBan"]);
                            int soLuong = Convert.ToInt32(reader["SoLuongTon"]);
                            string donViTinh = reader["DonViTinh"].ToString();

                            // Tạo button cho mỗi sản phẩm
                            Button btnProduct = new Button();
                            btnProduct.Text = string.Format("{0}\n{1:N0}₫", tenSanPham, giaBan);
                            btnProduct.Size = new Size(140, 180); 
                            btnProduct.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold); // Đặt font chữ in đậm và kích thước 12
                            btnProduct.TextAlign = ContentAlignment.BottomCenter; // Văn bản nằm ở giữa đáy button
                            btnProduct.ImageAlign = ContentAlignment.TopCenter;
                            btnProduct.Tag = maSanPham; // Lưu mã sản phẩm vào tag để dùng sau này

                            // Sự kiện click để hiển thị chi tiết sản phẩm
                           
                            btnProduct.Click += (s, e) =>
                            {
                                ShowProductDetails(maSanPham, tenSanPham, giaBan, soLuong, donViTinh, loaiSanPham); 
                                if(!string.IsNullOrEmpty(maSanPham))
                                {
                                    btnXoaSanPham.Enabled = true;
                                    btnSuaSanPham.Enabled = true;
                                }
                            };

                            // Chọn hình ảnh dựa vào trạng thái
                            try
                            {
                                if (loaiSanPham == "Bia")
                                {
                                    btnProduct.Image = Image.FromFile(@"\HTQLKaraoke\HTQLKaraoke\Image\bia.gif");
                                }
                                else if (loaiSanPham == "Rượu")
                                {
                                    btnProduct.Image = Image.FromFile(@"\HTQLKaraoke\HTQLKaraoke\Image\ruou.gif"); // Sử dụng @ để tránh escape
                                }
                                else if (loaiSanPham == "Nước ngọt")
                                {
                                    btnProduct.Image = Image.FromFile(@"\HTQLKaraoke\HTQLKaraoke\Image\nuocngot.gif"); // Sử dụng @ để tránh escape
                                }
                                else if (loaiSanPham == "Nước suối")
                                {
                                    btnProduct.Image = Image.FromFile(@"\HTQLKaraoke\HTQLKaraoke\Image\nuocsuoi.gif"); // Sử dụng @ để tránh escape
                                }
                                else if (loaiSanPham == "Đồ ăn nhẹ")
                                {
                                    btnProduct.Image = Image.FromFile(@"\HTQLKaraoke\HTQLKaraoke\Image\doannhe.gif"); // Sử dụng @ để tránh escape
                                }
                                else if (loaiSanPham == "Trái cây")
                                {
                                    btnProduct.Image = Image.FromFile(@"\HTQLKaraoke\HTQLKaraoke\Image\traicay.gif"); // Sử dụng @ để tránh escape
                                }
                                else if (loaiSanPham == "Combo")
                                {
                                    btnProduct.Image = Image.FromFile(@"\HTQLKaraoke\HTQLKaraoke\Image\combo.gif"); // Sử dụng @ để tránh escape
                                }
                            }
                            catch (FileNotFoundException ex)
                            {
                                MessageBox.Show("Không tìm thấy file hình ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Đã xảy ra lỗi khi tải hình ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            flowLayoutPanel.Controls.Add(btnProduct);
                        }
                    }
                }
            }
        }

        private void ShowProductDetails(string maSanPham, string tenSanPham, decimal giaSanPham, int soLuong, string donViTinh, string loaiSanPham)
        {
            // Hiển thị thông tin sản phẩm trong các TextBox
            groupBoxProductDetails.Text = string.Format("Thông Tin Sản Phẩm: {0}", tenSanPham);
            txtMaSanPham.Text = maSanPham;
            txtTenSanPham.Text = tenSanPham;
            txtLoaiSanPham.Text = loaiSanPham;  // Hiển thị loại sản phẩm
            txtGiaBan.Text = giaSanPham.ToString("N0") + "₫";
            txtSoLuong.Text = soLuong.ToString();
            txtDonViTinh.Text = donViTinh;
            this.maSanPham = txtMaSanPham.Text;

            // Cập nhật thông tin trong groupbox
            groupBoxProductDetails.Visible = true;
        }


        // Các nút nhấn load sản phẩm theo từng loại
        private void btnBia_Click(object sender, EventArgs e)
        {
            LoadProductData("Bia");
        }

        private void btnRuou_Click(object sender, EventArgs e)
        {
            LoadProductData("Rượu");
        }

        private void btnNuocNgot_Click(object sender, EventArgs e)
        {
            LoadProductData("Nước ngọt");
        }

        private void btnNuocSuoi_Click(object sender, EventArgs e)
        {
            LoadProductData("Nước suối");
        }

        private void btnDoAnNhe_Click(object sender, EventArgs e)
        {
            LoadProductData("Đồ ăn nhẹ");
        }

        private void btnTraiCay_Click(object sender, EventArgs e)
        {
            LoadProductData("Trái cây");
        }

        private void btnCombo_Click(object sender, EventArgs e)
        {
            LoadProductData("Combo");
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThemSanPham_Click(object sender, EventArgs e)
        {
            frmThemSanPham frmThemSanPham = new frmThemSanPham();
            frmThemSanPham.FormClosed += (s, args) =>
            {
                LoadAllProducts();
            };
             
            frmThemSanPham.ShowDialog();
        }

        private void frmQLSP_Kho_Load(object sender, EventArgs e)
        {

        }


        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadAllProducts()
        {
            // Bạn có thể thêm tùy chọn nếu muốn load toàn bộ hoặc gọi lại từng loại.
            LoadProductData("Bia");
            LoadProductData("Rượu");
            LoadProductData("Nước ngọt");
            LoadProductData("Nước suối");
            LoadProductData("Đồ ăn nhẹ");
            LoadProductData("Trái cây");
            LoadProductData("Combo");
        }

        private void btnSuaSanPham_Click(object sender, EventArgs e)
        {
            frmSuaSanPham frmSuaSanPham = new frmSuaSanPham(maSanPham);
            frmSuaSanPham.FormClosed += (s, args) =>
            {
                LoadAllProducts();
            };

            frmSuaSanPham.ShowDialog();
        }

        private void btnXoaSanPham_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này không?",
                                                  "Xác nhận xóa",
                                                  MessageBoxButtons.OKCancel,
                                                  MessageBoxIcon.Warning);

            if (result == DialogResult.OK)
            {
                maSanPham = txtMaSanPham.Text;

                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();

                    // Bắt đầu một transaction để đảm bảo tính toàn vẹn dữ liệu
                    SqlTransaction transaction = conn.BeginTransaction();

                    try
                    {
                        // Xóa sản phẩm khỏi bảng QuanLyKho trước
                        string deleteQuanLyKho = @"DELETE FROM QuanLyKho WHERE MaSanPham = @MaSanPham";
                        using (SqlCommand cmd = new SqlCommand(deleteQuanLyKho, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@MaSanPham", maSanPham);
                            cmd.ExecuteNonQuery();
                        }

                        // Xóa sản phẩm khỏi bảng NhapHang
                        string deleteNhapHang = @"DELETE FROM NhapHang WHERE MaSanPham = @MaSanPham";
                        using (SqlCommand cmd = new SqlCommand(deleteNhapHang, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@MaSanPham", maSanPham);
                            cmd.ExecuteNonQuery();
                        }

                        // Kiểm tra sự tồn tại của sản phẩm trong ChiTietDonHang và lấy trạng thái, mã phòng từ DonHang
                        string queryChiTietDonHang = @"
                            SELECT dh.TrangThai, dh.MaPhong
                            FROM ChiTietDonHang ctdh
                            INNER JOIN DonHang dh ON ctdh.MaDonHang = dh.MaDonHang
                            WHERE ctdh.MaSanPham = @MaSanPham";
                
                        using (SqlCommand cmd = new SqlCommand(queryChiTietDonHang, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@MaSanPham", maSanPham);
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    bool trangThai = (bool)reader["TrangThai"];
                                    string maPhong = reader["MaPhong"].ToString();

                                    // Nếu trạng thái là 1 (đã thanh toán), xóa sản phẩm khỏi ChiTietDonHang
                                    if (trangThai)
                                    {
                                        // Xóa sản phẩm khỏi ChiTietDonHang
                                        string deleteChiTietDonHang = @"DELETE FROM ChiTietDonHang WHERE MaSanPham = @MaSanPham";
                                        using (SqlCommand cmdDelete = new SqlCommand(deleteChiTietDonHang, conn, transaction))
                                        {
                                            cmdDelete.Parameters.AddWithValue("@MaSanPham", maSanPham);
                                            cmdDelete.ExecuteNonQuery();
                                        }
                                    }
                                    else
                                    {
                                        // Thông báo cho người dùng biết sản phẩm đang được order
                                        MessageBox.Show("Sản phẩm hiện đang được order tại phòng " + maPhong + " và không thể tiến hành xóa sản phẩm lúc này!", 
                                                        "Thông báo", 
                                                        MessageBoxButtons.OK, 
                                                        MessageBoxIcon.Warning);
                                        transaction.Rollback();
                                        return;
                                    }
                                }
                            }
                        }

                        // Commit transaction sau khi xóa thành công
                        transaction.Commit();
                        MessageBox.Show("Sản phẩm đã được xóa thành công.");
                    }
                    catch
                    {
                        // Rollback transaction nếu có lỗi xảy ra
                        transaction.Rollback();
                    }
                }

                // Làm mới danh sách sản phẩm sau khi xóa
                LoadAllProducts();
            }
        }


        private void btnCanhBao_Click(object sender, EventArgs e)
        {
            frmCanhBao frmCanhBao = new frmCanhBao();
            frmCanhBao.ShowDialog();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            frmThongKeSanPham frmThongKe = new frmThongKeSanPham();
            frmThongKe.ShowDialog();
        }


    }
}
