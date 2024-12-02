﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServerHTQLKaraoke.ThongKe
{
    public partial class frmXemChiTiet : Form
    {
        string connection = ConfigurationManager.ConnectionStrings["ServerHTQLKaraoke.Properties.Settings.KaraokeConnectionString"].ConnectionString;
        public frmXemChiTiet()
        {
            InitializeComponent();
        }

        private void frmXemChiTiet_Load(object sender, EventArgs e)
        {
            LoadDoanhThu();
            LoadChiNhanh();
            LoadThang();
            LoadNam();
        }
        private void LoadThang()
        {
            cbxThang.Items.Clear();
            cbxThang.Items.Add("Tất cả");
            for (int i = 1; i <= 12; i++)
            {
                cbxThang.Items.Add(i.ToString());
            }
            cbxThang.SelectedIndex = 0;
        }

        private void LoadNam()
        {
            string query = "SELECT DISTINCT YEAR(Thang) AS Nam FROM DoanhThu";

            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                cbxNam.Items.Clear();
                cbxNam.Items.Add("Tất cả");
                while (reader.Read())
                {
                    cbxNam.Items.Add(reader["Nam"].ToString());
                }
                cbxNam.SelectedIndex = 0;
            }
        }
        private void LoadChiNhanh()
        {
            string query = "SELECT TenChiNhanh FROM ChiNhanh";

            using (SqlConnection conn = new SqlConnection(connection))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    cbxChiNhanh.Items.Clear();
                    cbxChiNhanh.Items.Add("Tất cả"); // Thêm mục "Tất cả"
                    while (reader.Read())
                    {
                        cbxChiNhanh.Items.Add(reader["TenChiNhanh"].ToString());
                    }

                    cbxChiNhanh.SelectedIndex = 0; // Chọn mục "Tất cả" mặc định
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải danh sách chi nhánh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadDoanhThu(string chiNhanh = "Tất cả", string thang = "Tất cả", string nam = "Tất cả")
        {
            string query = @"
                SELECT 
                    DoanhThu.MaDoanhThu, 
                    DoanhThu.Thang, 
                    DoanhThu.TongDoanhThu, 
                    DoanhThu.TongChiPhi, 
                    DoanhThu.LoiNhuan, 
                    ChiNhanh.TenChiNhanh,
                    DoanhThu.GhiChu
                FROM 
                    DoanhThu 
                INNER JOIN 
                    ChiNhanh 
                ON 
                    DoanhThu.MaChiNhanh = ChiNhanh.MaChiNhanh
                WHERE 1 = 1";

            if (chiNhanh != "Tất cả")
                query += " AND ChiNhanh.TenChiNhanh = @TenChiNhanh";
            if (thang != "Tất cả")
                query += " AND MONTH(DoanhThu.Thang) = @Thang";
            if (nam != "Tất cả")
                query += " AND YEAR(DoanhThu.Thang) = @Nam";

            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);

                if (chiNhanh != "Tất cả")
                    cmd.Parameters.AddWithValue("@TenChiNhanh", chiNhanh);
                if (thang != "Tất cả")
                    cmd.Parameters.AddWithValue("@Thang", thang);
                if (nam != "Tất cả")
                    cmd.Parameters.AddWithValue("@Nam", nam);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dtgDoanhThu.DataSource = dataTable;

                dtgDoanhThu.Columns["MaDoanhThu"].HeaderText = "Mã Doanh Thu";
                dtgDoanhThu.Columns["Thang"].HeaderText = "Tháng";
                dtgDoanhThu.Columns["TongDoanhThu"].HeaderText = "Tổng Doanh Thu";
                dtgDoanhThu.Columns["TongChiPhi"].HeaderText = "Tổng Chi Phí";
                dtgDoanhThu.Columns["LoiNhuan"].HeaderText = "Lợi Nhuận";
                dtgDoanhThu.Columns["TenChiNhanh"].HeaderText = "Tên Chi Nhánh";
                dtgDoanhThu.Columns["GhiChu"].HeaderText = "Ghi Chú";
                dtgDoanhThu.Columns["GhiChu"].Width = 200;

                UpdateTotals(dataTable);
            }
        }
       private void UpdateTotals(DataTable dataTable)
{
    // Kiểm tra nếu bảng dữ liệu không phải null và không trống
    if (dataTable != null && dataTable.Rows.Count > 0)
    {
        decimal tongDoanhThu = 0;
        decimal tongChiPhi = 0;
        decimal loiNhuan = 0;

        // Kiểm tra nếu chi nhánh là "Tất cả"
        if (cbxChiNhanh.SelectedItem != null && cbxChiNhanh.SelectedItem.ToString() == "Tất cả")
        {
            // Duyệt qua tất cả các dòng và cộng tổng lại
            foreach (DataRow row in dataTable.Rows)
            {
                tongDoanhThu += Convert.ToDecimal(row["TongDoanhThu"]);
                tongChiPhi += Convert.ToDecimal(row["TongChiPhi"]);
                loiNhuan += Convert.ToDecimal(row["LoiNhuan"]);
            }
        }
        else
        {
            // Kiểm tra và lấy dòng cuối cùng nếu bảng có dữ liệu
            if (dataTable.Rows.Count > 0)
            {
                DataRow lastRow = dataTable.Rows[dataTable.Rows.Count - 1];
                tongDoanhThu = Convert.ToDecimal(lastRow["TongDoanhThu"]);
                tongChiPhi = Convert.ToDecimal(lastRow["TongChiPhi"]);
                loiNhuan = Convert.ToDecimal(lastRow["LoiNhuan"]);
            }
        }

        // Hiển thị kết quả
        txtTongDoanhThu.Text = tongDoanhThu.ToString("C0", CultureInfo.CreateSpecificCulture("vi-VN"));
        txtTongChiPhi.Text = tongChiPhi.ToString("C0", CultureInfo.CreateSpecificCulture("vi-VN"));
        txtLoiNhuan.Text = loiNhuan.ToString("C0", CultureInfo.CreateSpecificCulture("vi-VN"));
    }
    else
    {
        // Nếu bảng không có dữ liệu hoặc là null, hiển thị giá trị mặc định
        txtTongDoanhThu.Text = "0";
        txtTongChiPhi.Text = "0";
        txtLoiNhuan.Text = "0";
    }
}

        private void cbxChiNhanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void cbxThang_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void cbxNam_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            string chiNhanh = cbxChiNhanh.SelectedItem != null ? cbxChiNhanh.SelectedItem.ToString() : "Tất cả";
            string thang = cbxThang.SelectedItem != null ? cbxThang.SelectedItem.ToString() : "Tất cả";
            string nam = cbxNam.SelectedItem != null ? cbxNam.SelectedItem.ToString() : "Tất cả";

            LoadDoanhThu(chiNhanh, thang, nam);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDoanhThu();
        }
    }
}