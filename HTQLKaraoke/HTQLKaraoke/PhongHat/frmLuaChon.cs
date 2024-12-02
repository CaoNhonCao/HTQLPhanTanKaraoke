using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HTQLKaraoke.PhongHat
{
    public partial class frmLuaChon : Form
    {
        public frmLuaChon()
        {
            InitializeComponent();
        }
        private void btnDatPhong_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes; // Trả về Yes nếu chọn Đặt phòng
            this.Close();
        }

        private void btnDungPhongNgay_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No; // Trả về No nếu chọn Dùng phòng ngay
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel; // Trả về Cancel nếu chọn Hủy
            this.Close();
        }
    }
}
