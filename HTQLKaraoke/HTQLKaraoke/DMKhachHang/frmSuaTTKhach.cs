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
using System.Configuration;
using System.Text.RegularExpressions;

namespace HTQLKaraoke.DMKhachHang
{
    public partial class frmSuaTTKhach : Form
    {
        private string customerId; // Mã khách hàng
        string connection = ConfigurationManager.ConnectionStrings["HTQLKaraoke.Properties.Settings.KaraokeConnectionString"].ConnectionString;
        public frmSuaTTKhach(string id)
        {
            InitializeComponent();
            SetupComboBox();
            customerId = id;
            LoadCustomerData(); // Gọi hàm để tải dữ liệu khách hàng

            toolTipbtn.SetToolTip(this.btnLuu, "Lưu Thông Tin");
            toolTipbtn.SetToolTip(this.btnHuy, "Thoát Trang");
        }

        private void SetupComboBox()
        {
            // Cài đặt lựa chọn cho ComboBox Giới tính
            cbxGioiTinh.Items.AddRange(new string[] { "Nam", "Nữ", "Khác" });
            cbxGioiTinh.SelectedIndex = 0;
            cbxGioiTinh.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void LoadCustomerData()
        {
            // Kết nối đến cơ sở dữ liệu và lấy thông tin khách hàng theo customerId
            string query = "SELECT * FROM KhachHang WHERE MaKhachHang = @Id";

            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", customerId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Gán giá trị vào các TextBox
                            txtHoTen.Text = reader["HoTen"].ToString();
                            txtDiaChi.Text = reader["DiaChi"].ToString();
                            txtEmail.Text = reader["Email"].ToString();
                            txtSDT.Text = reader["SoDienThoai"].ToString();
                            txtGhiChu.Text = reader["GhiChu"].ToString();
                            dtpNgaySinh.Value = Convert.ToDateTime(reader["NgaySinh"]);
                            cbxGioiTinh.Text = reader["GioiTinh"].ToString();
                            cbxGioiTinh.SelectedItem = reader["GioiTinh"].ToString();
                            txtMaKhach.Text = reader["MaKhachHang"].ToString();
                        }
                    }
                }
            }
        }

        // Sự kiện nhấn nút Lưu khi sửa khách hàng
        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Kiểm tra tính hợp lệ của dữ liệu nhập vào
            if (ValidateInput())
            {
                // Hiển thị hộp thoại xác nhận
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thay đổi thông tin khách hàng không?",
                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    // Lấy các thông tin để cập nhật
                    string hoTen = txtHoTen.Text.Trim();
                    string diaChi = txtDiaChi.Text.Trim();
                    string email = txtEmail.Text.Trim();
                    string soDienThoai = txtSDT.Text.Trim();
                    DateTime ngaySinh = dtpNgaySinh.Value;
                    string gioiTinh = cbxGioiTinh.SelectedItem.ToString();
                                        
                    try
                    {
                        using (SqlConnection conn = new SqlConnection(connection))
                        {
                            conn.Open();

                            // Cập nhật thông tin khách hàng và Ngày Cập Nhật
                            string updateQuery = "UPDATE KhachHang SET HoTen = @HoTen, DiaChi = @DiaChi, Email = @Email, SoDienThoai = @SoDienThoai, NgaySinh = @NgaySinh, GioiTinh = @GioiTinh, NgayCapNhat = @NgayCapNhat WHERE MaKhachHang = @Id";

                            using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                            {
                                updateCmd.Parameters.AddWithValue("@HoTen", hoTen);
                                updateCmd.Parameters.AddWithValue("@DiaChi", diaChi);
                                updateCmd.Parameters.AddWithValue("@Email", email);
                                updateCmd.Parameters.AddWithValue("@SoDienThoai", soDienThoai);
                                updateCmd.Parameters.AddWithValue("@NgaySinh", ngaySinh);
                                updateCmd.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                                updateCmd.Parameters.AddWithValue("@NgayCapNhat", DateTime.Now);
                                updateCmd.Parameters.AddWithValue("@Id", customerId);

                                // Thực thi câu lệnh cập nhật
                                updateCmd.ExecuteNonQuery();
                            }

                        }

                        MessageBox.Show("Cập nhật khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    catch (SqlException ex)
                    {
                        // Xử lý lỗi khi thực hiện truy vấn SQL
                        MessageBox.Show("Có lỗi xảy ra khi cập nhật khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        // Xử lý lỗi chung
                        MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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

            if (!string.IsNullOrWhiteSpace(txtEmail.Text) && !Regex.IsMatch(txtEmail.Text, @"^[\w-\.]+@gmail\.com$"))
            {
                MessageBox.Show("Email phải có định dạng @gmail.com", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtSDT.Text) && !Regex.IsMatch(txtSDT.Text, @"^(0|\+84)[3|5|7|8|9][0-9]{8,11}$"))
            {
                MessageBox.Show("Số điện thoại phải từ 10 đến 12 số và đúng đầu số của Việt Nam", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return false;
            }

            if (cbxGioiTinh.SelectedIndex != -1 && cbxGioiTinh.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn Giới tính hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbxGioiTinh.Focus();
                return false;
            }

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
    }
}
