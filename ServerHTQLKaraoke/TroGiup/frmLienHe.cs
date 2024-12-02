using System;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace ServerHTQLKaraoke.TroGiup
{
    public partial class frmLienHe : Form
    {
        public frmLienHe()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string fullName = txtFullName.Text;
            string email = txtEmail.Text;
            string phone = txtPhone.Text;
            string subject = txtSubject.Text;
            string description = txtDescription.Text;

            // Kiểm tra nếu các trường thông tin không rỗng
            if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(subject) ||
                string.IsNullOrWhiteSpace(description))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                return;
            }
            // Kiểm tra định dạng email
            if (!IsValidEmail(email))
            {
                MessageBox.Show("Địa chỉ email không hợp lệ.");
                return;
            }

            // Kiểm tra số điện thoại có đầu số Việt Nam
            if (!IsValidPhoneNumber(phone))
            {
                MessageBox.Show("Số điện thoại không hợp lệ. Vui lòng nhập đúng đầu số Việt Nam.");
                return;
            }
            // Hiển thị hộp thoại yêu cầu người dùng xác nhận có muốn gửi email không
            var result = MessageBox.Show("Bạn có muốn gửi thông tin qua email không?",
                                         "Xác nhận gửi email",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Mở ứng dụng email mặc định và điền đầy đủ thông tin vào đó
                try
                {
                    // URL chuẩn để mở ứng dụng email mặc định với các thông tin điền sẵn
                    string body = $"Họ tên: {txtFullName.Text}\nEmail: {txtEmail.Text}\nSố điện thoại: {txtPhone.Text}\n\nMô tả vấn đề:\n{txtDescription.Text}";

                    string mailtoLink = $"mailto:caomay2908@gmail.com?subject={Uri.EscapeDataString(subject)}&body={Uri.EscapeDataString(body)}";
                    System.Diagnostics.Process.Start(mailtoLink);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi mở ứng dụng email: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Yêu cầu chưa được gửi.");
            }
        }
        // Kiểm tra định dạng email bằng biểu thức chính quy
        private bool IsValidEmail(string email)
        {
            var emailRegex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
            return emailRegex.IsMatch(email);
        }

        // Kiểm tra số điện thoại có đầu số Việt Nam (đầu số +84 hoặc 0)
        private bool IsValidPhoneNumber(string phone)
        {
            var phoneRegex = new Regex(@"^(0|\+84)[0-9]{9,10}$");
            return phoneRegex.IsMatch(phone);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
