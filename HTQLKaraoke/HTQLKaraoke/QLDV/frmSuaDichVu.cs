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
    public partial class frmSuaDichVu : Form
    {
        string connection = ConfigurationManager.ConnectionStrings["HTQLKaraoke.Properties.Settings.KaraokeConnectionString"].ConnectionString;
        string maDichVu;
        public frmSuaDichVu(string maDichVu)
        {
            InitializeComponent();
            this.maDichVu = maDichVu;
        }

        private void frmSuaDichVu_Load(object sender, EventArgs e)
        {
            LoadThongTinDichVu();
        }
        private void LoadThongTinDichVu()
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string query = "SELECT TenDichVu, GiaDichVu, GhiChu FROM DichVu WHERE MaDichVu = @MaDichVu";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaDichVu", maDichVu);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtMaDichVu.Text = maDichVu;
                            txtTenDichVu.Text = reader["TenDichVu"].ToString();
                            txtGiaDichVu.Text = reader["GiaDichVu"].ToString();
                            txtGhiChu.Text = reader["GhiChu"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy dịch vụ.");
                            this.Close();
                        }
                    }
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtTenDichVu.Text.Length == 0 || txtTenDichVu.Text.Length > 20)
            {
                MessageBox.Show("Tên dịch vụ không được rỗng và không quá 20 ký tự.");
                return;
            }

            decimal giaDichVu;
            if (!decimal.TryParse(txtGiaDichVu.Text, out giaDichVu) || giaDichVu <= 0)
            {
                MessageBox.Show("Giá dịch vụ phải là số lớn hơn 0.");
                return;
            }

            // Cập nhật thông tin dịch vụ vào cơ sở dữ liệu
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();

                string updateDichVu = @"UPDATE DichVu SET TenDichVu = @TenDichVu, GiaDichVu = @GiaDichVu, GhiChu = @GhiChu, NgayCapNhat = GETDATE() WHERE MaDichVu = @MaDichVu";
                using (SqlCommand cmd = new SqlCommand(updateDichVu, conn))
                {
                    cmd.Parameters.AddWithValue("@MaDichVu", maDichVu);
                    cmd.Parameters.AddWithValue("@TenDichVu", txtTenDichVu.Text);
                    cmd.Parameters.AddWithValue("@GiaDichVu", giaDichVu);
                    cmd.Parameters.AddWithValue("@GhiChu", txtGhiChu.Text);

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Cập nhật dịch vụ thành công!");
            this.Close();
        }

        private void btnHuy_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
