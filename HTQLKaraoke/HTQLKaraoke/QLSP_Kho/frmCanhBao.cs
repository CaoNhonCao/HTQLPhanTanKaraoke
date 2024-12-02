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
using HTQLKaraoke.NhapHang;

namespace HTQLKaraoke.QLSP_Kho
{
    public partial class frmCanhBao : Form
    {
        string connection = ConfigurationManager.ConnectionStrings["HTQLKaraoke.Properties.Settings.KaraokeConnectionString"].ConnectionString;
        private string selectedProductCode; 
        public frmCanhBao()
        {
            InitializeComponent();
        }

        private void frmCanhBao_Load(object sender, EventArgs e)
        {
            LoadCanhBao();
        }
        private void LoadCanhBao()
        {
            int mucCanhBao = 10;

            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string query = @"
            SELECT sp.MaSanPham, sp.TenSanPham, qlk.SoLuongTon
            FROM QuanLyKho qlk
            JOIN SanPham sp ON qlk.MaSanPham = sp.MaSanPham
            WHERE qlk.SoLuongTon < @MucCanhBao";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MucCanhBao", mucCanhBao);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Hiển thị kết quả lên DataGridView (dgvCanhBao)
                    dtgCanhBao.DataSource = dataTable;
                    dtgCanhBao.Columns["MaSanPham"].HeaderText = "Mã sản phẩm";
                    dtgCanhBao.Columns["TenSanPham"].HeaderText = "Tên sản phẩm";
                    dtgCanhBao.Columns["SoLuongTon"].HeaderText = "Số lượng tồn";
                    dtgCanhBao.AllowUserToAddRows = false;
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNhapHang_Click(object sender, EventArgs e)
        {

            // Tạo danh sách mã sản phẩm và truyền vào form Nhập Hàng
            List<string> selectedProductCodes = new List<string> { selectedProductCode };
            frmNhapHang frm = new frmNhapHang(selectedProductCodes);
            frm.FormClosed += (s, args) =>
            {
                LoadCanhBao();
            };
            frm.ShowDialog();
        }


        private void dtgCanhBao_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                selectedProductCode = dtgCanhBao.Rows[e.RowIndex].Cells["MaSanPham"].Value.ToString();
                if (!string.IsNullOrEmpty(selectedProductCode))
                {
                    btnNhapHang.Enabled = true;
                }
                else
                {
                    btnNhapHang.Enabled = false;
                }
            }
        }
    }
}
