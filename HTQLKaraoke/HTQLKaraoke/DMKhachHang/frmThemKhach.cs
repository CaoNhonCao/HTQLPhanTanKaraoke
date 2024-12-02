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

namespace HTQLKaraoke.DMKhachHang
{
    public partial class frmThemKhach : Form
    {
        string connection = ConfigurationManager.ConnectionStrings["HTQLKaraoke.Properties.Settings.KaraokeConnectionString"].ConnectionString;
        public frmThemKhach()
        {
            InitializeComponent(); 
            SetupComboBox();
            SetupDateTimePicker();
            LoadMaKhachHang();
            toolTipbtn.SetToolTip(this.btnLuu, "Lưu Thông Tin");
            toolTipbtn.SetToolTip(this.btnHuy, "Thoát Trang");
        }

        private void LoadMaKhachHang()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();

                    // Tạo mã khách hàng ngẫu nhiên gồm 10 ký tự (chữ và số)
                    string randomMaKH = GenerateRandomCode(10);

                    // Kiểm tra nếu mã này đã tồn tại trong cơ sở dữ liệu
                    string query = "SELECT COUNT(*) FROM KhachHang WHERE MaKhachHang = @MaKhachHang";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaKhachHang", randomMaKH);
                        int count = (int)cmd.ExecuteScalar();

                        // Nếu mã đã tồn tại, tạo mã khác
                        while (count > 0)
                        {
                            randomMaKH = GenerateRandomCode(10);
                            cmd.Parameters["@MaKhachHang"].Value = randomMaKH;
                            count = (int)cmd.ExecuteScalar();
                        }
                    }

                    // Gán mã khách hàng ngẫu nhiên vào ô txtMaKhach
                    txtMaKhach.Text = randomMaKH;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải mã khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Hàm tạo mã ngẫu nhiên với độ dài tuỳ chọn
        private string GenerateRandomCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void SetupComboBox()
        {
            // Cài đặt lựa chọn cho ComboBox Giới tính
            cbxGioiTinh.Items.AddRange(new string[] { "Nam", "Nữ", "Khác" });
            cbxGioiTinh.SelectedIndex = 0;
            cbxGioiTinh.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void SetupDateTimePicker()
        {
            // Thiết lập định dạng cho DateTimePicker (dd/MM/yyyy)
            dtpNgaySinh.Format = DateTimePickerFormat.Custom;
            dtpNgaySinh.CustomFormat = "dd/MM/yyyy";
            dtpNgaySinh.MaxDate = DateTime.Today; // Không cho phép chọn ngày tương lai
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                string maKH = txtMaKhach.Text.Trim();
                string hoTen = txtHoTen.Text.Trim();
                string diaChi = txtDiaChi.Text.Trim();
                string email = txtEmail.Text.Trim();
                string soDienThoai = txtSDT.Text.Trim();
                DateTime ngaySinh = dtpNgaySinh.Value;
                string gioiTinh = cbxGioiTinh.SelectedItem.ToString();
                string loaiKhach = "Thường";
                string ghiChu = txtGhiChu.Text.Trim();  
                string maChiNhanh = ""; 

                if ((DateTime.Now.Year - ngaySinh.Year) < 15)
                {
                    MessageBox.Show("Khách hàng phải lớn hơn 15 tuổi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpNgaySinh.Focus();
                    return;
                }

                if (!string.IsNullOrEmpty(soDienThoai))
                {
                    string phonePattern = @"^(0[3|5|7|8|9])+([0-9]{8})$"; 
                    if (!Regex.IsMatch(soDienThoai, phonePattern))
                    {
                        MessageBox.Show("Số điện thoại không hợp lệ. Vui lòng nhập lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtSDT.Focus();
                        return;
                    }
                }

                if (!string.IsNullOrEmpty(email))
                {
                    string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"; // Mẫu email hợp lệ
                    if (!Regex.IsMatch(email, emailPattern))
                    {
                        MessageBox.Show("Email không hợp lệ. Vui lòng nhập lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtEmail.Focus();
                        return;
                    }
                }
                try
                {
                    using (SqlConnection conn = new SqlConnection(connection))
                    {
                        conn.Open();

                        // Lấy mã chi nhánh từ bảng ChiNhanh
                        string chiNhanhQuery = "SELECT TOP 1 MaChiNhanh FROM ChiNhanh";
                        using (SqlCommand chiNhanhCmd = new SqlCommand(chiNhanhQuery, conn))
                        {
                            var chiNhanhResult = chiNhanhCmd.ExecuteScalar();
                            if (chiNhanhResult != null)
                            {
                                maChiNhanh = chiNhanhResult.ToString();
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy mã chi nhánh.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }

                        // Thêm khách hàng mới
                        string insertQuery = "INSERT INTO KhachHang (MaKhachHang, HoTen, DiaChi, Email, SoDienThoai, NgaySinh, GioiTinh, LoaiKhachHang, GhiChu, MaChiNhanh) " +
                                             "VALUES (@MaKhachHang, @HoTen, @DiaChi, @Email, @SoDienThoai, @NgaySinh, @GioiTinh, @LoaiKhachHang, @GhiChu, @MaChiNhanh)";

                        using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                        {
                            insertCmd.Parameters.AddWithValue("@MaKhachHang", maKH);
                            insertCmd.Parameters.AddWithValue("@HoTen", hoTen);
                            insertCmd.Parameters.AddWithValue("@DiaChi", diaChi);
                            insertCmd.Parameters.AddWithValue("@Email", email);
                            insertCmd.Parameters.AddWithValue("@SoDienThoai", soDienThoai);
                            insertCmd.Parameters.AddWithValue("@NgaySinh", ngaySinh);
                            insertCmd.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                            insertCmd.Parameters.AddWithValue("@LoaiKhachHang", loaiKhach);
                            insertCmd.Parameters.AddWithValue("@GhiChu", ghiChu);
                            insertCmd.Parameters.AddWithValue("@MaChiNhanh", maChiNhanh); // Thêm mã chi nhánh vào truy vấn

                            insertCmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Thêm khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Có lỗi xảy ra khi thêm khách hàng vào cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Kiểm tra dữ liệu nhập vào
        private bool ValidateInput()
        {
            // Kiểm tra Họ và Tên không được để trống
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập Họ và Tên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                return true;
            }
            else if (!Regex.IsMatch(txtEmail.Text, @"^[\w-\.]+@gmail\.com$"))
            {
                MessageBox.Show("Email phải có định dạng @gmail.com", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            // Kiểm tra Số điện thoại không được để trống và đúng định dạng đầu số của Việt Nam (10 đến 12 số)
            if (string.IsNullOrWhiteSpace(txtSDT.Text))
            {
                MessageBox.Show("Vui lòng nhập Số điện thoại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return false;
            }
            else if (!Regex.IsMatch(txtSDT.Text, @"^(0|\+84)[3|5|7|8|9][0-9]{8,11}$"))
            {
                MessageBox.Show("Số điện thoại phải từ 10 đến 12 số và đúng đầu số của Việt Nam", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return false;
            }

            // Kiểm tra Giới tính đã được chọn trong ComboBox
            if (cbxGioiTinh.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn Giới tính.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbxGioiTinh.Focus();
                return false;
            }

            // Kiểm tra Ngày sinh không được để trống (nếu cần ràng buộc)
            if (dtpNgaySinh.Value > DateTime.Now)
            {
                MessageBox.Show("Vui lòng nhập Ngày sinh hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNgaySinh.Focus();
                return false;
            }

            return true;
        }


        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmThemKhach_Load(object sender, EventArgs e)
        {

        }


    }
}
