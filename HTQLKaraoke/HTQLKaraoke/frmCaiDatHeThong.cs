﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace HTQLKaraoke
{
    public partial class frmCaiDatHeThong : Form
    {
        // Khởi tạo mã code mặc định cho từng chi nhánh
        private const string CodeCN001 = "nhoncao001";
        private const string CodeCN002 = "nhoncao002";

        public frmCaiDatHeThong()
        {
            InitializeComponent();
            LoadBranches();

            branchCodeLabel.Visible = false;
        }

        private void LoadBranches()
        {
            // Chuỗi kết nối đến Server chính (SERVERNC)
            string serverConnectionString = ConfigurationManager.ConnectionStrings["HTQLKaraoke.Properties.Settings.KaraokeConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(serverConnectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT TenChiNhanh, MaChiNhanh FROM ChiNhanh";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable branchTable = new DataTable();
                    adapter.Fill(branchTable);
                    if (branchTable.Rows.Count > 0)
                        {
                            // Lấy tên chi nhánh từ dòng đầu tiên
                            string tenChiNhanh = branchTable.Rows[0]["TenChiNhanh"].ToString();
                            string maChiNhanh = branchTable.Rows[0]["MaChiNhanh"].ToString();

                            // Gán giá trị vào TextBox
                            txtTenChiNhanh.Text = tenChiNhanh;
                            txtTenChiNhanh.Tag = maChiNhanh;
                        }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải chi nhánh: " + ex.Message);
                }
            }
        }


       

        private void btnShowBranchInfo_Click(object sender, EventArgs e)
        {
            string branchCode = txtTenChiNhanh.Tag.ToString();
            string expectedCode;

            // Xác định mã code dựa trên mã chi nhánh
            if (branchCode == "CN001")
            {
                expectedCode = CodeCN001;
            }
            else if (branchCode == "CN002")
            {
                expectedCode = CodeCN002;
            }
            else
            {
                MessageBox.Show("Chi nhánh không hợp lệ");
                return;
            }

            // Khởi tạo và hiển thị form thông tin chi nhánh
            frmThongTinChiNhanh frmInfo = new frmThongTinChiNhanh(branchCode, expectedCode);
            frmInfo.ShowDialog();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}