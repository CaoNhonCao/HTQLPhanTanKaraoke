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


namespace HTQLKaraoke.QLChiPhi
{
    public partial class frmSuaChiPhi : Form
    {
        string connection = ConfigurationManager.ConnectionStrings["HTQLKaraoke.Properties.Settings.KaraokeConnectionString"].ConnectionString;
        string maChiPhi;
        public frmSuaChiPhi(string maChiPhi)
        {
            InitializeComponent();
            this.maChiPhi = maChiPhi;

            LoadExpenseDetails(maChiPhi);
        }

        private void LoadExpenseDetails(string maChiPhi)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string query = "SELECT TenChiPhi, SoTien, NgayChi, GhiChu FROM ChiPhiKhac WHERE MaChiPhi = @MaChiPhi";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaChiPhi", maChiPhi);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtMaChiPhi.Text = maChiPhi; // Giữ nguyên mã chi phí không thay đổi
                    txtTenChiPhi.Text = reader["TenChiPhi"].ToString();
                    txtSoTien.Text = reader["SoTien"].ToString();
                    dtpNgayChi.Value = Convert.ToDateTime(reader["NgayChi"]);
                    txtGhiChu.Text = reader["GhiChu"].ToString();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy chi phí với mã này!");
                    this.Close();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenChiPhi.Text) ||
                string.IsNullOrEmpty(txtSoTien.Text) ||
                string.IsNullOrEmpty(txtGhiChu.Text))
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin!");
                return;
            }

            // Cập nhật chi phí vào cơ sở dữ liệu
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string query = "UPDATE ChiPhiKhac SET TenChiPhi = @TenChiPhi, SoTien = @SoTien, NgayChi = @NgayChi, GhiChu = @GhiChu WHERE MaChiPhi = @MaChiPhi";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaChiPhi", maChiPhi);
                cmd.Parameters.AddWithValue("@TenChiPhi", txtTenChiPhi.Text);
                cmd.Parameters.AddWithValue("@SoTien", decimal.Parse(txtSoTien.Text));
                cmd.Parameters.AddWithValue("@NgayChi", dtpNgayChi.Value);
                cmd.Parameters.AddWithValue("@GhiChu", txtGhiChu.Text);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Cập nhật chi phí thành công!");
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
