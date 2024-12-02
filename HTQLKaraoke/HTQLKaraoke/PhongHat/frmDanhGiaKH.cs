using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace HTQLKaraoke.PhongHat
{
    public partial class frmDanhGiaKH : Form
    {
        private string connection = ConfigurationManager.ConnectionStrings["HTQLKaraoke.Properties.Settings.KaraokeConnectionString"].ConnectionString;
        private string maKhachHang;
        private string maPhong;
        private int diemDanhGia = 0;
        string imagePath = @"D:\HTQLKaraoke\HTQLKaraoke\Image\";
        private PictureBox[] starPictures;

        public frmDanhGiaKH(string maKhachHang, string maPhong, string tenKhachHang, string soPhong)
        {
            InitializeComponent();
            this.maKhachHang = maKhachHang;
            this.maPhong = maPhong;

            // Thiết lập các thông tin ban đầu
            txtTenKhachHang.Text = tenKhachHang;
            txtSoPhong.Text = soPhong;
            dtpNgayDanhGia.Value = DateTime.Now;

            // Khởi tạo giao diện ngôi sao
            InitializeStars();

        }

        private void InitializeStars()
        {
            // Tạo ngôi sao
            starPictures = new PictureBox[5];
            FlowLayoutPanel starPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                Width = 300, // Bề rộng panel cho 5 ngôi sao (5 * 40px)
                Height = 50, // Chiều cao của panel
                Location = new Point(210, 110), // Vị trí panel trên form
                AutoSize = true, // Tự động điều chỉnh kích thước theo số lượng ngôi sao
                AutoSizeMode = AutoSizeMode.GrowAndShrink, // Điều chỉnh kích thước cho vừa với ngôi sao
                Dock = DockStyle.Top // Canh panel vào phía trên của form
            };

            // Canh giữa bằng cách thiết lập phần điều chỉnh vị trí trong parent container
            starPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.Controls.Add(starPanel);

            for (int i = 0; i < 5; i++)
            {
                starPictures[i] = new PictureBox
                {
                    Width = 40,
                    Height = 40,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Cursor = Cursors.Hand,
                    Image = Image.FromFile(Path.Combine(imagePath, "star_empty.png")),
                    Tag = i + 1 // Gán giá trị điểm cho mỗi ngôi sao
                };
                starPictures[i].Click += Star_Click;
                starPanel.Controls.Add(starPictures[i]);
            }
        }


        private void Star_Click(object sender, EventArgs e)
        {
            PictureBox clickedStar = sender as PictureBox;
            diemDanhGia = (int)clickedStar.Tag;

            for (int i = 0; i < starPictures.Length; i++)
            {
                if (i < diemDanhGia)
                {
                    starPictures[i].Image = Image.FromFile(Path.Combine(imagePath, "star_filled.png"));
                }
                else
                {
                    starPictures[i].Image = Image.FromFile(Path.Combine(imagePath, "star_empty.png"));
                }
            }
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            string noiDung = txtNoiDung.Text.Trim();
            DateTime ngayDanhGia = dtpNgayDanhGia.Value;

            if (string.IsNullOrEmpty(noiDung))
            {
                MessageBox.Show("Vui lòng nhập nội dung đánh giá.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (diemDanhGia == 0)
            {
                MessageBox.Show("Vui lòng chọn điểm đánh giá.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();
                    string query = "INSERT INTO DanhGiaKhachHang (MaKhachHang, MaPhong, NoiDung, DiemDanhGia, NgayDanhGia) VALUES (@MaKhachHang, @MaPhong, @NoiDung, @DiemDanhGia, @NgayDanhGia)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaKhachHang", maKhachHang);
                        cmd.Parameters.AddWithValue("@MaPhong", maPhong);
                        cmd.Parameters.AddWithValue("@NoiDung", noiDung);
                        cmd.Parameters.AddWithValue("@DiemDanhGia", diemDanhGia);
                        cmd.Parameters.AddWithValue("@NgayDanhGia", ngayDanhGia);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Lưu đánh giá thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Lưu đánh giá thất bại. Vui lòng thử lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
