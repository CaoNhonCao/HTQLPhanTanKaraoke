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
    public partial class frmOrder : Form
    {
        string connection = ConfigurationManager.ConnectionStrings["HTQLKaraoke.Properties.Settings.KaraokeConnectionString"].ConnectionString;

        string maPhong, loaiPhong, soPhong, maDonHang;
        public frmOrder(string maDonHang, string maPhong, string soPhong, string loaiPhong)
        {
            InitializeComponent();
            this.maPhong = maPhong;
            this.loaiPhong = loaiPhong;
            this.soPhong = soPhong;
            this.maDonHang = maDonHang;
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

        private void btnOrder_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaSanPham.Text))
            {
                MessageBox.Show("Vui lòng chọn sản phẩm để order.");
                return;
            } 
            

            string maSanPham = txtMaSanPham.Text;
            decimal donGia = Convert.ToDecimal(txtGiaBan.Text.Replace("₫", "").Replace(",", ""));

            frmOrderDetails frm = new frmOrderDetails(maDonHang, maSanPham, maPhong, txtTenSanPham.Text, donGia);
            frm.ShowDialog();
        }

        private void frmOrder_Load(object sender, EventArgs e)
        {
            txtMaPhong.Text = maPhong;
            txtSoPhong.Text = soPhong;
            txtLoaiPhong.Text = loaiPhong;
            txtMaDonHang.Text = maDonHang;
            string queryDonHang = "SELECT GhiChu FROM DonHang WHERE MaDonHang = @MaDonHang and TrangThai = 0";

            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();

                // Load dữ liệu cho TextBox từ bảng DonHang
                using (SqlCommand cmd = new SqlCommand(queryDonHang, conn))
                {
                    cmd.Parameters.AddWithValue("@MaDonHang", maDonHang);

                    // Sử dụng ExecuteScalar để lấy giá trị của GhiChu
                    var ghiChu = cmd.ExecuteScalar();

                    // Kiểm tra nếu giá trị không null thì gán vào TextBox, nếu null thì gán chuỗi rỗng
                    if (ghiChu != null)
                    {
                        txtGhiChu.Text = ghiChu.ToString();
                    }
                    else
                    {
                        txtGhiChu.Text = string.Empty;
                    }
                }
            }
        }

        private void btnDonHangHienTai_Click(object sender, EventArgs e)
        {
            frmDonHangHienTai frmDonHang = new frmDonHangHienTai(maDonHang);
            
            frmDonHang.FormClosed += (s, args) =>
            {
                frmOrder_Load(sender, e); 
            };

            frmDonHang.ShowDialog();
        }
    }
}
