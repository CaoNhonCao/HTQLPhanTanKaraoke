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

namespace HTQLKaraoke.BaoTri
{
    public partial class frmEditBaoTri : Form
    {
        string connection = ConfigurationManager.ConnectionStrings["HTQLKaraoke.Properties.Settings.KaraokeConnectionString"].ConnectionString;
        private string maBaoTri;
        public frmEditBaoTri(string maBaoTri)
        {
            InitializeComponent();
            this.maBaoTri = maBaoTri;
            txtMaBaoTri.Text = maBaoTri;
            LoadData();
        }

        private void LoadData()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["HTQLKaraoke.Properties.Settings.KaraokeConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT MoTaBaoTri, ChiPhiBaoTri FROM LichSuBaoTri WHERE MaBaoTri = @MaBaoTri";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaBaoTri", maBaoTri);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        txtMoTa.Text = reader["MoTaBaoTri"] != DBNull.Value ? reader["MoTaBaoTri"].ToString() : string.Empty;
                        txtChiPhi.Text = reader["ChiPhiBaoTri"] != DBNull.Value ? reader["ChiPhiBaoTri"].ToString() : string.Empty;
                    }
                }
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            string moTa = txtMoTa.Text;
            decimal chiPhi;

            if (decimal.TryParse(txtChiPhi.Text, out chiPhi)) // Kiểm tra chi phí nhập vào có phải là số không
            {
                string connectionString = ConfigurationManager.ConnectionStrings["HTQLKaraoke.Properties.Settings.KaraokeConnectionString"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string updateQuery = "UPDATE LichSuBaoTri SET MoTaBaoTri = @MoTa, ChiPhiBaoTri = @ChiPhi WHERE MaBaoTri = @MaBaoTri";

                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MoTa", (object)moTa ?? DBNull.Value); 
                        cmd.Parameters.AddWithValue("@ChiPhi", chiPhi);
                        cmd.Parameters.AddWithValue("@MaBaoTri", maBaoTri);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Cập nhật thành công!");
                            this.DialogResult = DialogResult.OK; 
                            this.Close(); // Đóng form
                        }
                        else
                        {
                            MessageBox.Show("Cập nhật không thành công!");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Chi phí không hợp lệ.");
            }
        }

        private void frmEditBaoTri_Load(object sender, EventArgs e)
        {

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
